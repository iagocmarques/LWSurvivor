using UnityEngine;

namespace Project.Gameplay.LF2
{
    /// <summary>
    /// Detects double-tap direction to activate LF2 running.
    /// </summary>
    public sealed class Lf2RunDetector
    {
        private const float DoubleTapWindow = 0.3f; // 300ms

        private Vector2 _lastTapDir;
        private float _lastTapTime;
        private bool _isRunning;

        public bool IsRunning => _isRunning;

        /// <summary>
        /// Call each tick with current move input. Returns true when run starts.
        /// </summary>
        public bool Update(Vector2 moveDir, bool movePressed, float time)
        {
            if (!movePressed || moveDir.sqrMagnitude < 0.0001f)
            {
                _isRunning = false;
                return false;
            }

            var dir = moveDir.normalized;

            if (Vector2.Dot(dir, _lastTapDir) > 0.9f && (time - _lastTapTime) < DoubleTapWindow)
            {
                _isRunning = true;
                _lastTapTime = 0;
                return true;
            }

            _lastTapDir = dir;
            _lastTapTime = time;

            return false;
        }

        public void Cancel()
        {
            _isRunning = false;
        }
    }
}
