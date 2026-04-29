# Guia: Usar arte e file system do LF2 no nosso Unity (briefing pro Cursor)

> **Objetivo deste documento**: dar contexto completo e regras práticas pra o Cursor (ou qualquer LLM de código) trabalhar com os assets do Little Fighter 2 dentro do nosso projeto Unity 6 + URP 2D, sem alucinar e sem chutar formato.

---

## TL;DR (leia isto primeiro)

1. **As BMPs do LF2 são utilizáveis** mas precisam de pré-processamento. A cor preta (RGB 0,0,0) é o **chroma key transparente** — sem converter, todo personagem aparece com fundo preto na Unity (foi exatamente o que aconteceu na nossa screenshot).
2. **As `.dat` do LF2 são obfuscadas** (algoritmo de subtração com chave + skip de 123 bytes em cada ponta). Mesmo decodadas, o formato é proprietário e não-versionado. **Não vamos parsear `.dat` no nosso projeto** — é frágil, é trabalho enorme, e o nosso `levantamento_e_diretrizes.md` já diz que o conteúdo deve ser data-driven em ScriptableObjects autorais nossos.
3. **Caminho recomendado**: rodar uma vez o script de pré-processamento (`prepare_lf2_assets.py`) que converte todas as BMPs pra PNG com alpha correto e organiza tudo em `Assets/Art/lf2_ref/`. Depois, autorar `CharacterDefinition`/`MoveDefinition` em ScriptableObjects nossos, **usando os sprites como referência/placeholder** até termos arte original.
4. **Aviso de IP** (do nosso próprio `levantamento_e_diretrizes.md`, seção 10.8): "não copiar sprites/nomes/moves específicos; 'inspirado', não clonado". Os assets do LF2 servem pra **prototipar e testar feel**, mas devem ser substituídos antes de qualquer release público.

---

## 1. O que tem dentro de `LittleFighter/`

```
LittleFighter/
├── lf2.exe              # binário do jogo — ignorar
├── Uninstal.exe         # ignorar
├── readme.txt           # versão 2.0a, julho/2009 (Marti Wong)
├── data/                # .dat (obfuscados) + .wav + 3 .txt unencrypted
│   ├── data.txt         # MAPA de IDs → arquivos. NÃO é encrypted.
│   ├── *.dat            # 67 arquivos. Personagens, projéteis, armas. Encrypted.
│   ├── *.wav            # 200+ efeitos sonoros. Padrão WAV PCM. Diretamente usáveis.
│   └── _ad*.txt         # ads — irrelevante pra gameplay
├── sprite/
│   ├── sys/             # 321 BMPs — sprites de personagens, projéteis, ícones
│   └── template1/       # template em branco pra modders
├── bg/
│   ├── sys/             # backgrounds oficiais (lf, hkc, qi, etc.)
│   └── template/        # 3 backgrounds template
├── bgm/                 # músicas (.mp3 / .wav)
└── recording/           # replays — ignorar
```

### O que importa pra nós (em ordem)

| Item                       | Status             | Ação                                   |
|----------------------------|--------------------|----------------------------------------|
| `sprite/sys/*.bmp`         | ✅ usável           | Converter via script (alpha)           |
| `bg/sys/*/`, `bg/template/`| ✅ usável           | Converter via script (alpha condicional)|
| `data/*.wav`               | ✅ usável           | Copiar direto pra Unity                |
| `data/data.txt`            | ✅ usável           | Ler como referência humana             |
| `data/*.dat`               | ⚠️ obfuscado/IP gray| **Ignorar**. Re-autorar em SO nossos   |
| `bgm/*.mp3`                | ❌ não usar         | IP de terceiros, substituir            |

---

## 2. Convenções dos sprites do LF2

### 2.1. Naming

Cada personagem tem o conjunto:

```
sprite/sys/
├── <name>_0.bmp           # spritesheet principal (frames 0-69)
├── <name>_1.bmp           # spritesheet 2 (frames 70-139), opcional
├── <name>_2.bmp           # spritesheet 3 (frames 140+), opcional
├── <name>_f.bmp           # face/portrait 120x120 (sem transparência)
├── <name>_s.bmp           # selection icon 40x45 (sem transparência)
├── <name>_*_mirror.bmp    # versões pré-espelhadas (esquerda)
└── <name>_ball*.bmp       # spritesheets dos projéteis do char (se houver)
```

### 2.2. Layout do spritesheet

| Propriedade            | Valor                                  |
|------------------------|----------------------------------------|
| Tamanho da folha       | **800 × 560 pixels**                   |
| Grid                   | **10 colunas × 7 linhas = 70 frames**  |
| Tamanho de cada célula | **80 × 80 pixels**                     |
| Sprite real por frame  | até **79 × 79** (1px de gutter)        |
| Cor de transparência   | **RGB(0, 0, 0)** — preto puro          |
| Formato                | BMP indexed-palette (mode `P`)         |

A enumeração dos frames é linear, da esquerda pra direita, de cima pra baixo:

```
Frame 0   Frame 1   ... Frame 9
Frame 10  Frame 11  ... Frame 19
...
Frame 60  Frame 61  ... Frame 69
```

Se um char usa `_1.bmp`, os frames continuam: 70 = (0,0) de `_1.bmp`, 71 = (1,0), etc.

### 2.3. Sprites espelhados (`_mirror.bmp`)

**IGNORAR.** O LF2 tem versões pré-flipadas porque o engine deles (DirectDraw legado) não fazia mirror via shader. Na Unity é mais simples espelhar o `Transform.localScale.x = -1` ou usar `SpriteRenderer.flipX`. Esses arquivos só ocupam espaço.

### 2.4. Pivô

LF2 usa pivô **center-bottom** dos personagens (os pés no chão, simetricamente centrado). Em coordenadas locais da célula 80×80 isso é:

```
pivot = (centerx, centery) = (39 a 40, 79)
```

Na Unity, ao slicear, configure pivô como `Bottom Center` (que é (0.5, 0)).

### 2.5. Faces (`_f.bmp`) e ícones (`_s.bmp`)

- **Não têm transparência** (são imagens cheias).
- 120×120 e 40×45 fixos.
- Usar como portrait na UI / seleção de personagem.

---

## 3. Convenções dos backgrounds

Estrutura típica de uma fase:

```
bg/<theme>/<stage>/
├── bg.dat                 # METADADOS DA FASE (encrypted) — não vamos parsear
├── pic1.bmp ... pic8.bmp  # camadas (parallax). Ordem importa.
├── shadow.bmp / s.bmp     # sombra que fica sob os personagens
├── land1.bmp ... land4.bmp# tiles do chão (em alguns temas)
└── forest*.bmp            # elementos especiais (em alguns temas)
```

- Cada `picN.bmp` é uma camada de parallax. O `bg.dat` (encrypted) define posição Y, velocidade, repetição. Sem ele, vamos **definir nossas próprias `StageDefinition`** em ScriptableObject.
- Tamanhos variam: `pic1` costuma ser 800×145 (skyline), `pic7/pic8` 800×115, etc. Nada uniforme.
- Cor de transparência dos backgrounds **não é sempre preta**: alguns usam o pixel top-left como chroma key. O script de pré-processamento detecta isso heuristicamente.

---

## 4. O sistema de IDs do LF2 (`data/data.txt`)

Esse arquivo é a única fonte legível de "que arquivos compõem o jogo". Estrutura:

```
<object>
id:  0   type: 0  file: data\template.dat
id: 30   type: 0  file: data\bandit.dat
id: 11   type: 0  file: data\davis.dat
id: 100  type: 1  file: data\weapon0.dat   #stick
id: 200  type: 3  file: data\john_ball.dat
...
```

**Tipos de objeto** (`type:`):

| Type | Significado          |
|------|----------------------|
| 0    | Personagem           |
| 1    | Arma de impacto      |
| 2    | Arma jogável (pedra) |
| 3    | Projétil             |
| 4    | Arma de baseball     |
| 5    | Inimigo especial     |
| 6    | Pickup (cerveja/leite)|

A gente vai **espelhar essa lista** mas com **nossos próprios IDs e nossos próprios `ScriptableObject`**. Isso vai virar um `Registry` (uma das estruturas mencionadas no `levantamento_e_diretrizes.md`).

---

## 5. Sobre os `.dat` (e por que não vamos parsear eles)

### 5.1. O que tem dentro

Cada `.dat` de personagem é um arquivo de texto (em ASCII) com a estrutura:

```
<bmp_begin>
name: Bandit
head: sprite/sys/bandit_f.bmp
small: sprite/sys/bandit_s.bmp
file(0-69): sprite/sys/bandit_0.bmp  w: 79  h: 79  row: 10  col: 7
file(70-139): sprite/sys/bandit_1.bmp ...
walking_frame_rate 3
walking_speed 5
running_speed 8
heavy_walking_speed 4.5
jump_height -16.3
weapon_strength_list ...
<bmp_end>

<frame> 0 standing
   pic: 0  state: 0  wait: 3  next: 999  dvx: 0  dvy: 0  centerx: 39  centery: 79  hit_a: 0  hit_d: 0  hit_j: 0
   bdy:
      kind: 0  x: 28  y: 12  w: 25  h: 67
   bdy_end:
<frame_end>

<frame> 1 standing
   pic: 1  state: 0  wait: 3  next: 0  ...
   itr:                          # hitbox de ataque
      kind: 0  x: 38  y: 32  w: 54  h: 21  dvx: 5  fall: 20  vrest: 7
   itr_end:
<frame_end>
... [200+ frames por personagem]
```

### 5.2. A obfuscação

- **Algoritmo**: pula 123 bytes do começo e 123 do fim, depois subtrai byte a byte uma chave repetida `"odBearBecauseHeIsVeryGoodSiuMan!"` (32 chars).
- Versões: confirmado pra v2.0a; pode haver pequenas variações em outras versões.
- Há um decoder Python parcial em `tools/decode_lf2_dat.py` (incluído neste pacote, mas use só pra inspecionar — não faz parte do pipeline).

### 5.3. Por que NÃO vamos parsear

1. **Arquitetura**: nosso `levantamento_e_diretrizes.md` define explicitamente `CharacterDefinition`, `MoveDefinition`, `HitboxDefinition` como ScriptableObjects autorais. Importar `.dat` cria uma dependência morta nesse formato.
2. **IP**: copiar movesets, frame-counts, valores de dvx/dvy, hitbox kinds verbatim do LF2 é **clonagem** — exatamente o que nossa seção 10.8 diz pra evitar.
3. **Trabalho**: parsear `.dat` direito requer resolver state machines de 50+ estados (`state:`), 14 tipos de `itr` (interaction), 9 tipos de `bdy` (body), o sistema de `wpoint`/`opoint`/`cpoint`, e mapear tudo pra Unity. Estimativa realista: 2-3 semanas de dev pra um parser robusto. Não cabe no MVP.
4. **Game feel é decisão nossa**: copiar os números do LF2 amarra nosso combat feel ao do jogo de 1999. A gente quer LF2 *como inspiração*, não como specsheet.

### 5.4. Quando peekar no `.dat` faz sentido

Quando você (ou o Cursor) precisa entender **como o LF2 resolve um problema específico** — tipo "como eles fazem o grab funcionar?", "qual o frame rate da corrida?", "que valor de dvy tem o pulo do Davis?". Aí roda o decoder, lê a parte relevante, **internaliza a ideia, e implementa do nosso jeito** com nossos números.

---

## 6. Pipeline recomendado

### Etapa 1 — Pré-processamento (uma vez só)

```bash
python tools/prepare_lf2_assets.py \
  --src LittleFighter/ \
  --dst Assets/Art/lf2_ref/
```

Isso vai:
- Converter todas as BMPs de personagens pra PNG com alpha (preto → transparente).
- Pular os `_mirror.bmp` (a Unity flipa).
- Converter backgrounds com chroma key heurístico.
- Copiar `_f.bmp` e `_s.bmp` direto (sem alpha).
- Copiar `.wav` pra `Assets/Audio/lf2_ref/`.
- Gerar um `manifest.json` listando tudo (nome, dimensões, fileset).

### Etapa 2 — Import na Unity (automático via `AssetPostprocessor`)

O script `LF2SpriteImporter.cs` (em `Assets/Editor/`) detecta sprites em `Assets/Art/lf2_ref/` e configura:
- `Texture Type: Sprite (2D and UI)`
- `Sprite Mode: Multiple`
- `Filter Mode: Point (no filter)` ← essencial pra pixel art
- `Compression: None`
- `Pixels Per Unit: 80` (ajuste conforme nossa decisão de PPU)
- Slicing automático em grid 80×80
- Pivô `Bottom Center` (0.5, 0)

### Etapa 3 — Autorar `CharacterDefinition` (manual, mas escalável)

Pra cada personagem que você quer usar no jogo, criar um asset:
```
Assets/Game/Characters/Bandit/
├── Bandit_CharacterDefinition.asset       # ScriptableObject
├── Moves/
│   ├── Bandit_Idle.asset
│   ├── Bandit_Walk.asset
│   ├── Bandit_Punch1.asset
│   └── ...
└── README.md                              # quem é, que função tem no nosso jogo
```

### Etapa 4 — Wire na FSM do player

O player carrega o `CharacterDefinition`, e a FSM (`Idle/Move/Dash/Attack/Hitstun/Dead` — definida no nosso doc, seção 4) consome os `MoveDefinition`s de acordo com o input.

---

## 7. Regras pro Cursor (e pra nós)

### ✅ DO

- Ler **este documento** antes de mexer com qualquer asset do LF2.
- Usar o `prepare_lf2_assets.py` como única forma de converter BMPs.
- Tratar a pasta `Assets/Art/lf2_ref/` como **read-only** (assets de referência).
- Criar `CharacterDefinition`/`MoveDefinition` em pasta separada (`Assets/Game/Characters/`).
- Usar pivô `Bottom Center`, Filter Point, Compression None pra qualquer sprite-art.
- Quando inventar números (damage, knockback, hitstop, dvx/dvy), começar com chutes razoáveis e iterar — **não copiar dos `.dat`**.
- Versionar PNGs convertidos no Git LFS (mesma regra do nosso doc, seção 2 — "Git LFS para assets").

### ❌ DON'T

- **Não** importe BMP direto na Unity sem passar pelo script.
- **Não** escreva código que lê `.dat` em runtime — não vamos depender disso.
- **Não** copie hitbox values, frame timings, ou movesets verbatim do LF2 (mesmo se decodar). Inspirar sim, copiar não.
- **Não** use os `_mirror.bmp` — a Unity flipa.
- **Não** use a `bgm/` (música LF2) em builds — IP de terceiros.
- **Não** crie pastas duplicadas em `Assets/Resources/` com sprites — usar Addressables (regra do nosso doc, seção 2).

---

## 8. Estrutura final esperada

```
ProjectRoot/
├── LittleFighter/                       # extraído do .rar, fora do Assets/. Read-only. Não versionar.
├── tools/
│   ├── prepare_lf2_assets.py            # roda uma vez por update do LF2
│   └── decode_lf2_dat.py                # uso humano só (pra peekar no .dat)
└── unity/
    └── Assets/
        ├── Art/
        │   └── lf2_ref/                 # PNGs convertidos. Read-only via convenção.
        │       ├── characters/
        │       │   ├── bandit_0.png     # sheet 800x560 com alpha
        │       │   ├── bandit_1.png
        │       │   ├── bandit_face.png
        │       │   ├── bandit_icon.png
        │       │   └── ...
        │       ├── backgrounds/
        │       │   ├── lf/
        │       │   │   ├── pic1.png
        │       │   │   └── ...
        │       │   └── ...
        │       └── manifest.json
        ├── Audio/
        │   └── lf2_ref/
        │       └── *.wav
        ├── Editor/
        │   └── LF2SpriteImporter.cs     # AssetPostprocessor
        ├── Game/
        │   ├── Data/
        │   │   ├── CharacterDefinition.cs
        │   │   ├── MoveDefinition.cs
        │   │   ├── HitboxDefinition.cs
        │   │   └── EnemyDefinition.cs
        │   └── Characters/
        │       └── Bandit/
        │           ├── Bandit_CharacterDefinition.asset
        │           └── Moves/
        │               └── Bandit_Idle.asset
        └── Scripts/
            └── ... (FSM, combat, etc.)
```

---

## 9. Erros comuns e como evitar

| Sintoma                                  | Causa                                                       | Fix                                                     |
|------------------------------------------|-------------------------------------------------------------|---------------------------------------------------------|
| Personagem aparece com fundo preto       | BMP indexed importado direto, sem converter                 | Rodar `prepare_lf2_assets.py`                           |
| Personagem aparece "esticado" / borrado  | Filter Mode = Bilinear, ou Compression = Compressed         | Filter Point, Compression None                          |
| Sprite "flutua" no chão                  | Pivô em Center, não Bottom Center                           | Configurar pivô (0.5, 0) na Sprite Editor               |
| Animação rasga entre frames              | PPU ≠ tamanho da célula, ou pixel snapping desligado        | PPU = 80, Pixel Perfect Camera (URP)                    |
| Cor muito saturada / errada              | Color space Linear sem sRGB no asset                        | Sprite Importer → "sRGB (Color Texture)" ON             |
| Sprite "cortado" nas bordas              | Frame real é 79×79 mas slice em 80×80 corretíssimo          | Não é bug — é o gutter do LF2. Manter 80                |
| Personagem espelhado errado              | Usando o `_mirror.bmp` em vez do original                   | Usar só os originais; flipar via `flipX`                |

---

## 10. Próximas decisões pra travar (referência ao nosso doc)

Da seção 11 do `levantamento_e_diretrizes.md`:

> **3. Resolução base/PPU (definir cedo e congelar)**

Sugestão concreta dado o LF2: **Pixels Per Unit = 80** (1 célula LF2 = 1 unit Unity). Isso facilita aritmética de movement e hitbox. Câmera ortográfica com `size = 4.375` dá 1280×720 nativo (16:9, 700px de altura útil) com 8.75 unidades verticais.

> **4. Schema de dados (moves/hitboxes/upgrades)**

Os arquivos `CharacterDefinition.cs`, `MoveDefinition.cs`, `HitboxDefinition.cs` (incluídos neste pacote) são uma **proposta inicial** baseada nos campos que o LF2 expõe (`bdy`, `itr`, `state`, `wait`, `next`, `dvx`, `dvy`, `hit_a/d/j`). A gente itera sobre eles enquanto autoria os primeiros 3-4 personagens.

---

## Anexo A — Cheatsheet do `.dat` do LF2 (pra peekar, não pra parsear)

| Termo `.dat`        | Significado                                         | Equivalente nosso              |
|---------------------|-----------------------------------------------------|--------------------------------|
| `<frame> N name`    | Frame único (estado + visual + hitboxes)            | Um índice do `MoveDefinition.frames` |
| `pic: N`            | Qual frame da spritesheet usar                      | `frame.spriteIndex`            |
| `state: N`          | Categoria de estado (0=stand, 1=walk, ...)          | `frame.state` (enum nosso)     |
| `wait: N`           | Quantos ticks o frame fica ativo                    | `frame.durationTicks`          |
| `next: N`           | Qual frame vem depois (999 = loop/end)              | `frame.nextFrameId`            |
| `dvx: N` / `dvy: N` | Impulso aplicado no player ao entrar nesse frame    | `frame.impulse`                |
| `centerx/centery`   | Pivô (geralmente 39, 79)                            | já tratado no import (0.5, 0)  |
| `hit_a/d/j`         | Frame seguinte se input Atk/Def/Jump for pressionado| `frame.cancelOnAttack/Defense/Jump` |
| `bdy:` ... `bdy_end:`| Hurtbox (você apanha aqui)                         | `frame.hurtboxes[]`            |
| `itr:` ... `itr_end:`| Hitbox de ataque (você causa dano aqui)            | `frame.hitboxes[]`             |
| `wpoint:`           | Ponto onde o personagem segura arma                 | `frame.weaponPoint`            |
| `opoint:`           | Ponto onde spawna projétil                          | `frame.spawnPoint`             |
| `cpoint:`           | Ponto onde o pego em grab fica                      | `frame.grabPoint`              |

Os números absolutos dos LF2 (e.g. `dvx: 7`, `wait: 3`) **não devem ser copiados** — são chutes deles, balanceados pra um jogo diferente. Use só pra ter ideia da ordem de grandeza.

---

## Anexo B — Cor de transparência: a tabela completa

| Tipo de asset            | Chroma key                              | No script           |
|--------------------------|-----------------------------------------|---------------------|
| `<name>_0/1/2.bmp`       | RGB(0,0,0) — preto puro                 | hardcoded           |
| `<name>_f.bmp`           | nenhuma (imagem cheia)                  | skip                |
| `<name>_s.bmp`           | nenhuma                                 | skip                |
| `<name>_ball.bmp`        | RGB(0,0,0)                              | hardcoded           |
| `bg/*/picN.bmp`          | top-left pixel                          | heurística          |
| `bg/*/landN.bmp`         | top-left pixel (geralmente verde/cinza) | heurística          |
| `bg/*/shadow.bmp`, `s.bmp`| RGB(255,255,255) ou top-left           | heurística          |

---

Fim. Se algo aqui ficou ambíguo ou conflita com decisão do `levantamento_e_diretrizes.md`, o `levantamento_e_diretrizes.md` ganha — esse documento aqui é só sobre integração de asset, não sobre arquitetura.
