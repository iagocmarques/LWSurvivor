# SPEC — Importador de personagens do LF2 para Unity (BMP + DAT → assets data-driven)

> Esta SPEC descreve como implementar um importador no **Unity 6** para ler os arquivos do **Little Fighter 2** do seu pacote (inclui `.dat` criptografados), fatiar sprites BMP e gerar assets/definições no formato do nosso projeto (FSM + hitboxes + spawns).
>
> **Objetivo do importador**: acelerar a recriação *técnica* (timing, inputs, hitboxes, spawns) — e não necessariamente clonar 1:1 o jogo.
>
> **Nota legal**: importar e redistribuir os sprites BMP originais do LF2 em um jogo comercial geralmente exige permissão/licença. Use este importador para **referência, prototipagem ou conteúdo licenciado**.

---

## 1) Escopo desta SPEC (o que entra / não entra)

### Entra (MVP do importador)
1) Decriptar `.dat` (formato clássico do LF2).
2) Parse de:
   - `<bmp_begin> ... <bmp_end>` (inclui ranges `file(start-end)` com `w/h/row/col`)
   - blocos `<frame> ... <frame_end>` com `pic/state/wait/next/dvx/dvy/dvz/hit_*/mp/sound`
   - blocos de colisão/ataque/spawn: `bdy`, `itr`, `opoint`
3) Resolver `opoint.oid` consultando `data/data.txt` (id → file/type).
4) Fatiar BMP em sprites e gerar assets Unity:
   - Sprites (sliced)
   - AnimationClips (sequências seguindo `next`)
   - Definições data-driven (ScriptableObjects) para Character/Move/Hitbox/Hurtbox/Spawn

### Não entra (por enquanto)
- Física avançada e interações 100% idênticas ao LF2
- Parsing completo de todos os blocos possíveis (ex.: `cpoint`, `bpoint`, casos exóticos de `state`)
- Importação automática de áudio WAV para eventos (podemos só gerar IDs/paths)
- Export para Addressables (fica para “v2”)

---

## 2) Entradas/saídas

### Entradas (do seu pacote)
- Pasta `data/` (inclui `data.txt` e `.dat` criptografados)
- Pasta `sprite/` (especialmente `sprite/sys/*.bmp`)

### Saídas (no Unity)
Gerar em:
- `Assets/_Imported/LF2/Characters/{characterId}_{name}/`
  - `Sprites/` (sprites gerados)
  - `Animations/` (clips)
  - `Data/` (ScriptableObjects gerados)
  - `Raw/` (opcional: cópia do `.dat` decriptado para debug, somente em editor)

---

## 3) Requisitos de implementação (Unity)

### 3.1) Onde roda
Implementação como **Editor Tool** (não runtime):
- Menu: `Tools/LF2/Import Character...`
- Menu: `Tools/LF2/Import All Characters (from data.txt)...`

### 3.2) Dependências
- Unity 6
- URP 2D no projeto (não é requisito do importador em si)
- Sem pacotes externos obrigatórios (tudo em C# + UnityEditor)

---

## 4) Decriptação dos `.dat` (obrigatório no seu pacote)

Os `.dat` do seu pacote estão criptografados/encodados (não são texto legível).

### 4.1) Algoritmo
- Ignorar os **123 bytes** iniciais (header).
- Para cada byte subsequente (índice `i`):
  - `decrypted = (encrypted - key[i % 37]) mod 256`
- `key` (ASCII): `odBearBecauseHeIsVeryGoodSiuHungIsAGo` (37 chars)

> Observação: isso bate com implementações públicas como LF2DD (README).

### 4.2) Detecção automática (heurística)
Se os primeiros ~4KB contiverem `"<bmp_begin>"` ou `"<frame>"`, trate como plaintext. Caso contrário, decripte.

### 4.3) Encoding
Decodificar bytes com **latin-1** para manter 1:1 bytes→chars (ASCII compatível).

---

## 5) Parser do `.dat`

### 5.1) Estruturas de dados (internas do importador)

```csharp
class Lf2BmpEntry {
  int start, end;
  string path;
  int cellW, cellH;
  int row, col; // grade
}

class Lf2Frame {
  int id;
  string name;
  Dictionary<string,string> props; // pic,state,wait,next,dvx...
  List<Lf2Rect> bdy;   // hurtboxes/colliders
  List<Lf2Rect> itr;   // hitboxes
  List<Dictionary<string,string>> opoint; // spawns
}

class Lf2Dat {
  string name, head, small;
  List<Lf2BmpEntry> bmps;
  Dictionary<string,string> movement; // walking_speed, running_speed...
  Dictionary<int,Lf2Frame> frames;
}
```

### 5.2) Parsing `<bmp_begin>`
Extrair:
- `name:`
- `head:`
- `small:`
- múltiplas linhas `file(A-B): sprite\sys\xxx.bmp w: 79 h: 79 row: 10 col: 7`

### 5.3) Parsing parâmetros de movimento (após `<bmp_end>`)
Linhas do tipo:
- `walking_speed 5.000000`
- `jump_height -16.299999`

Parse simplificado: `key value`.

### 5.4) Parsing de frames
Cada frame:
- Cabeçalho: `<frame> 0 standing`
- Corpo: linhas com `key: value` (ex.: `pic: 0 state: 0 wait: 4 next: 1 ...`)
- Blocos:
  - `bdy: ... bdy_end:`
  - `itr: ... itr_end:` (pode haver múltiplos `itr` por frame)
  - `opoint: ... opoint_end:`

**Regra**: o parser deve ser tolerante:
- campos podem aparecer em múltiplas linhas
- pode haver blocos que ignoramos (mantemos raw, mas não quebramos)

---

## 6) Resolver `data.txt` (ID → file/type)

O `data/data.txt` contém mapeamento de objetos (exemplos reais do seu pacote):
- `type: 0` → personagens
- `type: 1..6` → armas/objetos/balls etc.

O importador deve:
1) Parsear `data.txt` e criar um dicionário `id -> {type, file}`.
2) Ao encontrar `opoint` num frame, ler `oid`.
3) Resolver `oid` para arquivo correspondente (para gerar referência de prefab futuro).

---

## 7) Slicer BMP → Sprites (Unity)

### 7.1) Regra de slicing (grade)
Para cada `file(start-end)`:
- `localIndex = pic - start`
- `cellX = localIndex % row`
- `cellY = floor(localIndex / row)`
- `rect = (cellX*cellW, cellY*cellH, cellW, cellH)`

### 7.2) Transparência
BMP do LF2 geralmente usa **colorkey** (fundo sólido). Estratégias:
- (simples) converter BMP → PNG com alpha antes de importar (pipeline externo), ou
- (avançado) pós-processar textura e remover cor de fundo (colorkey) em Editor.

Para o MVP, recomendo: **converter para PNG** (offline) para evitar dor no importador.

---

## 8) Construção de AnimationClips (a partir do grafo `next`)

### 8.1) Problema
LF2 define animações como um **grafo**:
- cada frame tem `wait` + `next`
- pode haver loops

### 8.2) Estratégia prática (MVP)
Gerar clips para conjuntos que nos interessam:
1) **Base locomotion**
   - standing (state 0)
   - walking (state 1)
   - running (state 2)
2) **Ataques normais (state 3)**
3) **Defend/hitstun (state 7/11/12/...)** conforme existir

Como encontrar “raízes”:
- frames com `state` alvo e que são frequentemente referenciados por outros `next` (heurística), ou
- usar listas fixas por convenção (ex.: standing começa no frame 0 em muitos chars) + fallback para heurística.

### 8.3) Conversão de tempo
O LF2 usa `wait` em unidades internas. Muitos tutoriais chamam de “time unit” (~1/30s), mas isso pode variar por implementação.  
No Unity, defina:
- `seconds = wait * (1/30f)` como default
- e permita override global por import (ex.: 1/60, 1/40) para calibrar feel.

---

## 9) Geração de ScriptableObjects do nosso projeto

### 9.1) CharacterDefinition (alvo)
Campos mínimos:
- `id` (do data.txt)
- `displayName` (do `name:`)
- movement:
  - walking_speed / running_speed / jump_height / dash_distance...
- `moves` (lista)

### 9.2) MoveDefinition (por move)
Para cada move, guardar:
- `name`
- `startFrameId`
- sequência de frames (expandida seguindo `next`, com proteção anti-loop)
- `inputBindings` (quais `hit_*` apontam para esse startFrameId)

### 9.3) Hitbox/Hurtbox
Converter `itr` e `bdy` para retângulos por frame/tick:
- retângulo (x,y,w,h)
- `injury` → dano
- `effect` → status (ex.: burn/freeze/blood)
- `dvx/dvy/dvz` → knockback

**Observação importante:** coordenadas do LF2 são relativas ao sprite e ao `centerx/centery`.  
No MVP, você pode:
- definir um pivot padrão (ex.: “pé”) para todos os sprites
- e aplicar offsets por frame no runtime (mais simples do que pivot por sprite).

### 9.4) Spawns (opoint)
Gerar “SpawnEvents”:
- frameId
- oid (int)
- referência resolvida (file do data.txt, se existir)

---

## 10) Como isso encaixa no nosso runtime (jogo Unity)

O importador gera dados.
O runtime executa:
- FSM do player
- input buffering
- hit stop / shake
- netcode

Mapeamento recomendado:
- `hit_*` → “Command → StartFrameId”
- `state` → estado da FSM (ou subestado)
- `dvx/dvy/dvz` → impulso aplicado no tick
- `wait`/`next` → timeline do move
- `itr` → hitboxes ativas
- `bdy` → hurtbox por frame
- `opoint` → spawn de projéteis/objetos (prefabs futuros)

---

## 11) Plano de implementação (tarefas)

### Fase A — Importer Core (1–2 dias)
1) `Lf2DatDecryptor` (detecção + decriptação)
2) `Lf2DataTxtParser` (id/type/file)
3) `Lf2DatParser` (bmp_begin + frames + bdy/itr/opoint)
4) Teste: parse do `dennis.dat` e checar contagem de frames

### Fase B — Sprites/Animations (2–4 dias)
5) `Lf2BmpSlicer` (gera Sprite assets)
6) `Lf2ClipBuilder` (gera AnimationClips por state/raiz)
7) Teste visual: preview de standing/walk/run

### Fase C — ScriptableObjects (2–4 dias)
8) `Lf2SoBuilder` (CharacterDefinition/MoveDefinition/Hitboxes/Spawns)
9) Validador: warnings para:
   - `pic` fora de range
   - `next` apontando frame inexistente
   - `opoint.oid` sem resolução no `data.txt`

### Fase D — Integração no jogo (1–2 dias)
10) Runtime loader que lê `CharacterDefinition` importado e toca um move (sem combate ainda)

---

## 12) Critérios de aceitação

1) Importar todos os `type:0` do `data.txt` sem crash.
2) Para cada personagem importado:
   - sprites gerados para todos os `pic` referenciados
   - pelo menos 3 clips: standing / walking / running (quando existirem)
   - tabela de moves com bindings `hit_*`
3) Spawns `opoint` listados com `oid` resolvido (quando possível).

---

## 13) Apêndice: mapeamento inicial de type (data.txt)

No seu `data.txt`:
- `type: 0` = personagens
- `type: 1,2,4,6` = armas/itens (varia por peso/tipo)
- `type: 3` = balls/projéteis
- `type: 5` = criminal (NPC)  

> Não dependa hardcoded disso para sempre; use apenas como referência e logue o que encontrar.

