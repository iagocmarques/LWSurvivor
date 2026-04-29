# Progresso de execucao automatica do MVP

## Status geral

- Prompt 0-1: base de bootstrap + tick 60Hz + HUD debug ja funcional. [VERIFICADO]
- Prompt 2-4: movimento 2.5D, FSM player, buffer de input e combo minimo ja implementados. [VERIFICADO]
- Prompt 5-6: hitboxes data-driven por frame + pipeline de dano com hit stop e shake. [VERIFICADO]
- Prompt 7-8: horda com pooling + loop de run/XP/level up e escolha de upgrades. [VERIFICADO]
- Prompt 9: definitions com IDs + registry + documentacao de schema. [VERIFICADO]
- Prompt 10-12: camada de netcode inicial (Host/Join, sessao, snapshots compactos de horda) implementada com transporte loopback para validar arquitetura. [VERIFICADO]
- Prompt 13: base de robustez (desconexao, progresso parcial e UX minima de recuperacao) implementada. [VERIFICADO]
- Prompt 14: pipeline CI Windows + guia de operacao (LFS, crash reporting, telemetria) adicionado. [VERIFICADO]

## Integracao MVP (2026-04-28)

### Arquitetura consolidada

**GameRuntimeInstaller.cs** e o bootstrap unico de todo o jogo:
- Cria todas as instancias de ScriptableObject programaticamente via `MvpSoFactory` (sem necessidade de .asset files)
- Cria o Player automaticamente se nao existir
- Fia PlayerHsmController, componentes de combate, camera e arena bounds
- Cria RunManager, EnemySpawnerDirector e conecta tudo
- Ticks CombatReadabilityFx popup pool

### Player

- `PlayerHsmController` com metodo `Configure()` para setup de ataques
- 3 ataques: Jab, Launcher, DashAttack
- Sprite flip baseado em facing direction
- Integra com sistema de dano existente (hitStop, shake, flash)

### Inimigos

- `EnemySpawnerDirector` com `SetEnemyDefinition()` para configurar tipo de inimigo
- Sprite de bandit integrado
- Sprite flip funcional
- `DepthSortByY` aplicado para rendering correto
- Hurtbox na layer 9

### Game loop funcional

- Run de 120s com timer
- XP e leveling
- Escolha de upgrades (1 de 3)
- Tela de vitoria ao completar run
- Morte e restart funcional

### Rendering

- `CameraFollow2D` com null-safe tracking
- `ArenaBounds` para limitar movimento na arena
- `DepthSortByY` com tiebreaker para empates de Y
- Background com sortingOrder -10000

### Combat tuning (Game Designer review)

| Ataque | hitStop (ticks) | shake |
|--------|----------------|-------|
| Jab | 3 | 0.12 |
| Launcher | 4 | 0.18 |
| DashAttack | 3 | 0.14 |

- Bandit: touchDamage 6, hitFlash 0.10s
- Enemy touch: hitStop 2 ticks, shake 0.08
- Shake duration escala com hitstop

### Performance

- `CombatReadabilityFx` usa object pooling (sem GC pressure)
- Enemy prefab na layer Hurtbox
- `CameraFollow2D` null-safe

### Sprite Pipeline (2026-04-29)

**Problema resolvido:** Sprites do LF2 apareciam como retangulos pretos/sem transparência no Unity.

**Causas raiz identificadas e corrigidas:**
1. `textureFormat: 1` (Alpha8) nos .meta — armazenava apenas alpha sem dados de cor. Corrigido para `textureFormat: -1` (RGBA32).
2. `LF2SpriteImporter` chamava `SaveAndReimport()` dentro de `OnPostprocessTexture` (proibido pela Unity). Reescrito para configurar tudo em `OnPreprocessTexture`.
3. `isReadable: 0` nos .meta — impossibilitava fallback de leitura de textura. Corrigido para `isReadable: 1`.
4. `Lf2VisualApplier` nao resetava `sr.color` apos GameRuntimeInstaller setar amarelo placeholder. Adicionado `sr.color = Color.white`.

**Arquivos alterados:**
- `Assets/_Project/Editor/LF2SpriteImporter.cs` (reescrito)
- `Assets/_Project/Gameplay/Visual/Lf2VisualLibrary.cs` (fallback CreateSpritesFromGrid)
- `Assets/_Project/Gameplay/Visual/Lf2VisualApplier.cs` (color reset)
- Todos os 16 `*_alpha.png.meta` em `Assets/_Project/Resources/LF2/`

**Documentacao:**
- Criado `doc/sprite_pipeline.md` com pipeline completo atualizado
- Arquivado `doc/lf2_asset_pipeline.md` (desatualizado, referenciava ArquivosOpus)
- Arquivado `doc/ArquivosOpus/` (usuario orientou usar ArquivosTRAE)

## Implementacoes da rodada anterior

- `Health` e `Damageable` como base de dano para player/inimigos/dummy.
- `ScreenShake2D` e hit stop global integrado ao `FixedTickSystem`.
- `AttackDefinition` expandido com `HitboxFrameDefinition[]`.
- Inspector custom de `AttackDefinition` para editar hitboxes por frame.
- `EnemyAgent` (AI barata com think interval) + `EnemySpawnerDirector`.
- `GameObjectPool` genérico para spawn/retorno de entidades.
- `RunManager` com XP, level up e escolhas de upgrade (1/2/3).
- `CharacterDefinition`, `EnemyDefinition`, `UpgradeDefinition` e `DefinitionRegistry`.
- `Docs/data_schema.md` explicando o modelo de dados.
- HUD de vida do player + estado de derrota com restart (`R`) para fechar loop jogável.
- Novo upgrade `AutoPulse` (ataque automático em área) para sensação Survivors.
- `RunBalanceDefinition` para pacing data-driven de run/xp/horda.
- HUD de rede melhorado (ping simulado, status, reconnect).
- `SessionRecoveryService` conectado a eventos de conexão para salvar progresso parcial em queda de sessão.
- Variedade de inimigos por progressão (Grunt/Scout/Bruiser) com spawn ponderado.
- Feedback de combate mais legível: popup de dano e KO + flash de hit.
- Pool de upgrades expandido (XP gain, pulse rate, pulse radius e variantes II).

## O que precisa de teste no Unity Editor

- [x] Sprite Davis renderiza com transparencia (sem retangulo preto)
- [x] Console mostra "Loaded 70 sprites for 'player_davis_0_alpha'" (sem erros)
- [x] F8 cicla backgrounds
- [x] Inimigos spawnam com cooldown 0.7s
- [ ] GameRuntimeInstaller roda sem erros no console
- [ ] Player spawna e responde a inputs (WASD + ataques)
- [ ] Inimigos aparecem via EnemySpawnerDirector
- [ ] Dano aplica hitStop + shake + flash corretamente
- [ ] XP acumula, level up funciona, upgrade picker aparece
- [ ] Timer de 120s conta e tela de vitoria aparece
- [ ] Morte do player mostra tela de morte + restart com R
- [ ] Camera segue player sem jitter
- [ ] DepthSortByY ordena corretamente (sem flicker)
- [ ] Arena bounds impede saida da arena

## Proximo bloco (planejado)

- Integração real Steamworks (lobby/invite/IDs reais) na camada `SteamLobbyService`.
- Transporte real SteamNetworkingSockets/SDR substituindo `LoopbackTransport`.
- Reconciliador de player por input buffer de rede (prediction/reconciliation visual completo).
- Rejoin com snapshot full de run completo (estado de inimigos, upgrades e timers).
- Sistema de projeteis (energy ball do Davis).
- Sistema de audio (SFX de hit, KO, level up).
- Migracao de HUD para Unity UI Canvas.
- Mais personagens e tipos de inimigos.
