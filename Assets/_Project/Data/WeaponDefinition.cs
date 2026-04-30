using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Data
{
    public enum Lf2WeaponCategory
    {
        Light = 1,
        Heavy = 2,
        Thrown = 3,
        Broken = 5,
    }

    [CreateAssetMenu(fileName = "NewWeapon", menuName = "_Project/Data/Weapon Definition", order = 2)]
    public sealed class WeaponDefinition : ScriptableObject
    {
        [Header("Identity")]
        public int lf2Id;
        public string displayName;

        [Header("Classification")]
        public Lf2WeaponCategory category;

        [Header("Stats")]
        [Tooltip("Durability HP. Weapon breaks when depleted.")]
        public int hp = 100;
        [Tooltip("Damage dealt when weapon hits the ground after being thrown.")]
        public int dropHurt;
        [Tooltip("Damage multipliers per attack direction: [neutral, forward, back].")]
        public float[] strengths = { 1f, 1f, 1f };

        [Header("Throw")]
        public int throwDamage = 10;
        public float throwSpeed = 8f;

        [Header("Weapon Attack Frames (frame IDs in the weapon .dat)")]
        [Tooltip("Frame ID for neutral weapon attack.")]
        public int attackNeutralFrame = 90;
        [Tooltip("Frame ID for forward weapon attack.")]
        public int attackForwardFrame = 91;
        [Tooltip("Frame ID for back weapon attack.")]
        public int attackBackFrame = 92;

        [Header("Visual")]
        public Sprite sprite;

        [Header("LF2 Data Reference")]
        [Tooltip("Raw .dat bytes for runtime parsing.")]
        public byte[] rawDatBytes;

        public int GetStrengthIndex(int direction)
        {
            if (strengths == null || strengths.Length == 0) return 1;
            int idx = Mathf.Clamp(direction, 0, strengths.Length - 1);
            return Mathf.RoundToInt(strengths[idx]);
        }
    }
}
