using UnityEngine;

namespace Project.Gameplay.Visual
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Lf2EnemySpriteAnimator : MonoBehaviour
    {
        [SerializeField] private string enemyId;
        [SerializeField] private float idleFps = 4f;
        [SerializeField] private float moveFps = 10f;
        [SerializeField] private float attackFps = 8f;
        [SerializeField] private float moveThreshold = 0.002f;

        private SpriteRenderer _sr;
        private float _timer;
        private int _cursor;
        private Vector3 _lastPos;
        private bool _facingRight = true;
        private bool _attacking;

        private int[] _idleFrames = { 0, 1, 2 };
        private int[] _moveFrames = { 3, 4, 5, 6, 7 };
        private int[] _attackFrames;

        public void Configure(string id)
        {
            enemyId = id;
            _cursor = 0;
            _timer = 0f;
            _lastPos = transform.position;
            _facingRight = true;
            _attacking = false;
            _attackFrames = null;

            if (string.IsNullOrEmpty(id))
                return;

            if (id.Contains("bruiser") || id.Contains("scout"))
                _moveFrames = new[] { 3, 4, 5, 6, 7, 8, 9 };

            if (id.Contains("hunter"))
            {
                _moveFrames = new[] { 3, 4, 5, 6, 7 };
                _attackFrames = new[] { 10, 11, 12, 13 };
            }

            if (id.Contains("mark") || id.Contains("knight"))
            {
                _moveFrames = new[] { 3, 4, 5, 6 };
                _attackFrames = new[] { 10, 11, 12, 13, 14 };
                idleFps = 3f;
            }

            if (id.Contains("jack") || id.Contains("justin"))
            {
                _moveFrames = new[] { 3, 4, 5, 6, 7, 8 };
                _attackFrames = new[] { 10, 11, 12, 13, 14, 15 };
                moveFps = 14f;
            }

            if (id.Contains("sorcerer") || id.Contains("jan"))
            {
                _moveFrames = new[] { 3, 4, 5, 6, 7 };
                _attackFrames = new[] { 10, 11, 12, 13, 14 };
            }

            if (id.Contains("monk"))
            {
                _moveFrames = new[] { 3, 4, 5, 6, 7, 8 };
                _attackFrames = new[] { 10, 11, 12, 13 };
                moveFps = 12f;
            }

            if (id.Contains("bat") || id.Contains("louisEX") || id.Contains("firzen") || id.Contains("julian"))
            {
                _moveFrames = new[] { 3, 4, 5, 6, 7, 8, 9 };
                _attackFrames = new[] { 10, 11, 12, 13, 14, 15, 16 };
                attackFps = 10f;
            }
        }

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _lastPos = transform.position;
        }

        public void SetFacing(bool facingRight)
        {
            _facingRight = facingRight;
        }

        public void PlayAttack()
        {
            if (_attackFrames == null || _attackFrames.Length == 0)
                return;

            _attacking = true;
            _cursor = 0;
            _timer = 0f;
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

            var pos = transform.position;
            var moving = (pos - _lastPos).sqrMagnitude > moveThreshold * moveThreshold;
            _lastPos = pos;

            int[] frames;
            float fps;

            if (_attacking && _attackFrames != null)
            {
                frames = _attackFrames;
                fps = attackFps;

                if (_cursor >= frames.Length)
                {
                    _attacking = false;
                    _cursor = 0;
                    frames = moving ? _moveFrames : _idleFrames;
                    fps = moving ? moveFps : idleFps;
                }
            }
            else
            {
                _attacking = false;
                frames = moving ? _moveFrames : _idleFrames;
                fps = moving ? moveFps : idleFps;
            }

            if (frames.Length == 0)
                return;

            var step = 1f / Mathf.Max(1f, fps);
            _timer += Time.deltaTime;
            if (_timer >= step)
            {
                _timer -= step;
                _cursor = (_cursor + 1) % frames.Length;
            }

            if (_cursor >= frames.Length)
                _cursor = 0;
            var frame = frames[_cursor];
            var sprite = Lf2VisualLibrary.GetEnemyFrame(enemyId, frame);
            if (sprite != null)
                _sr.sprite = sprite;
            _sr.flipX = !_facingRight;
        }
    }
}
