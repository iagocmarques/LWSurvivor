using System.Collections.Generic;
using Project.Data;
using Project.Gameplay.Audio;
using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using Project.Gameplay.Rendering;
using Project.Gameplay.Visual;
using Project.UI.HUD;
using Project.UI.Screens;
using UnityEngine;
using Project.Core.Input;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2VsManager : MonoBehaviour
    {
        private enum VsPhase
        {
            CharacterSelect,
            RoundIntro,
            Fighting,
            RoundEnd,
            MatchEnd
        }

        [Header("Config")]
        [SerializeField] private int winsToVictory = 2;
        [SerializeField] private float roundIntroDuration = 1.5f;
        [SerializeField] private float roundEndDuration = 2f;
        [SerializeField] private float spawnSpacing = 3f;

        [Header("References")]
        [SerializeField] private CharacterSelectScreen characterSelectScreen;
        [SerializeField] private Lf2CharacterDatabase characterDatabase;
        [SerializeField] private MatchHud matchHud;

        private VsPhase _phase;
        private int _p1Wins;
        private int _p2Wins;
        private int _currentRound;
        private float _phaseTimer;

        private GameObject _player1;
        private GameObject _player2;
        private Health _p1Health;
        private Health _p2Health;
        private Mana _p1Mana;
        private Mana _p2Mana;
        private PlayerHsmController _p1Hsm;
        private PlayerHsmController _p2Hsm;

        private string _p1CharName;
        private string _p2CharName;
        private int _confirmedP1Id = -1;
        private int _confirmedP2Id = -1;

        private Texture2D _whiteTex;
        private GUIStyle _roundStyle;
        private GUIStyle _winStyle;
        private GUIStyle _scoreStyle;
        private GUIStyle _victoryStyle;

        private void Awake()
        {
            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();

            if (characterSelectScreen == null)
                characterSelectScreen = GetComponentInChildren<CharacterSelectScreen>();
            if (characterDatabase == null)
                characterDatabase = Resources.Load<Lf2CharacterDatabase>("LF2CharacterDatabase");
            if (matchHud == null)
                matchHud = GetComponentInChildren<MatchHud>();

            if (characterSelectScreen != null)
            {
                if (characterDatabase != null)
                    characterSelectScreen.Initialize(characterDatabase);
                characterSelectScreen.OnConfirmed += OnCharacterConfirmed;
            }

            _phase = VsPhase.CharacterSelect;
        }

        private void OnDestroy()
        {
            if (characterSelectScreen != null)
                characterSelectScreen.OnConfirmed -= OnCharacterConfirmed;
            if (_whiteTex != null) Destroy(_whiteTex);
            CleanupPlayers();
        }

        private void OnCharacterConfirmed(int p1Id, int p2Id)
        {
            _confirmedP1Id = p1Id;
            _confirmedP2Id = p2Id;
        }

        private void Update()
        {
            CombatReadabilityFx.Tick(Time.deltaTime);

            switch (_phase)
            {
                case VsPhase.CharacterSelect:
                    UpdateCharacterSelect();
                    break;
                case VsPhase.RoundIntro:
                    UpdateRoundIntro();
                    break;
                case VsPhase.Fighting:
                    UpdateFighting();
                    break;
                case VsPhase.RoundEnd:
                    UpdateRoundEnd();
                    break;
                case VsPhase.MatchEnd:
                    UpdateMatchEnd();
                    break;
            }
        }

        private void UpdateCharacterSelect()
        {
            if (characterSelectScreen == null || !characterSelectScreen.BothConfirmed) return;
            if (_confirmedP1Id < 0 || _confirmedP2Id < 0) return;

            int p1Id = _confirmedP1Id;
            int p2Id = _confirmedP2Id;

            var p1Data = characterDatabase.GetCharacter(p1Id);
            var p2Data = characterDatabase.GetCharacter(p2Id);

            _p1CharName = p1Data?.Name ?? $"P1_{p1Id}";
            _p2CharName = p2Data?.Name ?? $"P2_{p2Id}";

            var p1Def = BuildDefinition(p1Id, p1Data);
            var p2Def = BuildDefinition(p2Id, p2Data);

            var p1Projectiles = LoadProjectileMap(_p1CharName);
            var p2Projectiles = LoadProjectileMap(_p2CharName);

            SetupPlayers(p1Def, p1Data, p1Projectiles, p2Def, p2Data, p2Projectiles);
            StartRound();
        }

        private CharacterDefinition BuildDefinition(int id, Lf2CharacterData charData)
        {
            if (charData == null) return null;

            var def = ScriptableObject.CreateInstance<CharacterDefinition>();
            def.lf2Id = id;
            def.displayName = charData.Name ?? $"Character_{id}";
            def.movement = CharacterMovementConfig.FromLf2Movement(charData.Movement);
            def.rawDatBytes = characterDatabase.GetDatBytes(id);
            def.frameRoleIds = charData.RoleIds ?? Lf2FrameRoleIds.BuildFromCharacterData(charData);
            return def;
        }

        private void UpdateRoundIntro()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                _phase = VsPhase.Fighting;
                if (matchHud != null) matchHud.SetPhase(string.Empty);
            }
        }

        private void UpdateFighting()
        {
            if (_p1Health == null || _p2Health == null) return;

            if (_p1Health.IsDead)
            {
                _p2Wins++;
                EndRound();
            }
            else if (_p2Health.IsDead)
            {
                _p1Wins++;
                EndRound();
            }
        }

        private void UpdateRoundEnd()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                if (_p1Wins >= winsToVictory || _p2Wins >= winsToVictory)
                {
                    _phase = VsPhase.MatchEnd;
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
                _confirmedP1Id = -1;
                _confirmedP2Id = -1;
                if (characterSelectScreen != null) characterSelectScreen.ResetSelection();
                if (characterSelectScreen != null && characterDatabase != null)
                    characterSelectScreen.Initialize(characterDatabase);
                _p1Wins = 0;
                _p2Wins = 0;
                _currentRound = 0;
                _phase = VsPhase.CharacterSelect;
            }
        }

        private void StartRound()
        {
            _currentRound++;
            ResetPlayersHealth();
            PositionPlayers();

            if (_player1 != null) _player1.SetActive(true);
            if (_player2 != null) _player2.SetActive(true);

            if (matchHud != null)
            {
                matchHud.SetRound(_currentRound);
                matchHud.SetPhase($"ROUND {_currentRound}");
            }

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                audio.PlaySfx(Lf2SoundId.RoundStart);
                audio.PlayStageMusic(0);
            }

            _phase = VsPhase.RoundIntro;
            _phaseTimer = roundIntroDuration;
        }

        private void EndRound()
        {
            _phase = VsPhase.RoundEnd;
            _phaseTimer = roundEndDuration;

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                audio.StopMusic();
                audio.PlaySfx(Lf2SoundId.RoundEnd);
            }

            if (_p1Hsm != null) _p1Hsm.enabled = false;
            if (_p2Hsm != null) _p2Hsm.enabled = false;

            string winner = _p1Health != null && _p1Health.IsDead ? _p2CharName : _p1CharName;
            if (matchHud != null)
                matchHud.SetPhase($"{winner} WINS THE ROUND");
        }

        private void ResetRound()
        {
            if (_p1Hsm != null) _p1Hsm.enabled = true;
            if (_p2Hsm != null) _p2Hsm.enabled = true;
        }

        private void ResetPlayersHealth()
        {
            if (_p1Health != null) _p1Health.ResetToFull();
            if (_p2Health != null) _p2Health.ResetToFull();
            if (_p1Mana != null) _p1Mana.ResetToFull();
            if (_p2Mana != null) _p2Mana.ResetToFull();
        }

        private void PositionPlayers()
        {
            if (_player1 != null) _player1.transform.position = new Vector3(-spawnSpacing, 0f, 0f);
            if (_player2 != null) _player2.transform.position = new Vector3(spawnSpacing, 0f, 0f);
        }

        private void SetupPlayers(
            CharacterDefinition p1Def, Lf2CharacterData p1Data, Dictionary<int, Lf2CharacterData> p1Projectiles,
            CharacterDefinition p2Def, Lf2CharacterData p2Data, Dictionary<int, Lf2CharacterData> p2Projectiles)
        {
            CleanupPlayers();

            _player1 = CreatePlayer("P1", 0, p1Def, p1Data, p1Projectiles, _p1CharName);
            _player2 = CreatePlayer("P2", 1, p2Def, p2Data, p2Projectiles, _p2CharName);

            _p1Health = _player1.GetComponent<Health>();
            _p2Health = _player2.GetComponent<Health>();
            _p1Mana = _player1.GetComponent<Mana>();
            _p2Mana = _player2.GetComponent<Mana>();
            _p1Hsm = _player1.GetComponent<PlayerHsmController>();
            _p2Hsm = _player2.GetComponent<PlayerHsmController>();

            if (matchHud != null)
            {
                matchHud.BindPlayer(0, _p1Health, _p1Mana, _p1CharName);
                matchHud.BindPlayer(1, _p2Health, _p2Mana, _p2CharName);
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

        private GameObject CreatePlayer(string playerName, int playerIndex,
            CharacterDefinition definition, Lf2CharacterData charData,
            Dictionary<int, Lf2CharacterData> projectileMap, string charName)
        {
            var go = new GameObject(playerName);
            go.transform.position = playerIndex == 0
                ? new Vector3(-spawnSpacing, 0f, 0f)
                : new Vector3(spawnSpacing, 0f, 0f);

            int playerLayer = LayerMask.NameToLayer("Player");
            if (playerLayer >= 0) go.layer = playerLayer;

            var sr = go.AddComponent<SpriteRenderer>();
            Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
            sr.sprite = CreatePlaceholderSprite(playerIndex == 0
                ? new Color(1f, 0.95f, 0.35f)
                : new Color(0.35f, 0.6f, 1f));
            sr.sortingOrder = 10 + playerIndex;

            var health = go.AddComponent<Health>();
            health.ConfigureMaxHealth(100, true);

            go.AddComponent<Mana>();

            var motor = go.AddComponent<CharacterMotor>();

            go.AddComponent<Damageable>();

            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(0.8f, 1.6f);

            go.AddComponent<DepthSortByY>();
            go.AddComponent<Lf2PlayerSpriteAnimator>();

            var hsm = go.AddComponent<PlayerHsmController>();
            var inputScheme = new Lf2InputScheme(playerIndex);
            hsm.SetInputScheme(inputScheme);
            hsm.Configure(definition, charData, projectileMap);

            var charLower = (charName ?? "davis").ToLowerInvariant();
            if (playerIndex == 0)
                Lf2VisualLibrary.SetPlayerCharacter(charLower);

            return go;
        }

        private void CleanupPlayers()
        {
            if (_player1 != null) Destroy(_player1);
            if (_player2 != null) Destroy(_player2);
            _player1 = null;
            _player2 = null;
            _p1Health = null;
            _p2Health = null;
            _p1Mana = null;
            _p2Mana = null;
            _p1Hsm = null;
            _p2Hsm = null;
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
                case VsPhase.RoundIntro:
                    DrawCenteredText($"ROUND {_currentRound}", _roundStyle);
                    break;
                case VsPhase.Fighting:
                    DrawScoreBar();
                    break;
                case VsPhase.RoundEnd:
                    DrawScoreBar();
                    string winner = _p1Health != null && _p1Health.IsDead ? _p2CharName : _p1CharName;
                    DrawCenteredText($"{winner} WINS THE ROUND", _winStyle);
                    break;
                case VsPhase.MatchEnd:
                    DrawMatchEnd();
                    break;
            }
        }

        private void DrawScoreBar()
        {
            float barW = 300f;
            float x = (Screen.width - barW) * 0.5f;
            GUI.Label(new Rect(x, 6f, barW, 28f),
                $"{_p1CharName}  {_p1Wins} - {_p2Wins}  {_p2CharName}", _scoreStyle);
        }

        private void DrawMatchEnd()
        {
            string winner = _p1Wins >= winsToVictory ? _p1CharName : _p2CharName;
            DrawCenteredText($"{winner} WINS!", _victoryStyle);

            var subStyle = new GUIStyle(_scoreStyle) { fontSize = 16 };
            DrawCenteredTextOffset($"{_p1Wins} - {_p2Wins}", subStyle, 50f);
            DrawCenteredTextOffset("Press R to rematch", subStyle, 80f);
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
