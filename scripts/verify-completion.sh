#!/usr/bin/env bash
# verify-completion.sh — Verificação de close-out / Stop hook
# Hook: Stop
#
# Este script é chamado quando a sessão está prestes a encerrar.
# Ele verifica se os artefatos de completion foram produzidos.
#
# Fase 1: DoD summary table + Files Changed table
# Fase 2: Orchestration self-review com wording canônica de fallback
#
# NOTA: No Claude Code, este script é informativo. O Stop hook pode
# imprimir mensagens que o agente deve considerar antes de finalizar.
# A enforcement real depende do agente seguir as instruções.

set -euo pipefail

cat << 'VERIFY_EOF'
=== STOP HOOK: Verificação de Completion ===

Antes de finalizar, verifique:

FASE 1 — Artefatos obrigatórios:
- [ ] Tabela de resumo DoD (Definition of Done) foi produzida?
- [ ] Tabela de Files Changed foi produzida?

FASE 2 — Auto-auditoria de orquestração:
Verifique cada princípio na ordem canônica:
1. Ser confiável
2. Ser o mais rápido possível na implementação (paralelização)
3. Realizar auto-auditoria
4. Rodar testes confiáveis em cada iteração
5. Escrever DoD com evidências e relatórios
6. Ser autônomo
7. Ser custo-efetivo

Para cada princípio, se nenhuma melhoria foi identificada, use EXATAMENTE:
"Nenhuma melhoria identificada para este princípio."

NÃO parafraseie esta wording.

=== FIM DA VERIFICAÇÃO ===
VERIFY_EOF
