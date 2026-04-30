using Project.Gameplay.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.HUD
{
    public sealed class ManaBar : MonoBehaviour
    {
        [SerializeField] private Transform segmentContainer;
        [SerializeField] private GameObject segmentPrefab;
        [SerializeField] private int segmentCount = 10;
        [SerializeField] private float lerpSpeed = 8f;

        [SerializeField] private Color fullColor = new Color(0.2f, 0.5f, 0.95f);
        [SerializeField] private Color emptyColor = new Color(0.1f, 0.15f, 0.3f);

        private Mana _mana;
        private readonly List<Image> _segments = new();
        private float _displayedRatio = 1f;

        public void Bind(Mana mana)
        {
            _mana = mana;
            if (_mana != null)
            {
                _displayedRatio = _mana.CurrentMana / (float)_mana.MaxMana;
                RebuildSegments();
                ApplyVisuals();
            }
        }

        private void Awake()
        {
            if (_segments.Count == 0 && segmentContainer != null)
                RebuildSegments();
        }

        private void Update()
        {
            if (_mana == null) return;

            float targetRatio = _mana.CurrentMana / (float)_mana.MaxMana;
            _displayedRatio = Mathf.Lerp(_displayedRatio, targetRatio, lerpSpeed * Time.deltaTime);
            ApplyVisuals();
        }

        private void RebuildSegments()
        {
            _segments.Clear();

            if (segmentContainer == null) return;

            for (int i = segmentContainer.childCount - 1; i >= 0; i--)
                Destroy(segmentContainer.GetChild(i).gameObject);

            if (segmentPrefab == null) return;

            for (int i = 0; i < segmentCount; i++)
            {
                var seg = Instantiate(segmentPrefab, segmentContainer);
                var img = seg.GetComponent<Image>();
                if (img != null)
                    _segments.Add(img);
            }
        }

        private void ApplyVisuals()
        {
            if (_segments.Count == 0) return;

            for (int i = 0; i < _segments.Count; i++)
            {
                float segThreshold = (i + 1) / (float)_segments.Count;
                bool filled = _displayedRatio >= segThreshold;
                _segments[i].color = filled ? fullColor : emptyColor;
            }
        }

        public void SetSegmentCount(int count)
        {
            segmentCount = Mathf.Max(1, count);
            if (_mana != null)
                RebuildSegments();
        }
    }
}
