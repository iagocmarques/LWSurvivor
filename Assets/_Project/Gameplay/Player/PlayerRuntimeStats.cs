using UnityEngine;

namespace Project.Gameplay.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerRuntimeStats : MonoBehaviour
    {
        public float MoveSpeedMultiplier { get; private set; } = 1f;
        public float DamageMultiplier { get; private set; } = 1f;
        public float MaxHealthMultiplier { get; private set; } = 1f;
        public float XpGainMultiplier { get; private set; } = 1f;
        public float PulseRateMultiplier { get; private set; } = 1f;
        public float PulseRadiusMultiplier { get; private set; } = 1f;

        public void AddMoveSpeedPercent(float percent)
        {
            MoveSpeedMultiplier *= 1f + percent;
        }

        public void AddDamagePercent(float percent)
        {
            DamageMultiplier *= 1f + percent;
        }

        public void AddMaxHealthPercent(float percent)
        {
            MaxHealthMultiplier *= 1f + percent;
        }

        public void AddXpGainPercent(float percent)
        {
            XpGainMultiplier *= 1f + percent;
        }

        public void AddPulseRatePercent(float percent)
        {
            PulseRateMultiplier *= 1f + percent;
        }

        public void AddPulseRadiusPercent(float percent)
        {
            PulseRadiusMultiplier *= 1f + percent;
        }
    }
}
