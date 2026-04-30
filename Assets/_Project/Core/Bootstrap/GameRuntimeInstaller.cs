using System.Collections.Generic;
using Project.Data;
using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
using Project.Gameplay.Player;
using Project.Gameplay.Rendering;
using Project.Gameplay.Visual;
using Project.UI.Debug;
using UnityEngine;
using Project.Core.Input;

namespace Project.Core.Bootstrap
{
    public sealed class GameRuntimeInstaller : MonoBehaviour
    {
        public enum GameMode { Run, Vs, Stage, Championship, Battle, Demo, Survival }

#pragma warning disable 0414
        private static GameMode _currentMode = GameMode.Run;
#pragma warning restore 0414
        private static bool _vsModeRequested;
        private static bool _stageModeRequested;
        private static int _stageModeCharacterId;
        private static int _stageModeDifficulty;
        private static bool _championshipModeRequested;
        private static bool _battleModeRequested;
        private static bool _demoModeRequested;
        private static bool _survivalModeRequested;

        private static CharacterDefinition _davis;
        private static DefinitionRegistry _registry;
        private static Lf2CharacterData _davisLf2Data;
        private static Dictionary<int, Lf2CharacterData> _davisProjectileMap;

        private static Lf2CharacterData _deepLf2Data;
        private static Dictionary<int, Lf2CharacterData> _deepProjectileMap;

        private static Lf2CharacterData _johnLf2Data;
        private static Dictionary<int, Lf2CharacterData> _johnProjectileMap;

        private static int _selectedCharacterIndex;
        private static Lf2CharacterData _activeLf2Data;
        private static Dictionary<int, Lf2CharacterData> _activeProjectileMap;
        private static CharacterDefinition _activeCharacterDef;
        private static string _activeCharacterName;

        public static CharacterDefinition Davis => _davis;
        public static DefinitionRegistry Registry => _registry;
        public static int SelectedCharacterIndex => _selectedCharacterIndex;
        public static string ActiveCharacterName => _activeCharacterName;
        public static int CharacterCount => 3;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name != "Game")
                return;

            if (FindAnyObjectByType<GameRuntimeInstaller>() != null)
                return;

            var go = new GameObject("GameRuntimeInstaller");
            DontDestroyOnLoad(go);
            var installer = go.AddComponent<GameRuntimeInstaller>();

            if (_stageModeRequested)
            {
                _stageModeRequested = false;
                installer.InstallStageMode(_stageModeCharacterId, _stageModeDifficulty);
            }
            else if (_vsModeRequested)
            {
                _vsModeRequested = false;
                installer.InstallVsMode();
            }
            else if (_championshipModeRequested)
            {
                _championshipModeRequested = false;
                installer.InstallChampionshipMode();
            }
            else if (_battleModeRequested)
            {
                _battleModeRequested = false;
                installer.InstallBattleMode();
            }
            else if (_demoModeRequested)
            {
                _demoModeRequested = false;
                installer.InstallDemoMode();
            }
            else if (_survivalModeRequested)
            {
                _survivalModeRequested = false;
                installer.InstallSurvivalMode();
            }
            else
            {
                installer.Install();
            }
        }

        public static void StartVsMode()
        {
            _vsModeRequested = true;
            _currentMode = GameMode.Vs;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                var existing = FindAnyObjectByType<GameRuntimeInstaller>();
                if (existing != null)
                {
                    Destroy(existing.gameObject);
                }

                var go = new GameObject("GameRuntimeInstaller");
                DontDestroyOnLoad(go);
                go.AddComponent<GameRuntimeInstaller>().InstallVsMode();
                _vsModeRequested = false;
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public static void StartStageMode(int characterId = 0, int difficulty = 1)
        {
            _stageModeRequested = true;
            _stageModeCharacterId = characterId;
            _stageModeDifficulty = Mathf.Clamp(difficulty, 0, 3);
            _currentMode = GameMode.Stage;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                var existing = FindAnyObjectByType<GameRuntimeInstaller>();
                if (existing != null)
                    Destroy(existing.gameObject);

                var go = new GameObject("GameRuntimeInstaller");
                DontDestroyOnLoad(go);
                go.AddComponent<GameRuntimeInstaller>().InstallStageMode(_stageModeCharacterId, _stageModeDifficulty);
                _stageModeRequested = false;
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public static void StartChampionshipMode()
        {
            _championshipModeRequested = true;
            _currentMode = GameMode.Championship;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                var existing = FindAnyObjectByType<GameRuntimeInstaller>();
                if (existing != null)
                    Destroy(existing.gameObject);

                var go = new GameObject("GameRuntimeInstaller");
                DontDestroyOnLoad(go);
                go.AddComponent<GameRuntimeInstaller>().InstallChampionshipMode();
                _championshipModeRequested = false;
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public static void StartBattleMode()
        {
            _battleModeRequested = true;
            _currentMode = GameMode.Battle;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                var existing = FindAnyObjectByType<GameRuntimeInstaller>();
                if (existing != null)
                    Destroy(existing.gameObject);

                var go = new GameObject("GameRuntimeInstaller");
                DontDestroyOnLoad(go);
                go.AddComponent<GameRuntimeInstaller>().InstallBattleMode();
                _battleModeRequested = false;
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public static void StartDemoMode()
        {
            _demoModeRequested = true;
            _currentMode = GameMode.Demo;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                var existing = FindAnyObjectByType<GameRuntimeInstaller>();
                if (existing != null)
                    Destroy(existing.gameObject);

                var go = new GameObject("GameRuntimeInstaller");
                DontDestroyOnLoad(go);
                go.AddComponent<GameRuntimeInstaller>().InstallDemoMode();
                _demoModeRequested = false;
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        public static void StartSurvivalMode()
        {
            _survivalModeRequested = true;
            _currentMode = GameMode.Survival;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name == "Game")
            {
                var existing = FindAnyObjectByType<GameRuntimeInstaller>();
                if (existing != null)
                    Destroy(existing.gameObject);

                var go = new GameObject("GameRuntimeInstaller");
                DontDestroyOnLoad(go);
                go.AddComponent<GameRuntimeInstaller>().InstallSurvivalMode();
                _survivalModeRequested = false;
                return;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

        private void InstallVsMode()
        {
            LoadAllCharacterData();

            if (FindAnyObjectByType<Lf2VsManager>() != null)
                return;

            var oldPlayer = GameObject.Find("Player");
            if (oldPlayer != null) Destroy(oldPlayer);

            var vsGo = new GameObject("VsManager");
            DontDestroyOnLoad(vsGo);
            vsGo.AddComponent<Lf2CharacterSelect>();
            vsGo.AddComponent<Lf2VsManager>();
        }

        private void InstallStageMode(int characterId, int difficulty)
        {
            LoadAllCharacterData();

            if (FindAnyObjectByType<Lf2StageModeManager>() != null)
                return;

            var oldPlayer = GameObject.Find("Player");
            if (oldPlayer != null) Destroy(oldPlayer);

            var stageGo = new GameObject("StageModeManager");
            DontDestroyOnLoad(stageGo);
            var mgr = stageGo.AddComponent<Lf2StageModeManager>();
            mgr.StartStageMode(characterId, difficulty);
        }

        private void InstallChampionshipMode()
        {
            LoadAllCharacterData();

            if (FindAnyObjectByType<Lf2ChampionshipManager>() != null)
                return;

            var oldPlayer = GameObject.Find("Player");
            if (oldPlayer != null) Destroy(oldPlayer);

            var champGo = new GameObject("ChampionshipManager");
            DontDestroyOnLoad(champGo);
            champGo.AddComponent<Lf2CharacterSelect>();
            champGo.AddComponent<Lf2ChampionshipManager>();
        }

        private void InstallBattleMode()
        {
            LoadAllCharacterData();

            if (FindAnyObjectByType<Lf2BattleManager>() != null)
                return;

            var oldPlayer = GameObject.Find("Player");
            if (oldPlayer != null) Destroy(oldPlayer);

            var battleGo = new GameObject("BattleManager");
            DontDestroyOnLoad(battleGo);
            battleGo.AddComponent<Lf2CharacterSelect>();
            battleGo.AddComponent<Lf2BattleManager>();
        }

        private void InstallDemoMode()
        {
            LoadAllCharacterData();

            if (FindAnyObjectByType<Lf2DemoMode>() != null)
                return;

            var oldPlayer = GameObject.Find("Player");
            if (oldPlayer != null) Destroy(oldPlayer);

            var demoGo = new GameObject("DemoMode");
            DontDestroyOnLoad(demoGo);
            demoGo.AddComponent<Lf2DemoMode>();
        }

        private void InstallSurvivalMode()
        {
            LoadAllCharacterData();

            if (FindAnyObjectByType<Lf2SurvivalManager>() != null)
                return;

            var oldPlayer = GameObject.Find("Player");
            if (oldPlayer != null) Destroy(oldPlayer);

            var survivalGo = new GameObject("SurvivalManager");
            DontDestroyOnLoad(survivalGo);
            survivalGo.AddComponent<Lf2CharacterSelect>();
            survivalGo.AddComponent<Lf2SurvivalManager>();
        }

        private void Install()
        {
            _davis = MvpSoFactory.CreateDavis();
            _registry = MvpSoFactory.CreateRegistry(_davis);

            LoadAllCharacterData();
            ApplyCharacterSelection(_selectedCharacterIndex);

            var player = GameObject.Find("Player");
            if (player == null)
            {
                player = new GameObject("Player");
                var cam = Camera.main;
                if (cam != null)
                    player.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);

                var sr = player.AddComponent<SpriteRenderer>();
                Sprite2DMaterialUtility.EnsureCompatibleMaterial(sr);
                sr.sprite = CreatePlaceholderSprite();
                sr.color = new Color(1f, 0.95f, 0.35f, 1f);

                player.AddComponent<PlayerHsmController>();
            }

            if (player != null)
            {
                Sprite2DMaterialUtility.EnsureCompatibleMaterial(player.GetComponent<SpriteRenderer>());

                if (player.GetComponent<Health>() == null)
                    player.AddComponent<Health>();
                if (player.GetComponent<Mana>() == null)
                    player.AddComponent<Mana>();
                if (player.GetComponent<CharacterMotor>() == null)
                    player.AddComponent<CharacterMotor>();
                if (player.GetComponent<Damageable>() == null)
                    player.AddComponent<Damageable>();
                if (player.GetComponent<CircleCollider2D>() == null)
                {
                    var c = player.AddComponent<CircleCollider2D>();
                    c.isTrigger = true;
                    c.radius = 0.35f;
                }
                if (player.GetComponent<Rigidbody2D>() == null)
                {
                    var rb = player.AddComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.simulated = true;
                }

                if (player.GetComponent<DepthSortByY>() == null)
                    player.AddComponent<DepthSortByY>();
                if (player.GetComponent<Lf2PlayerSpriteAnimator>() == null)
                    player.AddComponent<Lf2PlayerSpriteAnimator>();
                if (player.GetComponent<ReactiveCombatDebugOverlay>() == null)
                    player.AddComponent<ReactiveCombatDebugOverlay>();
                if (player.GetComponent<ArenaBounds>() == null)
                    player.AddComponent<ArenaBounds>();

                if (player.GetComponent<Lf2Hud>() == null)
                {
                    var hud = player.AddComponent<Lf2Hud>();
                    hud.Bind(
                        player.GetComponent<Health>(),
                        player.GetComponent<Mana>(),
                        "Davis",
                        0);
                }

                var hsm = player.GetComponent<PlayerHsmController>();
                if (hsm == null)
                    hsm = player.AddComponent<PlayerHsmController>();
                hsm.Configure(_activeCharacterDef ?? _davis, _activeLf2Data, _activeProjectileMap);

                if (player.GetComponent<Lf2Hud>() == null)
                {
                    var hud = player.AddComponent<Lf2Hud>();
                    hud.Bind(
                        player.GetComponent<Health>(),
                        player.GetComponent<Mana>(),
                        _activeCharacterName ?? "Davis",
                        _selectedCharacterIndex);
                }
            }

            var mainCam = Camera.main;
            if (mainCam != null && mainCam.GetComponent<CameraFollow2D>() == null)
                mainCam.gameObject.AddComponent<CameraFollow2D>();

            PlayerHealthHud.AutoInstallForRuntime();
        }

private void Update()
        {
            CombatReadabilityFx.Tick(Time.deltaTime);

#if ENABLE_INPUT_SYSTEM
            var kb = UnityEngine.InputSystem.Keyboard.current;
            if (kb != null)
            {
                if (kb.f1Key.wasPressedThisFrame)
                    SwitchCharacter(0);
                else if (kb.f2Key.wasPressedThisFrame)
                    SwitchCharacter(1);
                else if (kb.f3Key.wasPressedThisFrame)
                    SwitchCharacter(2);
            }
#else
            if (Lf2Input.GetKeyDown(KeyCode.F1))
                SwitchCharacter(0);
            else if (Lf2Input.GetKeyDown(KeyCode.F2))
                SwitchCharacter(1);
            else if (Lf2Input.GetKeyDown(KeyCode.F3))
                SwitchCharacter(2);
#endif
        }

        private static void LoadAllCharacterData()
        {
            _davisLf2Data = null;
            _davisProjectileMap = null;
            _deepLf2Data = null;
            _deepProjectileMap = null;
            _johnLf2Data = null;
            _johnProjectileMap = null;

            LoadCharacterPair("davis", "davis_ball", ref _davisLf2Data, ref _davisProjectileMap, 207);
            LoadCharacterPair("deep", "deep_ball", ref _deepLf2Data, ref _deepProjectileMap, 203);
            LoadJohnData();
        }

        private static void LoadCharacterPair(
            string charName, string ballName,
            ref Lf2CharacterData charData,
            ref Dictionary<int, Lf2CharacterData> projMap,
            int defaultOid)
        {
            var charDat = Resources.Load<TextAsset>($"LF2/{charName}");
            if (charDat != null)
            {
                charData = Lf2DatRuntimeLoader.LoadFromBytes(charDat.bytes);
                if (charData?.Frames != null && charData.Frames.Count > 0)
                    Debug.Log($"[GameRuntimeInstaller] {charName} LF2 data loaded: {charData.Frames.Count} frames");
                else
                    Debug.LogWarning($"[GameRuntimeInstaller] {charName} .dat parsed but produced 0 frames.");
            }
            else
            {
                Debug.LogWarning($"[GameRuntimeInstaller] Resources/LF2/{charName} not found.");
            }

            var ballDat = Resources.Load<TextAsset>($"LF2/{ballName}");
            if (ballDat != null)
            {
                var ballData = Lf2DatRuntimeLoader.LoadFromBytes(ballDat.bytes);
                if (ballData?.Frames != null && ballData.Frames.Count > 0)
                {
                    projMap = new Dictionary<int, Lf2CharacterData> { { defaultOid, ballData } };
                    Debug.Log($"[GameRuntimeInstaller] {ballName} LF2 data loaded: {ballData.Frames.Count} frames (oid={defaultOid})");
                }
                else
                {
                    Debug.LogWarning($"[GameRuntimeInstaller] {ballName}.dat parsed but produced 0 frames.");
                }
            }
            else
            {
                Debug.LogWarning($"[GameRuntimeInstaller] Resources/LF2/{ballName} not found.");
            }
        }

        private static void LoadJohnData()
        {
            var johnDat = Resources.Load<TextAsset>("LF2/john");
            if (johnDat != null)
            {
                _johnLf2Data = Lf2DatRuntimeLoader.LoadFromBytes(johnDat.bytes);
                if (_johnLf2Data?.Frames != null && _johnLf2Data.Frames.Count > 0)
                    Debug.Log($"[GameRuntimeInstaller] john LF2 data loaded: {_johnLf2Data.Frames.Count} frames");
                else
                    Debug.LogWarning("[GameRuntimeInstaller] john .dat parsed but produced 0 frames.");
            }
            else
            {
                Debug.LogWarning("[GameRuntimeInstaller] Resources/LF2/john not found.");
            }

            _johnProjectileMap = new Dictionary<int, Lf2CharacterData>();

            var johnBallDat = Resources.Load<TextAsset>("LF2/john_ball");
            if (johnBallDat != null)
            {
                var ballData = Lf2DatRuntimeLoader.LoadFromBytes(johnBallDat.bytes);
                if (ballData?.Frames != null && ballData.Frames.Count > 0)
                {
                    _johnProjectileMap[200] = ballData;
                    Debug.Log($"[GameRuntimeInstaller] john_ball LF2 data loaded: {ballData.Frames.Count} frames (oid=200)");
                }
            }

            var johnBiscuitDat = Resources.Load<TextAsset>("LF2/john_biscuit");
            if (johnBiscuitDat != null)
            {
                var biscuitData = Lf2DatRuntimeLoader.LoadFromBytes(johnBiscuitDat.bytes);
                if (biscuitData?.Frames != null && biscuitData.Frames.Count > 0)
                {
                    _johnProjectileMap[214] = biscuitData;
                    Debug.Log($"[GameRuntimeInstaller] john_biscuit LF2 data loaded: {biscuitData.Frames.Count} frames (oid=214)");
                }
            }
        }

        public static void ApplyCharacterSelection(int index)
        {
            _selectedCharacterIndex = index;
            switch (index)
            {
                case 1:
                    _activeLf2Data = _deepLf2Data;
                    _activeProjectileMap = _deepProjectileMap;
                    _activeCharacterName = "Deep";
                    break;
                case 2:
                    _activeLf2Data = _johnLf2Data;
                    _activeProjectileMap = _johnProjectileMap;
                    _activeCharacterName = "John";
                    break;
                default:
                    _activeLf2Data = _davisLf2Data;
                    _activeProjectileMap = _davisProjectileMap;
                    _activeCharacterName = "Davis";
                    break;
            }

            _activeCharacterDef = CreateCharacterDef(_selectedCharacterIndex, _activeCharacterName, _activeLf2Data);
            Debug.Log($"[GameRuntimeInstaller] Character selected: {_activeCharacterName} (index={index})");
        }

        private static CharacterDefinition CreateCharacterDef(int id, string name, Lf2CharacterData lf2Data)
        {
            var def = ScriptableObject.CreateInstance<CharacterDefinition>();
            def.lf2Id = id;
            def.displayName = name ?? "Unknown";
            def.movement = lf2Data?.Movement != null
                ? CharacterMovementConfig.FromLf2Movement(lf2Data.Movement)
                : default;
            def.frameRoleIds = lf2Data?.RoleIds ?? Lf2FrameRoleIds.BuildFromCharacterData(lf2Data);
            return def;
        }

        public static void SwitchCharacter(int index)
        {
            ApplyCharacterSelection(index);

            var player = GameObject.Find("Player");
            if (player == null) return;

            var hsm = player.GetComponent<PlayerHsmController>();
            if (hsm != null)
            {
                hsm.Configure(_activeCharacterDef, _activeLf2Data, _activeProjectileMap);
            }

            var hud = player.GetComponent<Lf2Hud>();
            if (hud != null)
                hud.Bind(player.GetComponent<Health>(), player.GetComponent<Mana>(),
                    _activeCharacterName, index);

            var charLower = (_activeCharacterName ?? "davis").ToLowerInvariant();
            Lf2VisualLibrary.SetPlayerCharacter(charLower);
        }

        private static Sprite CreatePlaceholderSprite()
        {
            const int size = 32;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;
            var pixels = new Color32[size * size];
            for (var i = 0; i < pixels.Length; i++) pixels[i] = new Color32(255, 255, 255, 255);
            tex.SetPixels32(pixels);
            tex.Apply(false, true);
            return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 32f);
        }

        private static class MvpSoFactory
        {
            public static CharacterDefinition CreateDavis()
            {
                var so = ScriptableObject.CreateInstance<CharacterDefinition>();
                so.lf2Id = 0;
                so.displayName = "Davis";
                return so;
            }

            public static DefinitionRegistry CreateRegistry(CharacterDefinition character)
            {
                var so = ScriptableObject.CreateInstance<DefinitionRegistry>();
                so.characters = new[] { character };
                so.Rebuild();
                return so;
            }
        }
    }
}
