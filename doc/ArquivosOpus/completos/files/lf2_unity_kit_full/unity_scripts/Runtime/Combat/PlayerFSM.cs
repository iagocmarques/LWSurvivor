// Assets/Game/Runtime/Combat/PlayerFSM.cs
//
// Per-player state machine driven by CharacterDefinition + MoveDefinition data.
// Operates on ticks (60Hz). Reads input from PlayerInputReader, drives motion
// via CharacterController2_5D, and submits hits to HitboxResolver.
//
// State outline (per levantamento_e_diretrizes.md §4):
//   Idle/Walk/Run -> free movement, can transition to Attack/Jump/Dash
//   Attack        -> playing a MoveDefinition's frames, can be canceled
//   Hitstun       -> brief lockout; when ticks expire returns to Idle
//   Falling       -> launched into the air; ends when grounded
//   GettingUp     -> brief invuln after fall; returns to Idle
//   Dead          -> terminal; physics still runs
//
// MVP simplifications (clearly marked):
//   - No "Run" yet (would be double-tap dash detection).
//   - No "Dash" move yet (will plug in once we have the data asset).
//   - GettingUp is just a timed Idle return.
//   - Hurtbox is a fixed AABB around the player feet.
//   - Single hit applied per attacker move per victim (HitboxResolver enforces).

using System.Collections.Generic;
using LF2Game.Data;
using LF2Game.Input;
using LF2Game.Movement;
using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Combat
{
    [RequireComponent(typeof(CharacterController2_5D))]
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class PlayerFSM : MonoBehaviour, ITickable, IHitVictim
    {
        [Header("Data")]
        public CharacterDefinition Character;

        [Header("Hurtbox (relative to feet, world units)")]
        public Vector2 HurtboxSize   = new(0.6f, 1.6f);
        public Vector2 HurtboxOffset = new(0f, 0.8f);

        [Header("Runtime (read-only)")]
        [SerializeField] CharacterState _state;
        [SerializeField] float _hp;
        [SerializeField] int _hitstunRemaining;
        [SerializeField] int _fallenTicks;

        public CharacterState State => _state;
        public float Hp => _hp;
        public bool  IsAlive => _state != CharacterState.Dead;

        // Move execution
        MoveDefinition _move;
        int _frameIndex;
        int _frameTicksRemaining;
        int _moveInstanceId; // increments each StartMove

        static int _nextEntityId = 1;
        readonly int _entityId = _nextEntityId++;

        // Cached
        CharacterController2_5D _ctl;
        PlayerInputReader _input;
        SpriteRenderer _sr;

        void Awake()
        {
            _ctl   = GetComponent<CharacterController2_5D>();
            _input = GetComponent<PlayerInputReader>();
            _sr    = GetComponent<SpriteRenderer>();
            if (Character != null) _hp = Character.maxHp;
            _state = CharacterState.Idle;
        }

        void OnEnable()  { TickRunner.Instance?.Register(this); }
        void OnDisable() { TickRunner.Instance?.Unregister(this); }

        // ---------- TICK ----------
        public void Tick(int tick)
        {
            if (Character == null) return;
            if (HitFeel.IsHitstopActive) return;

            // Always submit hurtbox so attackers can hit us.
            if (IsAlive) HitboxResolver.Instance?.SubmitVictim(this);

            switch (_state)
            {
                case CharacterState.Hitstun:
                    if (--_hitstunRemaining <= 0) _state = CharacterState.Idle;
                    break;

                case CharacterState.Falling:
                    _fallenTicks++;
                    if (_ctl.IsGrounded && _fallenTicks > 6) // small landing buffer
                    {
                        _state = CharacterState.GettingUp;
                        _fallenTicks = 0;
                        _hitstunRemaining = 30; // ~0.5s recovery
                    }
                    break;

                case CharacterState.GettingUp:
                    if (--_hitstunRemaining <= 0) _state = CharacterState.Idle;
                    break;

                case CharacterState.Dead:
                    break;

                case CharacterState.Attack:
                    TickActiveMove();
                    break;

                default:
                    TickFreeMovement();
                    break;
            }

            UpdateAnimSprite();
        }

        // ---------- FREE MOVEMENT ----------
        void TickFreeMovement()
        {
            var axis = _input.MoveAxis;
            float speed = Character.walkSpeed;
            _ctl.SetGroundVelocity(axis.x * speed, axis.y * speed);

            if      (axis.x >  0.1f) _ctl.FacingLeft = false;
            else if (axis.x < -0.1f) _ctl.FacingLeft = true;

            _state = axis.sqrMagnitude > 0.01f ? CharacterState.Walk : CharacterState.Idle;

            // Try to start a move from buffered input.
            if (_input.Buffer.Consume(InputAction.Jump))    { TryStartMoveFor(InputAction.Jump);    return; }
            if (_input.Buffer.Consume(InputAction.Attack))  { TryStartMoveFor(InputAction.Attack);  return; }
            if (_input.Buffer.Consume(InputAction.Defense)) { TryStartMoveFor(InputAction.Defense); return; }
        }

        // ---------- MOVES ----------
        void TryStartMoveFor(InputAction trigger)
        {
            for (int i = 0; i < Character.moves.Count; i++)
            {
                var m = Character.moves[i];
                if (m == null) continue;
                if (m.inputTrigger != trigger) continue;
                if (!m.validEntryStates.Contains(_state)) continue;
                StartMove(m);
                return;
            }
        }

        void StartMove(MoveDefinition m)
        {
            if (m.frames == null || m.frames.Count == 0) return;
            _move = m;
            _frameIndex = 0;
            _moveInstanceId++;
            _state = CharacterState.Attack;
            EnterFrame(m.frames[0]);
        }

        void EnterFrame(FrameDefinition f)
        {
            _frameTicksRemaining = Mathf.Max(1, f.durationTicks);

            // Apply impulse (one-shot, mirrored by facing).
            var imp = f.impulse;
            _ctl.Velocity += new Vector3(_ctl.FacingLeft ? -imp.x : imp.x, imp.y, imp.z);

            if (f.sound != null) AudioSource.PlayClipAtPoint(f.sound, transform.position, 0.6f);

            SubmitFrameHitboxes(f);
        }

        void TickActiveMove()
        {
            // Cancels (peek without consuming yet — only consume if we actually transition).
            if (_input.Buffer.Peek(InputAction.Attack)  && _move.cancelOnAttack  != null)
            { _input.Buffer.Consume(InputAction.Attack);  StartMove(_move.cancelOnAttack);  return; }
            if (_input.Buffer.Peek(InputAction.Defense) && _move.cancelOnDefense != null)
            { _input.Buffer.Consume(InputAction.Defense); StartMove(_move.cancelOnDefense); return; }
            if (_input.Buffer.Peek(InputAction.Jump)    && _move.cancelOnJump    != null)
            { _input.Buffer.Consume(InputAction.Jump);    StartMove(_move.cancelOnJump);    return; }

            // Continue current frame, submit its hitboxes again.
            SubmitFrameHitboxes(_move.frames[_frameIndex]);

            _frameTicksRemaining--;
            if (_frameTicksRemaining <= 0)
            {
                _frameIndex++;
                if (_frameIndex >= _move.frames.Count) { EndMove(); return; }
                EnterFrame(_move.frames[_frameIndex]);
            }
        }

        void EndMove()
        {
            _move = null;
            _frameIndex = 0;
            _frameTicksRemaining = 0;
            _state = CharacterState.Idle;
            _ctl.SetGroundVelocity(0, 0);
        }

        // ---------- HITBOX SUBMISSION ----------
        void SubmitFrameHitboxes(in FrameDefinition f)
        {
            if (f.hitboxes == null || HitboxResolver.Instance == null) return;
            for (int i = 0; i < f.hitboxes.Length; i++)
            {
                var hb = f.hitboxes[i];
                if (hb.kind != HitboxKind.Strike && hb.kind != HitboxKind.Grab) continue;

                // Convert local rect (cell-local, origin = bottom-center pivot) to world.
                Rect localRect = hb.rect;
                if (_ctl.FacingLeft)
                {
                    // mirror horizontally: x' = -x - w
                    localRect.x = -localRect.x - localRect.width;
                }
                Vector3 feet = transform.position;
                Rect worldRect = new(
                    feet.x + localRect.x,
                    feet.y + localRect.y,
                    localRect.width,
                    localRect.height
                );

                HitboxResolver.Instance.SubmitAttack(_entityId, _moveInstanceId, worldRect, _ctl.Position.Depth, transform.position, hb);
            }
        }

        // ---------- IHitVictim ----------
        public Vector3 GetWorldPos()         => transform.position;
        public float   GetDepth()            => _ctl.Position.Depth;
        public int     GetVictimId()         => _entityId;
        public Rect    GetHurtboxWorldRect()
        {
            Vector3 feet = transform.position;
            return new Rect(
                feet.x + HurtboxOffset.x - HurtboxSize.x * 0.5f,
                feet.y + HurtboxOffset.y - HurtboxSize.y * 0.5f,
                HurtboxSize.x, HurtboxSize.y);
        }

        public void TakeHit(in HitboxDefinition hb, Vector3 sourcePos)
        {
            if (!IsAlive) return;
            if (_state == CharacterState.GettingUp) return; // brief invuln

            _hp -= hb.damage;
            HitFeel.RequestHitstop(hb.hitstopTicks);
            HitFeel.RequestShake(Mathf.Min(0.6f, hb.knockback.magnitude * 0.05f));

            // Knockback: away from source, on X primarily, with vertical kick.
            Vector3 dir = transform.position - sourcePos;
            dir.z = 0f;
            float sx = dir.x >= 0f ? 1f : -1f;
            _ctl.Velocity = new Vector3(sx * hb.knockback.x, 0f, hb.knockback.y);

            _move = null;
            if (_hp <= 0f)        { _state = CharacterState.Dead; }
            else if (hb.causesKnockdown || hb.knockback.y > 0.1f)
                                  { _state = CharacterState.Falling; _fallenTicks = 0; }
            else                  { _state = CharacterState.Hitstun; _hitstunRemaining = hb.hitstunTicks; }
        }

        // ---------- VISUAL ----------
        void UpdateAnimSprite()
        {
            if (Character == null || Character.frames == null || Character.frames.Length == 0) return;

            int frameIndex = 0;

            if (_state == CharacterState.Attack && _move != null && _frameIndex < _move.frames.Count)
            {
                frameIndex = _move.frames[_frameIndex].spriteIndex;
            }
            else
            {
                // Pick idle/walk anim from a "none-trigger" move that matches state.
                var anim = FindAmbientMove(_state);
                if (anim != null && anim.frames.Count > 0)
                {
                    int subTick = TickRunner.Instance != null ? TickRunner.Instance.CurrentTick : 0;
                    int subIdx = (subTick / 6) % anim.frames.Count;
                    frameIndex = anim.frames[subIdx].spriteIndex;
                }
            }

            if (frameIndex >= 0 && frameIndex < Character.frames.Length)
                _sr.sprite = Character.frames[frameIndex];
        }

        MoveDefinition FindAmbientMove(CharacterState s)
        {
            for (int i = 0; i < Character.moves.Count; i++)
            {
                var m = Character.moves[i];
                if (m == null) continue;
                if (m.inputTrigger != InputAction.None) continue;
                if (m.validEntryStates.Contains(s)) return m;
            }
            return null;
        }

        // Visualize hurtbox in editor.
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 feet = transform.position;
            Vector3 c = new(feet.x + HurtboxOffset.x, feet.y + HurtboxOffset.y, 0);
            Gizmos.DrawWireCube(c, new Vector3(HurtboxSize.x, HurtboxSize.y, 0.01f));
        }
    }
}
