using System.Collections.Generic;
using Project.Core.Input;
using Project.Data;
using Project.Gameplay.Audio;
using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using Project.Gameplay.Rendering;
using Project.Gameplay.Visual;
using Project.UI.HUD;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2BattleManager : MonoBehaviour
    {
        public enum BattleModeType
        {
            FreeForAll,
            TeamBattle
        }

        public struct BattleConfig
        {
            public BattleModeType mode;
            public int playerCount;
            public int teamCount;
            public bool itemsEnabled;
            public int stageIndex;
        }

        private enum BattlePhase
        {
            Config,
            CharacterSelect,
            RoundIntro,
            Fighting,
            RoundEnd,
            MatchEnd
        }

        private const int WinsToVictory = 2;
        private const float RoundIntroDuration = 1.5f;
        private const float RoundEndDuration = 2f;

        [SerializeField] private Lf2CharacterSelect characterSelect;
        [SerializeField] private Lf2CharacterDatabase _database;
        [SerializeField] private MatchHud _matchHud;

        private BattlePhase _phase;
        private BattleConfig _config;
        private float _phaseTimer;

        private readonly List<GameObject> _players = new List<GameObject>(8);
        private readonly List<Health> _playerHealths = new List<Health>(8);
        private readonly List<PlayerHsmController> _playerHsms = new List<PlayerHsmController>(8);
        private readonly List<int> _teamScores = new List<int>(4);
        private readonly List<int> _playerTeams = new List<int>(8);
        private readonly List<string> _playerCharNames = new List<string>(8);
        private readonly List<Lf2CharacterData> _playerLf2Datas = new List<Lf2CharacterData>(8);
        private readonly List<Dictionary<int, Lf2CharacterData>> _playerProjMaps = new List<Dictionary<int, Lf2CharacterData>>(8);

        private int _currentRound;

        private Texture2D _whiteTex;
        private GUIStyle _titleStyle;
        private GUIStyle _optionStyle;
        private GUIStyle _selectedStyle;
        private GUIStyle _roundStyle;
        private GUIStyle _winStyle;
        private GUIStyle _scoreStyle;
        private GUIStyle _victoryStyle;

        private int _configCursor;

        private static readonly Color[] TeamColors =
        {
            new Color(1f, 0.95f, 0.35f),
            new Color(0.35f, 0.6f, 1f),
            new Color(0.4f, 1f, 0.4f),
            new Color(1f, 0.4f, 0.8f),
        };

        private static readonly string[] PlayerCountLabels = { "2 Players", "3 Players", "4 Players" };
        private static readonly string[] ModeLabels = { "Free For All", "Team Battle" };
        private static readonly string[] TeamCountLabels = { "2 Teams", "3 Teams" };
        private static readonly string[] ItemLabels = { "Items OFF", "Items ON" };

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

            _config = new BattleConfig
            {
                mode = BattleModeType.FreeForAll,
                playerCount = 2,
                teamCount = 2,
                itemsEnabled = false,
                stageIndex = 0
            };

            _phase = BattlePhase.Config;
        }

        private void OnDestroy()
        {
            if (_whiteTex != null) Destroy(_whiteTex);
            CleanupPlayers();
        }

        private void Update()
        {
            CombatReadabilityFx.Tick(Time.deltaTime);

            switch (_phase)
            {
                case BattlePhase.Config:
                    UpdateConfig();
                    break;
                case BattlePhase.CharacterSelect:
                    UpdateCharacterSelect();
                    break;
                case BattlePhase.RoundIntro:
                    UpdateRoundIntro();
                    break;
                case BattlePhase.Fighting:
                    UpdateFighting();
                    break;
                case BattlePhase.RoundEnd:
                    UpdateRoundEnd();
                    break;
                case BattlePhase.MatchEnd:
                    UpdateMatchEnd();
                    break;
            }
        }

        private void UpdateConfig()
        {
            if (Lf2Input.GetKeyDown(KeyCode.UpArrow))
                _configCursor = (_configCursor - 1 + 5) % 5;
            else if (Lf2Input.GetKeyDown(KeyCode.DownArrow))
                _configCursor = (_configCursor + 1) % 5;

            if (Lf2Input.GetKeyDown(KeyCode.LeftArrow) || Lf2Input.GetKeyDown(KeyCode.RightArrow))
            {
                int dir = Lf2Input.GetKeyDown(KeyCode.RightArrow) ? 1 : -1;
                switch (_configCursor)
                {
                    case 0:
                        _config.playerCount = Mathf.Clamp(_config.playerCount + dir, 2, 4);
                        break;
                    case 1:
                        _config.mode = _config.mode == BattleModeType.FreeForAll
                            ? BattleModeType.TeamBattle
                            : BattleModeType.FreeForAll;
                        break;
                    case 2:
                        _config.teamCount = Mathf.Clamp(_config.teamCount + dir, 2, 3);
                        break;
                    case 3:
                        _config.itemsEnabled = !_config.itemsEnabled;
                        break;
                    case 4:
                        _config.stageIndex = (_config.stageIndex + dir + 5) % 5;
                        break;
                }
            }

            if (Lf2Input.GetKeyDown(KeyCode.Return) || Lf2Input.GetKeyDown(KeyCode.J))
            {
                _phase = BattlePhase.CharacterSelect;
                characterSelect.ResetSelection();
            }
        }

        private void UpdateCharacterSelect()
        {
            int needed = _config.playerCount;
            bool allReady = characterSelect.P1Confirmed;
            if (needed >= 2) allReady = allReady && characterSelect.P2Confirmed;

            if (!allReady) return;

            LoadAllCharacters();
            AssignTeams();
            SetupPlayers();
            StartRound();
        }

        private void UpdateRoundIntro()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
                _phase = BattlePhase.Fighting;
        }

        private void UpdateFighting()
        {
            int aliveCount = 0;
            int lastAliveTeam = -1;
            bool allSameTeam = true;

            for (int i = 0; i < _playerHealths.Count; i++)
            {
                if (_playerHealths[i] != null && !_playerHealths[i].IsDead)
                {
                    aliveCount++;
                    int team = _playerTeams[i];
                    if (lastAliveTeam < 0)
                        lastAliveTeam = team;
                    else if (team != lastAliveTeam)
                        allSameTeam = false;
                }
            }

            if (aliveCount <= 1 || (_config.mode == BattleModeType.TeamBattle && allSameTeam && aliveCount > 0))
            {
                if (lastAliveTeam >= 0 && lastAliveTeam < _teamScores.Count)
                    _teamScores[lastAliveTeam]++;
                EndRound();
            }
        }

        private void UpdateRoundEnd()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                bool matchOver = false;
                for (int i = 0; i < _teamScores.Count; i++)
                {
                    if (_teamScores[i] >= WinsToVictory)
                    {
                        matchOver = true;
                        break;
                    }
                }

                if (matchOver)
                {
                    _phase = BattlePhase.MatchEnd;
                    _phaseTimer = 0f;
                    var audio = Lf2AudioManager.Instance;
                    if (audio != null)
                        audio.PlaySfx(Lf2SoundId.Victory);
                }
                else
                {
                    ResetRound();
                    StartRound();
                }
            }
        }

        private void UpdateMatchEnd()
        {
            if (Lf2Input.GetKeyDown(KeyCode.R))
            {
                CleanupPlayers();
                _playerLf2Datas.Clear();
                _playerProjMaps.Clear();
                _playerCharNames.Clear();
                _playerTeams.Clear();
                _teamScores.Clear();
                _currentRound = 0;
                characterSelect.ResetSelection();
                _phase = BattlePhase.Config;
            }
        }

        private void LoadAllCharacters()
        {
            _playerLf2Datas.Clear();
            _playerProjMaps.Clear();
            _playerCharNames.Clear();

            for (int i = 0; i < _config.playerCount; i++)
            {
                int slotIdx = i == 0 ? characterSelect.P1Index : characterSelect.P2Index;
                if (i >= 2)
                    slotIdx = (slotIdx + i) % characterSelect.CharacterCount;

                var slot = characterSelect.GetSlot(slotIdx);
                _playerCharNames.Add(slot.name);

                var data = LoadCharacterData(slot.datResourcePath, slot.name);
                _playerLf2Datas.Add(data ?? LoadFallbackData());
                _playerProjMaps.Add(LoadProjectileMap(slot.name));
            }
        }

        private void AssignTeams()
        {
            _playerTeams.Clear();
            _teamScores.Clear();

            if (_config.mode == BattleModeType.FreeForAll)
            {
                for (int i = 0; i < _config.playerCount; i++)
                    _playerTeams.Add(i);
                for (int i = 0; i < _config.playerCount; i++)
                    _teamScores.Add(0);
            }
            else
            {
                for (int i = 0; i < _config.playerCount; i++)
                    _playerTeams.Add(i % _config.teamCount);
                for (int i = 0; i < _config.teamCount; i++)
                    _teamScores.Add(0);
            }
        }

        private void StartRound()
        {
            _currentRound++;
            ResetPlayersHealth();
            PositionPlayers();

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i] != null) _players[i].SetActive(true);
            }

            if (_matchHud != null) _matchHud.SetRound(_currentRound);

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                audio.PlaySfx(Lf2SoundId.RoundStart);
                audio.PlayStageMusic(_config.stageIndex);
            }

            _phase = BattlePhase.RoundIntro;
            _phaseTimer = RoundIntroDuration;
        }

        private void EndRound()
        {
            _phase = BattlePhase.RoundEnd;
            _phaseTimer = RoundEndDuration;

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                audio.StopMusic();
                audio.PlaySfx(Lf2SoundId.RoundEnd);
            }

            for (int i = 0; i < _playerHsms.Count; i++)
            {
                if (_playerHsms[i] != null) _playerHsms[i].enabled = false;
            }
        }

        private void ResetRound()
        {
            for (int i = 0; i < _playerHsms.Count; i++)
            {
                if (_playerHsms[i] != null) _playerHsms[i].enabled = true;
            }
        }

        private void ResetPlayersHealth()
        {
            for (int i = 0; i < _playerHealths.Count; i++)
            {
                if (_playerHealths[i] != null) _playerHealths[i].ResetToFull();
                if (_players[i] != null)
                {
                    var mana = _players[i].GetComponent<Mana>();
                    if (mana != null) mana.ResetToFull();
                }
            }
        }

        private void PositionPlayers()
        {
            float spacing = 3f;
            float startX = -spacing * (_config.playerCount - 1) * 0.5f;
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i] != null)
                    _players[i].transform.position = new Vector3(startX + i * spacing, 0f, 0f);
            }
        }

        private void SetupPlayers()
        {
            CleanupPlayers();

            for (int i = 0; i < _config.playerCount; i++)
            {
                string pName = $"P{i + 1}";
                var go = CreatePlayer(pName, i, _playerLf2Datas[i], _playerProjMaps[i], _playerCharNames[i]);
                _players.Add(go);
                _playerHealths.Add(go.GetComponent<Health>());
                _playerHsms.Add(go.GetComponent<PlayerHsmController>());
            }

            if (_matchHud != null)
            {
                for (int i = 0; i < Mathf.Min(_config.playerCount, 2); i++)
                {
                    _matchHud.BindPlayer(i, _playerHealths[i], _players[i].GetComponent<Mana>(), _playerCharNames[i]);
                }
                _matchHud.SetPhase(_config.mode == BattleModeType.TeamBattle ? "TEAM BATTLE" : "FREE FOR ALL");
            }

            PositionPlayers();

            var cam = Camera.main;
            if (cam != null)
            {
                cam.transform.position = new Vector3(0f, 0f, cam.transform.position.z);
                if (cam.GetComponent<CameraFollow2D>() == null)
                    cam.gameObject.AddComponent<CameraFollow2D>();
            }
        }

        private void CleanupPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i] != null) Destroy(_players[i]);
            }
            _players.Clear();
            _playerHealths.Clear();
            _playerHsms.Clear();
        }

        private GameObject CreatePlayer(string playerName, int playerIndex,
            Lf2CharacterData lf2Data, Dictionary<int, Lf2CharacterData> projectileMap, string charName)
        {
            int teamIdx = playerIndex < _playerTeams.Count ? _playerTeams[playerIndex] : 0;
            Color tint = teamIdx < TeamColors.Length ? TeamColors[teamIdx] : Color.white;

            var go = new GameObject(playerName);
            var sr = go.AddComponent<SpriteRenderer>();
            Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
            sr.sprite = CreatePlaceholderSprite(tint);
            sr.sortingOrder = 10 + playerIndex;

            var health = go.AddComponent<Health>();
            health.ConfigureMaxHealth(100, true);
            go.AddComponent<Mana>();
            go.AddComponent<CharacterMotor>();
            go.AddComponent<Damageable>();

            var col = go.AddComponent<CircleCollider2D>();
            col.isTrigger = true;
            col.radius = 0.35f;

            var rb = go.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;

            go.AddComponent<DepthSortByY>();
            go.AddComponent<Lf2PlayerSpriteAnimator>();
            go.AddComponent<ArenaBounds>();

            var definition = CreateCharacterDefinition(playerIndex, charName, lf2Data);

            var inputScheme = new Lf2InputScheme(playerIndex <= 1 ? playerIndex : 0);
            var hsm = go.AddComponent<PlayerHsmController>();
            hsm.SetInputScheme(inputScheme);
            hsm.Configure(definition, lf2Data, projectileMap);

            var charLower = (charName ?? "davis").ToLowerInvariant();
            if (playerIndex == 0)
                Lf2VisualLibrary.SetPlayerCharacter(charLower);

            return go;
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
            if (dat == null)
            {
                Debug.LogWarning($"[Lf2BattleManager] Could not load {datResourcePath}");
                return null;
            }

            var fallbackData = Lf2DatRuntimeLoader.LoadFromBytes(dat.bytes);
            if (fallbackData?.Frames != null && fallbackData.Frames.Count > 0)
                Debug.Log($"[Lf2BattleManager] {charName} loaded: {fallbackData.Frames.Count} frames");
            return fallbackData;
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
                case BattlePhase.Config:
                    DrawConfigScreen();
                    break;
                case BattlePhase.RoundIntro:
                    DrawCenteredText($"ROUND {_currentRound}", _roundStyle);
                    break;
                case BattlePhase.Fighting:
                    DrawBattleScoreBar();
                    break;
                case BattlePhase.RoundEnd:
                    DrawBattleScoreBar();
                    DrawCenteredText("ROUND END", _winStyle);
                    break;
                case BattlePhase.MatchEnd:
                    DrawMatchEnd();
                    break;
            }
        }

        private void DrawConfigScreen()
        {
            float w = 400f;
            float startX = (Screen.width - w) * 0.5f;
            float startY = 100f;
            float lineH = 36f;

            GUI.Label(new Rect(startX, 40f, w, 40f), "BATTLE CONFIGURATION", _titleStyle);

            string[] options =
            {
                PlayerCountLabels[_config.playerCount - 2],
                ModeLabels[(int)_config.mode],
                TeamCountLabels[Mathf.Clamp(_config.teamCount - 2, 0, 1)],
                ItemLabels[_config.itemsEnabled ? 1 : 0],
                $"Stage: {_config.stageIndex + 1}"
            };

            for (int i = 0; i < options.Length; i++)
            {
                var style = i == _configCursor ? _selectedStyle : _optionStyle;
                GUI.Label(new Rect(startX, startY + i * lineH, w, lineH), options[i], style);
            }

            var hintStyle = new GUIStyle(_optionStyle) { fontSize = 12, normal = { textColor = new Color(0.6f, 0.6f, 0.6f) } };
            GUI.Label(new Rect(startX, startY + options.Length * lineH + 20f, w, lineH),
                "Arrows: Navigate | Left/Right: Change | Enter: Start", hintStyle);
        }

        private void DrawBattleScoreBar()
        {
            float barW = 400f;
            float x = (Screen.width - barW) * 0.5f;
            string scoreText = "";

            for (int i = 0; i < _teamScores.Count; i++)
            {
                if (i > 0) scoreText += "  |  ";
                string label = _config.mode == BattleModeType.TeamBattle ? $"Team {i + 1}" : _playerCharNames.Count > i ? _playerCharNames[i] : $"P{i + 1}";
                scoreText += $"{label}: {_teamScores[i]}";
            }

            GUI.Label(new Rect(x, 6f, barW, 28f), scoreText, _scoreStyle);
        }

        private void DrawMatchEnd()
        {
            int winner = 0;
            int maxScore = 0;
            for (int i = 0; i < _teamScores.Count; i++)
            {
                if (_teamScores[i] > maxScore)
                {
                    maxScore = _teamScores[i];
                    winner = i;
                }
            }

            string winnerLabel = _config.mode == BattleModeType.TeamBattle
                ? $"TEAM {winner + 1} WINS!"
                : $"{_playerCharNames[winner]} WINS!";

            DrawCenteredText(winnerLabel, _victoryStyle);

            var subStyle = new GUIStyle(_scoreStyle) { fontSize = 16 };
            DrawCenteredTextOffset("Press R to return to config", subStyle, 60f);
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
                fontSize = 28,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.85f, 0.2f) }
            };

            _optionStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _selectedStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.9f, 0.3f) }
            };

            _roundStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 36,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _winStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 28,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.85f, 0.2f) }
            };

            _scoreStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _victoryStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 42,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.9f, 0.3f) }
            };
        }
    }
}
