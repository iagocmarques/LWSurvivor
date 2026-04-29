# Sugestões de Orquestração Diferidas

> Melhorias de arquitetura intencionalmente diferidas.
> Agrupadas pelos 7 princípios canônicos de auto-auditoria.
> Entradas datadas e acionáveis. Remover quando implementadas ou substituídas.

---

## 1. Ser confiável

### [2026-04-29] CharacterMotor extraction from reactive combat

**Problema:** HurtAir state applies gravity inline in PlayerHsmController. No CharacterMotor abstraction exists. When more characters and airborne states are added, gravity logic will be duplicated.
**Solução:** Extract a CharacterMotor component handling gravity, grounded check, velocity integration. HSM states delegate to it instead of computing physics directly.
**Impacto:** Alto (prevents duplication across characters, unblocks projectile arcs, cleaner state logic).
**Prioridade:** Média (works for now, becomes urgent when adding more characters or projectile system).

### [2026-04-28] Lambda closure caching in PlayerHsmController

**Problema:** `PlayerHsmController.Configure()` aloca delegates via lambda closures em cada chamada. Se `Configure()` for chamado multiplas vezes (ex: respawn), gera pressao de GC desnecessária.
**Solução:** Cachear os delegates em campos privados após primeira alocação.
**Impacto:** Baixo (Configure roda raramente), mas elimina footgun de performance.
**Prioridade:** Baixa.

### [2026-04-28] Enemy arena bounds clamping

**Problema:** Inimigos podem sair dos limites da arena se spawnados perto das bordas ou com knockback forte.
**Solução:** Aplicar `ArenaBounds.ClampToBounds()` no `EnemyAgent.FixedTick()` ou no `EnemySpawnerDirector` ao posicionar spawn points.
**Impacto:** Médio (inimigos fora da arena quebram gameplay loop).
**Prioridade:** Média.

## 2. Ser o mais rápido possível na implementação (paralelização)

_Nenhuma sugestão pendente._

## 3. Realizar auto-auditoria

_Nenhuma sugestão pendente._

## 4. Rodar testes confiáveis em cada iteração

_Nenhuma sugestão pendente._

## 5. Escrever DoD com evidências e relatórios

_Nenhuma sugestão pendente._

## 6. Ser autônomo

### [2026-04-28] Projectile system (Davis energy ball)

**Problema:** O jogo atual só tem ataques melee. Davis (personagem do LF2 original) tem projéteis (energy ball). Sem sistema de projéteis, o leque de gameplay é limitado.
**Solução:** Implementar `ProjectileDefinition` (ScriptableObject) + `ProjectileAgent` (movimento, colisão hitbox/hurtbox, TTL, pooling). Integrar com `GameObjectPool` existente e `AttackDefinition`.
**Impacto:** Alto (expande variedade de gameplay significativamente).
**Prioridade:** Alta (próximo feature após MVP validado).

### [2026-04-28] Audio system

**Problema:** O projeto não tem nenhum sistema de áudio. Hit stops, shake e flash estão visuais mas sem feedback sonoro.
**Solução:** Implementar `AudioManager` singleton com pooling de AudioSource. Criar `SfxDefinition` (ScriptableObject) para sons de hit, KO, level up, UI. Integrar chamadas no `CombatReadabilityFx` e `RunManager`.
**Impacto:** Alto (áudio é crítico para game feel e feedback de combate).
**Prioridade:** Alta.

### [2026-04-28] UI Canvas (replace OnGUI)

**Problema:** HUD atual usa `OnGUI` (immediate mode). Não escala para telas complexas, não suporta animações de UI, e é ineficiente para updates frequentes.
**Solução:** Migrar para Unity UI Canvas com components reutilizáveis (`HealthBar`, `UpgradePicker`, `VictoryScreen`, `DeathScreen`). Manter `OnGUI` apenas para debug overlay.
**Impacto:** Médio (HUD funcional, mas migração é trabalho substancial).
**Prioridade:** Média.

## 7. Ser custo-efetivo

### [2026-04-28] More characters beyond Davis

**Problema:** Só existe 1 personagem jogável (Davis). Para variedade de gameplay e replayability, precisamos de mais personagens com movesets distintos.
**Solução:** Criar `CharacterDefinition` variants (ex: Deep, Freeze) com `AttackDefinition[]` próprios. Os dados já estão em ScriptableObject, então a extensão é puramente data + sprites.
**Impacto:** Médio (replayability), mas alto custo de arte.
**Prioridade:** Média (pos-MVP, depende de arte).

### [2026-04-28] More enemy types beyond Bandit

**Problema:** Só existe 1 tipo de inimigo (Bandit). Para variedade de combate e progression, precisamos de mais tipos.
**Solução:** Criar `EnemyDefinition` variants (ex: Archer, Heavy, Ninja) com stats, sprites e comportamentos distintos. `EnemySpawnerDirector` já suporta weighted spawning.
**Impacto:** Médio (variedade de combate).
**Prioridade:** Média (pos-MVP, depende de arte).

### [2026-04-28] Real Steamworks integration

**Problema:** Netcode atual usa `LoopbackTransport` (local apenas). Sem Steamworks real, não é possível jogar online.
**Solução:** Implementar `SteamLobbyService` com Steamworks.NET (lobby, invite, IDs reais). Substituir `LoopbackTransport` por `SteamNetworkingSockets`/SDR. Reconciliador de player por input buffer de rede.
**Impacto:** Alto (funcionalidade core para multiplayer).
**Prioridade:** Alta (pos-MVP, mas blocker para playtest online).
