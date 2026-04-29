# Manual LF2 — Catálogo completo de personagens (extraído do seu pacote)

Este manual foi gerado automaticamente a partir dos arquivos do seu `LittleFighter` (data + sprite).

## Sumário
- [Deep](#personagem-1-deep)
- [John](#personagem-2-john)
- [Henry](#personagem-4-henry)
- [Rudolf](#personagem-5-rudolf)
- [Louis](#personagem-6-louis)
- [Firen](#personagem-7-firen)
- [Freeze](#personagem-8-freeze)
- [Dennis](#personagem-9-dennis)
- [Woody](#personagem-10-woody)
- [Davis](#personagem-11-davis)
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

> Observação: os `.dat` deste pacote estão criptografados/encodados (formato clássico do LF2).

---

## Personagem 1: Deep
<a id="personagem-1-deep"></a>

- **Arquivo (.dat):** `deep.dat`
- **Head (seleção):** `sprite\sys\deep_f.bmp`
- **Small (HUD):** `sprite\sys\deep_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\deep_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\deep_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\deep_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Da` | 260 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 290 |
| 0 | `standing` | `hit_Uj` | 266 |
| 1 | `standing` | `hit_Da` | 260 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 290 |
| 1 | `standing` | `hit_Uj` | 266 |
| 2 | `standing` | `hit_Da` | 260 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 290 |
| 2 | `standing` | `hit_Uj` | 266 |
| 3 | `standing` | `hit_Da` | 260 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 290 |
| 3 | `standing` | `hit_Uj` | 266 |
| 5 | `walking` | `hit_Da` | 260 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 290 |
| 5 | `walking` | `hit_Uj` | 266 |
| 6 | `walking` | `hit_Da` | 260 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 290 |
| 6 | `walking` | `hit_Uj` | 266 |
| 7 | `walking` | `hit_Da` | 260 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 290 |
| 7 | `walking` | `hit_Uj` | 266 |
| 8 | `walking` | `hit_Da` | 260 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 290 |
| 8 | `walking` | `hit_Uj` | 266 |
| 102 | `rowing` | `hit_Fj` | 290 |
| 102 | `rowing` | `hit_Uj` | 266 |
| 103 | `rowing` | `hit_Fj` | 290 |
| 103 | `rowing` | `hit_Uj` | 266 |
| 104 | `rowing` | `hit_Fj` | 290 |
| 104 | `rowing` | `hit_Uj` | 266 |
| 105 | `rowing` | `hit_Fj` | 290 |
| 105 | `rowing` | `hit_Uj` | 266 |
| 110 | `defend` | `hit_Da` | 260 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 290 |
| 110 | `defend` | `hit_Uj` | 266 |
| 121 | `catching` | `hit_Fj` | 310 |
| 239 | `blast` | `hit_a` | 240 |
| 244 | `blast2` | `hit_a` | 245 |
| 265 | `jump_sword` | `hit_Uj` | 266 |
| 265 | `jump_sword` | `hit_j` | 266 |
| 267 | `jump_sword` | `hit_a` | 268 |
| 275 | `jump_sword` | `hit_j` | 266 |
| 276 | `jump_sword` | `hit_a` | 277 |
| 276 | `jump_sword` | `hit_j` | 266 |
| 282 | `jump_sword2` | `hit_a` | 285 |
| 282 | `jump_sword2` | `hit_j` | 266 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 4 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 4 | 3 |  |
| 3 | `standing` | 3 | 0 | 4 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 3 | 61 |  |
| 61 | `punch` | 18 | 3 | 1 | 62 |  |
| 62 | `punch` | 11 | 3 | 1 | 999 |  |
| 65 | `punch` | 12 | 3 | 3 | 66 |  |
| 66 | `punch` | 19 | 3 | 1 | 67 |  |
| 67 | `punch` | 13 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 4 | 71 |  |
| 71 | `super_punch` | 29 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 38 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 39 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 1 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 11 | 999 |  |
| 85 | `run_attack` | 102 | 3 | 4 | 86 |  |
| 86 | `run_attack` | 103 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 104 | 3 | 3 | 88 |  |
| 88 | `run_attack` | 105 | 3 | 3 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 1 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 2 | 92 |  |
| 92 | `dash_attack` | 108 | 15 | 0 | 0 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |

---

## Personagem 2: John
<a id="personagem-2-john"></a>

- **Arquivo (.dat):** `john.dat`
- **Head (seleção):** `sprite\sys\john_f.bmp`
- **Small (HUD):** `sprite\sys\john_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\john_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\john_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\john_3.bmp` | 79×79 | 10×7 |
| 210-213 | `sprite\sys\john_2.bmp` | 109×109 | 4×1 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Dj` | 250 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 270 |
| 0 | `standing` | `hit_Ua` | 300 |
| 0 | `standing` | `hit_Uj` | 260 |
| 1 | `standing` | `hit_Dj` | 250 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 270 |
| 1 | `standing` | `hit_Ua` | 300 |
| 1 | `standing` | `hit_Uj` | 260 |
| 2 | `standing` | `hit_Dj` | 250 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 270 |
| 2 | `standing` | `hit_Ua` | 300 |
| 2 | `standing` | `hit_Uj` | 260 |
| 3 | `standing` | `hit_Dj` | 250 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 270 |
| 3 | `standing` | `hit_Ua` | 300 |
| 3 | `standing` | `hit_Uj` | 260 |
| 5 | `walking` | `hit_Dj` | 250 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 270 |
| 5 | `walking` | `hit_Ua` | 300 |
| 5 | `walking` | `hit_Uj` | 260 |
| 6 | `walking` | `hit_Dj` | 250 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 270 |
| 6 | `walking` | `hit_Ua` | 300 |
| 6 | `walking` | `hit_Uj` | 260 |
| 7 | `walking` | `hit_Dj` | 250 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 270 |
| 7 | `walking` | `hit_Ua` | 300 |
| 7 | `walking` | `hit_Uj` | 260 |
| 8 | `walking` | `hit_Dj` | 250 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 270 |
| 8 | `walking` | `hit_Ua` | 300 |
| 8 | `walking` | `hit_Uj` | 260 |
| 73 | `super_punch` | `hit_j` | 290 |
| 110 | `defend` | `hit_Dj` | 250 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 270 |
| 110 | `defend` | `hit_Ua` | 300 |
| 110 | `defend` | `hit_Uj` | 260 |
| 291 | `jumphit` | `hit_a` | 292 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 6 | 1 |  |
| 1 | `standing` | 1 | 0 | 5 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 6 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 136 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 137 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 138 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 137 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 4 | 71 |  |
| 71 | `super_punch` | 29 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 38 | 3 | 1 | 73 |  |
| 73 | `super_punch` | 39 | 3 | 5 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 4 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 2 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 2 | 83 |  |
| 83 | `jump_attack` | 17 | 3 | 5 | 999 |  |
| 85 | `run_attack` | 102 | 3 | 4 | 86 |  |
| 86 | `run_attack` | 103 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 104 | 3 | 3 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 3 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 3 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |
| 201 | `ice` | 9 | 13 | 90 | 202 |  |
| 202 | `ice` | 8 | 15 | 1 | 182 |  |

---

## Personagem 4: Henry
<a id="personagem-4-henry"></a>

- **Arquivo (.dat):** `henry.dat`
- **Head (seleção):** `sprite\sys\henry_f.bmp`
- **Small (HUD):** `sprite\sys\henry_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\henry_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\henry_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\henry_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 270 |
| 0 | `standing` | `hit_Uj` | 250 |
| 0 | `standing` | `hit_ja` | 280 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 270 |
| 1 | `standing` | `hit_Uj` | 250 |
| 1 | `standing` | `hit_ja` | 280 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 270 |
| 2 | `standing` | `hit_Uj` | 250 |
| 2 | `standing` | `hit_ja` | 280 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 270 |
| 3 | `standing` | `hit_Uj` | 250 |
| 3 | `standing` | `hit_ja` | 280 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 270 |
| 5 | `walking` | `hit_Uj` | 250 |
| 5 | `walking` | `hit_ja` | 280 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 270 |
| 6 | `walking` | `hit_Uj` | 250 |
| 6 | `walking` | `hit_ja` | 280 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 270 |
| 7 | `walking` | `hit_Uj` | 250 |
| 7 | `walking` | `hit_ja` | 280 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 270 |
| 8 | `walking` | `hit_Uj` | 250 |
| 8 | `walking` | `hit_ja` | 280 |
| 84 | `jump_attack` | `hit_a` | 80 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 270 |
| 110 | `defend` | `hit_Uj` | 250 |
| 110 | `defend` | `hit_ja` | 280 |
| 214 | `dash` | `hit_a` | 81 |
| 217 | `dash` | `hit_a` | 81 |
| 234 | `throw_lying_man` | `hit_Fa` | 245 |
| 287 | `5_arrow` | `hit_a` | 281 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 4 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 4 | 3 |  |
| 3 | `standing` | 3 | 0 | 4 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 24 | `rowing` | 69 | 6 | 2 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 49 | `throw_lying_man` | 28 | 15 | 3 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 2 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 1 | 61 |  |
| 61 | `punch` | 11 | 3 | 1 | 62 |  |
| 62 | `punch` | 12 | 3 | 1 | 63 |  |
| 63 | `punch` | 13 | 3 | 1 | 64 |  |
| 64 | `punch` | 14 | 3 | 1 | 66 | 201 |
| 65 | `punch` | 10 | 3 | 1 | 61 |  |
| 66 | `punch` | 15 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 29 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 38 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 39 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 8 | 3 | 1 | 81 |  |
| 81 | `jump_attack` | 9 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 49 | 3 | 1 | 83 |  |
| 83 | `jump_attack` | 58 | 3 | 1 | 84 |  |
| 84 | `jump_attack` | 59 | 3 | 11 | 999 | 201 |
| 85 | `run_attack` | 37 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 29 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 38 | 3 | 3 | 88 |  |
| 88 | `run_attack` | 39 | 3 | 2 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 1 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 2 | 92 |  |
| 92 | `dash_attack` | 108 | 15 | 0 | 0 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 16 | 6 | 2 | 103 |  |
| 103 | `rowing` | 17 | 6 | 2 | 104 |  |
| 104 | `rowing` | 18 | 6 | 2 | 105 |  |
| 105 | `rowing` | 19 | 6 | 2 | 106 |  |
| 106 | `rowing` | 67 | 6 | 2 | 107 |  |
| 107 | `rowing` | 68 | 6 | 2 | 24 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |

---

## Personagem 5: Rudolf
<a id="personagem-5-rudolf"></a>

- **Arquivo (.dat):** `rudolf.dat`
- **Head (seleção):** `sprite\sys\rudolf_f.bmp`
- **Small (HUD):** `sprite\sys\rudolf_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\rudolf_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\rudolf_1.bmp` | 79×79 | 10×7 |
| 140-149 | `sprite\sys\rudolf_2.bmp` | 149×87 | 5×2 |
| 150-155 | `sprite\sys\rudolf_3.bmp` | 94×64 | 6×1 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Dj` | 260 |
| 0 | `standing` | `hit_Fa` | 285 |
| 0 | `standing` | `hit_Fj` | 273 |
| 0 | `standing` | `hit_Uj` | 250 |
| 0 | `standing` | `hit_ja` | 295 |
| 1 | `standing` | `hit_Dj` | 260 |
| 1 | `standing` | `hit_Fa` | 285 |
| 1 | `standing` | `hit_Fj` | 273 |
| 1 | `standing` | `hit_Uj` | 250 |
| 1 | `standing` | `hit_ja` | 295 |
| 2 | `standing` | `hit_Dj` | 260 |
| 2 | `standing` | `hit_Fa` | 285 |
| 2 | `standing` | `hit_Fj` | 273 |
| 2 | `standing` | `hit_Uj` | 250 |
| 2 | `standing` | `hit_ja` | 295 |
| 3 | `standing` | `hit_Dj` | 260 |
| 3 | `standing` | `hit_Fa` | 285 |
| 3 | `standing` | `hit_Fj` | 273 |
| 3 | `standing` | `hit_Uj` | 250 |
| 3 | `standing` | `hit_ja` | 295 |
| 5 | `walking` | `hit_Dj` | 260 |
| 5 | `walking` | `hit_Fa` | 285 |
| 5 | `walking` | `hit_Fj` | 273 |
| 5 | `walking` | `hit_Uj` | 250 |
| 5 | `walking` | `hit_ja` | 295 |
| 6 | `walking` | `hit_Dj` | 260 |
| 6 | `walking` | `hit_Fa` | 285 |
| 6 | `walking` | `hit_Fj` | 273 |
| 6 | `walking` | `hit_Uj` | 250 |
| 6 | `walking` | `hit_ja` | 295 |
| 7 | `walking` | `hit_Dj` | 260 |
| 7 | `walking` | `hit_Fa` | 285 |
| 7 | `walking` | `hit_Fj` | 273 |
| 7 | `walking` | `hit_Uj` | 250 |
| 7 | `walking` | `hit_ja` | 295 |
| 8 | `walking` | `hit_Dj` | 260 |
| 8 | `walking` | `hit_Fa` | 285 |
| 8 | `walking` | `hit_Fj` | 273 |
| 8 | `walking` | `hit_Uj` | 250 |
| 8 | `walking` | `hit_ja` | 295 |
| 63 | `punch` | `hit_a` | 64 |
| 67 | `punch` | `hit_a` | 68 |
| 102 | `rowing` | `hit_Fj` | 274 |
| 103 | `rowing` | `hit_Fj` | 274 |
| 104 | `rowing` | `hit_Fj` | 274 |
| 105 | `rowing` | `hit_Fj` | 274 |
| 106 | `rowing` | `hit_Fj` | 274 |
| 107 | `rowing` | `hit_Fj` | 274 |
| 108 | `rowing` | `hit_Fj` | 274 |
| 109 | `rowing` | `hit_Fj` | 274 |
| 110 | `defend` | `hit_Dj` | 260 |
| 110 | `defend` | `hit_Fa` | 285 |
| 110 | `defend` | `hit_Fj` | 273 |
| 110 | `defend` | `hit_Uj` | 250 |
| 110 | `defend` | `hit_ja` | 295 |
| 121 | `catching` | `hit_ja` | 235 |
| 289 | `punch` | `hit_a` | 285 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 4 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 4 | 3 |  |
| 3 | `standing` | 3 | 0 | 4 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 71 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 72 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 73 | 0 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 80 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 81 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 82 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 83 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 105 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 106 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 107 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 106 | 17 | 3 | 55 |  |
| 59 | `punch` | 13 | 3 | 4 | 999 |  |
| 60 | `punch` | 10 | 3 | 3 | 61 |  |
| 61 | `punch` | 11 | 3 | 2 | 62 |  |
| 62 | `punch` | 12 | 3 | 1 | 63 | 202 |
| 63 | `punch` | 13 | 3 | 4 | 999 |  |
| 64 | `punch` | 14 | 3 | 2 | 66 |  |
| 65 | `punch` | 10 | 3 | 3 | 61 |  |
| 66 | `punch` | 15 | 3 | 3 | 67 | 202 |
| 67 | `punch` | 13 | 3 | 1 | 999 |  |
| 68 | `punch` | 14 | 3 | 2 | 69 |  |
| 69 | `punch` | 12 | 3 | 2 | 59 | 202 |
| 70 | `super_punch` | 132 | 3 | 1 | 71 |  |
| 71 | `super_punch` | 133 | 3 | 2 | 72 |  |
| 72 | `super_punch` | 134 | 3 | 1 | 73 |  |
| 73 | `super_punch` | 135 | 3 | 1 | 74 |  |
| 74 | `super_punch` | 136 | 3 | 2 | 75 |  |
| 75 | `super_punch` | 137 | 3 | 1 | 76 |  |
| 76 | `super_punch` | 138 | 3 | 1 | 77 |  |
| 77 | `super_punch` | 139 | 3 | 2 | 78 |  |
| 78 | `super_punch` | 129 | 3 | 2 | 79 |  |
| 79 | `super_punch` | 119 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 67 | 3 | 1 | 81 |  |
| 81 | `jump_attack` | 68 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 69 | 3 | 1 | 83 |  |
| 83 | `jump_attack` | 29 | 3 | 1 | 84 |  |
| 84 | `jump_attack` | 39 | 3 | 1 | 118 |  |
| 85 | `run_attack` | 140 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 141 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 142 | 3 | 1 | 88 |  |
| 88 | `run_attack` | 143 | 3 | 1 | 89 |  |
| 89 | `run_attack` | 144 | 3 | 6 | 78 |  |
| 90 | `dash_attack` | 145 | 3 | 1 | 91 |  |
| 91 | `dash_attack` | 146 | 3 | 1 | 92 |  |
| 92 | `dash_attack` | 147 | 3 | 1 | 93 |  |
| 93 | `dash_attack` | 148 | 3 | 1 | 94 |  |
| 94 | `dash_attack` | 149 | 3 | 6 | 0 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 16 | 6 | 1 | 103 |  |
| 103 | `rowing` | 17 | 6 | 1 | 104 |  |
| 104 | `rowing` | 37 | 6 | 4 | 105 |  |
| 105 | `rowing` | 37 | 6 | 1 | 106 |  |
| 106 | `rowing` | 18 | 6 | 1 | 219 |  |
| 107 | `rowing` | 19 | 6 | 1 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 118 | `jump_attack` | 49 | 3 | 1 | 119 |  |
| 119 | `jump_attack` | 59 | 3 | 12 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |

---

## Personagem 6: Louis
<a id="personagem-6-louis"></a>

- **Arquivo (.dat):** `louis.dat`
- **Head (seleção):** `sprite\sys\louis_f.bmp`
- **Small (HUD):** `sprite\sys\louis_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\louis_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\louis_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\louis_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 247 |
| 0 | `standing` | `hit_Uj` | 275 |
| 0 | `standing` | `hit_ja` | 300 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 247 |
| 1 | `standing` | `hit_Uj` | 275 |
| 1 | `standing` | `hit_ja` | 300 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 247 |
| 2 | `standing` | `hit_Uj` | 275 |
| 2 | `standing` | `hit_ja` | 300 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 247 |
| 3 | `standing` | `hit_Uj` | 275 |
| 3 | `standing` | `hit_ja` | 300 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 247 |
| 5 | `walking` | `hit_Uj` | 275 |
| 5 | `walking` | `hit_ja` | 300 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 247 |
| 6 | `walking` | `hit_Uj` | 275 |
| 6 | `walking` | `hit_ja` | 300 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 247 |
| 7 | `walking` | `hit_Uj` | 275 |
| 7 | `walking` | `hit_ja` | 300 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 247 |
| 8 | `walking` | `hit_Uj` | 275 |
| 8 | `walking` | `hit_ja` | 300 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 247 |
| 110 | `defend` | `hit_Uj` | 275 |
| 110 | `defend` | `hit_ja` | 300 |
| 121 | `catching` | `hit_j` | -260 |
| 266 | `c-throw` | `hit_j` | 261 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 9 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 4 | 3 |  |
| 3 | `standing` | 3 | 0 | 5 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 137 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 138 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 139 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 138 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 4 | 61 |  |
| 61 | `punch` | 11 | 3 | 2 | 999 |  |
| 65 | `punch` | 12 | 3 | 4 | 66 |  |
| 66 | `punch` | 13 | 3 | 2 | 999 |  |
| 70 | `super_punch` | 14 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 15 | 3 | 2 | 72 |  |
| 72 | `super_punch` | 16 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 17 | 3 | 1 | 74 |  |
| 74 | `super_punch` | 18 | 3 | 2 | 999 |  |
| 75 | `run_attack` | 105 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 2 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 2 | 83 |  |
| 83 | `jump_attack` | 17 | 3 | 1 | 84 |  |
| 84 | `jump_attack` | 18 | 3 | 2 | 999 |  |
| 85 | `run_attack` | 100 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 101 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 102 | 3 | 4 | 88 |  |
| 88 | `run_attack` | 103 | 3 | 2 | 89 |  |
| 89 | `run_attack` | 104 | 3 | 2 | 75 |  |
| 90 | `dash_attack` | 132 | 15 | 1 | 91 |  |
| 91 | `dash_attack` | 132 | 15 | 2 | 92 |  |
| 92 | `dash_attack` | 133 | 15 | 2 | 93 |  |
| 93 | `dash_attack` | 134 | 100 | 90 | 216 |  |
| 94 | `dash_attack` | 135 | 15 | 2 | 95 |  |
| 95 | `dash_attack` | 136 | 15 | 2 | 999 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |

---

## Personagem 7: Firen
<a id="personagem-7-firen"></a>

- **Arquivo (.dat):** `firen.dat`
- **Head (seleção):** `sprite\sys\firen_f.bmp`
- **Small (HUD):** `sprite\sys\firen_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\firen_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\firen_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\firen_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Dj` | 267 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 255 |
| 0 | `standing` | `hit_Uj` | 285 |
| 1 | `standing` | `hit_Dj` | 267 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 255 |
| 1 | `standing` | `hit_Uj` | 285 |
| 2 | `standing` | `hit_Dj` | 267 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 255 |
| 2 | `standing` | `hit_Uj` | 285 |
| 3 | `standing` | `hit_Dj` | 267 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 255 |
| 3 | `standing` | `hit_Uj` | 285 |
| 5 | `walking` | `hit_Dj` | 267 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 255 |
| 5 | `walking` | `hit_Uj` | 285 |
| 6 | `walking` | `hit_Dj` | 267 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 255 |
| 6 | `walking` | `hit_Uj` | 285 |
| 7 | `walking` | `hit_Dj` | 267 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 255 |
| 7 | `walking` | `hit_Uj` | 285 |
| 8 | `walking` | `hit_Dj` | 267 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 255 |
| 8 | `walking` | `hit_Uj` | 285 |
| 110 | `defend` | `hit_Dj` | 267 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 255 |
| 110 | `defend` | `hit_Uj` | 285 |
| 111 | `defend` | `hit_Dj` | 267 |
| 111 | `defend` | `hit_Fa` | 235 |
| 111 | `defend` | `hit_Fj` | 255 |
| 111 | `defend` | `hit_Uj` | 285 |
| 240 | `ball1` | `hit_a` | 241 |
| 246 | `ball2` | `hit_a` | 247 |
| 257 | `burn_run` | `hit_d` | 218 |
| 257 | `burn_run` | `hit_j` | 218 |
| 258 | `burn_run` | `hit_d` | 218 |
| 258 | `burn_run` | `hit_j` | 218 |
| 259 | `burn_run` | `hit_d` | 218 |
| 259 | `burn_run` | `hit_j` | 218 |
| 260 | `burn_run` | `hit_d` | 218 |
| 260 | `burn_run` | `hit_j` | 218 |
| 261 | `burn_run` | `hit_d` | 218 |
| 261 | `burn_run` | `hit_j` | 218 |
| 272 | `flame` | `hit_d` | 283 |
| 272 | `flame` | `hit_j` | 283 |
| 273 | `flame` | `hit_d` | 283 |
| 273 | `flame` | `hit_j` | 283 |
| 274 | `flame` | `hit_d` | 283 |
| 274 | `flame` | `hit_j` | 283 |
| 275 | `flame` | `hit_d` | 283 |
| 275 | `flame` | `hit_j` | 283 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 4 | 1 |  |
| 1 | `standing` | 1 | 0 | 6 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 5 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 17 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 18 | 3 | 2 | 72 |  |
| 72 | `super_punch` | 19 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 29 | 3 | 2 | 74 |  |
| 74 | `super_punch` | 39 | 3 | 2 | 75 |  |
| 75 | `super_punch` | 49 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 3 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 5 | 999 |  |
| 85 | `run_attack` | 102 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 103 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 104 | 3 | 2 | 88 |  |
| 88 | `run_attack` | 105 | 3 | 4 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 3 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 1 | 92 |  |
| 92 | `dash_attack` | 108 | 15 | 3 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |

---

## Personagem 8: Freeze
<a id="personagem-8-freeze"></a>

- **Arquivo (.dat):** `freeze.dat`
- **Head (seleção):** `sprite\sys\freeze_f.bmp`
- **Small (HUD):** `sprite\sys\freeze_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\freeze_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\freeze_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\freeze_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Dj` | 270 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 245 |
| 0 | `standing` | `hit_Uj` | 260 |
| 1 | `standing` | `hit_Dj` | 270 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 245 |
| 1 | `standing` | `hit_Uj` | 260 |
| 2 | `standing` | `hit_Dj` | 270 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 245 |
| 2 | `standing` | `hit_Uj` | 260 |
| 3 | `standing` | `hit_Dj` | 270 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 245 |
| 3 | `standing` | `hit_Uj` | 260 |
| 5 | `walking` | `hit_Dj` | 270 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 245 |
| 5 | `walking` | `hit_Uj` | 260 |
| 6 | `walking` | `hit_Dj` | 270 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 245 |
| 6 | `walking` | `hit_Uj` | 260 |
| 7 | `walking` | `hit_Dj` | 270 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 245 |
| 7 | `walking` | `hit_Uj` | 260 |
| 8 | `walking` | `hit_Dj` | 270 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 245 |
| 8 | `walking` | `hit_Uj` | 260 |
| 110 | `defend` | `hit_Dj` | 270 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 245 |
| 110 | `defend` | `hit_Uj` | 260 |
| 111 | `defend` | `hit_Dj` | 270 |
| 111 | `defend` | `hit_Fa` | 235 |
| 111 | `defend` | `hit_Fj` | 245 |
| 111 | `defend` | `hit_Uj` | 260 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 16 | 1 |  |
| 1 | `standing` | 1 | 0 | 7 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 7 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 2 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 17 | 3 | 1 | 71 |  |
| 71 | `super_punch` | 18 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 19 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 29 | 3 | 1 | 74 |  |
| 74 | `super_punch` | 39 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 3 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 5 | 999 |  |
| 85 | `run_attack` | 102 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 103 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 104 | 3 | 1 | 88 |  |
| 88 | `run_attack` | 105 | 3 | 2 | 89 |  |
| 89 | `run_attack` | 106 | 3 | 3 | 999 |  |
| 90 | `dash_attack` | 107 | 15 | 2 | 91 |  |
| 91 | `dash_attack` | 108 | 15 | 3 | 92 |  |
| 92 | `dash_attack` | 109 | 15 | 3 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |

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

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Da` | 265 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 280 |
| 0 | `standing` | `hit_Ua` | 295 |
| 1 | `standing` | `hit_Da` | 265 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 280 |
| 1 | `standing` | `hit_Ua` | 295 |
| 2 | `standing` | `hit_Da` | 265 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 280 |
| 2 | `standing` | `hit_Ua` | 295 |
| 3 | `standing` | `hit_Da` | 265 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 280 |
| 3 | `standing` | `hit_Ua` | 295 |
| 5 | `walking` | `hit_Da` | 265 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 280 |
| 5 | `walking` | `hit_Ua` | 295 |
| 6 | `walking` | `hit_Da` | 265 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 280 |
| 6 | `walking` | `hit_Ua` | 295 |
| 7 | `walking` | `hit_Da` | 265 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 280 |
| 7 | `walking` | `hit_Ua` | 295 |
| 8 | `walking` | `hit_Da` | 265 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 280 |
| 8 | `walking` | `hit_Ua` | 295 |
| 75 | `super_punch` | `hit_Da` | 265 |
| 75 | `super_punch` | `hit_Fj` | 280 |
| 75 | `super_punch` | `hit_Ua` | 295 |
| 88 | `run_attack` | `hit_Da` | 265 |
| 88 | `run_attack` | `hit_Fj` | 280 |
| 88 | `run_attack` | `hit_Ua` | 295 |
| 89 | `run_attack` | `hit_Da` | 265 |
| 89 | `run_attack` | `hit_Fj` | 280 |
| 89 | `run_attack` | `hit_Ua` | 295 |
| 89 | `run_attack` | `hit_a` | 70 |
| 110 | `defend` | `hit_Da` | 265 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 280 |
| 110 | `defend` | `hit_Ua` | 295 |
| 121 | `catching` | `hit_Da` | 265 |
| 121 | `catching` | `hit_Fj` | 280 |
| 121 | `catching` | `hit_Ua` | 295 |
| 241 | `ball1` | `hit_a` | 242 |
| 246 | `ball2` | `hit_a` | 251 |
| 261 | `ball34` | `hit_a` | 262 |
| 284 | `c_foot` | `hit_d` | 288 |
| 284 | `c_foot` | `hit_j` | 288 |
| 285 | `c_foot` | `hit_d` | 288 |
| 285 | `c_foot` | `hit_j` | 288 |
| 286 | `c_foot` | `hit_d` | 288 |
| 286 | `c_foot` | `hit_j` | 288 |
| 287 | `c_foot` | `hit_d` | 288 |
| 287 | `c_foot` | `hit_j` | 288 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 6 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 6 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 1 | 62 |  |
| 62 | `punch` | 12 | 3 | 1 | 63 |  |
| 63 | `punch` | 13 | 3 | 1 | 999 |  |
| 65 | `punch` | 14 | 3 | 2 | 66 |  |
| 66 | `punch` | 15 | 3 | 1 | 67 |  |
| 67 | `punch` | 16 | 3 | 1 | 68 |  |
| 68 | `punch` | 17 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 8 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 9 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 19 | 3 | 1 | 73 |  |
| 73 | `super_punch` | 29 | 3 | 3 | 74 |  |
| 74 | `super_punch` | 39 | 3 | 1 | 75 |  |
| 75 | `super_punch` | 49 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 37 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 38 | 3 | 5 | 82 |  |
| 82 | `jump_attack` | 37 | 3 | 3 | 999 |  |
| 85 | `run_attack` | 102 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 103 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 104 | 3 | 1 | 88 |  |
| 88 | `run_attack` | 105 | 3 | 1 | 89 |  |
| 89 | `run_attack` | 106 | 3 | 1 | 999 |  |
| 90 | `dash_attack` | 107 | 15 | 2 | 91 |  |
| 91 | `dash_attack` | 108 | 15 | 6 | 92 |  |
| 92 | `dash_attack` | 109 | 15 | 4 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 4 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |

---

## Personagem 10: Woody
<a id="personagem-10-woody"></a>

- **Arquivo (.dat):** `woody.dat`
- **Head (seleção):** `sprite\sys\woody_f.bmp`
- **Small (HUD):** `sprite\sys\woody_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\woody_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\woody_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\woody_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Da` | 260 |
| 0 | `standing` | `hit_Dj` | 290 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 250 |
| 0 | `standing` | `hit_Ua` | 70 |
| 0 | `standing` | `hit_Uj` | 275 |
| 1 | `standing` | `hit_Da` | 260 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 250 |
| 1 | `standing` | `hit_Ua` | 70 |
| 2 | `standing` | `hit_Da` | 260 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 250 |
| 2 | `standing` | `hit_Ua` | 70 |
| 3 | `standing` | `hit_Da` | 260 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 250 |
| 3 | `standing` | `hit_Ua` | 70 |
| 5 | `walking` | `hit_Da` | 260 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 250 |
| 5 | `walking` | `hit_Ua` | 70 |
| 6 | `walking` | `hit_Da` | 260 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 250 |
| 6 | `walking` | `hit_Ua` | 70 |
| 7 | `walking` | `hit_Da` | 260 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 250 |
| 7 | `walking` | `hit_Ua` | 70 |
| 8 | `walking` | `hit_Da` | 260 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 250 |
| 8 | `walking` | `hit_Ua` | 70 |
| 78 | `run_attack` | `hit_a` | 70 |
| 88 | `run_attack` | `hit_a` | 89 |
| 102 | `rowing` | `hit_Da` | 260 |
| 103 | `rowing` | `hit_Da` | 260 |
| 104 | `rowing` | `hit_Da` | 260 |
| 105 | `rowing` | `hit_Da` | 260 |
| 106 | `rowing` | `hit_Da` | 260 |
| 107 | `rowing` | `hit_Da` | 260 |
| 110 | `defend` | `hit_Da` | 260 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 250 |
| 110 | `defend` | `hit_Ua` | 70 |
| 212 | `jump` | `hit_Dj` | 290 |
| 212 | `jump` | `hit_Uj` | 275 |
| 215 | `crouch` | `hit_Fj` | 252 |
| 219 | `crouch2` | `hit_Fj` | 252 |
| 271 | `cleg` | `hit_Fj` | 252 |
| 271 | `cleg` | `hit_Ua` | 70 |
| 286 | `teleport` | `hit_Fj` | 252 |
| 286 | `teleport` | `hit_Ua` | 70 |
| 287 | `teleport` | `hit_Fj` | 252 |
| 287 | `teleport` | `hit_Ua` | 70 |
| 288 | `teleport` | `hit_Fj` | 252 |
| 288 | `teleport` | `hit_Ua` | 70 |
| 301 | `teleport` | `hit_Fj` | 252 |
| 301 | `teleport` | `hit_Ua` | 70 |
| 302 | `teleport` | `hit_Fj` | 252 |
| 302 | `teleport` | `hit_Ua` | 70 |
| 303 | `teleport` | `hit_Fj` | 252 |
| 303 | `teleport` | `hit_Ua` | 70 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 9 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 1 | 61 |  |
| 61 | `punch` | 11 | 3 | 1 | 62 |  |
| 62 | `punch` | 12 | 3 | 1 | 63 |  |
| 63 | `punch` | 13 | 3 | 1 | 999 |  |
| 65 | `punch` | 14 | 3 | 1 | 66 |  |
| 66 | `punch` | 15 | 3 | 1 | 67 |  |
| 67 | `punch` | 16 | 3 | 1 | 68 |  |
| 68 | `punch` | 17 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 8 | 3 | 1 | 71 |  |
| 71 | `super_punch` | 9 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 19 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 29 | 3 | 2 | 74 |  |
| 74 | `super_punch` | 39 | 3 | 1 | 215 |  |
| 75 | `super_punch` | 49 | 3 | 3 | 999 |  |
| 76 | `run_attack` | 105 | 3 | 2 | 77 |  |
| 77 | `run_attack` | 106 | 3 | 2 | 78 |  |
| 78 | `run_attack` | 107 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 37 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 38 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 38 | 3 | 2 | 83 |  |
| 83 | `jump_attack` | 18 | 3 | 5 | 84 |  |
| 84 | `jump_attack` | 68 | 3 | 6 | 999 |  |
| 85 | `run_attack` | 100 | 3 | 3 | 86 |  |
| 86 | `run_attack` | 101 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 102 | 3 | 1 | 88 |  |
| 88 | `run_attack` | 103 | 3 | 3 | 999 |  |
| 89 | `run_attack` | 104 | 3 | 2 | 76 |  |
| 90 | `dash_attack` | 108 | 15 | 2 | 91 |  |
| 91 | `dash_attack` | 109 | 15 | 6 | 92 |  |
| 92 | `dash_attack` | 108 | 15 | 4 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 4 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |

---

## Personagem 11: Davis
<a id="personagem-11-davis"></a>

- **Arquivo (.dat):** `davis.dat`
- **Head (seleção):** `sprite\sys\davis_f.bmp`
- **Small (HUD):** `sprite\sys\davis_s.bmp`

### Spritesheets (BMP) e ranges `pic`
| Range pic | BMP | Célula (w×h) | Grade (row×col) |
|---|---|---:|---:|
| 0-69 | `sprite\sys\davis_0.bmp` | 79×79 | 10×7 |
| 70-139 | `sprite\sys\davis_1.bmp` | 79×79 | 10×7 |
| 140-209 | `sprite\sys\davis_2.bmp` | 79×79 | 10×7 |

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Da` | 270 |
| 0 | `standing` | `hit_Fa` | 240 |
| 0 | `standing` | `hit_Ua` | 300 |
| 0 | `standing` | `hit_Uj` | 290 |
| 1 | `standing` | `hit_Da` | 270 |
| 1 | `standing` | `hit_Fa` | 240 |
| 1 | `standing` | `hit_Ua` | 300 |
| 1 | `standing` | `hit_Uj` | 290 |
| 2 | `standing` | `hit_Da` | 270 |
| 2 | `standing` | `hit_Fa` | 240 |
| 2 | `standing` | `hit_Ua` | 300 |
| 2 | `standing` | `hit_Uj` | 290 |
| 3 | `standing` | `hit_Da` | 270 |
| 3 | `standing` | `hit_Fa` | 240 |
| 3 | `standing` | `hit_Ua` | 300 |
| 3 | `standing` | `hit_Uj` | 290 |
| 5 | `walking` | `hit_Da` | 270 |
| 5 | `walking` | `hit_Fa` | 240 |
| 5 | `walking` | `hit_Ua` | 300 |
| 5 | `walking` | `hit_Uj` | 290 |
| 6 | `walking` | `hit_Da` | 270 |
| 6 | `walking` | `hit_Fa` | 240 |
| 6 | `walking` | `hit_Ua` | 300 |
| 6 | `walking` | `hit_Uj` | 290 |
| 7 | `walking` | `hit_Da` | 270 |
| 7 | `walking` | `hit_Fa` | 240 |
| 7 | `walking` | `hit_Ua` | 300 |
| 7 | `walking` | `hit_Uj` | 290 |
| 8 | `walking` | `hit_Da` | 270 |
| 8 | `walking` | `hit_Fa` | 240 |
| 8 | `walking` | `hit_Ua` | 300 |
| 8 | `walking` | `hit_Uj` | 290 |
| 9 | `running` | `hit_Da` | 270 |
| 9 | `running` | `hit_Fa` | 240 |
| 9 | `running` | `hit_Ua` | 300 |
| 9 | `running` | `hit_Uj` | 290 |
| 10 | `running` | `hit_Da` | 270 |
| 10 | `running` | `hit_Fa` | 240 |
| 10 | `running` | `hit_Ua` | 300 |
| 10 | `running` | `hit_Uj` | 290 |
| 11 | `running` | `hit_Da` | 270 |
| 11 | `running` | `hit_Fa` | 240 |
| 11 | `running` | `hit_Ua` | 300 |
| 11 | `running` | `hit_Uj` | 290 |
| 39 | `super_punch` | `hit_j` | 290 |
| 87 | `run_attack` | `hit_a` | 89 |
| 102 | `rowing` | `hit_Ua` | 300 |
| 102 | `rowing` | `hit_Uj` | 290 |
| 103 | `rowing` | `hit_Ua` | 300 |
| 103 | `rowing` | `hit_Uj` | 290 |
| 104 | `rowing` | `hit_Ua` | 300 |
| 104 | `rowing` | `hit_Uj` | 290 |
| 105 | `rowing` | `hit_Ua` | 300 |
| 105 | `rowing` | `hit_Uj` | 290 |
| 106 | `rowing` | `hit_Ua` | 300 |
| 106 | `rowing` | `hit_Uj` | 290 |
| 107 | `rowing` | `hit_Ua` | 300 |
| 107 | `rowing` | `hit_Uj` | 290 |
| 110 | `defend` | `hit_Da` | 270 |
| 110 | `defend` | `hit_Fa` | 240 |
| 110 | `defend` | `hit_Ua` | 300 |
| 110 | `defend` | `hit_Uj` | 290 |
| 121 | `catching` | `hit_Da` | 274 |
| 121 | `catching` | `hit_Ua` | 300 |
| 246 | `ball1` | `hit_a` | 247 |
| 252 | `ball2` | `hit_a` | 253 |
| 258 | `ball3` | `hit_a` | 259 |
| 263 | `ball4` | `hit_a` | 264 |
| 278 | `many_punch` | `hit_Ua` | 300 |
| 279 | `many_punch` | `hit_Ua` | 300 |
| 281 | `many_punch` | `hit_j` | 290 |
| 282 | `many_punch` | `hit_j` | 290 |
| 292 | `jumphit` | `hit_a` | 293 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 9 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 |  |
| 38 | `super_punch` | 39 | 3 | 2 | 39 |  |
| 39 | `super_punch` | 49 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 107 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 108 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 109 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 108 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 1 | 61 |  |
| 61 | `punch` | 11 | 3 | 1 | 62 |  |
| 62 | `punch` | 12 | 3 | 1 | 63 |  |
| 63 | `punch` | 13 | 3 | 1 | 999 |  |
| 65 | `punch` | 14 | 3 | 1 | 66 |  |
| 66 | `punch` | 15 | 3 | 1 | 67 |  |
| 67 | `punch` | 16 | 3 | 1 | 68 |  |
| 68 | `punch` | 17 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 67 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 68 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 8 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 9 | 3 | 1 | 74 |  |
| 74 | `super_punch` | 19 | 3 | 2 | 75 |  |
| 75 | `super_punch` | 29 | 3 | 1 | 38 |  |
| 79 | `jump_attack` | 137 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 132 | 3 | 1 | 81 |  |
| 81 | `jump_attack` | 133 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 134 | 3 | 1 | 83 |  |
| 83 | `jump_attack` | 135 | 3 | 3 | 84 |  |
| 84 | `jump_attack` | 136 | 3 | 1 | 79 |  |
| 85 | `run_attack` | 100 | 3 | 2 | 86 |  |
| 86 | `run_attack` | 101 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 102 | 3 | 1 | 88 |  |
| 88 | `run_attack` | 103 | 3 | 3 | 89 |  |
| 89 | `run_attack` | 104 | 3 | 2 | 97 |  |
| 90 | `dash_attack` | 132 | 3 | 1 | 91 |  |
| 91 | `dash_attack` | 133 | 3 | 1 | 92 |  |
| 92 | `dash_attack` | 134 | 3 | 1 | 93 |  |
| 93 | `dash_attack` | 135 | 3 | 7 | 94 |  |
| 94 | `dash_attack` | 136 | 3 | 1 | 96 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 96 | `dash_attack` | 137 | 3 | 3 | 96 |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 4 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |

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

### Inputs mapeados (`hit_*`) encontrados
_Nenhum `hit_*` encontrado._

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 5 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 5 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 6 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 6 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 10 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 6 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 38 | 3 | 2 | 72 |  |
| 72 | `super_punch` | 39 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 104 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 6 | 999 |  |
| 85 | `run_attack` | 102 | 3 | 4 | 86 |  |
| 86 | `run_attack` | 103 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 104 | 3 | 3 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 3 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 20 | 999 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 5 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 5 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 5 | 999 |  |
| 120 | `catching` | 51 | 9 | 5 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 105 | 15 | 2 | 201 |  |
| 201 | `ice` | 115 | 13 | 90 | 202 |  |
| 202 | `ice` | 105 | 15 | 1 | 182 |  |
| 203 | `fire` | 100 | 18 | 1 | 204 |  |
| 204 | `fire` | 101 | 18 | 1 | 203 |  |

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

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 214 | `dash` | `hit_a` | 81 |
| 217 | `dash` | `hit_a` | 81 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 4 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 4 | 3 |  |
| 3 | `standing` | 3 | 0 | 6 | 999 | 0 |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 49 | `throw_lying_man` | 28 | 15 | 3 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 2 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 1 | 61 |  |
| 61 | `punch` | 11 | 3 | 1 | 62 |  |
| 62 | `punch` | 12 | 3 | 2 | 63 |  |
| 63 | `punch` | 13 | 3 | 3 | 64 |  |
| 64 | `punch` | 14 | 3 | 1 | 66 | 201 |
| 65 | `punch` | 10 | 3 | 1 | 61 |  |
| 66 | `punch` | 15 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 3 | 71 |  |
| 71 | `super_punch` | 29 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 39 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 39 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 8 | 3 | 1 | 81 |  |
| 81 | `jump_attack` | 9 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 49 | 3 | 2 | 83 |  |
| 83 | `jump_attack` | 58 | 3 | 1 | 84 |  |
| 84 | `jump_attack` | 59 | 3 | 11 | 999 | 201 |
| 85 | `run_attack` | 37 | 3 | 1 | 86 |  |
| 86 | `run_attack` | 29 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 39 | 3 | 3 | 88 |  |
| 88 | `run_attack` | 39 | 3 | 2 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 1 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 2 | 92 |  |
| 92 | `dash_attack` | 108 | 15 | 0 | 0 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 18 | 6 | 2 | 103 |  |
| 103 | `rowing` | 16 | 6 | 2 | 104 |  |
| 104 | `rowing` | 17 | 6 | 2 | 105 |  |
| 105 | `rowing` | 18 | 6 | 2 | 219 |  |
| 106 | `rowing` | 16 | 6 | 2 | 219 |  |
| 107 | `rowing` | 17 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 3 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 3 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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
| 240 | `run` | `hit_d` | 245 |
| 240 | `run` | `hit_j` | 245 |
| 241 | `run` | `hit_d` | 245 |
| 241 | `run` | `hit_j` | 245 |
| 242 | `run` | `hit_d` | 245 |
| 242 | `run` | `hit_j` | 245 |
| 243 | `run` | `hit_d` | 245 |
| 243 | `run` | `hit_j` | 245 |
| 244 | `run` | `hit_d` | 245 |
| 244 | `run` | `hit_j` | 245 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 3 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 6 | 999 |  |
| 5 | `walking` | 4 | 1 | 2 | 999 |  |
| 6 | `walking` | 5 | 1 | 2 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 2 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 4 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 3 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 4 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 2 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 4 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 3 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 3 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 5 | 86 |  |
| 71 | `super_punch` | 19 | 3 | 5 | 72 |  |
| 72 | `run_attack` | 18 | 3 | 1 | 73 |  |
| 73 | `run_attack` | 9 | 3 | 3 | 74 |  |
| 74 | `run_attack` | 8 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 1 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 16 | 999 |  |
| 85 | `run_attack` | 37 | 3 | 3 | 86 |  |
| 86 | `run_attack` | 38 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 39 | 3 | 3 | 88 |  |
| 88 | `run_attack` | 29 | 3 | 1 | 89 |  |
| 89 | `run_attack` | 29 | 3 | 2 | 999 |  |
| 90 | `dash_attack` | 106 | 15 | 1 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 20 | 999 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 5 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 2 | 124 |  |
| 124 | `catching` | 52 | 9 | 1 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 9 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 107 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 108 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 109 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 108 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 14 | 3 | 2 | 71 |  |
| 71 | `super_punch` | 15 | 3 | 2 | 72 |  |
| 72 | `super_punch` | 16 | 3 | 1 | 73 |  |
| 73 | `super_punch` | 17 | 3 | 2 | 74 |  |
| 74 | `super_punch` | 18 | 3 | 2 | 75 |  |
| 75 | `super_punch` | 19 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 132 | 3 | 4 | 81 |  |
| 81 | `jump_attack` | 133 | 3 | 9 | 999 |  |
| 85 | `run_attack` | 14 | 3 | 2 | 71 |  |
| 90 | `jump_attack` | 132 | 3 | 4 | 91 |  |
| 91 | `jump_attack` | 133 | 3 | 9 | 999 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 4 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 37 | 15 | 2 | 201 |  |
| 201 | `ice` | 38 | 13 | 90 | 202 |  |
| 202 | `ice` | 37 | 15 | 1 | 182 |  |

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

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Dj` | 250 |
| 0 | `standing` | `hit_Fa` | 235 |
| 0 | `standing` | `hit_Fj` | 270 |
| 0 | `standing` | `hit_Uj` | 260 |
| 1 | `standing` | `hit_Dj` | 250 |
| 1 | `standing` | `hit_Fa` | 235 |
| 1 | `standing` | `hit_Fj` | 270 |
| 1 | `standing` | `hit_Uj` | 260 |
| 2 | `standing` | `hit_Dj` | 250 |
| 2 | `standing` | `hit_Fa` | 235 |
| 2 | `standing` | `hit_Fj` | 270 |
| 2 | `standing` | `hit_Uj` | 260 |
| 3 | `standing` | `hit_Dj` | 250 |
| 3 | `standing` | `hit_Fa` | 235 |
| 3 | `standing` | `hit_Fj` | 270 |
| 3 | `standing` | `hit_Uj` | 260 |
| 5 | `walking` | `hit_Dj` | 250 |
| 5 | `walking` | `hit_Fa` | 235 |
| 5 | `walking` | `hit_Fj` | 270 |
| 5 | `walking` | `hit_Uj` | 260 |
| 6 | `walking` | `hit_Dj` | 250 |
| 6 | `walking` | `hit_Fa` | 235 |
| 6 | `walking` | `hit_Fj` | 270 |
| 6 | `walking` | `hit_Uj` | 260 |
| 7 | `walking` | `hit_Dj` | 250 |
| 7 | `walking` | `hit_Fa` | 235 |
| 7 | `walking` | `hit_Fj` | 270 |
| 7 | `walking` | `hit_Uj` | 260 |
| 8 | `walking` | `hit_Dj` | 250 |
| 8 | `walking` | `hit_Fa` | 235 |
| 8 | `walking` | `hit_Fj` | 270 |
| 8 | `walking` | `hit_Uj` | 260 |
| 110 | `defend` | `hit_Dj` | 250 |
| 110 | `defend` | `hit_Fa` | 235 |
| 110 | `defend` | `hit_Fj` | 270 |
| 110 | `defend` | `hit_Uj` | 260 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 6 | 1 |  |
| 1 | `standing` | 1 | 0 | 5 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 6 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 136 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 137 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 138 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 137 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 14 | 3 | 1 | 71 |  |
| 71 | `super_punch` | 15 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 16 | 3 | 1 | 73 |  |
| 73 | `super_punch` | 17 | 3 | 2 | 74 |  |
| 74 | `super_punch` | 37 | 3 | 1 | 75 |  |
| 75 | `super_punch` | 38 | 3 | 1 | 76 |  |
| 76 | `super_punch` | 13 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 29 | 3 | 3 | 81 |  |
| 81 | `jump_attack` | 39 | 3 | 3 | 82 |  |
| 82 | `jump_attack` | 67 | 3 | 5 | 999 |  |
| 85 | `run_attack` | 14 | 3 | 1 | 71 |  |
| 90 | `dash_attack` | 29 | 3 | 3 | 81 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |
| 201 | `ice` | 9 | 13 | 90 | 202 |  |
| 202 | `ice` | 8 | 15 | 1 | 182 |  |
| 203 | `fire` | 18 | 18 | 1 | 204 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 5 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 5 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 2 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 3 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 3 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 6 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 135 | 7 | 1 | 71 |  |
| 71 | `super_punch` | 136 | 7 | 1 | 72 |  |
| 72 | `super_punch` | 137 | 3 | 2 | 73 |  |
| 73 | `super_punch` | 138 | 3 | 2 | 74 |  |
| 74 | `super_punch` | 139 | 3 | 2 | 75 |  |
| 75 | `super_punch` | 129 | 3 | 3 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 6 | 999 |  |
| 85 | `run_attack` | 135 | 7 | 1 | 71 |  |
| 90 | `dash_attack` | 106 | 15 | 3 | 91 |  |
| 91 | `dash_attack` | 107 | 15 | 20 | 999 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 47 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 46 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 5 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 5 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 5 | 999 |  |
| 120 | `catching` | 51 | 9 | 5 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 105 | 15 | 2 | 201 |  |
| 201 | `ice` | 115 | 13 | 90 | 202 |  |
| 202 | `ice` | 105 | 15 | 1 | 182 |  |
| 203 | `fire` | 100 | 18 | 1 | 204 |  |
| 204 | `fire` | 101 | 18 | 1 | 203 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 9 | 1 |  |
| 1 | `standing` | 1 | 0 | 5 | 2 |  |
| 2 | `standing` | 2 | 0 | 3 | 3 |  |
| 3 | `standing` | 3 | 0 | 8 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 9 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 2 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 2 | 26 |  |
| 26 | `normal_weapon_atck` | 75 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 85 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 6 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 91 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 95 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 6 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 11 | 999 |  |
| 55 | `weapon_drink` | 136 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 137 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 138 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 137 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 2 | 62 |  |
| 62 | `punch` | 12 | 3 | 1 | 999 |  |
| 65 | `punch` | 16 | 3 | 2 | 66 |  |
| 66 | `punch` | 17 | 3 | 2 | 67 |  |
| 67 | `punch` | 29 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 37 | 3 | 2 | 86 |  |
| 80 | `jump_attack` | 13 | 3 | 3 | 81 |  |
| 81 | `jump_attack` | 14 | 3 | 4 | 82 |  |
| 82 | `jump_attack` | 15 | 3 | 1 | 83 |  |
| 83 | `jump_attack` | 13 | 3 | 5 | 999 |  |
| 85 | `run_attack` | 37 | 3 | 2 | 86 |  |
| 86 | `run_attack` | 38 | 3 | 2 | 87 |  |
| 87 | `run_attack` | 39 | 3 | 2 | 88 |  |
| 88 | `run_attack` | 67 | 3 | 2 | 999 |  |
| 90 | `dash_attack` | 104 | 15 | 2 | 91 |  |
| 91 | `dash_attack` | 105 | 15 | 9 | 216 |  |
| 92 | `dash_attack` | 108 | 15 | 5 | 93 |  |
| 93 | `dash_attack` | 109 | 15 | 4 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 49 | 6 | 2 | 104 |  |
| 104 | `rowing` | 59 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 49 | 6 | 2 | 219 |  |
| 107 | `rowing` | 59 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 5 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 4 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 10 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 3 | 3 |  |
| 3 | `standing` | 3 | 0 | 8 | 4 |  |
| 4 | `standing` | 1 | 0 | 2 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 26 | 2 | 3 | 0 |  |
| 10 | `running` | 27 | 2 | 3 | 0 |  |
| 11 | `running` | 28 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 30 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 31 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 32 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 33 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 34 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 35 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 36 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 41 | 15 | 9 | 999 |  |
| 20 | `normal_weapon_atck` | 37 | 15 | 3 | 21 |  |
| 21 | `normal_weapon_atck` | 38 | 15 | 3 | 999 |  |
| 25 | `normal_weapon_atck` | 37 | 15 | 3 | 21 |  |
| 30 | `jump_weapon_atck` | 37 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 38 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 38 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 37 | 15 | 3 | 36 |  |
| 36 | `run_weapon_atck` | 38 | 15 | 3 | 999 |  |
| 40 | `dash_weapon_atck` | 37 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 38 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 38 | 3 | 4 | 999 |  |
| 45 | `light_weapon_thw` | 37 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 38 | 15 | 3 | 999 |  |
| 50 | `heavy_weapon_thw` | 37 | 15 | 3 | 51 |  |
| 51 | `heavy_weapon_thw` | 38 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 37 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 38 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 38 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 46 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 47 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 46 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 47 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 1 | 241 |  |
| 65 | `punch` | 10 | 3 | 1 | 241 |  |
| 70 | `super` | 20 | 3 | 2 | 252 |  |
| 80 | `jump_attack` | 29 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 39 | 3 | 1 | 82 |  |
| 82 | `jump_attack` | 53 | 3 | 1 | 83 |  |
| 83 | `jump_attack` | 49 | 3 | 5 | 999 |  |
| 85 | `run_attack` | 20 | 3 | 2 | 252 |  |
| 90 | `dash_attack` | 14 | 15 | 3 | 91 |  |
| 91 | `dash_attack` | 15 | 15 | 3 | 92 |  |
| 92 | `dash_attack` | 16 | 15 | 9 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 55 | 6 | 2 | 101 |  |
| 101 | `rowing` | 38 | 6 | 6 | 0 |  |
| 102 | `rowing` | 42 | 7 | 12 | 999 |  |
| 108 | `rowing` | 54 | 6 | 3 | 109 |  |
| 109 | `rowing` | 67 | 6 | 6 | 0 |  |
| 110 | `defend` | 42 | 7 | 12 | 999 |  |
| 111 | `defend` | 42 | 7 | 12 | 999 |  |
| 112 | `broken_defend` | 66 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 67 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 69 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 14 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 14 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 14 | 15 | 2 | 999 |  |
| 120 | `catching` | 44 | 9 | 2 | 121 |  |
| 121 | `catching` | 43 | 9 | 0 | 0 |  |
| 122 | `catching` | 44 | 9 | 5 | 123 |  |
| 123 | `catching` | 45 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 68 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 69 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 69 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 64 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 65 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 56 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 74 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 75 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 57 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 58 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 59 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 58 | 10 | 3 | 0 |  |
| 180 | `falling` | 64 | 12 | 3 | 0 |  |
| 181 | `falling` | 65 | 12 | 3 | 0 |  |
| 182 | `falling` | 54 | 12 | 3 | 0 |  |
| 183 | `falling` | 55 | 12 | 3 | 0 |  |
| 184 | `falling` | 56 | 12 | 3 | 0 |  |
| 185 | `falling` | 54 | 12 | 3 | 0 |  |
| 186 | `falling` | 74 | 12 | 3 | 0 |  |
| 187 | `falling` | 75 | 12 | 3 | 0 |  |
| 188 | `falling` | 57 | 12 | 3 | 0 |  |
| 189 | `falling` | 58 | 12 | 3 | 0 |  |
| 190 | `falling` | 59 | 12 | 3 | 0 |  |
| 191 | `falling` | 57 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |
| 201 | `ice` | 9 | 13 | 90 | 202 |  |
| 202 | `ice` | 8 | 15 | 1 | 182 |  |
| 203 | `fire` | 61 | 18 | 1 | 204 |  |
| 204 | `fire` | 60 | 18 | 1 | 203 |  |
| 205 | `fire` | 61 | 18 | 1 | 206 |  |
| 206 | `fire` | 60 | 18 | 1 | 205 |  |
| 207 | `tired` | 69 | 15 | 2 | 0 |  |
| 210 | `jump` | 14 | 4 | 2 | 211 |  |
| 211 | `jump` | 14 | 4 | 2 | 212 |  |
| 212 | `jump` | 48 | 4 | 1 | 0 |  |
| 213 | `jump` | 48 | 4 | 1 | 0 |  |
| 214 | `jump` | 48 | 4 | 1 | 0 |  |
| 215 | `crouch` | 14 | 15 | 2 | 999 |  |
| 218 | `stop_running` | 40 | 15 | 8 | 999 |  |
| 219 | `crouch2` | 14 | 15 | 2 | 999 |  |
| 220 | `injured` | 66 | 11 | 2 | 221 |  |
| 221 | `injured` | 67 | 11 | 3 | 999 |  |
| 222 | `injured` | 69 | 11 | 2 | 223 |  |
| 223 | `injured` | 68 | 11 | 3 | 999 |  |
| 224 | `injured` | 69 | 11 | 2 | 225 |  |
| 225 | `injured` | 68 | 11 | 3 | 999 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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
| 121 | `catching` | `hit_Da` | 274 |
| 121 | `catching` | `hit_Ua` | 300 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 9 | 4 |  |
| 4 | `standing` | 2 | 0 | 0 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 74 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 75 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 76 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 27 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 2 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 3 | 999 |  |
| 25 | `normal_weapon_atck` | 70 | 3 | 2 | 21 |  |
| 30 | `jump_weapon_atck` | 72 | 3 | 2 | 31 |  |
| 31 | `jump_weapon_atck` | 73 | 3 | 3 | 999 |  |
| 35 | `run_weapon_atck` | 70 | 3 | 2 | 36 |  |
| 36 | `run_weapon_atck` | 71 | 3 | 3 | 999 |  |
| 40 | `dash_weapon_atck` | 72 | 3 | 2 | 41 |  |
| 41 | `dash_weapon_atck` | 73 | 3 | 3 | 999 |  |
| 45 | `light_weapon_thw` | 70 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 71 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 72 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 73 | 15 | 6 | 999 |  |
| 55 | `weapon_drink` | 92 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 93 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 94 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 93 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 14 | 3 | 3 | 71 |  |
| 71 | `super_punch` | 15 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 16 | 3 | 3 | 73 |  |
| 73 | `super_punch` | 17 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 95 | 3 | 2 | 81 |  |
| 81 | `jump_attack` | 96 | 3 | 6 | 999 |  |
| 85 | `run_attack` | 14 | 3 | 3 | 71 |  |
| 90 | `dash_attack` | 95 | 3 | 2 | 91 |  |
| 91 | `dash_attack` | 96 | 3 | 9 | 999 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 96 | `dash_attack` | 137 | 3 | 3 | 96 |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 80 | 6 | 3 | 109 |  |
| 109 | `rowing` | 81 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 4 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 37 | 15 | 2 | 201 |  |
| 201 | `ice` | 38 | 13 | 90 | 202 |  |
| 202 | `ice` | 37 | 15 | 1 | 182 |  |
| 203 | `fire` | 78 | 18 | 1 | 204 |  |
| 204 | `fire` | 79 | 18 | 1 | 203 |  |
| 205 | `fire` | 88 | 18 | 1 | 206 |  |
| 206 | `fire` | 89 | 18 | 1 | 205 |  |
| 207 | `tired` | 69 | 15 | 2 | 0 |  |
| 210 | `jump` | 60 | 4 | 1 | 211 |  |
| 211 | `jump` | 61 | 4 | 1 | 212 |  |
| 212 | `jump` | 62 | 4 | 1 | 0 |  |
| 213 | `dash` | 63 | 5 | 8 | 0 |  |
| 214 | `dash` | 64 | 5 | 8 | 0 |  |
| 215 | `crouch` | 60 | 15 | 2 | 999 |  |
| 218 | `stop_running` | 12 | 15 | 5 | 999 |  |

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

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Da` | 240 |
| 0 | `standing` | `hit_Fa` | 250 |
| 1 | `standing` | `hit_Da` | 240 |
| 1 | `standing` | `hit_Fa` | 250 |
| 2 | `standing` | `hit_Da` | 240 |
| 2 | `standing` | `hit_Fa` | 250 |
| 3 | `standing` | `hit_Da` | 240 |
| 3 | `standing` | `hit_Fa` | 250 |
| 4 | `standing` | `hit_Da` | 240 |
| 4 | `standing` | `hit_Fa` | 250 |
| 5 | `walking` | `hit_Da` | 240 |
| 5 | `walking` | `hit_Fa` | 250 |
| 6 | `walking` | `hit_Da` | 240 |
| 6 | `walking` | `hit_Fa` | 250 |
| 7 | `walking` | `hit_Da` | 240 |
| 7 | `walking` | `hit_Fa` | 250 |
| 8 | `walking` | `hit_Da` | 240 |
| 8 | `walking` | `hit_Fa` | 250 |
| 9 | `running` | `hit_Da` | 240 |
| 9 | `running` | `hit_Fa` | 250 |
| 10 | `running` | `hit_Da` | 240 |
| 10 | `running` | `hit_Fa` | 250 |
| 11 | `running` | `hit_Da` | 240 |
| 11 | `running` | `hit_Fa` | 250 |
| 110 | `defend` | `hit_Da` | 240 |
| 110 | `defend` | `hit_Fa` | 250 |
| 121 | `catching` | `hit_Da` | 274 |
| 121 | `catching` | `hit_Ua` | 300 |
| 245 | `punch` | `hit_Da` | 260 |
| 245 | `punch` | `hit_a` | 260 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 5 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 5 | 3 |  |
| 3 | `standing` | 3 | 0 | 9 | 4 |  |
| 4 | `standing` | 2 | 0 | 0 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 54 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 55 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 56 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 27 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 2 | 21 |  |
| 21 | `normal_weapon_atck` | 71 | 3 | 3 | 999 |  |
| 25 | `normal_weapon_atck` | 70 | 3 | 2 | 21 |  |
| 30 | `jump_weapon_atck` | 72 | 3 | 2 | 31 |  |
| 31 | `jump_weapon_atck` | 73 | 3 | 3 | 999 |  |
| 35 | `run_weapon_atck` | 70 | 3 | 2 | 36 |  |
| 36 | `run_weapon_atck` | 71 | 3 | 3 | 999 |  |
| 40 | `dash_weapon_atck` | 72 | 3 | 2 | 41 |  |
| 41 | `dash_weapon_atck` | 73 | 3 | 3 | 999 |  |
| 45 | `light_weapon_thw` | 70 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 71 | 15 | 9 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 4 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 72 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 73 | 15 | 6 | 999 |  |
| 55 | `weapon_drink` | 74 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 75 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 76 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 75 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 3 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 3 | 999 |  |
| 70 | `super_punch` | 14 | 3 | 5 | 71 |  |
| 71 | `super_punch` | 15 | 3 | 2 | 72 |  |
| 72 | `super_punch` | 16 | 3 | 4 | 73 |  |
| 73 | `super_punch` | 17 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 60 | 3 | 4 | 81 |  |
| 81 | `jump_attack` | 61 | 3 | 9 | 999 |  |
| 85 | `run_attack` | 14 | 3 | 5 | 71 |  |
| 90 | `dash_attack` | 60 | 3 | 4 | 91 |  |
| 91 | `dash_attack` | 61 | 3 | 11 | 999 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 96 | `dash_attack` | 137 | 3 | 3 | 96 |  |
| 97 | `run_attack` | 105 | 3 | 2 | 98 |  |
| 98 | `run_attack` | 106 | 3 | 1 | 999 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 67 | 6 | 3 | 109 |  |
| 109 | `rowing` | 68 | 6 | 6 | 0 |  |
| 110 | `defend` | 53 | 7 | 12 | 999 |  |
| 111 | `defend` | 53 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 5 | 123 |  |
| 123 | `catching` | 52 | 9 | 4 | 121 |  |
| 130 | `picked_caught` | 47 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 49 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 49 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 37 | 15 | 2 | 201 |  |
| 201 | `ice` | 38 | 13 | 90 | 202 |  |
| 202 | `ice` | 37 | 15 | 1 | 182 |  |
| 203 | `fire` | 8 | 18 | 1 | 204 |  |
| 204 | `fire` | 9 | 18 | 1 | 203 |  |
| 205 | `fire` | 18 | 18 | 1 | 206 |  |
| 206 | `fire` | 19 | 18 | 1 | 205 |  |
| 207 | `tired` | 69 | 15 | 2 | 0 |  |
| 210 | `jump` | 36 | 4 | 1 | 211 |  |
| 211 | `jump` | 36 | 4 | 1 | 212 |  |
| 212 | `jump` | 62 | 4 | 1 | 0 |  |
| 213 | `dash` | 63 | 5 | 8 | 0 |  |
| 214 | `dash` | 64 | 5 | 8 | 0 |  |
| 215 | `crouch` | 36 | 15 | 2 | 999 |  |
| 218 | `stop_running` | 12 | 15 | 5 | 999 |  |

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

### Inputs mapeados (`hit_*`) encontrados
| Frame origem | Nome | Binding | Vai para frame |
|---:|---|---|---:|
| 0 | `standing` | `hit_Da` | 240 |
| 0 | `standing` | `hit_Fa` | 260 |
| 1 | `standing` | `hit_Da` | 240 |
| 1 | `standing` | `hit_Fa` | 260 |
| 2 | `standing` | `hit_Da` | 240 |
| 2 | `standing` | `hit_Fa` | 260 |
| 3 | `standing` | `hit_Da` | 240 |
| 3 | `standing` | `hit_Fa` | 260 |
| 5 | `walking` | `hit_Da` | 240 |
| 5 | `walking` | `hit_Fa` | 260 |
| 6 | `walking` | `hit_Da` | 240 |
| 6 | `walking` | `hit_Fa` | 260 |
| 7 | `walking` | `hit_Da` | 240 |
| 7 | `walking` | `hit_Fa` | 260 |
| 8 | `walking` | `hit_Da` | 240 |
| 8 | `walking` | `hit_Fa` | 260 |
| 110 | `defend` | `hit_Da` | 240 |
| 110 | `defend` | `hit_Fa` | 260 |
| 111 | `defend` | `hit_Da` | 240 |
| 111 | `defend` | `hit_Fa` | 260 |
| 121 | `catching` | `hit_j` | -260 |
| 263 | `air_push` | `hit_a` | 260 |
| 264 | `air_push` | `hit_a` | 260 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 9 | 1 |  |
| 1 | `standing` | 1 | 0 | 3 | 2 |  |
| 2 | `standing` | 2 | 0 | 6 | 3 |  |
| 3 | `standing` | 3 | 0 | 8 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 4 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 72 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 73 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 74 | 3 | 1 | 27 |  |
| 27 | `normal_weapon_atck` | 76 | 3 | 1 | 28 |  |
| 28 | `normal_weapon_atck` | 77 | 3 | 1 | 999 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 1 | 33 |  |
| 33 | `jump_weapon_atck` | 83 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 84 | 3 | 1 | 37 |  |
| 37 | `run_weapon_atck` | 86 | 3 | 3 | 999 |  |
| 40 | `dash_weapon_atck` | 90 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 92 | 3 | 1 | 43 |  |
| 43 | `dash_weapon_atck` | 93 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 94 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 4 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 2 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 4 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 3 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 99 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 137 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 138 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 139 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 138 | 17 | 3 | 55 |  |
| 60 | `punch` | 210 | 3 | 1 | 61 |  |
| 61 | `punch` | 211 | 3 | 1 | 62 |  |
| 62 | `punch` | 212 | 3 | 1 | 999 |  |
| 65 | `punch` | 213 | 3 | 1 | 66 |  |
| 66 | `punch` | 214 | 3 | 1 | 67 |  |
| 67 | `punch` | 215 | 3 | 1 | 999 |  |
| 70 | `super_punch` | 153 | 9 | 2 | 254 |  |
| 75 | `run_attack` | 15 | 3 | 3 | 999 |  |
| 80 | `up_spear` | 222 | 9 | 2 | 81 |  |
| 81 | `up_spear` | 223 | 9 | 2 | 82 |  |
| 82 | `up_spear` | 224 | 9 | 2 | 83 |  |
| 83 | `up_spear` | 225 | 9 | 3 | 84 |  |
| 84 | `up_spear` | 226 | 9 | 2 | 999 |  |
| 85 | `run_attack` | 10 | 3 | 2 | 86 |  |
| 86 | `run_attack` | 11 | 3 | 1 | 87 |  |
| 87 | `run_attack` | 12 | 3 | 2 | 88 |  |
| 88 | `run_attack` | 13 | 3 | 1 | 89 |  |
| 89 | `run_attack` | 14 | 3 | 2 | 75 |  |
| 90 | `dash_attack` | 132 | 15 | 2 | 92 |  |
| 91 | `dash_attack` | 132 | 15 | 1 | 92 |  |
| 92 | `dash_attack` | 133 | 15 | 1 | 93 |  |
| 93 | `dash_attack` | 134 | 100 | 90 | 216 |  |
| 94 | `dash_attack` | 135 | 15 | 2 | 95 |  |
| 95 | `dash_attack` | 136 | 15 | 2 | 999 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 49 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 49 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 2 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 2 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 3 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 1 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 2 | 999 |  |
| 120 | `catching` | 51 | 9 | 1 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 4 | 123 |  |
| 123 | `catching` | 52 | 9 | 2 | 121 |  |
| 130 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |
| 201 | `ice` | 9 | 13 | 90 | 202 |  |
| 202 | `ice` | 8 | 15 | 1 | 182 |  |
| 203 | `fire` | 78 | 18 | 1 | 204 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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
| 246 | `disaster` | `hit_Ua` | 240 |
| 246 | `disaster` | `hit_a` | 240 |
| 270 | `ball` | `hit_d` | 280 |
| 270 | `ball` | `hit_j` | 280 |
| 271 | `ball` | `hit_d` | 280 |
| 271 | `ball` | `hit_j` | 280 |
| 272 | `ball` | `hit_d` | 280 |
| 272 | `ball` | `hit_j` | 280 |
| 273 | `ball` | `hit_d` | 280 |
| 273 | `ball` | `hit_j` | 280 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 10 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 3 | 3 |  |
| 3 | `standing` | 3 | 0 | 10 | 4 |  |
| 4 | `standing` | 1 | 0 | 2 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 20 | 2 | 3 | 0 |  |
| 10 | `running` | 21 | 2 | 3 | 0 |  |
| 11 | `running` | 22 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 23 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 24 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 25 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 26 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 125 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 126 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 127 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 128 | 15 | 4 | 999 |  |
| 20 | `normal_weapon_atck` | 70 | 3 | 1 | 22 |  |
| 22 | `normal_weapon_atck` | 71 | 3 | 1 | 23 |  |
| 23 | `normal_weapon_atck` | 72 | 3 | 1 | 999 |  |
| 25 | `normal_weapon_atck` | 70 | 3 | 1 | 22 |  |
| 30 | `jump_weapon_atck` | 80 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 81 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 82 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 70 | 3 | 1 | 36 |  |
| 36 | `run_weapon_atck` | 71 | 3 | 2 | 37 |  |
| 37 | `run_weapon_atck` | 72 | 3 | 2 | 999 |  |
| 40 | `dash_weapon_atck` | 80 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 81 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 82 | 3 | 8 | 999 |  |
| 45 | `light_weapon_thw` | 95 | 15 | 1 | 46 |  |
| 46 | `light_weapon_thw` | 96 | 15 | 1 | 47 |  |
| 47 | `light_weapon_thw` | 96 | 15 | 3 | 999 |  |
| 50 | `heavy_weapon_thw` | 27 | 15 | 2 | 51 |  |
| 51 | `heavy_weapon_thw` | 28 | 15 | 7 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 97 | 15 | 1 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 98 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 98 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 132 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 133 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 134 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 133 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 1 | 61 |  |
| 61 | `punch` | 11 | 3 | 2 | 999 |  |
| 65 | `punch` | 12 | 3 | 1 | 66 |  |
| 66 | `punch` | 13 | 3 | 2 | 999 |  |
| 70 | `super_punch` | 17 | 3 | 1 | 71 |  |
| 71 | `super_punch` | 18 | 3 | 1 | 72 |  |
| 72 | `super_punch` | 19 | 3 | 1 | 73 |  |
| 73 | `super_punch` | 29 | 3 | 2 | 74 |  |
| 74 | `super_punch` | 39 | 3 | 2 | 75 |  |
| 75 | `super_punch` | 49 | 3 | 2 | 999 |  |
| 80 | `jump_attack` | 14 | 3 | 3 | 81 |  |
| 81 | `jump_attack` | 15 | 3 | 3 | 82 |  |
| 82 | `jump_attack` | 16 | 3 | 6 | 999 |  |
| 85 | `run_attack` | 17 | 3 | 1 | 71 |  |
| 90 | `dash_attack` | 14 | 15 | 3 | 91 |  |
| 91 | `dash_attack` | 15 | 15 | 3 | 92 |  |
| 92 | `dash_attack` | 16 | 15 | 9 | 216 |  |
| 95 | `dash_defend` | 111 | 7 | 2 | 0 |  |
| 100 | `rowing` | 66 | 6 | 2 | 101 |  |
| 101 | `rowing` | 65 | 6 | 6 | 0 |  |
| 102 | `rowing` | 58 | 6 | 2 | 103 |  |
| 103 | `rowing` | 59 | 6 | 2 | 104 |  |
| 104 | `rowing` | 69 | 6 | 2 | 105 |  |
| 105 | `rowing` | 58 | 6 | 2 | 219 |  |
| 106 | `rowing` | 59 | 6 | 2 | 219 |  |
| 107 | `rowing` | 69 | 6 | 2 | 219 |  |
| 108 | `rowing` | 117 | 6 | 3 | 109 |  |
| 109 | `rowing` | 118 | 6 | 6 | 0 |  |
| 110 | `defend` | 56 | 7 | 12 | 999 |  |
| 111 | `defend` | 57 | 7 | 0 | 110 |  |
| 112 | `broken_defend` | 46 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 47 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 48 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 36 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 36 | 15 | 2 | 117 |  |
| 117 | `picking_heavy` | 36 | 15 | 1 | 999 |  |
| 120 | `catching` | 51 | 9 | 2 | 121 |  |
| 121 | `catching` | 50 | 9 | 0 | 0 |  |
| 122 | `catching` | 51 | 9 | 3 | 123 |  |
| 123 | `catching` | 52 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 53 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 30 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 31 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 32 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 33 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 34 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 35 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 40 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 41 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 42 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 43 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 44 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 45 | 10 | 3 | 0 |  |
| 180 | `falling` | 30 | 12 | 3 | 0 |  |
| 181 | `falling` | 31 | 12 | 3 | 0 |  |
| 182 | `falling` | 32 | 12 | 3 | 0 |  |
| 183 | `falling` | 33 | 12 | 3 | 0 |  |
| 184 | `falling` | 34 | 12 | 3 | 0 |  |
| 185 | `falling` | 35 | 12 | 3 | 0 |  |
| 186 | `falling` | 40 | 12 | 3 | 0 |  |
| 187 | `falling` | 41 | 12 | 3 | 0 |  |
| 188 | `falling` | 42 | 12 | 3 | 0 |  |
| 189 | `falling` | 43 | 12 | 3 | 0 |  |
| 190 | `falling` | 44 | 12 | 3 | 0 |  |
| 191 | `falling` | 45 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |
| 201 | `ice` | 9 | 13 | 90 | 202 |  |
| 202 | `ice` | 8 | 15 | 1 | 182 |  |
| 203 | `fire` | 37 | 18 | 1 | 204 |  |
| 204 | `fire` | 38 | 18 | 1 | 203 |  |
| 205 | `fire` | 67 | 18 | 1 | 206 |  |
| 206 | `fire` | 68 | 18 | 1 | 205 |  |
| 207 | `tired` | 69 | 15 | 2 | 0 |  |

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

### Inputs mapeados (`hit_*`) encontrados
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
| 105 | `rowing` | `hit_Fa` | 260 |
| 105 | `rowing` | `hit_j` | 106 |
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
| 262 | `ball` | `hit_d` | 276 |
| 262 | `ball` | `hit_j` | 276 |
| 266 | `ball` | `hit_d` | 276 |
| 266 | `ball` | `hit_j` | 276 |
| 269 | `ball` | `hit_d` | 276 |
| 269 | `ball` | `hit_j` | 276 |
| 272 | `ball` | `hit_d` | 276 |
| 272 | `ball` | `hit_j` | 276 |
| 275 | `ball` | `hit_Fa` | 263 |
| 275 | `ball` | `hit_a` | 263 |
| 275 | `ball` | `hit_d` | 276 |
| 275 | `ball` | `hit_j` | 276 |

### Frames (visão rápida)
> Para manter o arquivo legível, aqui vai uma visão rápida. Se você quiser, eu gero uma versão ‘ultra detalhada’ por personagem com todos os frames em tabela gigante.

| id | nome | pic | state | wait | next | opoint oid (se houver) |
|---:|---|---:|---:|---:|---:|---|
| 0 | `standing` | 0 | 0 | 10 | 1 |  |
| 1 | `standing` | 1 | 0 | 4 | 2 |  |
| 2 | `standing` | 2 | 0 | 3 | 3 |  |
| 3 | `standing` | 3 | 0 | 8 | 4 |  |
| 4 | `standing` | 2 | 0 | 2 | 999 |  |
| 5 | `walking` | 4 | 1 | 3 | 999 |  |
| 6 | `walking` | 5 | 1 | 3 | 999 |  |
| 7 | `walking` | 6 | 1 | 3 | 999 |  |
| 8 | `walking` | 7 | 1 | 3 | 999 |  |
| 9 | `running` | 26 | 2 | 3 | 0 |  |
| 10 | `running` | 27 | 2 | 3 | 0 |  |
| 11 | `running` | 28 | 2 | 3 | 0 |  |
| 12 | `heavy_obj_walk` | 30 | 1 | 3 | 0 |  |
| 13 | `heavy_obj_walk` | 31 | 1 | 3 | 0 |  |
| 14 | `heavy_obj_walk` | 32 | 1 | 3 | 0 |  |
| 15 | `heavy_obj_walk` | 33 | 1 | 3 | 0 |  |
| 16 | `heavy_obj_run` | 34 | 2 | 3 | 0 |  |
| 17 | `heavy_obj_run` | 35 | 2 | 3 | 0 |  |
| 18 | `heavy_obj_run` | 36 | 2 | 3 | 0 |  |
| 19 | `heavy_stop_run` | 41 | 15 | 7 | 999 |  |
| 20 | `normal_weapon_atck` | 37 | 15 | 2 | 21 |  |
| 21 | `normal_weapon_atck` | 38 | 15 | 2 | 999 |  |
| 25 | `normal_weapon_atck` | 37 | 15 | 2 | 21 |  |
| 30 | `jump_weapon_atck` | 37 | 3 | 1 | 31 |  |
| 31 | `jump_weapon_atck` | 39 | 3 | 1 | 32 |  |
| 32 | `jump_weapon_atck` | 39 | 3 | 4 | 999 |  |
| 35 | `run_weapon_atck` | 37 | 15 | 3 | 36 |  |
| 36 | `run_weapon_atck` | 38 | 15 | 3 | 999 |  |
| 40 | `dash_weapon_atck` | 37 | 3 | 1 | 41 |  |
| 41 | `dash_weapon_atck` | 39 | 3 | 1 | 42 |  |
| 42 | `dash_weapon_atck` | 39 | 3 | 4 | 999 |  |
| 45 | `light_weapon_thw` | 37 | 15 | 3 | 46 |  |
| 46 | `light_weapon_thw` | 38 | 15 | 3 | 999 |  |
| 50 | `heavy_weapon_thw` | 37 | 15 | 3 | 51 |  |
| 51 | `heavy_weapon_thw` | 38 | 15 | 3 | 999 |  |
| 52 | `sky_lgt_wp_thw` | 37 | 15 | 3 | 53 |  |
| 53 | `sky_lgt_wp_thw` | 39 | 15 | 1 | 54 |  |
| 54 | `sky_lgt_wp_thw` | 39 | 15 | 9 | 999 |  |
| 55 | `weapon_drink` | 46 | 17 | 3 | 56 |  |
| 56 | `weapon_drink` | 47 | 17 | 3 | 57 |  |
| 57 | `weapon_drink` | 48 | 17 | 3 | 58 |  |
| 58 | `weapon_drink` | 47 | 17 | 3 | 55 |  |
| 60 | `punch` | 10 | 3 | 2 | 61 |  |
| 61 | `punch` | 11 | 3 | 1 | 999 |  |
| 65 | `punch` | 12 | 3 | 2 | 66 |  |
| 66 | `punch` | 13 | 3 | 1 | 999 |  |
| 70 | `super` | 15 | 3 | 1 | 71 |  |
| 71 | `super` | 16 | 3 | 1 | 72 |  |
| 72 | `super` | 17 | 3 | 1 | 73 |  |
| 73 | `super` | 18 | 3 | 1 | 74 |  |
| 74 | `super` | 19 | 3 | 1 | 75 |  |
| 75 | `super` | 29 | 3 | 4 | 76 |  |
| 76 | `super` | 10 | 3 | 2 | 999 |  |
| 79 | `singlong` | 25 | 3 | 9 | 0 |  |
| 80 | `jump_attack` | 76 | 3 | 3 | 81 |  |
| 81 | `jump_attack` | 50 | 3 | 20 | 999 |  |
| 85 | `run_attack` | 51 | 3 | 4 | 300 |  |
| 86 | `singlong` | 21 | 3 | 1 | 87 |  |
| 87 | `singlong` | 22 | 3 | 1 | 88 |  |
| 88 | `singlong` | 23 | 3 | 2 | 89 |  |
| 89 | `singlong` | 24 | 3 | 2 | 79 |  |
| 90 | `dash_attack` | 76 | 3 | 3 | 81 |  |
| 91 | `dash_attack` | 50 | 3 | 20 | 999 |  |
| 100 | `rowing` | 55 | 6 | 2 | 101 |  |
| 101 | `rowing` | 39 | 6 | 6 | 0 |  |
| 102 | `rowing` | 78 | 6 | 1 | 103 |  |
| 103 | `rowing` | 49 | 6 | 10 | 104 | 52 |
| 104 | `rowing` | 77 | 6 | 3 | 105 |  |
| 105 | `rowing` | 78 | 6 | 4 | 999 |  |
| 106 | `rowing` | 78 | 6 | 1 | 103 |  |
| 108 | `rowing` | 54 | 6 | 3 | 109 |  |
| 109 | `rowing` | 67 | 6 | 6 | 0 |  |
| 110 | `defend` | 42 | 7 | 12 | 999 |  |
| 111 | `defend` | 42 | 7 | 12 | 999 |  |
| 112 | `broken_defend` | 66 | 8 | 1 | 113 |  |
| 113 | `broken_defend` | 67 | 8 | 2 | 114 |  |
| 114 | `broken_defend` | 69 | 8 | 3 | 999 |  |
| 115 | `picking_light` | 14 | 15 | 4 | 999 |  |
| 116 | `picking_heavy` | 14 | 15 | 1 | 117 |  |
| 117 | `picking_heavy` | 14 | 15 | 1 | 999 |  |
| 120 | `catching` | 44 | 9 | 2 | 121 |  |
| 121 | `catching` | 43 | 9 | 0 | 0 |  |
| 122 | `catching` | 44 | 9 | 3 | 123 |  |
| 123 | `catching` | 45 | 9 | 3 | 121 |  |
| 130 | `picked_caught` | 68 | 10 | 3 | 0 |  |
| 131 | `picked_caught` | 69 | 10 | 3 | 0 |  |
| 132 | `picked_caught` | 69 | 10 | 3 | 0 |  |
| 133 | `picked_caught` | 64 | 10 | 3 | 0 |  |
| 134 | `picked_caught` | 65 | 10 | 3 | 0 |  |
| 135 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 136 | `picked_caught` | 55 | 10 | 3 | 0 |  |
| 137 | `picked_caught` | 56 | 10 | 3 | 0 |  |
| 138 | `picked_caught` | 54 | 10 | 3 | 0 |  |
| 139 | `picked_caught` | 74 | 10 | 3 | 0 |  |
| 140 | `picked_caught` | 75 | 10 | 3 | 0 |  |
| 141 | `picked_caught` | 57 | 10 | 3 | 0 |  |
| 142 | `picked_caught` | 58 | 10 | 3 | 0 |  |
| 143 | `picked_caught` | 59 | 10 | 3 | 0 |  |
| 144 | `picked_caught` | 58 | 10 | 3 | 0 |  |
| 180 | `falling` | 64 | 12 | 3 | 0 |  |
| 181 | `falling` | 65 | 12 | 3 | 0 |  |
| 182 | `falling` | 54 | 12 | 3 | 0 |  |
| 183 | `falling` | 55 | 12 | 3 | 0 |  |
| 184 | `falling` | 56 | 12 | 3 | 0 |  |
| 185 | `falling` | 54 | 12 | 3 | 0 |  |
| 186 | `falling` | 74 | 12 | 3 | 0 |  |
| 187 | `falling` | 75 | 12 | 3 | 0 |  |
| 188 | `falling` | 57 | 12 | 3 | 0 |  |
| 189 | `falling` | 58 | 12 | 3 | 0 |  |
| 190 | `falling` | 59 | 12 | 3 | 0 |  |
| 191 | `falling` | 57 | 12 | 3 | 0 |  |
| 200 | `ice` | 8 | 15 | 2 | 201 |  |
| 201 | `ice` | 9 | 13 | 90 | 202 |  |
| 202 | `ice` | 8 | 15 | 1 | 182 |  |
| 203 | `fire` | 61 | 18 | 1 | 204 |  |
| 204 | `fire` | 60 | 18 | 1 | 203 |  |
| 205 | `fire` | 61 | 18 | 1 | 206 |  |
| 206 | `fire` | 60 | 18 | 1 | 205 |  |
| 207 | `tired` | 69 | 15 | 2 | 0 |  |
| 210 | `jump` | 14 | 4 | 2 | 211 |  |

---
