using Project.Gameplay.Rendering;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    /// <summary>
    /// Manages LF2 stages: backgrounds, arena bounds, stage progression.
    /// </summary>
    public sealed class Lf2StageManager : MonoBehaviour
    {
        public enum StageId
        {
            CUHK = 1,
            LF = 2,
            FT = 3,
            GW = 4,
            SP = 5,
        }

        [SerializeField] private SpriteRenderer backgroundRenderer;
        [SerializeField] private SpriteRenderer floorRenderer;
        [SerializeField] private ArenaBounds arenaBounds;
        [SerializeField] private Lf2StageData[] stageData;

        [Header("Transition")]
#pragma warning disable 0414
        [SerializeField] private float transitionDuration = 0.5f;
#pragma warning restore 0414

        private StageId _currentStage;
        private SpriteRenderer[] _bgLayerRenderers;

        public StageId CurrentStage => _currentStage;

        public Lf2StageData[] GetStageDataArray() => stageData;

        public void LoadStage(StageId stage)
        {
            _currentStage = stage;

            var data = GetStageData(stage);
            if (data == null)
            {
                Debug.LogWarning($"[Lf2StageManager] No stage data found for {stage}");
                return;
            }

            ApplyBackground(data);
            ApplyFloor(data);
            ApplyArenaBounds(data);
        }

        public void TransitionToStage(StageId stage)
        {
            LoadStage(stage);
        }

        private Lf2StageData GetStageData(StageId stage)
        {
            if (stageData == null) return null;

            for (int i = 0; i < stageData.Length; i++)
            {
                if (stageData[i] != null && stageData[i].stageId == stage)
                    return stageData[i];
            }

            return null;
        }

        private void ApplyBackground(Lf2StageData data)
        {
            if (backgroundRenderer == null) return;

            if (data.backgroundLayers != null && data.backgroundLayers.Length > 0)
            {
                backgroundRenderer.sprite = data.backgroundLayers[0];
            }
        }

        private void ApplyFloor(Lf2StageData data)
        {
            if (floorRenderer == null) return;
            floorRenderer.sprite = data.floorSprite;
        }

        private void ApplyArenaBounds(Lf2StageData data)
        {
            if (arenaBounds == null) return;
            arenaBounds.SetBounds(data.minX, data.maxX, data.minY, data.maxY);
        }
    }
}
