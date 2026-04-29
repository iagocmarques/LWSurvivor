# Brief pro Cursor — projeto LF2-inspired (beat 'em up + survivors) em Unity 6

> **Cole isto no `.cursorrules` da raiz do projeto.** Esse brief vale pra qualquer pedido relacionado ao gameplay. Detalhes adicionais em `docs/`.

---

## Contexto

Beat 'em up 2.5D em Unity 6 + URP 2D, **inspirado** em Little Fighter 2 (LF2) + Vampire Survivors. Co-op online 2-4p host-client (Steamworks). Tick fixo 60Hz, alvo 200-500 entidades. Arquitetura **data-driven** via ScriptableObjects.

Documentos mestres:
- `levantamento_e_diretrizes.md` — arquitetura macro do projeto (autoritativo).
- `docs/lf2_assets_guide.md` — assets do LF2, conversão, naming.
- `docs/architecture_walkthrough.md` — como os scripts do `Runtime/` se conectam.
- `docs/bandit_authoring_guide.md` — passo a passo pra um personagem.

Quando houver conflito entre o que vou pedir e qualquer um desses docs, **o doc ganha**. Quando houver conflito entre os docs, `levantamento_e_diretrizes.md` ganha.

---

## Regras absolutas

### Sobre os assets do LF2

1. **NUNCA** importar BMPs do LF2 direto na Unity. Eles usam preto puro (RGB 0,0,0) como chroma key. Use `tools/prepare_lf2_assets.py`.
2. **NUNCA** parsear `.dat` do LF2 em runtime. São obfuscados, proprietários, e copiar valores literais é problema de IP. Use `tools/decode_lf2_dat.py` apenas pra peekar (humano-only).
3. **NUNCA** copiar verbatim hitbox values, frame counts, dvx/dvy, movesets ou nomes do LF2. "Inspirado", não clonado.
4. **NUNCA** usar arquivos `_mirror.bmp` (a Unity flipa via `flipX`).
5. **NUNCA** usar `bgm/` do LF2 em build (IP de terceiros).

### Sobre arquitetura

6. **NUNCA** sair do tick fixo 60Hz na simulação. Toda lógica de gameplay roda em `ITickable.Tick(int)`. Visual pode rodar em Update/LateUpdate, mas tem que ler de `_previous`/`_current` interpolados via `TickRunner.Alpha`.
7. **NUNCA** usar `Time.deltaTime` em código de gameplay. Use `TickRunner.TICK_DT`.
8. **NUNCA** usar `Resources.Load` ou `FindObjectOfType` em runtime hot-path. Use `Registry` (lookup por ID) ou referências diretas via inspector.
9. **NUNCA** usar `OnTriggerEnter2D` pra detecção de hits. Use `HitboxResolver.SubmitAttack` / `SubmitVictim` no tick.
10. **NUNCA** instanciar GameObjects no hot loop (200+ enemies). Use `PrefabPool`.
11. **NUNCA** colocar gameplay logic em `Animator`/`StateMachineBehaviour`. Use `PlayerFSM` (ou similar) puro código.

---

## Layout dos scripts (já no repo)

```
Assets/
├── Editor/
│   └── LF2SpriteImporter.cs           # auto-config sprite import
├── Game/
│   └── Runtime/
│       ├── Tick/
│       │   └── TickRunner.cs          # 60Hz driver + ITickable
│       ├── Input/
│       │   └── PlayerInputReader.cs   # InputBuffer + Reader
│       ├── Movement/
│       │   └── CharacterController2_5D.cs  # Position2_5D + ctl
│       ├── Combat/
│       │   ├── HitFeel.cs             # hitstop + screen shake
│       │   ├── HitboxResolver.cs      # pairwise hit resolution
│       │   └── PlayerFSM.cs           # state machine
│       ├── Data/
│       │   ├── CharacterDefinition.cs # SO
│       │   ├── MoveDefinition.cs      # SO + FrameDefinition + HitboxDefinition + enums
│       │   ├── EnemyDefinition.cs     # SO
│       │   └── Registry.cs            # ID lookup SO
│       ├── Pooling/
│       │   └── ObjectPool.cs          # GenericPool + PrefabPool
│       ├── Swarm/
│       │   └── SwarmManager.cs        # struct-array horde controller
│       └── Bootstrap/
│           ├── BootstrapInstaller.cs  # scene init
│           └── PixelPerfectCameraSetup.cs
├── Art/
│   └── lf2_ref/                       # convertido por prepare_lf2_assets.py — read-only
└── Game/
    └── Characters/<Name>/             # CharacterDefinition + Moves/ — autoral
```

---

## Convenções de código

- **C#**: nullable não habilitado por enquanto. `var` quando o tipo é óbvio.
- **Naming**: `PascalCase` pra public, `_camelCase` pra private fields, `camelCase` pra locals.
- **Namespaces**: `LF2Game.<Subsistema>` (e.g. `LF2Game.Combat`, `LF2Game.Tick`).
- **MonoBehaviours**: campos `[Header]`-grouped, com `[SerializeField]` em vez de `public` quando não precisa de write externo.
- **Determinismo**: zero `Random.Range` sem seed em código de tick. Vai ter um `IRng` injetável quando o netcode chegar.
- **Allocations**: zero `new List<>()` ou `string.Format` no hot loop. Pré-aloca em Awake.
- **Comentários**: explicar o "porquê", não o "o quê". Especialmente referenciando seções do `levantamento_e_diretrizes.md` quando uma decisão veio de lá.

---

## Convenções de assets

- **Pivô de personagens**: Bottom Center (0.5, 0).
- **Pixels Per Unit**: 80 (1 célula LF2 = 1 unit Unity).
- **Filter Mode**: Point. **Compression**: None. **sRGB**: ON. **Alpha is Transparency**: ON.
- **Spritesheet de char**: 800×560, grid 10×7, células 80×80.
- **Layer de física**: irrelevante (não usamos Rigidbody2D pra combat). 2D Lights e camadas podem usar layers conforme necessário.

---

## Como fazer coisas comuns

### "Adicionar um personagem novo"

Siga `docs/bandit_authoring_guide.md`. Resumo:
1. Garantir que `prepare_lf2_assets.py` rodou.
2. Criar `CharacterDefinition`, preencher portrait + icon + frames + stats.
3. Criar `MoveDefinition`s em `Assets/Game/Characters/<Name>/Moves/`.
4. Adicionar moves ao char.
5. Registrar no `Registry`.

### "Adicionar um inimigo novo (swarm)"

1. Criar `EnemyDefinition` em `Assets/Game/Enemies/`.
2. Preencher `frames`, `walkFrames`, stats.
3. Adicionar à lista `Defs` do `SwarmManager`.
4. Pra spawnar: `SwarmManager.Instance.Spawn(defIndex, position)`.

### "Adicionar um novo move"

Sempre via `MoveDefinition` ScriptableObject — nunca código novo. Se precisar de comportamento que `MoveDefinition` não suporta (e.g. teleporte, projétil que persegue), avise: vamos estender `FrameDefinition` ou criar um `IMoveBehaviour` callback.

### "Tunar feel de um soco"

Edite o `MoveDefinition` no inspector. Não toque no código de combat.

### "Implementar parry/block"

Vai precisar de:
- Novo `CharacterState.Blocking` (já existe enum, basta usar).
- Flag `blockable` em `HitboxDefinition`.
- Logic em `PlayerFSM.TakeHit` pra checar se está blocking + facing right way.
- Pode ser que adicione `IsBlocking` em `IHitVictim`.

### "Adicionar suporte a um 2º jogador local"

- Duplicar o GameObject Player.
- Trocar as KeyCodes do `PlayerInputReader` no segundo (e.g. WASD em vez de setas).
- Cada `PlayerFSM` tem seu próprio `_entityId` automaticamente (static counter).
- O `HitboxResolver` já filtra `attackerId == victimId` (não bate em si mesmo). Mas dois jogadores podem se bater — adicionar field `Team` em `PlayerFSM` quando isso virar problema.

---

## Performance budgets (do `levantamento_e_diretrizes.md` §3)

| Recurso                              | Target                              |
|--------------------------------------|--------------------------------------|
| FPS                                  | 60 estável                           |
| Tick rate                            | 60Hz fixo                            |
| Entidades simultâneas                | 200-500 (com VFX leves)              |
| AI think rate (swarm)                | 10-20Hz (não 60Hz)                   |
| Replicação (futura, swarm)           | 10-20Hz, snapshots compactos         |

Se uma feature ameaçar esses budgets, **sinaliza antes de implementar**.

---

## O que eu posso pedir e o que NÃO posso pedir

### Posso pedir
- "Crie uma novo move para o Bandit chamado X"
- "Adicione uma feature Y ao FSM"
- "Refatore Z pra ser mais X"
- "Crie uma cena de teste com 100 inimigos"
- "Implemente um boss baseado em CharacterDefinition"

### NÃO posso pedir (sem discussão prévia)
- "Coloque tudo num Animator" — fere regra 11
- "Use OnTriggerEnter2D" — fere regra 9
- "Spawne 500 GameObjects de inimigo" — fere regra 10
- "Carregue tudo via Resources.Load" — fere regra 8
- "Copie o moveset do Davis do LF2" — fere regra 3

Quando eu pedir algo que viola uma regra, **me lembre** e proponha a alternativa.

---

## Ferramentas de inspeção

- `tools/prepare_lf2_assets.py` — converte BMPs uma vez por update do LF2.
- `tools/decode_lf2_dat.py` — peeks num `.dat` decodado pra entender (não pra parsear).

---

## Quando estiver na dúvida

1. Releia o doc relevante (`levantamento_e_diretrizes.md` pra arquitetura, `lf2_assets_guide.md` pra assets, `architecture_walkthrough.md` pra scripts).
2. Se a resposta não está lá, **pergunte ao humano**. Não chute.
