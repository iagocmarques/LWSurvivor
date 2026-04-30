using System;
using System.Collections.Generic;
using Project.Data;
using Project.Gameplay.AI;
using Project.Gameplay.Combat;
using Project.Gameplay.Enemies;
using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Gameplay.Modes
{
    public sealed class StageWaveRunner : MonoBehaviour
    {
        [SerializeField] private StageDefinition stageDefinition;
        [SerializeField] private Lf2CharacterDatabase characterDatabase;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector2 spawnRangeX = new(-5f, 5f);
        [SerializeField] private Vector2 spawnRangeY = new(-2f, 2f);

        private int _currentPhase;
        private readonly List<GameObject> _activeEnemies = new();
        private bool _phaseActive;

        public event Action<int, StagePhaseDefinition> OnPhaseChanged;
        public event Action<string> OnPhaseMusicRequested;
        public event Action OnStageCleared;
        public event Action OnGameOver; // Reserved for stage game-over flow
#pragma warning disable CS0067

        public int CurrentPhase => _currentPhase;
        public bool IsComplete => _currentPhase >= stageDefinition.phases.Count;
        public StageDefinition Definition => stageDefinition;

        public void Init(StageDefinition definition, Lf2CharacterDatabase database, Transform player)
        {
            stageDefinition = definition;
            characterDatabase = database;
            playerTransform = player;
        }

        public void StartStage()
        {
            if (stageDefinition == null || stageDefinition.phases.Count == 0)
            {
                Debug.LogWarning("[StageWaveRunner] No stage definition or no phases.");
                return;
            }

            _currentPhase = 0;
            StartPhase();
        }

        public void StopStage()
        {
            _phaseActive = false;
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                if (_activeEnemies[i] != null)
                    Destroy(_activeEnemies[i]);
            }
            _activeEnemies.Clear();
        }

        private void StartPhase()
        {
            if (_currentPhase >= stageDefinition.phases.Count)
            {
                OnStageCleared?.Invoke();
                return;
            }

            var phase = stageDefinition.phases[_currentPhase];
            OnPhaseChanged?.Invoke(_currentPhase, phase);

            if (!string.IsNullOrEmpty(phase.musicPath))
                OnPhaseMusicRequested?.Invoke(phase.musicPath);

            foreach (var entry in phase.entries)
            {
                if (entry.ratio < 1f && UnityEngine.Random.value > entry.ratio)
                    continue;

                for (int i = 0; i < entry.times; i++)
                    SpawnEntry(entry);
            }

            _phaseActive = true;
        }

        private void SpawnEntry(StageSpawnEntry entry)
        {
            if (characterDatabase == null)
            {
                Debug.LogError("[StageWaveRunner] No character database assigned.");
                return;
            }

            var charData = characterDatabase.GetCharacter(entry.objectId);
            if (charData == null)
            {
                Debug.LogWarning($"[StageWaveRunner] Character id {entry.objectId} not found.");
                return;
            }

            var datBytes = characterDatabase.GetDatBytes(entry.objectId);

            AIArchetype archetype = entry.role == SpawnRole.Boss
                ? AIArchetypePresets.CreateBoss()
                : AIArchetypePresets.CreateBandit();

            Vector3 basePos = playerTransform != null ? playerTransform.position : Vector3.zero;
            var pos = new Vector3(
                basePos.x + UnityEngine.Random.Range(spawnRangeX.x, spawnRangeX.y),
                basePos.y + UnityEngine.Random.Range(spawnRangeY.x, spawnRangeY.y),
                0f);

            int maxHp = entry.hpOverride > 0 ? entry.hpOverride : 100;
            var enemy = EnemyFactory.CreateEnemy(charData, datBytes, archetype, playerTransform, pos, maxHp);

            if (entry.role == SpawnRole.Boss)
                enemy.name = $"Boss_{charData.Name ?? "Unknown"}";

            _activeEnemies.Add(enemy);
        }

        private void Update()
        {
            if (!_phaseActive) return;

            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                var go = _activeEnemies[i];
                if (go == null)
                {
                    _activeEnemies.RemoveAt(i);
                    continue;
                }

                var health = go.GetComponent<Health>();
                if (health != null && health.IsDead)
                {
                    _activeEnemies.RemoveAt(i);
                }
            }

            if (_activeEnemies.Count == 0)
            {
                _phaseActive = false;
                _currentPhase++;
                StartPhase();
            }
        }
    }
}
