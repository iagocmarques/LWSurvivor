using System.Collections.Generic;
using System.Text;
using Project.Core.Tick;
using Project.Data;
using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Gameplay.Player
{
    public sealed class PlayerHsmController : MonoBehaviour, ITickable
    {
        private enum SuperState
        {
            Locomotion = 0, Dash = 1, Attack = 2, Hitstun = 3,
            Defending = 4, DefendBreak = 5, HurtGrounded = 6, HurtAir = 7,
            Lying = 8, GetUp = 9, Dead = 10, Caught = 11
        }

        [SerializeField] private LayerMask enemyHurtboxMask = ~0;
        [SerializeField] private LayerMask itemMask;
        [SerializeField] private int projectileLayer = -1;

        private Lf2InputScheme _lf2Input;
        private Lf2InputScheme.Lf2InputState _currentInput;
        private Lf2StateMachine _lf2Sm;
        private Lf2OpointProcessor _opointProcessor;
        private readonly CombatInputBuffer _combatBuffer = new CombatInputBuffer();
        private SuperState _superState;
        private Vector2 _lastMoveDir = Vector2.right;
        private CharacterMovementConfig _movementConfig;
        private DamageReactionRouter _reactionRouter;
        private ReactiveStateId _currentReactiveState;
        private int _reactiveFrameIndex;
        private int _reactiveFrameTickCounter;
        private int _lyingTimerTicks;
        private Vector2 _reactiveVelocity;
        private HitResult _lastHitResult;
        private Vector2 _knockbackVel;
        private StatusEffectInstance _statusEffect;
        private Color _originalTint = Color.white;
        private SpriteRenderer _cachedSR;
        private Health _cachedHealth;
        private CharacterMotor _motor;
        private Mana _cachedMana;
        private StatusEffectTuning _statusEffectTuning;
        private System.Action<int> _burnDamageCallback;
        private float _invulnLeft;

        public long CurrentTick { get; private set; }
        public bool FacingRight => _lastMoveDir.x >= 0f;
        public bool IsInvulnerable => _invulnLeft > 0f;
        public bool IsMoving => _superState == SuperState.Locomotion && _motor != null && _motor.PlanarVelocity.sqrMagnitude > 0.001f;
        public bool IsAttacking => _superState == SuperState.Attack;
        public bool IsDefending => _superState == SuperState.Defending;
        public bool IsDefendHeld => _currentInput.DefendHeld;
        public bool IsDefendHit => _superState == SuperState.Defending && _currentReactiveState == ReactiveStateId.DefendHit;
        public bool IsCaught => _superState == SuperState.Caught;
        public bool IsDefendBreak => _superState == SuperState.DefendBreak;
        public bool IsHurtGrounded => _superState == SuperState.HurtGrounded;
        public bool IsHurtAir => _superState == SuperState.HurtAir;
        public bool IsLying => _superState == SuperState.Lying;
        public bool IsGetUp => _superState == SuperState.GetUp;
        public bool IsDead => _superState == SuperState.Dead;
        public ReactiveStateId CurrentReactiveState => _currentReactiveState;
        public int ReactiveFrameIndex => _reactiveFrameIndex;
        public HitResult LastHitResult => _lastHitResult;
        public StatusEffectInstance ActiveStatusEffect => _statusEffect;
        public Lf2StateMachine Lf2Sm => _lf2Sm;
        public int Lf2CurrentPic => _lf2Sm?.CurrentPic ?? -1;
        public int Lf2CurrentFrameId => _lf2Sm?.CurrentFrameId ?? -1;
        public bool HasLf2Sm => _lf2Sm != null;
        public bool IsLf2Airborne => _lf2Sm != null && _lf2Sm.IsAirborne;

        public CombatAttackId ActiveAttackId
        {
            get
            {
                if (_lf2Sm == null || _superState != SuperState.Attack)
                    return CombatAttackId.None;
                int startId = _lf2Sm.AttackStartFrameId;
                if (startId >= 0 && startId == _lf2Sm.Roles.AttackForward)
                    return CombatAttackId.Launcher;
                if (startId >= 0 && startId == _lf2Sm.Roles.AttackBack)
                    return CombatAttackId.DashAttack;
                return CombatAttackId.Jab;
            }
        }

        public int ActiveAttackFrameIndex => _lf2Sm?.AttackFrameIndex ?? 0;

        private bool IsInReactiveChain => (_superState >= SuperState.DefendBreak && _superState <= SuperState.Dead)
            || (_superState == SuperState.Defending && _currentReactiveState == ReactiveStateId.DefendHit);

        private void Awake()
        {
            _lf2Input = new Lf2InputScheme();
            _superState = SuperState.Locomotion;
            enabled = false;
        }

        public void SetInputScheme(Lf2InputScheme scheme)
        {
            _lf2Input?.Dispose();
            _lf2Input = scheme;
        }

        public void Configure(
            CharacterDefinition definition,
            Lf2CharacterData lf2CharacterData = null,
            Dictionary<int, Lf2CharacterData> projectileDataMap = null,
            DamageReactionRouter reactionRouter = null,
            StatusEffectTuning statusEffectTuning = null)
        {
            if (definition == null) { enabled = false; return; }
            _movementConfig = definition.movement;
            _reactionRouter = reactionRouter;
            _statusEffectTuning = statusEffectTuning;
            _cachedSR = GetComponent<SpriteRenderer>();
            _cachedHealth = GetComponent<Health>();
            _motor = GetComponent<CharacterMotor>();
            _cachedMana = GetComponent<Mana>();
            _burnDamageCallback = dmg =>
            {
                if (_cachedHealth != null) _cachedHealth.ApplyDamage(dmg);
                if (_cachedHealth != null && _cachedHealth.IsDead && _currentReactiveState != ReactiveStateId.Dead)
                    EnterReactiveState(ReactiveStateId.Dead, default);
            };
            if (_motor != null) _motor.Configure(_movementConfig.gravity);

            if (lf2CharacterData?.Frames != null && lf2CharacterData.Frames.Count > 0)
            {
                _lf2Sm = new Lf2StateMachine();
                _lf2Sm.Initialize(lf2CharacterData, 0f);
                var itr = new Lf2ItrProcessor();
                itr.Initialize(_lf2Sm, transform, enemyHurtboxMask);
                itr.SetItemMask(itemMask);
                _lf2Sm.SetItrProcessor(itr);
                _lf2Sm.InitializeGrabProcessor();
                if (projectileDataMap != null && projectileDataMap.Count > 0)
                {
                    var opoint = new Lf2OpointProcessor();
                    opoint.Initialize(transform, enemyHurtboxMask, projectileDataMap, projectileLayer);
                    _lf2Sm.SetOpointProcessor(opoint);
                    _lf2Sm.SetOwnerTransform(transform);
                    _opointProcessor = opoint;
                }
                _lf2Sm.SetManaSpendCallback(cost => _cachedMana != null && _cachedMana.Spend(cost));
            }
            enabled = true;
        }

        private void OnEnable() => FixedTickSystem.Register(this);
        private void OnDisable() => FixedTickSystem.Unregister(this);
        private void OnDestroy() { _lf2Input?.Dispose(); _lf2Input = null; }

        public void Tick(in TickContext context)
        {
            CurrentTick = context.Tick;
            var dt = context.FixedDelta;
            _currentInput = _lf2Input?.Read() ?? default;
            if (_invulnLeft > 0f) _invulnLeft = Mathf.Max(0f, _invulnLeft - dt);

            _opointProcessor?.TickAll();

            if (IsInReactiveChain) { TickReactiveState(in context, dt); return; }

            if (_lf2Sm != null)
            {
                _lf2Sm.ProcessInput(in _currentInput, FacingRight, Time.time);
                _lf2Sm.Tick();
                SyncSuperStateFromLf2();
            }

            if (_superState == SuperState.Locomotion)
            {
                var move = _currentInput.MoveDir;
                if (move.sqrMagnitude > 0.0001f) _motor.ApplyMovementInput(move);
                else _motor?.StopMovement();
            }
            else if (_superState == SuperState.Caught)
            {
                // Position managed by Lf2GrabProcessor.SyncVictimPosition — skip LF2 velocity sync
            }
            else if (_lf2Sm != null && _motor != null)
            {
                var pos = transform.position;
                _lf2Sm.SetPosition(ref pos);
                _motor.SetPosition(pos);
            }

            if (_statusEffect.IsActive)
                StatusEffectProcessor.Tick(ref _statusEffect, _cachedSR, _originalTint, _burnDamageCallback);
            if (_currentInput.MoveDir.sqrMagnitude > 0.0001f) _lastMoveDir = _currentInput.MoveDir.normalized;
        }

        private void SyncSuperStateFromLf2()
        {
            switch (_lf2Sm.CurrentState)
            {
                case Lf2State.Standing: case Lf2State.Walking: case Lf2State.Running:
                case Lf2State.Jumping: case Lf2State.Dashing:
                case Lf2State.Falling: case Lf2State.FallingAlt:
                    _superState = SuperState.Locomotion; break;
                case Lf2State.Attacking: case Lf2State.Catching:
                    _superState = SuperState.Attack; break;
                case Lf2State.Defending: _superState = SuperState.Defending; break;
                case Lf2State.Caught: _superState = SuperState.Caught; break;
                case Lf2State.BrokenDefend: _superState = SuperState.DefendBreak; break;
                case Lf2State.Injured: case Lf2State.Ice: case Lf2State.Fire:
                    _superState = SuperState.Hitstun; break;
                case Lf2State.Lying: _superState = SuperState.Lying; break;
            }
        }

        public void ApplyReactiveHit(in HitResult hit)
        {
            _lastHitResult = hit;
            if (IsInvulnerable) return;
            if (hit.Effect != StatusEffect.None)
            {
                if (_cachedSR != null) _originalTint = _cachedSR.color;
                _statusEffect = StatusEffectProcessor.ApplyFromTuning(hit.Effect, _statusEffectTuning);
            }
            if (_reactionRouter != null)
            {
                bool isDef = _superState == SuperState.Defending;
                bool isAir = _superState == SuperState.HurtAir || (_lf2Sm != null && _lf2Sm.IsAirborne);
                EnterReactiveState(_reactionRouter.Evaluate(in hit, isDef, isAir), in hit);
            }
        }

        private void EnterReactiveState(ReactiveStateId stateId, in HitResult hit)
        {
            _currentReactiveState = stateId;
            _reactiveFrameIndex = 0;
            _reactiveFrameTickCounter = 0;
            _lyingTimerTicks = 0;
            switch (stateId)
            {
                case ReactiveStateId.Defend:
                    _superState = SuperState.Defending; break;
                case ReactiveStateId.DefendBreak:
                    _superState = SuperState.DefendBreak;
                    SetKnockback(hit.Knockback, hit.AttackerFacing, 1f); break;
                case ReactiveStateId.DefendHit:
                    _superState = SuperState.Defending;
                    SetKnockback(hit.Knockback, hit.AttackerFacing, 0.3f); break;
                case ReactiveStateId.HurtGrounded:
                    _superState = SuperState.HurtGrounded;
                    SetKnockback(hit.Knockback, hit.AttackerFacing, 1f); break;
                case ReactiveStateId.HurtAir:
                    _superState = SuperState.HurtAir;
                    _knockbackVel = Vector2.zero;
                    _reactiveVelocity = new Vector2(
                        (hit.AttackerFacing >= 0 ? -1f : 1f) * Mathf.Abs(hit.Knockback.x) * 0.2f, 0f);
                    _motor?.Launch(hit.Knockback.y * 0.25f); break;
                case ReactiveStateId.Lying: _superState = SuperState.Lying; break;
                case ReactiveStateId.GetUp:
                    _superState = SuperState.GetUp;
                    _invulnLeft = _movementConfig.invulnOnGetUpTicks / 60f; break;
                case ReactiveStateId.Dead: _superState = SuperState.Dead; break;
            }
        }

        private void SetKnockback(Vector2 knockback, int attackerFacing, float scale)
        {
            _knockbackVel = new Vector2(
                (attackerFacing >= 0 ? -1f : 1f) * knockback.x * scale * 0.2f, 0f);
        }

        private void TickReactiveState(in TickContext ctx, float dt)
        {
            if (_statusEffect.IsActive)
                StatusEffectProcessor.Tick(ref _statusEffect, _cachedSR, _originalTint, _burnDamageCallback);

            var move = _reactionRouter?.GetMove(_currentReactiveState);
            if (move?.frames == null || move.frames.Length == 0) { _superState = SuperState.Locomotion; return; }

            switch (_currentReactiveState)
            {
                case ReactiveStateId.Defend:
                    AdvanceReactiveFrame(dt, move);
                    if (!_currentInput.DefendHeld)
                    { _superState = SuperState.Locomotion; _currentReactiveState = ReactiveStateId.HurtGrounded; }
                    break;
                case ReactiveStateId.DefendHit:
                    AdvanceReactiveFrame(dt, move);
                    if (_reactiveFrameIndex >= move.frames.Length)
                    {
                        _superState = _currentInput.DefendHeld ? SuperState.Defending : SuperState.Locomotion;
                        _currentReactiveState = _currentInput.DefendHeld ? ReactiveStateId.Defend : ReactiveStateId.HurtGrounded;
                        _reactiveFrameIndex = 0; _reactiveFrameTickCounter = 0;
                    }
                    break;
                case ReactiveStateId.DefendBreak: case ReactiveStateId.HurtGrounded: case ReactiveStateId.GetUp:
                    TickReactiveSequence(dt, move); break;
                case ReactiveStateId.HurtAir:
                    if (_reactiveVelocity.x != 0f && _motor != null)
                    {
                        _motor.SetHorizontalVelocity(_reactiveVelocity.x);
                        _reactiveVelocity.x = Mathf.MoveTowards(_reactiveVelocity.x, 0f, dt * 5f);
                    }
                    if (_motor != null && _motor.IsGrounded)
                    { _reactiveVelocity = Vector2.zero; EnterReactiveState(ReactiveStateId.Lying, default); return; }
                    AdvanceReactiveFrame(dt, move);
                    if (_reactiveFrameIndex >= move.frames.Length && move.loop)
                    { _reactiveFrameIndex = 0; _reactiveFrameTickCounter = 0; }
                    break;
                case ReactiveStateId.Lying:
                    _lyingTimerTicks++;
                    if (_lyingTimerTicks >= _movementConfig.lyingDurationTicks)
                        EnterReactiveState(ReactiveStateId.GetUp, default);
                    break;
                case ReactiveStateId.Dead: break;
            }

            if (_knockbackVel.sqrMagnitude > 0.001f)
            {
                _motor?.SetHorizontalVelocity(_knockbackVel.x);
                _knockbackVel = Vector2.Lerp(_knockbackVel, Vector2.zero, dt * 5f);
            }
        }

        private void TickReactiveSequence(float dt, ReactiveMoveDefinition move)
        {
            AdvanceReactiveFrame(dt, move);
            if (_reactiveFrameIndex >= move.frames.Length)
            {
                if (move.loop) { _reactiveFrameIndex = 0; _reactiveFrameTickCounter = 0; }
                else
                {
                    if (move.stateId == ReactiveStateId.GetUp && move.nextStateOnFinish == ReactiveStateId.GetUp)
                    { _superState = SuperState.Locomotion; _currentReactiveState = ReactiveStateId.HurtGrounded; return; }
                    switch (move.nextStateOnFinish)
                    {
                        case ReactiveStateId.GetUp: EnterReactiveState(ReactiveStateId.GetUp, default); break;
                        case ReactiveStateId.Lying: EnterReactiveState(ReactiveStateId.Lying, default); break;
                        default: _superState = SuperState.Locomotion; _currentReactiveState = ReactiveStateId.HurtGrounded; break;
                    }
                }
            }
        }

        private void AdvanceReactiveFrame(float dt, ReactiveMoveDefinition move)
        {
            if (move.frames == null || _reactiveFrameIndex >= move.frames.Length) return;
            if (_reactiveFrameTickCounter == 0) ApplyFrameEffects(move.frames[_reactiveFrameIndex]);
            _reactiveFrameTickCounter++;
            if (_reactiveFrameTickCounter >= move.frames[_reactiveFrameIndex].durationTicks)
            {
                _reactiveFrameIndex++; _reactiveFrameTickCounter = 0;
                if (_reactiveFrameIndex < move.frames.Length) ApplyFrameEffects(move.frames[_reactiveFrameIndex]);
            }
        }

        private void ApplyFrameEffects(ReactiveFrameDefinition frame)
        {
            if (frame.impulse.sqrMagnitude > 0.001f)
            {
                var f = FacingRight ? 1f : -1f;
                _reactiveVelocity += new Vector2(frame.impulse.x * f, frame.impulse.y) * 0.1f;
            }
            if (frame.invulnerable) _invulnLeft = frame.durationTicks / 60f;
        }

        public void GetCombatDebugHudText(StringBuilder sb, List<CombatInputDebugEntry> ringScratch, int ringLines)
        {
            sb.Clear();
            sb.Append("Tick: ").Append(CurrentTick).AppendLine();
            sb.Append("Super: ").Append(_superState).AppendLine();
            if (_superState == SuperState.Attack)
                sb.Append("LF2 Frame: ").Append(Lf2CurrentFrameId).Append(" Pic: ").Append(Lf2CurrentPic).AppendLine();
            if (IsInReactiveChain)
            {
                sb.Append("Reactive: ").Append(_currentReactiveState).Append(" | frame ").Append(_reactiveFrameIndex).AppendLine();
                if (_lastHitResult.Damage > 0)
                    sb.Append("LastHit: dmg=").Append(_lastHitResult.Damage)
                      .Append(" kb=").Append(_lastHitResult.Knockback)
                      .Append(" flags=").Append(_lastHitResult.Flags).AppendLine();
            }
            sb.AppendLine("Buffer (FIFO, ≤8 ticks):");
            var buf = _combatBuffer.BufferedInputs;
            if (buf.Count == 0) sb.AppendLine("  (vazio)");
            else for (var i = 0; i < buf.Count; i++)
            { var e = buf[i]; sb.Append("  ").Append(e.AttackId).Append(" @ tick ").Append(e.PressedTick).AppendLine(); }
            sb.AppendLine("Ring debug (últimos):");
            _combatBuffer.GetRecentDebugEntries(ringLines, ringScratch);
            if (ringScratch.Count == 0) sb.AppendLine("  (nenhum)");
            else for (var i = 0; i < ringScratch.Count; i++)
            {
                var d = ringScratch[i];
                sb.Append("  t=").Append(d.Tick).Append(' ').Append(d.AttackId);
                if (!string.IsNullOrEmpty(d.Label)) sb.Append(" — ").Append(d.Label);
                sb.AppendLine();
            }
        }
    }
}
