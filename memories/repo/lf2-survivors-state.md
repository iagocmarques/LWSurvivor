# Memória Duradoura do Projeto — LF2 + Survivors

> Estado atual do projeto para referência futura de todos os agentes.
> Mantido pelo Documentation Agent. Apenas verdade atual — sem histórico.

---

## Stack e Engine

- **Engine:** Unity 6 (6000.x) + URP 2D
- **Linguagem:** C# 12
- **Netcode:** Unity Netcode for GameObjects (host-client, Steam-first)
- **Build target:** PC (Windows, Steam)

## Arquitetura de Código

- **Assembly definitions** para separação de módulos.
- **ScriptableObjects** como fonte de verdade para dados de gameplay (attacks, characters, enemies).
- **Object pooling** via `GameObjectPool` para entidades frequentes.
- **FSM hierárquica** para estados de player: `PlayerHsmController` com estados Idle/Move/Dash/Attack/Hitstun/Dead.
- **2.5D movement**: X/Y onde Y = profundidade, ordenação visual por Y.
- **Game feel effects** parametrizados por golpe (hit stop, screen shake, flash — dados na hitbox), não hardcoded.

## Gameplay Data Model

- `CharacterDefinition` (ScriptableObject): define playable characters.
- `AttackDefinition` (ScriptableObject): defines attacks with hitbox data, damage, knockback, hit stop, etc.
- `EnemyDefinition` (ScriptableObject): defines enemy types with stats, sprites, behaviors.
- `EnemySpawnerDirector` suporta weighted spawning.

## Performance Targets

| Métrica | Meta |
| --- | --- |
| FPS | 60 |
| Tick de simulação | 60Hz fixo |
| Entidades em pico | 200–500 |
| GC allocation/frame | Mínima |

## Frequências de Sistema

- Movimento/colisão: 60Hz
- AI think: 10-20Hz
- Replicação swarm: 10-20Hz

## Estados Atuais

- **1 personagem jogável:** Davis
- **1 tipo de inimigo:** Bandit
- **Netcode:** LoopbackTransport (local apenas — sem Steamworks real)
- **Audio:** Nenhum sistema de audio implementado
- **UI:** OnGUI (immediate mode) — não Canvas

## Pending (ver `memories/orchestration-suggestions.md`)

- Projectile system (Davis energy ball)
- Audio system
- UI Canvas migration
- CharacterMotor extraction
- More characters/enemies
- Real Steamworks integration
- Enemy arena bounds clamping

## MCP Server

- `user-unity-mcp` em `http://localhost:23163`
- Não usar `user-ai-game-developer`
