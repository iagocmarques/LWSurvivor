using UnityEngine;

namespace Project.Gameplay.Feedback
{
    public sealed class ScreenShake2D : MonoBehaviour
    {
        public static ScreenShake2D Instance { get; private set; }

        private Camera _cam;
        private Vector3 _origin;
        private float _timeLeft;
        private float _amplitude;
        private float _totalDuration;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            if (Instance != null)
                return;

            var go = new GameObject("ScreenShake2D");
            DontDestroyOnLoad(go);
            go.AddComponent<ScreenShake2D>();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Shake(float amplitude, float duration)
        {
            if (amplitude <= 0f || duration <= 0f)
                return;

            if (_timeLeft <= 0f || amplitude > _amplitude)
                _amplitude = amplitude;

            _timeLeft = Mathf.Max(_timeLeft, duration);
            _totalDuration = _timeLeft;
        }

        private void LateUpdate()
        {
            if (_cam == null)
            {
                _cam = Camera.main;
                if (_cam != null)
                    _origin = _cam.transform.position;
            }

            if (_cam == null)
                return;

            if (_timeLeft <= 0f)
            {
                _origin = _cam.transform.position;
                _cam.transform.position = _origin;
                return;
            }

            _timeLeft -= Time.unscaledDeltaTime;

            var fade = _totalDuration > 0f ? Mathf.Clamp01(_timeLeft / _totalDuration) : 0f;
            var offset = Random.insideUnitCircle * (_amplitude * fade);
            _cam.transform.position = new Vector3(_origin.x + offset.x, _origin.y + offset.y, _origin.z);
        }
    }
}
