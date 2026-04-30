#if false
using System.Collections.Generic;
using Project.Core.Tick;
using Project.Core.Telemetry;
using Project.Data;
using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Run
{
    [DisallowMultipleComponent]
    public sealed class RunManager : MonoBehaviour, ITickable
    {
        [SerializeField] private float runDurationSeconds = 120f;
        [SerializeField] private float xpBasePerLevel = 5f;
        [SerializeField] private float xpGrowth = 1.2f;
        [SerializeField] private RunBalanceDefinition balanceDefinition;
        [SerializeField] private UpgradeDefinition[] upgradesPool;
        [SerializeField] private bool showOverlay = true;

        private float _runTime;
        private float _xp;
        private int _level = 1;
        private bool _isChoosing;
        private readonly List<UpgradeDefinition> _offer = new List<UpgradeDefinition>(3);
        private PlayerRuntimeStats _playerStats;
        private Health _playerHealth;
        private TelemetryService _telemetry;
        private bool _runEndSent;
        private bool _won;
        private bool _runFailed;
        private bool _spawnerBalancePending;
        private Enemies.EnemySpawnerDirector _spawner;
        private int _kills;

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
            ResolvePlayerRefs();
            DisableLegacyAutoPulse();
            ApplyBalanceDefinition();
            EnsureDefaultUpgradesIfNeeded();
            _telemetry = FindAnyObjectByType<TelemetryService>();
            _telemetry?.TrackRunStart("offline_or_local");
            HookPlayerHealthEvents();
            _spawner = FindAnyObjectByType<Enemies.EnemySpawnerDirector>();
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
            UnhookPlayerHealthEvents();
        }

        public void Tick(in TickContext context)
        {
            if (_isChoosing)
                return;

            if (_runFailed)
                return;

            if (_won)
                return;

            if (_spawnerBalancePending)
                TryApplyPendingSpawnerBalance();

            _runTime += context.FixedDelta;
            if (_runTime >= runDurationSeconds)
            {
                _runTime = runDurationSeconds;
                if (!_won)
                {
                    _won = true;
                    _isChoosing = false;
                }
                if (!_runEndSent)
                {
                    _runEndSent = true;
                    _telemetry?.TrackRunEnd("time_limit", _runTime);
                }
            }
        }

        public void AddXp(float amount)
        {
            if (amount <= 0f)
                return;

            var xpMult = _playerStats != null ? _playerStats.XpGainMultiplier : 1f;
            _xp += amount * xpMult;
            TryLevelUp();
        }

        public void RegisterEnemyKill(string enemyLabel, float xpAmount)
        {
            _kills++;
            AddXp(xpAmount);
            _telemetry?.TrackEnemyKilled(string.IsNullOrWhiteSpace(enemyLabel) ? "enemy" : enemyLabel, _kills);
        }

        public void SetUpgradesPool(UpgradeDefinition[] upgrades)
        {
            upgradesPool = upgrades;
        }

        private void TryLevelUp()
        {
            while (_xp >= RequiredXpForNextLevel())
            {
                _xp -= RequiredXpForNextLevel();
                _level++;
                StartUpgradeChoice();
            }
        }

        private float RequiredXpForNextLevel()
        {
            return xpBasePerLevel * Mathf.Pow(xpGrowth, Mathf.Max(0, _level - 1));
        }

        private void StartUpgradeChoice()
        {
            _offer.Clear();
            _isChoosing = true;

            if (upgradesPool == null || upgradesPool.Length == 0)
                return;

            var max = Mathf.Min(3, upgradesPool.Length);
            var used = new HashSet<int>();
            var guard = 0;
            while (_offer.Count < max)
            {
                guard++;
                if (guard > upgradesPool.Length * 4)
                    break;

                var idx = Random.Range(0, upgradesPool.Length);
                if (!used.Add(idx))
                    continue;

                var candidate = upgradesPool[idx];
                if (candidate == null || IsAutoUpgrade(candidate.kind))
                    continue;

                _offer.Add(candidate);
            }
        }

        private void ApplyUpgrade(UpgradeDefinition upgrade)
        {
            if (upgrade == null)
                return;

            ResolvePlayerRefs();
            if (_playerStats == null)
                return;

            switch (upgrade.kind)
            {
                case UpgradeKind.MoveSpeed:
                    _playerStats.AddMoveSpeedPercent(upgrade.magnitude);
                    break;
                case UpgradeKind.Damage:
                    _playerStats.AddDamagePercent(upgrade.magnitude);
                    break;
                case UpgradeKind.MaxHealth:
                    _playerStats.AddMaxHealthPercent(upgrade.magnitude);
                    if (_playerHealth != null)
                    {
                        var newMax = Mathf.RoundToInt(_playerHealth.MaxHealth * (1f + upgrade.magnitude));
                        _playerHealth.ConfigureMaxHealth(newMax, true);
                    }
                    break;
                case UpgradeKind.AutoPulse:
                    var pulse = EnsurePlayerComponent<AutoPulseAura>();
                    if (pulse != null)
                    {
                        var pDamage = Mathf.RoundToInt(2 + (6 * upgrade.magnitude));
                        var pInterval = Mathf.Lerp(0.6f, 0.25f, upgrade.magnitude);
                        var pRadius = Mathf.Lerp(1.8f, 3f, upgrade.magnitude);
                        pulse.Configure(pDamage, pInterval, pRadius);
                    }
                    break;
                case UpgradeKind.AutoPulseRate:
                    _playerStats.AddPulseRatePercent(upgrade.magnitude);
                    break;
                case UpgradeKind.AutoPulseRadius:
                    _playerStats.AddPulseRadiusPercent(upgrade.magnitude);
                    break;
                case UpgradeKind.XpGain:
                    _playerStats.AddXpGainPercent(upgrade.magnitude);
                    break;
            }

            _telemetry?.TrackUpgradePick(upgrade.id, _level);
        }

        private void TryApplyPendingSpawnerBalance()
        {
            if (!_spawnerBalancePending)
                return;

            _spawner = FindAnyObjectByType<Enemies.EnemySpawnerDirector>();
            if (_spawner == null || balanceDefinition == null)
                return;

            _spawner.ApplyBalance(
                balanceDefinition.spawnRampDurationSeconds,
                balanceDefinition.targetEnemyCountAtEnd,
                balanceDefinition.spawnCooldown);
            _spawnerBalancePending = false;
        }

        private void ResolvePlayerRefs()
        {
            if (_playerStats != null)
                return;

            var player = GameObject.Find("Player");
            if (player == null)
                return;

            _playerStats = player.GetComponent<PlayerRuntimeStats>();
            if (_playerStats == null)
                _playerStats = player.AddComponent<PlayerRuntimeStats>();

            _playerHealth = player.GetComponent<Health>();
            if (_playerHealth == null)
                _playerHealth = player.AddComponent<Health>();

            if (player.GetComponent<Damageable>() == null)
                player.AddComponent<Damageable>();
        }

        private void EnsureDefaultUpgradesIfNeeded()
        {
            if (upgradesPool != null && upgradesPool.Length > 0)
                return;

            upgradesPool = new[]
            {
                CreateRuntimeUpgrade("upgrade.move.runtime", "Swift Feet", "+12% movement speed", UpgradeKind.MoveSpeed, 0.12f),
                CreateRuntimeUpgrade("upgrade.damage.runtime", "Heavy Hands", "+15% attack damage", UpgradeKind.Damage, 0.15f),
                CreateRuntimeUpgrade("upgrade.hp.runtime", "Iron Body", "+20% max health", UpgradeKind.MaxHealth, 0.20f),
                CreateRuntimeUpgrade("upgrade.xp.runtime", "Scholar's Badge", "+18% XP ganho", UpgradeKind.XpGain, 0.18f),
                CreateRuntimeUpgrade("upgrade.move.2.runtime", "Quick Steps II", "+10% movement speed", UpgradeKind.MoveSpeed, 0.10f),
                CreateRuntimeUpgrade("upgrade.damage.2.runtime", "Brutal Force II", "+12% attack damage", UpgradeKind.Damage, 0.12f),
                CreateRuntimeUpgrade("upgrade.hp.2.runtime", "Iron Body II", "+15% max health", UpgradeKind.MaxHealth, 0.15f),
            };
        }

        private void ApplyBalanceDefinition()
        {
            if (balanceDefinition == null)
                balanceDefinition = CreateRuntimeBalance();

            runDurationSeconds = balanceDefinition.runDurationSeconds;
            xpBasePerLevel = balanceDefinition.xpBasePerLevel;
            xpGrowth = balanceDefinition.xpGrowth;

            if (_spawner == null)
                _spawner = FindAnyObjectByType<Enemies.EnemySpawnerDirector>();
            if (_spawner != null)
            {
                _spawner.ApplyBalance(
                    balanceDefinition.spawnRampDurationSeconds,
                    balanceDefinition.targetEnemyCountAtEnd,
                    balanceDefinition.spawnCooldown);
                _spawnerBalancePending = false;
            }
            else
            {
                _spawnerBalancePending = true;
            }
        }

        private static RunBalanceDefinition CreateRuntimeBalance()
        {
            var b = ScriptableObject.CreateInstance<RunBalanceDefinition>();
            b.runDurationSeconds = 120f;
            b.xpBasePerLevel = 4.5f;
            b.xpGrowth = 1.16f;
            b.targetEnemyCountAtEnd = 320;
            b.spawnRampDurationSeconds = 90f;
            b.spawnCooldown = 0.06f;
            return b;
        }

        private static UpgradeDefinition CreateRuntimeUpgrade(string id, string name, string desc, UpgradeKind kind, float mag)
        {
            var u = ScriptableObject.CreateInstance<UpgradeDefinition>();
            u.id = id;
            u.displayName = name;
            u.description = desc;
            u.kind = kind;
            u.magnitude = mag;
            return u;
        }

        private static bool IsAutoUpgrade(UpgradeKind kind)
        {
            return kind == UpgradeKind.AutoPulse
                || kind == UpgradeKind.AutoPulseRate
                || kind == UpgradeKind.AutoPulseRadius;
        }

        private void Update()
        {
            var kb = Keyboard.current;

            if ((_runFailed || _won) && kb != null && kb.rKey.wasPressedThisFrame)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
                return;
            }

            if (!_isChoosing)
                return;

            if (kb != null && kb.digit1Key.wasPressedThisFrame)
                Choose(0);
            else if (kb != null && kb.digit2Key.wasPressedThisFrame)
                Choose(1);
            else if (kb != null && kb.digit3Key.wasPressedThisFrame)
                Choose(2);
        }

        private void Choose(int idx)
        {
            if (idx < 0 || idx >= _offer.Count)
                return;

            ApplyUpgrade(_offer[idx]);
            _offer.Clear();
            _isChoosing = false;
        }

        private void OnGUI()
        {
            if (!showOverlay)
                return;

            GUILayout.BeginArea(new Rect(12f, 110f, 440f, 300f), GUI.skin.box);
            GUILayout.Label($"Run: {_runTime:0.0}s / {runDurationSeconds:0}s");
            GUILayout.Label($"Level: {_level}");
            GUILayout.Label($"Kills: {_kills}");
            GUILayout.Label($"XP: {_xp:0.0} / {RequiredXpForNextLevel():0.0}");
            if (_playerHealth != null)
                GUILayout.Label($"HP: {_playerHealth.CurrentHealth}/{_playerHealth.MaxHealth}");

            if (_spawner == null)
                _spawner = FindAnyObjectByType<Enemies.EnemySpawnerDirector>();
            if (_spawner != null)
                GUILayout.Label($"Enemies alive: {_spawner.AliveCount}");

            if (_isChoosing)
            {
                GUILayout.Space(6f);
                GUILayout.Label("Escolha upgrade (teclas 1/2/3):");
                for (var i = 0; i < _offer.Count; i++)
                {
                    var u = _offer[i];
                    GUILayout.Label($"{i + 1}) {u.displayName} - {u.description}");
                }
            }
            GUILayout.EndArea();

            if (_runFailed)
            {
                GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 180f, Screen.height * 0.5f - 70f, 360f, 140f), GUI.skin.box);
                GUILayout.Label("Você foi derrotado.");
                GUILayout.Label("Pressione R para reiniciar a run.");
                GUILayout.EndArea();
            }

            if (_won)
            {
                GUILayout.BeginArea(new Rect(Screen.width * 0.5f - 200f, Screen.height * 0.5f - 90f, 400f, 180f), GUI.skin.box);
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("YOU SURVIVED!");
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.Space(6f);
                GUILayout.Label($"Level: {_level}  |  Kills: {_kills}  |  Time: {_runTime:0}s");
                GUILayout.Space(6f);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Press R to play again");
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.EndArea();
            }
        }

        private T EnsurePlayerComponent<T>() where T : Component
        {
            var player = GameObject.Find("Player");
            if (player == null)
                return null;

            var c = player.GetComponent<T>();
            if (c == null)
                c = player.AddComponent<T>();
            return c;
        }

        private void DisableLegacyAutoPulse()
        {
            var player = GameObject.Find("Player");
            if (player == null)
                return;

            var pulse = player.GetComponent<AutoPulseAura>();
            if (pulse != null)
                Destroy(pulse);
        }

        private void HookPlayerHealthEvents()
        {
            if (_playerHealth == null)
                return;

            _playerHealth.OnDied -= OnPlayerDied;
            _playerHealth.OnDied += OnPlayerDied;
        }

        private void UnhookPlayerHealthEvents()
        {
            if (_playerHealth == null)
                return;
            _playerHealth.OnDied -= OnPlayerDied;
        }

        private void OnPlayerDied(Health _)
        {
            _runFailed = true;
            _isChoosing = false;
            _telemetry?.TrackRunEnd("player_dead", _runTime);
        }
    }
}
#endif
