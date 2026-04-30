using UnityEngine;

namespace Project.Gameplay.AI
{
    public static class AIArchetypePresets
    {
        public static AIArchetype CreateBandit()
        {
            var arch = ScriptableObject.CreateInstance<AIArchetype>();
            arch.attackRange = 1.5f;
            arch.retreatRange = 4f;
            arch.attackCooldown = 1.5f;
            arch.aggression = 0.3f;
            arch.defendChance = 0.2f;
            arch.thinkInterval = 0.15f;
            arch.defendDuration = 0.5f;
            arch.recoverDuration = 1f;
            arch.usesRangedAttacks = false;
            arch.usesSpecials = false;
            return arch;
        }

        public static AIArchetype CreateBoss()
        {
            var arch = ScriptableObject.CreateInstance<AIArchetype>();
            arch.attackRange = 2f;
            arch.retreatRange = 6f;
            arch.attackCooldown = 1.2f;
            arch.aggression = 0.8f;
            arch.defendChance = 0.1f;
            arch.thinkInterval = 0.08f;
            arch.defendDuration = 0.4f;
            arch.recoverDuration = 0.6f;
            arch.usesRangedAttacks = false;
            arch.usesSpecials = true;
            return arch;
        }

        public static AIArchetype CreateHunter()
        {
            var arch = ScriptableObject.CreateInstance<AIArchetype>();
            arch.attackRange = 4f;
            arch.retreatRange = 2f;
            arch.attackCooldown = 2f;
            arch.aggression = 0.5f;
            arch.defendChance = 0.4f;
            arch.thinkInterval = 0.1f;
            arch.defendDuration = 0.5f;
            arch.recoverDuration = 1f;
            arch.usesRangedAttacks = true;
            arch.usesSpecials = false;
            return arch;
        }
    }
}
