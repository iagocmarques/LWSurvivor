using Project.Core.Bootstrap;
using Project.Gameplay.Audio;
using UnityEngine;
using Project.Core.Input;

namespace Project.Gameplay.UI
{
    public sealed class Lf2MainMenu : MonoBehaviour
    {
        private enum MenuState
        {
            Main,
            Options,
            ModeSelect,
        }

        private enum GameMode
        {
            VS,
            Stage,
            Championship,
            Battle,
            Demo,
            Survival,
        }

        private static readonly string[] ModeLabels =
        {
            "VS Mode",
            "Stage Mode",
            "Championship",
            "Battle Mode",
            "Demo Mode",
            "Survival Mode",
        };

        private static readonly string[] ModeDescriptions =
        {
            "1v1 local battle with character select",
            "Fight through stages with enemy waves",
            "Tournament bracket against AI opponents",
            "Free-for-all arena combat",
            "Watch AI characters fight each other",
            "Survive endless waves as long as you can",
        };

        private static readonly string[] MainOptions =
        {
            "Game Start",
            "Options",
            "Exit",
        };

        private int _mainSelection;
        private int _modeSelection;
        private int _optionsSelection;
        private MenuState _state = MenuState.Main;

        private Texture2D _whiteTex;
        private Texture2D _logoBg;

        private GUIStyle _titleStyle;
        private GUIStyle _menuStyle;
        private GUIStyle _menuSelectedStyle;
        private GUIStyle _descStyle;
        private GUIStyle _headerStyle;
        private GUIStyle _controlsStyle;
        private GUIStyle _versionStyle;

        private float _animTimer;
        private int _titleFlashFrame;
        private Lf2AudioManager _audio;

        private const string VersionString = "v0.9 — Wave 12";

        private void Awake()
        {
            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();

            _logoBg = new Texture2D(1, 1);
            _logoBg.SetPixel(0, 0, new Color(0.05f, 0.05f, 0.12f, 0.92f));
            _logoBg.Apply();
        }

        private void Start()
        {
            EnsureAudioManager();
        }

        private void OnDestroy()
        {
            if (_whiteTex != null) Destroy(_whiteTex);
            if (_logoBg != null) Destroy(_logoBg);
        }

        private void EnsureAudioManager()
        {
            if (Lf2AudioManager.Instance != null)
            {
                _audio = Lf2AudioManager.Instance;
                return;
            }

            var go = new GameObject("[Lf2AudioManager]");
            go.AddComponent<Lf2AudioManager>();
            _audio = Lf2AudioManager.Instance;
        }

        private void Update()
        {
            _animTimer += Time.deltaTime;

            if (_animTimer > 0.5f)
            {
                _animTimer = 0f;
                _titleFlashFrame = (_titleFlashFrame + 1) % 4;
            }

            switch (_state)
            {
                case MenuState.Main:
                    UpdateMainMenu();
                    break;
                case MenuState.ModeSelect:
                    UpdateModeSelect();
                    break;
                case MenuState.Options:
                    UpdateOptions();
                    break;
            }
        }

        private void UpdateMainMenu()
            {
            if (Lf2Input.GetKeyDown(KeyCode.UpArrow))
            {
                _mainSelection = (_mainSelection - 1 + MainOptions.Length) % MainOptions.Length;
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCursor);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.DownArrow))
            {
                _mainSelection = (_mainSelection + 1) % MainOptions.Length;
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCursor);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.Return) || Lf2Input.GetKeyDown(KeyCode.J))
            {
                _audio?.PlayMenuSfx(Lf2SoundId.MenuOk);

                switch (_mainSelection)
                {
                    case 0:
                        _state = MenuState.ModeSelect;
                        _modeSelection = 0;
                        break;
                    case 1:
                        _state = MenuState.Options;
                        _optionsSelection = 0;
                        break;
                    case 2:
                        Application.Quit();
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#endif
                        break;
                }
            }
        }

        private void UpdateModeSelect()
        {
            if (Lf2Input.GetKeyDown(KeyCode.UpArrow))
            {
                _modeSelection = (_modeSelection - 1 + ModeLabels.Length) % ModeLabels.Length;
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCursor);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.DownArrow))
            {
                _modeSelection = (_modeSelection + 1) % ModeLabels.Length;
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCursor);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.Return) || Lf2Input.GetKeyDown(KeyCode.J))
            {
                _audio?.PlayMenuSfx(Lf2SoundId.MenuOk);
                LaunchMode((GameMode)_modeSelection);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.Escape) || Lf2Input.GetKeyDown(KeyCode.K))
            {
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCancel);
                _state = MenuState.Main;
            }
        }

        private void UpdateOptions()
        {
            const int optionCount = 3;

            if (Lf2Input.GetKeyDown(KeyCode.UpArrow))
            {
                _optionsSelection = (_optionsSelection - 1 + optionCount) % optionCount;
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCursor);
            }
            else if (Lf2Input.GetKeyDown(KeyCode.DownArrow))
            {
                _optionsSelection = (_optionsSelection + 1) % optionCount;
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCursor);
            }

            float step = 0.05f;
            if (Lf2Input.GetKeyDown(KeyCode.RightArrow))
                AdjustOption(_optionsSelection, step);
            else if (Lf2Input.GetKeyDown(KeyCode.LeftArrow))
                AdjustOption(_optionsSelection, -step);

            if (Lf2Input.GetKeyDown(KeyCode.Escape) || Lf2Input.GetKeyDown(KeyCode.K))
            {
                _audio?.PlayMenuSfx(Lf2SoundId.MenuCancel);
                _state = MenuState.Main;
            }
        }

        private void AdjustOption(int index, float delta)
        {
            if (_audio == null) return;

            switch (index)
            {
                case 0:
                    _audio.MasterVolume += delta;
                    break;
                case 1:
                    _audio.SfxVolume += delta;
                    break;
                case 2:
                    _audio.MusicVolume += delta;
                    break;
            }

            _audio.PlayMenuSfx(Lf2SoundId.MenuCursor);
        }

        private void LaunchMode(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.VS:
                    GameRuntimeInstaller.StartVsMode();
                    break;
                case GameMode.Stage:
                    GameRuntimeInstaller.StartStageMode();
                    break;
                case GameMode.Championship:
                    GameRuntimeInstaller.StartChampionshipMode();
                    break;
                case GameMode.Battle:
                    GameRuntimeInstaller.StartBattleMode();
                    break;
                case GameMode.Demo:
                    GameRuntimeInstaller.StartDemoMode();
                    break;
                case GameMode.Survival:
                    GameRuntimeInstaller.StartSurvivalMode();
                    break;
            }
        }

        private void OnGUI()
        {
            InitStyles();

            DrawBackground();

            switch (_state)
            {
                case MenuState.Main:
                    DrawMainMenu();
                    break;
                case MenuState.ModeSelect:
                    DrawModeSelect();
                    break;
                case MenuState.Options:
                    DrawOptions();
                    break;
            }

            DrawVersion();
        }

        private void DrawBackground()
        {
            GUI.color = new Color(0.06f, 0.06f, 0.14f, 1f);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _whiteTex);
            GUI.color = Color.white;
        }

        private void DrawMainMenu()
        {
            float centerX = Screen.width * 0.5f;
            float startY = Screen.height * 0.25f;

            DrawTitle(centerX, startY);
            startY += 100f;

            GUI.DrawTexture(new Rect(centerX - 180f, startY - 15f, 360f, MainOptions.Length * 48f + 30f), _logoBg);

            for (int i = 0; i < MainOptions.Length; i++)
            {
                bool selected = i == _mainSelection;
                var style = selected ? _menuSelectedStyle : _menuStyle;
                float y = startY + i * 48f;

                if (selected)
                {
                    GUI.color = new Color(0.15f, 0.15f, 0.3f, 0.6f);
                    GUI.DrawTexture(new Rect(centerX - 160f, y - 4f, 320f, 36f), _whiteTex);
                    GUI.color = Color.white;

                    GUI.Label(new Rect(centerX - 170f, y, 30f, 32f), "▶", _menuSelectedStyle);
                }

                GUI.Label(new Rect(centerX - 140f, y, 280f, 32f), MainOptions[i], style);
            }

            GUI.Label(new Rect(centerX - 140f, startY + MainOptions.Length * 48f + 20f, 280f, 24f),
                "↑↓ Navigate   Enter/J Select", _controlsStyle);
        }

        private void DrawModeSelect()
        {
            float centerX = Screen.width * 0.5f;
            float startY = Screen.height * 0.15f;

            GUI.Label(new Rect(centerX - 200f, startY, 400f, 40f), "SELECT MODE", _headerStyle);
            startY += 55f;

            GUI.DrawTexture(new Rect(centerX - 200f, startY - 10f, 400f, ModeLabels.Length * 42f + 20f), _logoBg);

            for (int i = 0; i < ModeLabels.Length; i++)
            {
                bool selected = i == _modeSelection;
                var style = selected ? _menuSelectedStyle : _menuStyle;
                float y = startY + i * 42f;

                if (selected)
                {
                    GUI.color = new Color(0.15f, 0.15f, 0.3f, 0.6f);
                    GUI.DrawTexture(new Rect(centerX - 180f, y - 3f, 360f, 34f), _whiteTex);
                    GUI.color = Color.white;

                    GUI.Label(new Rect(centerX - 190f, y, 30f, 30f), "▶", _menuSelectedStyle);
                }

                GUI.Label(new Rect(centerX - 160f, y, 320f, 30f), ModeLabels[i], style);
            }

            float descY = startY + ModeLabels.Length * 42f + 20f;
            if (_modeSelection >= 0 && _modeSelection < ModeDescriptions.Length)
            {
                GUI.Label(new Rect(centerX - 200f, descY, 400f, 30f),
                    ModeDescriptions[_modeSelection], _descStyle);
            }

            GUI.Label(new Rect(centerX - 200f, descY + 35f, 400f, 24f),
                "↑↓ Navigate   Enter/J Play   Esc/K Back", _controlsStyle);
        }

        private void DrawOptions()
        {
            float centerX = Screen.width * 0.5f;
            float startY = Screen.height * 0.2f;

            GUI.Label(new Rect(centerX - 200f, startY, 400f, 40f), "OPTIONS", _headerStyle);
            startY += 60f;

            GUI.DrawTexture(new Rect(centerX - 200f, startY - 10f, 400f, 180f), _logoBg);

            if (_audio != null)
            {
                DrawVolumeSlider(centerX, startY, "Master Volume", _audio.MasterVolume, _optionsSelection == 0);
                DrawVolumeSlider(centerX, startY + 50f, "SFX Volume", _audio.SfxVolume, _optionsSelection == 1);
                DrawVolumeSlider(centerX, startY + 100f, "Music Volume", _audio.MusicVolume, _optionsSelection == 2);
            }
            else
            {
                GUI.Label(new Rect(centerX - 140f, startY + 40f, 280f, 30f),
                    "Audio Manager not found", _descStyle);
            }

            GUI.Label(new Rect(centerX - 200f, startY + 155f, 400f, 24f),
                "←→ Adjust   ↑↓ Navigate   Esc/K Back", _controlsStyle);
        }

        private void DrawVolumeSlider(float centerX, float y, string label, float value, bool selected)
        {
            float sliderX = centerX - 140f;
            float sliderW = 280f;
            float barH = 14f;

            var style = selected ? _menuSelectedStyle : _menuStyle;
            GUI.Label(new Rect(sliderX, y, 150f, 28f), label, style);

            float barY = y + 30f;
            GUI.color = new Color(0.2f, 0.2f, 0.3f);
            GUI.DrawTexture(new Rect(sliderX + 100f, barY, sliderW - 100f, barH), _whiteTex);

            GUI.color = selected ? new Color(0.9f, 0.7f, 0.2f) : new Color(0.5f, 0.5f, 0.7f);
            GUI.DrawTexture(new Rect(sliderX + 100f, barY, (sliderW - 100f) * Mathf.Clamp01(value), barH), _whiteTex);

            GUI.color = Color.white;
        }

        private void DrawTitle(float centerX, float y)
        {
            var titleColor = _titleFlashFrame < 2
                ? new Color(1f, 0.85f, 0.2f)
                : new Color(1f, 0.95f, 0.5f);

            var prev = _titleStyle.normal.textColor;
            _titleStyle.normal.textColor = titleColor;
            GUI.Label(new Rect(centerX - 250f, y, 500f, 60f), "LF2 + SURVIVORS", _titleStyle);
            _titleStyle.normal.textColor = prev;

            GUI.Label(new Rect(centerX - 150f, y + 55f, 300f, 30f),
                "Beat 'em up × Vampire Survivors", _descStyle);
        }

        private void DrawVersion()
        {
            GUI.Label(new Rect(Screen.width - 120f, Screen.height - 28f, 110f, 20f), VersionString, _versionStyle);
        }

        private void InitStyles()
        {
            if (_titleStyle != null) return;

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 42,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.85f, 0.2f) },
            };

            _menuStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                alignment = TextAnchor.MiddleLeft,
                normal = { textColor = new Color(0.7f, 0.7f, 0.8f) },
            };

            _menuSelectedStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft,
                normal = { textColor = new Color(1f, 0.9f, 0.3f) },
            };

            _descStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.6f, 0.6f, 0.7f) },
            };

            _headerStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 30,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.9f, 0.3f) },
            };

            _controlsStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.5f, 0.5f, 0.6f) },
            };

            _versionStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 10,
                alignment = TextAnchor.MiddleRight,
                normal = { textColor = new Color(0.4f, 0.4f, 0.5f) },
            };
        }
    }
}
