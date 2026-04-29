using Project.Data;
using Project.Gameplay.Combat;
using Project.Gameplay.Enemies;
using Project.Gameplay.Player;
using Project.Gameplay.Rendering;
using Project.Gameplay.Run;
using Project.Gameplay.Visual;
using Project.UI.Debug;
using UnityEngine;

namespace Project.Core.Bootstrap
{
    public sealed class GameRuntimeInstaller : MonoBehaviour
    {
        // ── Runtime SO singletons (persist for the session) ──────────────
        private static PlayerTuning _playerTuning;
        private static AttackDefinition _jab;
        private static AttackDefinition _launcher;
        private static AttackDefinition _dashAttack;
        private static EnemyDefinition _bandit;
        private static ReactiveMoveSet _davisReactiveMoves;
        private static CharacterDefinition _davis;
        private static DefinitionRegistry _registry;

        public static PlayerTuning PlayerTuning => _playerTuning;
        public static CharacterDefinition Davis => _davis;
        public static DefinitionRegistry Registry => _registry;

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
            // ── 1. Create all MVP ScriptableObject instances ────────────
            _playerTuning = MvpSoFactory.CreatePlayerTuning();
            _jab = MvpSoFactory.CreateJab();
            _launcher = MvpSoFactory.CreateLauncher();
            _dashAttack = MvpSoFactory.CreateDashAttack();
            _bandit = MvpSoFactory.CreateBandit();
            _davis = MvpSoFactory.CreateDavis(_playerTuning, _jab, _launcher, _dashAttack);
            _davisReactiveMoves = MvpSoFactory.CreateDavisReactiveMoveSet();
            _registry = MvpSoFactory.CreateRegistry(_davis, _bandit);

            // ── 2. Create / find Player ───────────────────────────────
            var player = GameObject.Find("Player");
            if (player == null)
            {
                player = new GameObject("Player");
                var cam = Camera.main;
                if (cam != null)
                    player.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);

                var sr = player.AddComponent<SpriteRenderer>();
                Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
                sr.sprite = CreatePlaceholderSprite();
                sr.color = new Color(1f, 0.95f, 0.35f, 1f);

                player.AddComponent<PlayerHsmController>();
            }

            if (player != null)
            {
                Sprite2DMaterialUtility.EnsureCompatibleMaterial(player.GetComponent<SpriteRenderer>());

                // Remove stale components from previous dev sessions
                var oldController = player.GetComponent<Player25DController>();
                if (oldController != null)
                    Destroy(oldController);

                if (player.GetComponent<PlayerRuntimeStats>() == null)
                    player.AddComponent<PlayerRuntimeStats>();
                if (player.GetComponent<Health>() == null)
                    player.AddComponent<Health>();
                if (player.GetComponent<CharacterMotor>() == null)
                    player.AddComponent<CharacterMotor>();
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

                if (player.GetComponent<DepthSortByY>() == null)
                    player.AddComponent<DepthSortByY>();
                if (player.GetComponent<Lf2PlayerSpriteAnimator>() == null)
                    player.AddComponent<Lf2PlayerSpriteAnimator>();
                if (player.GetComponent<ReactiveCombatDebugOverlay>() == null)
                    player.AddComponent<ReactiveCombatDebugOverlay>();
                if (player.GetComponent<ArenaBounds>() == null)
                    player.AddComponent<ArenaBounds>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (player.GetComponent<TestAttacker>() == null)
                    player.AddComponent<TestAttacker>();
#endif

                var hsm = player.GetComponent<PlayerHsmController>();
                if (hsm == null)
                    hsm = player.AddComponent<PlayerHsmController>();
                hsm.Configure(_playerTuning, _jab, _launcher, _dashAttack, _davisReactiveMoves);
            }

            // ── Wire Camera ──────────────────────────────────────────────
            var mainCam = Camera.main;
            if (mainCam != null && mainCam.GetComponent<CameraFollow2D>() == null)
                mainCam.gameObject.AddComponent<CameraFollow2D>();

            // ── 3. Wire RunManager ──────────────────────────────────────
            var run = FindAnyObjectByType<RunManager>();
            if (run == null)
            {
                var go = new GameObject("RunManager");
                run = go.AddComponent<RunManager>();
                AssignDefaultUpgrades(run);
            }

            // ── 4. Wire EnemySpawnerDirector ────────────────────────────
            var spawner = FindAnyObjectByType<EnemySpawnerDirector>();
            if (spawner == null)
            {
                var go = new GameObject("EnemySpawnerDirector");
                spawner = go.AddComponent<EnemySpawnerDirector>();
            }
            spawner.SetEnemyDefinition(_bandit);

            // ── 5. Combat dummy ─────────────────────────────────────────
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

        private void Update()
        {
            CombatReadabilityFx.Tick(Time.deltaTime);
        }

        private static Sprite CreatePlaceholderSprite()
        {
            const int size = 32;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;
            var pixels = new Color32[size * size];
            for (var i = 0; i < pixels.Length; i++) pixels[i] = new Color32(255, 255, 255, 255);
            tex.SetPixels32(pixels);
            tex.Apply(false, true);
            return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 32f);
        }

        private static void AssignDefaultUpgrades(RunManager run)
        {
            var upgrades = Resources.LoadAll<UpgradeDefinition>(string.Empty);
            if (upgrades == null || upgrades.Length == 0)
                return;
            run.SetUpgradesPool(upgrades);
        }

        // =================================================================
        //  MvpSoFactory -- programmatic SO creation for the MVP
        // =================================================================
        private static class MvpSoFactory
        {
            // ── PlayerTuning ────────────────────────────────────────────
            public static PlayerTuning CreatePlayerTuning()
            {
                var so = ScriptableObject.CreateInstance<PlayerTuning>();
                so.moveSpeed = 6f;
                so.inputDeadzone = 0.15f;
                so.dashSpeed = 12f;
                so.dashDuration = 0.18f;
                so.dashCooldown = 0.65f;
                so.dashInvulnDuration = 0.10f;
                so.attackDuration = 0.22f;
                so.hitstunDuration = 0.25f;
                so.knockbackSpeed = 10f;
                so.guardBreakThreshold = 15f;
                so.airLaunchThreshold = 5f;
                so.lyingDurationTicks = 60;
                so.getUpDurationTicks = 15;
                so.defendHitStunTicks = 6;
                so.invulnOnGetUpTicks = 10;
                so.defendDamageReduction = 0.5f;
                so.gravityPerTick = 0.5f;
                return so;
            }

            // ── Jab ─────────────────────────────────────────────────────
            public static AttackDefinition CreateJab()
            {
                var so = ScriptableObject.CreateInstance<AttackDefinition>();
                so.attackId = CombatAttackId.Jab;
                so.durationTicks = 18;
                so.hitboxStartTick = 3;
                so.hitboxEndTick = 5;
                so.damage = 8;
                so.knockback = new Vector2(6f, 0f);
                so.hitStopTicks = 3;
                so.screenShakeAmplitude = 0.12f;
                so.cancelWindowStartTick = 10;
                so.cancelWindowEndTick = 14;
                so.allowedCancels = new[] { CombatAttackId.Jab, CombatAttackId.Launcher };
                so.usePerFrameHitboxes = true;
                so.hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = 3,
                        endTick = 5,
                        localOffset = new Vector2(0.65f, 0.05f),
                        halfExtents = new Vector2(0.55f, 0.32f),
                        damage = 8,
                        knockback = new Vector2(6f, 0f),
                        hitStopTicks = 3,
                        screenShakeAmplitude = 0.12f,
                        isGrab = false
                    }
                };
                return so;
            }

            // ── Launcher ────────────────────────────────────────────────
            public static AttackDefinition CreateLauncher()
            {
                var so = ScriptableObject.CreateInstance<AttackDefinition>();
                so.attackId = CombatAttackId.Launcher;
                so.durationTicks = 24;
                so.hitboxStartTick = 4;
                so.hitboxEndTick = 7;
                so.damage = 12;
                so.knockback = new Vector2(3f, 8f);
                so.hitStopTicks = 4;
                so.screenShakeAmplitude = 0.18f;
                so.cancelWindowStartTick = 16;
                so.cancelWindowEndTick = 20;
                so.allowedCancels = new[] { CombatAttackId.Jab };
                so.usePerFrameHitboxes = true;
                so.hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = 4,
                        endTick = 7,
                        localOffset = new Vector2(0.5f, 0.3f),
                        halfExtents = new Vector2(0.6f, 0.5f),
                        damage = 12,
                        knockback = new Vector2(3f, 8f),
                        hitStopTicks = 4,
                        screenShakeAmplitude = 0.18f,
                        isGrab = false
                    }
                };
                return so;
            }

            // ── Dash Attack ─────────────────────────────────────────────
            public static AttackDefinition CreateDashAttack()
            {
                var so = ScriptableObject.CreateInstance<AttackDefinition>();
                so.attackId = CombatAttackId.DashAttack;
                so.durationTicks = 16;
                so.hitboxStartTick = 2;
                so.hitboxEndTick = 6;
                so.damage = 10;
                so.knockback = new Vector2(8f, 2f);
                so.hitStopTicks = 3;
                so.screenShakeAmplitude = 0.14f;
                so.cancelWindowStartTick = 0;
                so.cancelWindowEndTick = 0;
                so.allowedCancels = System.Array.Empty<CombatAttackId>();
                so.usePerFrameHitboxes = true;
                so.hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = 2,
                        endTick = 6,
                        localOffset = new Vector2(0.7f, 0.0f),
                        halfExtents = new Vector2(0.7f, 0.35f),
                        damage = 10,
                        knockback = new Vector2(8f, 2f),
                        hitStopTicks = 3,
                        screenShakeAmplitude = 0.14f,
                        isGrab = false
                    }
                };
                return so;
            }

            // ── Bandit (enemy) ──────────────────────────────────────────
            public static EnemyDefinition CreateBandit()
            {
                var so = ScriptableObject.CreateInstance<EnemyDefinition>();
                so.id = "enemy.bandit.01";
                so.displayName = "Bandit";
                so.maxHealth = 20;
                so.moveSpeed = 2.8f;
                so.touchDamage = 6f;
                so.thinkInterval = 0.08f;
                so.xpDrop = 1f;
                so.tint = new Color(1f, 0.75f, 0.75f, 1f);
                so.scale = 1f;
                so.hitFlashSeconds = 0.10f;
                return so;
            }

            // ── Davis (character) ───────────────────────────────────────
            public static CharacterDefinition CreateDavis(
                PlayerTuning tuning,
                AttackDefinition jab,
                AttackDefinition launcher,
                AttackDefinition dashAttack)
            {
                var so = ScriptableObject.CreateInstance<CharacterDefinition>();
                so.id = "character.davis.01";
                so.displayName = "Davis";
                so.tuning = tuning;
                so.jab = jab;
                so.launcher = launcher;
                so.dashAttack = dashAttack;
                return so;
            }

            // ── Davis Reactive Move Set ─────────────────────────────
            public static ReactiveMoveSet CreateDavisReactiveMoveSet()
            {
                var so = ScriptableObject.CreateInstance<ReactiveMoveSet>();

                // Defend (hold stance)
                so.defend = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.Defend,
                    loop = true,
                    nextStateOnFinish = ReactiveStateId.Defend,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 56, durationTicks = 12, lockInput = true, invulnerable = false, impulse = Vector2.zero },
                        new ReactiveFrameDefinition { picId = 57, durationTicks = 0, lockInput = true, invulnerable = false, impulse = Vector2.zero }
                    }
                };

                // DefendBreak (guard broken)
                so.defendBreak = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.DefendBreak,
                    loop = false,
                    nextStateOnFinish = ReactiveStateId.HurtGrounded,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 46, durationTicks = 2, lockInput = true, invulnerable = false, impulse = new Vector2(-3f, 0f) },
                        new ReactiveFrameDefinition { picId = 47, durationTicks = 3, lockInput = true, invulnerable = false, impulse = new Vector2(-2f, 0f) },
                        new ReactiveFrameDefinition { picId = 48, durationTicks = 4, lockInput = true, invulnerable = false, impulse = Vector2.zero }
                    }
                };

                // DefendHit (micro-stun while blocking)
                so.defendHit = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.DefendHit,
                    loop = false,
                    nextStateOnFinish = ReactiveStateId.Defend,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 56, durationTicks = 3, lockInput = true, invulnerable = false, impulse = new Vector2(-1f, 0f) },
                        new ReactiveFrameDefinition { picId = 57, durationTicks = 3, lockInput = true, invulnerable = false, impulse = Vector2.zero }
                    }
                };

                // HurtGrounded (hit while standing)
                so.hurtGrounded = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.HurtGrounded,
                    loop = false,
                    nextStateOnFinish = ReactiveStateId.HurtGrounded,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 30, durationTicks = 3, lockInput = true, invulnerable = false, impulse = new Vector2(-4f, 0f) },
                        new ReactiveFrameDefinition { picId = 31, durationTicks = 3, lockInput = true, invulnerable = false, impulse = new Vector2(-2f, 0f) },
                        new ReactiveFrameDefinition { picId = 32, durationTicks = 4, lockInput = false, invulnerable = false, impulse = Vector2.zero }
                    }
                };

                // HurtAir (launched/falling)
                so.hurtAir = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.HurtAir,
                    loop = true,
                    nextStateOnFinish = ReactiveStateId.HurtAir,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 30, durationTicks = 3, lockInput = true, invulnerable = false, impulse = Vector2.zero },
                        new ReactiveFrameDefinition { picId = 31, durationTicks = 3, lockInput = true, invulnerable = false, impulse = Vector2.zero },
                        new ReactiveFrameDefinition { picId = 32, durationTicks = 3, lockInput = true, invulnerable = false, impulse = Vector2.zero },
                        new ReactiveFrameDefinition { picId = 33, durationTicks = 3, lockInput = true, invulnerable = false, impulse = Vector2.zero }
                    }
                };

                // Lying (on ground)
                so.lying = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.Lying,
                    loop = false,
                    nextStateOnFinish = ReactiveStateId.GetUp,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 33, durationTicks = 60, lockInput = true, invulnerable = true, impulse = Vector2.zero }
                    }
                };

                // GetUp
                so.getUp = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.GetUp,
                    loop = false,
                    nextStateOnFinish = ReactiveStateId.HurtGrounded,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 34, durationTicks = 4, lockInput = true, invulnerable = true, impulse = Vector2.zero },
                        new ReactiveFrameDefinition { picId = 35, durationTicks = 4, lockInput = true, invulnerable = true, impulse = Vector2.zero },
                        new ReactiveFrameDefinition { picId = 0, durationTicks = 2, lockInput = false, invulnerable = false, impulse = Vector2.zero }
                    }
                };

                // Dead
                so.dead = new ReactiveMoveDefinition
                {
                    stateId = ReactiveStateId.Dead,
                    loop = true,
                    nextStateOnFinish = ReactiveStateId.Dead,
                    frames = new[]
                    {
                        new ReactiveFrameDefinition { picId = 33, durationTicks = 9999, lockInput = true, invulnerable = true, impulse = Vector2.zero }
                    }
                };

                return so;
            }

            // ── Registry ────────────────────────────────────────────────
            public static DefinitionRegistry CreateRegistry(
                CharacterDefinition character,
                EnemyDefinition enemy)
            {
                var so = ScriptableObject.CreateInstance<DefinitionRegistry>();
                so.characters = new[] { character };
                so.enemies = new[] { enemy };
                so.upgrades = System.Array.Empty<UpgradeDefinition>();
                so.Rebuild();
                return so;
            }
        }
    }
}
