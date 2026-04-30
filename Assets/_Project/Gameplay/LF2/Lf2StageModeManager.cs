using System.Collections.Generic;
using Project.Data;
using Project.Gameplay.Audio;
using Project.Gameplay.Combat;
using Project.Gameplay.Modes;
using Project.Gameplay.Player;
using Project.Gameplay.Rendering;
using Project.Gameplay.Visual;
using UnityEngine;
using Project.Core.Input;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2StageModeManager : MonoBehaviour
    {
        private enum StagePhase
        {
            CharacterSelect,
            StageIntro,
            Fighting,
            StageClear,
            GameOver,
            Victory,
        }

        private enum Difficulty
        {
            Easy = 0,
            Normal = 1,
            Difficult = 2,
            CRAZY = 3,
        }

        [Header("Stage Data")]
        [SerializeField] private StageDefinition[] stageDefinitions;
        [SerializeField] private Lf2CharacterDatabase characterDatabase;

        private const int MaxStages = 5;
        private const float StageIntroDuration = 2f;
        private const float StageClearDuration = 2.5f;

        private StagePhase _phase;
        private Difficulty _difficulty;
        private int _currentStage;
        private float _phaseTimer;
        private bool _stageModeActive;

        private Lf2CharacterSelect _characterSelect;
        private StageWaveRunner _waveRunner;

        private GameObject _player;
        private Health _playerHealth;
        private PlayerHsmController _playerHsm;
        private Lf2Hud _playerHud;

        private Lf2CharacterData _playerLf2Data;
        private Dictionary<int, Lf2CharacterData> _playerProjectileMap;
        private string _playerCharName;
        private int _selectedCharacterId;

        private Texture2D _whiteTex;
        private GUIStyle _titleStyle;
        private GUIStyle _subtitleStyle;
        private GUIStyle _infoStyle;
        private GUIStyle _bigStyle;

        public void StartStageMode(int characterId, int difficulty)
        {
            _selectedCharacterId = characterId;
            _difficulty = (Difficulty)Mathf.Clamp(difficulty, 0, 3);
            _currentStage = 1;
            _stageModeActive = true;

            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();

            var csGo = new GameObject("StageCharacterSelect");
            csGo.transform.SetParent(transform);
            _characterSelect = csGo.AddComponent<Lf2CharacterSelect>();

            _phase = StagePhase.CharacterSelect;
        }

        private void OnDestroy()
        {
            if (_whiteTex != null) Destroy(_whiteTex);
            CleanupPlayer();
            CleanupWaveRunner();
        }

        private void Update()
        {
            if (!_stageModeActive) return;

            CombatReadabilityFx.Tick(Time.deltaTime);

            switch (_phase)
            {
                case StagePhase.CharacterSelect:
                    UpdateCharacterSelect();
                    break;
                case StagePhase.StageIntro:
                    UpdateStageIntro();
                    break;
                case StagePhase.Fighting:
                    UpdateFighting();
                    break;
                case StagePhase.StageClear:
                    UpdateStageClear();
                    break;
                case StagePhase.GameOver:
                    UpdateGameOver();
                    break;
                case StagePhase.Victory:
                    UpdateVictory();
                    break;
            }
        }

        // ── Character Select ─────────────────────────────────────────────

        private void UpdateCharacterSelect()
        {
            if (_characterSelect == null || !_characterSelect.P1Confirmed) return;

            var slot = _characterSelect.GetSlot(_characterSelect.P1Index);
            _playerCharName = slot.name;
            _selectedCharacterId = _characterSelect.P1Index;

            _playerLf2Data = LoadCharacterData(slot.datResourcePath, slot.name);
            _playerProjectileMap = LoadProjectileMap(slot.name);

            if (_playerLf2Data == null)
                _playerLf2Data = LoadCharacterData("LF2/davis", "Davis");

            Destroy(_characterSelect.gameObject);
            _characterSelect = null;

            StartStage(_currentStage);
        }

        // ── Stage Flow ───────────────────────────────────────────────────

        private void StartStage(int stage)
        {
            _currentStage = stage;

            CleanupWaveRunner();

            if (stageDefinitions == null || stage < 1 || stage > stageDefinitions.Length)
            {
                Debug.LogError($"[Lf2StageModeManager] No StageDefinition for stage {stage}");
                return;
            }

            var stageDef = stageDefinitions[stage - 1];
            if (stageDef == null)
            {
                Debug.LogError($"[Lf2StageModeManager] StageDefinition is null for stage {stage}");
                return;
            }

            SetupStageVisuals(stage);

            if (_player == null)
            {
                CreatePlayer();
            }
            else
            {
                _playerHealth?.ResetToFull();
                var mana = _player.GetComponent<Mana>();
                if (mana != null) mana.ResetToFull();
                _player.transform.position = Vector3.zero;
            }

            var runnerGo = new GameObject($"WaveRunner_S{stage}");
            runnerGo.transform.SetParent(transform);
            _waveRunner = runnerGo.AddComponent<StageWaveRunner>();

            float hpMul = GetDifficultyHpMultiplier();
            var effectiveDef = ApplyDifficultyToDefinition(stageDef, hpMul);
            _waveRunner.Init(effectiveDef, characterDatabase, _player.transform);
            _waveRunner.OnStageCleared += OnStageCleared;

            _waveRunner.StartStage();
            _phase = StagePhase.StageIntro;
            _phaseTimer = StageIntroDuration;

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
                audio.PlayStageMusic(stage - 1);

            Debug.Log($"[Lf2StageModeManager] Stage {stage} started ({_difficulty})");
        }

        private void OnStageCleared()
        {
            var audio = Lf2AudioManager.Instance;

            if (_currentStage >= MaxStages)
            {
                _phase = StagePhase.Victory;
                _phaseTimer = 0f;
                if (audio != null)
                {
                    audio.StopMusic();
                    audio.PlaySfx(Lf2SoundId.Victory);
                }
            }
            else
            {
                _phase = StagePhase.StageClear;
                _phaseTimer = StageClearDuration;
                if (audio != null)
                {
                    audio.StopMusic();
                    audio.PlaySfx(Lf2SoundId.RoundEnd);
                }
            }
        }

        private StageDefinition ApplyDifficultyToDefinition(StageDefinition source, float hpMul)
        {
            if (hpMul == 1f) return source;

            var copy = ScriptableObject.CreateInstance<StageDefinition>();
            copy.lf2StageId = source.lf2StageId;
            copy.displayName = source.displayName;
            copy.background = source.background;
            copy.phases = new List<StagePhaseDefinition>();

            foreach (var phase in source.phases)
            {
                var phaseCopy = new StagePhaseDefinition();
                phaseCopy.bound = phase.bound;
                phaseCopy.musicPath = phase.musicPath;
                phaseCopy.entries = new List<StageSpawnEntry>();

                foreach (var entry in phase.entries)
                {
                    phaseCopy.entries.Add(new StageSpawnEntry
                    {
                        objectId = entry.objectId,
                        hpOverride = entry.hpOverride > 0
                            ? Mathf.RoundToInt(entry.hpOverride * hpMul)
                            : Mathf.RoundToInt(100 * hpMul),
                        times = entry.times,
                        ratio = entry.ratio,
                        role = entry.role,
                    });
                }

                copy.phases.Add(phaseCopy);
            }

            return copy;
        }

        // ── Phase Updates ────────────────────────────────────────────────

        private void UpdateStageIntro()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                _phase = StagePhase.Fighting;
            }
        }

        private void UpdateFighting()
        {
            if (_playerHealth != null && _playerHealth.IsDead)
            {
                _phase = StagePhase.GameOver;
                _phaseTimer = 0f;
                return;
            }

            if (_waveRunner != null && _waveRunner.IsComplete)
            {
                OnStageCleared();
            }
        }

        private void UpdateStageClear()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                _currentStage++;
                StartStage(_currentStage);
            }
        }

        private void UpdateGameOver()
        {
            if (Lf2Input.GetKeyDown(KeyCode.R))
            {
                StartStage(_currentStage);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }

        private void UpdateVictory()
        {
            if (Lf2Input.GetKeyDown(KeyCode.R))
            {
                _currentStage = 1;
                StartStage(1);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }

        private void ReturnToMenu()
        {
            _stageModeActive = false;
            CleanupPlayer();
            CleanupWaveRunner();
            Destroy(gameObject);
        }

        // ── Stage Visuals ────────────────────────────────────────────────

        private void SetupStageVisuals(int stage)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                cam.transform.position = new Vector3(0f, 0f, cam.transform.position.z);
            }

            var bg = GameObject.Find("StageBackground");
            if (bg == null)
            {
                bg = new GameObject("StageBackground");
                var sr = bg.AddComponent<SpriteRenderer>();
                sr.sprite = CreateStageBackground(stage);
                sr.color = GetStageColor(stage);
                sr.sortingOrder = -100;
                bg.transform.position = new Vector3(0f, 1f, 0f);
            }
            else
            {
                var sr = bg.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = CreateStageBackground(stage);
                    sr.color = GetStageColor(stage);
                }
            }
        }

        private Color GetStageColor(int stage)
        {
            switch (stage)
            {
                case 1: return new Color(0.3f, 0.5f, 0.3f);
                case 2: return new Color(0.4f, 0.3f, 0.2f);
                case 3: return new Color(0.2f, 0.2f, 0.4f);
                case 4: return new Color(0.35f, 0.25f, 0.2f);
                case 5: return new Color(0.2f, 0.15f, 0.25f);
                default: return Color.gray;
            }
        }

        private Sprite CreateStageBackground(int stage)
        {
            const int w = 64;
            const int h = 32;
            var tex = new Texture2D(w, h, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            var baseColor = GetStageColor(stage);
            var pixels = new Color32[w * h];

            for (int y = 0; y < h; y++)
            {
                float t = (float)y / h;
                var c = Color.Lerp(baseColor * 0.6f, baseColor * 1.2f, t);
                var c32 = new Color32(
                    (byte)Mathf.Clamp(c.r * 255, 0, 255),
                    (byte)Mathf.Clamp(c.g * 255, 0, 255),
                    (byte)Mathf.Clamp(c.b * 255, 0, 255),
                    255);

                for (int x = 0; x < w; x++)
                    pixels[y * w + x] = c32;
            }

            tex.SetPixels32(pixels);
            tex.Apply(false, true);
            return Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), 16f);
        }

        // ── Player Creation ──────────────────────────────────────────────

        private void CreatePlayer()
        {
            CleanupPlayer();

            _player = new GameObject("Player");

            var sr = _player.AddComponent<SpriteRenderer>();
            Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
            sr.sprite = CreatePlaceholderSprite(new Color(1f, 0.95f, 0.35f));
            sr.sortingOrder = 10;

            var health = _player.AddComponent<Health>();
            health.ConfigureMaxHealth(100, true);
            _player.AddComponent<Mana>();
            _player.AddComponent<CharacterMotor>();
            _player.AddComponent<Damageable>();

            var col = _player.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = 0.35f;

            var rb = _player.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;

            _player.AddComponent<DepthSortByY>();
            _player.AddComponent<Lf2PlayerSpriteAnimator>();
            _player.AddComponent<ArenaBounds>();

            var hud = _player.AddComponent<Lf2Hud>();
            hud.Bind(health, _player.GetComponent<Mana>(), _playerCharName ?? "Davis", _selectedCharacterId);

            var hsm = _player.AddComponent<PlayerHsmController>();
            hsm.SetInputScheme(new Lf2InputScheme(0));

            var definition = ScriptableObject.CreateInstance<CharacterDefinition>();
            definition.lf2Id = _selectedCharacterId;
            definition.displayName = _playerCharName ?? "Davis";
            definition.movement = _playerLf2Data?.Movement != null
                ? CharacterMovementConfig.FromLf2Movement(_playerLf2Data.Movement)
                : default;
            definition.frameRoleIds = _playerLf2Data?.RoleIds ?? Lf2FrameRoleIds.BuildFromCharacterData(_playerLf2Data);

            hsm.Configure(definition, _playerLf2Data, _playerProjectileMap);

            _playerHealth = health;
            _playerHsm = hsm;
            _playerHud = hud;

            _player.transform.position = Vector3.zero;

            var cam = Camera.main;
            if (cam != null)
            {
                if (cam.GetComponent<CameraFollow2D>() == null)
                    cam.gameObject.AddComponent<CameraFollow2D>();
            }

            var charLower = (_playerCharName ?? "davis").ToLowerInvariant();
            Lf2VisualLibrary.SetPlayerCharacter(charLower);
        }

        private void CleanupPlayer()
        {
            if (_player != null)
                Destroy(_player);
            _player = null;
            _playerHealth = null;
            _playerHsm = null;
            _playerHud = null;
        }

        private void CleanupWaveRunner()
        {
            if (_waveRunner != null)
            {
                _waveRunner.OnStageCleared -= OnStageCleared;
                _waveRunner.StopStage();
                Destroy(_waveRunner.gameObject);
            }
            _waveRunner = null;
        }

        // ── Difficulty ───────────────────────────────────────────────────

        private float GetDifficultyHpMultiplier()
        {
            switch (_difficulty)
            {
                case Difficulty.Easy: return 0.6f;
                case Difficulty.Normal: return 1f;
                case Difficulty.Difficult: return 1.5f;
                case Difficulty.CRAZY: return 2.2f;
                default: return 1f;
            }
        }

        // ── Character Data Loading ───────────────────────────────────────

        private Lf2CharacterData LoadCharacterData(string datResourcePath, string charName)
        {
            if (string.IsNullOrEmpty(datResourcePath)) return null;

            var dat = Resources.Load<TextAsset>(datResourcePath);
            if (dat == null)
            {
                Debug.LogWarning($"[Lf2StageModeManager] Could not load {datResourcePath}");
                return null;
            }

            var data = Lf2DatRuntimeLoader.LoadFromBytes(dat.bytes);
            if (data?.Frames != null && data.Frames.Count > 0)
                Debug.Log($"[Lf2StageModeManager] {charName} loaded: {data.Frames.Count} frames");
            return data;
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
                    ballPath = "LF2/davis_ball";
                    oid = 207;
                    break;
                case "deep":
                    ballPath = "LF2/deep_ball";
                    oid = 203;
                    break;
                case "john":
                    ballPath = "LF2/john_ball";
                    oid = 200;
                    break;
                case "henry":
                    ballPath = "LF2/henry_arrow1";
                    oid = 200;
                    break;
                case "firen":
                    ballPath = "LF2/firen_ball";
                    oid = 200;
                    break;
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

        // ── Helpers ──────────────────────────────────────────────────────

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

        // ── GUI ──────────────────────────────────────────────────────────

        private void OnGUI()
        {
            InitStyles();

            switch (_phase)
            {
                case StagePhase.StageIntro:
                    DrawStageIntro();
                    break;
                case StagePhase.Fighting:
                    DrawStageHud();
                    break;
                case StagePhase.StageClear:
                    DrawStageClear();
                    break;
                case StagePhase.GameOver:
                    DrawGameOver();
                    break;
                case StagePhase.Victory:
                    DrawVictory();
                    break;
            }
        }

        private void DrawStageIntro()
        {
            DrawCenteredText($"STAGE {_currentStage}", _bigStyle);

            string diffName = _difficulty.ToString().ToUpper();
            DrawCenteredTextOffset(diffName, _subtitleStyle, 50f);

            string stageName = GetStageName(_currentStage);
            DrawCenteredTextOffset(stageName, _infoStyle, 85f);
        }

        private void DrawStageHud()
        {
            float barW = 400f;
            float x = (Screen.width - barW) * 0.5f;

            string diffLabel = _difficulty.ToString();
            GUI.Label(new Rect(x, 6f, barW, 24f),
                $"Stage {_currentStage}/{MaxStages}  [{diffLabel}]  {_playerCharName}",
                _subtitleStyle);

            if (_waveRunner != null)
            {
                int phase = _waveRunner.CurrentPhase + 1;
                int total = _waveRunner.Definition.phases.Count;
                string state = _waveRunner.IsComplete ? "Victory" : "Active";
                GUI.Label(new Rect(x, 28f, barW, 20f),
                    $"Phase {phase}/{total}  ({state})",
                    _infoStyle);
            }
        }

        private void DrawStageClear()
        {
            DrawCenteredText("STAGE CLEAR!", _bigStyle);
            DrawCenteredTextOffset($"Stage {_currentStage} completed!", _subtitleStyle, 50f);
        }

        private void DrawGameOver()
        {
            DrawCenteredText("GAME OVER", _bigStyle);
            DrawCenteredTextOffset($"Reached Stage {_currentStage}", _subtitleStyle, 50f);
            DrawCenteredTextOffset("Press R to retry | ESC to quit", _infoStyle, 85f);
        }

        private void DrawVictory()
        {
            DrawCenteredText("VICTORY!", _bigStyle);
            DrawCenteredTextOffset($"{_playerCharName} conquered all stages!", _subtitleStyle, 50f);
            DrawCenteredTextOffset($"Difficulty: {_difficulty}", _infoStyle, 80f);
            DrawCenteredTextOffset("Press R to replay | ESC to quit", _infoStyle, 110f);
        }

        private string GetStageName(int stage)
        {
            if (stageDefinitions != null && stage >= 1 && stage <= stageDefinitions.Length)
            {
                var def = stageDefinitions[stage - 1];
                if (def != null) return def.displayName;
            }
            return $"Stage {stage}";
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
            if (_titleStyle != null) return;

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 36,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _bigStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 42,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.9f, 0.3f) }
            };

            _subtitleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.85f, 0.5f) }
            };

            _infoStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.8f, 0.8f, 0.8f) }
            };
        }
    }
}
