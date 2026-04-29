#!/usr/bin/env bash
# format-changed.sh — Formata automaticamente arquivos alterados após edições
# Hook: PostToolUse (matcher: Edit|Write)
#
# Este script recebe o path do arquivo alterado e aplica formatação apropriada.
# Adaptado para o projeto Unity (C#, JSON, Markdown).

set -euo pipefail

FILE_PATH="${1:-}"

if [ -z "$FILE_PATH" ]; then
  exit 0
fi

# Só formatar se o arquivo existe
if [ ! -f "$FILE_PATH" ]; then
  exit 0
fi

# Formatação por tipo de arquivo
case "$FILE_PATH" in
  *.cs)
    # C# — formatar com dotnet-format se disponível
    if command -v dotnet-format &> /dev/null; then
      dotnet-format "$FILE_PATH" --verbosity quiet 2>/dev/null || true
    fi
    ;;
  *.json)
    # JSON — validar se é JSON válido
    if command -v python3 &> /dev/null; then
      python3 -c "import json; json.load(open('$FILE_PATH'))" 2>/dev/null || true
    fi
    ;;
  *.md)
    # Markdown — garantir newline no final
    if [ "$(tail -c 1 "$FILE_PATH" | wc -l)" -eq 0 ]; then
      echo "" >> "$FILE_PATH"
    fi
    ;;
  *.sh)
    # Shell — garantir newline no final
    if [ "$(tail -c 1 "$FILE_PATH" | wc -l)" -eq 0 ]; then
      echo "" >> "$FILE_PATH"
    fi
    ;;
esac

exit 0
