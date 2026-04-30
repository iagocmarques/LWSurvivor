using UnityEngine;

namespace Project.Gameplay.LF2
{
    [System.Serializable]
    public struct EnemyWave
    {
        public string[] enemies;
        public float[] spawnDelays;
        public float waveDelay;
    }

    [CreateAssetMenu(fileName = "Lf2StageData", menuName = "_Project/Gameplay/LF2 Stage Data", order = 0)]
    public sealed class Lf2StageData : ScriptableObject
    {
        [Header("Stage Identity")]
        public Lf2StageManager.StageId stageId;

        [Header("Background Layers (back to front)")]
        public Sprite[] backgroundLayers;

        [Header("Floor")]
        public Sprite floorSprite;

        [Header("Arena Bounds")]
        public float minX = -15f;
        public float maxX = 15f;
        public float minY = -2f;
        public float maxY = 8f;

        [Header("Music")]
        public AudioClip musicClip;

        [Header("Enemy Waves")]
        public EnemyWave[] waves;
    }
}
