#if false
using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDefinition_Grunt", menuName = "_Project/Gameplay/Enemy Definition", order = 0)]
    public sealed class EnemyDefinition : ScriptableObject
    {
        public string id = "enemy.grunt.01";
        public string displayName = "Grunt";
        [Min(1)] public int maxHealth = 20;
        [Min(0f)] public float moveSpeed = 2.5f;
        [Min(0f)] public float touchDamage = 5f;
        [Min(0.05f)] public float thinkInterval = 0.08f;
        [Min(0f)] public float xpDrop = 1f;
        public Color tint = new Color(1f, 0.75f, 0.75f, 1f);
        [Min(0.25f)] public float scale = 1f;
        [Min(0f)] public float hitFlashSeconds = 0.06f;

        public bool isRanged;
        [Min(0f)] public float rangedMinDistance = 5f;
        [Min(0f)] public float rangedMaxDistance = 8f;
        [Min(0f)] public float fleeDistance = 3f;
        [Min(0.05f)] public float shotCooldown = 1.2f;
        [Range(0f, 1f)] public float defendChance = 0.2f;
        public int attackFrameId = 60;
        public int projectileOid = 1;

        public Lf2EnemyPersonality personality = Lf2EnemyPersonality.Aggressive;
        public string characterDatName;
    }
}
#endif
