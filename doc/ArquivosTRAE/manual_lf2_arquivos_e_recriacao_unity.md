# Manual LF2 (arquivos, frames e como recriar o pipeline no Unity)

> **Aviso legal (importante)**  
> Eu consigo te ajudar a **entender o formato** do LF2 e a criar um pipeline para importar/interpretar os arquivos.  
> Mas **reutilizar os BMPs originais do LF2 no seu jogo comercial** (ou “recriar os personagens com as mesmas imagens”) provavelmente **infringe direitos autorais**, a menos que você tenha **permissão/licença explícita** do detentor dos direitos.  
> Recomendações seguras:
> - use isso para **referência interna/protótipo**; e depois **refaça sprites próprios** (ou licencie/contrate arte).  
> - ou crie personagens “inspirados” sem copiar sprites/nomes/moves icônicos.

---

## 0) O que você vai conseguir com este manual

1) Entender **como o LF2 organiza sprites** em BMPs (spritesheets em grade) e como o campo `pic:` aponta para cada frame.  
2) Entender o que é uma **frame** no `.dat` e os campos principais: `state`, `wait`, `next`, `dvx/dvy/dvz`, `hit_*`, `mp`, `sound`, `bdy`, `itr`, `opoint` etc.  
3) Ter uma **SPEC (especificação) pronta** para orientar um agente/engenheiro a:
   - extrair frames dos BMPs,
   - ler/decriptar `.dat`,
   - gerar assets no Unity (sprites, animation clips, hitboxes),
   - recriar comportamento básico dos personagens.

---

## 1) Estrutura típica de arquivos do LF2 (visão geral)

O LF2 é “data-driven”: o comportamento fica em **arquivos `.dat`** e os gráficos em **`.bmp`**.  
O fluxo geral é:

**BMP (spritesheet) + DAT (descrição/frames) → animação + colisão + ataques + efeitos**.

Exemplos comuns (nomes variam por instalação/mod):
- `data/*.dat` → dados (personagens, projéteis/“balls”, armas)
- `sprite/sys/*.bmp` → spritesheets dos personagens/armas/efeitos

> O LF2 também possui um arquivo central (`data.txt`) que referencia IDs de objetos (ex.: projéteis/balls) usados por ataques via `opoint` (explicado mais abaixo). Isso é citado em tutoriais de data changing.  
> Fonte: tutorial em PT-BR (Oocities) [1].

---

## 2) Como os BMPs funcionam (spritesheet em grade)

No `.dat`, existe um bloco `<bmp_begin> ... <bmp_end>` que lista os arquivos BMP e como fatiá-los.

Exemplo (personagem):

```
<bmp_begin>
name: Firen
head: sprite\sys\firen_f.bmp
small: sprite\sys\firen_s.bmp
file(0-69): sprite\sys\firen_0.bmp w: 79 h: 79 row: 10 col: 7
file(70-139): sprite\sys\firen_1.bmp w: 79 h: 79 row: 10 col: 7
file(140-209): sprite\sys\firen_2.bmp w: 79 h: 79 row: 10 col: 7
...
<bmp_end>
```

Onde:
- `file(A-B): caminho.bmp` define um **intervalo de índices de sprites** (A..B) contidos naquele BMP.  
- `w` e `h` são o tamanho de **cada célula** (sprite) em pixels.  
- `row` e `col` definem a **grade** do spritesheet. Em guias comuns, `row` é a contagem de **colunas** e `col` a contagem de **linhas** (grade total = `row * col`).  
Fonte: tutorial PT-BR (Oocities) [1] e tutorial (EGKP) [2].

### 2.1) Como o `pic:` encontra um sprite dentro do BMP

Dentro de cada `<frame>`, o campo `pic:` aponta para um índice global do conjunto de sprites.

Exemplo:
```
<frame> 0 standing
 pic: 0 ...
```
Significa “use o sprite de índice 0”.

Se você tem `file(0-69)` no primeiro BMP, então:
- `pic: 0` → primeiro sprite do `firen_0.bmp`
- `pic: 69` → último sprite do `firen_0.bmp`
- `pic: 70` → primeiro sprite do `firen_1.bmp`

Isso está explícito em guias de data changing: o intervalo `file(0-69)` define quais `pic` pertencem ao BMP. Fonte: [1], [2].

### 2.2) Fórmula de fatiamento (importador)

Para um BMP com `row = R` (colunas) e `col = C` (linhas), cada sprite tem `w x h`.

Dado um `pic` dentro do intervalo do arquivo (`start..end`):
- `localIndex = pic - start`
- `cellX = localIndex % R`
- `cellY = floor(localIndex / R)`

Retângulo do sprite:
- `x = cellX * w`
- `y = cellY * h`
- `width = w`
- `height = h`

**Observação:** BMPs do LF2 costumam usar uma paleta/colorkey (background) e não PNG com alpha; você provavelmente terá que tratar transparência ao importar para Unity (ou converter para PNG).

---

## 3) Como os “frames” do `.dat` funcionam

No LF2, uma `<frame>` é um “instante” do personagem/objeto: define **qual sprite aparece**, **quanto tempo fica**, **para qual frame vai depois**, **velocidades**, **inputs que redirecionam para moves**, e **caixas de colisão/ataque**.

Exemplo mínimo (standing):

```
<frame> 0 standing
 pic: 0 state: 0 wait: 4 next: 1 dvx: 0 dvy: 0 dvz: 0 centerx: 39 centery: 79 hit_a: 0 hit_d: 0 hit_j: 0 hit_Fa: 235 hit_Fj: 255 hit_Uj: 285 hit_Dj: 267
 bdy: kind: 0 x: 21 y: 18 w: 43 h: 62
 bdy_end:
<frame_end>
```

Explicações básicas (consolidadas de guias):
- `<frame> N nome`  
  - `N` é o ID numérico do frame (o que `next` aponta).  
  - `nome` é apenas organização (não muda o comportamento por si só). Fonte: [1], [2].
- `pic:` índice do sprite a exibir naquele frame. Fonte: [1], [2].
- `state:` “estado” do personagem/objeto naquele frame (standing, walking, run, attacking, defending, etc.). Há listas de estados em guias (ex.: 0=stand, 1=walk, 2=run, 3=punch/kick, 7=defend...). Fonte: [2].
- `wait:` quanto tempo (ticks internos do LF2) esse frame dura antes de ir para `next`. Fonte: [1], [2].
- `next:` ID do frame seguinte. Valor `999` costuma encerrar sequência. Fonte: [1], [2].
- `dvx/dvy/dvz:` deslocamento/velocidade aplicada no frame. Fonte: [1], [2].
- `centerx/centery:` ponto de referência do sprite (usado para colisão/posicionamento). Fonte: [1], [2].
- `hit_*:` redirecionamentos de input (quando o jogador aperta algo, o frame pode ir direto para outro). Fonte: [1], [2].
- `mp:` custo de energia/MP para iniciar um especial. Fonte: guia LF-Empire [3] e [1].
- `sound:` toca um WAV ao entrar no frame. Fonte: [1].

---

## 4) Inputs (combos e especiais): `hit_a`, `hit_j`, `hit_d`, `hit_Fa`, etc.

O LF2 define “atalhos” de input dentro de frames base (geralmente `standing`, `walking`, `defend`) para pular para o primeiro frame de um golpe/move.

Campos comuns:
- `hit_a`: Attack
- `hit_j`: Jump
- `hit_d`: Defend

E combinações “especiais” citadas nos tutoriais:
- `hit_Fa`: Defend + (frente) + Attack  
- `hit_Fj`: Defend + (frente) + Jump  
- `hit_Ua`: Defend + Up + Attack  
- `hit_Uj`: Defend + Up + Jump  
- `hit_Da`: Defend + Down + Attack  
- `hit_Dj`: Defend + Down + Jump  
- `hit_ja`: Defend + Jump + Attack  
Fonte: LF-Empire (Basics) [3] e tutoriais [1], [2].

No LF2, esses campos apontam para um **número de frame** (ex.: `hit_Fa: 235`), que é a “entrada” do golpe especial.

---

## 5) Caixas: `bdy` (corpo) e `itr` (ataque)

### 5.1) `bdy` (body)
Normalmente define a área do corpo (colisão/hurtbox básica):

```
bdy:
 kind: 0 x: 21 y: 18 w: 43 h: 62
bdy_end:
```

É comum adicionar `effect` em alguns casos (congelar/queimar) em exemplos didáticos, mas o padrão do “efeito no ataque” costuma aparecer em `itr` também. Fonte: [1] e [3].

### 5.2) `itr` (interaction / hitbox de ataque)
O LF-Empire afirma que **dano** e **efeitos** do ataque são definidos por `itr`, incluindo `injury` e `effect`. Fonte: [3].

No tutorial indonésio, `itr` é explicado como a seção que define o retângulo que acerta inimigos e parâmetros como `injury`, `dvx/dvy/dvz`, `fall`, `bdefend`, `effect`. Fonte: [2].

Exemplo (arma, mostrando vários itrs em frames):
O Weapon2.dat possui `itr` em frames de “on_hand”/“throwing” e também uma lista de “weapon_strength_list”. Fonte: [4].

---

## 6) Spawn de projéteis/objetos: `opoint` + `data.txt` (IDs)

Ataques que disparam projéteis/objetos usam um bloco `opoint`, com campo `oid`.

O tutorial PT-BR descreve `opoint` e explica que `oid` deve corresponder ao `id` no `data.txt` (ex.: `firen_ball.dat` com `id: 210`). Fonte: [1].

Isso é a base para recriar “bola de fogo”, “ice blast”, etc. no Unity:
- Em vez de spawnar “oid 210”, você spawna um **Prefab**/objeto do seu Registry com aquele ID.

---

## 7) Decriptação dos `.dat` (quando necessário)

Algumas distribuições/mods do LF2 usam `.dat` “codificado/criptografado” (na prática, um encoding com chave).

O projeto LF2DD descreve:
- Cada `.dat` começa com um **header de 123 bytes** (ignorável).
- Depois, cada byte corresponde a um caractere ASCII “ofuscado”.
- Fórmula: `Encrypted = Char + Key[i % 37]`, com chave `odBearBecauseHeIsVeryGoodSiuHungIsAGo`
- Decriptação: `Decrypted = Char - Key[i % 37]`  
Fonte: README do LF2DD [5].

> Para o seu pipeline, isso significa: você pode criar um importador que detecta “texto ilegível” e aplica a decriptação antes de parsear.

---

## 8) SPEC: como recriar no Unity (pipeline recomendado)

Esta é a parte que você queria: uma especificação para orientar um agente/engenheiro a implementar a importação e reconstrução.

### 8.1) Objetivo do importador
Dado um “pack” do LF2 (ou de um personagem específico), gerar no Unity:
1) Sprites (slice dos BMPs)
2) AnimationClips (por sequência de frames)
3) Um asset “CharacterDefinition” (data-driven) com:
   - stats base (walk/run/jump/dash)
   - moves (standing, walk, run, attacks)
   - hitboxes/hurtboxes por frame

### 8.2) Entradas
- Um `.dat` do personagem (ex.: `data/dennis.dat` ou equivalente)
- Os `.bmp` listados no `<bmp_begin>`
- Opcional: `data.txt` para resolver `oid` → arquivo/objeto

### 8.3) Saídas (Unity)
Criar em `Assets/_Imported/LF2/{CharacterId}/`:
- `Sprites/` (sprites gerados, nomeados de forma estável)
- `Animations/` (clips por ação)
- `Data/` (ScriptableObjects: `CharacterDefinition`, `MoveDefinition`, `HitboxDefinition` etc.)

### 8.4) Parsing do `.dat`
Implementar parser tolerante a “formatação solta”:
1) Se necessário, **decriptar** (ver seção 7) e obter texto puro.
2) Ler `<bmp_begin> ... <bmp_end>`:
   - `head`, `small`
   - múltiplos `file(start-end): path w h row col`
   - stats de movimento: `walking_speed`, `running_speed`, `jump_height`, `dash_distance`, etc. (presentes em tutoriais) [1], [2], [3]
3) Ler blocos `<frame> ... <frame_end>`:
   - Cabeçalho: ID numérico + nome
   - Linha principal com `pic/state/wait/next/dv*/center*/hit*/mp`
   - Blocos opcionais: `bdy`, `itr`, `wpoint`, `opoint`, `sound` (o mínimo que aparece em guias) [1], [2], [3]

### 8.5) Reconstrução de sprites (slice BMP)
Para cada entrada `file(start-end)`:
1) Carregar o BMP.
2) Fatiar em grade `row x col` com célula `w x h`.
3) Para cada índice global `i` no intervalo:
   - calcular `localIndex = i - start`  
   - `cellX = localIndex % row`, `cellY = floor(localIndex / row)`  
   - extrair retângulo e criar sprite.
4) Nome do sprite (estável): `"{character}_{i:0000}"`.

### 8.6) Pivot/origem (mapeando `centerx/centery`)
O LF2 usa `centerx/centery` como referência do sprite/colisão. Os tutoriais tratam isso como “centro” por coordenadas X/Y. Fonte: [1], [2].

**Regra recomendada (Unity):**
- Definir o pivot do sprite de forma que o “ponto (centerx, centery)” vire a origem local do personagem.
  - Em Unity, pivots são normalizados (0..1).  
  - `pivotX = centerx / w`  
  - `pivotY = centery / h`  

> Observação: isso pode exigir pivot por sprite (porque `centerx/centery` muda por frame). Se ficar caro, normalize para um pivot comum (ex.: “sola do pé”) e aplique offsets no transform por frame.  
> Para um MVP, eu recomendo: **pivot comum** + **offset por frame** (mais simples para animation clips).

### 8.7) Geração de AnimationClips
O LF2 não “define animação por nome”; ele define um **grafo de frames** via `next`.

Para gerar clipes:
1) Escolher um “frame raiz” de cada ação (ex.: standing = 0).
2) Seguir `next` até:
   - encontrar `999` (fim), ou
   - detectar loop (volta para um frame já visitado).
3) Converter cada `frame` em um keyframe de sprite:
   - duração = `wait` (em ticks LF2) → converter para segundos:
     - você pode tratar cada unidade como 1/30s (muitos guias tratam como time unit, mas isso varia; valide no jogo).  
4) Gerar também “events”:
   - quando um frame tem `sound`, gerar `AnimationEvent` para tocar SFX.
   - quando um frame possui `itr`, marcar “active hitbox” naquele trecho.

### 8.8) Reconstrução de gameplay (Unity runtime)
**Meta (MVP):** ficar “próximo” do feel, sem tentar clonar 1:1.

Mapeamento sugerido:
- `state` → estado da FSM (Idle/Move/Run/Attack/Defend/Hitstun/etc.)
- `dvx/dvy/dvz` → impulso/velocidade aplicada no tick
- `hit_*` → transições de input para moves (jump to frame)
- `bdy` → hurtbox/collider base (por frame)
- `itr` → hitbox de ataque (pode haver múltiplos `itr` no mesmo frame; ver Weapon2.dat) [4]
- `injury` → dano
- `effect` (1/2/3) → sangrar/queimar/congelar (exemplos em [3] e [2])
- `opoint` + `oid` → spawn de projétil/objeto (ver [1])

### 8.9) Validação (teste de fidelidade)
Para cada personagem importado:
1) Teste “standing/walk/run/jump/dash” (velocidades batem “de olho”?)
2) Teste 1 golpe normal (hitbox posicionada corretamente?)
3) Teste 1 especial com `opoint` (spawna e colide?)
4) Ajuste unidade de tempo do `wait` até o feel ficar certo.

---

## 9) O que eu recomendo para o seu projeto (decisão prática)

Se o objetivo é **homenagear LF2** e não se complicar:
- Use este manual para extrair **estrutura e timing** (frames, hitboxes, comandos).
- Refazer sprites e efeitos originais com arte própria.
- Manter a “linguagem” LF2: inputs `D+→+A`, `D+↓+J`, etc.

Se você realmente tiver licença/permite usar assets:
- Aí sim, faz sentido construir um importador automático (BMP+DAT → Unity).

---

## 10) Próximo passo (se você quiser que eu avance)

Se você puder, me diga:
1) Qual versão do LF2 você baixou (original / mod / community pack)?  
2) Os `.dat` que você tem estão “legíveis” em texto ou parecem codificados?  
3) Você quer que eu gere uma **spec em formato de tarefas de implementação** (checklist + pseudocódigo + estruturas de dados em C#) para o importador?

---

## Sources
[1] Mini-Tutorial / Editor de Data Little Fighter 2 (PT-BR) — https://www.oocities.org/br/fireageonline/lftutorial.html  
[2] LF2 data changing (IND) — https://egkp.wordpress.com/2008/04/02/lf2-data-changing-ind/  
[3] LF-Empire: Basics of Data Changing — https://www.lf-empire.de/en/lf2-empire/data-changing/basics/163  
[4] LF2 Wiki: Weapon2.dat (decrypted) — https://lf2.fandom.com/wiki/Weapon2.dat  
[5] LF2DD README (encryption header/key) — https://github.com/Mcpg/lf2dd/blob/master/README.md  
