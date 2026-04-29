using Project.Core.Tick;
using Project.Gameplay.Combat;
using Project.Gameplay.Pooling;
using Project.Gameplay.Run;
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
        [SerializeField] private float spawnCooldown = 0.08f;

        private float _runTimer;
        private float _spawnCooldownLeft;
        private int _alive;
        private int _nextEnemyId = 1;
        private readonly Dictionary<int, EnemyAgent> _aliveById = new Dictionary<int, EnemyAgent>(512);
        private readonly List<EnemySpawnEntry> _spawnScratch = new List<EnemySpawnEntry>(16);

        public int AliveCount => _alive;
        public IReadOnlyDictionary<int, EnemyAgent> AliveById => _aliveById;

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

            enemyPool.Return(enemy.gameObject);
        }

        private void SpawnEnemy(float progress)
        {
            var angle = Random.value * Mathf.PI * 2f;
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

            var roll = Random.value * sum;
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

            spawnEntries = new[]
            {
                new EnemySpawnEntry { definition = enemyDefinition, weight = 0.65f, unlockAtProgress = 0f },
                new EnemySpawnEntry { definition = scout, weight = 0.22f, unlockAtProgress = 0.2f },
                new EnemySpawnEntry { definition = bruiser, weight = 0.13f, unlockAtProgress = 0.45f }
            };
        }
    }
}
