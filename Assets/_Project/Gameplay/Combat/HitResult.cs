using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Flags de contexto do golpe, combinaveis via bitwise.
    /// </summary>
    [System.Flags]
    public enum HitFlags : byte
    {
        None = 0,
        IsProjectile = 1,
        IsHeavy = 2,
        BreaksGuard = 4
    }

    /// <summary>
    /// Dados enriquecidos de um golpe do lado do receptor.
    /// Combina informacoes do atacante (via CombatHitInfo) com dados do itr/receptor.
    /// </summary>
    public readonly struct HitResult
    {
        public readonly int Damage;
        public readonly Vector2 Knockback;
        public readonly int Fall;
        public readonly int Bdefend;
        public readonly StatusEffect Effect;
        public readonly int AttackerFacing;
        public readonly HitFlags Flags;
        public readonly CombatAttackId AttackId;
        public readonly int HitStopTicks;
        public readonly float ScreenShakeAmplitude;
        public readonly GameObject Attacker;

        public HitResult(
            int damage,
            Vector2 knockback,
            int fall,
            int bdefend,
            StatusEffect effect,
            int attackerFacing,
            HitFlags flags,
            CombatAttackId attackId,
            int hitStopTicks,
            float screenShakeAmplitude,
            GameObject attacker)
        {
            Damage = damage;
            Knockback = knockback;
            Fall = fall;
            Bdefend = bdefend;
            Effect = effect;
            AttackerFacing = attackerFacing;
            Flags = flags;
            AttackId = attackId;
            HitStopTicks = hitStopTicks;
            ScreenShakeAmplitude = screenShakeAmplitude;
            Attacker = attacker;
        }

        /// <summary>
        /// Construtor de fabrica que ponta do pipeline existente (CombatHitInfo)
        /// para o formato enriquecido do reactive combat router.
        /// </summary>
        public static HitResult FromCombatHitInfo(
            in CombatHitInfo hit,
            int attackerFacing,
            int fall,
            int bdefend,
            StatusEffect effect,
            HitFlags flags)
        {
            return new HitResult(
                damage: hit.Damage,
                knockback: hit.Knockback,
                fall: fall,
                bdefend: bdefend,
                effect: effect,
                attackerFacing: attackerFacing,
                flags: flags,
                attackId: hit.AttackId,
                hitStopTicks: hit.HitStopTicks,
                screenShakeAmplitude: hit.ScreenShakeAmplitude,
                attacker: hit.Attacker);
        }
    }
}
