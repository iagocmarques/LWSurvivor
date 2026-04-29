using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Visual
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Lf2PlayerSpriteAnimator : MonoBehaviour
    {
        [SerializeField] private float idleFps = 7f;
        [SerializeField] private float moveFps = 11f;
        [SerializeField] private float attackFps = 14f;

        private SpriteRenderer _sr;
        private PlayerHsmController _hsm;
        private float _timer;
        private int _cursor;

        private static readonly int[] IdleFrames = { 0, 1, 2, 3 };
        private static readonly int[] MoveFrames = { 20, 21, 22, 23, 24, 25, 26, 27 };
        private static readonly int[] JabFrames = { 60, 61, 62, 63 };
        private static readonly int[] LauncherFrames = { 64, 65, 66, 67 };
        private static readonly int[] DashAttackFrames = { 40, 41, 42, 43, 44, 45 };

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _hsm = GetComponent<PlayerHsmController>();
        }

        private void Update()
        {
            if (_sr == null)
                return;

            var frames = IdleFrames;
            var fps = idleFps;
            var directByAttackFrame = false;

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

            var frameIndex = frames[_cursor];
            var sprite = Lf2VisualLibrary.GetPlayerFrame(frameIndex);
            if (sprite != null)
                _sr.sprite = sprite;
        }
    }
}
