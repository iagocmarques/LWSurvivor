# Sugestoes de Orquestracao Diferidas

> Melhorias de arquitetura intencionalmente diferidas.
> Agrupadas pelos 7 principios canonicos de auto-auditoria.
> Entradas datadas e acionaveis. Remover quando implementadas ou substituidas.

---

## 1. Ser confiavel

### [2026-04-28] Lambda closure caching in PlayerHsmController

**Problema:** `PlayerHsmController.Configure()` aloca delegates via lambda closures em cada chamada. Se `Configure()` for chamado multiplas vezes (ex: respawn), gera pressao de GC desnecessaria.
**Solucao:** Cachear os delegates em campos privados apos primeira alocacao.
**Impacto:** Baixo (Configure roda raramente), mas elimina footgun de performance.
**Prioridade:** Baixa.

### [2026-04-28] Enemy arena bounds clamping

**Problema:** Inimigos podem sair dos limites da arena se spawnados perto das bordas ou com knockback forte.
**Solucao:** Aplicar `ArenaBounds.ClampToBounds()` no `EnemyAgent.FixedTick()` ou no `EnemySpawnerDirector` ao posicionar spawn points.
**Impacto:** Medio (inimigos fora da arena quebram gameplay loop).
**Prioridade:** Media.

## 2. Ser o mais rapido possivel na implementacao (paralelizacao)

_Nenhuma sugestao pendente._

## 3. Realizar auto-auditoria

_Nenhuma sugestao pendente._

## 4. Rodar testes confiaveis em cada iteracao

_Nenhuma sugestao pendente._

## 5. Escrever DoD com evidencias e relatorios

_Nenhuma sugestao pendente._

## 6. Ser autonomo

### [2026-04-28] Projectile system (Davis energy ball)

**Problema:** O jogo atual so tem ataques melee. Davis (personagem do LF2 original) tem projeteis (energy ball). Sem sistema de projeteis, o leque de gameplay e limitado.
**Solucao:** Implementar `ProjectileDefinition` (ScriptableObject) + `ProjectileAgent` (movimento, colisao hitbox/hurtbox, TTL, pooling). Integrar com `GameObjectPool` existente e `AttackDefinition`.
**Impacto:** Alto (expande variedade de gameplay significativamente).
**Prioridade:** Alta (proximo feature apos MVP validado).

### [2026-04-28] Audio system

**Problema:** O projeto nao tem nenhum sistema de audio. Hit stops, shake e flash estao visuais mas sem feedback sonoro.
**Solucao:** Implementar `AudioManager` singleton com pooling de AudioSource. Criar `SfxDefinition` (ScriptableObject) para sons de hit, KO, level up, UI. Integrar chamadas no `CombatReadabilityFx` e `RunManager`.
**Impacto:** Alto (audio e critico para game feel e feedback de combate).
**Prioridade:** Alta.

### [2026-04-28] UI Canvas (replace OnGUI)

**Problema:** HUD atual usa `OnGUI` (immediate mode). Nao escala para telas complexas, nao suporta animacoes de UI, e e ineficiente para updates frequentes.
**Solucao:** Migrar para Unity UI Canvas com components reutilizaveis (`HealthBar`, `UpgradePicker`, `VictoryScreen`, `DeathScreen`). Manter `OnGUI` apenas para debug overlay.
**Impacto:** Medio (HUD funcional, mas migracao e trabalho substancial).
**Prioridade:** Media.

## 7. Ser custo-efetivo

### [2026-04-28] More characters beyond Davis

**Problema:** So existe 1 personagem jogavel (Davis). Para variedade de gameplay e replayability, precisamos de mais personagens com movesets distintos.
**Solucao:** Criar `CharacterDefinition` variants (ex: Deep, Freeze) com `AttackDefinition[]` proprios. Os dados ja estao em ScriptableObject, entao a extensao e puramente data + sprites.
**Impacto:** Medio (replayability), mas alto custo de arte.
**Prioridade:** Media (pos-MVP, depende de arte).

### [2026-04-28] More enemy types beyond Bandit

**Problema:** So existe 1 tipo de inimigo (Bandit). Para variedade de combate e progression, precisamos de mais tipos.
**Solucao:** Criar `EnemyDefinition` variants (ex: Archer, Heavy, Ninja) com stats, sprites e comportamentos distintos. `EnemySpawnerDirector` ja suporta weighted spawning.
**Impacto:** Medio (variedade de combate).
**Prioridade:** Media (pos-MVP, depende de arte).

### [2026-04-28] Real Steamworks integration

**Problema:** Netcode atual usa `LoopbackTransport` (local apenas). Sem Steamworks real, nao e possivel jogar online.
**Solucao:** Implementar `SteamLobbyService` com Steamworks.NET (lobby, invite, IDs reais). Substituir `LoopbackTransport` por `SteamNetworkingSockets`/SDR. Reconciliador de player por input buffer de rede.
**Impacto:** Alto (funcionalidade core para multiplayer).
**Prioridade:** Alta (pos-MVP, mas blocker para playtest online).
