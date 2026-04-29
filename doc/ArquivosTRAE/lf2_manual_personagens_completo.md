# Manual LF2 — Catálogo completo de personagens (extraído do seu pacote)

Este manual foi gerado automaticamente a partir dos arquivos do seu `LittleFighter` (data + sprite).

## Sumário
- [Dennis](#personagem-9-dennis)
- [Bandit](#personagem-30-bandit)
- [Hunter](#personagem-31-hunter)
- [Mark](#personagem-32-mark)
- [Jack](#personagem-33-jack)
- [Sorcerer](#personagem-34-sorcerer)
- [Monk](#personagem-35-monk)
- [Jan](#personagem-36-jan)
- [Knight](#personagem-37-knight)
- [Bat](#personagem-38-bat)
- [Justin](#personagem-39-justin)
- [LouisEX](#personagem-50-louisex)
- [Firzen](#personagem-51-firzen)
- [Julian](#personagem-52-julian)

---

## Notas gerais (como ler)
- **`pic`**: índice global do sprite dentro dos ranges `file(start-end)` do `<bmp_begin>`.
- **`wait` + `next`**: duração do frame e próximo frame (grafo de animação).
- **`hit_*`**: bindings de input que pulam para frames (entrada de golpes).
- **`bdy`**: hurtbox/corpo; **`itr`**: hitbox/dano/efeitos; **`opoint`**: spawn de objeto (projétil/weapon/etc.).

> Observação: os `.dat` deste pacote estão **criptografados/encodados** (formato clássico do LF2). A geração deste manual já inclui a decriptação antes do parse.

---

## Personagem 9: Dennis
<a id="personagem-9-dennis"></a>

- **Arquivo (.dat):** `dennis.dat`
- **Head (seleção):** `sprite\sys\dennis_f.bmp`
- **Small (HUD):** `sprite\sys\dennis_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\dennis_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\dennis_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\dennis_2.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 280 |
| 0 | `standing` | `hit_Ua` | 295 |
| 0 | `standing` | `hit_Da` | 265 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 280 |
| 1 | `standing` | `hit_Ua` | 295 |
| 1 | `standing` | `hit_Da` | 265 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 280 |
| 2 | `standing` | `hit_Ua` | 295 |
| 2 | `standing` | `hit_Da` | 265 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 280 |
| 3 | `standing` | `hit_Ua` | 295 |
| 3 | `standing` | `hit_Da` | 265 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 280 |
| 5 | `walking` | `hit_Ua` | 295 |
| 5 | `walking` | `hit_Da` | 265 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 280 |
| 6 | `walking` | `hit_Ua` | 295 |
| 6 | `walking` | `hit_Da` | 265 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 280 |
| 7 | `walking` | `hit_Ua` | 295 |
| 7 | `walking` | `hit_Da` | 265 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 280 |
| 8 | `walking` | `hit_Ua` | 295 |
| 8 | `walking` | `hit_Da` | 265 |
| 75 | `super_punch` | `hit_Fj` | 280 |
| 75 | `super_punch` | `hit_Ua` | 295 |
| 75 | `super_punch` | `hit_Da` | 265 |
| 88 | `run_attack` | `hit_Fj` | 280 |
| 88 | `run_attack` | `hit_Ua` | 295 |
| 88 | `run_attack` | `hit_Da` | 265 |
| 89 | `run_attack` | `hit_a` | 70 |
| 89 | `run_attack` | `hit_Fj` | 280 |
| 89 | `run_attack` | `hit_Ua` | 295 |
| 89 | `run_attack` | `hit_Da` | 265 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 280 |
| 110 | `defend` | `hit_Ua` | 295 |
| 110 | `defend` | `hit_Da` | 265 |
| 121 | `catching` | `hit_Fj` | 280 |
| 121 | `catching` | `hit_Ua` | 295 |
| 121 | `catching` | `hit_Da` | 265 |
| 241 | `ball1` | `hit_a` | 242 |
| 246 | `ball2` | `hit_a` | 251 |
| 261 | `ball34` | `hit_a` | 262 |
| 284 | `c_foot` | `hit_j` | 288 |
| 284 | `c_foot` | `hit_d` | 288 |
| 285 | `c_foot` | `hit_j` | 288 |
| 285 | `c_foot` | `hit_d` | 288 |
| 286 | `c_foot` | `hit_j` | 288 |
| 286 | `c_foot` | `hit_d` | 288 |
| 287 | `c_foot` | `hit_j` | 288 |
| 287 | `c_foot` | `hit_d` | 288 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 5 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 6 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=5 y=15 w=45 h=65 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=33 h=69 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=13 w=33 h=66 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=25 y=11 w=36 h=68 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=9 w=36 h=66; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=21 y=11 w=30 h=69 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=11 w=32 h=68 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=18 y=10 w=36 h=71 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=25 y=11 w=34 h=68 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=26 y=7 w=32 h=58 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=26 y=7 w=26 h=39; kind=0 x=13 y=40 w=24 h=30 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=21 y=15 w=31 h=65 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=15 w=34 h=65 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=17 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=10 w=35 h=52 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=8 w=34 h=33 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=34 h=65; kind=0 x=9 y=36 w=17 h=18 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=22 w=32 h=57 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=38 w=54 h=21; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=15 y=9 w=29 h=54 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=35 y=10 w=36 h=23; kind=0 x=15 y=18 w=30 h=43 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=3 y=37 w=34 h=26 |  |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 1 | 62 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=16 y=19 w=33 h=59 | kind=0 x=21 y=30 w=54 h=23 (bdefend=30, dvx=2, fall=25, injury=30) |  |
| 62 | `punch` | 12 | 3 | 1 | 63 | 0 | 0 | 0 |  |  | kind=0 x=16 y=20 w=33 h=61 |  |  |
| 63 | `punch` | 13 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=9 w=36 h=71 |  |  |
| 65 | `punch` | 14 | 3 | 2 | 66 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=16 y=59 w=35 h=21 (vrest=1) |  |
| 66 | `punch` | 15 | 3 | 1 | 67 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=14 y=19 w=32 h=62 | kind=0 x=17 y=37 w=60 h=19 (bdefend=30, dvx=2, fall=25, injury=30) |  |
| 67 | `punch` | 16 | 3 | 1 | 68 | 0 | 0 | 0 |  |  | kind=0 x=17 y=15 w=37 h=64 |  |  |
| 68 | `punch` | 17 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=17 y=15 w=34 h=65 |  |  |
| 70 | `super_punch` | 8 | 3 | 2 | 71 | 3 | 0 | 0 |  | `data\007.wav` | kind=0 x=22 y=14 w=30 h=65 |  |  |
| 71 | `super_punch` | 9 | 3 | 1 | 72 | 5 | 0 | 0 |  |  | kind=0 x=20 y=12 w=26 h=65 |  |  |
| 72 | `super_punch` | 19 | 3 | 1 | 73 | 3 | 0 | 0 |  |  | kind=0 x=4 y=12 w=42 h=68 |  |  |
| 73 | `super_punch` | 29 | 3 | 3 | 74 | 3 | 0 | 0 |  |  | kind=0 x=6 y=20 w=37 h=55 | kind=0 x=-13 y=28 w=84 h=28 (arest=15, bdefend=60, dvx=15, dvy=-6, fall=70, injury=70) |  |
| 74 | `super_punch` | 39 | 3 | 1 | 75 | 3 | 0 | 0 |  |  | kind=0 x=8 y=17 w=36 h=61 |  |  |
| 75 | `super_punch` | 49 | 3 | 2 | 999 | 3 | 0 | 0 |  |  | kind=0 x=8 y=17 w=36 h=61 |  |  |
| 80 | `jump_attack` | 37 | 3 | 2 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=17 w=43 h=52 |  |  |
| 81 | `jump_attack` | 38 | 3 | 5 | 82 | 0 | 0 | 0 |  |  | kind=0 x=10 y=19 w=43 h=49 | kind=0 x=19 y=40 w=58 h=17 (arest=15, bdefend=30, dvx=7, fall=70, injury=35) |  |
| 82 | `jump_attack` | 37 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=17 w=40 h=52 |  |  |
| 85 | `run_attack` | 102 | 3 | 1 | 86 | 6 | 0 | 0 |  |  | kind=0 x=11 y=17 w=37 h=62 |  |  |
| 86 | `run_attack` | 103 | 3 | 1 | 87 | 4 | 0 | 0 |  | `data\007.wav` | kind=0 x=11 y=15 w=39 h=64 |  |  |
| 87 | `run_attack` | 104 | 3 | 1 | 88 | 0 | 0 | 0 |  |  | kind=0 x=10 y=16 w=38 h=62 | kind=0 x=9 y=38 w=68 h=18 (arest=15, bdefend=16, dvx=10, fall=60, injury=50) |  |
| 88 | `run_attack` | 105 | 3 | 1 | 89 | 0 | 0 | 0 |  |  | kind=0 x=12 y=16 w=34 h=63 | kind=0 x=33 y=41 w=42 h=25 (arest=15, bdefend=16, dvx=10, fall=60, injury=50) |  |
| 89 | `run_attack` | 106 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=11 w=36 h=67 |  |  |
| 90 | `dash_attack` | 107 | 15 | 2 | 91 | 0 | 0 | 0 |  |  | kind=0 x=15 y=20 w=47 h=51 |  |  |
| 91 | `dash_attack` | 108 | 15 | 6 | 92 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=11 y=20 w=41 h=48 | kind=0 x=24 y=23 w=55 h=39 (arest=20, bdefend=60, dvx=12, fall=70, injury=70) |  |
| 92 | `dash_attack` | 109 | 15 | 4 | 216 | 0 | 0 | 0 |  |  | kind=0 x=15 y=22 w=55 h=42 |  |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=293 y=198 w=1 h=1 (vrest=1) |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 4 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=8 y=15 w=30 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=27 y=35 w=28 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 67 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 68 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 67 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 78 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 79 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 88 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 89 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=29 h=61 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=25 y=13 w=30 h=65 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 235 | `ball1` | 140 | 3 | 1 | 236 | 0 | 0 | 0 | 40 |  | kind=0 x=26 y=12 w=27 h=68 |  |  |
| 236 | `ball1` | 141 | 3 | 1 | 237 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 |  |  |
| 237 | `ball1` | 142 | 3 | 1 | 238 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=26 y=12 w=27 h=68 |  |  |
| 238 | `ball1` | 143 | 3 | 1 | 239 | 0 | 0 | 0 |  | `data\046.wav` | kind=0 x=29 y=19 w=39 h=62 |  |  |
| 239 | `ball1` | 144 | 3 | 1 | 240 | 0 | 0 | 0 |  |  | kind=0 x=33 y=21 w=36 h=60 |  | id 205 (type 3): data\dennis_ball.dat |
| 240 | `ball1` | 145 | 3 | 1 | 241 | 0 | 0 | 0 |  |  | kind=0 x=34 y=19 w=36 h=60 |  |  |
| 241 | `ball1` | 145 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=30 y=20 w=39 h=59 |  |  |
| 242 | `ball2` | 146 | 3 | 1 | 243 | 0 | 0 | 0 | 40 |  | kind=0 x=31 y=20 w=39 h=58 |  |  |
| 243 | `ball2` | 147 | 3 | 1 | 244 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=29 y=18 w=41 h=60 |  |  |
| 244 | `ball2` | 148 | 3 | 1 | 245 | 0 | 0 | 0 |  | `data\046.wav` | kind=0 x=26 y=19 w=34 h=58 |  |  |
| 245 | `ball2` | 149 | 3 | 1 | 246 | 0 | 0 | 0 |  |  | kind=0 x=30 y=13 w=30 h=64 |  | id 205 (type 3): data\dennis_ball.dat |
| 246 | `ball2` | 150 | 3 | 1 | 247 | 0 | 0 | 0 |  |  | kind=0 x=29 y=13 w=30 h=68 |  |  |
| 247 | `ball2` | 150 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=32 h=66 |  |  |
| 251 | `ball34` | 151 | 3 | 1 | 252 | 0 | 0 | 0 | 70 |  | kind=0 x=32 y=15 w=30 h=64 |  |  |
| 252 | `ball34` | 152 | 3 | 1 | 253 | 0 | 0 | 0 |  |  | kind=0 x=23 y=13 w=34 h=67 |  |  |
| 253 | `ball34` | 153 | 3 | 1 | 254 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=26 y=12 w=27 h=68 |  |  |
| 254 | `ball34` | 154 | 3 | 1 | 255 | 0 | 0 | 0 |  | `data\046.wav` | kind=0 x=33 y=12 w=35 h=68 |  |  |
| 255 | `ball34` | 155 | 3 | 1 | 256 | 0 | 0 | 0 |  |  | kind=0 x=31 y=15 w=36 h=65 |  | id 205 (type 3): data\dennis_ball.dat |
| 256 | `ball34` | 156 | 3 | 1 | 257 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=22 y=19 w=39 h=61 |  |  |
| 257 | `ball34` | 157 | 3 | 1 | 258 | 0 | 0 | 0 |  | `data\046.wav` | kind=0 x=21 y=29 w=42 h=49 |  |  |
| 258 | `ball34` | 158 | 3 | 1 | 259 | 0 | 0 | 0 |  |  | kind=0 x=21 y=29 w=42 h=49 |  | id 205 (type 3): data\dennis_ball.dat |
| 259 | `ball34` | 159 | 3 | 1 | 260 | 0 | 0 | 0 |  |  | kind=0 x=23 y=27 w=37 h=51 |  |  |
| 260 | `ball34` | 160 | 3 | 1 | 261 | 0 | 0 | 0 |  |  | kind=0 x=19 y=19 w=36 h=61 |  |  |
| 261 | `ball34` | 161 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=16 w=30 h=64 |  |  |
| 262 | `ball34` | 161 | 3 | 1 | 235 | 0 | 0 | 0 |  |  | kind=0 x=18 y=17 w=33 h=63 |  |  |
| 265 | `many_foot` | 13 | 3 | 1 | 266 | 2 | 0 | 0 | 75 |  | kind=0 x=18 y=17 w=33 h=63 |  |  |
| 266 | `many_foot` | 162 | 3 | 1 | 267 | 2 | 0 | 0 |  | `data\007.wav` | kind=0 x=9 y=14 w=35 h=65 | kind=0 x=12 y=51 w=68 h=17 (bdefend=16, dvx=2, fall=1, injury=27, vrest=7) |  |
| 267 | `many_foot` | 163 | 3 | 1 | 268 | 2 | 0 | 0 |  |  | kind=0 x=18 y=17 w=33 h=63 | kind=0 x=49 y=9 w=18 h=64 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 268 | `many_foot` | 164 | 3 | 1 | 269 | 2 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=18 w=39 h=59 | kind=0 x=21 y=43 w=63 h=15 (bdefend=16, dvx=2, fall=1, injury=27, vrest=7) |  |
| 269 | `many_foot` | 165 | 3 | 1 | 270 | 2 | 0 | 0 |  |  | kind=0 x=15 y=18 w=43 h=61 | kind=0 x=49 y=9 w=18 h=64 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 270 | `many_foot` | 166 | 3 | 1 | 271 | 2 | 0 | 0 |  | `data\007.wav` | kind=0 x=13 y=19 w=32 h=62 | kind=0 x=17 y=35 w=61 h=13 (bdefend=16, dvx=2, fall=1, injury=27, vrest=7) |  |
| 271 | `many_foot` | 167 | 3 | 1 | 272 | 2 | 0 | 0 |  |  | kind=0 x=18 y=17 w=33 h=63 | kind=0 x=49 y=9 w=18 h=64 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 272 | `many_foot` | 168 | 3 | 1 | 273 | 2 | 0 | 0 |  | `data\007.wav` | kind=0 x=9 y=14 w=36 h=65 | kind=0 x=49 y=9 w=18 h=64 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 273 | `many_foot` | 169 | 3 | 1 | 274 | 2 | 0 | 0 |  |  | kind=0 x=0 y=15 w=35 h=66 | kind=0 x=11 y=20 w=56 h=60 (bdefend=60, dvx=15, dvy=5, fall=70, injury=27, vrest=15) |  |
| 274 | `many_foot` | 179 | 3 | 1 | 275 | 0 | 0 | 0 |  |  | kind=0 x=3 y=17 w=40 h=64 |  |  |
| 275 | `many_foot` | 178 | 3 | 1 | 276 | 0 | 0 | 0 |  |  | kind=0 x=9 y=17 w=39 h=66 |  |  |
| 276 | `many_foot` | 178 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=17 w=33 h=63 |  |  |
| 280 | `c_foot` | 170 | 3 | 1 | 281 | 0 | 0 | 0 | 75 |  | kind=0 x=24 y=21 w=32 h=58 |  |  |
| 281 | `c_foot` | 171 | 3 | 1 | 282 | 0 | 0 | 0 |  |  | kind=0 x=16 y=12 w=31 h=62 |  |  |
| 282 | `c_foot` | 172 | 3 | 1 | 283 | 1 | 0 | 0 |  |  | kind=0 x=13 y=3 w=41 h=37 | kind=0 x=72 y=5 w=18 h=73 (bdefend=16, dvx=2, effect=4, fall=70, injury=45, vrest=7); kind=0 x=20 y=13 w=64 h=58 (bdefend=16, dvx=13, fall=20, injury=35, vrest=7) |  |
| 283 | `c_foot` | 173 | 3 | 1 | 284 | 2 | 0 | 0 |  |  | kind=0 x=14 y=5 w=37 h=37 | kind=0 x=47 y=12 w=17 h=60 (bdefend=16, dvx=12, effect=4, fall=20, injury=45, vrest=7) |  |
| 284 | `c_foot` | 174 | 3 | 1 | 285 | 10 | 0 | 1 |  | `data\007.wav` | kind=0 x=28 y=9 w=32 h=35 | kind=0 x=-1 y=9 w=70 h=71 (bdefend=16, dvx=18, effect=4, fall=70, injury=45, vrest=7); kind=0 x=3 y=11 w=62 h=65 (bdefend=16, dvx=-4, fall=20, injury=35, vrest=7) |  |
| 285 | `c_foot` | 175 | 3 | 1 | 286 | 10 | 0 | 2 |  |  | kind=0 x=24 y=6 w=31 h=35 | kind=0 x=49 y=9 w=18 h=64 (bdefend=16, dvx=18, effect=4, fall=70, injury=45, vrest=7) |  |
| 286 | `c_foot` | 176 | 3 | 1 | 287 | 10 | 0 | 1 | -17 |  | kind=0 x=18 y=11 w=32 h=30 | kind=0 x=77 y=8 w=14 h=64 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7); kind=0 x=25 y=13 w=61 h=56 (bdefend=16, dvx=13, fall=20, injury=35, vrest=7) |  |
| 287 | `c_foot` | 173 | 3 | 1 | 284 | 10 | 0 | 2 | -17 |  | kind=0 x=22 y=11 w=31 h=32 | kind=0 x=52 y=5 w=11 h=70 (bdefend=16, dvx=12, effect=4, fall=20, injury=45, vrest=7) |  |
| 288 | `c_foot` | 177 | 3 | 1 | 289 | 3 | 0 | 0 |  |  | kind=0 x=18 y=10 w=37 h=30 |  |  |
| 289 | `c_foot` | 180 | 3 | 1 | 290 | 2 | 0 | 0 |  |  | kind=0 x=26 y=19 w=34 h=61 |  |  |
| 290 | `c_foot` | 178 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 295 | `chase_ball` | 181 | 3 | 2 | 296 | 0 | 0 | 0 | 100 |  | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 296 | `chase_ball` | 182 | 3 | 2 | 297 | 0 | 0 | 0 |  | `data\077.wav` | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 297 | `chase_ball` | 183 | 3 | 2 | 298 | 0 | 0 | 0 |  |  | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 298 | `chase_ball` | 184 | 3 | 1 | 299 | 6 | 0 | 0 |  |  | kind=0 x=12 y=20 w=36 h=60 |  | id 215 (type 3): data\dennis_chase.dat |
| 299 | `chase_ball` | 185 | 3 | 4 | 300 | 0 | 0 | 0 |  |  | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 300 | `chase_ball` | 186 | 3 | 2 | 301 | 0 | 0 | 0 |  |  | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 301 | `chase_ball` | 187 | 3 | 2 | 999 | 3 | 0 | 0 |  |  | kind=0 x=12 y=20 w=36 h=60 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 30: Bandit
<a id="personagem-30-bandit"></a>

- **Arquivo (.dat):** `bandit.dat`
- **Head (seleção):** `sprite\sys\bandit_f.bmp`
- **Small (HUD):** `sprite\sys\bandit_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\bandit_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\bandit_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\bandit_0b.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\bandit_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.
_Nenhum `hit_*` encontrado._

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 5 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 5 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=27 y=20 w=32 h=60 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=30 y=19 w=28 h=61 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=17 y=21 w=23 h=59; kind=0 x=4 y=64 w=14 h=15; kind=0 x=36 y=46 w=20 h=14 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=41 y=19 w=29 h=61; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=29 y=16 w=29 h=63 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=29 y=16 w=29 h=63 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=41 y=21 w=26 h=58; kind=0 x=27 y=61 w=18 h=17 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=23 w=31 h=55 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=27 y=17 w=39 h=56 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=11 y=32 w=33 h=43 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=28 y=16 w=34 h=64 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=29 y=38 w=51 h=20; kind=0 x=9 y=54 w=45 h=26 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=16 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=15 w=40 h=53 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=8 y=32 w=33 h=45 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 6 | 46 | 0 | 0 | 0 |  |  | kind=0 x=25 y=22 w=35 h=57; kind=0 x=10 y=40 w=20 h=13 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=22 w=32 h=57 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=38 w=54 h=21; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 6 | 51 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 10 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 6 | 53 | 0 | 0 | 0 |  |  | kind=0 x=22 y=10 w=40 h=52 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=35 y=10 w=36 h=23; kind=0 x=15 y=18 w=30 h=43 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=3 y=37 w=34 h=26 |  |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 1 | 0 | 0 |  |  | kind=0 x=30 y=18 w=28 h=60; kind=0 x=23 y=35 w=37 h=24 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=39 y=19 w=21 h=61; kind=0 x=51 y=34 w=26 h=16 | kind=0 x=49 y=33 w=30 h=16 (bdefend=16, dvx=2, injury=20) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 1 | 0 | 0 |  |  | kind=0 x=27 y=17 w=31 h=63 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=28 y=17 w=29 h=63; kind=0 x=45 y=37 w=32 h=11 | kind=0 x=45 y=34 w=33 h=14 (bdefend=16, dvx=2, injury=20) |  |
| 70 | `super_punch` | 37 | 3 | 2 | 71 | 0 | 0 | 0 |  |  | kind=0 x=22 y=14 w=30 h=65 |  |  |
| 71 | `super_punch` | 38 | 3 | 2 | 72 | 4 | 0 | 0 |  |  | kind=0 x=17 y=13 w=31 h=66 |  |  |
| 72 | `super_punch` | 39 | 3 | 2 | 73 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=17 y=31 w=32 h=48; kind=0 x=43 y=35 w=28 h=18 | kind=0 x=35 y=26 w=44 h=17 (arest=15, bdefend=60, dvx=13, dvy=-6, fall=70, injury=50) |  |
| 73 | `super_punch` | 104 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=17 y=31 w=32 h=48; kind=0 x=43 y=35 w=28 h=18 |  |  |
| 80 | `jump_attack` | 14 | 3 | 2 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=20 y=18 w=34 h=61 |  |  |
| 81 | `jump_attack` | 15 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=19 w=32 h=60; kind=0 x=45 y=20 w=28 h=27 | kind=0 x=37 y=30 w=36 h=20 (arest=15, bdefend=16, dvx=2, injury=35) |  |
| 85 | `run_attack` | 102 | 3 | 4 | 86 | 6 | 0 | 0 |  |  | kind=0 x=19 y=24 w=35 h=54; kind=0 x=10 y=36 w=27 h=15 |  |  |
| 86 | `run_attack` | 103 | 3 | 2 | 87 | 4 | 0 | 0 |  | `data\007.wav` | kind=0 x=24 y=36 w=55 h=19; kind=0 x=8 y=51 w=38 h=27 | kind=0 x=19 y=37 w=61 h=17 (arest=15, bdefend=16, dvx=10, fall=70, injury=50) |  |
| 87 | `run_attack` | 104 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=36 w=39 h=24; kind=0 x=2 y=57 w=41 h=22 |  |  |
| 90 | `dash_attack` | 106 | 15 | 3 | 91 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=25 h=55; kind=0 x=13 y=36 w=52 h=18 |  |  |
| 91 | `dash_attack` | 107 | 15 | 20 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=7 y=19 w=37 h=45; kind=0 x=22 y=36 w=50 h=18 | kind=0 x=27 y=38 w=53 h=23 (arest=20, bdefend=60, dvx=12, fall=70, injury=70) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 5 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 5 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 5 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 105 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 115 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 105 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 100 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 101 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 110 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 111 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=33 y=33 w=20 h=48; kind=0 x=25 y=50 w=15 h=29 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=15 y=41 w=48 h=20; kind=0 x=25 y=55 w=30 h=27 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=15 w=21 h=64; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=46 y=18 w=22 h=25; kind=0 x=28 y=29 w=21 h=33; kind=0 x=13 y=54 w=18 h=17 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=18 y=14 w=19 h=27 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=40 w=29 h=38 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=42 y=15 w=24 h=21; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=47 y=23 w=23 h=54; kind=0 x=29 y=41 w=20 h=37 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=46 y=23 w=21 h=56; kind=0 x=30 y=40 w=17 h=41 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 7 | 233 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 10 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 31: Hunter
<a id="personagem-31-hunter"></a>

- **Arquivo (.dat):** `hunter.dat`
- **Head (seleção):** `sprite\sys\hunter_f.bmp`
- **Small (HUD):** `sprite\sys\hunter_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\hunter_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\hunter_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\hunter_0b.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\hunter_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 214 | `dash` | `hit_a` | 81 |
| 217 | `dash` | `hit_a` | 81 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 4 | 1 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=34 h=67 |  |  |
| 1 | `standing` | 1 | 0 | 4 | 2 | 0 | 0 | 0 |  |  | kind=0 x=23 y=9 w=32 h=67 |  |  |
| 2 | `standing` | 2 | 0 | 4 | 3 | 0 | 0 | 0 |  |  | kind=0 x=24 y=9 w=31 h=70 |  |  |
| 3 | `standing` | 3 | 0 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=6 w=31 h=71 |  | id 0 (type None): bg\sys\thv\bg.dat |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=7 w=31 h=72 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=7 w=26 h=72 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=26 y=7 w=30 h=72 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=6 w=28 h=74 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=11 w=39 h=68 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=10 w=42 h=68 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=14 w=32 h=64 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=8 w=29 h=74 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=6 w=28 h=72 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=3 w=25 h=76 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=36 y=6 w=26 h=74 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=22 y=10 w=30 h=68 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=7 w=32 h=71 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=4 w=28 h=75 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=13 y=16 w=40 h=62 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=21 y=14 w=34 h=65 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=12 w=28 h=67 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=7 y=12 w=40 h=68 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=12 w=34 h=69; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=19 y=8 w=33 h=72 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=12 w=27 h=67 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=9 y=15 w=32 h=64 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=15 w=34 h=64 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=28 h=58 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=10 w=23 h=45; kind=0 x=13 y=38 w=24 h=33 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=31 y=8 w=28 h=72 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=13 y=9 w=41 h=70 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=36 h=65 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=15 y=15 w=27 h=56 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=8 w=34 h=33 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=34 h=65; kind=0 x=9 y=36 w=17 h=18 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=22 w=32 h=57 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=38 w=54 h=21; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 49 | `throw_lying_man` | 28 | 15 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 | 0 | 0 | 0 |  |  | kind=0 x=19 y=17 w=30 h=65 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 2 | 53 | 0 | 0 | 0 |  |  | kind=0 x=15 y=9 w=29 h=54 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=20 y=7 w=31 h=66 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=3 y=37 w=34 h=26 |  |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=15 y=12 w=37 h=67 |  |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=16 y=20 w=36 h=59 |  |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=17 y=17 w=32 h=63 |  |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=36 h=65 |  |  |
| 60 | `punch` | 10 | 3 | 1 | 61 | 0 | 0 | 0 | 12 |  | kind=0 x=28 y=13 w=33 h=63 | kind=2 x=18 y=60 w=44 h=18 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 1 | 62 | 0 | 0 | 0 |  |  | kind=0 x=15 y=5 w=35 h=73 |  |  |
| 62 | `punch` | 12 | 3 | 2 | 63 | 0 | 0 | 0 |  | `data\022.wav` | kind=0 x=9 y=9 w=35 h=71; kind=0 x=23 y=36 w=41 h=16 |  |  |
| 63 | `punch` | 13 | 3 | 3 | 64 | 0 | 0 | 0 |  |  | kind=0 x=24 y=10 w=33 h=68; kind=0 x=28 y=34 w=42 h=19 |  |  |
| 64 | `punch` | 14 | 3 | 1 | 66 | 0 | 0 | 0 |  | `data\024.wav` | kind=0 x=22 y=7 w=35 h=71; kind=0 x=26 y=22 w=47 h=29 |  | id 201 (type 1): data\henry_arrow1.dat |
| 65 | `punch` | 10 | 3 | 1 | 61 | 0 | 0 | 0 | 12 |  | kind=0 x=28 y=13 w=33 h=63 | kind=2 x=18 y=60 w=44 h=18 (vrest=1) |  |
| 66 | `punch` | 15 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=23 y=9 w=33 h=70; kind=0 x=15 y=32 w=51 h=15 |  |  |
| 70 | `super_punch` | 37 | 3 | 3 | 71 | 0 | 0 | 0 |  |  | kind=0 x=20 y=8 w=35 h=71 |  |  |
| 71 | `super_punch` | 29 | 3 | 1 | 72 | 4 | 0 | 0 |  |  | kind=0 x=3 y=12 w=36 h=67 | kind=0 x=32 y=3 w=43 h=50 (bdefend=16, dvx=10, fall=60, injury=35, vrest=15) |  |
| 72 | `super_punch` | 39 | 3 | 2 | 73 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=5 y=27 w=43 h=52 | kind=0 x=34 y=59 w=48 h=15 (bdefend=16, dvx=10, fall=60, injury=35, vrest=15) |  |
| 73 | `super_punch` | 39 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=19 w=37 h=61 |  |  |
| 80 | `jump_attack` | 8 | 3 | 1 | 81 | 0 | 0 | 0 | 20 |  | kind=0 x=19 y=10 w=39 h=57 |  |  |
| 81 | `jump_attack` | 9 | 3 | 1 | 82 | 0 | 0 | 0 | 20 |  | kind=0 x=19 y=12 w=38 h=59 |  |  |
| 82 | `jump_attack` | 49 | 3 | 2 | 83 | 0 | 0 | 0 |  | `data\022.wav` | kind=0 x=18 y=12 w=38 h=58 |  |  |
| 83 | `jump_attack` | 58 | 3 | 1 | 84 | 0 | -3 | 0 |  |  | kind=0 x=12 y=7 w=44 h=63 |  |  |
| 84 | `jump_attack` | 59 | 3 | 11 | 999 | 0 | 0 | 0 |  | `data\024.wav` | kind=0 x=15 y=10 w=42 h=63 |  | id 201 (type 1): data\henry_arrow1.dat |
| 85 | `run_attack` | 37 | 3 | 1 | 86 | 5 | 0 | 0 |  |  | kind=0 x=20 y=8 w=35 h=71 |  |  |
| 86 | `run_attack` | 29 | 3 | 2 | 87 | 4 | 0 | 0 |  |  | kind=0 x=3 y=12 w=36 h=67 | kind=0 x=34 y=2 w=44 h=77 (bdefend=16, dvx=10, fall=60, injury=35, vrest=15) |  |
| 87 | `run_attack` | 39 | 3 | 3 | 88 | 4 | 0 | 0 |  | `data\007.wav` | kind=0 x=5 y=27 w=43 h=52 | kind=0 x=43 y=57 w=42 h=17 (bdefend=16, dvx=10, fall=60, injury=20, vrest=15) |  |
| 88 | `run_attack` | 39 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=19 w=37 h=61 |  |  |
| 90 | `dash_attack` | 106 | 15 | 1 | 91 | 0 | 0 | 0 |  |  | kind=0 x=20 y=3 w=31 h=74 |  |  |
| 91 | `dash_attack` | 107 | 15 | 2 | 92 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=9 y=7 w=33 h=70 | kind=0 x=27 y=3 w=50 h=57 (bdefend=60, dvx=12, fall=60, injury=45, vrest=20) |  |
| 92 | `dash_attack` | 108 | 15 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=9 y=7 w=33 h=70 |  |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 18 | 6 | 2 | 103 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 16 | 6 | 2 | 104 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 17 | 6 | 2 | 105 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 18 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 16 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 107 | `rowing` | 17 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=28 y=19 w=32 h=58 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 3 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=30 y=23 w=30 h=54 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=30 y=23 w=29 h=54 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 78 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 79 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 78 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 87 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 88 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 104 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 105 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=18 y=26 w=30 h=54 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=15 y=27 w=35 h=54 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=3 w=23 h=65; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=19 y=23 w=27 h=59 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=18 y=21 w=29 h=59 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=25 y=18 w=31 h=61; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 1 | 49 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 32: Mark
<a id="personagem-32-mark"></a>

- **Arquivo (.dat):** `mark.dat`
- **Head (seleção):** `sprite\sys\mark_f.bmp`
- **Small (HUD):** `sprite\sys\mark_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\mark_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\mark_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\mark_0b.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\mark_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 85 |
| 0 | `standing` | `hit_Fj` | 240 |
| 1 | `standing` | `hit_Fa` | 85 |
| 1 | `standing` | `hit_Fj` | 240 |
| 2 | `standing` | `hit_Fa` | 85 |
| 2 | `standing` | `hit_Fj` | 240 |
| 3 | `standing` | `hit_Fa` | 85 |
| 3 | `standing` | `hit_Fj` | 240 |
| 5 | `walking` | `hit_Fa` | 85 |
| 5 | `walking` | `hit_Fj` | 240 |
| 6 | `walking` | `hit_Fa` | 85 |
| 6 | `walking` | `hit_Fj` | 240 |
| 7 | `walking` | `hit_Fa` | 85 |
| 7 | `walking` | `hit_Fj` | 240 |
| 8 | `walking` | `hit_Fa` | 85 |
| 8 | `walking` | `hit_Fj` | 240 |
| 87 | `run_attack` | `hit_a` | 71 |
| 88 | `run_attack` | `hit_a` | 71 |
| 89 | `run_attack` | `hit_a` | 71 |
| 110 | `defend` | `hit_Fa` | 85 |
| 110 | `defend` | `hit_Fj` | 240 |
| 111 | `defend` | `hit_Fa` | 85 |
| 111 | `defend` | `hit_Fj` | 240 |
| 240 | `run` | `hit_j` | 245 |
| 240 | `run` | `hit_d` | 245 |
| 241 | `run` | `hit_j` | 245 |
| 241 | `run` | `hit_d` | 245 |
| 242 | `run` | `hit_j` | 245 |
| 242 | `run` | `hit_d` | 245 |
| 243 | `run` | `hit_j` | 245 |
| 243 | `run` | `hit_d` | 245 |
| 244 | `run` | `hit_j` | 245 |
| 244 | `run` | `hit_d` | 245 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 3 | 1 | 0 | 0 | 0 |  |  | kind=0 x=17 y=5 w=48 h=73 |  |  |
| 1 | `standing` | 1 | 0 | 3 | 2 | 0 | 0 | 0 |  |  | kind=0 x=19 y=1 w=46 h=78 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=19 y=1 w=45 h=79 |  |  |
| 3 | `standing` | 3 | 0 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=17 y=1 w=47 h=79 |  |  |
| 5 | `walking` | 4 | 1 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=2 w=41 h=77 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=2 w=40 h=77 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=2 w=42 h=77 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=16 y=3 w=45 h=76 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=13 y=3 w=48 h=72 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=4 w=43 h=71 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=21 y=4 w=43 h=78 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=17 y=1 w=46 h=81 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=0 w=41 h=78 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=-1 w=38 h=79 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=-1 w=35 h=78 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=17 y=-2 w=36 h=79 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=-2 w=37 h=79 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=18 y=-2 w=41 h=79 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=13 y=-2 w=35 h=80 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=25 y=-3 w=34 h=83 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=2 w=33 h=78 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=24 y=1 w=35 h=78 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=6 w=41 h=72; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=14 y=5 w=36 h=74 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=13 y=6 w=38 h=73 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=24 y=7 w=37 h=72 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=6 w=41 h=73 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=27 y=5 w=37 h=67 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=8 w=40 h=65 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=23 y=9 w=38 h=60 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=25 y=8 w=38 h=63 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=25 y=5 w=40 h=74 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=14 y=6 w=44 h=50; kind=0 x=9 y=54 w=45 h=26 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=7 w=39 h=74 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 2 | 41 | 0 | 0 | 0 |  |  | kind=0 x=26 y=9 w=37 h=68 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=3 w=37 h=72 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=26 y=9 w=35 h=65 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=26 y=6 w=40 h=67 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 | 0 | 0 | 0 |  |  | kind=0 x=28 y=5 w=31 h=73 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=3 w=35 h=76 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=27 y=5 w=41 h=54; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 3 | 51 | 0 | 0 | 0 |  |  | kind=0 x=20 y=0 w=35 h=79 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=29 y=1 w=40 h=60; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 2 | 53 | 0 | 0 | 0 |  |  | kind=0 x=22 y=10 w=40 h=52 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=24 y=5 w=31 h=28; kind=0 x=21 y=25 w=33 h=44 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=22 y=43 w=39 h=25 |  |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=22 y=4 w=36 h=76 |  |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=24 y=3 w=34 h=77 |  |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=23 y=3 w=35 h=80 |  |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=23 y=2 w=33 h=80 |  |  |
| 60 | `punch` | 10 | 3 | 3 | 61 | 2 | 0 | 0 |  |  | kind=0 x=25 y=8 w=36 h=31; kind=0 x=23 y=35 w=37 h=24 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=19 y=7 w=27 h=74; kind=0 x=33 y=25 w=21 h=12 | kind=0 x=23 y=14 w=49 h=37 (bdefend=30, dvx=2, fall=25, injury=35, vrest=8) |  |
| 65 | `punch` | 12 | 3 | 3 | 66 | 2 | 0 | 0 |  |  | kind=0 x=14 y=2 w=30 h=75 | kind=2 x=8 y=59 w=38 h=20 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=15 y=9 w=34 h=69; kind=0 x=37 y=21 w=27 h=14 | kind=0 x=21 y=6 w=47 h=43 (bdefend=30, dvx=2, fall=25, injury=35, vrest=8) |  |
| 70 | `super_punch` | 37 | 3 | 5 | 86 | 3 | 0 | 0 |  |  | kind=0 x=19 y=24 w=35 h=54; kind=0 x=10 y=6 w=41 h=38 |  |  |
| 71 | `super_punch` | 19 | 3 | 5 | 72 | 8 | 0 | 0 |  |  | kind=0 x=20 y=13 w=35 h=38; kind=0 x=3 y=50 w=52 h=29 |  |  |
| 72 | `run_attack` | 18 | 3 | 1 | 73 | 4 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=18 w=45 h=38; kind=0 x=8 y=51 w=38 h=27 | kind=0 x=10 y=23 w=68 h=31 (bdefend=60, dvx=8, dvy=-10, fall=70, injury=35, vrest=15) |  |
| 73 | `run_attack` | 9 | 3 | 3 | 74 | 0 | 0 | 0 |  |  | kind=0 x=12 y=6 w=35 h=55; kind=0 x=7 y=55 w=45 h=23 | kind=0 x=19 y=4 w=51 h=39 (bdefend=60, dvx=8, dvy=-10, fall=70, injury=35, vrest=15) |  |
| 74 | `run_attack` | 8 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=4 w=34 h=56; kind=0 x=8 y=52 w=46 h=28 |  |  |
| 80 | `jump_attack` | 14 | 3 | 1 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=20 y=18 w=34 h=61 |  |  |
| 81 | `jump_attack` | 15 | 3 | 1 | 82 | 0 | 0 | 0 |  |  | kind=0 x=22 y=19 w=32 h=60; kind=0 x=20 y=8 w=23 h=20 | kind=0 x=30 y=42 w=36 h=37 (bdefend=60, dvx=9, dvy=-9, fall=70, injury=50, vrest=15) |  |
| 82 | `jump_attack` | 16 | 3 | 16 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=19 w=32 h=60; kind=0 x=17 y=7 w=28 h=24 | kind=0 x=25 y=29 w=51 h=40 (bdefend=60, dvx=9, dvy=-9, fall=70, injury=50, vrest=15) |  |
| 85 | `run_attack` | 37 | 3 | 3 | 86 | 3 | 0 | 0 |  |  | kind=0 x=19 y=24 w=35 h=54; kind=0 x=12 y=8 w=32 h=32 |  |  |
| 86 | `run_attack` | 38 | 3 | 1 | 87 | 3 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=18 w=45 h=38; kind=0 x=8 y=51 w=38 h=27 | kind=0 x=5 y=15 w=80 h=45 (bdefend=60, dvx=7, dvy=-9, fall=70, injury=45, vrest=10) |  |
| 87 | `run_attack` | 39 | 3 | 3 | 88 | 0 | 0 | 0 |  |  | kind=0 x=20 y=22 w=47 h=37; kind=0 x=2 y=57 w=41 h=22 | kind=0 x=11 y=24 w=69 h=43 (bdefend=60, dvx=7, dvy=-9, fall=70, injury=45, vrest=10) |  |
| 88 | `run_attack` | 29 | 3 | 1 | 89 | 0 | 0 | 0 |  |  | kind=0 x=21 y=19 w=48 h=43; kind=0 x=2 y=57 w=41 h=22 |  |  |
| 89 | `run_attack` | 29 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=19 w=48 h=43; kind=0 x=2 y=57 w=41 h=22 |  |  |
| 90 | `dash_attack` | 106 | 15 | 1 | 91 | 0 | 0 | 0 |  |  | kind=0 x=30 y=35 w=27 h=37; kind=0 x=18 y=6 w=35 h=42 |  |  |
| 91 | `dash_attack` | 107 | 15 | 20 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=4 w=25 h=68; kind=0 x=27 y=39 w=40 h=20 | kind=0 x=27 y=38 w=53 h=23 (bdefend=60, dvx=12, fall=70, injury=80, vrest=20) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=17 y=4 w=36 h=75 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=11 y=0 w=42 h=78 |  |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=25 w=39 h=50 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=23 y=22 w=38 h=54 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=23 y=22 w=38 h=59 |  |  |
| 120 | `catching` | 51 | 9 | 5 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 2 | 124 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 124 | `catching` | 52 | 9 | 1 | 121 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 | kind=0 x=10 y=22 w=76 h=21 (bdefend=16, dvx=2, injury=35, vrest=8) |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 105 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 115 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 105 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 100 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 101 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 110 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 111 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=33 y=33 w=20 h=48; kind=0 x=25 y=50 w=15 h=29 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=15 y=41 w=48 h=20; kind=0 x=25 y=55 w=30 h=27 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=6 w=33 h=61; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 0 | 0 | 0 | 0 |  |  | kind=0 x=32 y=6 w=26 h=27; kind=0 x=27 y=29 w=36 h=25; kind=0 x=29 y=47 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 0 | 0 | 0 | 0 |  |  | kind=0 x=17 y=6 w=37 h=49 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=40 w=29 h=38 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=16 y=7 w=38 h=36 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=47 y=23 w=23 h=54; kind=0 x=29 y=41 w=20 h=37 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=46 y=23 w=21 h=56; kind=0 x=30 y=40 w=17 h=41 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 4 | 233 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=24 y=7 w=44 h=48; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=31 y=11 w=38 h=47; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `run` | 37 | 3 | 4 | 241 | 2 | 0 | 2 | 50 |  | kind=0 x=19 y=11 w=27 h=65 |  |  |
| 241 | `run` | 102 | 3 | 2 | 242 | 9 | 0 | 2 | -27 |  | kind=0 x=19 y=11 w=27 h=65 | kind=0 x=27 y=10 w=39 h=67 (bdefend=60, dvx=16, fall=70, injury=80, vrest=20) |  |
| 242 | `run` | 103 | 3 | 2 | 243 | 9 | 0 | 2 | -27 | `data\003.wav` | kind=0 x=18 y=9 w=29 h=68 | kind=0 x=28 y=7 w=41 h=70 (bdefend=60, dvx=16, fall=70, injury=80, vrest=20) |  |
| 243 | `run` | 104 | 3 | 2 | 244 | 9 | 0 | 2 | -27 |  | kind=0 x=24 y=5 w=24 h=75 | kind=0 x=28 y=7 w=40 h=73 (bdefend=60, dvx=16, fall=70, injury=80, vrest=20) |  |
| 244 | `run` | 103 | 3 | 2 | 241 | 9 | 0 | 2 | -27 | `data\004.wav` | kind=0 x=21 y=5 w=31 h=73 | kind=0 x=28 y=5 w=40 h=72 (bdefend=60, dvx=16, fall=70, injury=80, vrest=20) |  |
| 245 | `stop_running` | 114 | 15 | 1 | 246 | 1 | 0 | 0 |  |  | kind=0 x=17 y=25 w=30 h=55; kind=0 x=16 y=7 w=38 h=36 |  |  |
| 246 | `stop_running` | 114 | 15 | 4 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=16 y=7 w=38 h=36 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 33: Jack
<a id="personagem-33-jack"></a>

- **Arquivo (.dat):** `jack.dat`
- **Head (seleção):** `sprite\sys\jack_f.bmp`
- **Small (HUD):** `sprite\sys\jack_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\jack_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\jack_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\jack_0b.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\jack_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 240 |
| 0 | `standing` | `hit_Ua` | 300 |
| 1 | `standing` | `hit_Fa` | 240 |
| 1 | `standing` | `hit_Ua` | 300 |
| 2 | `standing` | `hit_Fa` | 240 |
| 2 | `standing` | `hit_Ua` | 300 |
| 3 | `standing` | `hit_Fa` | 240 |
| 3 | `standing` | `hit_Ua` | 300 |
| 5 | `walking` | `hit_Fa` | 240 |
| 5 | `walking` | `hit_Ua` | 300 |
| 6 | `walking` | `hit_Fa` | 240 |
| 6 | `walking` | `hit_Ua` | 300 |
| 7 | `walking` | `hit_Fa` | 240 |
| 7 | `walking` | `hit_Ua` | 300 |
| 8 | `walking` | `hit_Fa` | 240 |
| 8 | `walking` | `hit_Ua` | 300 |
| 9 | `running` | `hit_Fa` | 240 |
| 9 | `running` | `hit_Ua` | 300 |
| 10 | `running` | `hit_Fa` | 240 |
| 10 | `running` | `hit_Ua` | 300 |
| 11 | `running` | `hit_Fa` | 240 |
| 11 | `running` | `hit_Ua` | 300 |
| 102 | `rowing` | `hit_Ua` | 300 |
| 103 | `rowing` | `hit_Ua` | 300 |
| 104 | `rowing` | `hit_Ua` | 300 |
| 105 | `rowing` | `hit_Ua` | 300 |
| 106 | `rowing` | `hit_Ua` | 300 |
| 107 | `rowing` | `hit_Ua` | 300 |
| 110 | `defend` | `hit_Fa` | 240 |
| 110 | `defend` | `hit_Ua` | 300 |
| 121 | `catching` | `hit_Ua` | 300 |
| 245 | `ball1` | `hit_Fa` | 242 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 5 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 3 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=28 y=16 w=27 h=64 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=33 y=10 w=26 h=67 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=31 y=8 w=29 h=71 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=25 y=11 w=36 h=68 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=9 w=36 h=66; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=21 y=11 w=30 h=69 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=26 y=11 w=31 h=69 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=18 y=10 w=36 h=71 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=25 y=11 w=34 h=68 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=26 y=7 w=32 h=58 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=27 y=8 w=34 h=59 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=20 y=35 w=28 h=35; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=30 w=28 h=38; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=21 y=15 w=31 h=65 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=15 w=34 h=65 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=11 w=31 h=68 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=10 w=35 h=52 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=31 w=30 h=37; kind=0 x=23 y=8 w=34 h=33 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=21 y=35 w=27 h=38; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=16 y=39 w=33 h=33; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=29 y=11 w=32 h=68; kind=0 x=17 y=40 w=24 h=10 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=32 y=8 w=27 h=71 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=17 y=18 w=48 h=33; kind=0 x=27 y=49 w=23 h=29 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 | 0 | 0 | 0 |  |  | kind=0 x=29 y=7 w=22 h=73 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=28 y=4 w=31 h=63 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=35 y=10 w=36 h=23; kind=0 x=28 y=30 w=32 h=39 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=19 y=37 w=26 h=29 |  |  |
| 55 | `weapon_drink` | 107 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 108 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 109 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 108 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 1 | 0 | 0 |  |  | kind=0 x=30 y=18 w=28 h=60; kind=0 x=23 y=35 w=37 h=24 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=39 y=19 w=21 h=61; kind=0 x=51 y=34 w=26 h=16 | kind=0 x=49 y=33 w=30 h=16 (bdefend=16, dvx=2, injury=20) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 1 | 0 | 0 |  |  | kind=0 x=27 y=17 w=31 h=63 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=28 y=17 w=29 h=63; kind=0 x=45 y=37 w=32 h=11 | kind=0 x=45 y=34 w=33 h=14 (bdefend=16, dvx=2, injury=20) |  |
| 70 | `super_punch` | 14 | 3 | 2 | 71 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=16 w=30 h=61 |  |  |
| 71 | `super_punch` | 15 | 3 | 2 | 72 | 8 | 0 | 0 |  |  | kind=0 x=20 y=14 w=34 h=66 |  |  |
| 72 | `super_punch` | 16 | 3 | 1 | 73 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=35 h=66 |  |  |
| 73 | `super_punch` | 17 | 3 | 2 | 74 | 0 | 0 | 0 |  |  | kind=0 x=23 y=10 w=29 h=69 | kind=0 x=34 y=19 w=46 h=30 (bdefend=60, dvx=13, dvy=-7, fall=70, injury=65, vrest=15) |  |
| 74 | `super_punch` | 18 | 3 | 2 | 75 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=41 h=65 | kind=0 x=37 y=38 w=41 h=15 (bdefend=60, dvx=13, dvy=-7, fall=70, injury=65, vrest=15) |  |
| 75 | `super_punch` | 19 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=10 w=34 h=69 |  |  |
| 80 | `jump_attack` | 132 | 3 | 4 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=18 y=10 w=35 h=57 |  |  |
| 81 | `jump_attack` | 133 | 3 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=9 w=35 h=56 | kind=0 x=32 y=40 w=36 h=16 (arest=15, bdefend=30, dvx=9, dvy=-5, fall=70, injury=50) |  |
| 85 | `run_attack` | 14 | 3 | 2 | 71 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=16 w=30 h=61 |  |  |
| 90 | `jump_attack` | 132 | 3 | 4 | 91 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=18 y=10 w=35 h=57 |  |  |
| 91 | `jump_attack` | 133 | 3 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=9 w=35 h=56 | kind=0 x=32 y=40 w=36 h=16 (arest=15, bdefend=30, dvx=14, dvy=-7, fall=70, injury=60) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=31 h=68 |  |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=31 h=68 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=293 y=198 w=1 h=1 (vrest=1) |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 4 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=8 y=15 w=30 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=27 y=35 w=28 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 37 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 38 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 | kind=14 x=10 y=9 w=55 h=68 (vrest=1) |  |
| 202 | `ice` | 37 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 203 | `fire` | 78 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 79 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 88 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 89 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=29 h=61 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=25 y=13 w=30 h=65 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `ball1` | 100 | 3 | 3 | 241 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 241 | `ball1` | 101 | 3 | 3 | 242 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 242 | `ball1` | 102 | 3 | 2 | 243 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 243 | `ball1` | 103 | 3 | 1 | 244 | 0 | 0 | 0 | 50 | `data\047.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 244 | `ball1` | 104 | 3 | 1 | 245 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  | id 216 (type 3): data\jack_ball.dat |
| 245 | `ball1` | 105 | 3 | 2 | 246 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 246 | `ball1` | 101 | 3 | 2 | 247 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 247 | `ball1` | 100 | 3 | 2 | 999 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 300 | `singlong` | 134 | 3 | 1 | 301 | 0 | 0 | 0 | 125 | `data\044.wav` | kind=0 x=16 y=6 w=18 h=73 |  |  |
| 301 | `singlong` | 134 | 3 | 1 | 302 | 1 | -5 | 0 | 200 |  | kind=0 x=15 y=5 w=19 h=74 |  |  |
| 302 | `singlong` | 135 | 3 | 1 | 303 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=22 w=13 h=48 | kind=0 x=26 y=4 w=59 h=99 (bdefend=60, dvx=7, dvy=-13, fall=70, injury=70, vrest=10) |  |
| 303 | `singlong` | 136 | 3 | 3 | 304 | 0 | -1 | 0 |  |  | kind=0 x=19 y=7 w=36 h=69 |  |  |
| 304 | `singlong` | 137 | 3 | 3 | 305 | 0 | 0 | 0 |  |  | kind=0 x=22 y=6 w=30 h=69 |  |  |
| 305 | `singlong` | 138 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=12 w=29 h=66 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 34: Sorcerer
<a id="personagem-34-sorcerer"></a>

- **Arquivo (.dat):** `sorcerer.dat`
- **Head (seleção):** `sprite\sys\sorcerer_f.bmp`
- **Small (HUD):** `sprite\sys\sorcerer_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\sorcerer_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\sorcerer_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\sorcerer_0b.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\sorcerer_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 270 |
| 0 | `standing` | `hit_Uj` | 260 |
| 0 | `standing` | `hit_Dj` | 250 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 270 |
| 1 | `standing` | `hit_Uj` | 260 |
| 1 | `standing` | `hit_Dj` | 250 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 270 |
| 2 | `standing` | `hit_Uj` | 260 |
| 2 | `standing` | `hit_Dj` | 250 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 270 |
| 3 | `standing` | `hit_Uj` | 260 |
| 3 | `standing` | `hit_Dj` | 250 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 270 |
| 5 | `walking` | `hit_Uj` | 260 |
| 5 | `walking` | `hit_Dj` | 250 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 270 |
| 6 | `walking` | `hit_Uj` | 260 |
| 6 | `walking` | `hit_Dj` | 250 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 270 |
| 7 | `walking` | `hit_Uj` | 260 |
| 7 | `walking` | `hit_Dj` | 250 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 270 |
| 8 | `walking` | `hit_Uj` | 260 |
| 8 | `walking` | `hit_Dj` | 250 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 270 |
| 110 | `defend` | `hit_Uj` | 260 |
| 110 | `defend` | `hit_Dj` | 250 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 6 | 1 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=35 h=63 |  |  |
| 1 | `standing` | 1 | 0 | 5 | 2 | 0 | 0 | 0 |  |  | kind=0 x=16 y=15 w=37 h=63 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=34 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=14 w=33 h=66 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=14 w=30 h=66 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=14 w=28 h=64 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=12 w=31 h=66 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=12 w=30 h=67 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=5 y=15 w=45 h=65 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=27 y=20 w=32 h=60 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=30 y=19 w=28 h=61 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=7 y=12 w=40 h=68 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=10 w=31 h=70; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=29 y=16 w=29 h=63 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=29 y=16 w=29 h=63 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=14 y=11 w=30 h=69 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=14 w=29 h=67 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=26 y=7 w=32 h=58 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=26 y=7 w=26 h=39; kind=0 x=13 y=40 w=24 h=30 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=21 y=15 w=31 h=65 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=15 w=34 h=65 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=17 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=10 w=35 h=52 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=8 w=34 h=33 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=34 h=65; kind=0 x=9 y=36 w=17 h=18 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=22 w=32 h=57 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=38 w=54 h=21; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=15 y=9 w=29 h=54 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=35 y=10 w=36 h=23; kind=0 x=15 y=18 w=30 h=43 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=3 y=37 w=34 h=26 |  |  |
| 55 | `weapon_drink` | 136 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 137 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 138 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 137 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=13 y=12 w=29 h=68; kind=0 x=2 y=38 w=60 h=18 | kind=0 x=23 y=43 w=50 h=23 (bdefend=16, dvx=2, injury=15) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=16 y=59 w=35 h=21 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=15 y=11 w=31 h=69; kind=0 x=5 y=36 w=48 h=22 | kind=0 x=26 y=37 w=45 h=29 (bdefend=16, dvx=2, injury=15) |  |
| 70 | `super_punch` | 14 | 3 | 1 | 71 | 0 | 0 | 0 |  |  | kind=0 x=14 y=14 w=34 h=66 |  |  |
| 71 | `super_punch` | 15 | 3 | 1 | 72 | 4 | 0 | 0 |  |  | kind=0 x=21 y=14 w=39 h=64 |  |  |
| 72 | `super_punch` | 16 | 3 | 1 | 73 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=19 y=17 w=40 h=62 |  |  |
| 73 | `super_punch` | 17 | 3 | 2 | 74 | 0 | 0 | 0 |  |  | kind=0 x=12 y=17 w=40 h=62 | kind=0 x=11 y=10 w=62 h=49 (arest=15, bdefend=60, dvx=9, dvy=-7, fall=70, injury=45) |  |
| 74 | `super_punch` | 37 | 3 | 1 | 75 | 0 | 0 | 0 |  |  | kind=0 x=15 y=14 w=38 h=65 | kind=0 x=43 y=17 w=30 h=64 (arest=15, bdefend=60, dvx=9, dvy=-7, fall=70, injury=45) |  |
| 75 | `super_punch` | 38 | 3 | 1 | 76 | 0 | 0 | 0 |  |  | kind=0 x=15 y=15 w=39 h=65 |  |  |
| 76 | `super_punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=12 w=38 h=68 |  |  |
| 80 | `jump_attack` | 29 | 3 | 3 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=13 w=25 h=58 |  |  |
| 81 | `jump_attack` | 39 | 3 | 3 | 82 | 0 | 0 | 0 |  |  | kind=0 x=24 y=10 w=31 h=57 | kind=0 x=37 y=30 w=36 h=20 (arest=15, bdefend=60, dvx=7, fall=70, injury=45) |  |
| 82 | `jump_attack` | 67 | 3 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=30 y=7 w=28 h=61 | kind=0 x=46 y=40 w=31 h=21 (arest=15, bdefend=60, dvx=7, fall=70, injury=45) |  |
| 85 | `run_attack` | 14 | 3 | 1 | 71 | 0 | 0 | 0 |  |  | kind=0 x=14 y=14 w=34 h=66 |  |  |
| 90 | `dash_attack` | 29 | 3 | 3 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=13 w=25 h=58 |  |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 8 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 9 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 8 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 18 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 19 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 68 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 69 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=3 w=23 h=65; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 235 | `DA_action` | 78 | 15 | 1 | 236 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 236 | `DA_action` | 79 | 15 | 1 | 237 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 237 | `DA_action` | 87 | 15 | 1 | 238 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 238 | `DA_action` | 88 | 15 | 1 | 239 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 239 | `DA_action` | 89 | 15 | 2 | 240 | 0 | 0 | 0 | 75 | `data\067.wav` | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 240 | `DA_action` | 119 | 15 | 2 | 241 | 0 | 0 | 0 | 75 |  | kind=0 x=36 y=47 w=39 h=17; kind=0 x=29 y=16 w=38 h=63 |  | id 210 (type 3): data\firen_ball.dat |
| 241 | `DA_action` | 129 | 15 | 2 | 242 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 242 | `DA_action` | 78 | 15 | 3 | 999 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 250 | `heal_self` | 100 | 15 | 2 | 251 | 0 | 0 | 0 | 350 | `data\050.wav` | kind=0 x=20 y=13 w=37 h=67 |  |  |
| 251 | `heal_self` | 101 | 15 | 2 | 252 | 0 | 0 | 0 |  | `data\052.wav` | kind=0 x=18 y=14 w=39 h=66 |  |  |
| 252 | `heal_self` | 102 | 15 | 2 | 253 | 0 | 0 | 0 |  |  | kind=0 x=18 y=14 w=38 h=66 |  |  |
| 253 | `heal_self` | 103 | 15 | 2 | 254 | 0 | 0 | 0 |  |  | kind=0 x=17 y=16 w=39 h=64 |  |  |
| 254 | `heal_self` | 104 | 15 | 2 | 255 | 0 | 0 | 0 |  |  | kind=0 x=15 y=15 w=42 h=65 |  |  |
| 255 | `heal_self` | 105 | 1700 | 2 | 256 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=37 h=68 |  | id 200 (type 3): data\john_ball.dat |
| 256 | `heal_self` | 106 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=36 h=69 |  |  |
| 260 | `heal_other` | 100 | 15 | 2 | 261 | 0 | 0 | 0 | 350 | `data\050.wav` | kind=0 x=20 y=13 w=37 h=67 |  |  |
| 261 | `heal_other` | 101 | 15 | 2 | 262 | 0 | 0 | 0 |  | `data\052.wav` | kind=0 x=18 y=14 w=39 h=66 |  |  |
| 262 | `heal_other` | 102 | 15 | 2 | 263 | 0 | 0 | 0 |  |  | kind=0 x=18 y=14 w=38 h=66 |  |  |
| 263 | `heal_other` | 107 | 15 | 2 | 264 | 0 | 0 | 0 |  |  | kind=0 x=17 y=16 w=39 h=64 |  |  |
| 264 | `heal_other` | 108 | 15 | 2 | 265 | 0 | 0 | 0 |  |  | kind=0 x=15 y=15 w=42 h=65 |  |  |
| 265 | `heal_other` | 109 | 15 | 2 | 266 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=37 h=68 |  | id 200 (type 3): data\john_ball.dat |
| 266 | `heal_other` | 115 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=36 h=69 |  |  |
| 270 | `DA_action` | 78 | 15 | 1 | 271 | 0 | 0 | 0 | 125 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 271 | `DA_action` | 79 | 15 | 1 | 272 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 272 | `DA_action` | 132 | 15 | 1 | 273 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 273 | `DA_action` | 133 | 15 | 1 | 274 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 274 | `DA_action` | 134 | 15 | 2 | 275 | 0 | 0 | 0 | 75 | `data\064.wav` | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 275 | `DA_action` | 119 | 15 | 2 | 276 | 0 | 0 | 0 | 75 |  | kind=0 x=36 y=47 w=39 h=17; kind=0 x=29 y=16 w=38 h=63 |  | id 209 (type 3): data\freeze_ball.dat |
| 276 | `DA_action` | 129 | 15 | 2 | 277 | 0 | 0 | 0 | 75 |  | kind=0 x=31 y=44 w=38 h=20; kind=0 x=14 y=16 w=39 h=61 |  |  |
| 277 | `DA_action` | 78 | 15 | 3 | 999 | 0 | 0 | 0 | 75 |  | kind=0 x=14 y=16 w=39 h=61 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=14 w=35 h=64 |  |  |

</details>

---

## Personagem 35: Monk
<a id="personagem-35-monk"></a>

- **Arquivo (.dat):** `monk.dat`
- **Head (seleção):** `sprite\sys\monk_f.bmp`
- **Small (HUD):** `sprite\sys\monk_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\monk_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\monk_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\monk_0b.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\monk_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 240 |
| 1 | `standing` | `hit_Fa` | 240 |
| 2 | `standing` | `hit_Fa` | 240 |
| 3 | `standing` | `hit_Fa` | 240 |
| 5 | `walking` | `hit_Fa` | 240 |
| 6 | `walking` | `hit_Fa` | 240 |
| 7 | `walking` | `hit_Fa` | 240 |
| 8 | `walking` | `hit_Fa` | 240 |
| 110 | `defend` | `hit_Fa` | 240 |
| 111 | `defend` | `hit_Fa` | 240 |
| 248 | `ball` | `hit_Fa` | 243 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 5 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 5 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 | 0 | 0 | 0 |  |  | kind=0 x=27 y=20 w=32 h=60 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=30 y=19 w=28 h=61 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=16 y=7 w=40 h=72 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=39 h=68 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 | 0 | 0 | 0 |  |  | kind=0 x=17 y=11 w=38 h=70 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=18 y=8 w=36 h=73 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=17 y=8 w=40 h=71 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=4 w=40 h=77 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=27 y=17 w=39 h=56 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=21 y=14 w=35 h=58 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=17 y=18 w=39 h=54 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=11 w=39 h=61 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=28 y=16 w=34 h=64 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=11 w=43 h=45; kind=0 x=9 y=54 w=45 h=26 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=16 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=15 w=40 h=53 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=8 w=41 h=63 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=15 y=11 w=37 h=62 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=34 h=60 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 | 0 | 0 | 0 |  |  | kind=0 x=20 y=4 w=39 h=78 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=19 y=7 w=37 h=73 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=10 w=43 h=47; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 3 | 51 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 6 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=26 y=10 w=42 h=48; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=22 y=10 w=40 h=52 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=28 y=6 w=34 h=42; kind=0 x=19 y=25 w=31 h=51 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=18 y=40 w=31 h=35 |  |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 1 | 0 | 0 |  |  | kind=0 x=30 y=18 w=28 h=60; kind=0 x=23 y=35 w=37 h=24 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=6 w=38 h=73; kind=0 x=51 y=34 w=26 h=16 | kind=0 x=27 y=25 w=50 h=23 (bdefend=16, dvx=2, injury=20) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 1 | 0 | 0 |  |  | kind=0 x=27 y=17 w=31 h=63 | kind=2 x=26 y=58 w=37 h=23 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=17 y=7 w=41 h=72; kind=0 x=45 y=37 w=32 h=11 | kind=0 x=29 y=21 w=48 h=27 (bdefend=16, dvx=2, injury=20) |  |
| 70 | `super_punch` | 135 | 7 | 1 | 71 | 4 | 0 | 0 |  |  | kind=0 x=22 y=5 w=43 h=77 |  |  |
| 71 | `super_punch` | 136 | 7 | 1 | 72 | 4 | 0 | 0 |  |  | kind=0 x=21 y=6 w=40 h=76 |  |  |
| 72 | `super_punch` | 137 | 3 | 2 | 73 | 4 | 0 | 0 |  | `data\007.wav` | kind=0 x=23 y=8 w=42 h=70 |  |  |
| 73 | `super_punch` | 138 | 3 | 2 | 74 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=34 h=74; kind=0 x=43 y=35 w=28 h=18 | kind=0 x=25 y=10 w=50 h=41 (bdefend=60, dvx=11, dvy=-6, fall=70, injury=70, vrest=10) |  |
| 74 | `super_punch` | 139 | 3 | 2 | 75 | 0 | 0 | 0 |  |  | kind=0 x=7 y=4 w=38 h=76; kind=0 x=43 y=35 w=28 h=18 | kind=0 x=25 y=29 w=49 h=26 (bdefend=60, dvx=11, dvy=-6, fall=70, injury=70, vrest=10) |  |
| 75 | `super_punch` | 129 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=4 w=33 h=74; kind=0 x=36 y=40 w=22 h=29 |  |  |
| 80 | `jump_attack` | 14 | 3 | 2 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=16 y=3 w=43 h=77 |  |  |
| 81 | `jump_attack` | 15 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=19 w=32 h=60; kind=0 x=45 y=20 w=28 h=27 | kind=0 x=26 y=40 w=53 h=25 (arest=15, bdefend=16, dvx=2, injury=35) |  |
| 85 | `run_attack` | 135 | 7 | 1 | 71 | 4 | 0 | 0 |  |  | kind=0 x=22 y=14 w=30 h=65 |  |  |
| 90 | `dash_attack` | 106 | 15 | 3 | 91 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=25 h=55; kind=0 x=13 y=36 w=52 h=18 |  |  |
| 91 | `dash_attack` | 107 | 15 | 20 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=7 y=19 w=37 h=45; kind=0 x=22 y=36 w=50 h=18 | kind=0 x=27 y=38 w=53 h=23 (arest=20, bdefend=60, dvx=12, fall=70, injury=70) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 | 7 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 47 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 46 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 5 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 5 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 5 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 105 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 115 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 105 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 100 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 101 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 110 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 111 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=33 y=33 w=20 h=48; kind=0 x=25 y=50 w=15 h=29 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=15 y=41 w=48 h=20; kind=0 x=25 y=55 w=30 h=27 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=2 w=28 h=76 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=25 y=7 w=43 h=42; kind=0 x=17 y=45 w=33 h=30 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=7 w=38 h=64 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=40 w=29 h=38 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=7 w=31 h=35; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=15 y=10 w=42 h=67 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=11 w=39 h=66 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 7 | 233 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=22 y=11 w=47 h=46; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 10 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=10 w=46 h=46; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `ball` | 8 | 7 | 2 | 241 | 0 | 0 | 0 | 100 |  | kind=0 x=12 y=6 w=39 h=72 |  |  |
| 241 | `ball` | 9 | 7 | 3 | 242 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=6 w=39 h=72 |  |  |
| 242 | `ball` | 16 | 7 | 1 | 243 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=39 h=74 |  |  |
| 243 | `ball` | 17 | 7 | 3 | 244 | 0 | 0 | 0 | 75 | `data\007.wav` | kind=0 x=20 y=6 w=40 h=73 |  |  |
| 244 | `ball` | 18 | 7 | 1 | 245 | 3 | 0 | 0 |  | `data\098.wav` | kind=0 x=20 y=6 w=43 h=74 |  |  |
| 245 | `ball` | 19 | 3 | 1 | 246 | 0 | 0 | 0 |  | `data\020.wav` | kind=0 x=12 y=8 w=38 h=72 |  | id 204 (type 3): data\henry_wind.dat |
| 246 | `ball` | 29 | 3 | 5 | 247 | 0 | 0 | 0 |  |  | kind=0 x=12 y=6 w=39 h=72 | kind=0 x=25 y=23 w=55 h=24 (bdefend=60, dvx=11, dvy=-6, fall=70, injury=35, vrest=10) |  |
| 247 | `ball` | 39 | 3 | 1 | 248 | 0 | 0 | 0 |  |  | kind=0 x=12 y=6 w=39 h=72 |  |  |
| 248 | `ball` | 39 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=6 w=39 h=72 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 36: Jan
<a id="personagem-36-jan"></a>

- **Arquivo (.dat):** `jan.dat`
- **Head (seleção):** `sprite\sys\jan_f.bmp`
- **Small (HUD):** `sprite\sys\jan_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\jan_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\jan_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\jan_2.bmp` | 79×79 | 10×7 |
| 210-279 | `sprite\sys\jan_3.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Ua` | 260 |
| 0 | `standing` | `hit_Uj` | 240 |
| 1 | `standing` | `hit_Ua` | 260 |
| 1 | `standing` | `hit_Uj` | 240 |
| 2 | `standing` | `hit_Ua` | 260 |
| 2 | `standing` | `hit_Uj` | 240 |
| 3 | `standing` | `hit_Ua` | 260 |
| 3 | `standing` | `hit_Uj` | 240 |
| 5 | `walking` | `hit_Ua` | 260 |
| 5 | `walking` | `hit_Uj` | 240 |
| 6 | `walking` | `hit_Ua` | 260 |
| 6 | `walking` | `hit_Uj` | 240 |
| 7 | `walking` | `hit_Ua` | 260 |
| 7 | `walking` | `hit_Uj` | 240 |
| 8 | `walking` | `hit_Ua` | 260 |
| 8 | `walking` | `hit_Uj` | 240 |
| 110 | `defend` | `hit_Ua` | 260 |
| 110 | `defend` | `hit_Uj` | 240 |
| 111 | `defend` | `hit_Ua` | 260 |
| 111 | `defend` | `hit_Uj` | 240 |
| 252 | `standing` | `hit_Ua` | 260 |
| 252 | `standing` | `hit_Uj` | 240 |
| 272 | `standing` | `hit_Ua` | 260 |
| 272 | `standing` | `hit_Uj` | 240 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 9 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 5 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 3 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 9 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=23 y=9 w=37 h=70 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 2 | 21 | 0 | 0 | 0 |  |  | kind=0 x=27 y=20 w=32 h=60 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=30 y=19 w=28 h=61 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  |  | kind=0 x=23 y=12 w=33 h=68 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=34 h=65; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 2 | 26 | 0 | 0 | 0 |  |  | kind=0 x=17 y=8 w=36 h=71 |  |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=29 y=16 w=29 h=63 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  |  | kind=0 x=24 y=8 w=29 h=71 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=14 w=29 h=67 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=26 y=7 w=32 h=58 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=26 y=7 w=26 h=39; kind=0 x=13 y=40 w=24 h=30 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=21 y=15 w=31 h=65 |  |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=15 w=34 h=65 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=17 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=10 w=35 h=52 |  |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=8 w=34 h=33 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=34 h=65 |  |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=22 w=32 h=57 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=15 y=13 w=47 h=45; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 6 | 51 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=18 y=11 w=42 h=48; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=22 y=8 w=35 h=68 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=35 y=10 w=36 h=23; kind=0 x=15 y=18 w=30 h=43 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 11 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=19 y=38 w=32 h=38 |  |  |
| 55 | `weapon_drink` | 136 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 137 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 138 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 137 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 2 | 62 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=20 y=9 w=33 h=69; kind=0 x=21 y=34 w=47 h=23 | kind=0 x=25 y=36 w=47 h=15 (bdefend=16, dvx=2, injury=20) |  |
| 62 | `punch` | 12 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=7 w=37 h=71; kind=0 x=19 y=36 w=42 h=22 |  |  |
| 65 | `punch` | 16 | 3 | 2 | 66 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=1) |  |
| 66 | `punch` | 17 | 3 | 2 | 67 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=18 y=7 w=36 h=73; kind=0 x=28 y=33 w=39 h=19 | kind=0 x=35 y=30 w=39 h=19 (bdefend=16, dvx=2, injury=20) |  |
| 67 | `punch` | 29 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=11 w=33 h=66; kind=0 x=24 y=37 w=31 h=22 |  |  |
| 70 | `super_punch` | 37 | 3 | 2 | 86 | 6 | 0 | 0 |  |  | kind=0 x=19 y=24 w=35 h=54; kind=0 x=10 y=36 w=27 h=15 |  |  |
| 80 | `jump_attack` | 13 | 3 | 3 | 81 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=1) |  |
| 81 | `jump_attack` | 14 | 3 | 4 | 82 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=22 y=8 w=27 h=71; kind=0 x=25 y=44 w=44 h=17 | kind=0 x=46 y=40 w=31 h=21 (arest=15, bdefend=60, dvx=7, fall=70, injury=45) |  |
| 82 | `jump_attack` | 15 | 3 | 1 | 83 | 0 | 0 | 0 |  |  | kind=0 x=24 y=10 w=27 h=70; kind=0 x=20 y=44 w=49 h=17 | kind=0 x=46 y=40 w=31 h=21 (arest=15, bdefend=60, dvx=7, fall=70, injury=45) |  |
| 83 | `jump_attack` | 13 | 3 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=26 y=10 w=26 h=68 |  |  |
| 85 | `run_attack` | 37 | 3 | 2 | 86 | 6 | 0 | 0 |  |  | kind=0 x=19 y=24 w=35 h=54; kind=0 x=10 y=36 w=27 h=15 |  |  |
| 86 | `run_attack` | 38 | 3 | 2 | 87 | 4 | 0 | 0 |  | `data\007.wav` | kind=0 x=17 y=39 w=37 h=19; kind=0 x=8 y=51 w=38 h=27; kind=0 x=23 y=18 w=28 h=23 |  |  |
| 87 | `run_attack` | 39 | 3 | 2 | 88 | 0 | 0 | 0 |  |  | kind=0 x=21 y=38 w=44 h=19; kind=0 x=11 y=52 w=25 h=27; kind=0 x=15 y=11 w=24 h=40 | kind=0 x=19 y=37 w=61 h=17 (arest=15, bdefend=16, dvx=10, fall=70, injury=50) |  |
| 88 | `run_attack` | 67 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=36 w=39 h=24; kind=0 x=28 y=48 w=23 h=32; kind=0 x=29 y=10 w=21 h=31 |  |  |
| 90 | `dash_attack` | 104 | 15 | 2 | 91 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=25 h=55; kind=0 x=13 y=36 w=52 h=18; kind=0 x=31 y=9 w=25 h=29 |  |  |
| 91 | `dash_attack` | 105 | 15 | 9 | 216 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=23 y=32 w=21 h=47; kind=0 x=22 y=36 w=50 h=18; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=27 y=38 w=53 h=23 (arest=20, bdefend=60, dvx=12, fall=70, injury=70) |  |
| 92 | `dash_attack` | 108 | 15 | 5 | 93 | 0 | 0 | 0 |  |  | kind=0 x=7 y=19 w=37 h=45; kind=0 x=22 y=36 w=50 h=18; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=27 y=38 w=53 h=23 (arest=20, bdefend=60, dvx=12, fall=70, injury=70) |  |
| 93 | `dash_attack` | 109 | 15 | 4 | 216 | 0 | 0 | 0 |  |  | kind=0 x=27 y=32 w=24 h=44; kind=0 x=29 y=37 w=32 h=19; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=27 y=38 w=53 h=23 (arest=20, bdefend=60, dvx=12, fall=70, injury=70) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 49 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 59 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 49 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 107 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 | kind=0 x=44 y=33 w=17 h=19 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 | kind=0 x=43 y=32 w=18 h=21 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 5 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 8 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 9 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 8 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 18 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 19 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 68 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 69 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=3 w=23 h=65; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 6 | 216 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 6 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 3 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `standing` | 100 | 3 | 2 | 241 | 0 | 0 | 0 | 200 | `data\018.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 241 | `standing` | 101 | 3 | 1 | 242 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 242 | `standing` | 102 | 3 | 1 | 243 | 0 | 0 | 0 |  | `data\055.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 243 | `standing` | 132 | 3 | 2 | 244 | 0 | 0 | 0 |  | `data\052.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 244 | `standing` | 133 | 3 | 2 | 245 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 245 | `standing` | 132 | 3 | 2 | 246 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 219 (type 3): data\jan_chaseh.dat |
| 246 | `standing` | 133 | 3 | 2 | 247 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 247 | `standing` | 132 | 3 | 2 | 248 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 248 | `standing` | 133 | 3 | 2 | 249 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 249 | `standing` | 132 | 3 | 2 | 250 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 250 | `standing` | 102 | 3 | 2 | 251 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 251 | `standing` | 101 | 3 | 1 | 252 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 252 | `standing` | 100 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 260 | `standing` | 100 | 3 | 2 | 261 | 0 | 0 | 0 | 150 | `data\018.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 261 | `standing` | 101 | 3 | 1 | 262 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 262 | `standing` | 102 | 3 | 1 | 263 | 0 | 0 | 0 |  | `data\055.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 263 | `standing` | 134 | 3 | 2 | 264 | 0 | 0 | 0 |  | `data\052.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 264 | `standing` | 135 | 3 | 2 | 265 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 265 | `standing` | 134 | 3 | 2 | 266 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 220 (type 3): data\jan_chase.dat |
| 266 | `standing` | 135 | 3 | 2 | 267 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 267 | `standing` | 134 | 3 | 2 | 268 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 268 | `standing` | 135 | 3 | 2 | 269 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 269 | `standing` | 134 | 3 | 2 | 270 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 270 | `standing` | 102 | 3 | 2 | 271 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 271 | `standing` | 101 | 3 | 1 | 272 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 272 | `standing` | 100 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 37: Knight
<a id="personagem-37-knight"></a>

- **Arquivo (.dat):** `knight.dat`
- **Head (seleção):** `sprite\sys\knight_f.bmp`
- **Small (HUD):** `sprite\sys\knight_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-49 | `sprite\sys\knight_0.bmp` | 79×99 | 10×5 |
| 50-63 | `sprite\sys\knight_1.bmp` | 109×99 | 7×2 |
| 64-113 | `sprite\sys\knight_2.bmp` | 79×99 | 10×5 |
| 114-163 | `sprite\sys\knight_0b.bmp` | 79×99 | 10×5 |
| 164-177 | `sprite\sys\knight_1b.bmp` | 109×99 | 7×2 |
| 178-227 | `sprite\sys\knight_2b.bmp` | 79×99 | 10×5 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 240 |
| 1 | `standing` | `hit_Fa` | 240 |
| 2 | `standing` | `hit_Fa` | 240 |
| 3 | `standing` | `hit_Fa` | 240 |
| 4 | `standing` | `hit_Fa` | 240 |
| 5 | `walking` | `hit_Fa` | 240 |
| 6 | `walking` | `hit_Fa` | 240 |
| 7 | `walking` | `hit_Fa` | 240 |
| 8 | `walking` | `hit_Fa` | 240 |
| 9 | `running` | `hit_Fa` | 240 |
| 10 | `running` | `hit_Fa` | 240 |
| 11 | `running` | `hit_Fa` | 240 |
| 102 | `rowing` | `hit_Fa` | 240 |
| 110 | `defend` | `hit_Fa` | 240 |
| 111 | `defend` | `hit_Fa` | 240 |
| 259 | `sword` | `hit_a` | 246 |
| 260 | `sword` | `hit_a` | 251 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 10 | 1 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 1 | `standing` | 1 | 0 | 4 | 2 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 2 | `standing` | 2 | 0 | 3 | 3 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 3 | `standing` | 3 | 0 | 8 | 4 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 4 | `standing` | 1 | 0 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=31 y=10 w=57 h=89 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=31 y=10 w=57 h=89 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=31 y=10 w=57 h=89 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=31 y=10 w=57 h=89 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 26 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 10 | `running` | 27 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 11 | `running` | 28 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 12 | `heavy_obj_walk` | 30 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=10 y=13 w=63 h=86 |  |  |
| 13 | `heavy_obj_walk` | 31 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=14 w=60 h=85 |  |  |
| 14 | `heavy_obj_walk` | 32 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=13 y=10 w=60 h=91 |  |  |
| 15 | `heavy_obj_walk` | 33 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=12 y=7 w=62 h=93 |  |  |
| 16 | `heavy_obj_run` | 34 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=8 y=7 w=63 h=90 |  |  |
| 17 | `heavy_obj_run` | 35 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=11 y=11 w=64 h=88 |  |  |
| 18 | `heavy_obj_run` | 36 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=9 y=7 w=59 h=93 |  |  |
| 19 | `heavy_stop_run` | 41 | 15 | 9 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=13 y=9 w=55 h=90 |  |  |
| 20 | `normal_weapon_atck` | 37 | 15 | 3 | 21 | 3 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 21 | `normal_weapon_atck` | 38 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=4 y=15 w=56 h=86 |  |  |
| 25 | `normal_weapon_atck` | 37 | 15 | 3 | 21 | 3 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 30 | `jump_weapon_atck` | 37 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=20 y=13 w=47 h=85 |  |  |
| 31 | `jump_weapon_atck` | 38 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 32 | `jump_weapon_atck` | 38 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 35 | `run_weapon_atck` | 37 | 15 | 3 | 36 | 3 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 36 | `run_weapon_atck` | 38 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=4 y=15 w=56 h=86 |  |  |
| 40 | `dash_weapon_atck` | 37 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=20 y=13 w=47 h=85 |  |  |
| 41 | `dash_weapon_atck` | 38 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 42 | `dash_weapon_atck` | 38 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 45 | `light_weapon_thw` | 37 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 46 | `light_weapon_thw` | 38 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=4 y=15 w=56 h=86 |  |  |
| 50 | `heavy_weapon_thw` | 37 | 15 | 3 | 51 | 0 | 0 | 0 |  |  | kind=0 x=11 y=4 w=59 h=97 |  |  |
| 51 | `heavy_weapon_thw` | 38 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=16 w=50 h=84 |  |  |
| 52 | `sky_lgt_wp_thw` | 37 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=20 y=8 w=44 h=88 |  |  |
| 53 | `sky_lgt_wp_thw` | 38 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=11 y=14 w=46 h=80 |  |  |
| 54 | `sky_lgt_wp_thw` | 38 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=17 w=48 h=85 |  |  |
| 55 | `weapon_drink` | 46 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=15 y=12 w=37 h=67 |  |  |
| 56 | `weapon_drink` | 47 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=16 y=20 w=36 h=59 |  |  |
| 57 | `weapon_drink` | 46 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=17 y=17 w=32 h=63 |  |  |
| 58 | `weapon_drink` | 47 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=36 h=65 |  |  |
| 60 | `punch` | 10 | 3 | 1 | 241 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=2 x=12 y=80 w=55 h=25 (vrest=1) |  |
| 65 | `punch` | 10 | 3 | 1 | 241 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=2 x=13 y=81 w=54 h=23 (vrest=1) |  |
| 70 | `super` | 20 | 3 | 2 | 252 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 80 | `jump_attack` | 29 | 3 | 2 | 81 | 0 | 0 | 0 |  |  | kind=0 x=21 y=7 w=53 h=96 |  |  |
| 81 | `jump_attack` | 39 | 3 | 1 | 82 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=8 y=8 w=45 h=91 |  |  |
| 82 | `jump_attack` | 53 | 3 | 1 | 83 | 0 | 0 | 0 |  |  | kind=0 x=12 y=9 w=43 h=92 | kind=0 x=34 y=8 w=63 h=72 (bdefend=60, dvx=12, dvy=12, effect=1, fall=70, injury=60, vrest=20) |  |
| 83 | `jump_attack` | 49 | 3 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=8 w=50 h=90 |  |  |
| 85 | `run_attack` | 20 | 3 | 2 | 252 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 90 | `dash_attack` | 14 | 15 | 3 | 91 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=25 h=55; kind=0 x=13 y=36 w=52 h=18; kind=0 x=31 y=9 w=25 h=29 |  |  |
| 91 | `dash_attack` | 15 | 15 | 3 | 92 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=22 y=40 w=33 h=26; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=9 y=32 w=69 h=35 (bdefend=60, dvx=17, fall=70, injury=60, vrest=12) |  |
| 92 | `dash_attack` | 16 | 15 | 9 | 216 | 0 | 0 | 0 |  |  | kind=0 x=23 y=38 w=28 h=25; kind=0 x=22 y=36 w=50 h=18; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=10 y=29 w=68 h=40 (arest=20, bdefend=60, dvx=17, fall=70, injury=60) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 55 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 38 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 42 | 7 | 12 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=8 y=9 w=52 h=94 | kind=0 x=53 y=24 w=24 h=74 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 108 | `rowing` | 54 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 67 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 42 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=9 w=52 h=94 | kind=0 x=53 y=24 w=24 h=74 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 111 | `defend` | 42 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=9 w=52 h=94 | kind=0 x=53 y=24 w=24 h=74 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 112 | `broken_defend` | 66 | 8 | 1 | 113 | 0 | 0 | 0 |  |  | kind=0 x=19 y=6 w=53 h=94 |  |  |
| 113 | `broken_defend` | 67 | 8 | 2 | 114 | 0 | 0 | 0 |  |  | kind=0 x=15 y=6 w=57 h=92 |  |  |
| 114 | `broken_defend` | 69 | 8 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=15 y=10 w=54 h=90 |  |  |
| 115 | `picking_light` | 14 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=19 y=22 w=52 h=75 |  |  |
| 116 | `picking_heavy` | 14 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=19 y=23 w=49 h=75 |  |  |
| 117 | `picking_heavy` | 14 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=22 w=49 h=78 |  |  |
| 120 | `catching` | 44 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=17 y=13 w=49 h=89 |  |  |
| 121 | `catching` | 43 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=11 y=11 w=54 h=84 |  |  |
| 122 | `catching` | 44 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=18 y=10 w=52 h=86 |  |  |
| 123 | `catching` | 45 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=22 y=4 w=52 h=92 |  |  |
| 130 | `picked_caught` | 68 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 69 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 69 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 64 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 65 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 56 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 74 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 75 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 57 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 58 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 59 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 58 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 64 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=23 y=45 w=22 h=21 | kind=4 x=17 y=39 w=35 h=31 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 65 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=41 w=26 h=22 | kind=4 x=12 y=33 w=29 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=20 y=45 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 54 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=37 y=38 w=27 h=24 | kind=4 x=33 y=37 w=39 h=30 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 55 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=38 y=40 w=26 h=22 | kind=4 x=55 y=32 w=27 h=19 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=30 y=37 w=36 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 56 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 54 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 74 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=47 w=25 h=24 | kind=4 x=19 y=37 w=42 h=36 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 75 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=49 w=25 h=22 | kind=4 x=22 y=26 w=41 h=57 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 57 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=42 y=38 w=26 h=23 | kind=4 x=34 y=29 w=35 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 58 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 59 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 57 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 8 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=12 y=15 w=61 h=84 |  |  |
| 201 | `ice` | 9 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=12 w=66 h=86 | kind=14 x=8 y=12 w=66 h=86 (vrest=1) |  |
| 202 | `ice` | 8 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 61 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 60 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 61 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 60 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 14 | 4 | 2 | 211 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=53 h=83 |  |  |
| 211 | `jump` | 14 | 4 | 2 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=13 y=20 w=52 h=80 |  |  |
| 212 | `jump` | 48 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=5 w=37 h=84 |  |  |
| 213 | `jump` | 48 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=1 w=37 h=92 |  |  |
| 214 | `jump` | 48 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=5 w=38 h=88 |  |  |
| 215 | `crouch` | 14 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=15 y=22 w=50 h=76 |  |  |
| 218 | `stop_running` | 40 | 15 | 8 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=16 y=8 w=47 h=92; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 14 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=14 y=21 w=53 h=77 |  |  |
| 220 | `injured` | 66 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=27 y=16 w=38 h=85 |  |  |
| 221 | `injured` | 67 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=16 w=47 h=85; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 69 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=13 y=14 w=45 h=83 |  |  |
| 223 | `injured` | 68 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=14 y=15 w=50 h=78 |  |  |
| 224 | `injured` | 69 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=13 y=14 w=45 h=83 |  |  |
| 225 | `injured` | 68 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=14 y=15 w=50 h=78 |  |  |
| 226 | `injured` | 69 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=26 y=24 w=41 h=74 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 67 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=23 y=22 w=46 h=77 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 66 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=43 h=86 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 67 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=12 w=49 h=87 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 56 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 59 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 37 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=29 y=25 w=41 h=74 |  |  |
| 233 | `throw_lying_man` | 38 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=7 y=15 w=53 h=84; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 38 | 9 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=15 w=46 h=83 |  |  |
| 240 | `sword` | 10 | 3 | 0 | 241 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 241 | `sword` | 11 | 3 | 1 | 242 | 0 | 0 | 0 |  |  | kind=0 x=13 y=9 w=53 h=91 |  |  |
| 242 | `sword` | 12 | 3 | 1 | 243 | 2 | 0 | 0 |  |  | kind=0 x=12 y=8 w=50 h=90 |  |  |
| 243 | `sword` | 13 | 3 | 2 | 244 | 1 | 0 | 0 |  | `data\054.wav` | kind=0 x=27 y=8 w=47 h=93 |  |  |
| 244 | `sword` | 50 | 3 | 1 | 245 | 0 | 0 | 0 |  |  | kind=0 x=8 y=7 w=49 h=91 | kind=0 x=36 y=15 w=65 h=65 (bdefend=30, dvx=7, effect=1, fall=25, injury=45, vrest=7) |  |
| 245 | `sword` | 15 | 3 | 2 | 259 | 3 | 0 | 0 |  |  | kind=0 x=6 y=8 w=47 h=94 |  |  |
| 246 | `sword` | 16 | 3 | 2 | 247 | 3 | 0 | 0 |  |  | kind=0 x=21 y=9 w=53 h=98 |  |  |
| 247 | `sword` | 17 | 3 | 1 | 248 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=20 y=13 w=54 h=90 |  |  |
| 248 | `sword` | 51 | 3 | 1 | 249 | 7 | 0 | 0 |  |  | kind=0 x=5 y=10 w=54 h=90 | kind=0 x=19 y=24 w=81 h=44 (bdefend=30, dvx=10, effect=1, fall=25, injury=45, vrest=7) |  |
| 249 | `sword` | 18 | 3 | 1 | 250 | 0 | 0 | 0 |  |  | kind=0 x=0 y=9 w=55 h=93 |  |  |
| 250 | `sword` | 19 | 3 | 2 | 260 | 0 | 0 | 0 |  |  | kind=0 x=3 y=10 w=49 h=91 |  |  |
| 251 | `sword` | 20 | 3 | 1 | 252 | 0 | 0 | 0 |  |  | kind=0 x=30 y=11 w=48 h=89 |  |  |
| 252 | `sword` | 21 | 3 | 0 | 253 | 3 | 0 | 0 |  |  | kind=0 x=31 y=12 w=44 h=87 |  |  |
| 253 | `sword` | 22 | 3 | 0 | 254 | 0 | 0 | 0 |  |  | kind=0 x=30 y=9 w=46 h=91 |  |  |
| 254 | `sword` | 23 | 3 | 1 | 255 | 4 | 0 | 0 |  |  | kind=0 x=14 y=9 w=50 h=92 |  |  |
| 255 | `sword` | 24 | 3 | 2 | 256 | 4 | 0 | 0 |  | `data\054.wav` | kind=0 x=7 y=8 w=49 h=93 |  |  |
| 256 | `sword` | 52 | 3 | 1 | 257 | 4 | 0 | 0 |  |  | kind=0 x=4 y=9 w=53 h=90 | kind=0 x=42 y=18 w=52 h=65 (bdefend=60, dvx=12, dvy=12, effect=1, fall=70, injury=60, vrest=20) |  |
| 257 | `sword` | 25 | 3 | 2 | 258 | 4 | 0 | 0 |  |  | kind=0 x=7 y=8 w=50 h=91 |  |  |
| 258 | `sword` | 15 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=5 y=9 w=50 h=94 |  |  |
| 259 | `sword` | 15 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=6 y=11 w=51 h=93 |  |  |
| 260 | `sword` | 19 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=4 y=12 w=50 h=90 |  |  |

</details>

---

## Personagem 38: Bat
<a id="personagem-38-bat"></a>

- **Arquivo (.dat):** `bat.dat`
- **Head (seleção):** `sprite\sys\bat_f.bmp`
- **Small (HUD):** `sprite\sys\bat_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\bat_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\bat_1.bmp` | 79×79 | 10×7 |
| 140-141 | `sprite\sys\bat_2.bmp` | 159×79 | 2×1 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 240 |
| 0 | `standing` | `hit_Fj` | 270 |
| 0 | `standing` | `hit_Uj` | 260 |
| 1 | `standing` | `hit_Fa` | 240 |
| 1 | `standing` | `hit_Fj` | 270 |
| 1 | `standing` | `hit_Uj` | 260 |
| 2 | `standing` | `hit_Fa` | 240 |
| 2 | `standing` | `hit_Fj` | 270 |
| 2 | `standing` | `hit_Uj` | 260 |
| 3 | `standing` | `hit_Fa` | 240 |
| 3 | `standing` | `hit_Fj` | 270 |
| 3 | `standing` | `hit_Uj` | 260 |
| 4 | `standing` | `hit_Fa` | 240 |
| 4 | `standing` | `hit_Fj` | 270 |
| 4 | `standing` | `hit_Uj` | 260 |
| 5 | `walking` | `hit_Fa` | 240 |
| 5 | `walking` | `hit_Fj` | 270 |
| 5 | `walking` | `hit_Uj` | 260 |
| 6 | `walking` | `hit_Fa` | 240 |
| 6 | `walking` | `hit_Fj` | 270 |
| 6 | `walking` | `hit_Uj` | 260 |
| 7 | `walking` | `hit_Fa` | 240 |
| 7 | `walking` | `hit_Fj` | 270 |
| 7 | `walking` | `hit_Uj` | 260 |
| 8 | `walking` | `hit_Fa` | 240 |
| 8 | `walking` | `hit_Fj` | 270 |
| 8 | `walking` | `hit_Uj` | 260 |
| 9 | `running` | `hit_Fa` | 240 |
| 9 | `running` | `hit_Fj` | 270 |
| 9 | `running` | `hit_Uj` | 260 |
| 10 | `running` | `hit_Fa` | 240 |
| 10 | `running` | `hit_Fj` | 270 |
| 10 | `running` | `hit_Uj` | 260 |
| 11 | `running` | `hit_Fa` | 240 |
| 11 | `running` | `hit_Fj` | 270 |
| 11 | `running` | `hit_Uj` | 260 |
| 102 | `rowing` | `hit_Fj` | 270 |
| 103 | `rowing` | `hit_Fj` | 270 |
| 104 | `rowing` | `hit_Fj` | 270 |
| 105 | `rowing` | `hit_Fj` | 270 |
| 106 | `rowing` | `hit_Fj` | 270 |
| 107 | `rowing` | `hit_Fj` | 270 |
| 110 | `defend` | `hit_Fa` | 240 |
| 110 | `defend` | `hit_Fj` | 270 |
| 110 | `defend` | `hit_Uj` | 260 |
| 121 | `catching` | `hit_Ua` | 300 |
| 121 | `catching` | `hit_Da` | 274 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 5 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 3 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 9 | 4 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 4 | `standing` | 2 | 0 | 0 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 74 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 75 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 76 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 27 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=29 y=7 w=22 h=73 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 2 | 21 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 25 | `normal_weapon_atck` | 70 | 3 | 2 | 21 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 30 | `jump_weapon_atck` | 72 | 3 | 2 | 31 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 31 | `jump_weapon_atck` | 73 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 35 | `run_weapon_atck` | 70 | 3 | 2 | 36 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 36 | `run_weapon_atck` | 71 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 40 | `dash_weapon_atck` | 72 | 3 | 2 | 41 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 41 | `dash_weapon_atck` | 73 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 45 | `light_weapon_thw` | 70 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=29 y=11 w=32 h=68; kind=0 x=17 y=40 w=24 h=10 |  |  |
| 46 | `light_weapon_thw` | 71 | 15 | 9 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=18 w=48 h=33; kind=0 x=27 y=49 w=23 h=29 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 | 0 | 0 | 0 |  |  | kind=0 x=29 y=7 w=22 h=73 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 72 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=28 y=4 w=31 h=63 |  |  |
| 53 | `sky_lgt_wp_thw` | 73 | 15 | 6 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=22 y=15 w=38 h=38; kind=0 x=19 y=37 w=26 h=29 |  |  |
| 55 | `weapon_drink` | 92 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 93 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 94 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 93 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 2 | 0 | 0 |  |  | kind=0 x=28 y=12 w=33 h=70 | kind=2 x=27 y=57 w=36 h=25 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=10 w=39 h=70 | kind=0 x=31 y=35 w=40 h=14 (bdefend=16, dvx=2, injury=20) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 2 | 0 | 0 |  |  | kind=0 x=13 y=10 w=33 h=71 | kind=2 x=15 y=59 w=29 h=23 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=17 y=10 w=38 h=71 | kind=0 x=29 y=36 w=45 h=15 (bdefend=16, dvx=2, injury=20) |  |
| 70 | `super_punch` | 14 | 3 | 3 | 71 | 8 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=16 w=30 h=61 |  |  |
| 71 | `super_punch` | 15 | 3 | 1 | 72 | 0 | 0 | 0 |  |  | kind=0 x=20 y=14 w=34 h=66 | kind=0 x=15 y=16 w=54 h=49 (arest=15, bdefend=60, dvx=14, dvy=12, fall=70, injury=70) |  |
| 72 | `super_punch` | 16 | 3 | 3 | 73 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=35 h=66 |  |  |
| 73 | `super_punch` | 17 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=23 y=10 w=29 h=69 |  |  |
| 80 | `jump_attack` | 95 | 3 | 2 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=26 y=13 w=35 h=55 |  |  |
| 81 | `jump_attack` | 96 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=14 w=41 h=56; kind=0 x=35 y=51 w=30 h=13 | kind=0 x=28 y=38 w=45 h=31 (arest=15, bdefend=60, dvx=8, dvy=-5, fall=70, injury=50) |  |
| 85 | `run_attack` | 14 | 3 | 3 | 71 | 8 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=16 w=30 h=61 |  |  |
| 90 | `dash_attack` | 95 | 3 | 2 | 91 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=9 w=44 h=64 |  |  |
| 91 | `dash_attack` | 96 | 3 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=14 w=37 h=50; kind=0 x=39 y=45 w=29 h=16 | kind=0 x=19 y=37 w=51 h=32 (arest=15, bdefend=60, dvx=14, dvy=-5, fall=70, injury=70) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 96 | `dash_attack` | 137 | 3 | 3 | 96 | 0 | 0 | 0 |  |  | kind=0 x=21 y=6 w=29 h=58 |  |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=31 h=68 |  |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=31 h=68 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=293 y=198 w=1 h=1 (vrest=1) |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 80 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 81 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 4 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=8 y=15 w=30 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=27 y=35 w=28 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 37 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 38 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 | kind=14 x=10 y=9 w=55 h=68 (vrest=1) |  |
| 202 | `ice` | 37 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 203 | `fire` | 78 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 79 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 88 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 89 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=29 h=61 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 0 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 218 | `stop_running` | 12 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=10 w=28 h=69 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 82 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 83 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 85 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 86 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 90 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=25 y=13 w=30 h=65 |  |  |
| 225 | `injured` | 91 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 82 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 84 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 85 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 84 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `ball1` | 100 | 3 | 4 | 241 | 0 | 0 | 0 | 125 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 241 | `ball1` | 101 | 3 | 0 | 242 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 242 | `ball1` | 102 | 3 | 1 | 243 | 0 | 0 | 0 |  | `data\086.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 243 | `ball1` | 140 | 3 | 1 | 244 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 224 (type 3): data\bat_ball.dat |
| 244 | `ball1` | 141 | 3 | 0 | 245 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 245 | `ball1` | 140 | 3 | 0 | 246 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 224 (type 3): data\bat_ball.dat |
| 246 | `ball1` | 141 | 3 | 1 | 247 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 247 | `ball1` | 140 | 3 | 1 | 248 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 224 (type 3): data\bat_ball.dat |
| 248 | `ball1` | 141 | 3 | 0 | 249 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 249 | `ball1` | 140 | 3 | 0 | 250 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 224 (type 3): data\bat_ball.dat |
| 250 | `ball1` | 141 | 3 | 1 | 251 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 251 | `ball1` | 140 | 3 | 1 | 252 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 224 (type 3): data\bat_ball.dat |
| 252 | `ball1` | 141 | 3 | 0 | 253 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 253 | `ball1` | 102 | 3 | 2 | 254 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 254 | `ball1` | 101 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 260 | `bat` | 103 | 3 | 4 | 261 | 0 | 0 | 0 | 200 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 261 | `bat` | 100 | 3 | 2 | 262 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 262 | `bat` | 104 | 3 | 2 | 263 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 225 (type 3): data\bat_chase.dat |
| 263 | `bat` | 105 | 3 | 6 | 264 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 264 | `bat` | 106 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 270 | `punch` | 77 | 7 | 2 | 271 | 0 | 0 | 0 | 50 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 271 | `punch` | 87 | 7 | 4 | 272 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 272 | `punch` | 97 | 3 | 1 | 273 | 20 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 273 | `punch` | 98 | 3 | 0 | 274 | 10 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 274 | `punch` | 109 | 3 | 2 | 275 | 10 | 0 | 0 |  |  |  |  |  |
| 275 | `punch` | 99 | 3 | 2 | 276 | 0 | 0 | 0 |  |  | kind=0 x=8 y=20 w=43 h=57 | kind=0 x=-51 y=28 w=119 h=29 (bdefend=60, dvx=20, fall=70, injury=60, vrest=7) |  |
| 276 | `punch` | 107 | 3 | 1 | 277 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 277 | `punch` | 108 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 39: Justin
<a id="personagem-39-justin"></a>

- **Arquivo (.dat):** `justin.dat`
- **Head (seleção):** `sprite\sys\justin_f.bmp`
- **Small (HUD):** `sprite\sys\justin_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\justin_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\justin_1.bmp` | 79×79 | 10×7 |
| 0-69 | `sprite\sys\justin_0b.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\justin_1b.bmp` | 79×79 | 10×7 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 250 |
| 0 | `standing` | `hit_Da` | 240 |
| 1 | `standing` | `hit_Fa` | 250 |
| 1 | `standing` | `hit_Da` | 240 |
| 2 | `standing` | `hit_Fa` | 250 |
| 2 | `standing` | `hit_Da` | 240 |
| 3 | `standing` | `hit_Fa` | 250 |
| 3 | `standing` | `hit_Da` | 240 |
| 4 | `standing` | `hit_Fa` | 250 |
| 4 | `standing` | `hit_Da` | 240 |
| 5 | `walking` | `hit_Fa` | 250 |
| 5 | `walking` | `hit_Da` | 240 |
| 6 | `walking` | `hit_Fa` | 250 |
| 6 | `walking` | `hit_Da` | 240 |
| 7 | `walking` | `hit_Fa` | 250 |
| 7 | `walking` | `hit_Da` | 240 |
| 8 | `walking` | `hit_Fa` | 250 |
| 8 | `walking` | `hit_Da` | 240 |
| 9 | `running` | `hit_Fa` | 250 |
| 9 | `running` | `hit_Da` | 240 |
| 10 | `running` | `hit_Fa` | 250 |
| 10 | `running` | `hit_Da` | 240 |
| 11 | `running` | `hit_Fa` | 250 |
| 11 | `running` | `hit_Da` | 240 |
| 110 | `defend` | `hit_Fa` | 250 |
| 110 | `defend` | `hit_Da` | 240 |
| 121 | `catching` | `hit_Ua` | 300 |
| 121 | `catching` | `hit_Da` | 274 |
| 245 | `punch` | `hit_a` | 260 |
| 245 | `punch` | `hit_Da` | 260 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 5 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 3 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 5 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 9 | 4 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 4 | `standing` | 2 | 0 | 0 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 54 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 55 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 56 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 27 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=29 y=7 w=22 h=73 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 2 | 21 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 25 | `normal_weapon_atck` | 70 | 3 | 2 | 21 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 30 | `jump_weapon_atck` | 72 | 3 | 2 | 31 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 31 | `jump_weapon_atck` | 73 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 35 | `run_weapon_atck` | 70 | 3 | 2 | 36 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 36 | `run_weapon_atck` | 71 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 40 | `dash_weapon_atck` | 72 | 3 | 2 | 41 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=33 h=70 |  |  |
| 41 | `dash_weapon_atck` | 73 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=10 w=39 h=66 |  |  |
| 45 | `light_weapon_thw` | 70 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=29 y=11 w=32 h=68; kind=0 x=17 y=40 w=24 h=10 |  |  |
| 46 | `light_weapon_thw` | 71 | 15 | 9 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=18 w=48 h=33; kind=0 x=27 y=49 w=23 h=29 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 | 0 | 0 | 0 |  |  | kind=0 x=29 y=7 w=22 h=73 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 72 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=28 y=4 w=31 h=63 |  |  |
| 53 | `sky_lgt_wp_thw` | 73 | 15 | 6 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=22 y=15 w=38 h=38; kind=0 x=19 y=37 w=26 h=29 |  |  |
| 55 | `weapon_drink` | 74 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 56 | `weapon_drink` | 75 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 57 | `weapon_drink` | 76 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 58 | `weapon_drink` | 75 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=25 y=26 w=37 h=53 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 2 | 0 | 0 |  |  | kind=0 x=28 y=12 w=33 h=70 | kind=2 x=27 y=57 w=36 h=25 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=10 w=39 h=70 | kind=0 x=31 y=34 w=44 h=21 (bdefend=16, dvx=2, injury=20) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 2 | 0 | 0 |  |  | kind=0 x=13 y=10 w=33 h=71 | kind=2 x=15 y=59 w=29 h=23 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=17 y=10 w=38 h=71 | kind=0 x=33 y=26 w=37 h=22 (bdefend=16, dvx=2, injury=20) |  |
| 70 | `super_punch` | 14 | 3 | 5 | 71 | 2 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=16 w=30 h=61 |  |  |
| 71 | `super_punch` | 15 | 3 | 2 | 72 | 0 | 0 | 0 |  |  | kind=0 x=20 y=14 w=34 h=66 | kind=0 x=18 y=19 w=56 h=41 (arest=15, bdefend=60, dvx=12, dvy=-7, fall=70, injury=70) |  |
| 72 | `super_punch` | 16 | 3 | 4 | 73 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=35 h=66 |  |  |
| 73 | `super_punch` | 17 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=23 y=10 w=29 h=69 |  |  |
| 80 | `jump_attack` | 60 | 3 | 4 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=26 y=13 w=35 h=55 |  |  |
| 81 | `jump_attack` | 61 | 3 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=16 w=41 h=55 | kind=0 x=28 y=38 w=45 h=31 (arest=15, bdefend=60, dvx=8, dvy=-5, fall=70, injury=50) |  |
| 85 | `run_attack` | 14 | 3 | 5 | 71 | 2 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=16 w=30 h=61 |  |  |
| 90 | `dash_attack` | 60 | 3 | 4 | 91 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=21 y=9 w=44 h=64 |  |  |
| 91 | `dash_attack` | 61 | 3 | 11 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=18 w=41 h=57 | kind=0 x=30 y=38 w=48 h=33 (arest=15, bdefend=60, dvx=14, dvy=-5, fall=70, injury=70) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 96 | `dash_attack` | 137 | 3 | 3 | 96 | 0 | 0 | 0 |  |  | kind=0 x=21 y=6 w=29 h=58 |  |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=31 h=68 |  |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=31 h=68 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=293 y=198 w=1 h=1 (vrest=1) |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 67 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 68 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 53 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 53 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 5 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 4 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 47 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 49 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 49 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=8 y=15 w=30 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=27 y=35 w=28 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 37 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 38 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 | kind=14 x=10 y=9 w=55 h=68 (vrest=1) |  |
| 202 | `ice` | 37 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 203 | `fire` | 8 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 9 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 18 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 19 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 36 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 36 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=11 w=29 h=61 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 0 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 218 | `stop_running` | 12 | 15 | 5 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=10 w=28 h=69 |  |  |
| 219 | `crouch2` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 47 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 49 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 47 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 46 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 49 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=25 y=13 w=30 h=65 |  |  |
| 225 | `injured` | 47 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 47 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 46 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 47 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 46 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `punch` | 80 | 3 | 4 | 241 | 0 | 0 | 0 | 75 | `data\031.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 241 | `punch` | 81 | 3 | 3 | 242 | 11 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 242 | `punch` | 82 | 3 | 1 | 243 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 | kind=0 x=9 y=37 w=65 h=20 (bdefend=16, dvx=9, dvy=-10, fall=70, injury=55, vrest=7) |  |
| 243 | `punch` | 83 | 3 | 0 | 244 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 | kind=0 x=31 y=16 w=52 h=40 (bdefend=16, dvx=9, dvy=-10, fall=70, injury=55, vrest=7) |  |
| 244 | `punch` | 84 | 3 | 1 | 245 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 | kind=0 x=7 y=22 w=34 h=30 (bdefend=16, dvx=-5, fall=70, injury=30, vrest=7) |  |
| 245 | `punch` | 85 | 3 | 1 | 246 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 | kind=0 x=9 y=29 w=25 h=24 (bdefend=16, dvx=-5, fall=70, injury=30, vrest=7) |  |
| 246 | `punch` | 86 | 3 | 3 | 999 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 250 | `ball` | 90 | 3 | 3 | 251 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 251 | `ball` | 91 | 3 | 3 | 252 | 0 | 0 | 0 | 75 | `data\047.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 252 | `ball` | 92 | 3 | 2 | 253 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  | id 226 (type 3): data\justin_ball.dat |
| 253 | `ball` | 91 | 3 | 2 | 254 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 254 | `ball` | 90 | 3 | 1 | 999 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 260 | `punch2` | 93 | 3 | 2 | 261 | 0 | 0 | 0 | 75 | `data\031.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 261 | `punch2` | 94 | 3 | 2 | 262 | 11 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 | kind=0 x=31 y=16 w=52 h=40 (bdefend=16, dvx=11, dvy=-9, fall=70, injury=30, vrest=7) |  |
| 262 | `punch2` | 95 | 3 | 2 | 263 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 | kind=0 x=31 y=16 w=52 h=40 (bdefend=16, dvx=11, dvy=-9, fall=70, injury=30, vrest=7) |  |
| 263 | `punch2` | 96 | 3 | 2 | 264 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 264 | `punch2` | 97 | 3 | 3 | 999 | 0 | 0 | 0 | 75 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 50: LouisEX
<a id="personagem-50-louisex"></a>

- **Arquivo (.dat):** `louisEX.dat`
- **Head (seleção):** `sprite\sys\louisEX_f.bmp`
- **Small (HUD):** `sprite\sys\louisEX_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\louisEX_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\louisEX_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\louisEX_2.bmp` | 79×79 | 10×7 |
| 210-251 | `sprite\sys\louisEX_3.bmp` | 118×79 | 6×7 |
| 252-252 | `sprite\sys\louisEX_4.bmp` | 79×124 | 1×1 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 260 |
| 0 | `standing` | `hit_Da` | 240 |
| 1 | `standing` | `hit_Fa` | 260 |
| 1 | `standing` | `hit_Da` | 240 |
| 2 | `standing` | `hit_Fa` | 260 |
| 2 | `standing` | `hit_Da` | 240 |
| 3 | `standing` | `hit_Fa` | 260 |
| 3 | `standing` | `hit_Da` | 240 |
| 5 | `walking` | `hit_Fa` | 260 |
| 5 | `walking` | `hit_Da` | 240 |
| 6 | `walking` | `hit_Fa` | 260 |
| 6 | `walking` | `hit_Da` | 240 |
| 7 | `walking` | `hit_Fa` | 260 |
| 7 | `walking` | `hit_Da` | 240 |
| 8 | `walking` | `hit_Fa` | 260 |
| 8 | `walking` | `hit_Da` | 240 |
| 110 | `defend` | `hit_Fa` | 260 |
| 110 | `defend` | `hit_Da` | 240 |
| 111 | `defend` | `hit_Fa` | 260 |
| 111 | `defend` | `hit_Da` | 240 |
| 121 | `catching` | `hit_j` | -260 |
| 263 | `air_push` | `hit_a` | 260 |
| 264 | `air_push` | `hit_a` | 260 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 9 | 1 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=37 h=68 |  |  |
| 1 | `standing` | 1 | 0 | 3 | 2 | 0 | 0 | 0 |  |  | kind=0 x=8 y=8 w=39 h=70 |  |  |
| 2 | `standing` | 2 | 0 | 6 | 3 | 0 | 0 | 0 |  |  | kind=0 x=9 y=8 w=38 h=70 |  |  |
| 3 | `standing` | 3 | 0 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=7 y=7 w=40 h=71 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=8 w=37 h=71 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=35 h=70 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=35 h=69 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=8 w=36 h=70 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=6 y=10 w=44 h=69 | kind=0 x=46 y=37 w=33 h=26 (bdefend=20, dvx=13, effect=1, fall=40, injury=15, vrest=11) |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=7 y=9 w=41 h=71 | kind=0 x=46 y=37 w=33 h=26 (bdefend=20, dvx=13, effect=1, fall=40, injury=15, vrest=11) |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=7 y=11 w=37 h=69 | kind=0 x=46 y=37 w=33 h=26 (bdefend=20, dvx=13, effect=1, fall=40, injury=15, vrest=11) |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=8 y=9 w=41 h=71 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=6 y=6 w=40 h=73 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=7 y=6 w=41 h=74 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=10 w=32 h=68 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=9 y=11 w=34 h=71 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=10 y=7 w=29 h=73 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=11 y=8 w=33 h=71 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 4 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=10 y=9 w=39 h=69 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 22 | 4 | 0 | 0 |  |  | kind=0 x=12 y=8 w=35 h=70 |  |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=17 y=12 w=33 h=68 |  |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=10 w=31 h=70; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 27 | 4 | 0 | 0 |  |  | kind=0 x=29 y=16 w=29 h=63 |  |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=28 y=12 w=31 h=66 |  |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=14 w=29 h=67 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 32 | 0 | 0 | 0 |  |  | kind=0 x=6 y=4 w=33 h=68 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=11 y=32 w=33 h=43; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 37 | 6 | 0 | 0 |  |  | kind=0 x=21 y=15 w=31 h=65 |  |  |
| 37 | `run_weapon_atck` | 86 | 3 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=19 y=17 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 42 | 0 | 0 | 0 |  |  | kind=0 x=6 y=0 w=36 h=71 |  |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=16 y=33 w=30 h=41; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=44 w=29 h=30; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 94 | 15 | 1 | 47 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=34 h=65; kind=0 x=9 y=36 w=17 h=18 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=37 y=11 w=34 h=57; kind=0 x=17 y=46 w=37 h=29 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 2 | 51 | 0 | 0 | 0 |  |  | kind=0 x=7 y=7 w=40 h=71 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=18 y=50 w=37 h=27; kind=0 x=31 y=11 w=42 h=48 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 54 | 0 | 0 | 0 |  |  | kind=0 x=15 y=9 w=29 h=54 |  |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=22 y=15 w=38 h=38; kind=0 x=15 y=45 w=29 h=26 |  |  |
| 55 | `weapon_drink` | 137 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=15 y=12 w=37 h=67 |  |  |
| 56 | `weapon_drink` | 138 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=16 y=20 w=36 h=59 |  |  |
| 57 | `weapon_drink` | 139 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=17 y=17 w=32 h=63 |  |  |
| 58 | `weapon_drink` | 138 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=36 h=65 |  |  |
| 60 | `punch` | 210 | 3 | 1 | 61 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=7) |  |
| 61 | `punch` | 211 | 3 | 1 | 62 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=9 w=52 h=68 | kind=0 x=28 y=27 w=84 h=24 (bdefend=30, dvx=2, effect=1, fall=25, injury=35) |  |
| 62 | `punch` | 212 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=8 w=54 h=68 |  |  |
| 65 | `punch` | 213 | 3 | 1 | 66 | 0 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=16 y=59 w=35 h=21 (vrest=7) |  |
| 66 | `punch` | 214 | 3 | 1 | 67 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=11 y=10 w=50 h=70 | kind=0 x=25 y=41 w=87 h=28 (bdefend=30, dvx=2, effect=1, fall=25, injury=35) |  |
| 67 | `punch` | 215 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=10 w=49 h=67 |  |  |
| 70 | `super_punch` | 153 | 9 | 2 | 254 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 75 | `run_attack` | 15 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=16 y=9 w=41 h=71 |  |  |
| 80 | `up_spear` | 222 | 9 | 2 | 81 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=13 y=9 w=45 h=47; kind=0 x=15 y=53 w=47 h=26 | kind=0 x=30 y=5 w=86 h=49 (bdefend=60, dvx=14, effect=1, fall=70, injury=70, vrest=12) |  |
| 81 | `up_spear` | 223 | 9 | 2 | 82 | 0 | 0 | 0 |  |  | kind=0 x=12 y=13 w=42 h=41; kind=0 x=3 y=46 w=51 h=33 | kind=0 x=25 y=21 w=84 h=60 (bdefend=60, dvx=14, effect=1, fall=70, injury=70, vrest=12) |  |
| 82 | `up_spear` | 224 | 9 | 2 | 83 | 0 | 0 | 0 |  |  | kind=0 x=46 y=10 w=46 h=45; kind=0 x=39 y=54 w=47 h=26 | kind=0 x=-2 y=43 w=41 h=35 (bdefend=60, dvx=-7, fall=70, injury=55, vrest=12) |  |
| 83 | `up_spear` | 225 | 9 | 3 | 84 | 0 | 0 | 0 |  |  | kind=0 x=45 y=5 w=43 h=75 |  |  |
| 84 | `up_spear` | 226 | 9 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=45 y=8 w=38 h=71 |  |  |
| 85 | `run_attack` | 10 | 3 | 2 | 86 | 6 | 0 | 0 | 30 | `data\031.wav` | kind=0 x=18 y=11 w=40 h=68 |  |  |
| 86 | `run_attack` | 11 | 3 | 1 | 87 | 4 | 0 | 0 |  |  | kind=0 x=12 y=5 w=37 h=74 |  |  |
| 87 | `run_attack` | 12 | 3 | 2 | 88 | 16 | 0 | 0 |  |  | kind=0 x=10 y=10 w=48 h=70 | kind=0 x=12 y=19 w=68 h=41 (bdefend=60, dvx=26, fall=70, injury=80, vrest=10) |  |
| 88 | `run_attack` | 13 | 3 | 1 | 89 | 0 | 0 | 0 |  |  | kind=0 x=16 y=8 w=40 h=70 | kind=0 x=18 y=25 w=58 h=34 (bdefend=60, dvx=22, fall=70, injury=70, vrest=10) |  |
| 89 | `run_attack` | 14 | 3 | 2 | 75 | 0 | 0 | 0 |  |  | kind=0 x=16 y=9 w=41 h=71 |  |  |
| 90 | `dash_attack` | 132 | 15 | 2 | 92 | -2 | 0 | 0 | 40 | `data\031.wav` | kind=0 x=19 y=8 w=27 h=61; kind=0 x=11 y=31 w=43 h=22 |  |  |
| 91 | `dash_attack` | 132 | 15 | 1 | 92 | 0 | 0 | 0 |  |  | kind=0 x=19 y=16 w=28 h=53; kind=0 x=10 y=32 w=46 h=20 |  |  |
| 92 | `dash_attack` | 133 | 15 | 1 | 93 | 0 | 0 | 0 |  |  | kind=0 x=27 y=11 w=25 h=55; kind=0 x=22 y=25 w=39 h=31 |  |  |
| 93 | `dash_attack` | 134 | 100 | 90 | 216 | 25 | 2 | 0 |  |  | kind=0 x=29 y=19 w=39 h=45; kind=0 x=16 y=38 w=36 h=29 | kind=0 x=28 y=34 w=57 h=45 (bdefend=60, dvx=14, dvy=5, fall=70, injury=85, vrest=8) |  |
| 94 | `dash_attack` | 135 | 15 | 2 | 95 | 0 | 0 | 0 |  |  | kind=0 x=42 y=24 w=32 h=52; kind=0 x=18 y=50 w=47 h=28 |  |  |
| 95 | `dash_attack` | 136 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=38 y=24 w=24 h=55; kind=0 x=15 y=46 w=34 h=32 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=7) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=7) |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=34 y=41 w=13 h=26 (vrest=7) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=7) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=7) |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=34 y=47 w=12 h=22 (vrest=7) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=19 w=38 h=60 |  |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=16 y=19 w=42 h=60 |  |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=7) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=7) |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=7) |  |
| 115 | `picking_light` | 36 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=18 y=42 w=34 h=43; kind=0 x=16 y=19 w=33 h=25 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 1 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=15 y=47 w=36 h=31; kind=0 x=13 y=17 w=40 h=38 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=16 w=37 h=37; kind=0 x=13 y=52 w=32 h=28 |  |  |
| 120 | `catching` | 51 | 9 | 1 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 4 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=24 y=5 w=32 h=71 |  |  |
| 130 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=25 y=40 w=26 h=20 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 8 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 9 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 8 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 78 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=70) |  |
| 204 | `fire` | 79 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=70) |  |
| 205 | `fire` | 88 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=70) |  |
| 206 | `fire` | 89 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=70) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=18 y=23 w=30 h=59 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=17 y=20 w=31 h=61 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=13 y=-2 w=29 h=76; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 8 | 216 | 0 | 0 | 0 |  |  | kind=0 x=21 y=0 w=36 h=29; kind=0 x=11 y=29 w=40 h=24; kind=0 x=9 y=49 w=27 h=28 |  |  |
| 214 | `dash` | 64 | 5 | 8 | 217 | 0 | 0 | 0 |  |  | kind=0 x=15 y=4 w=37 h=46; kind=0 x=25 y=37 w=27 h=32 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 114 | 15 | 3 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=10 y=11 w=44 h=71 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=7) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=7) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=7) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=7) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=5 y=14 w=42 h=63 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=24 y=8 w=40 h=52; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=25 y=15 w=39 h=46; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 239 | `up_spear` | 252 | 9 | 1 | 250 | 0 | 0 | 0 |  |  | kind=0 x=28 y=58 w=43 h=65 | kind=0 x=20 y=19 w=86 h=62 (bdefend=60, dvx=2, dvy=-10, effect=1, fall=70, injury=60, vrest=75); kind=0 x=75 y=59 w=11 h=64 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 240 | `up_spear` | 140 | 9 | 1 | 241 | 0 | 0 | 0 |  | `data\097.wav` | kind=0 x=12 y=7 w=41 h=50; kind=0 x=14 y=54 w=30 h=28 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 241 | `up_spear` | 141 | 9 | 1 | 242 | 0 | 0 | 0 |  |  | kind=0 x=13 y=10 w=34 h=70 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 242 | `up_spear` | 142 | 9 | 1 | 243 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=11 y=7 w=33 h=49; kind=0 x=13 y=55 w=29 h=26 | kind=0 x=11 y=31 w=68 h=39 (bdefend=60, dvx=2, dvy=-13, fall=60, injury=40, vrest=9); kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 243 | `up_spear` | 143 | 9 | 1 | 244 | 2 | 0 | 0 |  |  | kind=0 x=6 y=8 w=38 h=50; kind=0 x=12 y=51 w=29 h=27 | kind=0 x=7 y=1 w=64 h=56 (bdefend=60, dvx=2, dvy=-13, fall=60, injury=40, vrest=9); kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 244 | `up_spear` | 144 | 9 | 1 | 245 | 2 | 0 | 0 |  |  | kind=0 x=16 y=7 w=35 h=48; kind=0 x=19 y=56 w=30 h=24 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 245 | `up_spear` | 145 | 9 | 1 | 246 | 2 | 0 | 0 |  |  | kind=0 x=22 y=7 w=42 h=55; kind=0 x=30 y=52 w=28 h=27 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 246 | `up_spear` | 146 | 9 | 1 | 247 | 0 | 0 | 0 |  |  | kind=0 x=30 y=10 w=35 h=46; kind=0 x=28 y=55 w=44 h=23 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 247 | `up_spear` | 147 | 9 | 1 | 248 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=10 y=8 w=37 h=50; kind=0 x=9 y=54 w=46 h=26 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7); kind=0 x=-4 y=1 w=26 h=41 (bdefend=60, dvx=-6, effect=1, fall=70, injury=30, vrest=12) |  |
| 248 | `up_spear` | 148 | 9 | 1 | 249 | 0 | 0 | 0 |  |  | kind=0 x=24 y=14 w=45 h=42; kind=0 x=20 y=52 w=47 h=27 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7); kind=0 x=-2 y=22 w=38 h=46 (bdefend=60, dvx=-6, effect=1, fall=70, injury=30, vrest=12) |  |
| 249 | `up_spear` | 149 | 9 | 1 | 239 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=3 y=11 w=34 h=43; kind=0 x=1 y=52 w=43 h=30 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7); kind=0 x=3 y=0 w=79 h=72 (bdefend=60, dvx=2, dvy=-10, effect=1, fall=70, injury=60, vrest=12) |  |
| 250 | `up_spear` | 150 | 9 | 1 | 251 | 0 | 0 | 0 |  |  | kind=0 x=33 y=13 w=32 h=48; kind=0 x=22 y=54 w=55 h=26 | kind=0 x=60 y=8 w=14 h=70 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7); kind=0 x=6 y=1 w=40 h=73 (bdefend=60, dvx=-6, effect=1, fall=70, injury=30, vrest=12) |  |
| 251 | `up_spear` | 151 | 9 | 1 | 252 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=39 h=43; kind=0 x=23 y=54 w=36 h=26 |  |  |
| 252 | `up_spear` | 152 | 9 | 1 | 253 | 0 | 0 | 0 |  |  | kind=0 x=19 y=13 w=33 h=50; kind=0 x=15 y=59 w=47 h=20 |  |  |
| 253 | `up_spear` | 153 | 9 | 1 | 254 | 0 | 0 | 0 |  |  | kind=0 x=37 y=13 w=36 h=43; kind=0 x=33 y=52 w=41 h=27 |  |  |
| 254 | `up_spear` | 216 | 9 | 1 | 255 | 0 | 0 | 0 |  | `data\054.wav` | kind=0 x=13 y=9 w=45 h=47; kind=0 x=15 y=53 w=47 h=26 | kind=0 x=30 y=5 w=86 h=49 (bdefend=60, dvx=14, effect=1, fall=70, injury=70, vrest=12) |  |
| 255 | `up_spear` | 217 | 9 | 1 | 256 | 2 | 0 | 0 |  |  | kind=0 x=12 y=13 w=42 h=41; kind=0 x=3 y=46 w=51 h=33 | kind=0 x=25 y=21 w=84 h=60 (bdefend=60, dvx=14, effect=1, fall=70, injury=70, vrest=12) |  |
| 256 | `up_spear` | 218 | 9 | 1 | 257 | 2 | 0 | 0 |  |  | kind=0 x=46 y=10 w=46 h=45; kind=0 x=39 y=54 w=47 h=26 | kind=0 x=-2 y=43 w=41 h=35 (bdefend=60, dvx=-7, fall=70, injury=55, vrest=12) |  |
| 257 | `up_spear` | 219 | 9 | 2 | 258 | 0 | 0 | 0 |  |  | kind=0 x=45 y=5 w=43 h=75 |  |  |
| 258 | `up_spear` | 220 | 9 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=45 y=8 w=38 h=71 |  |  |
| 260 | `air_push` | 100 | 9 | 1 | 261 | 0 | 0 | 0 | 100 | `data\097.wav` | kind=0 x=15 y=9 w=41 h=70 |  |  |
| 261 | `air_push` | 101 | 9 | 3 | 262 | 0 | 0 | 0 |  |  | kind=0 x=12 y=5 w=44 h=74 |  |  |
| 262 | `air_push` | 102 | 9 | 1 | 263 | 0 | 0 | 0 |  | `data\020.wav` | kind=0 x=11 y=6 w=42 h=73 |  | id 204 (type 3): data\henry_wind.dat |
| 263 | `air_push` | 103 | 9 | 4 | 264 | 0 | 0 | 0 |  |  | kind=0 x=10 y=8 w=43 h=71 |  | id 204 (type 3): data\henry_wind.dat |
| 264 | `air_push` | 104 | 9 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=8 w=46 h=70 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 51: Firzen
<a id="personagem-51-firzen"></a>

- **Arquivo (.dat):** `firzen.dat`
- **Head (seleção):** `sprite\sys\firzen_f.bmp`
- **Small (HUD):** `sprite\sys\firzen_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\firzen_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\firzen_1.bmp` | 79×79 | 10×7 |
| 140-142 | `sprite\sys\firzen_2.bmp` | 159×79 | 3×1 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fj` | 265 |
| 0 | `standing` | `hit_Ua` | 240 |
| 0 | `standing` | `hit_Uj` | 249 |
| 1 | `standing` | `hit_Fj` | 265 |
| 1 | `standing` | `hit_Ua` | 240 |
| 1 | `standing` | `hit_Uj` | 249 |
| 2 | `standing` | `hit_Fj` | 265 |
| 2 | `standing` | `hit_Ua` | 240 |
| 2 | `standing` | `hit_Uj` | 249 |
| 3 | `standing` | `hit_Fj` | 265 |
| 3 | `standing` | `hit_Ua` | 240 |
| 3 | `standing` | `hit_Uj` | 249 |
| 4 | `standing` | `hit_Fj` | 265 |
| 4 | `standing` | `hit_Ua` | 240 |
| 4 | `standing` | `hit_Uj` | 249 |
| 5 | `walking` | `hit_Fj` | 265 |
| 5 | `walking` | `hit_Ua` | 240 |
| 5 | `walking` | `hit_Uj` | 249 |
| 6 | `walking` | `hit_Fj` | 265 |
| 6 | `walking` | `hit_Ua` | 240 |
| 6 | `walking` | `hit_Uj` | 249 |
| 7 | `walking` | `hit_Fj` | 265 |
| 7 | `walking` | `hit_Ua` | 240 |
| 7 | `walking` | `hit_Uj` | 249 |
| 8 | `walking` | `hit_Fj` | 265 |
| 8 | `walking` | `hit_Ua` | 240 |
| 8 | `walking` | `hit_Uj` | 249 |
| 110 | `defend` | `hit_Fj` | 265 |
| 110 | `defend` | `hit_Ua` | 240 |
| 110 | `defend` | `hit_Uj` | 249 |
| 111 | `defend` | `hit_Fj` | 265 |
| 111 | `defend` | `hit_Ua` | 240 |
| 111 | `defend` | `hit_Uj` | 249 |
| 246 | `disaster` | `hit_a` | 240 |
| 246 | `disaster` | `hit_Ua` | 240 |
| 270 | `ball` | `hit_j` | 280 |
| 270 | `ball` | `hit_d` | 280 |
| 271 | `ball` | `hit_j` | 280 |
| 271 | `ball` | `hit_d` | 280 |
| 272 | `ball` | `hit_j` | 280 |
| 272 | `ball` | `hit_d` | 280 |
| 273 | `ball` | `hit_j` | 280 |
| 273 | `ball` | `hit_d` | 280 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 10 | 1 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 1 | `standing` | 1 | 0 | 4 | 2 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 2 | `standing` | 2 | 0 | 3 | 3 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 3 | `standing` | 3 | 0 | 10 | 4 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 4 | `standing` | 1 | 0 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=28 y=15 w=27 h=65 | kind=1 x=40 y=16 w=25 h=65 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 20 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 10 | `running` | 21 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 11 | `running` | 22 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=25 y=19 w=38 h=60 |  |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=29 y=15 w=28 h=64 |  |  |
| 19 | `heavy_stop_run` | 128 | 15 | 4 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=25 y=5 w=31 h=79 |  |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 22 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=33 h=69 |  |  |
| 22 | `normal_weapon_atck` | 71 | 3 | 1 | 23 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=25 y=11 w=36 h=68 |  |  |
| 23 | `normal_weapon_atck` | 72 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=9 w=36 h=66; kind=0 x=25 y=61 w=17 h=18 |  |  |
| 25 | `normal_weapon_atck` | 70 | 3 | 1 | 22 | 0 | 0 | 0 |  |  | kind=0 x=18 y=11 w=33 h=69 |  |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=26 y=7 w=32 h=58 |  |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=11 y=32 w=33 h=43; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=11 y=32 w=33 h=43; kind=0 x=26 y=9 w=26 h=37 |  |  |
| 35 | `run_weapon_atck` | 70 | 3 | 1 | 36 | 6 | 0 | 0 |  |  | kind=0 x=21 y=15 w=31 h=65 |  |  |
| 36 | `run_weapon_atck` | 71 | 3 | 2 | 37 | 4 | 0 | 0 |  | `data\008.wav` | kind=0 x=23 y=15 w=34 h=65 |  |  |
| 37 | `run_weapon_atck` | 72 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=17 w=34 h=64 |  |  |
| 40 | `dash_weapon_atck` | 80 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=25 y=10 w=35 h=52 |  |  |
| 41 | `dash_weapon_atck` | 81 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=8 y=32 w=33 h=45; kind=0 x=27 y=13 w=30 h=33 |  |  |
| 42 | `dash_weapon_atck` | 82 | 3 | 8 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=32 w=33 h=45; kind=0 x=23 y=13 w=31 h=37 |  |  |
| 45 | `light_weapon_thw` | 95 | 15 | 1 | 46 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=34 h=65; kind=0 x=9 y=36 w=17 h=18 |  |  |
| 46 | `light_weapon_thw` | 96 | 15 | 1 | 47 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=24 y=22 w=32 h=57 |  |  |
| 47 | `light_weapon_thw` | 96 | 15 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=38 w=54 h=21; kind=0 x=26 y=55 w=32 h=26 |  |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 2 | 51 | 0 | 0 | 0 |  |  | kind=0 x=28 y=19 w=30 h=62 |  |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=8 y=39 w=61 h=23; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 1 | 53 | 0 | 0 | 0 |  |  | kind=0 x=15 y=9 w=29 h=54 |  |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=35 y=10 w=36 h=23; kind=0 x=15 y=18 w=30 h=43 |  |  |
| 54 | `sky_lgt_wp_thw` | 98 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=22 y=15 w=38 h=38; kind=0 x=3 y=37 w=34 h=26 |  |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=15 y=12 w=37 h=67 |  |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=16 y=20 w=36 h=59 |  |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=17 y=17 w=32 h=63 |  |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=36 h=65 |  |  |
| 60 | `punch` | 10 | 3 | 1 | 61 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=21 y=57 w=37 h=24 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 2 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=11 w=35 h=69 | kind=0 x=33 y=33 w=42 h=16 (bdefend=16, dvx=2, injury=20) |  |
| 65 | `punch` | 12 | 3 | 1 | 66 | 1 | 0 | 0 |  |  | kind=0 x=26 y=12 w=27 h=68 | kind=2 x=16 y=59 w=35 h=21 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 2 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=25 y=12 w=37 h=69 | kind=0 x=30 y=34 w=48 h=18 (bdefend=16, dvx=2, injury=20) |  |
| 70 | `super_punch` | 17 | 3 | 1 | 71 | 2 | 0 | 0 |  |  | kind=0 x=16 y=12 w=12 h=65 |  |  |
| 71 | `super_punch` | 18 | 3 | 1 | 72 | 7 | 0 | 0 |  |  | kind=0 x=15 y=12 w=14 h=66 |  |  |
| 72 | `super_punch` | 19 | 3 | 1 | 73 | 7 | 0 | 0 |  | `data\007.wav` | kind=0 x=13 y=5 w=15 h=67 |  |  |
| 73 | `super_punch` | 29 | 3 | 2 | 74 | 7 | 0 | 0 |  |  | kind=0 x=6 y=12 w=17 h=59 | kind=0 x=5 y=31 w=72 h=26 (bdefend=60, dvx=13, dvy=-6, fall=70, injury=80, vrest=10) |  |
| 74 | `super_punch` | 39 | 3 | 2 | 75 | 7 | 0 | 0 |  |  | kind=0 x=8 y=17 w=36 h=61 |  |  |
| 75 | `super_punch` | 49 | 3 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=17 y=14 w=35 h=64 |  |  |
| 80 | `jump_attack` | 14 | 3 | 3 | 81 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=15 y=10 w=39 h=63 |  |  |
| 81 | `jump_attack` | 15 | 3 | 3 | 82 | 0 | 0 | 0 |  |  | kind=0 x=21 y=14 w=33 h=57 | kind=0 x=14 y=27 w=61 h=43 (bdefend=30, dvx=7, fall=70, injury=50, vrest=12) |  |
| 82 | `jump_attack` | 16 | 3 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=17 y=13 w=39 h=58 | kind=0 x=13 y=34 w=66 h=35 (arest=15, bdefend=30, dvx=7, fall=70, injury=50) |  |
| 85 | `run_attack` | 17 | 3 | 1 | 71 | 9 | 0 | 0 |  |  | kind=0 x=22 y=14 w=30 h=65 |  |  |
| 90 | `dash_attack` | 14 | 15 | 3 | 91 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=25 h=55; kind=0 x=13 y=36 w=52 h=18; kind=0 x=31 y=9 w=25 h=29 |  |  |
| 91 | `dash_attack` | 15 | 15 | 3 | 92 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=22 y=40 w=33 h=26; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=9 y=32 w=69 h=35 (bdefend=60, dvx=17, fall=70, injury=90, vrest=12) |  |
| 92 | `dash_attack` | 16 | 15 | 9 | 216 | 0 | 0 | 0 |  |  | kind=0 x=23 y=38 w=28 h=25; kind=0 x=22 y=36 w=50 h=18; kind=0 x=24 y=6 w=30 h=34 | kind=0 x=10 y=29 w=68 h=40 (arest=20, bdefend=60, dvx=17, fall=70, injury=90) |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=28 h=36; kind=0 x=28 y=37 w=24 h=34 |  |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=293 y=198 w=1 h=1 (vrest=1) |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 | 9 | 0 | 0 |  |  |  | kind=7 x=36 y=54 w=13 h=25 (vrest=1) |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 56 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=9 w=45 h=70 | kind=0 x=55 y=25 w=20 h=40 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 111 | `defend` | 57 | 7 | 0 | 110 | 0 | 0 | 0 |  |  | kind=0 x=20 y=8 w=42 h=75 | kind=0 x=51 y=24 w=26 h=43 (bdefend=16, dvx=12, effect=4, fall=70, injury=45, vrest=7) |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 | -2 | 0 | 0 |  |  | kind=0 x=5 y=17 w=64 h=65 | kind=6 x=-9 y=16 w=85 h=65 (vrest=1) |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=32 y=24 w=28 h=28 |  |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=24 y=43 w=33 h=37; kind=0 x=36 y=26 w=26 h=25 |  |  |
| 117 | `picking_heavy` | 36 | 15 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=43 w=33 h=37; kind=0 x=34 y=19 w=26 h=37 |  |  |
| 120 | `catching` | 51 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 121 | `catching` | 50 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 122 | `catching` | 51 | 9 | 3 | 123 | 0 | 0 | 0 |  |  | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 123 | `catching` | 52 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=19 y=15 w=28 h=65 |  |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 30 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=25 w=21 h=20 | kind=4 x=21 y=14 w=29 h=44 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 31 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=20 w=24 h=23 | kind=4 x=15 y=11 w=42 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=35 y=30 w=27 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 32 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=20 h=18 | kind=4 x=13 y=18 w=46 h=26 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 33 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=30 w=27 h=21 | kind=4 x=32 y=18 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=10 y=38 w=38 h=21 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 34 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 35 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 40 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=24 w=25 h=23 | kind=4 x=22 y=12 w=36 h=50 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 41 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=27 w=24 h=26 | kind=4 x=33 y=6 w=26 h=46 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=26 y=43 w=21 h=29 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 42 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=31 w=24 h=21 | kind=4 x=14 y=29 w=58 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 43 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 44 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 45 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 8 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=10 y=9 w=55 h=68 |  |  |
| 201 | `ice` | 9 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 | kind=14 x=8 y=6 w=67 h=73 (vrest=1) |  |
| 202 | `ice` | 8 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 37 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 38 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 67 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 68 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 60 | 4 | 1 | 211 | 0 | 0 | 0 |  |  | kind=0 x=22 y=24 w=35 h=58 |  |  |
| 211 | `jump` | 61 | 4 | 1 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=26 y=26 w=34 h=56 |  |  |
| 212 | `jump` | 62 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=28 y=3 w=23 h=65; kind=0 x=18 y=29 w=48 h=17 |  |  |
| 213 | `dash` | 63 | 5 | 5 | 216 | 0 | 0 | 0 |  |  | kind=0 x=43 y=5 w=23 h=33; kind=0 x=28 y=29 w=21 h=33; kind=0 x=18 y=48 w=27 h=21 |  |  |
| 214 | `dash` | 64 | 5 | 5 | 217 | 0 | 0 | 0 |  |  | kind=0 x=20 y=5 w=27 h=38; kind=0 x=16 y=37 w=36 h=22 |  |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=25 y=26 w=31 h=54 |  |  |
| 216 | `dash` | 112 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=35 y=8 w=27 h=27; kind=0 x=16 y=30 w=39 h=37 |  |  |
| 217 | `dash` | 113 | 5 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=13 w=29 h=51 |  |  |
| 218 | `stop_running` | 94 | 15 | 3 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=17 y=25 w=30 h=55; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 60 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=26 y=36 w=29 h=44 |  |  |
| 220 | `injured` | 120 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=25 y=17 w=29 h=61 |  |  |
| 221 | `injured` | 121 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=32 y=16 w=27 h=63; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 123 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=39 h=31; kind=0 x=25 y=53 w=40 h=27 |  |  |
| 223 | `injured` | 124 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=12 y=23 w=40 h=37; kind=0 x=27 y=56 w=36 h=24 |  |  |
| 224 | `injured` | 130 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=32 y=18 w=28 h=60; kind=0 x=52 y=38 w=20 h=19 |  |  |
| 225 | `injured` | 131 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=24 y=18 w=35 h=63 |  |  |
| 226 | `injured` | 120 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=27 y=22 w=42 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 122 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=28 y=24 w=39 h=57 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 121 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=28 y=23 w=37 h=58 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 122 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=29 y=26 w=37 h=53 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 34 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 44 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 27 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=18 y=15 w=36 h=65 |  |  |
| 233 | `throw_lying_man` | 28 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=28 w=61 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 28 | 9 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=30 w=57 h=28; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 240 | `disaster` | 83 | 3 | 1 | 241 | 0 | 0 | 0 | 100 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 241 | `disaster` | 84 | 3 | 1 | 242 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 242 | `disaster` | 85 | 3 | 1 | 243 | 0 | 0 | 0 |  | `data\019.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 243 | `disaster` | 86 | 3 | 1 | 244 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  | id 221 (type 3): data\firzen_chasef.dat |
| 244 | `disaster` | 87 | 3 | 1 | 245 | 0 | 0 | 0 |  | `data\020.wav` | kind=0 x=21 y=18 w=43 h=62 |  | id 221 (type 3): data\firzen_chasef.dat |
| 245 | `disaster` | 88 | 3 | 1 | 246 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 246 | `disaster` | 84 | 3 | 2 | 247 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 247 | `disaster` | 83 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 249 | `volcano` | 83 | 3 | 0 | 250 | 0 | 0 | 0 | 250 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 250 | `volcano` | 73 | 7 | 0 | 251 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 251 | `volcano` | 74 | 7 | 1 | 252 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 252 | `volcano` | 75 | 7 | 2 | 253 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 253 | `volcano` | 76 | 7 | 1 | 254 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 254 | `volcano` | 77 | 7 | 0 | 255 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 255 | `volcano` | 75 | 7 | 1 | 256 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 256 | `volcano` | 76 | 7 | 1 | 257 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 257 | `volcano` | 77 | 7 | 2 | 258 | 0 | 0 | 0 |  | `data\071.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 258 | `volcano` | 78 | 3 | 20 | 259 | 0 | 0 | 0 |  | `data\020.wav` | kind=0 x=21 y=18 w=43 h=62 |  | id 221 (type 3): data\firzen_chasef.dat |
| 259 | `volcano` | 79 | 3 | 2 | 260 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 260 | `volcano` | 89 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 265 | `ball` | 89 | 3 | 1 | 266 | 0 | 0 | 0 | 25 |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 266 | `ball` | 79 | 3 | 1 | 267 | 0 | 0 | 0 |  | `data\100.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 267 | `ball` | 90 | 3 | 1 | 268 | 0 | 0 | 0 |  | `data\067.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 268 | `ball` | 91 | 3 | 1 | 269 | 0 | 0 | 0 |  | `data\064.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 269 | `ball` | 92 | 3 | 1 | 270 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 270 | `ball` | 140 | 3 | 3 | 271 | 0 | 0 | 0 |  | `data\092.wav` | kind=0 x=21 y=18 w=43 h=62 |  | id 223 (type 3): data\firzen_ball.dat |
| 271 | `ball` | 141 | 3 | 0 | 272 | 0 | 0 | 0 |  | `data\070.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 272 | `ball` | 142 | 3 | 0 | 273 | 0 | 0 | 0 | -14 |  | kind=0 x=21 y=18 w=43 h=62 |  | id 223 (type 3): data\firzen_ball.dat |
| 273 | `ball` | 140 | 3 | 1 | 271 | 0 | 0 | 0 |  | `data\092.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 280 | `ball` | 83 | 3 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 290 | `mix` | 104 | 7 | 2 | 257 | 0 | 0 | 0 |  | `data\052.wav` | kind=0 x=21 y=18 w=43 h=62 |  |  |
| 399 | `dummy` | 0 | 0 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---

## Personagem 52: Julian
<a id="personagem-52-julian"></a>

- **Arquivo (.dat):** `julian.dat`
- **Head (seleção):** `sprite\sys\julian_f.bmp`
- **Small (HUD):** `sprite\sys\julian_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-49 | `sprite\sys\julian_0.bmp` | 79×99 | 10×5 |
| 50-63 | `sprite\sys\julian_1.bmp` | 109×99 | 7×2 |
| 64-113 | `sprite\sys\julian_2.bmp` | 79×99 | 10×5 |

### Parâmetros de movimento (do bloco pós-<bmp_end>)
_Não encontrado (ou arquivo fora do padrão de personagem)._

### Inputs mapeados (`hit_*`) encontrados
Esses bindings normalmente aparecem em frames base (standing/walking/defend) e apontam para o **primeiro frame** de um golpe/move.

<details><summary>Ver lista de bindings (pode ser longa)</summary>


| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 260 |
| 0 | `standing` | `hit_Fj` | 280 |
| 0 | `standing` | `hit_Ua` | 235 |
| 0 | `standing` | `hit_Uj` | 310 |
| 0 | `standing` | `hit_ja` | 106 |
| 1 | `standing` | `hit_Fa` | 260 |
| 1 | `standing` | `hit_Fj` | 280 |
| 1 | `standing` | `hit_Ua` | 235 |
| 1 | `standing` | `hit_Uj` | 310 |
| 1 | `standing` | `hit_ja` | 106 |
| 2 | `standing` | `hit_Fa` | 260 |
| 2 | `standing` | `hit_Fj` | 280 |
| 2 | `standing` | `hit_Ua` | 235 |
| 2 | `standing` | `hit_Uj` | 310 |
| 2 | `standing` | `hit_ja` | 106 |
| 3 | `standing` | `hit_Fa` | 260 |
| 3 | `standing` | `hit_Fj` | 280 |
| 3 | `standing` | `hit_Ua` | 235 |
| 3 | `standing` | `hit_Uj` | 310 |
| 3 | `standing` | `hit_ja` | 106 |
| 4 | `standing` | `hit_Fa` | 260 |
| 4 | `standing` | `hit_Fj` | 280 |
| 4 | `standing` | `hit_Ua` | 235 |
| 4 | `standing` | `hit_Uj` | 310 |
| 4 | `standing` | `hit_ja` | 106 |
| 5 | `walking` | `hit_Fa` | 260 |
| 5 | `walking` | `hit_Fj` | 280 |
| 5 | `walking` | `hit_Ua` | 235 |
| 5 | `walking` | `hit_Uj` | 310 |
| 5 | `walking` | `hit_ja` | 106 |
| 6 | `walking` | `hit_Fa` | 260 |
| 6 | `walking` | `hit_Fj` | 280 |
| 6 | `walking` | `hit_Ua` | 235 |
| 6 | `walking` | `hit_Uj` | 310 |
| 6 | `walking` | `hit_ja` | 106 |
| 7 | `walking` | `hit_Fa` | 260 |
| 7 | `walking` | `hit_Fj` | 280 |
| 7 | `walking` | `hit_Ua` | 235 |
| 7 | `walking` | `hit_Uj` | 310 |
| 7 | `walking` | `hit_ja` | 106 |
| 8 | `walking` | `hit_Fa` | 260 |
| 8 | `walking` | `hit_Fj` | 280 |
| 8 | `walking` | `hit_Ua` | 235 |
| 8 | `walking` | `hit_Uj` | 310 |
| 8 | `walking` | `hit_ja` | 106 |
| 9 | `running` | `hit_Fa` | 260 |
| 9 | `running` | `hit_Fj` | 280 |
| 9 | `running` | `hit_Ua` | 235 |
| 9 | `running` | `hit_Uj` | 310 |
| 9 | `running` | `hit_ja` | 106 |
| 10 | `running` | `hit_Fa` | 260 |
| 10 | `running` | `hit_Fj` | 280 |
| 10 | `running` | `hit_Ua` | 235 |
| 10 | `running` | `hit_Uj` | 310 |
| 10 | `running` | `hit_ja` | 106 |
| 11 | `running` | `hit_Fa` | 260 |
| 11 | `running` | `hit_Fj` | 280 |
| 11 | `running` | `hit_Ua` | 235 |
| 11 | `running` | `hit_Uj` | 310 |
| 11 | `running` | `hit_ja` | 106 |
| 79 | `singlong` | `hit_a` | 80 |
| 89 | `singlong` | `hit_a` | 80 |
| 105 | `rowing` | `hit_j` | 106 |
| 105 | `rowing` | `hit_Fa` | 260 |
| 110 | `defend` | `hit_Fa` | 260 |
| 110 | `defend` | `hit_Fj` | 280 |
| 110 | `defend` | `hit_Ua` | 235 |
| 110 | `defend` | `hit_Uj` | 310 |
| 110 | `defend` | `hit_ja` | 106 |
| 111 | `defend` | `hit_Fa` | 260 |
| 111 | `defend` | `hit_Fj` | 280 |
| 111 | `defend` | `hit_Ua` | 235 |
| 111 | `defend` | `hit_Uj` | 310 |
| 111 | `defend` | `hit_ja` | 106 |
| 262 | `ball` | `hit_j` | 276 |
| 262 | `ball` | `hit_d` | 276 |
| 266 | `ball` | `hit_j` | 276 |
| 266 | `ball` | `hit_d` | 276 |
| 269 | `ball` | `hit_j` | 276 |
| 269 | `ball` | `hit_d` | 276 |
| 272 | `ball` | `hit_j` | 276 |
| 272 | `ball` | `hit_d` | 276 |
| 275 | `ball` | `hit_a` | 263 |
| 275 | `ball` | `hit_j` | 276 |
| 275 | `ball` | `hit_d` | 276 |
| 275 | `ball` | `hit_Fa` | 263 |

</details>

### Tabela completa de frames
<details><summary>Ver todos os frames (pode ser MUITO longo)</summary>


| id | nome | pic | state | wait | next | dvx | dvy | dvz | mp | sound | bdy | itr | opoint (oid) |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|---|---|---|---|
| 0 | `standing` | 0 | 0 | 10 | 1 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 1 | `standing` | 1 | 0 | 4 | 2 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 2 | `standing` | 2 | 0 | 3 | 3 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 3 | `standing` | 3 | 0 | 8 | 4 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 4 | `standing` | 2 | 0 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 5 | `walking` | 4 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=31 y=10 w=57 h=89 (catchingact=120, caughtact=130) |  |
| 6 | `walking` | 5 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=33 y=11 w=59 h=90 (catchingact=120, caughtact=130) |  |
| 7 | `walking` | 6 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=32 y=9 w=60 h=92 (catchingact=120, caughtact=130) |  |
| 8 | `walking` | 7 | 1 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=1 x=32 y=6 w=63 h=96 (catchingact=120, caughtact=130) |  |
| 9 | `running` | 26 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 10 | `running` | 27 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 11 | `running` | 28 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 12 | `heavy_obj_walk` | 30 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=10 y=13 w=63 h=86 |  |  |
| 13 | `heavy_obj_walk` | 31 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=14 y=14 w=60 h=85 |  |  |
| 14 | `heavy_obj_walk` | 32 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=13 y=10 w=60 h=91 |  |  |
| 15 | `heavy_obj_walk` | 33 | 1 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=12 y=7 w=62 h=93 |  |  |
| 16 | `heavy_obj_run` | 34 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\003.wav` | kind=0 x=8 y=7 w=63 h=90 |  |  |
| 17 | `heavy_obj_run` | 35 | 2 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=11 y=11 w=64 h=88 |  |  |
| 18 | `heavy_obj_run` | 36 | 2 | 3 | 0 | 0 | 0 | 0 |  | `data\004.wav` | kind=0 x=9 y=7 w=59 h=93 |  |  |
| 19 | `heavy_stop_run` | 41 | 15 | 7 | 999 | 2 | 0 | 0 |  | `data\009.wav` | kind=0 x=13 y=9 w=55 h=90 |  |  |
| 20 | `normal_weapon_atck` | 37 | 15 | 2 | 21 | 3 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 21 | `normal_weapon_atck` | 38 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=4 y=15 w=56 h=86 |  |  |
| 25 | `normal_weapon_atck` | 37 | 15 | 2 | 21 | 3 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 30 | `jump_weapon_atck` | 37 | 3 | 1 | 31 | 0 | 0 | 0 |  |  | kind=0 x=20 y=13 w=47 h=85 |  |  |
| 31 | `jump_weapon_atck` | 39 | 3 | 1 | 32 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 32 | `jump_weapon_atck` | 39 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 35 | `run_weapon_atck` | 37 | 15 | 3 | 36 | 3 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 36 | `run_weapon_atck` | 38 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=4 y=15 w=56 h=86 |  |  |
| 40 | `dash_weapon_atck` | 37 | 3 | 1 | 41 | 0 | 0 | 0 |  |  | kind=0 x=20 y=13 w=47 h=85 |  |  |
| 41 | `dash_weapon_atck` | 39 | 3 | 1 | 42 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 42 | `dash_weapon_atck` | 39 | 3 | 4 | 999 | 0 | 0 | 0 |  |  | kind=0 x=7 y=32 w=44 h=67; kind=0 x=29 y=12 w=26 h=38 |  |  |
| 45 | `light_weapon_thw` | 37 | 15 | 3 | 46 | 0 | 0 | 0 |  |  | kind=0 x=9 y=9 w=59 h=91 |  |  |
| 46 | `light_weapon_thw` | 38 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\008.wav` | kind=0 x=4 y=15 w=56 h=86 |  |  |
| 50 | `heavy_weapon_thw` | 37 | 15 | 3 | 51 | 0 | 0 | 0 |  |  | kind=0 x=11 y=4 w=59 h=97 |  |  |
| 51 | `heavy_weapon_thw` | 38 | 15 | 3 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=16 w=50 h=84 |  |  |
| 52 | `sky_lgt_wp_thw` | 37 | 15 | 3 | 53 | 0 | 0 | 0 |  |  | kind=0 x=20 y=8 w=44 h=88 |  |  |
| 53 | `sky_lgt_wp_thw` | 39 | 15 | 1 | 54 | 0 | -2 | 0 |  | `data\008.wav` | kind=0 x=11 y=14 w=46 h=80 |  |  |
| 54 | `sky_lgt_wp_thw` | 39 | 15 | 9 | 999 | 0 | 0 | 0 |  |  | kind=0 x=13 y=17 w=48 h=85 |  |  |
| 55 | `weapon_drink` | 46 | 17 | 3 | 56 | 0 | 0 | 0 |  | `data\042.wav` | kind=0 x=15 y=12 w=37 h=67 |  |  |
| 56 | `weapon_drink` | 47 | 17 | 3 | 57 | 0 | 0 | 0 |  |  | kind=0 x=16 y=20 w=36 h=59 |  |  |
| 57 | `weapon_drink` | 48 | 17 | 3 | 58 | 0 | 0 | 0 |  |  | kind=0 x=17 y=17 w=32 h=63 |  |  |
| 58 | `weapon_drink` | 47 | 17 | 3 | 55 | 0 | 0 | 0 |  |  | kind=0 x=16 y=13 w=36 h=65 |  |  |
| 60 | `punch` | 10 | 3 | 2 | 61 | 2 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=2 x=11 y=79 w=65 h=28 (vrest=1) |  |
| 61 | `punch` | 11 | 3 | 1 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=10 w=64 h=90 | kind=0 x=35 y=36 w=46 h=26 (bdefend=30, dvx=2, fall=25, injury=35, vrest=8) |  |
| 65 | `punch` | 12 | 3 | 2 | 66 | 2 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=2 x=8 y=78 w=65 h=31 (vrest=1) |  |
| 66 | `punch` | 13 | 3 | 1 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=10 w=64 h=90 | kind=0 x=32 y=37 w=48 h=31 (bdefend=30, dvx=2, fall=25, injury=35, vrest=8) |  |
| 70 | `super` | 15 | 3 | 1 | 71 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 71 | `super` | 16 | 3 | 1 | 72 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 72 | `super` | 17 | 3 | 1 | 73 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 73 | `super` | 18 | 3 | 1 | 74 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 74 | `super` | 19 | 3 | 1 | 75 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=0 x=34 y=18 w=41 h=58 (bdefend=60, dvx=7, dvy=9, fall=70, injury=60, vrest=10) |  |
| 75 | `super` | 29 | 3 | 4 | 76 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=0 x=43 y=47 w=34 h=25 (bdefend=60, dvx=7, dvy=9, fall=70, injury=60, vrest=10) |  |
| 76 | `super` | 10 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 79 | `singlong` | 25 | 3 | 9 | 0 | 0 | 0 | 0 |  |  | kind=0 x=6 y=16 w=48 h=75 |  |  |
| 80 | `jump_attack` | 76 | 3 | 3 | 81 | 0 | 0 | 0 |  |  | kind=0 x=8 y=13 w=54 h=76 |  |  |
| 81 | `jump_attack` | 50 | 3 | 20 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=13 w=56 h=73 | kind=0 x=37 y=56 w=65 h=32 (bdefend=60, dvx=13, dvy=-4, fall=70, injury=60, vrest=10) |  |
| 85 | `run_attack` | 51 | 3 | 4 | 300 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 86 | `singlong` | 21 | 3 | 1 | 87 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=0 x=34 y=2 w=41 h=97 (bdefend=60, dvx=7, dvy=-13, fall=70, injury=80, vrest=10) |  |
| 87 | `singlong` | 22 | 3 | 1 | 88 | 0 | -7 | 0 |  | `data\007.wav` | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 88 | `singlong` | 23 | 3 | 2 | 89 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 | kind=0 x=1 y=6 w=65 h=118 (bdefend=60, dvx=7, dvy=-8, fall=70, injury=60, vrest=10) |  |
| 89 | `singlong` | 24 | 3 | 2 | 79 | 0 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 90 | `dash_attack` | 76 | 3 | 3 | 81 | 0 | 0 | 0 |  |  | kind=0 x=8 y=13 w=54 h=76 |  |  |
| 91 | `dash_attack` | 50 | 3 | 20 | 999 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=12 y=13 w=56 h=73 | kind=0 x=37 y=56 w=65 h=32 (bdefend=60, dvx=13, dvy=-4, fall=70, injury=80, vrest=10) |  |
| 100 | `rowing` | 55 | 6 | 2 | 101 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 101 | `rowing` | 39 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 102 | `rowing` | 78 | 6 | 1 | 103 | 0 | 0 | 3 |  | `data\030.wav` | kind=0 x=8 y=9 w=52 h=94 |  |  |
| 103 | `rowing` | 49 | 6 | 10 | 104 | 10 | 0 | 3 |  |  |  |  | id 52 (type 0): data\julian.dat |
| 104 | `rowing` | 77 | 6 | 3 | 105 | 0 | 0 | 1 |  |  | kind=0 x=8 y=9 w=52 h=94 |  |  |
| 105 | `rowing` | 78 | 6 | 4 | 999 | 0 | 0 | 1 |  |  | kind=0 x=8 y=9 w=52 h=94 |  |  |
| 106 | `rowing` | 78 | 6 | 1 | 103 | 0 | 0 | 3 | 25 | `data\030.wav` | kind=0 x=8 y=9 w=52 h=94 |  |  |
| 108 | `rowing` | 54 | 6 | 3 | 109 | 0 | 0 | 0 |  | `data\017.wav` |  |  |  |
| 109 | `rowing` | 67 | 6 | 6 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 110 | `defend` | 42 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=9 w=52 h=94 |  |  |
| 111 | `defend` | 42 | 7 | 12 | 999 | 0 | 0 | 0 |  |  | kind=0 x=8 y=9 w=52 h=94 |  |  |
| 112 | `broken_defend` | 66 | 8 | 1 | 113 | 0 | 0 | 0 |  |  | kind=0 x=19 y=6 w=53 h=94 |  |  |
| 113 | `broken_defend` | 67 | 8 | 2 | 114 | 0 | 0 | 0 |  |  | kind=0 x=15 y=6 w=57 h=92 |  |  |
| 114 | `broken_defend` | 69 | 8 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=15 y=10 w=54 h=90 |  |  |
| 115 | `picking_light` | 14 | 15 | 4 | 999 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=19 y=22 w=52 h=75 |  |  |
| 116 | `picking_heavy` | 14 | 15 | 1 | 117 | 0 | 0 | 0 |  | `data\009.wav` | kind=0 x=19 y=23 w=49 h=75 |  |  |
| 117 | `picking_heavy` | 14 | 15 | 1 | 999 | 0 | 0 | 0 |  |  | kind=0 x=19 y=22 w=49 h=78 |  |  |
| 120 | `catching` | 44 | 9 | 2 | 121 | 0 | 0 | 0 |  | `data\015.wav` | kind=0 x=17 y=13 w=49 h=89 |  |  |
| 121 | `catching` | 43 | 9 | 0 | 0 | 0 | 0 | 0 |  |  | kind=0 x=11 y=11 w=54 h=84 |  |  |
| 122 | `catching` | 44 | 9 | 3 | 123 | 0 | 0 | 0 |  |  | kind=0 x=18 y=10 w=52 h=86 |  |  |
| 123 | `catching` | 45 | 9 | 3 | 121 | 0 | 0 | 0 |  | `data\014.wav` | kind=0 x=22 y=4 w=52 h=92 |  |  |
| 130 | `picked_caught` | 68 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 131 | `picked_caught` | 69 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 132 | `picked_caught` | 69 | 10 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=26 y=14 w=28 h=66 |  |  |
| 133 | `picked_caught` | 64 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 134 | `picked_caught` | 65 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 135 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 136 | `picked_caught` | 55 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 137 | `picked_caught` | 56 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 138 | `picked_caught` | 54 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 139 | `picked_caught` | 74 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 140 | `picked_caught` | 75 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 141 | `picked_caught` | 57 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 142 | `picked_caught` | 58 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 143 | `picked_caught` | 59 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 144 | `picked_caught` | 58 | 10 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 180 | `falling` | 64 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=23 y=45 w=22 h=21 | kind=4 x=17 y=39 w=35 h=31 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 181 | `falling` | 65 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=20 y=41 w=26 h=22 | kind=4 x=12 y=33 w=29 h=23 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=20 y=45 w=33 h=27 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 182 | `falling` | 54 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=37 y=38 w=27 h=24 | kind=4 x=33 y=37 w=39 h=30 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 183 | `falling` | 55 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=38 y=40 w=26 h=22 | kind=4 x=55 y=32 w=27 h=19 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=30 y=37 w=36 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 184 | `falling` | 56 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 185 | `falling` | 54 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 186 | `falling` | 74 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=31 y=47 w=25 h=24 | kind=4 x=19 y=37 w=42 h=36 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 187 | `falling` | 75 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=25 y=49 w=25 h=22 | kind=4 x=22 y=26 w=41 h=57 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 188 | `falling` | 57 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=42 y=38 w=26 h=23 | kind=4 x=34 y=29 w=35 h=32 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 189 | `falling` | 58 | 12 | 3 | 0 | 0 | 0 | 0 |  |  | kind=0 x=30 y=39 w=23 h=21 | kind=4 x=24 y=27 w=26 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20); kind=4 x=37 y=45 w=31 h=28 (bdefend=10, dvx=2, fall=70, injury=30, vrest=20) |  |
| 190 | `falling` | 59 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 191 | `falling` | 57 | 12 | 3 | 0 | 0 | 0 | 0 |  |  |  |  |  |
| 200 | `ice` | 8 | 15 | 2 | 201 | 0 | 0 | 0 |  |  | kind=0 x=12 y=15 w=61 h=84 |  |  |
| 201 | `ice` | 9 | 13 | 90 | 202 | 0 | 0 | 0 |  |  | kind=0 x=8 y=12 w=66 h=86 | kind=14 x=8 y=12 w=66 h=86 (vrest=1) |  |
| 202 | `ice` | 8 | 15 | 1 | 182 | -4 | -3 | 0 |  |  | kind=0 x=8 y=6 w=67 h=73 |  |  |
| 203 | `fire` | 61 | 18 | 1 | 204 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 204 | `fire` | 60 | 18 | 1 | 203 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 205 | `fire` | 61 | 18 | 1 | 206 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 206 | `fire` | 60 | 18 | 1 | 205 | 0 | 0 | 0 |  |  | kind=0 x=22 y=35 w=26 h=19 | kind=0 x=22 y=35 w=26 h=19 (bdefend=16, dvx=-6, dvy=-6, effect=20, fall=70, injury=30, vrest=10) |  |
| 207 | `tired` | 69 | 15 | 2 | 0 | 0 | 0 | 0 |  |  | kind=0 x=44 y=28 w=22 h=37; kind=0 x=28 y=47 w=28 h=35 |  |  |
| 210 | `jump` | 14 | 4 | 2 | 211 | 0 | 0 | 0 |  |  | kind=0 x=14 y=19 w=53 h=83 |  |  |
| 211 | `jump` | 14 | 4 | 2 | 212 | 0 | 0 | 0 |  | `data\017.wav` | kind=0 x=13 y=20 w=52 h=80 |  |  |
| 212 | `jump` | 25 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=22 y=5 w=37 h=84 |  |  |
| 213 | `jump` | 25 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=18 y=1 w=37 h=92 |  |  |
| 214 | `jump` | 25 | 4 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=19 y=5 w=38 h=88 |  |  |
| 215 | `crouch` | 14 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=15 y=22 w=50 h=76 |  |  |
| 218 | `stop_running` | 40 | 15 | 6 | 999 | 1 | 0 | 0 |  | `data\009.wav` | kind=0 x=16 y=8 w=47 h=92; kind=0 x=45 y=47 w=16 h=32 |  |  |
| 219 | `crouch2` | 14 | 15 | 2 | 999 | 0 | 0 | 0 |  | `data\012.wav` | kind=0 x=14 y=21 w=53 h=77 |  |  |
| 220 | `injured` | 66 | 11 | 2 | 221 | 0 | 0 | 0 |  |  | kind=0 x=27 y=16 w=38 h=85 |  |  |
| 221 | `injured` | 67 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=16 w=47 h=85; kind=0 x=22 y=37 w=26 h=42 |  |  |
| 222 | `injured` | 69 | 11 | 2 | 223 | 0 | 0 | 0 |  |  | kind=0 x=13 y=14 w=45 h=83 |  |  |
| 223 | `injured` | 68 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=14 y=15 w=50 h=78 |  |  |
| 224 | `injured` | 69 | 11 | 2 | 225 | 0 | 0 | 0 |  |  | kind=0 x=13 y=14 w=45 h=83 |  |  |
| 225 | `injured` | 68 | 11 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=14 y=15 w=50 h=78 |  |  |
| 226 | `injured` | 69 | 16 | 6 | 227 | 0 | 0 | 0 |  |  | kind=0 x=26 y=24 w=41 h=74 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 227 | `injured` | 67 | 16 | 6 | 228 | 0 | 0 | 0 |  |  | kind=0 x=23 y=22 w=46 h=77 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 228 | `injured` | 66 | 16 | 6 | 229 | 0 | 0 | 0 |  |  | kind=0 x=23 y=11 w=43 h=86 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 229 | `injured` | 67 | 16 | 6 | 999 | 0 | 0 | 0 |  |  | kind=0 x=18 y=12 w=49 h=87 | kind=6 x=6 y=12 w=85 h=68 (vrest=1) |  |
| 230 | `lying` | 56 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 231 | `lying` | 59 | 14 | 30 | 219 | 0 | 0 | 0 |  |  |  |  |  |
| 232 | `throw_lying_man` | 37 | 9 | 3 | 233 | 0 | 0 | 0 |  |  | kind=0 x=29 y=25 w=41 h=74 |  |  |
| 233 | `throw_lying_man` | 38 | 9 | 1 | 234 | 0 | 0 | 0 |  | `data\007.wav` | kind=0 x=7 y=15 w=53 h=84; kind=0 x=19 y=56 w=30 h=24 |  |  |
| 234 | `throw_lying_man` | 38 | 9 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=9 y=15 w=46 h=83 |  |  |
| 235 | `singlong` | 20 | 3 | 1 | 86 | 10 | 0 | 0 |  |  | kind=0 x=10 y=10 w=64 h=90 |  |  |
| 240 | `shadow` | 78 | 15 | 25 | 241 | 0 | 0 | 0 |  |  |  |  |  |
| 241 | `shadow` | 80 | 15 | 2 | 242 | 0 | 0 | 0 |  |  |  |  |  |
| 242 | `shadow` | 79 | 15 | 2 | 243 | 0 | 0 | 0 |  |  |  |  |  |
| 243 | `shadow` | 80 | 15 | 2 | 244 | 0 | 0 | 0 |  |  |  |  |  |
| 244 | `shadow` | 78 | 15 | 2 | 245 | 0 | 0 | 0 |  |  |  |  |  |
| 245 | `shadow` | 81 | 15 | 2 | 246 | 0 | 0 | 0 |  |  |  |  |  |
| 246 | `shadow` | 82 | 15 | 2 | 247 | 0 | 0 | 0 |  |  |  |  |  |
| 247 | `shadow` | 81 | 15 | 2 | 248 | 0 | 0 | 0 |  |  |  |  |  |
| 248 | `shadow` | 78 | 15 | 2 | 249 | 0 | 0 | 0 |  |  |  |  |  |
| 249 | `shadow` | 70 | 15 | 2 | 250 | 0 | 0 | 0 |  |  |  |  |  |
| 250 | `shadow` | 71 | 15 | 2 | 251 | 0 | 0 | 0 |  |  |  |  |  |
| 251 | `shadow` | 72 | 15 | 2 | 1000 | 0 | 0 | 0 |  |  |  |  |  |
| 260 | `ball` | 84 | 3 | 3 | 261 | 0 | 0 | 0 | 10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 261 | `ball` | 85 | 3 | 0 | 262 | 0 | 0 | 0 |  | `data\091.wav` | kind=0 x=20 y=10 w=43 h=89 |  | id 228 (type 3): data\julian_ball.dat |
| 262 | `ball` | 86 | 3 | 1 | 263 | 0 | 0 | 0 | -10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 263 | `ball` | 87 | 3 | 1 | 264 | 0 | 0 | 0 | 10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 264 | `ball` | 88 | 3 | 0 | 265 | 0 | 0 | 0 |  | `data\091.wav` | kind=0 x=20 y=10 w=43 h=89 |  | id 228 (type 3): data\julian_ball.dat |
| 265 | `ball` | 89 | 3 | 1 | 266 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 266 | `ball` | 90 | 3 | 1 | 267 | 0 | 0 | 0 | -10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 267 | `ball` | 91 | 3 | 1 | 268 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 268 | `ball` | 92 | 3 | 0 | 269 | 0 | 0 | 0 |  | `data\091.wav` | kind=0 x=20 y=10 w=43 h=89 |  | id 228 (type 3): data\julian_ball.dat |
| 269 | `ball` | 93 | 3 | 1 | 270 | 0 | 0 | 0 | -10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 270 | `ball` | 103 | 3 | 1 | 271 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 271 | `ball` | 102 | 3 | 0 | 272 | 0 | 0 | 0 |  | `data\091.wav` | kind=0 x=20 y=10 w=43 h=89 |  | id 228 (type 3): data\julian_ball.dat |
| 272 | `ball` | 101 | 3 | 1 | 273 | 0 | 0 | 0 | -10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 273 | `ball` | 100 | 3 | 1 | 274 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 274 | `ball` | 99 | 3 | 0 | 275 | 0 | 0 | 0 |  | `data\091.wav` | kind=0 x=20 y=10 w=43 h=89 |  | id 228 (type 3): data\julian_ball.dat |
| 275 | `ball` | 98 | 3 | 1 | 276 | 0 | 0 | 0 | -10 |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 276 | `ball_end` | 97 | 3 | 3 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 280 | `big_ball` | 94 | 3 | 3 | 281 | 0 | 0 | 0 | 125 | `data\018.wav` | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 281 | `big_ball` | 95 | 3 | 0 | 282 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 282 | `big_ball` | 96 | 3 | 0 | 283 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 283 | `big_ball` | 95 | 3 | 0 | 284 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 284 | `big_ball` | 96 | 3 | 0 | 285 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 285 | `big_ball` | 95 | 3 | 0 | 286 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 286 | `big_ball` | 96 | 3 | 0 | 287 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 287 | `big_ball` | 95 | 3 | 0 | 288 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 288 | `big_ball` | 96 | 3 | 0 | 289 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 289 | `big_ball` | 104 | 3 | 0 | 290 | 0 | 0 | 0 |  | `data\019.wav` | kind=0 x=20 y=10 w=43 h=89 |  | id 229 (type 3): data\julian_ball2.dat |
| 290 | `big_ball` | 105 | 3 | 0 | 291 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 291 | `big_ball` | 106 | 3 | 5 | 999 | 0 | 0 | 0 |  |  | kind=0 x=20 y=10 w=43 h=89 |  |  |
| 300 | `run_attack` | 52 | 3 | 0 | 301 | 15 | 0 | 0 |  | `data\031.wav` | kind=0 x=7 y=19 w=71 h=82 | kind=0 x=-1 y=38 w=108 h=40 (bdefend=60, dvx=25, dvy=-9, fall=70, injury=90, vrest=8) |  |
| 301 | `run_attack` | 53 | 3 | 0 | 302 | 15 | 0 | 0 |  |  | kind=0 x=7 y=19 w=71 h=82 | kind=0 x=-1 y=38 w=108 h=40 (bdefend=60, dvx=25, dvy=-9, fall=70, injury=90, vrest=8) |  |
| 302 | `run_attack` | 52 | 3 | 0 | 303 | 0 | 0 | 0 |  |  | kind=0 x=7 y=19 w=71 h=82 | kind=0 x=-1 y=38 w=108 h=40 (bdefend=60, dvx=25, dvy=-9, fall=70, injury=90, vrest=8) |  |
| 303 | `run_attack` | 53 | 3 | 0 | 304 | 0 | 0 | 0 |  |  | kind=0 x=7 y=19 w=71 h=82 | kind=0 x=-1 y=38 w=108 h=40 (bdefend=60, dvx=25, dvy=-9, fall=70, injury=90, vrest=8) |  |
| 304 | `run_attack` | 52 | 3 | 0 | 305 | 0 | 0 | 0 |  |  | kind=0 x=7 y=19 w=71 h=82 | kind=0 x=-1 y=38 w=108 h=40 (bdefend=60, dvx=25, dvy=-9, fall=70, injury=90, vrest=8) |  |
| 305 | `run_attack` | 53 | 3 | 0 | 306 | 0 | 0 | 0 |  |  | kind=0 x=7 y=19 w=71 h=82 | kind=0 x=-1 y=38 w=108 h=40 (bdefend=60, dvx=25, dvy=-9, fall=70, injury=90, vrest=8) |  |
| 306 | `run_attack` | 62 | 3 | 4 | 307 | 0 | 0 | 0 |  |  | kind=0 x=11 y=24 w=73 h=76 |  |  |
| 307 | `run_attack` | 63 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 310 | `explosion` | 107 | 3 | 1 | 311 | 0 | 0 | 0 | 100 | `data\018.wav` | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 311 | `explosion` | 108 | 3 | 0 | 312 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 312 | `explosion` | 107 | 3 | 1 | 313 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 313 | `explosion` | 108 | 3 | 0 | 314 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 314 | `explosion` | 109 | 3 | 1 | 315 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 315 | `explosion` | 108 | 3 | 0 | 316 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 316 | `explosion` | 109 | 3 | 1 | 317 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 317 | `explosion` | 108 | 3 | 0 | 318 | 0 | 0 | 0 |  | `data\018.wav` | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 318 | `explosion` | 110 | 3 | 8 | 319 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  | id 229 (type 3): data\julian_ball2.dat |
| 319 | `explosion` | 110 | 3 | 7 | 320 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 320 | `explosion` | 110 | 3 | 7 | 321 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 321 | `explosion` | 111 | 3 | 2 | 322 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 322 | `explosion` | 112 | 3 | 2 | 999 | 0 | 0 | 0 |  |  | kind=0 x=15 y=27 w=70 h=71 |  |  |
| 399 | `dummy` | 0 | 0 | 1 | 0 | 0 | 0 | 0 |  |  | kind=0 x=21 y=18 w=43 h=62 |  |  |

</details>

---
