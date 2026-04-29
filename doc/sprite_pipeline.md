# Pipeline de Sprites LF2 → Unity (Estado Atual)

> Ultima atualizacao: 2026-04-29
> Ref: ArquivosTRAE/spec_importador_lf2_para_unity.md, ArquivosTRAE/manual_lf2_arquivos_e_recriacao_unity.md

---

## 1. Visao Geral

Os sprites do LF2 sao extraidos dos BMPs originais, convertidos para PNG com transparencia (chroma key preto → alpha), e importados no Unity via `LF2SpriteImporter` (AssetPostprocessor). O runtime carrega frames via `Lf2VisualLibrary` usando `Resources.LoadAll<Sprite>()`.

```
BMPs do LF2 (sprite/sys/*.bmp)
    │
    ├─ prepare_lf2_resources_inplace.py (doc/ArquivosOpus/ — legado)
    │   ou conversao manual com chroma key preto (0,0,0) → alpha 0
    │
    ├─ Assets/_Project/Resources/LF2/*_alpha.png
    │   └─ .meta configurado por LF2SpriteImporter.cs (OnPreprocessTexture)
    │
    └─ Runtime:
        ├─ Lf2VisualLibrary.GetPlayerFrame(frameIndex)
        ├─ Lf2VisualLibrary.GetEnemyFrame(enemyId, frameIndex)
        ├─ Lf2PlayerSpriteAnimator (player)
        └─ Lf2EnemySpriteAnimator (inimigos)
```

---

## 2. Arquivos na Pasta Resources

### Spritesheets de personagem (10x7, 80x80 por celula, 800x560)

| Arquivo | Personagem | Frames |
|---------|-----------|--------|
| `player_davis_0_alpha.png` | Davis sheet 0 | pic 0-69 |
| `player_davis_1_alpha.png` | Davis sheet 1 | pic 70-139 |
| `enemy_grunt_bandit_0_alpha.png` | Bandit sheet 0 | |
| `enemy_grunt_bandit_1_alpha.png` | Bandit sheet 1 | |
| `enemy_bruiser_knight_0_alpha.png` | Bruiser sheet 0 | |
| `enemy_scout_hunter_0b_alpha.png` | Scout sheet 0 | |

### Mirror sheets (nao usados em runtime — flip e feito via SpriteRenderer.flipX)

| Arquivo | Nota |
|---------|------|
| `player_davis_0_mirror_alpha.png` | Ignorado pelo importador |
| `player_davis_1_mirror_alpha.png` | Ignorado pelo importador |
| `enemy_grunt_bandit_0_mirror_alpha.png` | Ignorado |
| `enemy_grunt_bandit_1_mirror_alpha.png` | Ignorado |
| `enemy_bruiser_knight_0_mirror_alpha.png` | Ignorado |
| `enemy_scout_hunter_0b_mirror_alpha.png` | Ignorado |

### Backgrounds (single sprite, sem slice)

| Arquivo | Tipo |
|---------|------|
| `bg_pic1_alpha.png` | Background principal |
| `bg_template1_pic1_alpha.png` | Variante 1 |
| `bg_template2_pic1_alpha.png` | Variante 2 |
| `bg_template3_pic1_alpha.png` | Variante 3 |

---

## 3. Configuracao de Import (LF2SpriteImporter.cs)

Localizacao: `Assets/_Project/Editor/LF2SpriteImporter.cs`

O `LF2SpriteImporter` e um `AssetPostprocessor` que configura automaticamente as import settings de qualquer PNG em `Assets/_Project/Resources/LF2/`.

### Settings aplicadas em OnPreprocessTexture

```
textureType      = Sprite (2D and UI)
filterMode       = Point (pixel art)
mipmapEnabled    = false
sRGBTexture      = true
alphaIsTransparency = true
spritePixelsPerUnit = 80
isReadable       = true
textureCompression = Uncompressed
```

### Para character sheets (*_alpha.png que nao comecam com bg_)

```
spriteImportMode = Multiple
spritePivot      = (0.5, 0) — bottom-center
spritesheet      = 70 SpriteMetaDatas (10 cols x 7 rows, 80x80 cada)
```

### Para backgrounds e outros

```
spriteImportMode = Single
spritePivot      = (0.5, 0)
```

### .meta manual (textureFormat)

O .meta deve ter `textureFormat: -1` (auto = RGBA32). Se estiver como `textureFormat: 1` (Alpha8), a textura so tera canal alpha sem cor — sprite fica preto.

**Verificar:** todos os `*_alpha.png.meta` devem ter:
```yaml
textureFormat: -1
isReadable: 1
```

---

## 4. Runtime — Lf2VisualLibrary

Localizacao: `Assets/_Project/Gameplay/Visual/Lf2VisualLibrary.cs`

### Carregamento de frames

```csharp
// Frame especifico do player (0-69 = sheet _0, 70+ = sheet _1)
Lf2VisualLibrary.GetPlayerFrame(frameIndex)

// Frame especifico de inimigo
Lf2VisualLibrary.GetEnemyFrame(enemyId, frameIndex)

// Background por indice
Lf2VisualLibrary.GetBackgroundSpriteByIndex(index)
```

### Fluxo interno

1. `GetCharacterFrame(key, frameIndex)` — cacheia por `key#frame_N`
2. `GetSheetSprites(key)` — tenta `Resources.LoadAll<Sprite>("LF2/" + key)`
3. Se `LoadAll` retorna 0 sprites → fallback: `Resources.Load<Texture2D>()` + `CreateSpritesFromGrid(tex, 80, 80)` com `Sprite.Create()`
4. Cache em `Dictionary<string, Sprite[]>` (SheetCache)

### Mapeamento de enemyId para sheet key

| enemyId | Sheet key |
|---------|-----------|
| contém "scout" | `enemy_scout_hunter_0b_alpha` |
| contém "bruiser" | `enemy_bruiser_knight_0_alpha` |
| outro (grunt/bandit) | `enemy_grunt_bandit_0_alpha` |

---

## 5. Sprite Animators

### Lf2PlayerSpriteAnimator

Localizacao: `Assets/_Project/Gameplay/Visual/Lf2PlayerSpriteAnimator.cs`

- Le `PlayerHsmController` para estado atual (idle, moving, attacking, hurt, etc.)
- Mapeia estados para arrays de frame indices (pic values do LF2)
- Usa `Lf2VisualLibrary.GetPlayerFrame(frameIndex)` para obter sprite
- Flip via `_sr.flipX = !_hsm.FacingRight`

### Lf2EnemySpriteAnimator

Localizacao: `Assets/_Project/Gameplay/Visual/Lf2EnemySpriteAnimator.cs`

- Configurado via `Configure(enemyId)` — chamado por `EnemyAgent`
- Idle: frames 0-2, Move: frames 3-7 (ou 3-9 para bruiser/scout)
- Flip via `_sr.flipX = !_facingRight`

---

## 6. Lf2VisualApplier (Bootstrap Visual)

Localizacao: `Assets/_Project/Gameplay/Visual/Lf2VisualApplier.cs`

- `AutoInstall()` roda `[RuntimeInitializeOnLoadMethod(AfterSceneLoad)]`
- Cria instancia DontDestroyOnLoad se nao existir
- `ApplyPlayer()`: encontra GameObject "Player", aplica sprite, cor branca, material compativel, e adiciona `Lf2PlayerSpriteAnimator`
- `EnsureBackground()`: cria "LF2_Background" com sprite de background, sortingOrder -10000
- F8 ciclo de backgrounds (Update com Keyboard input)

---

## 7. Chroma Key / Transparencia

### Regra

O LF2 usa **preto puro (0,0,0)** como chroma key para sprites de personagens.
Para backgrounds, usa o pixel do canto superior esquerdo (0,0) como chroma key.

### Pipeline de conversao

1. BMP original → converter para RGBA
2. Pixels com cor exata (0,0,0) → alpha = 0 (transparente)
3. Demais pixels → alpha = 255 (opaco)
4. Salvar como PNG 32bpp com canal alpha

### Verificacao

O PNG resultante deve ter:
- Modo: RGBA ou ARGB (nao Indexed, nao RGB sem alpha)
- Pixels de fundo: (0,0,0,0) = transparente preto
- Pixels de sprite: cor com alpha = 255

---

## 8. Troubleshooting

### Sprite aparece como retangulo preto

**Causa provavel:** `textureFormat: 1` (Alpha8) no .meta — armazena apenas alpha, sem dados de cor.

**Fix:** Mudar para `textureFormat: -1` (auto = RGBA32) em todos os `*_alpha.png.meta`.

### Sprite aparece amarelo

**Causa:** `GameRuntimeInstaller` define `sr.color = new Color(1f, 0.95f, 0.35f, 1f)` como placeholder.

**Fix:** `Lf2VisualApplier.ApplyPlayer()` reseta `sr.color = Color.white` apos aplicar sprite.

### LoadAll retorna 0 sprites

**Causa:** .meta com `spriteMode: 1` (Single) em vez de `spriteMode: 2` (Multiple).

**Fix:** `LF2SpriteImporter` configura `spriteImportMode = Multiple` para character sheets. Se nao aplicar, reimportar o asset manualmente.

### NullReferenceException em SpriteBoneDataTransfer

**Causa:** `SaveAndReimport()` chamado dentro de `OnPostprocessTexture` (proibido pela Unity).

**Fix:** Ja corrigido — `LF2SpriteImporter` moveu toda configuracao para `OnPreprocessTexture`.

---

## 9. Referencias

- `doc/ArquivosTRAE/spec_importador_lf2_para_unity.md` — Spec completa do importador (DAT decrypt, BMP slice, ScriptableObjects)
- `doc/ArquivosTRAE/manual_lf2_arquivos_e_recriacao_unity.md` — Manual de formatos LF2 e recriacao no Unity
- `doc/ArquivosTRAE/lf2_manual_personagens_completo.md` — Catalogo completo de personagens com frame data
- `doc/ArquivosTRAE/prompts_mvp_trae 2.md` — Prompts de implementacao step-by-step
