using Project.Core.Tick;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public enum WaveState
    {
        Idle,
        WaitingToStart,
        Spawning,
        WaveActive,
        Complete,
        Victory,
    }

    [DisallowMultipleComponent]
    public sealed class Lf2StageWaveManager : MonoBehaviour, ITickable
    {
        [SerializeField] private Lf2StageManager stageManager;
        [SerializeField] private Transform spawnCenter;
        [SerializeField] private float spawnRadius = 8f;

        private Lf2StageData _stageData;
        private int _currentWaveIndex;
        private int _spawnIndex;
        private float _timer;
        private int _aliveInWave;
        private WaveState _state = WaveState.Idle;
        private readonly List<GameObject> _trackedEnemies = new List<GameObject>(32);

        public WaveState State => _state;
        public int CurrentWaveIndex => _currentWaveIndex;
        public int TotalWaves => _stageData != null && _stageData.waves != null ? _stageData.waves.Length : 0;
        public int AliveInWave => _aliveInWave;

        public event Action<int> OnWaveStarted;
        public event Action<int> OnWaveCleared;
        public event Action OnVictory;

        private void OnEnable()
        {
            FixedTickSystem.Register(this);

            if (spawnCenter == null)
            {
                var player = GameObject.Find("Player");
                if (player != null)
                    spawnCenter = player.transform;
            }
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
        }

        public void SetSpawnCenter(Transform center)
        {
            spawnCenter = center;
        }

        public void BeginStage(Lf2StageData data)
        {
            if (data == null || data.waves == null || data.waves.Length == 0)
            {
                Debug.LogWarning("[Lf2StageWaveManager] No wave data to begin.");
                return;
            }

            _stageData = data;
            _currentWaveIndex = 0;
            _state = WaveState.WaitingToStart;
            _timer = 0f;
            _trackedEnemies.Clear();
            _aliveInWave = 0;
        }

        public void BeginCurrentStage()
        {
            if (stageManager == null)
            {
                Debug.LogWarning("[Lf2StageWaveManager] No Lf2StageManager assigned.");
                return;
            }

            var data = GetCurrentStageData();
            if (data != null)
                BeginStage(data);
        }

        public void Tick(in TickContext context)
        {
            if (_stageData == null || _stageData.waves == null)
                return;

            switch (_state)
            {
                case WaveState.WaitingToStart:
                    TickWaiting(context);
                    break;
                case WaveState.Spawning:
                    TickSpawning(context);
                    break;
                case WaveState.WaveActive:
                    TickWaveActive(context);
                    break;
            }
        }

        public void NotifyEnemyDied(GameObject enemy)
        {
            if (enemy == null)
                return;

            if (_trackedEnemies.Remove(enemy))
            {
                _aliveInWave = Mathf.Max(0, _aliveInWave - 1);

                if (_aliveInWave <= 0 && _state == WaveState.WaveActive)
                    OnWaveFinished();
            }
        }

        private void TickWaiting(in TickContext context)
        {
            var wave = _stageData.waves[_currentWaveIndex];
            _timer += context.FixedDelta;

            if (_timer >= wave.waveDelay)
                StartWave();
        }

        private void TickSpawning(in TickContext context)
        {
            var wave = _stageData.waves[_currentWaveIndex];
            if (_spawnIndex >= wave.enemies.Length)
            {
                _state = WaveState.WaveActive;
                return;
            }

            _timer += context.FixedDelta;

            var delay = wave.spawnDelays != null && _spawnIndex < wave.spawnDelays.Length
                ? wave.spawnDelays[_spawnIndex]
                : 0f;

            if (_timer >= delay)
            {
                SpawnEnemy(wave.enemies[_spawnIndex]);
                _spawnIndex++;
                _timer = 0f;
            }
        }

        private void TickWaveActive(in TickContext context)
        {
            for (int i = _trackedEnemies.Count - 1; i >= 0; i--)
            {
                if (_trackedEnemies[i] == null || !_trackedEnemies[i].activeInHierarchy)
                {
                    _trackedEnemies.RemoveAt(i);
                    _aliveInWave = Mathf.Max(0, _aliveInWave - 1);
                }
            }

            if (_aliveInWave <= 0)
                OnWaveFinished();
        }

        private void StartWave()
        {
            _state = WaveState.Spawning;
            _spawnIndex = 0;
            _timer = 0f;
            _aliveInWave = 0;
            _trackedEnemies.Clear();

            OnWaveStarted?.Invoke(_currentWaveIndex);
        }

        private void OnWaveFinished()
        {
            _state = WaveState.WaitingToStart;
            _timer = 0f;

            OnWaveCleared?.Invoke(_currentWaveIndex);

            _currentWaveIndex++;
            if (_currentWaveIndex >= _stageData.waves.Length)
            {
                _state = WaveState.Victory;
                OnVictory?.Invoke();
                return;
            }

            _state = WaveState.WaitingToStart;
        }

        private void SpawnEnemy(string enemyId)
        {
            if (string.IsNullOrEmpty(enemyId))
                return;

            var center = spawnCenter != null ? spawnCenter.position : Vector3.zero;
            var angle = UnityEngine.Random.value * Mathf.PI * 2f;
            var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var pos = center + (Vector3)(dir * spawnRadius);

            var go = new GameObject($"Enemy_{enemyId}");
            go.transform.position = pos;
            _trackedEnemies.Add(go);
            _aliveInWave++;
        }

        private Lf2StageData GetCurrentStageData()
        {
            if (stageManager == null) return null;

            var stageId = stageManager.CurrentStage;
            var allData = stageManager.GetStageDataArray();
            if (allData == null) return null;

            for (int i = 0; i < allData.Length; i++)
            {
                if (allData[i] != null && allData[i].stageId == stageId)
                    return allData[i];
            }

            return null;
        }
    }
}
