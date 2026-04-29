# LF2 Unity Kit — pacote completo

Pacote de scripts, ferramentas e docs pra arrancar um beat 'em up 2.5D em Unity 6 + URP 2D usando os assets do Little Fighter 2 como referência.

## O que tem aqui

```
lf2_unity_kit_full/
├── README.md                             ← você está aqui
├── CURSOR_BRIEF.md                       ← cole no .cursorrules do projeto
│
├── docs/
│   ├── lf2_assets_guide.md               ← convenções dos assets do LF2 (chroma key, naming, etc.)
│   ├── architecture_walkthrough.md       ← como os scripts do Runtime/ se conectam
│   └── bandit_authoring_guide.md         ← passo a passo: do BMP cru até Bandit jogável
│
├── tools/
│   ├── prepare_lf2_assets.py             ← BMP → PNG (com alpha correto)
│   └── decode_lf2_dat.py                 ← decoder dos .dat (humano-only, NÃO usa em runtime)
│
└── unity_scripts/
    ├── Editor/
    │   └── LF2SpriteImporter.cs          ← AssetPostprocessor: auto-config sprite import
    └── Runtime/
        ├── Tick/
        │   └── TickRunner.cs             ← driver de simulação 60Hz + ITickable
        ├── Input/
        │   └── PlayerInputReader.cs      ← InputBuffer (8 frames) + leitor de teclado
        ├── Movement/
        │   └── CharacterController2_5D.cs ← Position2_5D + controller com gravidade
        ├── Combat/
        │   ├── HitFeel.cs                ← hitstop global + screen shake (trauma-based)
        │   ├── HitboxResolver.cs         ← resolução pareada de hits no tick
        │   └── PlayerFSM.cs              ← state machine do jogador
        ├── Data/
        │   ├── CharacterDefinition.cs    ← SO de personagem
        │   ├── MoveDefinition.cs         ← SO de move + FrameDefinition + HitboxDefinition + enums
        │   ├── EnemyDefinition.cs        ← SO de inimigo (swarm-friendly)
        │   └── Registry.cs               ← lookup por ID
        ├── Pooling/
        │   └── ObjectPool.cs             ← GenericPool<T> + PrefabPool
        ├── Swarm/
        │   └── SwarmManager.cs           ← horde controller (struct array, 500 inimigos)
        └── Bootstrap/
            ├── BootstrapInstaller.cs     ← inicialização da cena
            └── PixelPerfectCameraSetup.cs ← URP 2D + PPU=80
```

## Onde colocar cada arquivo no seu projeto Unity

```
<repo_root>/
├── tools/                                ← copie tudo de lf2_unity_kit_full/tools/ aqui
├── docs/                                 ← copie tudo de lf2_unity_kit_full/docs/ aqui
├── CURSOR_BRIEF.md                       ← raiz do repo (vira .cursorrules ou referência)
├── levantamento_e_diretrizes.md          ← (você já tem)
└── unity/
    └── Assets/
        ├── Editor/
        │   └── LF2SpriteImporter.cs      ← copie aqui
        ├── Game/
        │   └── Runtime/                  ← copie a árvore inteira de unity_scripts/Runtime/ aqui
        ├── Art/
        │   └── lf2_ref/                  ← saída do prepare_lf2_assets.py (gerado)
        └── Game/Characters/<Name>/       ← seus CharacterDefinition + Moves (autoral)
```

## Quickstart (do zero ao Bandit jogável)

### 1. Convencer a Unity dos assets do LF2

```bash
# extraia o LittleFighter.rar em algum lugar (não precisa estar no Assets/)
unar LittleFighter.rar

# converta os BMPs pra PNG com alpha
cd <repo_root>
python tools/prepare_lf2_assets.py \
  --src LittleFighter/ \
  --dst unity/Assets/Art/lf2_ref/
```

Saída esperada:
- `characters: 183 converted, 138 skipped, 0 failed`
- `backgrounds: 143 converted, 2 failed`

### 2. Abrir a Unity

O `LF2SpriteImporter.cs` (em `Assets/Editor/`) detecta os PNGs automaticamente e configura: Filter Point, Compression None, PPU=80, slice 10×7 (em sheets 800×560), pivô bottom-center.

### 3. Autorar o Bandit

Siga `docs/bandit_authoring_guide.md`. ~30 minutos de cliques no Inspector pra ter o Bandit andando + socando.

### 4. Iterar

Tunar números no `MoveDefinition` no inspector enquanto o jogo tá em Play. Combat feel é 80% iteração.

## Princípios do projeto (resumo dos docs)

1. **Conteúdo é dado, não código.** Adicionar personagem/move/inimigo = duplicar SO + ajustar números.
2. **Tick fixo 60Hz.** Tudo que afeta combat roda em `ITickable.Tick(int)`. Visual pode rodar a 144Hz com interpolação via `TickRunner.Alpha`.
3. **Resolução central de hits.** Tudo passa por `HitboxResolver` → fácil de debuggar, fácil de net-syncar.
4. **Pool agressivo.** Spawn/despawn em hot loop sem alocar.
5. **LF2 é referência, não codex.** Não copie números, hitboxes, nem nomes verbatim.

## Próximos passos sugeridos (em ordem)

1. **Validar pipeline**: rodar o quickstart até ter o Bandit andando na cena de teste.
2. **Adicionar 1 inimigo de swarm** (e.g. um "Bandit Lite" com 10 hp, andando em direção ao player).
3. **Spawnar 100 inimigos** num timer pra validar a meta de 200-500 entidades.
4. **Adicionar combo de 3 socos** (Bandit_Punch1 → Punch2 → Punch3 com cancels).
5. **Pular** (move com `impulse.z = 12` no primeiro frame).
6. **Background com parallax** (ler `Assets/Art/lf2_ref/backgrounds/template/1/pic*.png` em camadas).
7. **Telemetria simples** de hits (CSV log) pra começar a ver o balance.

A partir dali, a roadmap vira a do `levantamento_e_diretrizes.md` §9.

## Limites / o que NÃO está implementado

- **Netcode**: zero. A arquitetura prepara o terreno (tick fixo + submissão central de hits + IDs por entidade) mas a integração Steamworks fica pra depois.
- **UI/menu**: zero. Adicionar quando tiver gameplay loop fechado.
- **Save / unlocks persistentes**: zero. Será JSON no `Application.persistentDataPath` quando chegar a hora.
- **Audio mixing**: usa `AudioSource.PlayClipAtPoint` simplão. Migra pra um AudioBus quando tiver categorias (SFX, music, voice).
- **Localização**: zero.
- **Controle por gamepad**: zero (legacy `Input.GetKey` só lê teclado). Migra pro Input System novo quando importar.

Cada um desses tem um TODO comentado nos arquivos relevantes.

## Suporte

Lê os docs primeiro. Se ainda tem dúvida, pergunta humanamente — não deixa o LLM chutar.
