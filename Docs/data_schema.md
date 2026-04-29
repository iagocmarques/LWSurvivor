# Data Schema (MVP)

## Objetivo

Separar **conteúdo** de **runtime** para permitir expansão de personagens, inimigos e upgrades sem reescrever lógica.

## Assets principais

- `CharacterDefinition`
  - `id`: identificador único (`character.hero.01`)
  - `displayName`: nome exibido
  - `tuning`: referência de `PlayerTuning`
  - `jab`, `launcher`, `dashAttack`: referências para `AttackDefinition`

- `EnemyDefinition`
  - `id`: identificador único (`enemy.grunt.01`)
  - `displayName`: nome exibido
  - `maxHealth`, `moveSpeed`, `touchDamage`, `thinkInterval`, `xpDrop`

- `UpgradeDefinition`
  - `id`: identificador único (`upgrade.move.01`)
  - `displayName`, `description`
  - `kind`: `MoveSpeed`, `Damage`, `MaxHealth`
  - `magnitude`: intensidade percentual

## Registry

- `DefinitionRegistry`
  - Arrays de `characters`, `enemies`, `upgrades`
  - Índices por `id` reconstruídos em `OnEnable`/`OnValidate`
  - Lookup por `TryGetCharacter`, `TryGetEnemy`, `TryGetUpgrade`

## Convenções de ID

- Prefixo por domínio:
  - `character.*`
  - `enemy.*`
  - `upgrade.*`
- Sufixo incremental de versão/conteúdo:
  - `.01`, `.02`, ...
- IDs devem ser imutáveis após release para evitar quebrar save/telemetria.
