using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Dados mínimos de um golpe para o receptor aplicar dano / knockback.
    /// </summary>
    public readonly struct CombatHitInfo
    {
        public readonly GameObject Attacker;
        public readonly int Damage;
        public readonly Vector2 Knockback;
        public readonly CombatAttackId AttackId;
        public readonly int HitStopTicks;
        public readonly float ScreenShakeAmplitude;
        public readonly bool IsGrab;

        public CombatHitInfo(
            GameObject attacker,
            int damage,
            Vector2 knockback,
            CombatAttackId attackId,
            int hitStopTicks,
            float screenShakeAmplitude,
            bool isGrab)
        {
            Attacker = attacker;
            Damage = damage;
            Knockback = knockback;
            AttackId = attackId;
            HitStopTicks = hitStopTicks;
            ScreenShakeAmplitude = screenShakeAmplitude;
            IsGrab = isGrab;
        }
    }
}
