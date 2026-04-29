using Project.Data;
using Project.Gameplay.Combat;
using Project.Gameplay.Enemies;
using Project.Gameplay.Player;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    /// <summary>
    /// Gera todos os assets MVP (PlayerTuning, AttackDefinition, EnemyDefinition, CharacterDefinition)
    /// via menu Tools/LF2/Generate MVP Assets.
    /// </summary>
    public static class MvpAssetGenerator
    {
        private const string DataFolder = "Assets/_Project/Data";

        [MenuItem("Tools/LF2/Generate MVP Assets", false, 100)]
        public static void GenerateAll()
        {
            EnsureFolder(DataFolder);

            var playerTuning = CreatePlayerTuning();
            var jab = CreateAttackJab();
            var launcher = CreateAttackLauncher();
            var dashAttack = CreateAttackDashAttack();
            CreateEnemyDefinitionBandit();
            CreateCharacterDefinitionDavis(playerTuning, jab, launcher, dashAttack);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("[MvpAssetGenerator] All MVP assets generated under " + DataFolder);
        }

        // ───────────────────────────────────────────────
        //  PlayerTuning
        // ───────────────────────────────────────────────

        private static PlayerTuning CreatePlayerTuning()
        {
            const string path = DataFolder + "/PlayerTuning_Davis.asset";
            var so = LoadOrCreate<PlayerTuning>(path);
            so.moveSpeed = 6f;
            so.inputDeadzone = 0.15f;
            so.dashSpeed = 12f;
            so.dashDuration = 0.18f;
            so.dashCooldown = 0.65f;
            so.dashInvulnDuration = 0.10f;
            so.attackDuration = 0.22f;
            so.hitstunDuration = 0.25f;
            so.knockbackSpeed = 10f;
            EditorUtility.SetDirty(so);
            return so;
        }

        // ───────────────────────────────────────────────
        //  AttackDefinition — Jab
        // ───────────────────────────────────────────────

        private static AttackDefinition CreateAttackJab()
        {
            const string path = DataFolder + "/Attack_Jab.asset";
            var so = LoadOrCreate<AttackDefinition>(path);
            so.attackId = CombatAttackId.Jab;
            so.durationTicks = 16;
            so.usePerFrameHitboxes = true;

            // hitboxFrames — single entry at tick 3
            var hitboxData = new HitboxFrameDefinition
            {
                startTick = 3,
                endTick = 3,
                localOffset = new Vector2(0.5f, 0f),
                halfExtents = new Vector2(0.5f, 0.4f),
                damage = 8,
                knockback = new Vector2(6f, 0f),
                hitStopTicks = 2,
                screenShakeAmplitude = 0.12f,
                isGrab = false
            };
            SetHitboxFrames(so, new[] { hitboxData });

            so.cancelWindowStartTick = 8;
            so.cancelWindowEndTick = 14;
            so.allowedCancels = new[] { CombatAttackId.Jab, CombatAttackId.Launcher };

            EditorUtility.SetDirty(so);
            return so;
        }

        // ───────────────────────────────────────────────
        //  AttackDefinition — Launcher
        // ───────────────────────────────────────────────

        private static AttackDefinition CreateAttackLauncher()
        {
            const string path = DataFolder + "/Attack_Launcher.asset";
            var so = LoadOrCreate<AttackDefinition>(path);
            so.attackId = CombatAttackId.Launcher;
            so.durationTicks = 24;
            so.usePerFrameHitboxes = true;

            var hitboxData = new HitboxFrameDefinition
            {
                startTick = 4,
                endTick = 4,
                localOffset = new Vector2(0.3f, 0.5f),
                halfExtents = new Vector2(0.4f, 0.5f),
                damage = 14,
                knockback = new Vector2(3f, 8f),
                hitStopTicks = 4,
                screenShakeAmplitude = 0.2f,
                isGrab = false
            };
            SetHitboxFrames(so, new[] { hitboxData });

            so.cancelWindowStartTick = 16;
            so.cancelWindowEndTick = 20;
            so.allowedCancels = new[] { CombatAttackId.Jab };

            EditorUtility.SetDirty(so);
            return so;
        }

        // ───────────────────────────────────────────────
        //  AttackDefinition — Dash Attack
        // ───────────────────────────────────────────────

        private static AttackDefinition CreateAttackDashAttack()
        {
            const string path = DataFolder + "/Attack_DashAttack.asset";
            var so = LoadOrCreate<AttackDefinition>(path);
            so.attackId = CombatAttackId.DashAttack;
            so.durationTicks = 16;
            so.usePerFrameHitboxes = true;

            var hitboxData = new HitboxFrameDefinition
            {
                startTick = 2,
                endTick = 2,
                localOffset = new Vector2(0.6f, 0f),
                halfExtents = new Vector2(0.6f, 0.4f),
                damage = 10,
                knockback = new Vector2(8f, 2f),
                hitStopTicks = 3,
                screenShakeAmplitude = 0.15f,
                isGrab = false
            };
            SetHitboxFrames(so, new[] { hitboxData });

            // No cancel window for dash attack
            so.cancelWindowStartTick = 0;
            so.cancelWindowEndTick = 0;
            so.allowedCancels = System.Array.Empty<CombatAttackId>();

            EditorUtility.SetDirty(so);
            return so;
        }

        // ───────────────────────────────────────────────
        //  EnemyDefinition — Bandit
        // ───────────────────────────────────────────────

        private static EnemyDefinition CreateEnemyDefinitionBandit()
        {
            const string path = DataFolder + "/EnemyDefinition_Bandit.asset";
            var so = LoadOrCreate<EnemyDefinition>(path);
            so.id = "enemy.bandit.01";
            so.displayName = "Bandit";
            so.maxHealth = 20;
            so.moveSpeed = 2.8f;
            so.touchDamage = 5f;
            so.thinkInterval = 0.08f;
            so.xpDrop = 1f;
            so.tint = new Color(1f, 1f, 1f, 1f);
            so.scale = 1f;
            so.hitFlashSeconds = 0.08f;
            EditorUtility.SetDirty(so);
            return so;
        }

        // ───────────────────────────────────────────────
        //  CharacterDefinition — Davis
        // ───────────────────────────────────────────────

        private static void CreateCharacterDefinitionDavis(
            PlayerTuning tuning,
            AttackDefinition jab,
            AttackDefinition launcher,
            AttackDefinition dashAttack)
        {
            const string path = DataFolder + "/CharacterDefinition_Davis.asset";
            var so = LoadOrCreate<CharacterDefinition>(path);
            so.id = "hero.davis.01";
            so.displayName = "Davis";
            so.tuning = tuning;
            so.jab = jab;
            so.launcher = launcher;
            so.dashAttack = dashAttack;
            EditorUtility.SetDirty(so);
        }

        // ───────────────────────────────────────────────
        //  Helpers
        // ───────────────────────────────────────────────

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
            var current = parts[0]; // "Assets"
            for (var i = 1; i < parts.Length; i++)
            {
                var next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        /// <summary>
        /// Uses SerializedObject/SerializedProperty to set the hitboxFrames array
        /// on an AttackDefinition, ensuring Unity serializes the changes correctly.
        /// </summary>
        private static void SetHitboxFrames(AttackDefinition attack, HitboxFrameDefinition[] frames)
        {
            var so = new SerializedObject(attack);
            var prop = so.FindProperty("hitboxFrames");
            prop.arraySize = frames.Length;

            for (var i = 0; i < frames.Length; i++)
            {
                var element = prop.GetArrayElementAtIndex(i);
                element.FindPropertyRelative("startTick").intValue = frames[i].startTick;
                element.FindPropertyRelative("endTick").intValue = frames[i].endTick;
                element.FindPropertyRelative("localOffset").vector2Value = frames[i].localOffset;
                element.FindPropertyRelative("halfExtents").vector2Value = frames[i].halfExtents;
                element.FindPropertyRelative("damage").intValue = frames[i].damage;
                element.FindPropertyRelative("knockback").vector2Value = frames[i].knockback;
                element.FindPropertyRelative("hitStopTicks").intValue = frames[i].hitStopTicks;
                element.FindPropertyRelative("screenShakeAmplitude").floatValue = frames[i].screenShakeAmplitude;
                element.FindPropertyRelative("isGrab").boolValue = frames[i].isGrab;
            }

            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
