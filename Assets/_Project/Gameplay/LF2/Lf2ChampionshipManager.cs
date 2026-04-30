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
    public sealed class Lf2ChampionshipManager : MonoBehaviour
    {
        private enum ChampPhase
        {
            CharacterSelect,
            BracketDisplay,
            RoundIntro,
            Fighting,
            RoundEnd,
            MatchEnd,
            TournamentEnd
        }

        private const int WinsToVictory = 2;
        private const float RoundIntroDuration = 1.5f;
        private const float RoundEndDuration = 2f;
        private const float BracketDisplayDuration = 2.5f;

        [SerializeField] private Lf2CharacterSelect characterSelect;
        [SerializeField] private Lf2CharacterDatabase _database;
        [SerializeField] private MatchHud _matchHud;

        private ChampPhase _phase;
        private float _phaseTimer;

        private int _playerWins;
        private int _opponentWins;
        private int _currentRound;

        private int _playerSlotIndex;
        private int _currentOpponentIndex;
        private List<int> _opponentQueue;
        private int _tournamentWins;

        private GameObject _playerObj;
        private GameObject _opponentObj;
        private Health _playerHealth;
        private Health _opponentHealth;
        private PlayerHsmController _playerHsm;
        private PlayerHsmController _opponentHsm;

        private Lf2CharacterData _playerLf2Data;
        private Lf2CharacterData _opponentLf2Data;
        private Dictionary<int, Lf2CharacterData> _playerProjectileMap;
        private Dictionary<int, Lf2CharacterData> _opponentProjectileMap;
        private string _playerCharName;
        private string _opponentCharName;

        private Texture2D _whiteTex;
        private GUIStyle _titleStyle;
        private GUIStyle _roundStyle;
        private GUIStyle _winStyle;
        private GUIStyle _scoreStyle;
        private GUIStyle _victoryStyle;
        private GUIStyle _bracketStyle;

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

            _phase = ChampPhase.CharacterSelect;
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
                case ChampPhase.CharacterSelect:
                    UpdateCharacterSelect();
                    break;
                case ChampPhase.BracketDisplay:
                    UpdateBracketDisplay();
                    break;
                case ChampPhase.RoundIntro:
                    UpdateRoundIntro();
                    break;
                case ChampPhase.Fighting:
                    UpdateFighting();
                    break;
                case ChampPhase.RoundEnd:
                    UpdateRoundEnd();
                    break;
                case ChampPhase.MatchEnd:
                    UpdateMatchEnd();
                    break;
                case ChampPhase.TournamentEnd:
                    UpdateTournamentEnd();
                    break;
            }
        }

        private void UpdateCharacterSelect()
        {
            if (!characterSelect.P1Confirmed) return;

            _playerSlotIndex = characterSelect.P1Index;
            var p1Slot = characterSelect.GetSlot(_playerSlotIndex);
            _playerCharName = p1Slot.name;

            _playerLf2Data = LoadCharacterData(p1Slot.datResourcePath, p1Slot.name);
            _playerProjectileMap = LoadProjectileMap(p1Slot.name);
            if (_playerLf2Data == null) _playerLf2Data = LoadFallbackData();

            BuildOpponentQueue();
            _tournamentWins = 0;
            _currentOpponentIndex = 0;

            StartNextMatch();
        }

        private void UpdateBracketDisplay()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
                StartRound();
        }

        private void UpdateRoundIntro()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
                _phase = ChampPhase.Fighting;
        }

        private void UpdateFighting()
        {
            if (_playerHealth == null || _opponentHealth == null) return;

            if (_playerHealth.IsDead)
            {
                _opponentWins++;
                EndRound();
            }
            else if (_opponentHealth.IsDead)
            {
                _playerWins++;
                EndRound();
            }
        }

        private void UpdateRoundEnd()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                if (_playerWins >= WinsToVictory || _opponentWins >= WinsToVictory)
                {
                    if (_playerWins >= WinsToVictory)
                    {
                        _tournamentWins++;
                        _phase = ChampPhase.MatchEnd;
                    }
                    else
                    {
                        _phase = ChampPhase.TournamentEnd;
                    }
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
            if (Lf2Input.GetKeyDown(KeyCode.Return) || Lf2Input.GetKeyDown(KeyCode.J))
            {
                _currentOpponentIndex++;
                if (_currentOpponentIndex >= _opponentQueue.Count)
                {
                    _phase = ChampPhase.TournamentEnd;
                }
                else
                {
                    StartNextMatch();
                }
            }
        }

        private void UpdateTournamentEnd()
        {
            if (Lf2Input.GetKeyDown(KeyCode.R))
            {
                CleanupPlayers();
                characterSelect.ResetSelection();
                _tournamentWins = 0;
                _currentOpponentIndex = 0;
                _phase = ChampPhase.CharacterSelect;
            }
        }

        private void BuildOpponentQueue()
        {
            _opponentQueue = new List<int>();
            int totalSlots = characterSelect.CharacterCount;

            for (int i = 0; i < totalSlots; i++)
            {
                if (i == _playerSlotIndex) continue;
                var slot = characterSelect.GetSlot(i);
                if (string.IsNullOrEmpty(slot.datResourcePath)) continue;
                _opponentQueue.Add(i);
            }
        }

        private void StartNextMatch()
        {
            CleanupPlayers();

            int oppSlotIndex = _opponentQueue[_currentOpponentIndex];
            var oppSlot = characterSelect.GetSlot(oppSlotIndex);
            _opponentCharName = oppSlot.name;

            _opponentLf2Data = LoadCharacterData(oppSlot.datResourcePath, oppSlot.name);
            _opponentProjectileMap = LoadProjectileMap(oppSlot.name);
            if (_opponentLf2Data == null) _opponentLf2Data = LoadFallbackData();

            _playerWins = 0;
            _opponentWins = 0;
            _currentRound = 0;

            SetupPlayers();

            _phase = ChampPhase.BracketDisplay;
            _phaseTimer = BracketDisplayDuration;
        }

        private void StartRound()
        {
            _currentRound++;
            ResetPlayersHealth();
            PositionPlayers();

            if (_playerObj != null) _playerObj.SetActive(true);
            if (_opponentObj != null) _opponentObj.SetActive(true);

            if (_matchHud != null) _matchHud.SetRound(_currentRound);

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                audio.PlaySfx(Lf2SoundId.RoundStart);
                audio.PlayStageMusic(0);
            }

            _phase = ChampPhase.RoundIntro;
            _phaseTimer = RoundIntroDuration;
        }

        private void EndRound()
        {
            _phase = ChampPhase.RoundEnd;
            _phaseTimer = RoundEndDuration;

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                audio.StopMusic();
                audio.PlaySfx(Lf2SoundId.RoundEnd);
            }

            if (_playerHsm != null) _playerHsm.enabled = false;
            if (_opponentHsm != null) _opponentHsm.enabled = false;
        }

        private void ResetRound()
        {
            if (_playerHsm != null) _playerHsm.enabled = true;
            if (_opponentHsm != null) _opponentHsm.enabled = true;
        }

        private void ResetPlayersHealth()
        {
            if (_playerHealth != null) _playerHealth.ResetToFull();
            if (_opponentHealth != null) _opponentHealth.ResetToFull();

            if (_playerObj != null)
            {
                var mana = _playerObj.GetComponent<Mana>();
                if (mana != null) mana.ResetToFull();
            }
            if (_opponentObj != null)
            {
                var mana = _opponentObj.GetComponent<Mana>();
                if (mana != null) mana.ResetToFull();
            }
        }

        private void PositionPlayers()
        {
            if (_playerObj != null) _playerObj.transform.position = new Vector3(-3f, 0f, 0f);
            if (_opponentObj != null) _opponentObj.transform.position = new Vector3(3f, 0f, 0f);
        }

        private void SetupPlayers()
        {
            _playerObj = CreatePlayer("P1", 0, _playerLf2Data, _playerProjectileMap, _playerCharName);
            _opponentObj = CreatePlayer("CPU", 1, _opponentLf2Data, _opponentProjectileMap, _opponentCharName);

            _playerHealth = _playerObj.GetComponent<Health>();
            _opponentHealth = _opponentObj.GetComponent<Health>();
            _playerHsm = _playerObj.GetComponent<PlayerHsmController>();
            _opponentHsm = _opponentObj.GetComponent<PlayerHsmController>();

            if (_matchHud != null)
            {
                _matchHud.BindPlayer(0, _playerHealth, _playerObj.GetComponent<Mana>(), _playerCharName);
                _matchHud.BindPlayer(1, _opponentHealth, _opponentObj.GetComponent<Mana>(), _opponentCharName);
                _matchHud.SetPhase("CHAMPIONSHIP MATCH");
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
            if (_playerObj != null) Destroy(_playerObj);
            if (_opponentObj != null) Destroy(_opponentObj);
            _playerObj = null;
            _opponentObj = null;
            _playerHealth = null;
            _opponentHealth = null;
            _playerHsm = null;
            _opponentHsm = null;
        }

        private GameObject CreatePlayer(string playerName, int playerIndex,
            Lf2CharacterData lf2Data, Dictionary<int, Lf2CharacterData> projectileMap, string charName)
        {
            var go = new GameObject(playerName);
            var sr = go.AddComponent<SpriteRenderer>();
            Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
            sr.sprite = CreatePlaceholderSprite(playerIndex == 0
                ? new Color(1f, 0.95f, 0.35f)
                : new Color(1f, 0.4f, 0.4f));
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

            var definition = CreateCharacterDefinition(playerIndex, charName, lf2Data, projectileMap);

            var inputScheme = new Lf2InputScheme(playerIndex);
            var hsm = go.AddComponent<PlayerHsmController>();
            hsm.SetInputScheme(inputScheme);
            hsm.Configure(definition, lf2Data, projectileMap);

            var charLower = (charName ?? "davis").ToLowerInvariant();
            if (playerIndex == 0)
                Lf2VisualLibrary.SetPlayerCharacter(charLower);

            return go;
        }

        private CharacterDefinition CreateCharacterDefinition(int id, string charName,
            Lf2CharacterData lf2Data, Dictionary<int, Lf2CharacterData> projectileMap)
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
                Debug.LogWarning($"[Lf2ChampionshipManager] Could not load {datResourcePath}");
                return null;
            }

            var fallbackData = Lf2DatRuntimeLoader.LoadFromBytes(dat.bytes);
            if (fallbackData?.Frames != null && fallbackData.Frames.Count > 0)
                Debug.Log($"[Lf2ChampionshipManager] {charName} loaded: {fallbackData.Frames.Count} frames");
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
                case ChampPhase.BracketDisplay:
                    DrawBracketDisplay();
                    break;
                case ChampPhase.RoundIntro:
                    DrawCenteredText($"ROUND {_currentRound}", _roundStyle);
                    break;
                case ChampPhase.Fighting:
                    DrawMatchScoreBar();
                    break;
                case ChampPhase.RoundEnd:
                    DrawMatchScoreBar();
                    string winner = _playerHealth != null && _playerHealth.IsDead ? _opponentCharName : _playerCharName;
                    DrawCenteredText($"{winner} WINS THE ROUND", _winStyle);
                    break;
                case ChampPhase.MatchEnd:
                    DrawMatchVictory();
                    break;
                case ChampPhase.TournamentEnd:
                    DrawTournamentEnd();
                    break;
            }
        }

        private void DrawBracketDisplay()
        {
            float w = 500f;
            float h = 80f;
            float x = (Screen.width - w) * 0.5f;
            float y = (Screen.height - h) * 0.5f - 40f;

            GUI.Label(new Rect(x, y, w, 40f), "CHAMPIONSHIP MATCH", _titleStyle);
            GUI.Label(new Rect(x, y + 40f, w, 30f),
                $"Match {_currentOpponentIndex + 1}/{_opponentQueue.Count}: {_playerCharName} vs {_opponentCharName}",
                _bracketStyle);
        }

        private void DrawMatchScoreBar()
        {
            float barW = 300f;
            float x = (Screen.width - barW) * 0.5f;
            GUI.Label(new Rect(x, 6f, barW, 28f),
                $"{_playerCharName}  {_playerWins} - {_opponentWins}  {_opponentCharName}", _scoreStyle);
        }

        private void DrawMatchVictory()
        {
            DrawCenteredText($"{_playerCharName} WINS!", _victoryStyle);

            var subStyle = new GUIStyle(_scoreStyle) { fontSize = 16 };
            DrawCenteredTextOffset($"Match {_currentOpponentIndex + 1}/{_opponentQueue.Count} | Wins: {_tournamentWins}", subStyle, 50f);
            DrawCenteredTextOffset("Press ENTER for next match", subStyle, 80f);
        }

        private void DrawTournamentEnd()
        {
            bool wonAll = _tournamentWins >= _opponentQueue.Count;
            string title = wonAll ? "CHAMPION!" : "TOURNAMENT OVER";

            DrawCenteredText(title, _victoryStyle);

            var subStyle = new GUIStyle(_scoreStyle) { fontSize = 18 };
            DrawCenteredTextOffset($"{_playerCharName}: {_tournamentWins} wins", subStyle, 50f);
            DrawCenteredTextOffset("Press R to restart", subStyle, 80f);
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
            if (_roundStyle != null) return;

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 28,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.85f, 0.2f) }
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

            _bracketStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.9f, 0.9f, 0.9f) }
            };
        }
    }
}
