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
        [SerializeField] private float moveThreshold = 0.002f;

        private SpriteRenderer _sr;
        private float _timer;
        private int _cursor;
        private Vector3 _lastPos;
        private bool _facingRight = true;

        // LF2 enemy sprite sheets: 800x560, 10 cols x 7 rows, 80x80 per frame.
        // Frame index = pic value (row * 10 + col), top-to-bottom, left-to-right.
        // Row 0: standing (0-2) + walking (3-N, varies per character)
        private int[] _idleFrames = { 0, 1, 2 };
        private int[] _moveFrames = { 3, 4, 5, 6, 7 };

        public void Configure(string id)
        {
            enemyId = id;
            _cursor = 0;
            _timer = 0f;
            _lastPos = transform.position;
            _facingRight = true;

            // Walking frame count varies per LF2 character:
            // Bandit: 5 walk frames (3-7), Bruiser/Scout: 7 walk frames (3-9)
            if (!string.IsNullOrEmpty(id) && (id.Contains("bruiser") || id.Contains("scout")))
                _moveFrames = new[] { 3, 4, 5, 6, 7, 8, 9 };
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

            var frames = moving ? _moveFrames : _idleFrames;
            var fps = moving ? moveFps : idleFps;
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
