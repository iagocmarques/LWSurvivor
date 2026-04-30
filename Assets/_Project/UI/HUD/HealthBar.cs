using Project.Gameplay.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.HUD
{
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private Image drainImage;
        [SerializeField] private float drainSpeed = 2f;
        [SerializeField] private float lerpSpeed = 8f;

        [SerializeField] private Color highColor = new Color(0.2f, 0.85f, 0.2f);
        [SerializeField] private Color midColor = new Color(0.95f, 0.85f, 0.1f);
        [SerializeField] private Color lowColor = new Color(0.9f, 0.15f, 0.1f);

        private Health _health;
        private float _displayedRatio = 1f;
        private float _drainRatio = 1f;

        public void Bind(Health health)
        {
            _health = health;
            if (_health != null)
            {
                _displayedRatio = _health.CurrentHealth / (float)_health.MaxHealth;
                _drainRatio = _displayedRatio;
                ApplyVisuals();
            }
        }

        private void Update()
        {
            if (_health == null) return;

            float targetRatio = _health.CurrentHealth / (float)_health.MaxHealth;

            _displayedRatio = Mathf.Lerp(_displayedRatio, targetRatio, lerpSpeed * Time.deltaTime);
            _drainRatio = Mathf.MoveTowards(_drainRatio, _displayedRatio, drainSpeed * Time.deltaTime);

            ApplyVisuals();
        }

        private void ApplyVisuals()
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = _displayedRatio;
                fillImage.color = GetGradientColor(_displayedRatio);
            }

            if (drainImage != null)
                drainImage.fillAmount = _drainRatio;
        }

        private Color GetGradientColor(float ratio)
        {
            if (ratio > 0.5f)
                return Color.Lerp(midColor, highColor, (ratio - 0.5f) * 2f);
            return Color.Lerp(lowColor, midColor, ratio * 2f);
        }
    }
}
