#if false
using UnityEngine;

namespace Project.Data
{
    public enum UpgradeKind
    {
        MoveSpeed = 0,
        Damage = 1,
        MaxHealth = 2,
        AutoPulse = 3,
        AutoPulseRate = 4,
        AutoPulseRadius = 5,
        XpGain = 6
    }

    [CreateAssetMenu(fileName = "Upgrade_", menuName = "_Project/Data/Upgrade Definition", order = 0)]
    public sealed class UpgradeDefinition : ScriptableObject
    {
        public string id = "upgrade.move.01";
        public string displayName = "Swift Feet";
        [TextArea] public string description = "+10% movement speed";
        public UpgradeKind kind = UpgradeKind.MoveSpeed;
        [Range(0.01f, 1f)] public float magnitude = 0.1f;
    }
}
#endif
