using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "RunBalance_Default", menuName = "_Project/Data/Run Balance Definition", order = 0)]
    public sealed class RunBalanceDefinition : ScriptableObject
    {
        [Header("Run")]
        [Min(60f)] public float runDurationSeconds = 900f;
        [Min(0.1f)] public float xpBasePerLevel = 4.5f;
        [Min(1.01f)] public float xpGrowth = 1.16f;

        [Header("Horde")]
        [Min(10)] public int targetEnemyCountAtEnd = 320;
        [Min(10f)] public float spawnRampDurationSeconds = 600f;
        [Min(0.02f)] public float spawnCooldown = 0.06f;
    }
}
