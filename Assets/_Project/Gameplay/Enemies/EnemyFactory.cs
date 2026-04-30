using Project.Core.Tick;
using Project.Data;
using Project.Gameplay.AI;
using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Enemies
{
    public static class EnemyFactory
    {
        public static GameObject CreateEnemy(
            Lf2CharacterData charData,
            byte[] datBytes,
            AIArchetype archetype,
            Transform target,
            Vector3 position,
            int maxHealth = 100,
            float gravity = -0.05f)
        {
            var go = new GameObject($"Enemy_{charData.Name ?? "Unknown"}");
            go.transform.position = position;

            int enemyLayer = LayerMask.NameToLayer("Enemies");
            if (enemyLayer >= 0)
                go.layer = enemyLayer;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 10;

            var health = go.AddComponent<Health>();
            health.ConfigureMaxHealth(maxHealth);

            var mana = go.AddComponent<Mana>();

            var motor = go.AddComponent<CharacterMotor>();
            motor.Configure(gravity);

            var definition = ScriptableObject.CreateInstance<CharacterDefinition>();
            definition.lf2Id = 0;
            definition.displayName = charData.Name ?? "Enemy";
            definition.movement = CharacterMovementConfig.FromLf2Movement(charData.Movement);
            definition.rawDatBytes = datBytes;
            definition.frameRoleIds = charData.RoleIds ?? Lf2FrameRoleIds.BuildFromCharacterData(charData);

            var hsm = go.AddComponent<PlayerHsmController>();
            hsm.Configure(definition, charData);

            var damageable = go.AddComponent<Damageable>();

            var collider = go.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            collider.size = new Vector2(0.8f, 1.6f);

            var brain = go.AddComponent<EnemyBrain>();
            brain.Initialize(hsm.Lf2Sm, archetype);
            brain.SetTarget(target);

            return go;
        }

        public static GameObject CreateBandit(
            Lf2CharacterData charData,
            byte[] datBytes,
            Transform target,
            Vector3 position)
        {
            return CreateEnemy(charData, datBytes, AIArchetypePresets.CreateBandit(), target, position);
        }

        public static GameObject CreateHunter(
            Lf2CharacterData charData,
            byte[] datBytes,
            Transform target,
            Vector3 position)
        {
            return CreateEnemy(charData, datBytes, AIArchetypePresets.CreateHunter(), target, position);
        }
    }
}
