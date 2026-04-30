using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
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

        private static readonly int[] IdleFrames = { 0, 1, 2, 3 };
        private static readonly int[] MoveFrames = { 4, 5, 6, 7 };
        private static readonly int[] RunFrames = { 20, 21, 22 };
        private static readonly int[] JabFrames = { 10, 11, 12, 13 };
        private static readonly int[] LauncherFrames = { 67, 68, 8, 9, 19, 29 };
        private static readonly int[] DashAttackFrames = { 132, 133, 134, 135, 136, 137 };
        private static readonly int[] HitstunFrames = { 30, 31, 32, 33, 34, 35 };

        private static readonly int[] DefendFrames = { 56 };
        private static readonly int[] DefendHitFrames = { 57 };
        private static readonly int[] DefendBreakFrames = { 46, 47, 48 };
        private static readonly int[] HurtGroundedFrames = { 30, 31, 32 };
        private static readonly int[] HurtAirFrames = { 30, 31, 32, 33, 34, 35, 40, 41, 42, 43, 44, 45 };
        private static readonly int[] LyingFrames = { 33 };
        private static readonly int[] GetUpFrames = { 34, 35, 0 };
        private static readonly int[] DeadFrames = { 33 };

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            Sprite2DMaterialUtility.EnsureCompatibleMaterial(_sr);
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
            else if (_hsm.IsDefendHit)
            {
                frames = DefendHitFrames;
                fps = hitstunFps;
                directBySimFrame = true;
            }
            else if (_hsm.IsDefending)
            {
                frames = DefendFrames;
                fps = defendFps;
            }
            else if (_hsm.IsAttacking)
            {
                fps = attackFps;
                int startId = _hsm.Lf2Sm?.AttackStartFrameId ?? -1;
                var roles = _hsm.Lf2Sm?.Roles;
                if (roles != null && startId >= 0 && startId == roles.AttackForward)
                    frames = LauncherFrames;
                else if (roles != null && startId >= 0 && startId == roles.AttackBack)
                    frames = DashAttackFrames;
                else
                    frames = JabFrames;
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
            if (sprite != null && _sr.sprite != sprite)
                _sr.sprite = sprite;

            _sr.flipX = !_hsm.FacingRight;
        }
    }
}
