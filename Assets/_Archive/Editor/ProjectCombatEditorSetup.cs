#if false
using System;
using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Project.Editor.Combat
{
    /// <summary>
    /// Aplica de uma vez: matriz 2D Hitbox/Hurtbox, assets Attack_*, cena Game (player, dummy, HUD).
    /// Menus (no topo do Unity, ao lado de File / Edit / …): procura <b>Combate</b> ou <b>Tools &gt; Combate</b>.
    /// Não precisas de adicionar nada "à mão" ao personagem: este comando liga as referências no Inspector.
    /// </summary>
    public static class ProjectCombatEditorSetup
    {
        public const string GameScenePath = "Assets/_Project/Gameplay/Scenes/Game.unity";
        public const string DefinitionsFolder = "Assets/_Project/Gameplay/Combat/Definitions";

        public const string PathJab = DefinitionsFolder + "/Attack_Jab.asset";
        public const string PathLauncher = DefinitionsFolder + "/Attack_Launcher.asset";
        public const string PathDash = DefinitionsFolder + "/Attack_Dash.asset";

        // Vários atalhos: "Combate" e "Ferramentas" são fáceis de achar. O item "Project" confundia
        // com o teclado/Project (janela de ficheiros) — deixou de ser o menu principal.
        [MenuItem("Combate/Configurar tudo: Game.unity (ataques, dummy, HUD)", false, 0)]
        [MenuItem("Tools/Combate/Configurar tudo: Game.unity (ataques, dummy, HUD)")]
        [MenuItem("Window/Combate/Configurar tudo: Game.unity (ataques, dummy, HUD)")]
        public static void ApplyAll()
        {
            try
            {
                ApplyPhysics2D();
                var jab = CreateOrUpdateAttackJab();
                var launcher = CreateOrUpdateAttackLauncher();
                var dash = CreateOrUpdateAttackDash();

                var scene = EditorSceneManager.OpenScene(GameScenePath, OpenSceneMode.Single);
                if (!scene.IsValid())
                {
                    Debug.LogError("Cena inválida: " + GameScenePath);
                    return;
                }

                // Evita dependência de assinaturas de FindAnyObjectByType/FindObjectsByType (diferem por versão de Unity 6).
                var playerGo = GameObject.Find("Player");
                var player = playerGo != null ? playerGo.GetComponent<PlayerHsmController>() : null;
                if (player == null)
                {
                    Debug.LogError("PlayerHsmController não encontrado: precisa de um GameObject \"Player\" em Game.unity com esse componente.");
                    return;
                }

                Undo.RecordObject(player, "Wire combat (PlayerHsmController)");
                var hsmSo = new SerializedObject(player);
                hsmSo.FindProperty("jabDefinition").objectReferenceValue = jab;
                hsmSo.FindProperty("launcherDefinition").objectReferenceValue = launcher;
                hsmSo.FindProperty("dashAttackDefinition").objectReferenceValue = dash;
                var mask = LayerMask.GetMask("Hurtbox");
                if (mask == 0)
                {
                    Debug.LogWarning("Layer \"Hurtbox\" não encontrada; enemyHurtboxMask fica tudo (legacy).");
                    hsmSo.FindProperty("enemyHurtboxMask").intValue = ~0;
                }
                else
                {
                    hsmSo.FindProperty("enemyHurtboxMask").intValue = mask;
                }

                hsmSo.ApplyModifiedProperties();

                var playerT = player.transform;
                var dummy = CreateOrUpdateDummy(playerT, player);
                var hud = CreateOrUpdateCombatHud(player);

                EditorUtility.SetDirty(player);
                if (dummy != null)
                    EditorUtility.SetDirty(dummy);
                if (hud != null)
                    EditorUtility.SetDirty(hud);

                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
                AssetDatabase.SaveAssets();
                Debug.Log("Combat: setup concluído (física 2D, 3x AttackDefinition, player, dummy, HUD).");
            }
            catch (Exception e)
            {
                Debug.LogError("Combat setup falhou: " + e);
            }
        }

        public static void ApplyPhysics2D()
        {
            var hit = LayerMask.NameToLayer("Hitbox");
            var hurt = LayerMask.NameToLayer("Hurtbox");
            if (hit < 0 || hurt < 0)
            {
                Debug.LogError("Layers Hitbox e/ou Hurtbox em falta. Project Settings → Tags and Layers.");
                return;
            }

            // Hitbox ↔ Hurtbox: colidir
            Physics2D.IgnoreLayerCollision(hit, hurt, false);
            // Hitbox ↔ Hitbox: não colide (só dano em hurtbox)
            Physics2D.IgnoreLayerCollision(hit, hit, true);
        }

        public static AttackDefinition CreateOrUpdateAttackJab()
        {
            EnsureFolder(DefinitionsFolder);
            return CreateOrUpdate(PathJab, a =>
            {
                a.attackId = CombatAttackId.Jab;
                a.durationTicks = 18;
                a.hitboxStartTick = 3;
                a.hitboxEndTick = 5;
                a.hitboxLocalOffset = new Vector2(0.65f, 0.05f);
                a.hitboxHalfExtents = new Vector2(0.55f, 0.32f);
                a.damage = 5;
                a.knockback = new Vector2(5.5f, 0f);
                a.cancelWindowStartTick = 10;
                a.cancelWindowEndTick = 14;
                a.allowedCancels = new[] { CombatAttackId.Launcher };
                a.usePerFrameHitboxes = true;
                a.hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = 3,
                        endTick = 5,
                        localOffset = new Vector2(0.65f, 0.05f),
                        halfExtents = new Vector2(0.55f, 0.32f),
                        damage = 5,
                        knockback = new Vector2(5.5f, 0f),
                        hitStopTicks = 2,
                        screenShakeAmplitude = 0.12f,
                        isGrab = false
                    }
                };
            });
        }

        public static AttackDefinition CreateOrUpdateAttackLauncher()
        {
            EnsureFolder(DefinitionsFolder);
            return CreateOrUpdate(PathLauncher, a =>
            {
                a.attackId = CombatAttackId.Launcher;
                a.durationTicks = 22;
                a.hitboxStartTick = 4;
                a.hitboxEndTick = 6;
                a.hitboxLocalOffset = new Vector2(0.65f, 0.12f);
                a.hitboxHalfExtents = new Vector2(0.55f, 0.36f);
                a.damage = 8;
                a.knockback = new Vector2(2f, 10f);
                a.cancelWindowStartTick = 12;
                a.cancelWindowEndTick = 18;
                a.allowedCancels = Array.Empty<CombatAttackId>();
                a.usePerFrameHitboxes = true;
                a.hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = 4,
                        endTick = 6,
                        localOffset = new Vector2(0.65f, 0.12f),
                        halfExtents = new Vector2(0.55f, 0.36f),
                        damage = 8,
                        knockback = new Vector2(2f, 10f),
                        hitStopTicks = 3,
                        screenShakeAmplitude = 0.16f,
                        isGrab = false
                    }
                };
            });
        }

        public static AttackDefinition CreateOrUpdateAttackDash()
        {
            EnsureFolder(DefinitionsFolder);
            return CreateOrUpdate(PathDash, a =>
            {
                a.attackId = CombatAttackId.DashAttack;
                a.durationTicks = 14;
                a.hitboxStartTick = 2;
                a.hitboxEndTick = 4;
                a.hitboxLocalOffset = new Vector2(0.6f, 0.05f);
                a.hitboxHalfExtents = new Vector2(0.5f, 0.3f);
                a.damage = 6;
                a.knockback = new Vector2(4f, 0.5f);
                a.cancelWindowStartTick = 6;
                a.cancelWindowEndTick = 10;
                a.allowedCancels = new[] { CombatAttackId.Jab };
                a.usePerFrameHitboxes = true;
                a.hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = 2,
                        endTick = 4,
                        localOffset = new Vector2(0.6f, 0.05f),
                        halfExtents = new Vector2(0.5f, 0.3f),
                        damage = 6,
                        knockback = new Vector2(4f, 0.5f),
                        hitStopTicks = 2,
                        screenShakeAmplitude = 0.12f,
                        isGrab = false
                    }
                };
            });
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;
            if (!path.StartsWith("Assets/", StringComparison.Ordinal))
                return;
            var parts = path.Substring("Assets/".Length).Split('/');
            var parent = "Assets";
            for (var i = 0; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(parts[i]))
                    continue;
                var next = parent + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(parent, parts[i]);
                parent = next;
            }
        }

        private static AttackDefinition CreateOrUpdate(string assetPath, Action<AttackDefinition> init)
        {
            var a = AssetDatabase.LoadAssetAtPath<AttackDefinition>(assetPath);
            if (a == null)
            {
                a = ScriptableObject.CreateInstance<AttackDefinition>();
                init(a);
                AssetDatabase.CreateAsset(a, assetPath);
            }
            else
            {
                Undo.RecordObject(a, "Atualizar AttackDefinition");
                init(a);
                EditorUtility.SetDirty(a);
            }

            return a;
        }

        private static GameObject CreateOrUpdateDummy(Transform player, PlayerHsmController hsm)
        {
            const string goName = "CombatDummy";
            var existing = GameObject.Find(goName);
            GameObject go;
            if (existing == null)
            {
                go = new GameObject(goName);
                Undo.RegisterCreatedObjectUndo(go, "Create " + goName);
                go.transform.SetParent(null);
            }
            else
            {
                go = existing;
            }

            var p = player.position;
            go.transform.SetPositionAndRotation(
                new Vector3(p.x + 1.5f, p.y, p.z),
                Quaternion.identity);
            go.transform.localScale = Vector3.one;

            var hurt = LayerMask.NameToLayer("Hurtbox");
            if (hurt >= 0)
                go.layer = hurt;

            if (go.GetComponent<SpriteRenderer>() == null)
            {
                var pr = hsm != null && hsm.TryGetComponent<SpriteRenderer>(out var psr) ? psr : null;
                var sr = Undo.AddComponent<SpriteRenderer>(go);
                if (pr != null)
                {
                    sr.sprite = pr.sprite;
                    sr.color = new Color(1f, 0.55f, 0.55f, 1f);
                }
            }

            if (go.GetComponent<BoxCollider2D>() == null)
                Undo.AddComponent<BoxCollider2D>(go);
            var box = go.GetComponent<BoxCollider2D>();
            box.isTrigger = true;
            box.size = new Vector2(0.9f, 1.2f);

            if (go.GetComponent<DamageableDummy>() == null)
                Undo.AddComponent<DamageableDummy>(go);
            if (go.GetComponent<Health>() == null)
                Undo.AddComponent<Health>(go);
            if (go.GetComponent<Damageable>() == null)
                Undo.AddComponent<Damageable>(go);

            return go;
        }

        private static GameObject CreateOrUpdateCombatHud(PlayerHsmController player)
        {
            const string goName = "CombatInputDebug";
            var existing = GameObject.Find(goName);
            GameObject go;
            if (existing == null)
            {
                go = new GameObject(goName);
                Undo.RegisterCreatedObjectUndo(go, "Create " + goName);
            }
            else
            {
                go = existing;
            }

            var hud = go.GetComponent<CombatInputDebugHud>();
            if (hud == null)
                hud = Undo.AddComponent<CombatInputDebugHud>(go);

            Undo.RecordObject(hud, "Ligar CombatInputDebugHud");
            var so = new SerializedObject(hud);
            so.FindProperty("player").objectReferenceValue = player;
            so.FindProperty("show").boolValue = true;
            so.FindProperty("ringLines").intValue = 12;
            so.ApplyModifiedProperties();
            return go;
        }
    }
}
#endif
