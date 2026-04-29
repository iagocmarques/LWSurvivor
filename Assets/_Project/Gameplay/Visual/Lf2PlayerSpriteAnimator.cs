using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Visual
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Lf2PlayerSpriteAnimator : MonoBehaviour
    {
        [SerializeField] private float idleFps = 4f;
        [SerializeField] private float moveFps = 10f;
        [SerializeField] private float attackFps = 14f;
        [SerializeField] private float hitstunFps = 8f;
        [SerializeField] private float defendFps = 4f;
        [SerializeField] private float hurtFps = 8f;
        [SerializeField] private float lyingFps = 2f;
        [SerializeField] private float getUpFps = 6f;
        [SerializeField] private float hitstunMoveThreshold = 0.002f;

        private SpriteRenderer _sr;
        private PlayerHsmController _hsm;
        private float _timer;
        private int _cursor;
        private Vector3 _lastPos;

        // LF2 Davis: pic values from the official .dat manual.
        // Sheets: davis_0 (pic 0-69), davis_1 (pic 70-139), davis_2 (pic 140-209)
        // pic = row * 10 + col in the 10x7 grid (80x80 per cell).
        //
        // Standing: pic 0,1,2,3 (row 0: cols 0-3)
        // Walking:  pic 4,5,6,7 (row 0: cols 4-7)
        // Running:  pic 20,21,22 (row 2: cols 0-2)
        // Punch (jab combo 1): pic 10,11,12,13 (row 1: cols 0-3)
        // Punch (jab combo 2): pic 14,15,16,17 (row 1: cols 4-7)
        // Super punch (launcher): pic 67,68,8,9,19,29 (frames 70-75)
        // Dash attack: pic 132,133,134,135,136,137 (sheet _1, row 6: cols 2-7)
        // Hitstun (knocked): pic 30,31,32,33,34,35 (row 3: cols 0-5)
        private static readonly int[] IdleFrames = { 0, 1, 2, 3 };
        private static readonly int[] MoveFrames = { 4, 5, 6, 7 };
        private static readonly int[] RunFrames = { 20, 21, 22 };
        private static readonly int[] JabFrames = { 10, 11, 12, 13 };
        private static readonly int[] LauncherFrames = { 67, 68, 8, 9, 19, 29 };
        private static readonly int[] DashAttackFrames = { 132, 133, 134, 135, 136, 137 };
        private static readonly int[] HitstunFrames = { 30, 31, 32, 33, 34, 35 };

        // Reactive combat frames (Davis LF2 manual)
        private static readonly int[] DefendFrames = { 56 };                    // frame 110, pic 56
        private static readonly int[] DefendHitFrames = { 57 };                 // frame 111, pic 57
        private static readonly int[] DefendBreakFrames = { 46, 47, 48 };       // frames 112-114
        private static readonly int[] HurtGroundedFrames = { 30, 31, 32 };      // state 11 (injured)
        private static readonly int[] HurtAirFrames = { 30, 31, 32, 33, 34, 35, 40, 41, 42, 43, 44, 45 }; // state 12 (falling)
        private static readonly int[] LyingFrames = { 33 };                     // on ground (matches ReactiveMoveSet pic 33)
        private static readonly int[] GetUpFrames = { 34, 35, 0 };             // getting up
        private static readonly int[] DeadFrames = { 33 };                      // dead (matches ReactiveMoveSet pic 33)

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _hsm = GetComponent<PlayerHsmController>();
            _lastPos = transform.position;
        }

        private void OnEnable()
        {
            _cursor = 0;
            _timer = 0f;
            _lastPos = transform.position;
        }

        private void Update()
        {
            if (_sr == null || _hsm == null)
                return;

            var frames = IdleFrames;
            var fps = idleFps;
            var directBySimFrame = false;

            var pos = transform.position;
            var moved = (pos - _lastPos).sqrMagnitude > hitstunMoveThreshold * hitstunMoveThreshold;
            _lastPos = pos;

            // --- Reactive states (highest priority after Dead) ---
            if (_hsm.IsDead)
            {
                frames = DeadFrames;
                fps = 1f;
                directBySimFrame = true;
            }
            else if (_hsm.IsHurtAir)
            {
                frames = HurtAirFrames;
                fps = hurtFps;
                directBySimFrame = true;
            }
            else if (_hsm.IsLying)
            {
                frames = LyingFrames;
                fps = lyingFps;
            }
            else if (_hsm.IsGetUp)
            {
                frames = GetUpFrames;
                fps = getUpFps;
                directBySimFrame = true;
            }
            else if (_hsm.IsDefendBreak)
            {
                frames = DefendBreakFrames;
                fps = hurtFps;
                directBySimFrame = true;
            }
            else if (_hsm.IsHurtGrounded)
            {
                frames = HurtGroundedFrames;
                fps = hurtFps;
                directBySimFrame = true;
            }
            else if (_hsm.IsDefending)
            {
                frames = DefendFrames;
                fps = defendFps;
            }
            // --- Original states ---
            else if (_hsm.IsAttacking)
            {
                fps = attackFps;
                switch (_hsm.ActiveAttackId)
                {
                    case CombatAttackId.Launcher:
                        frames = LauncherFrames;
                        break;
                    case CombatAttackId.DashAttack:
                        frames = DashAttackFrames;
                        break;
                    default:
                        frames = JabFrames;
                        break;
                }
                directBySimFrame = true;
            }
            else if (_hsm.IsMoving)
            {
                frames = MoveFrames;
                fps = moveFps;
            }
            else if (moved)
            {
                frames = HitstunFrames;
                fps = hitstunFps;
            }

            if (frames.Length == 0)
                return;

            if (directBySimFrame)
            {
                // For attacks: use ActiveAttackFrameIndex
                // For reactive states: use ReactiveFrameIndex
                int simIndex = _hsm.IsAttacking ? _hsm.ActiveAttackFrameIndex : _hsm.ReactiveFrameIndex;
                _cursor = Mathf.Clamp(simIndex, 0, frames.Length - 1);
            }
            else
            {
                var step = 1f / Mathf.Max(1f, fps);
                _timer += Time.deltaTime;
                if (_timer >= step)
                {
                    _timer -= step;
                    _cursor = (_cursor + 1) % frames.Length;
                }
            }

            if (_cursor >= frames.Length)
                _cursor = 0;
            var frameIndex = frames[_cursor];
            var sprite = Lf2VisualLibrary.GetPlayerFrame(frameIndex);
            if (sprite != null)
                _sr.sprite = sprite;

            _sr.flipX = !_hsm.FacingRight;
        }
    }
}
