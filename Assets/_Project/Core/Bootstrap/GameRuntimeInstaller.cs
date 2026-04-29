using Project.Data;
using Project.Gameplay.Combat;
using Project.Gameplay.Enemies;
using Project.Gameplay.Player;
using Project.Gameplay.Run;
using Project.UI.Debug;
using UnityEngine;

namespace Project.Core.Bootstrap
{
    public sealed class GameRuntimeInstaller : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name != "Game")
                return;

            if (FindAnyObjectByType<GameRuntimeInstaller>() != null)
                return;

            var go = new GameObject("GameRuntimeInstaller");
            DontDestroyOnLoad(go);
            go.AddComponent<GameRuntimeInstaller>().Install();
        }

        private void Install()
        {
            var player = GameObject.Find("Player");
            if (player != null)
            {
                if (player.GetComponent<PlayerRuntimeStats>() == null)
                    player.AddComponent<PlayerRuntimeStats>();
                if (player.GetComponent<Health>() == null)
                    player.AddComponent<Health>();
                if (player.GetComponent<Damageable>() == null)
                    player.AddComponent<Damageable>();
                if (player.GetComponent<CircleCollider2D>() == null)
                {
                    var c = player.AddComponent<CircleCollider2D>();
                    c.isTrigger = true;
                    c.radius = 0.35f;
                }
                if (player.GetComponent<Rigidbody2D>() == null)
                {
                    var rb = player.AddComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.simulated = true;
                }
            }

            var run = FindAnyObjectByType<RunManager>();
            if (run == null)
            {
                var go = new GameObject("RunManager");
                run = go.AddComponent<RunManager>();
                AssignDefaultUpgrades(run);
            }

            if (FindAnyObjectByType<EnemySpawnerDirector>() == null)
            {
                var go = new GameObject("EnemySpawnerDirector");
                go.AddComponent<EnemySpawnerDirector>();
            }

            if (FindAnyObjectByType<DamageableDummy>() == null && player != null)
            {
                var dummy = new GameObject("CombatDummy");
                dummy.transform.position = player.transform.position + new Vector3(1.5f, 0f, 0f);
                dummy.AddComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1f);
                var box = dummy.AddComponent<BoxCollider2D>();
                box.isTrigger = true;
                dummy.AddComponent<Health>();
                dummy.AddComponent<Damageable>();
                dummy.AddComponent<DamageableDummy>();
            }

            PlayerHealthHud.AutoInstallForRuntime();
        }

        private static void AssignDefaultUpgrades(RunManager run)
        {
            var upgrades = Resources.LoadAll<UpgradeDefinition>(string.Empty);
            if (upgrades == null || upgrades.Length == 0)
                return;
            run.SetUpgradesPool(upgrades);
        }
    }
}
