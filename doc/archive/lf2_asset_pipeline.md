# Pipeline de Assets LF2 → Unity

## Visão Geral

Os sprites do LF2 são extraídos dos BMPs originais e convertidos para PNG com transparência.
O LF2 usa **preto puro (0,0,0)** como chroma key para sprites de personagens.

---

## Scripts de Conversão

### 1. `prepare_lf2_assets.py` (conversão principal)

**Localização:** `doc/ArquivosOpus/prepare_lf2_assets.py`

**Uso:**
```bash
python prepare_lf2_assets.py --src <pasta_LF2_extraida> --dst Assets/Art/lf2_ref/
```

**O que faz:**
- Converte `sprite/sys/*.bmp` → `Assets/Art/lf2_ref/characters/*.png`
- Converte backgrounds com chroma key heurístico (pixel do canto superior esquerdo)
- Copia áudio (`data/*.wav`) para `Assets/Audio/lf2_ref/`
- Copia `data/data.txt` (índice de IDs do LF2)
- Gera `manifest.json` com metadados

**Chroma key:** Preto puro `(0,0,0)` → `alpha = 0` (transparente)

**Arquivos ignorados:**
- `*_mirror.bmp` — espelhamento é feito em runtime pelo Unity
- `ad0*`, `ad1*` — banners de propaganda do LF2

**Tipos de arquivo:**
| Padrão | Tipo | Tratamento |
|--------|------|------------|
| `*_f.bmp` | Retrato/face | Conversão direta (sem alpha) |
| `*_s.bmp` | Ícone pequeno | Conversão direta (sem alpha) |
| `*_0.bmp`, `*_1.bmp`, etc. | Sprite sheet | Chroma key preto → alpha |
| `bg_*` | Background | Chroma key heurístico |

### 2. `prepare_lf2_resources_inplace.py` (conversão local)

**Localização:** `doc/ArquivosOpus/prepare_lf2_resources_inplace.py`

**Uso:**
```bash
python prepare_lf2_resources_inplace.py
```

**O que faz:**
- Converte BMPs em `Assets/_Project/Resources/LF2/` para `*_alpha.png`
- Backgrounds: chroma key do pixel (0,0)
- Personagens: chroma key preto (0,0,0)
- Sobrescreve PNGs existentes

---

## Estrutura de Spritesheets

### Formato padrão
- **Grade:** 10 colunas × 7 linhas
- **Tamanho da célula:** 80×80 pixels
- **Tamanho da textura:** 800×560 pixels
- **Pivot recomendado:** bottom-center `(0.5, 0)` (normalizado)

### Mapeamento pic → posição na grade
```
pic = row * 10 + col
col = pic % 10
row = pic / 10
```

### Coordenadas Unity (origem bottom-left)
```
x = col * 80
y = textureHeight - row * 80 - 80
```

### Multi-sheet (personagens com mais de 70 frames)
| Range pic | Sheet | Exemplo |
|-----------|-------|---------|
| 0-69 | `xxx_0.bmp` | `davis_0` |
| 70-139 | `xxx_1.bmp` | `davis_1` |
| 140-209 | `xxx_2.bmp` | `davis_2` |

No código: `if (frameIndex >= 70) → usa sheet _1 com frameIndex - 70`

---

## Espelhamento (Flip)

O LF2 **não usa mirror sheets** em runtime — o `.dat` referencia apenas os sheets normais.
O espelhamento é feito pelo engine quando o personagem vira para o outro lado.

**No Unity:** `Lf2VisualLibrary.GetCharacterFrame(key, frameIndex, flipped: true)`
- Cria uma cópia espelhada horizontalmente da textura inteira
- Cacheia em `FlippedTextureCache` (uma textura por sheet)
- Extrai o frame da posição espelhada: `x = textureWidth - rect.x - rect.width`
- Sprite criado com a textura espelhada

**Arquivos `*_mirror.bmp`** existem no pacote LF2 mas são:
- Redundantes (cópia espelhada do original)
- Não referenciados no `.dat` (não estão no bloco `<bmp_begin>`)
- Ignorados pelo script de conversão (`SKIP_PATTERNS`)

---

## Pipeline Completo

```
LF2 Game (BMPs)
    │
    ├─ prepare_lf2_assets.py
    │   └─ Assets/Art/lf2_ref/characters/*.png
    │       (chroma key preto, sem mirror sheets)
    │
    ├─ Copiar para Resources/LF2/
    │   └─ Assets/_Project/Resources/LF2/*.png
    │       (importados como Sprite, Read/Write enabled)
    │
    └─ Runtime:
        ├─ Lf2VisualLibrary.GetPlayerFrame(pic, flipped)
        ├─ Lf2VisualLibrary.GetEnemyFrame(enemyId, pic, flipped)
        └─ Cache: normal + flipped por sheet
```

---

## Import Settings no Unity

Para cada PNG em `Resources/LF2/`:
- **Texture Type:** Sprite (2D and UI)
- **Sprite Mode:** Single
- **Pixels Per Unit:** 48 (personagens) ou 80 (backgrounds)
- **Filter Mode:** Point (no filter) — pixel art
- **Compression:** None ou Low Quality
- **Read/Write Enabled:** necessário para flip em runtime
