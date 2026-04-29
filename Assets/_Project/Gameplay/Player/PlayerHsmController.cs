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
            Hitstun = 3
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

        public long CurrentTick { get; private set; }

        public bool IsInvulnerable => invulnLeft > 0f;
        public bool IsMoving => superState == SuperState.Locomotion && locomotionSubState == LocomotionSubState.Move;
        public bool IsAttacking => superState == SuperState.Attack;
        public CombatAttackId ActiveAttackId => currentAttackId;
        public int ActiveAttackFrameIndex => attackFrameIndex;

        private void Awake()
        {
            if (tuning == null)
            {
                enabled = false;
                return;
            }

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

            superState = SuperState.Locomotion;
            locomotionSubState = LocomotionSubState.Idle;
            _runtimeStats = GetComponent<PlayerRuntimeStats>();
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
        }

        public void Tick(in TickContext context)
        {
            CurrentTick = context.Tick;

            var dt = context.FixedDelta;

            if (dashCooldownLeft > 0f)
                dashCooldownLeft = Mathf.Max(0f, dashCooldownLeft - dt);

            if (invulnLeft > 0f)
                invulnLeft = Mathf.Max(0f, invulnLeft - dt);

            ReadCombatInputs(in context);

            if (superState == SuperState.Hitstun)
            {
                TickHitstun(dt);
                return;
            }

            if (superState == SuperState.Dash)
            {
                TickDash(in context, dt);
                return;
            }

            if (superState == SuperState.Attack)
            {
                TickAttack(in context, dt);
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
                _activeHitbox = CombatHitboxPool.Rent();
                _activeHitbox.Arm(
                    gameObject,
                    enemyHurtboxMask,
                    Mathf.Max(1, Mathf.RoundToInt(frame.damage * (_runtimeStats != null ? _runtimeStats.DamageMultiplier : 1f))),
                    frame.knockback,
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
