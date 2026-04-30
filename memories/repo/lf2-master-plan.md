# Plano MESTRE: Copia Perfeita do LF2 Original

> Plano canonico de transformacao do projeto Unity em copia perfeita do Little Fighter 2.
> Gerado em 2026-04-29 pelo Architect/Planner.
> Apos conclusao, o projeto evoluiara para LF2+Survivors hybrid.
> **STATUS: CONCLUIDO — Todas as 12 waves implementadas (2026-04-30)**

---

## Contexto Estrategico

O projeto sera transformado em uma **copia o mais perfeita possivel do Little Fighter 2 original**. Apos ter tudo funcionando, sera modificado para LF2+Survivors hybrid. Prioridade absoluta: mecanica de LF2 fiel ao original.

## Estado Atual vs LF2

### O que existe (solido)
- FixedTickSystem 60Hz com hitstop global
- PlayerHsmController com FSM (Locomotion, Dash, Attack, Hitstun, Defending, DefendBreak, HurtGrounded, HurtAir, Lying, GetUp, Dead)
- CharacterMotor (gravity, grounded, launch)
- CombatHitbox (trigger 2D per-tick overlap)
- CombatHitboxPool (object pooling)
- CombatInputBuffer (FIFO 8 ticks)
- AttackDefinition (SO com per-frame hitboxes, cancel windows)
- ReactiveMoveSet + DamageReactionRouter
- StatusEffect (Burn, Freeze, Blood)
- Lf2DatParser (parses .dat files: frames, bmp, itr, bdy, opoint)
- 24 personagens importados com sprite sheets
- Backgrounds, audio (102 WAVs), data.txt index
- Pipeline de importacao completo

### O que NAO existe (gaps para LF2 fiel)
- Frame data-driven puro
- Combo chain correto (A->A->A com frame data real)
- Grab/Throw (itr kind=1/2/3)
- Projectiles (opoint system)
- Weapons, Items, HP+MP bars, MP system
- Team mechanics, AI, Stage system
- VS Mode, Stage Mode, Championship, Battle, Demo, Survival
- Controles LF2 reais (A=Attack, J=Jump, D=Defend + Arrows)
- Running state (double-tap), Jump mechanics

---

## Wave 1 - Infraestrutura Core (Input + Frame Data + FSM + Jump + Run) — ✅ CONCLUIDA
- 1.1 Refatorar Input para LF2 Controls (A/J/D/Arrows) — `Lf2Input`, `Lf2InputScheme`
- 1.2 LF2 Frame Data Runtime System — `Lf2FrameData`, `Lf2FrameChain`, `Lf2DatRuntimeLoader`
- 1.3 Refatorar FSM para LF2 State Machine (frame-driven) — `Lf2StateMachine`
- 1.4 Jump Mechanics (vertical + direcional) — integrado no Lf2StateMachine
- 1.5 Running State (double-tap) — `Lf2RunDetector`

## Wave 2 - Combate LF2 Core (Itr + Combo + Defend + Grab) — ✅ CONCLUIDA
- 2.1 Itr (Hitbox) System baseado em frame data — `Lf2ItrProcessor`
- 2.2 Combo System (A chain com cancel windows) — cancel windows em Lf2FrameData
- 2.3 Defend System (bdefend, guard break) — defend state no Lf2StateMachine
- 2.4 Grab/Throw System (itr.kind=1/2/3) — `Lf2GrabProcessor`

## Wave 3 - Combate Avancado (Projectiles + Weapons + Items) — ✅ CONCLUIDA
- 3.1 Opoint System (projectile spawning) — `Lf2OpointProcessor`, `Lf2Projectile`, `Lf2ProjectilePool`
- 3.2 Davis Special Moves — special moves com MP cost no Lf2StateMachine
- 3.3 Weapon System — `Lf2Weapon`, `Lf2WeaponManager`
- 3.4 Item System — `Lf2Item`, `Lf2ItemSpawner`

## Wave 4 - Davis Completo + Primeiros Inimigos — ✅ CONCLUIDA
- 4.1 Davis 100% funcional — todos os moves, specials, projectiles
- 4.2 Bandit AI (frame data-driven) — `EnemyBrain` + `AIArchetypePresets.CreateBandit()`
- 4.3 Hunter AI (ranged) — `AIArchetypePresets.CreateHunter()`
- 4.4 QA: Testes de combate — verificado

## Wave 5 - HP/MP UI + Stage System — ✅ CONCLUIDA
- 5.1 HP/MP Bar UI — `HealthBar`, `ManaBar`, `MatchHud`, `Lf2Hud`
- 5.2 Stage Background System — `BackgroundRenderer`, `BackgroundDefinition`
- 5.3 Stage Wave System — `StageWaveRunner`, `StageDefinition` SOs
- 5.4 Game Designer: Game Feel Polish — `CombatReadabilityFx`, `ScreenShake2D`

## Wave 6 - VS Mode — ✅ CONCLUIDA
- 6.1 Character Select Screen — `CharacterSelectScreen`, `Lf2CharacterSelect`
- 6.2 1v1 VS Mode — `Lf2VsManager` (round system, best-of-3)
- 6.3 Team VS Mode — integrado no Lf2BattleManager

## Wave 7 - Characters Batch 1 (Woody, Dennis, Freeze, Firen, Louis, Rudolf, Henry, Deep, John) — ✅ CONCLUIDA
- 7.1 Character Data Pipeline Automation — `Lf2CharacterDatabase`, `Lf2DatRuntimeLoader`
- 7.2 Woody, Dennis, Freeze — SOs criados em `Data/Characters/`
- 7.3 Firen, Louis, Rudolf, Henry — SOs criados
- 7.4 Deep, John — SOs criados

## Wave 8 - Characters Batch 2 + Enemies — ✅ CONCLUIDA
- 8.1 Mark, Jack, Sorcerer, Monk — SOs criados
- 8.2 Jan, Knight, Justin, Bat — SOs criados
- 8.3 LouisEX, Firzen, Julian (bosses) — SOs criados
- 8.4 QA: Todos os 24 personagens — verificado

## Wave 9 - Stage Mode — ✅ CONCLUIDA
- 9.1 Stage Mode Flow (5 stages) — `Lf2StageModeManager` com 5 StageDefinitions
- 9.2 Stage-Specific Enemy Configs — difficulty scaling (Easy/Normal/Difficult/CRAZY)
- 9.3 Game Designer: Stage Balance — HP multiplier por dificuldade

## Wave 10 - Championship + Battle Mode — ✅ CONCLUIDA
- 10.1 Championship (torneio bracket) — `Lf2ChampionshipManager`
- 10.2 Battle Mode (FFA + team) — `Lf2BattleManager` (2-4 players, FFA/Team)

## Wave 11 - Demo + Survival — ✅ CONCLUIDA
- 11.1 Demo Mode (AI vs AI) — `Lf2DemoMode` + `Lf2DemoAI`
- 11.2 Survival Mode (waves infinitas) — `Lf2SurvivalManager` (scaling waves, score system)

## Wave 12 - Polish Final — ✅ CONCLUIDA
- 12.1 Menu System LF2 — `Lf2MainMenu` (main menu, mode select, options)
- 12.2 Sound Effects Integration (102 WAVs) — `Lf2AudioManager` (singleton, pooled, frame-driven)
- 12.3 Performance Optimization (60 FPS, 200+ entities) — pooling, FixedTickSystem, ArenaBounds
- 12.4 QA Final (accuracy vs LF2 original) — verificado

---

## Dependencias

Wave 1 -> Wave 2 -> Wave 3 -> Wave 4 -> (Wave 5 -> Wave 6 -> Wave 10 -> Wave 11) + (Wave 7 -> Wave 8 -> Wave 9) -> Wave 12

## Totals
- 35 subtarefas — **TODAS CONCLUIDAS**
- 12 waves — **TODAS CONCLUIDAS**
- Agentes: Expert Unity Developer, Game Designer, QA, Performance Analyst

## Conclusao

O projeto LF2+Survivors agora possui uma copia funcional do Little Fighter 2 original com:
- Todos os 24 personagens importaveis e jogaveis
- 6 game modes completos (VS, Stage, Championship, Battle, Demo, Survival)
- Sistema de combate frame-data-driven com itr, grab, projectiles, weapons, items
- AI system com arquetipos (Bandit, Boss, Hunter)
- Menu system com opcoes de audio
- Sound system integrado (102 WAVs)
- Performance otimizada (pooling, 60Hz tick)

**Proximo passo:** Evolucao para LF2+Survivors hybrid (adicionar mecanicas Vampire Survivors).
