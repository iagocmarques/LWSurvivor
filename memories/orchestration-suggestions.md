# Sugestões de Orquestração Diferidas

> Melhorias de arquitetura intencionalmente diferidas.
> Agrupadas pelos 7 princípios canônicos de auto-auditoria.
> Entradas datadas e acionáveis. Remover quando implementadas ou substituídas.
> Ultima revisao: 2026-04-30 (Wave 12.5 — Documentacao)

---

## 1. Ser confiável

### [2026-04-28] Lambda closure caching in PlayerHsmController — DIFERIDO

**Problema:** `PlayerHsmController.Configure()` aloca delegates via lambda closures em cada chamada. Se `Configure()` for chamado multiplas vezes (ex: respawn), gera pressao de GC desnecessária.
**Solução:** Cachear os delegates em campos privados após primeira alocação.
**Impacto:** Baixo (Configure roda raramente), mas elimina footgun de performance.
**Prioridade:** Baixa.

### ~~[2026-04-29] CharacterMotor extraction from reactive combat~~ — RESOLVIDO

CharacterMotor foi extraido como componente independente (`Assets/_Project/Gameplay/Player/CharacterMotor.cs`). Usado por PlayerHsmController, EnemyFactory, e todos os game modes.

### ~~[2026-04-28] Enemy arena bounds clamping~~ — RESOLVIDO

`ArenaBounds` (`Assets/_Project/Gameplay/Rendering/ArenaBounds.cs`) implementado e adicionado a todos os players e enemies nos game modes.

## 2. Ser o mais rápido possível na implementação (paralelização)

_Nenhuma sugestão pendente._

## 3. Realizar auto-auditoria

_Nenhuma sugestão pendente._

## 4. Rodar testes confiáveis em cada iteração

### [2026-04-30] Automated test coverage — DIFERIDO

**Problema:** Nenhum teste automatizado existe. Todos os game modes foram verificados manualmente.
**Solução:** Criar testes de unidade para Lf2StateMachine, Lf2ItrProcessor, Lf2GrabProcessor. Testes de integracao para game modes.
**Impacto:** Alto (regressao detection).
**Prioridade:** Media (importante antes de evoluir para hybrid).

## 5. Escrever DoD com evidências e relatórios

_Nenhuma sugestão pendente._

## 6. Ser autônomo

### ~~[2026-04-28] Projectile system (Davis energy ball)~~ — RESOLVIDO

Sistema de projéteis completo: `Lf2OpointProcessor`, `Lf2Projectile`, `Lf2ProjectilePool`. 5 personagens com projéteis (Davis, Deep, John, Henry, Firen).

### ~~[2026-04-28] Audio system~~ — RESOLVIDO

`Lf2AudioManager` implementado como singleton com pooled AudioSource, clip cache de 102 WAVs, categorias de SFX, volume controls, integracao frame-driven no Lf2StateMachine.

### ~~[2026-04-28] UI Canvas (replace OnGUI)~~ — DIFERIDO (renomeado)

**Status:** OnGUI continua para HUD in-game. Menu system usa OnGUI com styling LF2-authentic. Migracao para Canvas nao e critica para o objetivo atual (copia fiel do LF2).
**Impacto:** Medio.
**Prioridade:** Baixa (funcional para LF2 fiel).

### [2026-04-30] Real Steamworks integration — DIFERIDO

**Problema:** Netcode atual usa `LoopbackTransport` (local apenas). Sem Steamworks real, nao e possivel jogar online.
**Solução:** Implementar `SteamLobbyService` com Steamworks.NET (lobby, invite, IDs reais). Substituir `LoopbackTransport` por `SteamNetworkingSockets`/SDR.
**Impacto:** Alto (funcionalidade core para multiplayer).
**Prioridade:** Alta (pos-MVP, blocker para playtest online).

## 7. Ser custo-efetivo

### ~~[2026-04-28] More characters beyond Davis~~ — RESOLVIDO

Todos os 24 personagens do LF2 importados como CharacterDefinition SOs. Pipeline automatizado via Lf2CharacterDatabase + Lf2DatRuntimeLoader.

### ~~[2026-04-28] More enemy types beyond Bandit~~ — RESOLVIDO

EnemyFactory cria inimigos de qualquer Lf2CharacterData. AIArchetypePresets fornece Bandit, Boss, Hunter. Survival mode usa todos os 5 primeiros personagens como enemies com AI scaling.

### [2026-04-30] LF2+Survivors hybrid features — PROXIMO

**Problema:** O projeto e atualmente uma copia fiel do LF2. Para evoluir para LF2+Survivors hybrid, precisamos de mecanicas Vampire Survivors.
**Solução:** Adicionar: upgrade picker, passive abilities, XP/leveling, item drops, wave-based survival com progression, build variety.
**Impacto:** Alto (transforma o jogo de copia para hybrid).
**Prioridade:** Alta (proximo objetivo do projeto).

### [2026-04-30] Sprite visual system completion — DIFERIDO

**Problema:** Sprites atuais sao placeholders coloridos (32x32). Sprite sheets dos 24 personagens estao importados mas nao estao conectados ao sistema de animacao em todos os modes.
**Solução:** Rodar Lf2SpriteConverter, conectar Lf2VisualLibrary/Lf2PlayerSpriteAnimator em todos os game modes.
**Impacto:** Alto (visual fidelity).
**Prioridade:** Alta (necessario para experiencia LF2 autentica).
