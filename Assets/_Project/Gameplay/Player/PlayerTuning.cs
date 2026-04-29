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
    }
}