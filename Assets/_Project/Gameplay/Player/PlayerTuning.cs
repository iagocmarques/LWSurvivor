using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerTuning_Default", menuName = "_Project/Gameplay/Player Tuning", order = 0)]
    public sealed class PlayerTuning : ScriptableObject
    {
        [Header("Move")]
        [Min(0f)] public float moveSpeed = 6f;
        [Range(0f, 1f)] public float inputDeadzone = 0.15f;

        [Header("Dash")]
        [Min(0f)] public float dashSpeed = 12f;
        [Min(0f)] public float dashDuration = 0.18f;
        [Min(0f)] public float dashCooldown = 0.65f;
        [Min(0f)] public float dashInvulnDuration = 0.10f;

        [Header("Attack")]
        [Min(0f)] public float attackDuration = 0.22f;

        [Header("Hitstun")]
        [Min(0f)] public float hitstunDuration = 0.25f;
        [Min(0f)] public float knockbackSpeed = 10f;

        [Header("Reactive Combat")]
        [Min(0f)] public float guardBreakThreshold = 15f;    // damage >= this breaks guard
        [Min(0f)] public float airLaunchThreshold = 5f;      // knockback.y >= this launches into HurtAir
        [Min(0)] public int lyingDurationTicks = 60;          // ticks on ground before auto-getup (1 sec)
        [Min(0)] public int getUpDurationTicks = 15;          // getup animation length in ticks
        [Min(0)] public int defendHitStunTicks = 6;           // micro stun ticks when blocking a hit
        [Min(0)] public int invulnOnGetUpTicks = 10;          // i-frames after getting up
        [Range(0f, 1f)] public float defendDamageReduction = 0.5f; // 0=no damage, 1=full damage while blocking
        [Min(0f)] public float gravityPerTick = 0.5f;         // gravity applied per tick during HurtAir
        public StatusEffectTuning statusEffectTuning;          // optional: status effect durations/tuning
    }
}