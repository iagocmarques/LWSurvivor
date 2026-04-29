# Progresso de execução automática do MVP

## Status geral

- Prompt 0-1: base de bootstrap + tick 60Hz + HUD debug já funcional.
- Prompt 2-4: movimento 2.5D, FSM player, buffer de input e combo mínimo já implementados.
- Prompt 5-6: hitboxes data-driven por frame + pipeline de dano com hit stop e shake.
- Prompt 7-8: horda com pooling + loop de run/XP/level up e escolha de upgrades.
- Prompt 9: definitions com IDs + registry + documentação de schema.
- Prompt 10-12: camada de netcode inicial (Host/Join, sessão, snapshots compactos de horda) implementada com transporte loopback para validar arquitetura.
- Prompt 13: base de robustez (desconexão, progresso parcial e UX mínima de recuperação) implementada.
- Prompt 14: pipeline CI Windows + guia de operação (LFS, crash reporting, telemetria) adicionado.

## Implementações desta rodada

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

## Próximo bloco (planejado para continuar sem parar)

- Integração real Steamworks (lobby/invite/IDs reais) na camada `SteamLobbyService`.
- Transporte real SteamNetworkingSockets/SDR substituindo `LoopbackTransport`.
- Reconciliador de player por input buffer de rede (prediction/reconciliation visual completo).
- Rejoin com snapshot full de run completo (estado de inimigos, upgrades e timers).
