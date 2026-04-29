#!/usr/bin/env bash
# block-dangerous.sh — Bloqueia comandos destrutivos antes da execução
# Hook: PreToolUse (matcher: Bash)
#
# Este script recebe o comando Bash como argumento e deve:
# - Sair com código 0 (permitir) se o comando é seguro
# - Sair com código 1 (bloquear) e mensagem de erro se o comando é destrutivo

set -euo pipefail

COMMAND="${1:-}"

# Lista de padrões destrutivos (regex)
DESTRUCTIVE_PATTERNS=(
  "rm -rf /"
  "rm -rf ~"
  "rm -rf \*"
  "rm -rf \."
  "mkfs\."
  "dd if=.* of=/dev/"
  ":(){ :\|:& };:"
  "chmod -R 777 /"
  "chmod -R 000 /"
  "chown -R .* /"
  "> /dev/sda"
  "shutdown"
  "reboot"
  "halt"
  "poweroff"
  "init 0"
  "init 6"
  "systemctl stop"
  "systemctl disable"
  "kill -9 1"
  "killall -9"
  "pkill -9 -u root"
  "git push --force origin main"
  "git push --force origin master"
  "git reset --hard"
  "git clean -fdx"
  "npm publish"
  "pip install --break-system-packages"
  "sudo rm"
  "sudo chmod"
  "sudo chown"
  "sudo dd"
  "sudo mkfs"
)

for pattern in "${DESTRUCTIVE_PATTERNS[@]}"; do
  if echo "$COMMAND" | grep -qE "$pattern"; then
    echo "ERRO: Comando bloqueado pelo hook block-dangerous.sh" >&2
    echo "Padrão destrutivo detectado: $pattern" >&2
    echo "Comando: $COMMAND" >&2
    exit 1
  fi
done

exit 0
