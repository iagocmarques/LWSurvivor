#!/usr/bin/env bash
# inject-context.sh — Injeta contexto do projeto no início da sessão
# Pode ser chamado manualmente ou via hook de configuração.
#
# Este script imprime contexto relevante do projeto que deve estar
# disponível para todos os agentes durante a sessão.

set -euo pipefail

PROJECT_ROOT="$(cd "$(dirname "$0")/.." && pwd)"

cat << 'CONTEXT_EOF'
## Contexto do Projeto (auto-injetado)

**Projeto:** LF2 + Survivors
**Engine:** Unity 6 + URP 2D
**Stack:** C#, ScriptableObjects, Netcode (host-client Steam-first)
**Tipo:** Beat 'em up 2.5D com mecânicas de Vampire Survivors

### Metas de Performance
- 60 FPS padrão
- Tick de simulação: 60Hz fixo
- Pico: 200-500 entidades

### Convenções
- Assembly definitions para separação de módulos
- ScriptableObjects como fonte de verdade para dados de gameplay
- Object pooling para entidades frequentes
- FSM hierárquica para estados de player (Idle/Move/Dash/Attack/Hitstun/Dead)
- 2.5D: movimento em X/Y onde Y = profundidade, ordenação visual por Y

### MCP Server
- Usar apenas `user-unity-mcp` (não `user-ai-game-developer`)
- Tools disponíveis: Assets, Core, Debug, Editor, Profiler
CONTEXT_EOF
