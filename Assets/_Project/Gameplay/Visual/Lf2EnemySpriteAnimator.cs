using UnityEngine;

namespace Project.Gameplay.Visual
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Lf2EnemySpriteAnimator : MonoBehaviour
    {
        [SerializeField] private string enemyId;
        [SerializeField] private float idleFps = 6f;
        [SerializeField] private float moveFps = 10f;
        [SerializeField] private float moveThreshold = 0.002f;

        private SpriteRenderer _sr;
        private float _timer;
        private int _cursor;
        private Vector3 _lastPos;

        private static readonly int[] IdleFrames = { 0, 1, 2, 3 };
        private static readonly int[] MoveFrames = { 20, 21, 22, 23, 24, 25 };

        public void Configure(string id)
        {
            enemyId = id;
        }

        private void Awake()
        {
            _sr = GetComponent<SpriteRenderer>();
            _lastPos = transform.position;
        }

        private void Update()
        {
            if (_sr == null)
                return;

            var pos = transform.position;
            var moving = (pos - _lastPos).sqrMagnitude > moveThreshold * moveThreshold;
            _lastPos = pos;

            var frames = moving ? MoveFrames : IdleFrames;
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

            var frame = frames[_cursor];
            var sprite = Lf2VisualLibrary.GetEnemyFrame(enemyId, frame);
            if (sprite != null)
                _sr.sprite = sprite;
        }
    }
}
