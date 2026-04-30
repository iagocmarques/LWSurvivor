using System.Collections.Generic;
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
    public sealed class Lf2DemoMode : MonoBehaviour
    {
        private enum DemoPhase
        {
            Loading,
            Intro,
            Fighting,
            RoundEnd,
            NextMatch
        }

        private const float IntroDuration = 2f;
        private const float RoundEndDuration = 2.5f;
        private const float NextMatchDelay = 3f;
        private const int MaxRoundsPerMatch = 3;

        [SerializeField] private Lf2CharacterDatabase _database;
        [SerializeField] private MatchHud _matchHud;

        private DemoPhase _phase;
        private float _phaseTimer;
        private int _currentRound;
        private int _p1Wins;
        private int _p2Wins;

        private int _char1Index;
        private int _char2Index;

        private GameObject _player1;
        private GameObject _player2;
        private Health _p1Health;
        private Health _p2Health;
        private PlayerHsmController _p1Hsm;
        private PlayerHsmController _p2Hsm;

        private Lf2CharacterData _p1Lf2Data;
        private Lf2CharacterData _p2Lf2Data;
        private Dictionary<int, Lf2CharacterData> _p1ProjectileMap;
        private Dictionary<int, Lf2CharacterData> _p2ProjectileMap;
        private string _p1CharName;
        private string _p2CharName;

        private Lf2DemoAI _p1AI;
        private Lf2DemoAI _p2AI;

        private Texture2D _whiteTex;
        private GUIStyle _titleStyle;
        private GUIStyle _infoStyle;
        private GUIStyle _roundStyle;
        private GUIStyle _scoreStyle;

        private static readonly string[] CharacterNames =
        {
            "Davis", "Deep", "John", "Henry", "Rudolf", "Louis", "Firen"
        };

        private static readonly string[] CharacterPaths =
        {
            "LF2/davis", "LF2/deep", "LF2/john", "LF2/henry",
            "LF2/rudolf", "LF2/louis", "LF2/firen"
        };

        private void Awake()
        {
            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();

            _phase = DemoPhase.Loading;
            PickRandomMatchup();
        }

        private void OnDestroy()
        {
            if (_whiteTex != null) Destroy(_whiteTex);
            CleanupPlayers();
        }

        private void Update()
        {
            CombatReadabilityFx.Tick(Time.deltaTime);

            if (Input.anyKeyDown && _phase != DemoPhase.Loading)
            {
                ExitDemo();
                return;
            }

            switch (_phase)
            {
                case DemoPhase.Loading:
                    UpdateLoading();
                    break;
                case DemoPhase.Intro:
                    UpdateIntro();
                    break;
                case DemoPhase.Fighting:
                    UpdateFighting();
                    break;
                case DemoPhase.RoundEnd:
                    UpdateRoundEnd();
                    break;
                case DemoPhase.NextMatch:
                    UpdateNextMatch();
                    break;
            }
        }

        private void UpdateLoading()
        {
            SetupMatch();
            _phase = DemoPhase.Intro;
            _phaseTimer = IntroDuration;
        }

        private void UpdateIntro()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                EnableAI(true);
                _phase = DemoPhase.Fighting;
            }
        }

        private void UpdateFighting()
        {
            if (_p1Health == null || _p2Health == null) return;

            if (_p1AI != null) _p1AI.Tick(Time.deltaTime);
            if (_p2AI != null) _p2AI.Tick(Time.deltaTime);

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
                if (_p1Wins >= 2 || _p2Wins >= 2 || _currentRound >= MaxRoundsPerMatch)
                {
                    _phase = DemoPhase.NextMatch;
                    _phaseTimer = NextMatchDelay;
                }
                else
                {
                    ResetRound();
                    StartRound();
                }
            }
        }

        private void UpdateNextMatch()
        {
            _phaseTimer -= Time.deltaTime;
            if (_phaseTimer <= 0f)
            {
                CleanupPlayers();
                PickRandomMatchup();
                _phase = DemoPhase.Loading;
            }
        }

        private void PickRandomMatchup()
        {
            _char1Index = Random.Range(0, CharacterNames.Length);
            _char2Index = Random.Range(0, CharacterNames.Length);
            while (_char2Index == _char1Index && CharacterNames.Length > 1)
                _char2Index = Random.Range(0, CharacterNames.Length);

            _p1CharName = CharacterNames[_char1Index];
            _p2CharName = CharacterNames[_char2Index];
            _p1Wins = 0;
            _p2Wins = 0;
            _currentRound = 0;
        }

        private void SetupMatch()
        {
            CleanupPlayers();

            _p1Lf2Data = LoadCharacterData(CharacterPaths[_char1Index], _p1CharName);
            _p2Lf2Data = LoadCharacterData(CharacterPaths[_char2Index], _p2CharName);
            _p1ProjectileMap = LoadProjectileMap(_p1CharName);
            _p2ProjectileMap = LoadProjectileMap(_p2CharName);

            if (_p1Lf2Data == null) _p1Lf2Data = LoadFallbackData();
            if (_p2Lf2Data == null) _p2Lf2Data = LoadFallbackData();

            _player1 = CreateAIPlayer("AI_P1", 0, _p1Lf2Data, _p1ProjectileMap, _p1CharName);
            _player2 = CreateAIPlayer("AI_P2", 1, _p2Lf2Data, _p2ProjectileMap, _p2CharName);

            _p1Health = _player1.GetComponent<Health>();
            _p2Health = _player2.GetComponent<Health>();
            _p1Hsm = _player1.GetComponent<PlayerHsmController>();
            _p2Hsm = _player2.GetComponent<PlayerHsmController>();

            if (_matchHud != null)
            {
                _matchHud.BindPlayer(0, _p1Health, _player1.GetComponent<Mana>(), _p1CharName);
                _matchHud.BindPlayer(1, _p2Health, _player2.GetComponent<Mana>(), _p2CharName);
                _matchHud.SetPhase("DEMO MODE");
            }

            _p1AI = new Lf2DemoAI();
            _p1AI.Initialize(_player1.transform, _player2.transform, _p1Hsm);

            _p2AI = new Lf2DemoAI();
            _p2AI.Initialize(_player2.transform, _player1.transform, _p2Hsm);

            PositionPlayers();
            EnableAI(false);

            var cam = Camera.main;
            if (cam != null)
            {
                cam.transform.position = new Vector3(0f, 0f, cam.transform.position.z);
                if (cam.GetComponent<CameraFollow2D>() == null)
                    cam.gameObject.AddComponent<CameraFollow2D>();
            }
        }

        private void StartRound()
        {
            _currentRound++;
            ResetPlayersHealth();
            PositionPlayers();

            if (_player1 != null) _player1.SetActive(true);
            if (_player2 != null) _player2.SetActive(true);

            if (_matchHud != null) _matchHud.SetRound(_currentRound);

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
                audio.PlayStageMusic(0);

            EnableAI(false);
            _phase = DemoPhase.Intro;
            _phaseTimer = IntroDuration;
        }

        private void EndRound()
        {
            _phase = DemoPhase.RoundEnd;
            _phaseTimer = RoundEndDuration;

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
                audio.StopMusic();

            EnableAI(false);
        }

        private void ResetRound()
        {
            EnableAI(false);
        }

        private void EnableAI(bool enabled)
        {
            if (_p1AI != null) _p1AI.SetActive(enabled);
            if (_p2AI != null) _p2AI.SetActive(enabled);
        }

        private void ResetPlayersHealth()
        {
            if (_p1Health != null) _p1Health.ResetToFull();
            if (_p2Health != null) _p2Health.ResetToFull();

            if (_player1 != null)
            {
                var mana = _player1.GetComponent<Mana>();
                if (mana != null) mana.ResetToFull();
            }
            if (_player2 != null)
            {
                var mana = _player2.GetComponent<Mana>();
                if (mana != null) mana.ResetToFull();
            }
        }

        private void PositionPlayers()
        {
            if (_player1 != null) _player1.transform.position = new Vector3(-3f, 0f, 0f);
            if (_player2 != null) _player2.transform.position = new Vector3(3f, 0f, 0f);
        }

        private void CleanupPlayers()
        {
            _p1AI = null;
            _p2AI = null;

            if (_player1 != null) Destroy(_player1);
            if (_player2 != null) Destroy(_player2);
            _player1 = null;
            _player2 = null;
            _p1Health = null;
            _p2Health = null;
            _p1Hsm = null;
            _p2Hsm = null;
        }

        private void ExitDemo()
        {
            CleanupPlayers();
            Destroy(gameObject);
        }

        private GameObject CreateAIPlayer(string playerName, int playerIndex,
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

            var definition = CreateCharacterDefinition(playerIndex, charName, lf2Data);

            var inputScheme = new Lf2InputScheme(0);
            var hsm = go.AddComponent<PlayerHsmController>();
            hsm.SetInputScheme(inputScheme);
            hsm.Configure(definition, lf2Data, projectileMap);

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
            if (dat == null) return null;

            return Lf2DatRuntimeLoader.LoadFromBytes(dat.bytes);
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
                case DemoPhase.Intro:
                    GUI.Label(new Rect(0f, 20f, Screen.width, 40f), "DEMO MODE", _titleStyle);
                    DrawCenteredText($"{_p1CharName} vs {_p2CharName}", _infoStyle);
                    break;
                case DemoPhase.Fighting:
                    GUI.Label(new Rect(0f, 6f, Screen.width, 28f),
                        $"DEMO  {_p1CharName} {_p1Wins} - {_p2Wins} {_p2CharName}", _scoreStyle);
                    break;
                case DemoPhase.RoundEnd:
                    GUI.Label(new Rect(0f, 6f, Screen.width, 28f),
                        $"DEMO  {_p1CharName} {_p1Wins} - {_p2Wins} {_p2CharName}", _scoreStyle);
                    break;
                case DemoPhase.NextMatch:
                    DrawCenteredText("NEXT MATCH...", _infoStyle);
                    break;
            }

            var hintStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                alignment = TextAnchor.LowerCenter,
                normal = { textColor = new Color(0.6f, 0.6f, 0.6f) }
            };
            GUI.Label(new Rect(0f, Screen.height - 24f, Screen.width, 24f), "Press any key to exit", hintStyle);
        }

        private void DrawCenteredText(string text, GUIStyle style)
        {
            float w = 500f;
            float h = 50f;
            GUI.Label(new Rect((Screen.width - w) * 0.5f, (Screen.height - h) * 0.5f, w, h), text, style);
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

            _infoStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 22,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _roundStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 36,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _scoreStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };
        }
    }

    internal sealed class Lf2DemoAI
    {
        private Transform _owner;
        private Transform _target;
        private PlayerHsmController _hsm;
        private Lf2StateMachine _sm;

        private float _thinkTimer;
        private float _thinkInterval = 0.15f;
        private float _attackCooldown;
        private bool _active;
        private int _comboStep;
        private int[] _comboChain = { 60, 61, 62 };

        public void Initialize(Transform owner, Transform target, PlayerHsmController hsm)
        {
            _owner = owner;
            _target = target;
            _hsm = hsm;
            _sm = hsm != null ? hsm.Lf2Sm : null;
            _thinkTimer = 0f;
            _attackCooldown = 0f;
            _active = false;
            _comboStep = 0;
        }

        public void SetActive(bool active)
        {
            _active = active;
        }

        public void Tick(float deltaTime)
        {
            if (!_active || _owner == null || _target == null || _sm == null)
                return;

            _thinkTimer -= deltaTime;
            _attackCooldown -= deltaTime;

            if (_thinkTimer > 0f) return;
            _thinkTimer = _thinkInterval + Random.Range(-0.05f, 0.05f);

            var toTarget = _target.position - _owner.position;
            var dist = toTarget.magnitude;

            if (dist > 0.1f)
            {
                if (dist < 1.5f && _attackCooldown <= 0f)
                {
                    int frameId = _comboStep < _comboChain.Length ? _comboChain[_comboStep] : _comboChain[0];
                    _sm.SetFrame(frameId);
                    _comboStep++;
                    if (_comboStep >= _comboChain.Length) _comboStep = 0;
                    _attackCooldown = 0.4f + Random.Range(0f, 0.3f);
                }
                else
                {
                    _comboStep = 0;
                    if (dist > 2f)
                    {
                        _sm.SetFrame(1);
                    }
                    else if (Random.value < 0.15f)
                    {
                        _sm.SetFrame(100);
                    }
                }
            }
        }
    }
}
