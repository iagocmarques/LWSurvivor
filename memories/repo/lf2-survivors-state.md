# Memoria Duradoura do Projeto — LF2 + Survivors

> Estado atual do projeto para referencia futura de todos os agentes.
> Mantido pelo Documentation Agent. Apenas verdade atual — sem historico.
> Ultima atualizacao: 2026-04-30 (Wave 12.5 — Documentacao)

---

## Stack e Engine

- **Engine:** Unity 6 (6000.x) + URP 2D
- **Linguagem:** C# 12
- **Netcode:** Unity Netcode for GameObjects (host-client, Steam-first)
- **Build target:** PC (Windows, Steam)

## Progresso

- **Plano mestre:** Ver `lf2-master-plan.md` — 12 waves, 35 subtarefas
- **Progresso:** Waves 1-12 concluidas. Projeto LF2 fiel completo.
- **Proximo passo:** Evolucao para LF2+Survivors hybrid

## Arquitetura de Codigo

### Namespace e Assembly Layout

```
Project.Core.Input        — Lf2Input (unified input abstraction)
Project.Core.Tick          — FixedTickSystem 60Hz, ITickable, TickContext
Project.Data               — CharacterDefinition, StageDefinition, WeaponDefinition, MoveDefinition, BackgroundDefinition, DefinitionRegistry
Project.Gameplay.LF2       — Lf2StateMachine (sole authority), frame data, all game modes, combat processors
Project.Gameplay.AI        — EnemyBrain, AIArchetype, AIArchetypePresets, AISensor
Project.Gameplay.Combat    — Health, Mana, Damageable, CombatReadabilityFx, ReactiveMoveSet, StatusEffect, CombatInputBuffer
Project.Gameplay.Player    — PlayerHsmController, CharacterMotor, Lf2InputScheme
Project.Gameplay.Enemies   — EnemyFactory (static, creates enemies from Lf2CharacterData)
Project.Gameplay.Audio     — Lf2AudioManager (singleton, pooled SFX, music)
Project.Gameplay.Rendering — ArenaBounds, BackgroundRenderer, CameraFollow2D, DepthSortByY
Project.Gameplay.Visual    — Lf2VisualLibrary, Lf2PlayerSpriteAnimator, Lf2EnemySpriteAnimator, Sprite2DMaterialUtility
Project.Gameplay.Pooling   — GameObjectPool, Lf2ProjectilePool
Project.Gameplay.Modes     — StageWaveRunner
Project.Gameplay.Feedback  — ScreenShake2D
Project.UI.HUD             — HealthBar, ManaBar, MatchHud
Project.UI.Screens         — CharacterSelectScreen, ModeSelectScreen, OptionsMenuScreen
```

### Core Architecture: Lf2StateMachine

`Lf2StateMachine` (`Assets/_Project/Gameplay/LF2/Lf2StateMachine.cs`) is the **sole authority** for character state. It:

- Drives all state transitions from LF2 `.dat` frame data
- Processes input via `ProcessInput()` → returns `Lf2Action` enum
- Manages frame chain playback (`Lf2FrameChain`)
- Handles velocity, gravity, grounded check
- Integrates with `Lf2ItrProcessor` (hitbox), `Lf2GrabProcessor` (grab/throw), `Lf2OpointProcessor` (projectiles)
- Plays sounds via `Lf2AudioManager` on frame events
- Supports special moves (EnergyBlast, Shrafe, LeapAttack, DragonPunch, DownAttack, DownJump) with MP cost
- Run detection via `Lf2RunDetector` (double-tap)
- Combo chains via cancel windows in frame data

`PlayerHsmController` wraps `Lf2StateMachine` and bridges Unity MonoBehaviour lifecycle with the SM.

### Data-Driven Model

- `CharacterDefinition` (ScriptableObject): lf2Id, displayName, CharacterMovementConfig, ReactiveMoveSet, rawDatBytes, Lf2FrameRoleIds
- `StageDefinition` (ScriptableObject): lf2StageId, displayName, BackgroundDefinition, List<StagePhaseDefinition> (each with spawn entries, roles, HP overrides)
- `AIArchetype` (ScriptableObject): attackRange, retreatRange, aggression, defendChance, thinkInterval, cooldowns
- `Lf2CharacterDatabase` (ScriptableObject): central registry of all 24 characters with .dat bytes
- `Lf2DatRuntimeLoader`: parses .dat bytes at runtime into `Lf2CharacterData`

### Combat System

- `Lf2ItrProcessor`: processes itr (hitbox) data from frames, applies damage/knockback/status
- `Lf2GrabProcessor`: handles itr kind=1/2/3 (grab/caught/throw)
- `Lf2OpointProcessor`: spawns projectiles from opoint data in frames
- `Lf2Projectile`: projectile behavior with TTL, hitbox, pooling
- `Lf2WeaponManager` + `Lf2Weapon`: weapon pickup/drop system
- `Lf2ItemSpawner` + `Lf2Item`: item spawning (drinks, food)
- `Health`: HP with max, heal, damage, death events
- `Mana`: MP with spend/regen for special moves
- `CombatReadabilityFx`: hitstop, screen shake, flash (game feel)
- `StatusEffect`: Burn, Freeze, Blood

### AI System

- `EnemyBrain` (MonoBehaviour): state machine (Idle → SeekTarget → Approach → Attack → Retreat → Defend → Recover)
- `AIArchetype` (ScriptableObject): data-driven behavior parameters
- `AIArchetypePresets`: factory presets (Bandit, Boss, Hunter)
- `AISensor`: sensor data (distance, visibility, target state)
- `Lf2DemoAI`: lightweight AI for demo mode (AI vs AI)
- `EnemyFactory`: static factory creating enemies from Lf2CharacterData + AIArchetype

## Game Modes (Todos Implementados)

| Mode | Manager | Description |
| --- | --- | --- |
| VS Mode | `Lf2VsManager` | 1v1 local with character select, round system, best-of-3 |
| Stage Mode | `Lf2StageModeManager` | 5 stages with wave-based enemies, difficulty scaling (Easy/Normal/Difficult/CRAZY) |
| Championship | `Lf2ChampionshipManager` | Tournament bracket, sequential AI opponents |
| Battle Mode | `Lf2BattleManager` | FFA + Team Battle, 2-4 players, configurable |
| Demo Mode | `Lf2DemoMode` | AI vs AI showcase, random matchups, auto-cycling |
| Survival Mode | `Lf2SurvivalManager` | Endless waves with scaling difficulty, score system |

### Menu System

- `Lf2MainMenu`: main menu with mode select, options (volume sliders), LF2-authentic styling
- `CharacterSelectScreen`: character grid with P1/P2 selection
- `ModeSelectScreen`, `OptionsMenuScreen`: UI screens

## Personagens

### Todos os 24 personagens importados como CharacterDefinition SOs

Assets em `Assets/_Project/Data/Characters/`:
Davis, Deep, Dennis, Firen, Firzen, Freeze, Henry, Jack, Jan, John, Julian, Justin, Knight, Louis, LouisEX, Mark, Monk, Rudolf, Sorcerer, Woody, Bat

### Projectile Characters

Characters with projectile .dat files loaded at runtime:
- Davis → davis_ball (oid 207)
- Deep → deep_ball (oid 203)
- John → john_ball (oid 200), john_biscuit (oid 214)
- Henry → henry_arrow1 (oid 200), henry_arrow2 (oid 201), henry_wind (oid 218)
- Firen → firen_ball (oid 200), firen_flame (oid 212)

## Stages

5 stages with `StageDefinition` SOs, each with phased wave spawning, background definitions, and music paths.

## Sound System

- `Lf2AudioManager`: singleton, DontDestroyOnLoad, pooled AudioSource (8 SFX sources + 1 music)
- Clip cache from `Resources/Audio/lf2_ref/` (102 WAVs indexed by ID)
- SFX categories: Hit sounds (punch/kick/heavy/slash/bleed), Special moves, Projectiles, Defend, Movement, Items, Death/KO, Menu, Round, Stage music, Status effects, Crowd
- Volume controls: Master, SFX, Music (adjustable from options menu)
- Integrated into `Lf2StateMachine.PlayFrameSound()` for frame-driven audio

## Performance Optimizations

- `GameObjectPool`: generic object pooling
- `Lf2ProjectilePool`: dedicated projectile pooling
- `CombatHitboxPool`: hitbox overlap pooling
- `FixedTickSystem`: 60Hz fixed simulation tick
- `DepthSortByY`: 2.5D visual sorting
- `ArenaBounds`: entity clamping to arena limits
- Object pooling throughout (AudioSources, projectiles, hitboxes)

## Visual System

- `Lf2VisualLibrary`: sprite atlas management per character
- `Lf2PlayerSpriteAnimator` / `Lf2EnemySpriteAnimator`: frame-driven sprite animation
- `Lf2VisualApplier`: applies visual state from SM
- `Sprite2DMaterialUtility`: URP-compatible material for sprites
- `CameraFollow2D`: smooth camera tracking
- `BackgroundRenderer`: stage background rendering

## Pipeline de Importacao

- `Lf2DatParser` — parseia .dat files do LF2
- `Lf2DatDecryptor` — desencripta .dat files
- `Lf2DatRuntimeLoader` — carrega .dat bytes em Lf2CharacterData em runtime
- `Lf2FrameDataConverter` — converte frame data para formato interno
- `Lf2CharacterDatabase` — registro central de todos os personagens

## Sprite Pipeline (IMPORTANTE)

- Spritesheets em `Resources/LF2/` precisam ser `*_alpha.png` (nao `.bmp`)
- `LF2SpriteImporter` so processa arquivos `_alpha.png`
- Editor script `Lf2SpriteConverter` converte/copiar para formato correto
- Menu: `_Project/LF2/Convert and Import Sprites`
- **BUG CONHECIDO:** Se sprites mostram sheet inteira, rodar o converter

## MCP Server

- **Servidor:** `unity-mcp` via `relay_win.exe --mcp` (stdio, nao HTTP)
- **Config:** `kilo.json` → `type: "local"`, `command: ["relay_win.exe", "--mcp", "--project-path", "..."]`
- **Status:** Operacional (2026-04-30)
- **NAO usar** `http://localhost:23163` (endpoint nao existe, era configuracao antiga)
- **NAO usar** `user-unity-mcp` nem `user-ai-game-developer`
- **Relay path:** `C:\Users\Iago\.unity\relay\relay_win.exe`
- **Unity AI Assistant** (porta 9001) eh sistema separado, nao é o MCP

## Scenes

- `Bootstrap.unity` — cena de inicializacao
- `Game.unity` — cena principal de gameplay

## Input

- `Lf2Input`: unified input abstraction (KeyCode-based)
- `Lf2InputScheme`: per-player input scheme (P1/P2 mapping)
- LF2-authentic controls: A=Attack, J=Jump, D=Defend + Arrows

## Performance Targets

| Metrica | Meta |
| --- | --- |
| FPS | 60 |
| Tick de simulacao | 60Hz fixo |
| Entidades em pico | 200-500 |
| GC allocation/frame | Minima |
