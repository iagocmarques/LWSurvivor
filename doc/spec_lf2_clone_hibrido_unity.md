# SPEC Mestre — Base híbrida LF2 → novo jogo (Unity 6)

**Objetivo:** construir uma base técnica que reproduza o núcleo do **Little Fighter 2** com alta fidelidade operacional, mas já estruturada para evoluir para o jogo final pretendido.  
**Estratégia:** primeiro recriar o “motor LF2-like” de forma data-driven; depois expandir com novos sistemas, conteúdo, multiplayer e loop de progressão.

---

## 1) Resumo executivo

### Tese do projeto
O caminho com menor risco agora é:
1) **copiar o LF2 em termos de motor, estados, ações, hitboxes e fases**,  
2) **importar o máximo possível da estrutura original** (dados, spritesheets, stages, backgrounds, armas, projéteis),  
3) e só então **transformar essa base** no novo jogo.

### Princípios não-negociáveis
- **Unity 6 + URP 2D**
- **Tick fixo 60Hz**
- **Tudo data-driven**
- **Sem lógica por personagem**
- **Mirror/facing matemático**, não por `localScale = -1`
- **Input buffering** e combate por timeline de frames
- **Importadores editor-only** para LF2 (`.dat`, BMPs, stages, BGs)

### Resultado esperado
Uma aplicação Unity que seja capaz de:
- reproduzir personagens, ações, reações, especiais e armas do LF2,
- carregar stages e backgrounds inspirados/convertidos do LF2,
- executar IA e spawns semelhantes,
- suportar evolução posterior para co-op online, hordas, progressão e conteúdo autoral.

---

## 2) Arquitetura alvo (visão geral)

### 2.1 Camadas do sistema

```text
App Layer
├─ Bootstrap / Scene Flow / Save / Settings / Localization
├─ Game Modes (Versus, Stage, Survival, Training, Demo/Replay)
├─ Match Runtime (spawn, camera, round rules, wave rules)
├─ Character Runtime
│  ├─ FSM macro
│  ├─ Move Runner (timeline LF2-like)
│  ├─ Motor 2.5D
│  ├─ Hit/Hurt system
│  ├─ Weapon system
│  ├─ Status effects
│  └─ AI brain
├─ Content Runtime
│  ├─ CharacterDefinition
│  ├─ MoveDefinition
│  ├─ WeaponDefinition
│  ├─ ProjectileDefinition
│  ├─ StageDefinition
│  └─ BackgroundDefinition
└─ Import/Editor Tooling
   ├─ LF2 Dat decryptor/parser
   ├─ BMP slicer / alpha pipeline
   ├─ Character importer
   ├─ Stage importer
   ├─ Background importer
   └─ Validators/report builders
```

### 2.2 Pastas recomendadas

```text
Assets/_Project/
  Core/
  Gameplay/
    Characters/
    Combat/
    Weapons/
    Projectiles/
    AI/
    Modes/
    Camera/
  Data/
    Characters/
    Weapons/
    Projectiles/
    Stages/
    Backgrounds/
  UI/
  Audio/
  Tools/
    LF2Importer/
  Tests/

Assets/_Imported/LF2/
  Characters/
  Weapons/
  Projectiles/
  Stages/
  Backgrounds/
  Reports/
```

---

## 3) Base técnica recomendada

## 3.1 Engine e pacotes

### Obrigatórios
- **Unity 6**
- **URP 2D**
- **Input System**
- **Unity Test Framework**

### Recomendados
- **Addressables** para conteúdo importado/expansões futuras. A própria documentação da Unity recomenda Addressables para conteúdo empacotado/remoto e atualização incremental de grupos/catálogos. [1][2]
- **PlayerInput / Input Actions** do novo Input System para padronizar teclado/gamepad e separar bindings de gameplay e UI. A documentação oficial destaca `PlayerInput` como wrapper de alto nível para wiring rápido de input. [3][4]

### Opcionais
- **2D Animation / PSD Importer** apenas para conteúdo autoral futuro com rigs; não é prioridade para o clone LF2 porque o LF2 é sprite-per-frame. A Unity posiciona esse pacote para workflows de assets em layers/PSB, não como requisito para spritesheets estáticos. [5][6]

### Multiplayer (posterior)
- **Steamworks ISteamNetworkingSockets + SDR** para o futuro co-op, quando a base offline estiver estável. A documentação oficial destaca P2P com relay via rede da Steam. [7]

---

## 4) Modelo de jogo que deve ser copiado primeiro

### 4.1 Plano de fidelidade
Copiar primeiro estes pilares:
1) Personagens e actions frames
2) Sistema de hit/hurt/knockback/fall
3) Defesa e reações
4) Armas leves e pesadas
5) Projéteis e especiais
6) Stage mode / Survival mode
7) AI básica dos inimigos
8) Backgrounds e limites jogáveis

### 4.2 Modos de jogo a implementar

#### Fase 1 (mínimo)
- **Versus local**
- **Stage mode**
- **Survival**
- **Training**

#### Fase 2
- **Demo/attract mode**
- **Replay mínimo**
- **Co-op local**

#### Fase 3
- **Online host-client**
- **Loop híbrido com progressão**

---

## 5) Personagens — arquitetura de runtime

### 5.1 Modelo geral
Cada personagem é um conjunto de:
- `CharacterDefinition`
- `ReactiveMoveSet`
- `MoveDefinitions` ativas
- parâmetros de movimento
- tabela de importação dos frames LF2

### 5.2 Macro estados obrigatórios
- `Grounded`
- `Attacking`
- `Defending`
- `HurtGrounded`
- `HurtAir`
- `Lying`
- `GetUp`
- `Jumping`
- `Dashing`
- `Dead`

### 5.3 Três camadas de execução
1) **FSM macro**
2) **Move timeline**
3) **Eventos por frame**

### 5.4 Estruturas mínimas

```csharp
struct HitResult {
    int damage;
    Vector3 knockback;
    int fallValue;
    int guardBreakValue;
    int effect; // 0 none, 1 blood, 2 burn, 3 freeze
    int attackerFacing; // +1 / -1
    bool isProjectile;
    bool isHeavy;
}

class CharacterDefinition : ScriptableObject {
    int lf2Id;
    string displayName;
    CharacterMovementConfig movement;
    List<MoveDefinition> activeMoves;
    ReactiveMoveSet reactiveMoves;
}

class MoveDefinition : ScriptableObject {
    string moveName;
    int startFrameId;
    List<FrameData> frames;
    MoveKind moveKind;
}

class FrameData {
    int frameId;
    int pic;
    int state;
    int waitTicks;
    int nextFrameId;
    float dvx, dvy, dvz;
    bool lockInput;
    bool invulnerable;
    List<HurtBoxData> bdy;
    List<HitBoxData> itr;
    List<SpawnPointData> opoints;
    string soundPath;
}

class ReactiveMoveSet {
    MoveDefinition defend;
    MoveDefinition defendHit;
    MoveDefinition defendBreak;
    MoveDefinition hurtGrounded;
    MoveDefinition hurtAir;
    MoveDefinition lying;
    MoveDefinition getUp;
    MoveDefinition dead;
}
```

---

## 6) Correlação com `lf2_manual_personagens_completo.md`

### 6.1 Ações ativas
No manual, os campos `hit_*` apontam para o **startFrame** dos golpes:
- `hit_a`, `hit_j`, `hit_d`
- `hit_Fa`, `hit_Fj`
- `hit_Ua`, `hit_Uj`
- `hit_Da`, `hit_Dj`
- `hit_ja`

Cada `hit_*: N` vira:
- comando de entrada
- `MoveDefinition.startFrameId = N`
- sequência = seguir `next` até `999` ou loop

### 6.2 Ações reativas
As ações reativas não vêm por input, e sim por **`state`** + **grafo `next`**.

Mapeamento recomendado:
- `state 7` → Defending
- `state 8` → DefendBreak
- `state 11` → HurtGrounded
- `state 12` → HurtAir
- `state 14` → Lying
- `state 16` → Faint

### 6.3 Heurística para achar start frames reativos
Para cada personagem:
1) reunir frames por `state`,
2) encontrar os que **não são `next` de outro frame do mesmo state**,
3) usar isso como raiz provável do move reativo.

Isso deve ser automatizado pelo importador e salvo em relatório.

---

## 7) Combate — regras centrais

### 7.1 Dano e reação
Todo dano entra por `ApplyHit(HitResult hit)` e delega para `DamageReactionRouter`.

### 7.2 Prioridade de reação
1) Dead
2) DefendBreak
3) HurtAir
4) HurtGrounded
5) DefendHit

### 7.3 Router (regras default)
- Se `HP <= 0` → `Dead`
- Se `Defending` e `guardBreakValue >= threshold` → `DefendBreak`
- Se airborne ou launch vertical significativo → `HurtAir`
- Senão → `HurtGrounded`

### 7.4 Hitstop / feedback
- `itr.injury` e flags do golpe definem hitstop
- `effect` define burn/freeze/blood
- screen shake moderado por golpe forte/especial

---

## 8) Mirror / facing (obrigatório em todo runtime)

### Regra visual
- `SpriteRenderer.flipX` ou equivalente

### Regra de caixas
- Retângulo: `x' = frameWidth - (x + w)`
- Ponto: `x' = frameWidth - x`

### Regra de velocidades
- Se `dvx` é aplicada diretamente, inverter sinal em facing esquerda
- Se o motor usa `direction * speed`, não inverter `dvx` bruto duas vezes

### Requisito
Mirror deve funcionar em:
- ataques
- hurtboxes
- opoints
- estados reativos
- armas
- projéteis

---

## 9) Motor 2.5D (movimento e física)

### Eixos
- `X` = horizontal
- `Y` = profundidade no plano
- `Z` = opcional só para ordenação/engine, não para gameplay

### Física
- Não usar Rigidbody 2D como autoridade principal do personagem.
- Usar `CharacterMotor` custom com:
  - posição no plano
  - velocidade horizontal/profundidade
  - altura/vertical interna para saltos/falls (pode ser um eixo separado lógico)

### Regras
- Tick fixo 60Hz
- Gravidade definida por sistema, não pelo Animator
- `dvx/dvy/dvz` dos frames influenciam motor por tick

---

## 10) Armas

### 10.1 Tipos a suportar
- **Light weapons**
- **Heavy weapons**
- **Thrown weapons**
- **Broken weapons**

### 10.2 Fonte de verdade
Armas e seus frames vêm de `.dat` próprios (`weapon*.dat`) e listas como `weapon_strength_list`, além de states específicos (1000, 1001, 1002 etc.) visíveis em arquivos como `Weapon2.dat`.

### 10.3 Requisitos
- Pegar
- Segurar
- Atacar com arma
- Arremessar
- Cair/quebrar

### 10.4 Data model

```csharp
class WeaponDefinition : ScriptableObject {
    int lf2Id;
    WeaponType type;
    int hp;
    int dropHurt;
    List<WeaponStrengthEntry> strengths;
    List<FrameData> frames;
}
```

---

## 11) Projéteis e especiais

### 11.1 Origem
Especiais usam `opoint` + `oid`, resolvidos via `data.txt`.

### 11.2 Tipos
- Ball linear
- Ball com chase
- Wind
- Explosion
- Spawned helper/clone

### 11.3 Runtime
- `ProjectileDefinition`
- `ProjectileRunner`
- Colisão com personagens / projéteis / cenário

### 11.4 Campos importantes importados
- `dvx/dvy`
- `state 3000+`
- `hit_Fa` em balls que habilitam chase (referenciado em tutoriais da comunidade)

---

## 12) IA dos inimigos

### 12.1 Filosofia
Não usar BT complexo no começo.  
Implementar **IA de utilidade simples + FSM**.

### 12.2 Estados mínimos de AI
- Idle
- SeekTarget
- Approach
- Attack
- Retreat
- Defend
- Recover

### 12.3 Sensores
- distância horizontal
- distância em profundidade
- alvo visível
- alvo em stun/lying
- cooldowns

### 12.4 Regras por arquétipo
- **Bandit/Hunter**: agressão baixa, rush simples
- **Jack/Mark/Monk/etc.**: specials básicos, distância média
- **Bosses**: fase de ataque, summon/projétil, cadência menor

### 12.5 Requisito técnico
AI “think” roda a 10–20Hz, não a 60Hz.

---

## 13) Fases / `stage.dat`

### 13.1 O que copiar
`stage.dat` define:
- `stage id`
- múltiplas `phase`
- `bound`
- `music`
- listas de spawn por `id/hp/times/ratio`

### 13.2 Como modelar

```csharp
class StageDefinition : ScriptableObject {
    int lf2StageId;
    string displayName;
    List<StagePhaseDefinition> phases;
}

class StagePhaseDefinition {
    int bound;
    string musicPath;
    List<StageSpawnEntry> entries;
}

class StageSpawnEntry {
    int objectId;
    int hpOverride;
    int times;
    float ratio;
    SpawnRole role; // Boss, Soldier, Item
}
```

### 13.3 Uso no jogo
- Stage mode
- Survival
- Base para futuros modos de horda

---

## 14) Cenários / backgrounds

### 14.1 Fonte
Background `.dat` + seção `<Background>` do `data.txt`.

### 14.2 O que copiar
- `name`
- `width`
- `zboundary`
- `shadow`, `shadowsize`
- até 30 `layer`
- `loop`
- `cc/c1/c2` (animação por tempo)

### 14.3 Como modelar no Unity

```csharp
class BackgroundDefinition : ScriptableObject {
    int lf2BackgroundId;
    string displayName;
    int width;
    Vector2Int zBoundary;
    string shadowPath;
    Vector2Int shadowSize;
    List<BackgroundLayerDefinition> layers;
}
```

### 14.4 Recomendação prática
Importar BGs para referência e fidelidade, mas construir um runtime de parallax/layers no Unity, não tentar “executar o BG do LF2” literalmente.

---

## 15) Pipeline de migração do jogo original → nossa app

## 15.1 Escopo da migração

### Importar do original
- `data.txt`
- `.dat` de personagens
- `.dat` de armas
- `.dat` de projéteis
- `stage.dat`
- `.dat` de backgrounds
- BMPs / WAVs

### Converter
- BMP → PNG com alpha
- DAT criptografado → texto parseado
- IDs → registries internos

## 15.2 Importadores necessários

### A) `LF2 Dat Decryptor`
- remove header 123 bytes
- decripta com chave clássica

### B) `Character Importer`
- parse de `<bmp_begin>`, movement params, frames, `bdy`, `itr`, `opoint`
- gera Sprites, AnimationClips, `CharacterDefinition`

### C) `Weapon Importer`
- parse de frames de armas e strengths

### D) `Projectile Importer`
- parse de balls e efeitos

### E) `Stage Importer`
- parse de `stage.dat`

### F) `Background Importer`
- parse de BG layers e bounds

### G) `Report Generator`
- lista warnings: pic fora de range, `next` inválido, `oid` não resolvido

---

## 16) Pipeline de assets gráficos

### 16.1 Regra geral
- Não usar `_mirror.bmp`
- Espelhar em runtime
- Converter BMPs para PNG com alpha

### 16.2 Regras técnicas
- Sprite import: Point, sem compressão
- Usar **w/h do `.dat`**, não hardcoded 80
- Transparência com tolerância, não igualdade exata, ao converter BMP

### 16.3 Bibliotecas/ferramentas úteis
- Python + Pillow para conversão/alpha pipeline
- Unity Sprite Editor para validação
- Addressables para conteúdo importado se o volume crescer [1][2]

---

## 17) Input

### 17.1 Biblioteca
Usar o **Input System** da Unity com `Input Actions` e `PlayerInput`. [3][4]

### 17.2 Regras
- Input buffering de 6–8 frames
- Mapear comandos LF2:
  - A
  - J
  - D
  - D+→+A
  - D+→+J
  - D+↑+A
  - D+↑+J
  - D+↓+A
  - D+↓+J

### 17.3 Separação
- Action map de gameplay
- Action map de UI

---

## 18) Câmera

### Requisitos
- Frame de luta comportado (como LF2)
- Respeitar largura do cenário
- Priorizar legibilidade

### Recomendação
- câmera 2D com bounds
- lookahead leve no eixo X
- sem zoom dinâmico agressivo na fase 1

---

## 19) UI / HUD

### Mínimo
- HP/MP
- Round/phase
- ícones pequenos (`small:` do LF2 como referência/import)
- Training info/debug

### Futuro
- HUD de co-op
- seleção de personagem
- stage/mode selection

---

## 20) Save / config

### Salvar
- bindings
- opções gráficas
- progresso de unlocks (quando houver)
- flags de debug/import

### Não misturar
- save de gameplay
- base importada do LF2

---

## 21) Testes e qualidade

### Framework
Usar **Unity Test Framework** com:
- **Edit Mode tests** para parsers/importers
- **Play Mode tests** para runtime e combate. A documentação oficial separa claramente os dois modos e destaca PlayMode para código runtime/coroutines. [8][9]

### Testes obrigatórios

#### Importadores
- decriptação de `.dat`
- parse de `data.txt`
- parse de frames/personagens
- parse de `stage.dat`
- parse de BG `.dat`

#### Runtime
- hit/hurt
- defend/guard break
- hurt air → lying → getup
- espelhamento
- armas
- projéteis

---

## 22) Roadmap recomendado

### Fase 0 — Infra/import
- Decryptor
- Character importer
- Sprite pipeline
- Report generator

### Fase 1 — Clone jogável mínimo
- 1 stage
- 4 personagens
- 2 armas
- hurt/defend/knockdown
- AI básica

### Fase 2 — Clone robusto
- todos personagens originais
- stage mode + survival
- bosses
- backgrounds importados

### Fase 3 — Base híbrida
- refatorar para suportar progressão nova
- waves híbridas
- habilidades novas

### Fase 4 — Novo jogo
- conteúdo autoral
- multiplayer
- meta-progressão

---

## 23) Backlog técnico inicial (ordem sugerida)

1) Fixar pipeline BMP→PNG com alpha confiável  
2) Terminar importador de personagens  
3) Implementar `MoveRunner` LF2-like  
4) Implementar `DamageReactionRouter`  
5) Implementar armas  
6) Implementar projéteis  
7) Importar `stage.dat`  
8) Importar backgrounds  
9) Implementar AI simples  
10) Fechar versus + stage + survival  

---

## 24) Decisões de arquitetura que o orquestrador deve manter

### Deve fazer
- manter tudo data-driven
- usar o manual LF2 como fonte de verdade do comportamento
- separar importador de runtime
- testar importador com EditMode
- testar combate com PlayMode

### Não deve fazer
- hardcode por personagem
- usar localScale -1 para mirror
- amarrar lógica ao Animator como autoridade
- misturar sistema “novo” com exceções LF2 sem camada de abstração

---

## 25) Bibliotecas e referências úteis

### Oficiais Unity / Steam
- Unity Input System / PlayerInput [3][4]
- Unity Addressables [1][2]
- Unity 2D Animation / PSD Importer (para pipeline futuro autoral) [5][6]
- Steamworks `ISteamNetworkingSockets` [7]
- Unity Test Framework [8][9]

---

## 26) Fontes

[1] Unity Addressables getting started — https://docs.unity3d.com/Packages/com.unity.addressables@1.12/manual/AddressableAssetsGettingStarted.html  
[2] Unity Addressables FAQ — https://docs.unity.cn/Packages/com.unity.addressables@1.20/manual/AddressablesFAQ.html  
[3] Unity Input System components / PlayerInput — https://docs.unity.cn/Packages/com.unity.inputsystem@1.4/manual/Components.html  
[4] Unity PlayerInput API — https://docs.unity3d.com/Packages/com.unity.inputsystem@1.8/api/UnityEngine.InputSystem.PlayerInput.html  
[5] Unity PSD Importer properties — https://docs.unity3d.com/Packages/com.unity.2d.psdimporter@6.1/manual/PSD-importer-properties.html  
[6] Unity 2D Animation / Preparing Artwork — https://docs.unity3d.com/Packages/com.unity.2d.animation@9.2/manual/PreparingArtwork.html  
[7] Steamworks ISteamNetworkingSockets — https://partner.steamgames.com/doc/api/ISteamNetworkingSockets  
[8] Unity Test Framework — Edit mode vs Play mode tests — https://docs.unity3d.com/6000.3/Documentation/Manual/test-framework/edit-mode-vs-play-mode-tests.html  
[9] Unity automated tests overview — https://unity.com/how-to/automated-tests-unity-test-framework  

