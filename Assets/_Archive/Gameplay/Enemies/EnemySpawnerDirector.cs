#if false
using Project.Core.Tick;
using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
using Project.Gameplay.Pooling;
using Project.Gameplay.Run;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Enemies
{
    [System.Serializable]
    public struct EnemySpawnEntry
    {
        public EnemyDefinition definition;
        [Min(0.01f)] public float weight;
        [Range(0f, 1f)] public float unlockAtProgress;
    }

    [DisallowMultipleComponent]
    public sealed class EnemySpawnerDirector : MonoBehaviour, ITickable
    {
        [SerializeField] private GameObjectPool enemyPool;
        [SerializeField] private EnemyDefinition enemyDefinition;
        [SerializeField] private EnemySpawnEntry[] spawnEntries;
        [SerializeField] private Transform targetPlayer;
        [SerializeField] private float spawnRadius = 10f;
        [SerializeField] private float runDurationSeconds = 120f;
        [SerializeField] private int targetEnemyCountAtEnd = 200;
        [SerializeField] private float spawnCooldown = 0.7f;

        private float _runTimer;
        private float _spawnCooldownLeft;
        private int _alive;
        private int _nextEnemyId = 1;
        private readonly Dictionary<int, EnemyAgent> _aliveById = new Dictionary<int, EnemyAgent>(512);
        private readonly List<EnemySpawnEntry> _spawnScratch = new List<EnemySpawnEntry>(16);

        public int AliveCount => _alive;
        public IReadOnlyDictionary<int, EnemyAgent> AliveById => _aliveById;

        public event Action<EnemyAgent> OnEnemyDiedEvent;

        public void SetEnemyDefinition(EnemyDefinition def)
        {
            if (def == null) return;
            enemyDefinition = def;
            spawnEntries = null;
            EnsureSpawnEntries();
        }

        public void ApplyBalance(float rampDurationSeconds, int targetAtEnd, float cooldownSeconds)
        {
            runDurationSeconds = Mathf.Max(10f, rampDurationSeconds);
            targetEnemyCountAtEnd = Mathf.Max(10, targetAtEnd);
            spawnCooldown = Mathf.Max(0.02f, cooldownSeconds);
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
            EnsureDefinition();
            EnsurePool();
            if (targetPlayer == null)
            {
                var player = GameObject.Find("Player");
                if (player != null)
                    targetPlayer = player.transform;
            }
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
        }

        public void Tick(in TickContext context)
        {
            if (enemyPool == null || enemyDefinition == null || targetPlayer == null)
                return;

            _runTimer += context.FixedDelta;
            _spawnCooldownLeft -= context.FixedDelta;

            var t = Mathf.Clamp01(_runTimer / Mathf.Max(1f, runDurationSeconds));
            var desiredAlive = Mathf.RoundToInt(Mathf.Lerp(10f, targetEnemyCountAtEnd, t));
            var missing = Mathf.Max(0, desiredAlive - _alive);

            while (missing > 0 && _spawnCooldownLeft <= 0f)
            {
                SpawnEnemy(t);
                missing--;
                _spawnCooldownLeft += spawnCooldown;
            }
        }

        public void NotifyEnemyDied(EnemyAgent enemy)
        {
            if (enemy == null)
                return;

            _alive = Mathf.Max(0, _alive - 1);
            _aliveById.Remove(enemy.NetId);
            var xp = FindAnyObjectByType<RunManager>();
            if (xp != null)
                xp.RegisterEnemyKill(enemy.DefinitionId, enemy.XpDrop);

            OnEnemyDiedEvent?.Invoke(enemy);

            enemyPool.Return(enemy.gameObject);
        }

        public EnemyAgent SpawnAtPosition(EnemyDefinition def, Vector3 position)
        {
            if (enemyPool == null || def == null)
                return null;

            EnsurePool();

            var go = enemyPool.Rent(position);
            var enemy = go.GetComponent<EnemyAgent>();
            if (enemy == null)
                enemy = go.AddComponent<EnemyAgent>();

            if (targetPlayer == null)
            {
                var player = GameObject.Find("Player");
                if (player != null)
                    targetPlayer = player.transform;
            }

            var id = _nextEnemyId++;
            enemy.Initialize(id, def, targetPlayer, this);
            _aliveById[id] = enemy;
            _alive++;
            return enemy;
        }

        private void SpawnEnemy(float progress)
        {
            var angle = UnityEngine.Random.value * Mathf.PI * 2f;
            var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var pos = targetPlayer.position + (Vector3)(dir * spawnRadius);

            var go = enemyPool.Rent(pos);
            var enemy = go.GetComponent<EnemyAgent>();
            if (enemy == null)
                enemy = go.AddComponent<EnemyAgent>();

            var def = PickDefinition(progress);
            var id = _nextEnemyId++;
            enemy.Initialize(id, def, targetPlayer, this);
            _aliveById[id] = enemy;
            _alive++;
        }

        private void EnsurePool()
        {
            if (enemyPool != null)
                return;

            var poolRoot = new GameObject("EnemyPool");
            enemyPool = poolRoot.AddComponent<GameObjectPool>();

            var prefab = new GameObject("Enemy_Grunt");
            prefab.SetActive(false);
            var sr = prefab.AddComponent<SpriteRenderer>();
            sr.color = new Color(1f, 0.75f, 0.75f, 1f);
            var col = prefab.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            var rb = prefab.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;
            prefab.AddComponent<Health>();
            prefab.AddComponent<Damageable>();
            prefab.AddComponent<EnemyAgent>();
            prefab.AddComponent<Project.Gameplay.Rendering.DepthSortByY>();
            var hurtboxLayer = LayerMask.NameToLayer("Hurtbox");
            if (hurtboxLayer >= 0 && hurtboxLayer <= 31)
                prefab.layer = hurtboxLayer;

            enemyPool.Configure(prefab, 24);
        }

        private void EnsureDefinition()
        {
            if (enemyDefinition != null)
            {
                EnsureSpawnEntries();
                return;
            }

            enemyDefinition = ScriptableObject.CreateInstance<EnemyDefinition>();
            enemyDefinition.id = "enemy.grunt.01";
            enemyDefinition.displayName = "Grunt";
            enemyDefinition.maxHealth = 20;
            enemyDefinition.moveSpeed = 2.8f;
            enemyDefinition.touchDamage = 5f;
            enemyDefinition.thinkInterval = 0.08f;
            enemyDefinition.xpDrop = 1f;
            enemyDefinition.tint = new Color(1f, 0.75f, 0.75f, 1f);
            enemyDefinition.scale = 1f;
            enemyDefinition.hitFlashSeconds = 0.06f;

            EnsureSpawnEntries();
        }

        private EnemyDefinition PickDefinition(float progress)
        {
            _spawnScratch.Clear();
            if (spawnEntries != null)
            {
                for (var i = 0; i < spawnEntries.Length; i++)
                {
                    var e = spawnEntries[i];
                    if (e.definition == null || e.weight <= 0f)
                        continue;
                    if (progress < e.unlockAtProgress)
                        continue;
                    _spawnScratch.Add(e);
                }
            }

            if (_spawnScratch.Count == 0)
                return enemyDefinition;

            var sum = 0f;
            for (var i = 0; i < _spawnScratch.Count; i++)
                sum += Mathf.Max(0.01f, _spawnScratch[i].weight);

            var roll = UnityEngine.Random.value * sum;
            for (var i = 0; i < _spawnScratch.Count; i++)
            {
                var w = Mathf.Max(0.01f, _spawnScratch[i].weight);
                if (roll <= w)
                    return _spawnScratch[i].definition;
                roll -= w;
            }

            return _spawnScratch[_spawnScratch.Count - 1].definition;
        }

        private void EnsureSpawnEntries()
        {
            if (spawnEntries != null && spawnEntries.Length > 0)
                return;

            var bruiser = ScriptableObject.CreateInstance<EnemyDefinition>();
            bruiser.id = "enemy.bruiser.01";
            bruiser.displayName = "Bruiser";
            bruiser.maxHealth = 45;
            bruiser.moveSpeed = 1.8f;
            bruiser.touchDamage = 9f;
            bruiser.thinkInterval = 0.12f;
            bruiser.xpDrop = 2.2f;
            bruiser.tint = new Color(1f, 0.55f, 0.55f, 1f);
            bruiser.scale = 1.25f;
            bruiser.hitFlashSeconds = 0.08f;
            bruiser.personality = Lf2EnemyPersonality.Aggressive;

            var scout = ScriptableObject.CreateInstance<EnemyDefinition>();
            scout.id = "enemy.scout.01";
            scout.displayName = "Scout";
            scout.maxHealth = 12;
            scout.moveSpeed = 4.2f;
            scout.touchDamage = 3f;
            scout.thinkInterval = 0.06f;
            scout.xpDrop = 0.8f;
            scout.tint = new Color(0.65f, 1f, 0.75f, 1f);
            scout.scale = 0.85f;
            scout.hitFlashSeconds = 0.05f;
            scout.personality = Lf2EnemyPersonality.Fast;

            var hunter = ScriptableObject.CreateInstance<EnemyDefinition>();
            hunter.id = "enemy.hunter.01";
            hunter.displayName = "Hunter";
            hunter.maxHealth = 16;
            hunter.moveSpeed = 2.2f;
            hunter.touchDamage = 0f;
            hunter.thinkInterval = 0.1f;
            hunter.xpDrop = 1.5f;
            hunter.tint = new Color(0.7f, 0.85f, 1f, 1f);
            hunter.scale = 0.95f;
            hunter.hitFlashSeconds = 0.06f;
            hunter.isRanged = true;
            hunter.rangedMinDistance = 5f;
            hunter.rangedMaxDistance = 8f;
            hunter.fleeDistance = 3f;
            hunter.shotCooldown = 1.2f;
            hunter.defendChance = 0.2f;
            hunter.attackFrameId = 60;
            hunter.projectileOid = 1;
            hunter.personality = Lf2EnemyPersonality.Ranged;
            hunter.characterDatName = "hunter";

            var mark = ScriptableObject.CreateInstance<EnemyDefinition>();
            mark.id = "enemy.mark.32";
            mark.displayName = "Mark";
            mark.maxHealth = 80;
            mark.moveSpeed = 1.5f;
            mark.touchDamage = 14f;
            mark.thinkInterval = 0.1f;
            mark.xpDrop = 4.0f;
            mark.tint = new Color(0.9f, 0.6f, 0.4f, 1f);
            mark.scale = 1.4f;
            mark.hitFlashSeconds = 0.1f;
            mark.personality = Lf2EnemyPersonality.Brute;
            mark.characterDatName = "mark";

            var jack = ScriptableObject.CreateInstance<EnemyDefinition>();
            jack.id = "enemy.jack.33";
            jack.displayName = "Jack";
            jack.maxHealth = 28;
            jack.moveSpeed = 4.5f;
            jack.touchDamage = 6f;
            jack.thinkInterval = 0.05f;
            jack.xpDrop = 2.5f;
            jack.tint = new Color(0.5f, 0.8f, 1f, 1f);
            jack.scale = 1.0f;
            jack.hitFlashSeconds = 0.05f;
            jack.isRanged = true;
            jack.rangedMinDistance = 4f;
            jack.rangedMaxDistance = 7f;
            jack.fleeDistance = 2.5f;
            jack.shotCooldown = 1.5f;
            jack.defendChance = 0.25f;
            jack.attackFrameId = 60;
            jack.projectileOid = 1;
            jack.personality = Lf2EnemyPersonality.Fast;
            jack.characterDatName = "jack";

            var sorcerer = ScriptableObject.CreateInstance<EnemyDefinition>();
            sorcerer.id = "enemy.sorcerer.34";
            sorcerer.displayName = "Sorcerer";
            sorcerer.maxHealth = 22;
            sorcerer.moveSpeed = 2.0f;
            sorcerer.touchDamage = 3f;
            sorcerer.thinkInterval = 0.08f;
            sorcerer.xpDrop = 3.0f;
            sorcerer.tint = new Color(0.6f, 0.4f, 0.9f, 1f);
            sorcerer.scale = 1.0f;
            sorcerer.hitFlashSeconds = 0.06f;
            sorcerer.isRanged = true;
            sorcerer.rangedMinDistance = 5f;
            sorcerer.rangedMaxDistance = 9f;
            sorcerer.fleeDistance = 3.5f;
            sorcerer.shotCooldown = 1.0f;
            sorcerer.defendChance = 0.2f;
            sorcerer.attackFrameId = 60;
            sorcerer.projectileOid = 1;
            sorcerer.personality = Lf2EnemyPersonality.MagicSupport;
            sorcerer.characterDatName = "sorcerer";

            var monk = ScriptableObject.CreateInstance<EnemyDefinition>();
            monk.id = "enemy.monk.35";
            monk.displayName = "Monk";
            monk.maxHealth = 35;
            monk.moveSpeed = 3.0f;
            monk.touchDamage = 8f;
            monk.thinkInterval = 0.07f;
            monk.xpDrop = 2.8f;
            monk.tint = new Color(0.9f, 0.85f, 0.6f, 1f);
            monk.scale = 1.05f;
            monk.hitFlashSeconds = 0.06f;
            monk.personality = Lf2EnemyPersonality.Aggressive;
            monk.characterDatName = "monk";

            var jan = ScriptableObject.CreateInstance<EnemyDefinition>();
            jan.id = "enemy.jan.36";
            jan.displayName = "Jan";
            jan.maxHealth = 25;
            jan.moveSpeed = 2.8f;
            jan.touchDamage = 4f;
            jan.thinkInterval = 0.08f;
            jan.xpDrop = 3.2f;
            jan.tint = new Color(0.9f, 0.6f, 0.8f, 1f);
            jan.scale = 0.95f;
            jan.hitFlashSeconds = 0.06f;
            jan.isRanged = true;
            jan.rangedMinDistance = 5f;
            jan.rangedMaxDistance = 8f;
            jan.fleeDistance = 3f;
            jan.shotCooldown = 1.3f;
            jan.defendChance = 0.25f;
            jan.attackFrameId = 60;
            jan.projectileOid = 1;
            jan.personality = Lf2EnemyPersonality.MagicSupport;
            jan.characterDatName = "jan";

            var knight = ScriptableObject.CreateInstance<EnemyDefinition>();
            knight.id = "enemy.knight.37";
            knight.displayName = "Knight";
            knight.maxHealth = 90;
            knight.moveSpeed = 1.6f;
            knight.touchDamage = 12f;
            knight.thinkInterval = 0.1f;
            knight.xpDrop = 4.5f;
            knight.tint = new Color(0.7f, 0.75f, 0.85f, 1f);
            knight.scale = 1.45f;
            knight.hitFlashSeconds = 0.1f;
            knight.defendChance = 0.35f;
            knight.personality = Lf2EnemyPersonality.Brute;
            knight.characterDatName = "knight";

            var bat = ScriptableObject.CreateInstance<EnemyDefinition>();
            bat.id = "enemy.bat.38";
            bat.displayName = "Bat";
            bat.maxHealth = 150;
            bat.moveSpeed = 3.2f;
            bat.touchDamage = 10f;
            bat.thinkInterval = 0.07f;
            bat.xpDrop = 8.0f;
            bat.tint = new Color(0.4f, 0.3f, 0.5f, 1f);
            bat.scale = 1.6f;
            bat.hitFlashSeconds = 0.08f;
            bat.isRanged = true;
            bat.rangedMinDistance = 4f;
            bat.rangedMaxDistance = 10f;
            bat.fleeDistance = 2f;
            bat.shotCooldown = 0.8f;
            bat.defendChance = 0.25f;
            bat.attackFrameId = 60;
            bat.projectileOid = 1;
            bat.personality = Lf2EnemyPersonality.Boss;
            bat.characterDatName = "bat";

            var justin = ScriptableObject.CreateInstance<EnemyDefinition>();
            justin.id = "enemy.justin.39";
            justin.displayName = "Justin";
            justin.maxHealth = 32;
            justin.moveSpeed = 4.0f;
            justin.touchDamage = 7f;
            justin.thinkInterval = 0.05f;
            justin.xpDrop = 2.8f;
            justin.tint = new Color(0.6f, 0.6f, 0.6f, 1f);
            justin.scale = 1.05f;
            justin.hitFlashSeconds = 0.05f;
            justin.isRanged = true;
            justin.rangedMinDistance = 3f;
            justin.rangedMaxDistance = 7f;
            justin.fleeDistance = 2f;
            justin.shotCooldown = 1.4f;
            justin.defendChance = 0.2f;
            justin.attackFrameId = 60;
            justin.projectileOid = 1;
            justin.personality = Lf2EnemyPersonality.Fast;
            justin.characterDatName = "justin";

            var louisEX = ScriptableObject.CreateInstance<EnemyDefinition>();
            louisEX.id = "enemy.louisEX.50";
            louisEX.displayName = "LouisEX";
            louisEX.maxHealth = 200;
            louisEX.moveSpeed = 3.0f;
            louisEX.touchDamage = 15f;
            louisEX.thinkInterval = 0.07f;
            louisEX.xpDrop = 12.0f;
            louisEX.tint = new Color(1f, 0.85f, 0.3f, 1f);
            louisEX.scale = 1.7f;
            louisEX.hitFlashSeconds = 0.08f;
            louisEX.isRanged = true;
            louisEX.rangedMinDistance = 3f;
            louisEX.rangedMaxDistance = 10f;
            louisEX.fleeDistance = 2f;
            louisEX.shotCooldown = 0.7f;
            louisEX.defendChance = 0.3f;
            louisEX.attackFrameId = 60;
            louisEX.projectileOid = 1;
            louisEX.personality = Lf2EnemyPersonality.Boss;
            louisEX.characterDatName = "louisEX";

            var firzen = ScriptableObject.CreateInstance<EnemyDefinition>();
            firzen.id = "enemy.firzen.51";
            firzen.displayName = "Firzen";
            firzen.maxHealth = 220;
            firzen.moveSpeed = 2.8f;
            firzen.touchDamage = 18f;
            firzen.thinkInterval = 0.07f;
            firzen.xpDrop = 14.0f;
            firzen.tint = new Color(1f, 0.4f, 0.2f, 1f);
            firzen.scale = 1.6f;
            firzen.hitFlashSeconds = 0.08f;
            firzen.isRanged = true;
            firzen.rangedMinDistance = 4f;
            firzen.rangedMaxDistance = 11f;
            firzen.fleeDistance = 2f;
            firzen.shotCooldown = 0.6f;
            firzen.defendChance = 0.25f;
            firzen.attackFrameId = 60;
            firzen.projectileOid = 1;
            firzen.personality = Lf2EnemyPersonality.Boss;
            firzen.characterDatName = "firzen";

            var julian = ScriptableObject.CreateInstance<EnemyDefinition>();
            julian.id = "enemy.julian.52";
            julian.displayName = "Julian";
            julian.maxHealth = 300;
            julian.moveSpeed = 3.0f;
            julian.touchDamage = 20f;
            julian.thinkInterval = 0.06f;
            julian.xpDrop = 20.0f;
            julian.tint = new Color(0.3f, 0.15f, 0.4f, 1f);
            julian.scale = 1.8f;
            julian.hitFlashSeconds = 0.1f;
            julian.isRanged = true;
            julian.rangedMinDistance = 3f;
            julian.rangedMaxDistance = 12f;
            julian.fleeDistance = 1.5f;
            julian.shotCooldown = 0.5f;
            julian.defendChance = 0.35f;
            julian.attackFrameId = 60;
            julian.projectileOid = 1;
            julian.personality = Lf2EnemyPersonality.Boss;
            julian.characterDatName = "julian";

            spawnEntries = new[]
            {
                new EnemySpawnEntry { definition = enemyDefinition, weight = 0.35f, unlockAtProgress = 0f },
                new EnemySpawnEntry { definition = scout, weight = 0.15f, unlockAtProgress = 0.15f },
                new EnemySpawnEntry { definition = hunter, weight = 0.1f, unlockAtProgress = 0.25f },
                new EnemySpawnEntry { definition = monk, weight = 0.08f, unlockAtProgress = 0.2f },
                new EnemySpawnEntry { definition = jack, weight = 0.06f, unlockAtProgress = 0.3f },
                new EnemySpawnEntry { definition = sorcerer, weight = 0.05f, unlockAtProgress = 0.35f },
                new EnemySpawnEntry { definition = jan, weight = 0.04f, unlockAtProgress = 0.4f },
                new EnemySpawnEntry { definition = bruiser, weight = 0.05f, unlockAtProgress = 0.4f },
                new EnemySpawnEntry { definition = mark, weight = 0.03f, unlockAtProgress = 0.5f },
                new EnemySpawnEntry { definition = knight, weight = 0.025f, unlockAtProgress = 0.55f },
                new EnemySpawnEntry { definition = justin, weight = 0.025f, unlockAtProgress = 0.45f },
                new EnemySpawnEntry { definition = bat, weight = 0.015f, unlockAtProgress = 0.6f },
                new EnemySpawnEntry { definition = louisEX, weight = 0.01f, unlockAtProgress = 0.75f },
                new EnemySpawnEntry { definition = firzen, weight = 0.01f, unlockAtProgress = 0.85f },
                new EnemySpawnEntry { definition = julian, weight = 0.005f, unlockAtProgress = 0.95f },
            };
        }
    }
}
#endif
