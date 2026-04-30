using UnityEngine;

namespace Project.Gameplay.AI
{
    [CreateAssetMenu(fileName = "NewArchetype", menuName = "_Project/AI/AI Archetype", order = 0)]
    public sealed class AIArchetype : ScriptableObject
    {
        [Header("Distances")]
        public float attackRange = 2f;
        public float retreatRange = 5f;

        [Header("Timing")]
        public float attackCooldown = 1f;
        public float defendDuration = 0.5f;
        public float recoverDuration = 1f;

        [Header("Behavior")]
        [Range(0f, 1f)] public float aggression = 0.5f;
        [Range(0f, 1f)] public float defendChance = 0.3f;
        public bool usesRangedAttacks;
        public bool usesSpecials;

        [Header("Think Rate")]
        public float thinkInterval = 0.1f;
    }
}
