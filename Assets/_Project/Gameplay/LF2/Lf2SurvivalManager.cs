using System.Collections.Generic;
using Project.Core.Input;
using Project.Data;
using Project.Gameplay.AI;
using Project.Gameplay.Combat;
using Project.Gameplay.Enemies;
using Project.Gameplay.Player;
using Project.Gameplay.Rendering;
using Project.Gameplay.Visual;
using Project.UI.HUD;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2SurvivalManager : MonoBehaviour
    {
        private enum SurvivalPhase
        {
            CharacterSelect,
            WaveIntro,
            Fighting,
            WaveCleared,
            GameOver
        }

        private const float WaveIntroDuration = 2f;
        private const float WaveClearedDuration = 2f;
        private const int BaseEnemiesPerWave = 3;
        private const int ExtraEnemiesPerWave = 2;
        private const float SpawnRadius = 6f;

        [SerializeField] private Lf2CharacterSelect characterSelect;
        [SerializeField] private Lf2CharacterDatabase _database;
        [SerializeField] private MatchHud _matchHud;

        [Header("Enemy Templates")]
        [SerializeField] private Lf2CharacterDatabase _enemyDatabase;

        private SurvivalPhase _phase;
        private float _phaseTimer;
        private int _currentWave;
        private int _score;
        private int _enemiesSpawnedThisWave;
        private int _enemiesAliveThisWave;
        private int _totalEnemiesThisWave;
        private float _spawnTimer;
        private float _spawnInterval = 0.8f;

        private GameObject _playerObj;
        private Health _playerHealth;
        private PlayerHsmController _playerHsm;
        private Lf2CharacterData _playerLf2Data;
        private Dictionary<int, Lf2CharacterData> _playerProjectileMap;
        private string _playerCharName;

        private readonly List<GameObject> _enemies = new List<GameObject>(64);
        private readonly List<Health> _enemyHealths = new List<Health>(64);

        private Texture2D _whiteTex;
        private GUIStyle _titleStyle;
        private GUIStyle _waveStyle;
        private GUIStyle _scoreStyle;
        private GUIStyle _gameOverStyle;
        private GUIStyle _hudStyle;

        private static readonly string[] EnemyCharacterNames = { "Davis", "Deep", "John", "Henry", "Firen" };
        private static readonly string[] EnemyCharacterPaths = { "LF2/davis", "LF2/deep", "LF2/john", "LF2/henry", "LF2/firen" };

        private void Awake()
        {
            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();

            if (characterSelect == null)
                characterSelect = GetComponentInChildren<Lf2CharacterSelect>();
            if (characterSelect == null)
            {
                var csGo = new GameObject("CharacterSelect");
                csGo.transform.SetParent(transform);
                characterSelect = csGo.AddComponent<Lf2CharacterSelect>();
            }

            _phase = SurvivalPhase.CharacterSelect;
        }

        private void OnDestroy()
        {
            if (_whiteTex != null) Destroy(_whiteTex);
            CleanupAll();
        }

        private void Update()
        {
            CombatReadabilityFx.Tick(Time.deltaTime);

            switch (_phase)
            {
                case SurvivalPhase.CharacterSelect:
                    UpdateCharacterSelect();
                    break;
                case SurvivalPhase.WaveIntro:
                    UpdateWaveIntro();
                    break;
                case SurvivalPhase.Fighting:
                    UpdateFighting();
                    break;
                case SurvivalPhase.WaveCleared:
                    UpdateWaveCleared();
                    break;
                case SurvivalPhase.GameOver:
                    UpdateGameOver();
                    break;
            }
        }

        private void UpdateCharacterSelect()
        {
            if (!characterSelect.P1Confirmed) return;

            var p1Slot = characterSelect.GetSlot(characterSelect.P1Index);
            _playerCharName = p1Slot.name;

            _playerLf2Data = LoadCharacterData(p1Slot.datResourcePath, p1Slot.name);
            _playerProjectileMap = LoadProjectileMap(p1Slot.name);
            if (_playerLf2Data == null) _playerLf2Data = LoadFallbackData();

            SetupPlayer();
            _currentWave = 0;
            _score = 0;
            StartNextWave();
        }

        private void UpdateWaveIntro()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
                _phase = SurvivalPhase.Fighting;
        }

        private void UpdateFighting()
        {
            if (_playerHealth != null && _playerHealth.IsDead)
            {
                _phase = SurvivalPhase.GameOver;
                _phaseTimer = 0f;
                return;
            }

            CleanDeadEnemies();

            if (_enemiesSpawnedThisWave < _totalEnemiesThisWave)
            {
                _spawnTimer -= Time.deltaTime;
                if (_spawnTimer <= 0f)
                {
                    SpawnEnemy();
                    _spawnTimer = _spawnInterval;
                }
            }

            if (_enemiesSpawnedThisWave >= _totalEnemiesThisWave && _enemiesAliveThisWave <= 0)
            {
                _score += _currentWave * 100;
                _phase = SurvivalPhase.WaveCleared;
                _phaseTimer = WaveClearedDuration;
            }
        }

        private void UpdateWaveCleared()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                if (_playerHealth != null)
                {
                    int healAmount = Mathf.RoundToInt(_playerHealth.MaxHealth * 0.2f);
                    _playerHealth.Heal(healAmount);
                }

                StartNextWave();
            }
        }

        private void UpdateGameOver()
        {
            if (Lf2Input.GetKeyDown(KeyCode.R))
            {
                CleanupAll();
                characterSelect.ResetSelection();
                _phase = SurvivalPhase.CharacterSelect;
            }
        }

        private void StartNextWave()
        {
            _currentWave++;
            _enemiesSpawnedThisWave = 0;
            _enemiesAliveThisWave = 0;
            _totalEnemiesThisWave = BaseEnemiesPerWave + (_currentWave - 1) * ExtraEnemiesPerWave;
            _spawnTimer = 0f;

            if (_matchHud != null) _matchHud.SetPhase($"WAVE {_currentWave}");

            _phase = SurvivalPhase.WaveIntro;
            _phaseTimer = WaveIntroDuration;
        }

        private void SpawnEnemy()
        {
            if (_playerObj == null) return;

            int enemyIdx = Random.Range(0, EnemyCharacterNames.Length);
            string enemyName = EnemyCharacterNames[enemyIdx];
            string enemyPath = EnemyCharacterPaths[enemyIdx];

            var enemyData = LoadEnemyCharacterData(enemyPath, enemyName);
            if (enemyData == null) enemyData = LoadFallbackData();

            int maxHealth = Mathf.RoundToInt(30f * Mathf.Pow(1.15f, _currentWave - 1));
            float damageScale = Mathf.Pow(1.1f, _currentWave - 1);

            float angle = Random.value * Mathf.PI * 2f;
            var spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * SpawnRadius;
            var spawnPos = _playerObj.transform.position + (Vector3)spawnOffset;

            var datBytes = GetDatBytes(enemyIdx);
            var archetype = CreateWaveArchetype(_currentWave, damageScale);

            var go = EnemyFactory.CreateEnemy(
                enemyData,
                datBytes,
                archetype,
                _playerObj.transform,
                spawnPos,
                maxHealth);

            go.name = $"Enemy_W{_currentWave}_{_enemiesSpawnedThisWave}";

            float enemyScale = 1f + (_currentWave - 1) * 0.02f;
            go.transform.localScale = Vector3.one * enemyScale;

            var health = go.GetComponent<Health>();

            _enemies.Add(go);
            _enemyHealths.Add(health);
            _enemiesSpawnedThisWave++;
            _enemiesAliveThisWave++;
        }

        private AIArchetype CreateWaveArchetype(int wave, float damageScale)
        {
            var arch = ScriptableObject.CreateInstance<AIArchetype>();
            arch.attackRange = 1.5f + wave * 0.1f;
            arch.retreatRange = 4f;
            arch.attackCooldown = Mathf.Max(0.5f, 1.5f - wave * 0.05f);
            arch.aggression = Mathf.Min(0.9f, 0.3f + wave * 0.05f);
            arch.defendChance = Mathf.Min(0.5f, 0.1f + wave * 0.02f);
            arch.thinkInterval = Mathf.Max(0.05f, 0.15f - wave * 0.005f);
            arch.defendDuration = 0.5f;
            arch.recoverDuration = Mathf.Max(0.3f, 1f - wave * 0.03f);
            arch.usesRangedAttacks = wave >= 5;
            arch.usesSpecials = wave >= 8;
            return arch;
        }

        private byte[] GetDatBytes(int enemyIdx)
        {
            if (_enemyDatabase != null)
            {
                var ids = _enemyDatabase.GetAllIds();
                if (enemyIdx < ids.Count)
                    return _enemyDatabase.GetDatBytes(ids[enemyIdx]);
            }

            if (_database != null)
            {
                var ids = _database.GetAllIds();
                for (int i = 0; i < ids.Count; i++)
                {
                    var entry = _database.characters[i];
                    if (string.Equals(entry.characterName, EnemyCharacterNames[enemyIdx], System.StringComparison.OrdinalIgnoreCase))
                        return entry.datBytes;
                }
            }

            var dat = Resources.Load<TextAsset>(EnemyCharacterPaths[enemyIdx]);
            return dat != null ? dat.bytes : null;
        }

        private void CleanDeadEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                if (_enemies[i] == null || _enemyHealths[i] == null || _enemyHealths[i].IsDead)
                {
                    if (_enemies[i] != null) Destroy(_enemies[i]);
                    _enemies.RemoveAt(i);
                    _enemyHealths.RemoveAt(i);
                    _enemiesAliveThisWave = Mathf.Max(0, _enemiesAliveThisWave - 1);
                }
            }
        }

        private void SetupPlayer()
        {
            if (_playerObj != null) Destroy(_playerObj);

            _playerObj = new GameObject("Survivor");
            var sr = _playerObj.AddComponent<SpriteRenderer>();
            Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
            sr.sprite = CreatePlaceholderSprite(new Color(1f, 0.95f, 0.35f));
            sr.sortingOrder = 10;

            _playerHealth = _playerObj.AddComponent<Health>();
            _playerHealth.ConfigureMaxHealth(200, true);
            _playerObj.AddComponent<Mana>();
            _playerObj.AddComponent<CharacterMotor>();
            _playerObj.AddComponent<Damageable>();

            var col = _playerObj.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = 0.35f;

            var rb = _playerObj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;

            _playerObj.AddComponent<DepthSortByY>();
            _playerObj.AddComponent<Lf2PlayerSpriteAnimator>();
            _playerObj.AddComponent<ArenaBounds>();

            var definition = CreateCharacterDefinition(0, _playerCharName, _playerLf2Data);

            var inputScheme = new Lf2InputScheme(0);
            _playerHsm = _playerObj.AddComponent<PlayerHsmController>();
            _playerHsm.SetInputScheme(inputScheme);
            _playerHsm.Configure(definition, _playerLf2Data, _playerProjectileMap);

            if (_matchHud != null)
            {
                _matchHud.BindPlayer(0, _playerHealth, _playerObj.GetComponent<Mana>(), _playerCharName);
                _matchHud.SetPhase("SURVIVAL");
            }

            var cam = Camera.main;
            if (cam != null)
            {
                cam.transform.position = new Vector3(0f, 0f, cam.transform.position.z);
                if (cam.GetComponent<CameraFollow2D>() == null)
                    cam.gameObject.AddComponent<CameraFollow2D>();
            }
        }

        private CharacterDefinition CreateCharacterDefinition(int id, string charName, Lf2CharacterData lf2Data)
        {
            var definition = ScriptableObject.CreateInstance<CharacterDefinition>();
            definition.lf2Id = id;
            definition.displayName = charName ?? "Unknown";
            definition.movement = lf2Data?.Movement != null
                ? CharacterMovementConfig.FromLf2Movement(lf2Data.Movement)
                : default;
            definition.frameRoleIds = lf2Data?.RoleIds ?? Lf2FrameRoleIds.BuildFromCharacterData(lf2Data);
            return definition;
        }

        private void CleanupAll()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i] != null) Destroy(_enemies[i]);
            }
            _enemies.Clear();
            _enemyHealths.Clear();

            if (_playerObj != null) Destroy(_playerObj);
            _playerObj = null;
            _playerHealth = null;
            _playerHsm = null;
        }

        private Lf2CharacterData LoadCharacterData(string datResourcePath, string charName)
        {
            if (_database != null)
            {
                var ids = _database.GetAllIds();
                for (int i = 0; i < ids.Count; i++)
                {
                    var entry = _database.characters[i];
                    if (string.Equals(entry.characterName, charName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        var data = _database.GetCharacter(entry.id);
                        if (data != null)
                            return data;
                    }
                }
            }

            if (string.IsNullOrEmpty(datResourcePath)) return null;

            var dat = Resources.Load<TextAsset>(datResourcePath);
            if (dat == null) return null;

            return Lf2DatRuntimeLoader.LoadFromBytes(dat.bytes);
        }

        private Lf2CharacterData LoadEnemyCharacterData(string datResourcePath, string charName)
        {
            if (_enemyDatabase != null)
            {
                var ids = _enemyDatabase.GetAllIds();
                for (int i = 0; i < ids.Count; i++)
                {
                    var entry = _enemyDatabase.characters[i];
                    if (string.Equals(entry.characterName, charName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        var data = _enemyDatabase.GetCharacter(entry.id);
                        if (data != null)
                            return data;
                    }
                }
            }

            return LoadCharacterData(datResourcePath, charName);
        }

        private Dictionary<int, Lf2CharacterData> LoadProjectileMap(string charName)
        {
            var map = new Dictionary<int, Lf2CharacterData>();
            var lower = (charName ?? "").ToLowerInvariant();

            string ballPath = null;
            int oid = 200;
            switch (lower)
            {
                case "davis":
                    ballPath = "LF2/davis_ball"; oid = 207; break;
                case "deep":
                    ballPath = "LF2/deep_ball"; oid = 203; break;
                case "john":
                    ballPath = "LF2/john_ball"; oid = 200; break;
                case "henry":
                    ballPath = "LF2/henry_arrow1"; oid = 200; break;
                case "firen":
                    ballPath = "LF2/firen_ball"; oid = 200; break;
            }

            if (!string.IsNullOrEmpty(ballPath))
            {
                var dat = Resources.Load<TextAsset>(ballPath);
                if (dat != null)
                {
                    var data = Lf2DatRuntimeLoader.LoadFromBytes(dat.bytes);
                    if (data?.Frames != null && data.Frames.Count > 0)
                        map[oid] = data;
                }
            }

            if (lower == "john")
            {
                var biscuitDat = Resources.Load<TextAsset>("LF2/john_biscuit");
                if (biscuitDat != null)
                {
                    var biscuitData = Lf2DatRuntimeLoader.LoadFromBytes(biscuitDat.bytes);
                    if (biscuitData?.Frames != null && biscuitData.Frames.Count > 0)
                        map[214] = biscuitData;
                }
            }

            if (lower == "henry")
            {
                var arrow2Dat = Resources.Load<TextAsset>("LF2/henry_arrow2");
                if (arrow2Dat != null)
                {
                    var arrow2Data = Lf2DatRuntimeLoader.LoadFromBytes(arrow2Dat.bytes);
                    if (arrow2Data?.Frames != null && arrow2Data.Frames.Count > 0)
                        map[201] = arrow2Data;
                }
                var windDat = Resources.Load<TextAsset>("LF2/henry_wind");
                if (windDat != null)
                {
                    var windData = Lf2DatRuntimeLoader.LoadFromBytes(windDat.bytes);
                    if (windData?.Frames != null && windData.Frames.Count > 0)
                        map[218] = windData;
                }
            }

            if (lower == "firen")
            {
                var flameDat = Resources.Load<TextAsset>("LF2/firen_flame");
                if (flameDat != null)
                {
                    var flameData = Lf2DatRuntimeLoader.LoadFromBytes(flameDat.bytes);
                    if (flameData?.Frames != null && flameData.Frames.Count > 0)
                        map[212] = flameData;
                }
            }

            return map;
        }

        private Lf2CharacterData LoadFallbackData()
        {
            return LoadCharacterData("LF2/davis", "Davis");
        }

        private static Sprite CreatePlaceholderSprite(Color tint)
        {
            const int size = 32;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;
            var c32 = new Color32(
                (byte)(tint.r * 255), (byte)(tint.g * 255),
                (byte)(tint.b * 255), 255);
            var pixels = new Color32[size * size];
            for (var i = 0; i < pixels.Length; i++) pixels[i] = c32;
            tex.SetPixels32(pixels);
            tex.Apply(false, true);
            return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 32f);
        }

        private void OnGUI()
        {
            InitStyles();

            switch (_phase)
            {
                case SurvivalPhase.WaveIntro:
                    DrawWaveIntro();
                    break;
                case SurvivalPhase.Fighting:
                    DrawSurvivalHud();
                    break;
                case SurvivalPhase.WaveCleared:
                    DrawSurvivalHud();
                    DrawCenteredText($"WAVE {_currentWave} CLEARED!", _waveStyle);
                    break;
                case SurvivalPhase.GameOver:
                    DrawGameOver();
                    break;
            }
        }

        private void DrawWaveIntro()
        {
            DrawCenteredText($"WAVE {_currentWave}", _waveStyle);

            var subStyle = new GUIStyle(_scoreStyle) { fontSize = 16 };
            DrawCenteredTextOffset($"Enemies: {_totalEnemiesThisWave}", subStyle, 40f);
        }

        private void DrawSurvivalHud()
        {
            float barW = 350f;
            float x = (Screen.width - barW) * 0.5f;

            GUI.Label(new Rect(x, 6f, barW, 24f),
                $"WAVE {_currentWave}  |  SCORE: {_score}  |  ALIVE: {_enemiesAliveThisWave}",
                _scoreStyle);
        }

        private void DrawGameOver()
        {
            DrawCenteredText("GAME OVER", _gameOverStyle);

            var subStyle = new GUIStyle(_scoreStyle) { fontSize = 20 };
            DrawCenteredTextOffset($"Waves Survived: {_currentWave - 1}", subStyle, 50f);
            DrawCenteredTextOffset($"Final Score: {_score}", subStyle, 80f);
            DrawCenteredTextOffset("Press R to restart", subStyle, 120f);
        }

        private void DrawCenteredText(string text, GUIStyle style)
        {
            DrawCenteredTextOffset(text, style, 0f);
        }

        private void DrawCenteredTextOffset(string text, GUIStyle style, float yOffset)
        {
            float w = 500f;
            float h = 50f;
            GUI.Label(new Rect((Screen.width - w) * 0.5f, (Screen.height - h) * 0.5f + yOffset, w, h), text, style);
        }

        private void InitStyles()
        {
            if (_waveStyle != null) return;

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 28,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.85f, 0.2f) }
            };

            _waveStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 40,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.3f, 0.3f) }
            };

            _scoreStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _gameOverStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 48,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.2f, 0.2f) }
            };

            _hudStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Color.white }
            };
        }
    }
}
