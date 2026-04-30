using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct CombatHitInfo
    {
        public readonly GameObject Attacker;
        public readonly int Damage;
        public readonly Vector2 Knockback;
        public readonly CombatAttackId AttackId;
        public readonly int HitStopTicks;
        public readonly float ScreenShakeAmplitude;
        public readonly bool IsGrab;
        public readonly int Bdefend;
        public readonly int Fall;
        public readonly StatusEffect Effect;

        public CombatHitInfo(
            GameObject attacker,
            int damage,
            Vector2 knockback,
            CombatAttackId attackId,
            int hitStopTicks,
            float screenShakeAmplitude,
            bool isGrab,
            int bdefend = 0,
            int fall = 0,
            StatusEffect effect = StatusEffect.None)
        {
            Attacker = attacker;
            Damage = damage;
            Knockback = knockback;
            AttackId = attackId;
            HitStopTicks = hitStopTicks;
            ScreenShakeAmplitude = screenShakeAmplitude;
            IsGrab = isGrab;
            Bdefend = bdefend;
            Fall = fall;
            Effect = effect;
        }
    }
}
