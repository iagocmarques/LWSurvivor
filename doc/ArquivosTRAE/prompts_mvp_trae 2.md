
---

## PROMPT MESTRE — Importador LF2 → Unity (cole antes de tudo)
> Você vai implementar um importador **Editor-only** no Unity 6 para importar personagens do LF2 (do meu pacote), conforme a spec `spec_importador_lf2_para_unity.md`.  
> Regras:
> 1) Sempre entregar **arquivos C# completos** + caminho em `Assets/_Project/Tools/LF2Importer/...`.  
> 2) Sempre terminar com **Critérios de aceitação** + **Como testar**.  
> 3) Não usar pacotes externos obrigatórios.  
> 4) O importador deve suportar `.dat` criptografado (header 123 + key 37).  
> 5) O importador deve gerar assets em `Assets/_Imported/LF2/...` e nunca mexer em `_Project`.  
> 6) Implementar com tolerância a erros: logar warnings, não crashar.
> Comece pelo Prompt 0.

---

# Prompt 0 — Estrutura do plugin + menu no Unity
> Crie a estrutura base do importador:
> - Pastas e namespaces `LF2Importer`
> - Menu `Tools/LF2/Import Character...` e `Tools/LF2/Import All Characters (data.txt)...`
> - Um `Lf2ImportSettings` (ScriptableObject) com: rootPath (pasta do LF2), outputPath, timeUnit (default 1/30), flags (importSprites/importClips/importData).  
> Não implemente import ainda, só o esqueleto + UI de seleção de pastas.
> Entregue código e passo-a-passo de como testar que o menu aparece.

---

# Prompt 1 — Decriptação `.dat` + heurística plaintext
> Implemente `Lf2DatDecryptor`:
> - `bool LooksPlaintext(byte[] headBytes)` (procura `<bmp_begin>`/`<frame>`)
> - `string ReadDatAsText(string path)` que decide plaintext vs decrypt
> - decrypt: ignorar 123 bytes, aplicar `(b - key[i%37]) mod 256`, decode latin-1, normalizar \n
> - Inclua um método de teste Editor: selecionar um `.dat` e imprimir as primeiras 40 linhas no Console.
> Critérios: consigo ler `dennis.dat` e ver `<bmp_begin>` e `<frame>` no output.

---

# Prompt 2 — Parser do `data.txt` (id → type/file)
> Implemente `Lf2DataTxtParser`:
> - Ler `data/data.txt` do root
> - Retornar lista/dicionário de objetos com `id`, `type`, `file`
> - Método util: `IEnumerable<Lf2Object> GetCharacters()` para `type==0`
> - Criar uma janela simples que lista personagens (id + file) e permite selecionar um para importar depois.
> Critérios: lista mostra ids 1.. etc e inclui `dennis.dat`, `firen.dat` etc.

---

# Prompt 3 — Parser do `.dat` (bmp_begin + frames)
> Implemente `Lf2DatParser` (MVP):
> - Extrair `name`, `head`, `small`
> - Extrair todos `file(start-end): ... w/h/row/col`
> - Extrair movement params (walking_speed etc) após `<bmp_end>`
> - Extrair frames `<frame> ... <frame_end>` com:
>   - props (pic/state/wait/next/dvx/dvy/dvz/mp/sound/hit_*)
>   - blocos `bdy`, `itr` (múltiplos), `opoint` (múltiplos)
> - Logar warnings se `next` aponta para frame inexistente
> Critérios: parse de `dennis.dat` retorna contagem de frames > 300 e lista alguns `hit_*`.

---

# Prompt 4 — Resolver `opoint.oid` usando `data.txt`
> Implemente `Lf2OidResolver`:
> - Dado `oid`, retornar `type` e `file` do `data.txt`
> - Na exportação do parse, incluir `ResolvedOidFile` quando existir
> Critérios: frames com `opoint` mostram `oid` resolvido (ex.: `firen_ball.dat`, etc).

---

# Prompt 5 — Importação de sprites: BMP slicing → Sprites Unity
> Implemente `Lf2BmpSlicer`:
> - Para cada bmp entry: carregar textura e criar sprites por range
> - Nomear sprite como `{characterName}_{pic:0000}`
> - Salvar em `Assets/_Imported/LF2/Characters/{id}_{name}/Sprites/`
> Observação: por enquanto aceite fundo sem alpha (sem remover colorkey); só gerar os sprites.
> Critérios: vejo os sprites gerados no Project e consigo arrastar um para a Scene.

---

# Prompt 6 — Builder de AnimationClips (standing/walk/run)
> Implemente `Lf2ClipBuilder`:
> - Gerar pelo menos 3 clips: standing, walking, running
> - Heurística: achar frames por `state` (0/1/2) e seguir `next` até loop/999
> - Tempo: `wait * timeUnitSeconds`
> - Salvar em `.../Animations/`
> Critérios: consigo dar play no clip e ver troca de sprites.

---

# Prompt 7 — Gerar ScriptableObjects (CharacterDefinition/MoveDefinition)
> Crie SOs mínimos no seu formato:
> - `ImportedCharacterDefinition` (id, name, movement params, lista de moves)
> - `ImportedMoveDefinition` (startFrameId, name, frameSequence, bindings hit_*)
> - `ImportedFrameData` (props + hitboxes/hurtboxes/spawns)
> Salvar em `.../Data/`
> Critérios: consigo abrir o SO e ver os dados preenchidos.

---

# Prompt 8 — Importar “todos os personagens” (batch)
> Implementar `Import All Characters`:
> - Lê data.txt, pega type==0, importa em batch
> - Barra de progresso (EditorUtility.DisplayProgressBar)
> - Não falhar geral por 1 arquivo: registrar erro e continuar
> Critérios: gera pasta para todos (ou quase todos) personagens.

---

# Prompt 9 — Validador + relatório final (para você não se perder)
> Implementar um relatório `.md` gerado após import:
> - Lista personagens importados, warnings por personagem (pic fora de range, next inexistente, oid não resolvido)
> - Caminho do output
> Critérios: ao final do batch, tenho um relatório legível.

---
