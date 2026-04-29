using System.Collections.Generic;
using System.Text;
using Project.Core.Tick;
using Project.Gameplay.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Player
{
    public sealed class PlayerHsmController : MonoBehaviour, ITickable
    {
        private enum SuperState
        {
            Locomotion = 0,
            Dash = 1,
            Attack = 2,
            Hitstun = 3,
            Defending = 4,
            DefendBreak = 5,
            HurtGrounded = 6,
            HurtAir = 7,
            Lying = 8,
            GetUp = 9,
            Dead = 10
        }

        private enum LocomotionSubState
        {
            Idle = 0,
            Move = 1
        }

        [SerializeField] private PlayerTuning tuning;
        [Header("Combat (data-driven)")]
        [SerializeField] private AttackDefinition jabDefinition;
        [SerializeField] private AttackDefinition launcherDefinition;
        [SerializeField] private AttackDefinition dashAttackDefinition;
        [SerializeField] private LayerMask enemyHurtboxMask = ~0;

        private InputAction moveAction;
        private InputAction dashAction;
        private InputAction attackAction;
        private InputAction launcherAction;
        private InputAction defendAction;

        private readonly CombatInputBuffer _combatBuffer = new CombatInputBuffer();

        private SuperState superState;
        private LocomotionSubState locomotionSubState;

        private Vector2 moveInput;
        private Vector2 lastMoveDir = Vector2.right;

        private float dashTimeLeft;
        private float dashCooldownLeft;
        private float invulnLeft;
        private Vector2 dashDir;

        private CombatAttackId currentAttackId = CombatAttackId.None;
        private int attackFrameIndex;

        private float hitstunLeft;
        private Vector2 knockbackVel;

        private CombatHitbox _activeHitbox;
        private PlayerRuntimeStats _runtimeStats;

        // Reactive combat
        private DamageReactionRouter _reactionRouter;
        private ReactiveMoveSet _reactiveMoves;
        private ReactiveStateId _currentReactiveState;
        private int _reactiveFrameIndex;
        private int _reactiveFrameTickCounter;
        private int _lyingTimerTicks;
        private Vector2 _reactiveVelocity; // for HurtAir gravity
        private HitResult _lastHitResult;  // for debug overlay

        // Status effect
        private StatusEffectInstance _statusEffect;
        private Color _originalTint = Color.white;

        // Cached references (avoid GetComponent per-tick)
        private SpriteRenderer _cachedSR;
        private Health _cachedHealth;
        private CharacterMotor _motor;
        private StatusEffectTuning _statusEffectTuning;

        // Cached burn damage delegate (avoids lambda allocation per-tick)
        private System.Action<int> _burnDamageCallback;

        public long CurrentTick { get; private set; }

        public bool IsInvulnerable => invulnLeft > 0f;
        public bool IsMoving => superState == SuperState.Locomotion && locomotionSubState == LocomotionSubState.Move;
        public bool IsAttacking => superState == SuperState.Attack;
        public CombatAttackId ActiveAttackId => currentAttackId;
        public int ActiveAttackFrameIndex => attackFrameIndex;
        public bool FacingRight => lastMoveDir.x >= 0f;

        public bool IsDefendHeld => defendAction != null && defendAction.IsPressed();

        // Reactive combat state queries (for sprite animator and debug overlay)
        public bool IsDefending => superState == SuperState.Defending;
        public bool IsDefendBreak => superState == SuperState.DefendBreak;
        public bool IsHurtGrounded => superState == SuperState.HurtGrounded;
        public bool IsHurtAir => superState == SuperState.HurtAir;
        public bool IsLying => superState == SuperState.Lying;
        public bool IsGetUp => superState == SuperState.GetUp;
        public bool IsDead => superState == SuperState.Dead;

        public ReactiveStateId CurrentReactiveState => _currentReactiveState;
        public int ReactiveFrameIndex => _reactiveFrameIndex;
        public HitResult LastHitResult => _lastHitResult;
        public StatusEffectInstance ActiveStatusEffect => _statusEffect;

        private void Awake()
        {
            // Always create input actions so they're ready when Configure() enables us.
            // This is critical for the stale-serialized-data case: if tuning is a
            // destroyed SO reference (null at runtime), Awake would previously bail
            // early, leaving input actions uninitialized. Then Configure() enables the
            // component, OnEnable fires, but Tick has no actions to read.
            moveAction = new InputAction("Move", InputActionType.Value, expectedControlType: "Vector2");
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
            moveAction.AddBinding("<Gamepad>/leftStick");
            moveAction.Enable();

            dashAction = new InputAction("Dash", InputActionType.Button);
            dashAction.AddBinding("<Keyboard>/leftShift");
            dashAction.AddBinding("<Keyboard>/rightShift");
            dashAction.AddBinding("<Gamepad>/buttonSouth");
            dashAction.Enable();

            attackAction = new InputAction("Attack", InputActionType.Button);
            attackAction.AddBinding("<Keyboard>/j");
            attackAction.AddBinding("<Keyboard>/space");
            attackAction.AddBinding("<Gamepad>/buttonWest");
            attackAction.Enable();

            launcherAction = new InputAction("Launcher", InputActionType.Button);
            launcherAction.AddBinding("<Keyboard>/k");
            launcherAction.AddBinding("<Gamepad>/buttonNorth");
            launcherAction.Enable();

            defendAction = new InputAction("Defend", InputActionType.Button);
            defendAction.AddBinding("<Keyboard>/l");
            defendAction.AddBinding("<Gamepad>/buttonEast");
            defendAction.Enable();

            superState = SuperState.Locomotion;
            locomotionSubState = LocomotionSubState.Idle;
            _runtimeStats = GetComponent<PlayerRuntimeStats>();

            // If tuning is not yet configured, disable until Configure() is called.
            // Input actions are ready; they just won't be read while disabled.
            if (tuning == null)
                enabled = false;
        }

        /// <summary>
        /// Wires runtime-created SO instances into the private serialized fields.
        /// Call after Awake (e.g. from GameRuntimeInstaller) to bootstrap the
        /// player without pre-authored .asset files.
        /// </summary>
        public void Configure(
            PlayerTuning tuningData,
            AttackDefinition jab,
            AttackDefinition launcher,
            AttackDefinition dash,
            ReactiveMoveSet reactiveMoves = null)
        {
            tuning = tuningData;
            jabDefinition = jab;
            launcherDefinition = launcher;
            dashAttackDefinition = dash;
            _reactiveMoves = reactiveMoves;

            _cachedSR = GetComponent<SpriteRenderer>();
            _cachedHealth = GetComponent<Health>();
            _motor = GetComponent<CharacterMotor>();
            _statusEffectTuning = tuningData != null ? tuningData.statusEffectTuning : null;

            if (_motor != null && tuningData != null)
                _motor.Configure(tuningData.gravityPerTick);

            if (tuningData != null && reactiveMoves != null && _cachedHealth != null)
                _reactionRouter = new DamageReactionRouter(tuningData, reactiveMoves, _cachedHealth);

            // Cache burn damage delegate to avoid per-tick lambda allocation
            _burnDamageCallback = OnBurnDamageTick;

            enabled = true;
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
            ReleaseHitbox();
        }

        private void OnDestroy()
        {
            ReleaseHitbox();

            if (moveAction != null)
            {
                moveAction.Disable();
                moveAction.Dispose();
                moveAction = null;
            }

            if (dashAction != null)
            {
                dashAction.Disable();
                dashAction.Dispose();
                dashAction = null;
            }

            if (attackAction != null)
            {
                attackAction.Disable();
                attackAction.Dispose();
                attackAction = null;
            }

            if (launcherAction != null)
            {
                launcherAction.Disable();
                launcherAction.Dispose();
                launcherAction = null;
            }

            if (defendAction != null)
            {
                defendAction.Disable();
                defendAction.Dispose();
                defendAction = null;
            }
        }

        public void Tick(in TickContext context)
        {
            CurrentTick = context.Tick;
            var dt = context.FixedDelta;

            if (dashCooldownLeft > 0f)
                dashCooldownLeft = Mathf.Max(0f, dashCooldownLeft - dt);

            if (invulnLeft > 0f)
                invulnLeft = Mathf.Max(0f, invulnLeft - dt);

            // Skip input reading when in reactive states with locked input
            bool skipInput = superState == SuperState.DefendBreak
                || superState == SuperState.HurtGrounded
                || superState == SuperState.HurtAir
                || superState == SuperState.Lying
                || superState == SuperState.GetUp
                || superState == SuperState.Dead;

            if (!skipInput)
                ReadCombatInputs(in context);

            // Reactive states (Defending .. Dead)
            if (superState >= SuperState.Defending && superState <= SuperState.Dead)
            {
                TickReactiveState(in context, dt);
                return;
            }

            // Original states
            if (superState == SuperState.Hitstun) { TickHitstun(dt); return; }
            if (superState == SuperState.Dash) { TickDash(in context, dt); return; }
            if (superState == SuperState.Attack) { TickAttack(in context, dt); return; }

            // Check for defend input in Locomotion
            if (IsDefendHeld && superState == SuperState.Locomotion)
            {
                superState = SuperState.Defending;
                _currentReactiveState = ReactiveStateId.Defend;
                _reactiveFrameIndex = 0;
                _reactiveFrameTickCounter = 0;
                return;
            }

            TickLocomotion(in context, dt);
        }

        /// <summary>
        /// Texto para HUD de debug (buffer + ring).
        /// </summary>
        public void GetCombatDebugHudText(StringBuilder sb, List<CombatInputDebugEntry> ringScratch, int ringLines)
        {
            sb.Clear();
            sb.Append("Tick: ").Append(CurrentTick).AppendLine();
            sb.Append("Super: ").Append(superState).AppendLine();

            if (superState == SuperState.Attack)
                sb.Append("Ataque: ").Append(currentAttackId).Append(" | frame ").Append(attackFrameIndex).AppendLine();

            if (superState >= SuperState.Defending && superState <= SuperState.Dead)
            {
                sb.Append("Reactive: ").Append(_currentReactiveState)
                  .Append(" | frame ").Append(_reactiveFrameIndex).AppendLine();

                if (_lastHitResult.Damage > 0)
                    sb.Append("LastHit: dmg=").Append(_lastHitResult.Damage)
                      .Append(" kb=").Append(_lastHitResult.Knockback)
                      .Append(" flags=").Append(_lastHitResult.Flags).AppendLine();
            }

            sb.AppendLine("Buffer (FIFO, ≤8 ticks):");
            var buf = _combatBuffer.BufferedInputs;
            if (buf.Count == 0)
                sb.AppendLine("  (vazio)");
            else
            {
                for (var i = 0; i < buf.Count; i++)
                {
                    var e = buf[i];
                    sb.Append("  ").Append(e.AttackId).Append(" @ tick ").Append(e.PressedTick).AppendLine();
                }
            }

            sb.AppendLine("Ring debug (últimos):");
            _combatBuffer.GetRecentDebugEntries(ringLines, ringScratch);
            if (ringScratch.Count == 0)
                sb.AppendLine("  (nenhum)");
            else
            {
                for (var i = 0; i < ringScratch.Count; i++)
                {
                    var d = ringScratch[i];
                    sb.Append("  t=").Append(d.Tick).Append(' ').Append(d.AttackId);
                    if (!string.IsNullOrEmpty(d.Label))
                        sb.Append(" — ").Append(d.Label);
                    sb.AppendLine();
                }
            }
        }

        private void ReadCombatInputs(in TickContext ctx)
        {
            _combatBuffer.AgeOutExpired(ctx.Tick);

            if (launcherAction != null && launcherAction.WasPressedThisFrame())
                _combatBuffer.Push(in ctx, CombatAttackId.Launcher, "Launcher down");

            if (superState == SuperState.Dash && attackAction != null && attackAction.WasPressedThisFrame())
            {
                _combatBuffer.Push(in ctx, CombatAttackId.DashAttack, "Dash+Attack");
                return;
            }

            if (attackAction != null && attackAction.WasPressedThisFrame())
                _combatBuffer.Push(in ctx, CombatAttackId.Jab, "Jab down");
        }

        private bool TryBeginAttackFromBuffer(in TickContext ctx, float dt)
        {
            if (!HasAnyCombatAsset())
                return false;

            if (!_combatBuffer.TryConsumeOldest(
                    ctx.Tick,
                    id => (id == CombatAttackId.Jab || id == CombatAttackId.Launcher) && ResolveAttack(id) != null,
                    out var next))
                return false;

            EnterAttack(next.AttackId);
            TickAttack(in ctx, dt);
            return true;
        }

        private bool HasAnyCombatAsset()
        {
            return jabDefinition != null || launcherDefinition != null || dashAttackDefinition != null;
        }

        private void EnterDash()
        {
            superState = SuperState.Dash;

            dashTimeLeft = tuning.dashDuration;
            dashCooldownLeft = tuning.dashCooldown;
            invulnLeft = tuning.dashInvulnDuration;

            dashDir = moveInput.sqrMagnitude > 0.0001f ? moveInput.normalized : lastMoveDir.normalized;
            if (dashDir.sqrMagnitude < 0.0001f)
                dashDir = Vector2.right;
        }

        private void TickDash(in TickContext ctx, float dt)
        {
            if (attackAction != null && attackAction.WasPressedThisFrame() && dashAttackDefinition != null)
            {
                EnterAttack(CombatAttackId.DashAttack);
                TickAttack(in ctx, dt);
                return;
            }

            dashTimeLeft -= dt;

            var delta = new Vector3(dashDir.x, dashDir.y, 0f) * (tuning.dashSpeed * dt);
            transform.position += delta;

            if (dashTimeLeft <= 0f)
                superState = SuperState.Locomotion;
        }

        private void EnterAttack(CombatAttackId id)
        {
            ReleaseHitbox();

            if (ResolveAttack(id) == null)
            {
                superState = SuperState.Locomotion;
                currentAttackId = CombatAttackId.None;
                attackFrameIndex = 0;
                return;
            }

            superState = SuperState.Attack;
            currentAttackId = id;
            attackFrameIndex = 0;
        }

        private void TickAttack(in TickContext ctx, float dt)
        {
            var def = ResolveAttack(currentAttackId);
            if (def == null)
            {
                ReleaseHitbox();
                superState = SuperState.Locomotion;
                return;
            }

            moveInput = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
            if (moveInput.sqrMagnitude > 1f)
                moveInput.Normalize();

            var dz = tuning != null ? tuning.inputDeadzone : 0.15f;
            if (moveInput.magnitude < dz)
                moveInput = Vector2.zero;

            if (moveInput.sqrMagnitude > 0.0001f)
                lastMoveDir = moveInput.normalized;

            UpdateActiveHitbox(def);

            if (attackFrameIndex >= def.cancelWindowStartTick && attackFrameIndex <= def.cancelWindowEndTick)
            {
                if (_combatBuffer.TryConsumeOldest(
                        ctx.Tick,
                        id => IsAllowedCancel(def, id) && ResolveAttack(id) != null,
                        out var chain))
                {
                    ReleaseHitbox();
                    EnterAttack(chain.AttackId);
                    return;
                }
            }

            attackFrameIndex++;

            if (attackFrameIndex >= def.durationTicks)
            {
                ReleaseHitbox();
                superState = SuperState.Locomotion;
                currentAttackId = CombatAttackId.None;
            }
        }

        private static bool IsAllowedCancel(AttackDefinition def, CombatAttackId id)
        {
            if (def.allowedCancels == null || def.allowedCancels.Length == 0)
                return false;

            for (var i = 0; i < def.allowedCancels.Length; i++)
            {
                if (def.allowedCancels[i] == id)
                    return true;
            }

            return false;
        }

        private AttackDefinition ResolveAttack(CombatAttackId id)
        {
            switch (id)
            {
                case CombatAttackId.Jab:
                    return jabDefinition;
                case CombatAttackId.Launcher:
                    return launcherDefinition;
                case CombatAttackId.DashAttack:
                    return dashAttackDefinition;
                default:
                    return null;
            }
        }

        private void UpdateActiveHitbox(AttackDefinition def)
        {
            if (!def.TryGetActiveHitboxFrame(attackFrameIndex, out var frame))
            {
                ReleaseHitbox();
                return;
            }

            if (_activeHitbox == null)
            {
                var facingRight = lastMoveDir.x >= 0f;
                _activeHitbox = CombatHitboxPool.Rent();
                _activeHitbox.Arm(
                    gameObject,
                    enemyHurtboxMask,
                    Mathf.Max(1, Mathf.RoundToInt(frame.damage * (_runtimeStats != null ? _runtimeStats.DamageMultiplier : 1f))),
                    Lf2MirrorUtil.MirrorKnockback(frame.knockback, facingRight),
                    currentAttackId,
                    frame.hitStopTicks,
                    frame.screenShakeAmplitude,
                    frame.isGrab);
            }

            var facing = lastMoveDir.x >= 0f ? 1f : -1f;
            var offset = new Vector2(frame.localOffset.x * facing, frame.localOffset.y);
            var center = (Vector2)transform.position + offset;
            _activeHitbox.SetWorldPose(center, frame.halfExtents, 0f);
            _activeHitbox.TickOverlap();
        }

        private void ReleaseHitbox()
        {
            if (_activeHitbox == null)
                return;

            CombatHitboxPool.Return(_activeHitbox);
            _activeHitbox = null;
        }

        private void EnterHitstun(Vector2 knockbackDir)
        {
            ReleaseHitbox();
            superState = SuperState.Hitstun;

            hitstunLeft = tuning.hitstunDuration;

            var dir = knockbackDir.sqrMagnitude > 0.0001f ? knockbackDir.normalized : -lastMoveDir.normalized;
            if (dir.sqrMagnitude < 0.0001f)
                dir = Vector2.left;

            knockbackVel = dir * tuning.knockbackSpeed;
        }

        private void TickHitstun(float dt)
        {
            hitstunLeft -= dt;

            var t = tuning.hitstunDuration > 0f ? Mathf.Clamp01(hitstunLeft / tuning.hitstunDuration) : 0f;
            var vel = knockbackVel * t;

            transform.position += new Vector3(vel.x, vel.y, 0f) * dt;

            if (hitstunLeft <= 0f)
                superState = SuperState.Locomotion;
        }

        public bool TryApplyHit(Vector2 knockbackDir)
        {
            if (IsInvulnerable)
                return false;

            EnterHitstun(knockbackDir);
            return true;
        }

        /// <summary>
        /// Apply a reactive hit: evaluate state via DamageReactionRouter and enter the
        /// appropriate reactive state (HurtGrounded, HurtAir, DefendBreak, etc.).
        /// Falls back to old Hitstun if no router is configured.
        /// </summary>
        public void ApplyReactiveHit(in HitResult hit)
        {
            _lastHitResult = hit;

            if (IsInvulnerable)
                return;

            // Apply status effect if present
            if (hit.Effect != StatusEffect.None)
            {
                if (_cachedSR != null) _originalTint = _cachedSR.color;
                _statusEffect = StatusEffectProcessor.ApplyFromTuning(hit.Effect, _statusEffectTuning);
            }

            if (_reactionRouter != null)
            {
                bool isDefending = superState == SuperState.Defending;
                bool isAirborne = superState == SuperState.HurtAir;
                var stateId = _reactionRouter.Evaluate(in hit, isDefending, isAirborne);
                EnterReactiveState(stateId, in hit);
            }
            else
            {
                // Fallback: use old hitstun
                TryApplyHit(hit.Knockback);
            }
        }

        private void EnterReactiveState(ReactiveStateId stateId, in HitResult hit)
        {
            ReleaseHitbox();

            _currentReactiveState = stateId;
            _reactiveFrameIndex = 0;
            _reactiveFrameTickCounter = 0;
            _lyingTimerTicks = 0;

            switch (stateId)
            {
                case ReactiveStateId.Defend:
                    superState = SuperState.Defending;
                    break;
                case ReactiveStateId.DefendBreak:
                    superState = SuperState.DefendBreak;
                    ApplyReactiveKnockback(hit.Knockback, hit.AttackerFacing, 1f);
                    break;
                case ReactiveStateId.DefendHit:
                    superState = SuperState.Defending; // stays in defending, just micro-stun
                    ApplyReactiveKnockback(hit.Knockback, hit.AttackerFacing, 0.3f);
                    break;
                case ReactiveStateId.HurtGrounded:
                    superState = SuperState.HurtGrounded;
                    ApplyReactiveKnockback(hit.Knockback, hit.AttackerFacing, 1f);
                    break;
                case ReactiveStateId.HurtAir:
                    superState = SuperState.HurtAir;
                    // Horizontal knockback (HSM-owned)
                    var dir = hit.AttackerFacing >= 0 ? -1f : 1f; // knock away from attacker
                    _reactiveVelocity = new Vector2(dir * Mathf.Abs(hit.Knockback.x) * 0.1f, 0f);
                    // Vertical launch via CharacterMotor
                    if (_motor != null)
                        _motor.Launch(hit.Knockback.y * 0.15f);
                    break;
                case ReactiveStateId.Lying:
                    superState = SuperState.Lying;
                    break;
                case ReactiveStateId.GetUp:
                    superState = SuperState.GetUp;
                    invulnLeft = tuning.invulnOnGetUpTicks / 60f;
                    break;
                case ReactiveStateId.Dead:
                    superState = SuperState.Dead;
                    break;
            }
        }

        private void ApplyReactiveKnockback(Vector2 knockback, int attackerFacing, float scale)
        {
            var kbDir = attackerFacing >= 0 ? -1f : 1f;
            knockbackVel = new Vector2(
                kbDir * knockback.x * scale * tuning.knockbackSpeed * 0.1f, 0f);
            hitstunLeft = 0.2f; // brief knockback movement
        }

        private void TickReactiveState(in TickContext ctx, float dt)
        {
            // Tick status effect
            if (_statusEffect.IsActive)
            {
                StatusEffectProcessor.Tick(ref _statusEffect, _cachedSR, _originalTint, _burnDamageCallback);
            }

            var move = _reactionRouter?.GetMove(_currentReactiveState);
            if (move == null || move.frames == null || move.frames.Length == 0)
            {
                // No move data — fallback to Locomotion
                superState = SuperState.Locomotion;
                return;
            }

            // State-specific logic
            switch (_currentReactiveState)
            {
                case ReactiveStateId.Defend:
                    TickDefend(in ctx, dt, move);
                    break;
                case ReactiveStateId.DefendHit:
                    TickDefendHit(in ctx, dt, move);
                    break;
                case ReactiveStateId.DefendBreak:
                    TickReactiveSequence(dt, move);
                    break;
                case ReactiveStateId.HurtGrounded:
                    TickReactiveSequence(dt, move);
                    break;
                case ReactiveStateId.HurtAir:
                    TickHurtAir(dt, move);
                    break;
                case ReactiveStateId.Lying:
                    TickLying(dt);
                    break;
                case ReactiveStateId.GetUp:
                    TickReactiveSequence(dt, move);
                    break;
                case ReactiveStateId.Dead:
                    // Dead is permanent — no tick needed
                    break;
            }

            // Apply knockback velocity decay (for states that use it)
            if (knockbackVel.sqrMagnitude > 0.001f)
            {
                transform.position += new Vector3(knockbackVel.x, knockbackVel.y, 0f) * dt;
                knockbackVel = Vector2.Lerp(knockbackVel, Vector2.zero, dt * 5f);
            }
        }

        private void TickDefend(in TickContext ctx, float dt, ReactiveMoveDefinition move)
        {
            AdvanceReactiveFrame(dt, move);

            // If defend released, return to Locomotion
            if (!IsDefendHeld)
            {
                superState = SuperState.Locomotion;
                _currentReactiveState = ReactiveStateId.HurtGrounded; // reset to safe default
                return;
            }
        }

        private void TickDefendHit(in TickContext ctx, float dt, ReactiveMoveDefinition move)
        {
            AdvanceReactiveFrame(dt, move);

            // When sequence finishes, return to Defend (if still held) or Locomotion
            if (_reactiveFrameIndex >= move.frames.Length)
            {
                superState = IsDefendHeld ? SuperState.Defending : SuperState.Locomotion;
                _currentReactiveState = IsDefendHeld ? ReactiveStateId.Defend : ReactiveStateId.HurtGrounded;
                _reactiveFrameIndex = 0;
                _reactiveFrameTickCounter = 0;
            }
        }

        private void TickReactiveSequence(float dt, ReactiveMoveDefinition move)
        {
            AdvanceReactiveFrame(dt, move);

            if (_reactiveFrameIndex >= move.frames.Length)
            {
                if (move.loop)
                {
                    _reactiveFrameIndex = 0;
                    _reactiveFrameTickCounter = 0;
                }
                else
                {
                    TransitionFromReactiveMove(move);
                }
            }
        }

        private void TickHurtAir(float dt, ReactiveMoveDefinition move)
        {
            // Horizontal velocity (HSM-owned, not motor)
            if (_reactiveVelocity.x != 0f)
            {
                var pos = transform.position;
                pos.x += _reactiveVelocity.x * dt;
                transform.position = pos;
                _reactiveVelocity.x = Mathf.MoveTowards(_reactiveVelocity.x, 0f, dt * 5f);
            }

            // Vertical physics via CharacterMotor (gravity + grounded check)
            if (_motor != null)
            {
                _motor.Tick(dt);

                if (_motor.IsGrounded)
                {
                    // Landing → transition to Lying
                    _reactiveVelocity = Vector2.zero;
                    EnterReactiveState(ReactiveStateId.Lying, default);
                    return;
                }
            }

            // Advance frames (looping)
            AdvanceReactiveFrame(dt, move);
            if (_reactiveFrameIndex >= move.frames.Length && move.loop)
            {
                _reactiveFrameIndex = 0;
                _reactiveFrameTickCounter = 0;
            }
        }

        private void TickLying(float dt)
        {
            _lyingTimerTicks++;
            if (_lyingTimerTicks >= tuning.lyingDurationTicks)
            {
                EnterReactiveState(ReactiveStateId.GetUp, default);
            }
        }

        private void AdvanceReactiveFrame(float dt, ReactiveMoveDefinition move)
        {
            if (move.frames == null || _reactiveFrameIndex >= move.frames.Length)
                return;

            // Apply impulse for the very first frame on entry
            if (_reactiveFrameTickCounter == 0)
                ApplyFrameEffects(move.frames[_reactiveFrameIndex]);

            _reactiveFrameTickCounter++;
            var frame = move.frames[_reactiveFrameIndex];

            if (_reactiveFrameTickCounter >= frame.durationTicks)
            {
                _reactiveFrameIndex++;
                _reactiveFrameTickCounter = 0;

                // Apply impulse/invuln for the new frame we just entered
                if (_reactiveFrameIndex < move.frames.Length)
                    ApplyFrameEffects(move.frames[_reactiveFrameIndex]);
            }
        }

        private void ApplyFrameEffects(ReactiveFrameDefinition frame)
        {
            if (frame.impulse.sqrMagnitude > 0.001f)
            {
                var facing = FacingRight ? 1f : -1f;
                _reactiveVelocity += new Vector2(
                    frame.impulse.x * facing, frame.impulse.y) * 0.1f;
            }

            if (frame.invulnerable)
                invulnLeft = frame.durationTicks / 60f;
        }

        private void TransitionFromReactiveMove(ReactiveMoveDefinition move)
        {
            // Safety: if GetUp transitions back to GetUp, force Locomotion to prevent infinite loop
            if (move.stateId == ReactiveStateId.GetUp && move.nextStateOnFinish == ReactiveStateId.GetUp)
            {
                superState = SuperState.Locomotion;
                _currentReactiveState = ReactiveStateId.HurtGrounded;
                return;
            }

            switch (move.nextStateOnFinish)
            {
                case ReactiveStateId.HurtGrounded:
                case ReactiveStateId.Defend:
                    superState = SuperState.Locomotion;
                    _currentReactiveState = ReactiveStateId.HurtGrounded;
                    break;
                case ReactiveStateId.GetUp:
                    EnterReactiveState(ReactiveStateId.GetUp, default);
                    break;
                case ReactiveStateId.Lying:
                    EnterReactiveState(ReactiveStateId.Lying, default);
                    break;
                default:
                    superState = SuperState.Locomotion;
                    _currentReactiveState = ReactiveStateId.HurtGrounded;
                    break;
            }
        }

        private void OnBurnDamageTick(int dmg)
        {
            if (_cachedHealth != null) _cachedHealth.ApplyDamage(dmg);
            // Burn death → enter Dead state immediately
            if (_cachedHealth != null && _cachedHealth.IsDead && _currentReactiveState != ReactiveStateId.Dead)
                EnterReactiveState(ReactiveStateId.Dead, default);
        }

        private void TickLocomotionMovement(float dt)
        {
            moveInput = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;

            if (moveInput.sqrMagnitude > 1f)
                moveInput.Normalize();

            var dz = tuning != null ? tuning.inputDeadzone : 0.15f;
            if (moveInput.magnitude < dz)
                moveInput = Vector2.zero;

            if (moveInput.sqrMagnitude > 0.0001f)
                lastMoveDir = moveInput.normalized;
        }

        private void TickLocomotion(in TickContext ctx, float dt)
        {
            TickLocomotionMovement(dt);

            if (TryBeginAttackFromBuffer(in ctx, dt))
                return;

            var wantsDash = dashAction != null && dashAction.WasPressedThisFrame();
            if (wantsDash && dashCooldownLeft <= 0f)
            {
                EnterDash();
                TickDash(in ctx, dt);
                return;
            }

            locomotionSubState = moveInput.sqrMagnitude > 0.0001f ? LocomotionSubState.Move : LocomotionSubState.Idle;

            if (locomotionSubState == LocomotionSubState.Move)
            {
                var delta = new Vector3(moveInput.x, moveInput.y, 0f) * (tuning.moveSpeed * dt);
                if (_runtimeStats != null)
                    delta *= _runtimeStats.MoveSpeedMultiplier;
                transform.position += delta;
            }
        }
    }
}
