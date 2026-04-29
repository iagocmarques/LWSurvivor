# Guia: autorar o Bandit (do zero até jogável)

> Passo a passo pra você (ou o Cursor) levar o sprite do Bandit do LF2 até um personagem jogável no nosso Unity. Use isso na primeira vez que rodar o pipeline pra validar tudo.

---

## Pré-requisitos

- Projeto Unity 6 com URP 2D criado.
- Os arquivos do `lf2_unity_kit_full` instalados (ver `README.md` na raiz).
- O `LittleFighter/` extraído em algum lugar do disco (não precisa estar em `Assets/`).

---

## 1. Rodar o pipeline de assets

```bash
cd <repo_root>
python tools/prepare_lf2_assets.py \
  --src ../LittleFighter/ \
  --dst Assets/Art/lf2_ref/
```

Confere que apareceram:

```
Assets/Art/lf2_ref/characters/
├── bandit_0.png      (800x560)
├── bandit_1.png      (800x560)
├── bandit_f.png      (120x120, portrait)
└── bandit_s.png      (40x45, icon)
```

Abra a Unity. O `LF2SpriteImporter` vai detectar os PNGs automaticamente e:
- Tipo: Sprite (2D and UI)
- Filter: Point
- Compression: None
- Pixels Per Unit: 80
- `bandit_0.png` e `bandit_1.png`: sliceados em 70 sprites cada (10×7)
- `bandit_f.png` e `bandit_s.png`: Single sprite

Confirme abrindo `bandit_0.png`: na aba Sprite Editor você deve ver as 70 células marcadas.

---

## 2. Criar o `CharacterDefinition`

1. Crie a pasta: `Assets/Game/Characters/Bandit/`
2. Botão direito → Create → LF2Game → Character Definition
3. Renomeie pra `Bandit_CharacterDefinition`
4. No inspector preencha:
   - **id**: `bandit`
   - **displayName**: `Bandit`
   - **portrait**: arrastar `bandit_f` (o sprite, não o png raiz)
   - **icon**: arrastar `bandit_s`
   - **frames**: clique no cadeado pra travar inspector. No Project, expanda `bandit_0.png` (clique no arrow). Selecione todos os 70 sub-sprites (`bandit_0_000` a `bandit_0_069`) e arraste pra dentro do array `frames`. Repita pra `bandit_1.png` (vão entrar como índices 70-139).
   - **maxHp**: 100
   - **walkSpeed**: 3.5
   - **runSpeed**: 6.0
   - **jumpVelocity**: 12

**Dica**: pra arrastar muitos sprites de uma vez, segure Shift e clique no primeiro e no último.

---

## 3. Criar os primeiros `MoveDefinition`s

Crie a pasta `Assets/Game/Characters/Bandit/Moves/`.

### 3.1. `Bandit_Idle`

Botão direito → Create → LF2Game → Move Definition. Renomeie.

| Campo                | Valor                              |
|----------------------|------------------------------------|
| id                   | `bandit_idle`                      |
| displayName          | `Idle`                             |
| validEntryStates     | `Idle`                             |
| inputTrigger         | `None` (animação ambiente)         |
| frames               | 4 entradas (ver abaixo)            |

Para os frames (todos `durationTicks: 6`, sem hitboxes/hurtboxes/impulse):

- Frame 0: `spriteIndex: 0`
- Frame 1: `spriteIndex: 1`
- Frame 2: `spriteIndex: 2`
- Frame 3: `spriteIndex: 3`

(No LF2 o Bandit usa frames 0-3 pra parado. Olhe na Sprite Editor pra confirmar visualmente.)

### 3.2. `Bandit_Walk`

| Campo            | Valor                          |
|------------------|--------------------------------|
| id               | `bandit_walk`                  |
| displayName      | `Walk`                         |
| validEntryStates | `Walk`                         |
| inputTrigger     | `None`                         |
| frames           | frames de caminhada (5,6,7,8)  |

Cada frame: `durationTicks: 5`, sem hitboxes.

### 3.3. `Bandit_Punch1` (o primeiro ataque jogável!)

| Campo            | Valor                                               |
|------------------|-----------------------------------------------------|
| id               | `bandit_punch1`                                     |
| displayName      | `Punch 1`                                           |
| validEntryStates | `Idle`, `Walk`                                      |
| inputTrigger     | `Attack`                                            |

Frames (chutes pra começar — itere depois):

- **Frame 0** (windup): `spriteIndex: 60`, `durationTicks: 4`, sem hitboxes nem hurtboxes
- **Frame 1** (active): `spriteIndex: 61`, `durationTicks: 3`,
  - **hitboxes**: 1 entrada
    - `kind: Strike`
    - `rect: x=0.3, y=0.5, width=0.8, height=0.6` (à direita do char, na altura do peito)
    - `damage: 8`
    - `knockback: x=3.5, y=2.0`
    - `hitstunTicks: 18`
    - `hitstopTicks: 4`
    - `causesKnockdown: false`
- **Frame 2** (recovery): `spriteIndex: 62`, `durationTicks: 8`, sem hitboxes
- **Frame 3** (return): `spriteIndex: 0`, `durationTicks: 2`

Os índices 60/61/62 são chutes — ajuste pra os frames de soco do Bandit (no LF2 tipicamente ficam na linha 6 do `bandit_0.bmp`). Veja na Sprite Editor.

> ⚠️ Lembrete da seção 5.3 do `lf2_assets_guide.md`: **NÃO** copie os valores `dvx`/`dvy`/`bdy` literais do `.dat` do LF2. Os números acima são chutes nossos pra começar — itere depois.

### 3.4. (Opcional) `Bandit_Punch2` pra combo

Mesmo formato do Punch1 mas frames diferentes (63, 64, 65). Importante: no `Bandit_Punch1`, configure:
- **cancelOnAttack** = `Bandit_Punch2`

Isso permite o combo: pressionar Attack durante o Punch1 transiciona pro Punch2 antes do recovery acabar.

---

## 4. Adicionar tudo ao `CharacterDefinition`

Volte em `Bandit_CharacterDefinition`, no campo `moves` adicione (drag and drop):
- `Bandit_Idle`
- `Bandit_Walk`
- `Bandit_Punch1`
- `Bandit_Punch2` (se criou)

---

## 5. Criar o `Registry`

Botão direito → Create → LF2Game → Registry. Em `Characters` arraste o `Bandit_CharacterDefinition`. Salve.

---

## 6. Montar a cena de teste

### 6.1. Cena nova

`File → New Scene → Empty (URP)`. Salve como `Assets/Scenes/Sandbox.unity`.

### 6.2. Camera

A `Main Camera` que vem na cena:
1. Adicione o componente `PixelPerfectCameraSetup`.
2. Já vem com defaults bons (1280×720, PPU=80).

### 6.3. Bootstrap

Crie um GameObject vazio chamado `[Bootstrap]`.
- Adicione o componente `BootstrapInstaller`.
- Em `GameRegistry`, arraste seu Registry asset.
- Em `GameCamera`, arraste a Main Camera.

### 6.4. Player

Crie um GameObject vazio chamado `Player`.
1. Adicione `SpriteRenderer` (qualquer sprite, vai ser sobrescrito em runtime).
2. Adicione `CharacterController2_5D`.
3. Adicione `PlayerInputReader`.
4. Adicione `PlayerFSM`.
5. No `PlayerFSM`, arraste o `Bandit_CharacterDefinition` em `Character`.
6. Coloque o GameObject na origem (0, 0, 0).

### 6.5. Chão (placeholder visual)

Adicione um Quad simples atrás do player pra ter referência visual de chão. Ou um Sprite com um background do LF2 (`Assets/Art/lf2_ref/backgrounds/template/1/pic1.png`).

---

## 7. Play!

Pressione Play. O Bandit aparece, anda com setas, ataca com `A`, defende com `S`, pula com `D` (se o move existir).

### Esperado:
- Idle animation rodando suavemente.
- Andar com as setas faz a animação de Walk.
- Apertar `A` faz o Punch1 e impede movimento durante o ataque.
- O sprite vira (flipX) quando você inverte direção.

### Se não funcionar:
- **Personagem invisível**: confira que `Character.frames` tá preenchido com os 70+ sprites.
- **Personagem com fundo preto**: você importou o BMP cru. Rode o `prepare_lf2_assets.py` e use os PNGs de `lf2_ref/`.
- **Personagem flutuando**: pivô errado. Sprite Editor → confirma pivô em `Bottom Center` (0.5, 0).
- **Sprite borrado**: filter mode tá Bilinear. O `LF2SpriteImporter` deveria ter forçado Point — verifica que ele tá em `Assets/Editor/`.
- **Combo não engata**: confirma que `Bandit_Punch1.cancelOnAttack` aponta pro `Bandit_Punch2`.

---

## 8. Próximos passos (em ordem de prioridade)

1. **Adicionar inimigos**. Crie um `EnemyDefinition` simples e arraste no `SwarmManager`. Coloque um `SwarmManager` na cena, configure o `Target = Player`, e chame `SwarmManager.Instance.Spawn(0, pos)` em algum gerador de teste.
2. **Adicionar `Bandit_Defense`**. Move com `inputTrigger: Defense`, sem hitbox, com hurtboxes especiais (poise alto). Cancela em qualquer Idle/Walk.
3. **Adicionar `Bandit_Jump`**. Move com `impulse: (0, 0, 12)` no primeiro frame.
4. **Telemetria de balance**. O `levantamento_e_diretrizes.md` (§10.5) prevê isso — vamos colocar um logger simples nos hits pra começar a coletar dados.

---

## 9. Lembrete final

Esses números todos são chutes pra arrancar. Vai testar, iterar, e acertar pelo feel. **Não copie do LF2 verbatim.** Você vai descobrir que dá pra ter um soco maneiro com `damage: 8` ou com `damage: 14` — depende do hp dos inimigos, do hitstun, do combo length, do screen shake. Tunar números é metade do trabalho de design.

Se quiser uma referência inicial razoável (não LF2-copy mas baseada em beat 'em ups bons): hits leves causam 5-10 dano com 12-18 ticks de hitstun. Hits pesados causam 15-25 com knockdown. Hitstop de 2-4 ticks pra hits leves, 6-10 pra hits pesados.
