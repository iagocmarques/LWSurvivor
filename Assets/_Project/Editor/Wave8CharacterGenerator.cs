#if UNITY_EDITOR
using Project.Data;
using Project.Gameplay.AI;
using Project.Gameplay.LF2;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    public static class Wave8CharacterGenerator
    {
        private const string CharacterFolder = "Assets/_Project/Data/Characters";
        private const string AIFolder = "Assets/_Project/Data/AI";

        [MenuItem("Tools/LF2/Generate Wave 8 Characters", false, 200)]
        public static void Generate()
        {
            EnsureFolder(CharacterFolder);
            EnsureFolder(AIFolder);

            // Regular characters
            CreateCharacter(13, "Mark");
            CreateCharacter(14, "Jack");
            CreateCharacter(15, "Sorcerer");
            CreateCharacter(16, "Monk");
            CreateCharacter(17, "Jan");
            CreateCharacter(18, "Knight");
            CreateCharacter(19, "Justin");
            CreateCharacter(20, "Firzen");
            CreateCharacter(21, "Bat");

            // Boss characters with AI archetypes
            CreateBossCharacter(22, "Julian");
            CreateBossCharacter(23, "LouisEX");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[Wave8CharacterGenerator] All 11 Wave 8 characters generated!");
        }

        private static void CreateCharacter(int id, string name)
        {
            var path = $"{CharacterFolder}/{name}.asset";
            var so = LoadOrCreate<CharacterDefinition>(path);

            so.lf2Id = id;
            so.displayName = name;
            so.movement = DefaultMovement();
            so.reactiveMoves = null;
            so.rawDatBytes = null;
            so.frameRoleIds = new Lf2FrameRoleIds();

            EditorUtility.SetDirty(so);
        }

        private static void CreateBossCharacter(int id, string name)
        {
            CreateCharacter(id, name);

            var archetypePath = $"{AIFolder}/AI_{name}.asset";
            var archetype = LoadOrCreate<AIArchetype>(archetypePath);

            archetype.attackRange = 2f;
            archetype.retreatRange = 6f;
            archetype.attackCooldown = 1.2f;
            archetype.aggression = 0.8f;
            archetype.defendChance = 0.1f;
            archetype.thinkInterval = 0.08f;
            archetype.defendDuration = 0.4f;
            archetype.recoverDuration = 0.6f;
            archetype.usesRangedAttacks = false;
            archetype.usesSpecials = true;

            EditorUtility.SetDirty(archetype);
        }

        private static CharacterMovementConfig DefaultMovement()
        {
            return new CharacterMovementConfig
            {
                walkSpeed = 5f,
                walkAccel = 0.5f,
                runSpeed = 8f,
                runAccel = 0.5f,
                jumpPower = 12f,
                jumpForwardSpeed = 5f,
                gravity = -0.5f,
                dashSpeed = 10f,
                dashDuration = 0.3f,
                defendSpeedMultiplier = 0.5f,
                lyingDurationTicks = 60,
                invulnOnGetUpTicks = 30
            };
        }

        private static T LoadOrCreate<T>(string assetPath) where T : ScriptableObject
        {
            var existing = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (existing != null)
                return existing;

            var instance = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(instance, assetPath);
            return instance;
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;

            var parts = path.Split('/');
            var current = parts[0];
            for (var i = 1; i < parts.Length; i++)
            {
                var next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }
    }
}
#endif
