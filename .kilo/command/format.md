---
description: Formata automaticamente arquivos alterados após edições
---

## Auto-Format

Formate os arquivos alterados pela tarefa atual.

### Regras de Formatação

- **C# (`.cs`)**: rodar `dotnet-format` se disponível, senão apenas validar sintaxe.
- **JSON (`.json`)**: validar se é JSON válido.
- **Markdown (`.md`)**: garantir newline no final do arquivo.
- **Shell (`.sh`)**: garantir newline no final e permissão de execução.

### Execução

Se `scripts/format-changed.sh` existir, invocá-lo para cada arquivo alterado:

```bash
bash scripts/format-changed.sh "<caminho-do-arquivo>"
```

Se o script não existir, aplicar formatação manual conforme as regras acima.
