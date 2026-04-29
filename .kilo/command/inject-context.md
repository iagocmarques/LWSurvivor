---
description: Injeta contexto do projeto no início da sessão
---

## Context Injection

Injete o contexto canônico do projeto para todos os agentes na sessão.

### Execução

Se `scripts/inject-context.sh` existir, invocá-lo:

```bash
bash scripts/inject-context.sh
```

Caso contrário, referencie as seguintes informações:

### Contexto do Projeto

- **Projeto:** LF2 + Survivors
- **Engine:** Unity 6 + URP 2D
- **Stack:** C#, ScriptableObjects, Netcode (host-client Steam-first)
- **Tipo:** Beat 'em up 2.5D com mecânicas de Vampire Survivors

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
