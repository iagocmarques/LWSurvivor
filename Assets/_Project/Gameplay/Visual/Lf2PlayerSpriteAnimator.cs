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
            if (_sr == null)
                return;

            var frames = IdleFrames;
            var fps = idleFps;
            var directByAttackFrame = false;

            // Track position for hitstun detection (knockback causes movement
            // while neither IsAttacking nor IsMoving is true).
            var pos = transform.position;
            var moved = (pos - _lastPos).sqrMagnitude > hitstunMoveThreshold * hitstunMoveThreshold;
            _lastPos = pos;

            if (_hsm != null && _hsm.IsAttacking)
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

                directByAttackFrame = true;
            }
            else if (_hsm != null && _hsm.IsMoving)
            {
                frames = MoveFrames;
                fps = moveFps;
            }
            else if (moved)
            {
                // Knockback without intentional movement = hitstun
                frames = HitstunFrames;
                fps = hitstunFps;
            }

            if (frames.Length == 0)
                return;

            if (directByAttackFrame && _hsm != null)
            {
                _cursor = Mathf.Clamp(_hsm.ActiveAttackFrameIndex, 0, frames.Length - 1);
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

            if (_hsm != null)
                _sr.flipX = !_hsm.FacingRight;
        }
    }
}
