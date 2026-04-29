# CLAUDE.md — Arquitetura de Agentes Multi-Role para Claude Code

> Projeto: LF2 + Survivors (Beat 'em up 2.5D / Vampire Survivors hybrid)
> Engine: Unity 6 + URP 2D | Netcode: Host-client Steam-first
> Gerado a partir do blueprint `agent-architecture-template.md`, adaptado para Claude Code.

---

## 1. Propósito

Este documento define:

- a topologia canônica de orquestração multi-agente para Claude Code
- a hierarquia de regras e ordem de verdade-fonte
- os limites de papel (role boundaries) de cada agente
- o ciclo de vida: planejamento → dispatch → verificação → documentação
- o contrato de hooks-driven close-out
- o modelo de memória duradoura
- a validação e checks de drift pós-geração

---

## 2. Regras de Consumo para o Claude Code

Ao usar esta arquitetura, siga estas regras exatamente:

1. Seja maximalmente orientado a detalhes. Não colapse contratos importantes em resumos vagos.
2. Preserve a forma da arquitetura mesmo se nomes de stack, ferramentas ou modelos mudarem.
3. Separe regras genéricas de orquestração de regras específicas do projeto. Não as misture.
4. Planeje e execute atomicamente, seção por seção. Não pule e volte para preencher depois.
5. Trate cada seção deste documento como um deliverable. Se uma seção não for aplicável, diga por quê explicitamente em vez de omitir.
6. Não invente capabilities não suportadas pela plataforma. Se uma capability depende do ambiente, documente como environment-dependent.
7. Use a documentação oficial do Claude Code ao decidir features, hooks, subagent behavior, model options, MCP/tool-server support ou workspace capabilities.
8. Ao final do processo, forneça ao usuário uma lista dedicada de configurações opcionais que ele pode ativar.
9. Mantenha boilerplate de completion curto nos arquivos de agentes especializados. Preserve regras de role; comprima apenas frases repetitivas de close-out.
10. Não crie nenhuma superfície de memória duplicada fora do sistema de memória do Claude Code.
11. Finalize o processo com uma TODO list cobrindo todos os pontos de atenção restantes.
12. O último item da TODO deve exigir explicitamente um audit comparando a arquitetura gerada contra este blueprint, listando todos os desvios.

---

## 3. O Que Deve Ser Genérico Vs O Que Deve Ser Projeto-Específico

| Camada | Genérico no blueprint | Projeto-específico ao instanciar |
| --- | --- | --- |
| Topologia | Orchestrator → Planner → specialists → verification → documentation | nomes exatos dos specialists se o repo não for web/UI |
| Regras | routing, verificação, fases stop-hook, preflight, política de memória, contrato de paralelização | regras de UI do produto, caveats de test, quirks de build, políticas de domínio |
| Arquivos | layout de pastas, layout de hooks, estrutura de arquivos de agente, estrutura da pasta de memória | nomes de repo, arquivos de stack, corpos concretos de scripts |
| Modelos | estratégia de seleção por role | nomes exatos de modelos atuais e fallbacks |
| Ferramentas | categorias de tools por role | identificadores exatos de tools disponíveis no ambiente |
| MCP / tool servers | framework de decisão e opcionalidade | servers exatos, estratégia de secrets, integrações específicas |
| Política local-only | se existe boundary local-only | paths exatos e se a arquitetura é commitada ou excluída |

Regra: se um fato só seria verdadeiro para um projeto, ele pertence ao overlay projeto-específico do repo gerado, não no contrato genérico de arquitetura.

---

## 4. Topologia Canônica

Topologia padrão de roles:

```text
Usuário
  └─▶ Workflow Orchestrator          (entry point, nunca implementa)
        └─▶ Architect/Planner        (decomposição de tarefas, DoD, design de paralelização)
              ├─▶ Expert Unity Developer     (implementação)
              ├─▶ Game Designer              (game feel, balanceamento, UX de gameplay)
              ├─▶ QA                         (testes, verificação de qualidade)
              ├─▶ Security Reviewer          (verificação de segurança)
              ├─▶ Performance Analyst        (profiling, otimização)
              └─▶ Documentation Agent        (sempre por último)
```

### 4.1 Regras de Topologia

- `Workflow Orchestrator` é o único entry point que fala com o usuário.
- `Architect/Planner` roda antes de qualquer implementação não-trivial.
- Specialists fazem trabalho de domínio.
- Verificação é independente da implementação.
- `Documentation Agent` é sempre a última wave.

### 4.2 Adaptação Para Este Projeto (Unity/LF2)

- `Expert Unity Developer` cobre implementação em C#, ScriptableObjects, shaders, animações.
- `Game Designer` cobre game feel (hit stop, screen shake, input buffering), balanceamento de stats, UX de combate.
- `Performance Analyst` cobre profiling via Unity Profiler MCP, object pooling, otimização de entidades.
- `QA` cobre testes unitários (Unity Test Runner), testes de integração, smoke tests.
- `Security Reviewer` cobre auditoria de código, dependências, CVEs.
- `Documentation Agent` cobre memória duradoura do projeto, atualização de docs compartilhadas.

Se o repo não tiver UI visual (não é o caso deste projeto):
- mantenha a forma de orquestração
- renomeie specialists específicos de stack conforme necessário
- preserve limites de role
- preserve o contrato de paralelização do planner/orchestrator
- preserve a wave de verificação independente
- preserve a wave final de documentação

---

## 5. Ordem Canônica de Verdade-Fonte

A arquitetura deve tornar esta hierarquia explícita:

| Prioridade | Artefato | Propósito |
| --- | --- | --- |
| 1 | `CLAUDE.md` | contrato canônico de arquitetura e regras cross-role |
| 2 | `.claude/agents/*.md` | contratos role-locais, tools, boundaries, preflight, regras de completion |
| 3 | `.claude/settings.json` (hooks) + `scripts/*.sh` | enforcement executável em runtime |
| 4 | `CLAUDE.md` (seções de instruções compartilhadas) | fatas estáveis do repo e normas de código compartilhadas |
| 5 | `CLAUDE.md` (seções path-scoped ou domain-scoped) | regras de implementação por path ou domínio |
| 6 | Sistema de memória do Claude Code (`~/.claude/projects/.../memory/`) | memória duradoura de estado atual do projeto |
| 7 | `doc/orchestration-suggestions.md` | melhorias de orquestração diferidas |
| 8 | `.claude/skills/` | capability packs reutilizáveis opcionais |

### Regra de conflito:

- runtime enforcement vence sobre prose quando um hook bloqueia execução
- `CLAUDE.md` vence sobre documentação secundária quando prose conflita
- arquivos role-locais de agentes refinam, mas não enfraquecem, o contrato raiz de arquitetura
- memória armazena verdade atual apenas; não sobrescreve contratos de arquitetura

---

## 6. Blueprint de Filesystem

Crie esta estrutura, ajustando apenas nomes que são genuinamente específicos de stack:

```text
<repo-root>/
├─ CLAUDE.md                              # contrato canônico de arquitetura
├─ .claude/
│  ├─ settings.json                        # hooks, permissões, MCP servers
│  └─ agents/
│     ├─ workflow-orchestrator.md           # entry point de orquestração
│     ├─ architect-planner.md              # planejamento e decomposição
│     ├─ expert-unity-developer.md         # implementação Unity/C#
│     ├─ game-designer.md                  # game feel e balanceamento
│     ├─ qa-subagent.md                    # testes e verificação
│     ├─ security-reviewer.md              # auditoria de segurança
│     ├─ performance-analyst.md            # profiling e otimização
│     └─ documentation-agent.md            # documentação e memória
├─ scripts/
│  ├─ inject-context.sh                    # injeção de contexto no session start
│  ├─ format-changed.sh                    # formatação automática de arquivos alterados
│  ├─ block-dangerous.sh                   # bloqueio de comandos destrutivos
│  └─ verify-completion.sh                 # verificação de close-out
├─ doc/
│  ├─ orchestration-suggestions.md         # melhorias diferidas
│  └─ temp/
│     └─ agent-architecture-template.md    # blueprint original
├─ .vscode/
│  └─ settings.json                        # configurações do VS Code (se necessário)
```

### 6.1 O Que NÃO Criar

- nenhum outro arquivo de memória fora do sistema de memória do Claude Code
- cópias duplicadas ocultas do contrato de arquitetura em arquivos markdown aleatórios
- arquivos `.github/agents/` (formato Copilot — não aplicável ao Claude Code)

---

## 7. Estrutura Obrigatória do `CLAUDE.md` Raiz

O `CLAUDE.md` gerado deve conter, nesta ordem:

1. Título e identificador curto do repositório
2. Diagrama de arquitetura de alto nível
3. Princípios Core
4. Regras Específicas do Projeto
5. Tabela de Seleção de Modelos
6. Tabela de capabilities de plataforma e enforcement
7. Fases de close-out
8. Matriz hard-enforced vs audit-only
9. Guia de decisão MCP ou tool-server
10. Política de Git / worktree
11. Política de boundary local-only, se usada
12. Política de warning de build/lint, se relevante

### Por que essa ordem importa:

- a primeira metade estabelece o contrato operacional
- a segunda metade descreve enforcement de runtime e uso de ambiente sem espalhar os mesmos fatos de hook/capability em múltiplas seções
- regras específicas do projeto ficam visivelmente separadas da arquitetura genérica

---

## 8. Contrato Core de Arquitetura

O `CLAUDE.md` gerado deve representar estas regras explicitamente.

### 8.1 Regras Fundamentais

1. Toda tarefa multi-passo passa pelo `Workflow Orchestrator` primeiro.
2. `Workflow Orchestrator` roteia cada request para um agente dedicado.
3. `Workflow Orchestrator` é a fonte de verdade entre o request do usuário e o trabalho entregue.
4. O Orchestrator pede clarificação antes de delegar se a request for materialmente ambígua ou incompleta.
5. `Architect/Planner` roda antes da implementação em qualquer tarefa não-trivial.
6. O Orchestrator delega através da ferramenta `Agent` para o specialist apropriado.
7. Subtarefas independentes são despachadas em paralelo dentro da mesma wave.
8. Qualquer mudança de código dispara uma wave de verificação independente, no mínimo `QA` plus `Security Reviewer`.
9. Mudanças de UI adicionam auditoria visual/acessibilidade plus verificação browser/E2E quando relevante.
10. O Orchestrator nunca confia em auto-relatórios como prova de completion.
11. `Documentation Agent` é sempre a última wave.
12. O Orchestrator é o único roteador cross-role.
13. Specialists elegíveis podem auto-delegar apenas dentro de seu próprio role ou escopo herdado de worker.
14. Simplicidade vence sobre over-engineering.
15. Todos os agentes mantêm progress tracking via `TodoWrite`.
16. Todos os agentes conhecem todos os outros agentes através de um Agent Registry.
17. Lookup de documentação oficial é obrigatório para planejamento, implementação/refator grande, e auditorias/reviews envolvendo contratos de framework ou library.
18. Mudanças de arquitetura requerem aprovação explícita do usuário.
19. Corrija causas-raiz, não sintomas.
20. Preserve single sources of truth para valores e contratos reutilizáveis.
21. Prefira abstrações existentes antes de criar novas.
22. Extraia lógica reutilizável apenas quando justificado.
23. O Planner usa `AskUserQuestion` para trade-offs materiais ou blockers que só o usuário pode resolver.
24. O Planner lista trabalho adjacente como follow-up opcional, não como escopo assumido.
25. O Planner pode propor uma abordagem melhor, mas a execução segue a decisão do usuário se ele recusar.
26. Loops de verificação são limitados a três iterações fix → verify.
27. QA prioriza falhas bloqueantes antes de polish menor.
28. A auto-auditoria de orquestração segue uma ordem canônica de prioridade.
29. Artefatos de close-out são produzidos apenas quando o Stop hook os solicita; não emita a tabela de resumo DoD, tabela de Files Changed, ou self-review de orquestração proativamente.
30. Agentes reutilizam terminais abertos quando o ambiente expõe reutilização estável de terminal.
31. Agentes não re-perguntam decisões já respondidas através de `AskUserQuestion` durante a mesma sessão.
32. O Planner é o owner primário do design de paralelização.
33. O Orchestrator é o owner primário da execução de paralelização.
34. Specialists ainda avaliam oportunidades adicionais de fan-out durante a execução.
35. Mudanças em arquivos de orquestração requerem uma passagem de consistência independente.
36. Agentes verificam o working directory antes de comandos de terminal.
37. A wording de fallback da auto-auditoria é exata e canônica.
38. Todo specialist deve emitir um preflight estruturado antes do trabalho substantivo.
39. O reporting do Orchestrator para o usuário deve ser direto e conciso.

---

## 9. Contrato de Paralelização

Esta é a regra de coordenação mais importante após o routing.

### 9.1 Responsabilidades do Planner

- dividir trabalho em subtarefas atômicas e paralelizáveis
- especificar boundaries de wave
- especificar exatamente quais subtarefas rodam em paralelo
- especificar quando múltiplas instâncias do mesmo agente devem ser lançadas
- especificar fan-out de verificação por lane quando possível
- especificar fan-out de fallback se uma wave provavelmente retornará muitas falhas independentes

### 9.2 Responsabilidades do Orchestrator

- tratar as instruções de paralelização do Planner como requisitos de execução
- lançar todas as subtarefas independentes da mesma wave juntas
- maximizar fan-out seguro de mesmo role em vez de serializar trabalho independente
- preferir o maior número útil de instâncias de mesmo role quando o trabalho é separável
- re-fan-out verificação ou fixes se falhas se agruparem em concerns independentes

### 9.3 Responsabilidades do Specialist

- avaliar se mais fan-out de mesmo role se tornou possível após o trabalho ter começado
- auto-delegar dentro do role quando o ambiente permite e o speedup é material
- caso contrário, reportar uma recomendação concreta de fan-out de volta ao Orchestrator imediatamente

### 9.4 Exemplos

- uma auditoria de testes pode spawnar instâncias separadas de `QA` para unit, integration, smoke e E2E-adjacent
- uma wave de verificação de browser pode dividir o trabalho de `Playwright Tester` por fluxos independentes
- se dez falhas retornam e elas se separam em buckets independentes, não as serialize por padrão; divida-as em workers ou peça ao Orchestrator para fazê-lo

### 9.5 Tool Parallelism Vs Worker Subagents

Os docs gerados devem explicar a diferença claramente:

- reads/searches/web fetches em batch em um turno são tool parallelism dentro de uma sessão de agente
- worker subagents são execuções separadas de mesmo role criadas para isolamento ou velocidade
- uso de worker de mesmo role é permitido apenas se o ambiente ativo suporta
- uso de worker de mesmo role nunca concede autoridade de roteamento cross-role

**No Claude Code:**
- Tool parallelism = múltiplas chamadas de ferramenta na mesma mensagem (Read, Grep, Glob em paralelo)
- Worker subagents = usar `Agent` tool com o mesmo `subagent_type` múltiplas vezes com `run_in_background: true`
- Claude Code suporta ambos nativamente

---

## 10. Ordem de Auto-Auditoria de Orquestração

A arquitetura deve manter esta ordem canônica:

1. ser confiável
2. ser o mais rápido possível na implementação através de boa paralelização
3. realizar auto-auditoria
4. rodar testes confiáveis em cada iteração
5. escrever DoD com evidências e relatórios
6. ser autônomo
7. ser custo-efetivo

Para cada princípio, o fallback quando nenhuma issue é encontrada deve ser exatamente:

`Nenhuma melhoria identificada para este princípio.`

Não parafraseie o fallback.

---

## 11. Matriz de Ownership de Regras

Torne o ownership de regras explícito para que drift futuro seja fácil de detectar.

| Área de regra | Owner primário | Owner secundário | Owner de runtime |
| --- | --- | --- | --- |
| Roteamento cross-role | `Workflow Orchestrator` | `CLAUDE.md` | n/a |
| Planejamento e definição de DoD | `Architect/Planner` | `CLAUDE.md` | n/a |
| Design de paralelização | `Architect/Planner` | `CLAUDE.md`, formato de output do Planner | n/a |
| Execução de paralelização | `Workflow Orchestrator` | preflight de specialists e regras universais | pressão do Stop-hook audit |
| Boundaries de specialist | arquivos de agente | `CLAUDE.md` | n/a |
| Wave de verificação | `Workflow Orchestrator` | formato de output do Planner | n/a |
| Fases de close-out | `CLAUDE.md` | arquivo do Orchestrator | `verify-completion.sh` |
| Injeção de contexto | docs de hook | `CLAUDE.md` | `inject-context.sh` |
| Bloqueio de comando destrutivo | docs de hook | `CLAUDE.md` | `block-dangerous.sh` |
| Auto-format | docs de hook | `CLAUDE.md` | `format-changed.sh` |
| Memória duradoura do projeto | `Documentation Agent` + sistema de memória | `CLAUDE.md` (seções de instrução) | n/a |

---

## 12. Requisitos de Arquivo de Agente

Todo arquivo de agente em `.claude/agents/` deve conter:

- frontmatter YAML
- um resumo de role
- responsabilidades explícitas ou mandato core
- um Agent Registry listando todos os roles registrados
- limites de role
- regras universais compartilhadas que o role deve obedecer
- um protocolo de completion curto

Se você renomear agentes, atualize:
- frontmatter `name`
- todo registry
- registry do Orchestrator
- formato de plano do Planner
- handoffs
- quaisquer mensagens de hook ou expectativas de script que mencionam o agente por nome

### 12.1 Formato de Frontmatter Comum

Use este como baseline:

```yaml
---
name: 'Nome Exato do Agente'
description: 'Resumo do role'
tools: ['Read', 'Edit', 'Grep', 'Glob', 'Bash', 'Agent']  # tools Claude Code disponíveis
agents: ['Nome Exato do Agente']     # omita apenas para o Orchestrator, que lista todo specialist chamável
model: sonnet                        # sonnet | opus | haiku
handoffs:
  - label: 'Label opcional de handoff'
    agent: 'Agente alvo opcional'
    prompt: 'Prompt opcional de handoff'
user-invocable: true|false
---
```

### 12.2 Regras de Baseline

1. Apenas o `Workflow Orchestrator` é user-invocable.
2. O Orchestrator lista todo specialist chamável em `agents`.
3. Specialists não devem ser user-invocable por padrão.
4. Specialists auto-delegando de mesmo role podem incluir a ferramenta `Agent`, mas isso não permite chamadas cross-role.
5. Registries de Specialist podem ser uma lista compacta de uma linha se todos os roles permanecerem visíveis.
6. Protocolos de completion devem ser breves. Preservar substância, não prosa repetitiva de handoff.

---

## 13. Contrato do `Workflow Orchestrator`

O arquivo do Orchestrator é o coração da arquitetura de runtime.

### 13.1 Identidade Obrigatória

- entry point para todo trabalho do usuário
- nunca implementa código
- único roteador cross-role
- rastreia DoD
- lança verificação independente
- reporta de forma concisa

### 13.2 Ferramentas Obrigatórias

No Claude Code, o Orchestrator deve ter acesso a:

- `Agent` (para delegar a specialists)
- `TodoWrite` (para progress tracking)
- `AskUserQuestion` (para clarificação)
- `Read`, `Grep`, `Glob` (para visibilidade de contexto — read-only)
- `EnterPlanMode` (para planejamento não-trivial)

Opcional:
- `WebSearch`, `WebFetch` (para lookup de documentação oficial)
- `Skill` (para capabilities reutilizáveis)

### 13.3 Seções de Workflow Obrigatórias

O arquivo deve conter estas fases de workflow em ordem:

1. **Entender e clarificar** — usar `AskUserQuestion` se ambíguo
2. **Planejar** — delegar ao `Architect/Planner` via `Agent`
3. **Despachar em waves** — lançar specialists via `Agent` com paralelização
4. **Verificação independente** — `QA` + `Security Reviewer` (e `Game Designer` + `Performance Analyst` quando relevante)
5. **Wave de documentação** — `Documentation Agent`
6. **Close-out de completion** — gerar relatório final

A wording exata de stop-phase não precisa ser repetida verbatim em todo arquivo de orquestração se `CLAUDE.md` e o contrato de hook runtime já são canônicos. O Orchestrator pode referenciar essa wording canônica em vez de duplicá-la.

### 13.4 Regras Absolutas que o Orchestrator Deve Carregar

- nunca escrever código ou rodar comandos
- sempre delegar
- nunca bypassar o Planner para trabalho não-trivial
- nunca pular o Documentation Agent
- nunca confiar em auto-relatórios
- perguntar quando bloqueado ou ambíguo
- respeitar respostas anteriores de `AskUserQuestion`
- preferir fixes de causa-raiz simples
- executar o plano de paralelização do Planner agressivamente
- maximizar fan-out de mesmo role quando seguro
- manter reporting para o usuário direto e token-eficiente

### 13.5 Requisito de Reporting Conciso

O reporting final do Orchestrator para o usuário deve preferir:
- status atual
- blockers
- resultado de verificação
- próxima decisão ou próximo passo

Evite longos resumos narrativos a menos que o usuário peça explicitamente.

Artefatos de close-out enforced por hook (tabela de resumo DoD, tabela de Files Changed, self-review de orquestração) devem aparecer apenas quando o Stop hook bloqueia e os solicita.

---

## 14. Contrato do `Architect/Planner`

O Planner é responsável pela qualidade de decomposição e design explícito de paralelização.

### 14.1 Identidade Obrigatória

- nunca implementa
- lê código e docs existentes antes de planejar
- decompoem em subtarefas atômicas
- atribui cada subtarefa a um agente exato
- define DoD verificável
- sinaliza blockers
- faz perguntas materiais quando necessário
- define paralelização em nível de wave

### 14.2 Output Obrigatório do Planner

O arquivo do Planner deve incluir um formato de output estruturado similar a este:

```markdown
## Plano de Tarefa: <Nome>

**Complexidade:** Simples | Média | Complexa
**Agentes:** <lista>
**Blockers:** <lista ou Nenhum>

### Wave 1 — <Nome da Fase>

**Paralelização:** <instruções exatas de lançamento same-wave, contagens de instâncias same-agent, split de fallback se a wave pode retornar muitas falhas independentes>

#### Subtarefa 1.1 — <Nome>
**Agente:** <Nome Exato do Agente>
**Depende de:** nenhum
**Arquivos:** <lista>
**Instrução:** <instrução precisa>
**DoD:**
- [ ] <critério>

#### Subtarefa 1.2 — <Nome> ⇄ paralelo com 1.1
**Agente:** <Nome Exato do Agente>
**Depende de:** nenhum
**Arquivos:** <lista>
**Instrução:** <instrução precisa>
**DoD:**
- [ ] <critério>

### Wave N-1 — Verificação

**Agente:** QA + Security Reviewer (+ Game Designer + Performance Analyst quando relevante)
**Depende de:** todas as waves de implementação
**Paralelização:** <split explícito de lane>
**DoD:**
- [ ] <critério>

### Wave N — Documentação

**Agente:** Documentation Agent
**Depende de:** todas as waves anteriores
**DoD:**
- [ ] memória relevante atualizada se estado duradouro mudou
- [ ] sugestões de orquestração atualizadas se uma melhoria diferida deve ser registrada
- [ ] instruções compartilhadas atualizadas se justificado
```

### 14.3 Requisitos Específicos do Planner

- usar docs oficiais antes de planejar trabalho dependente de framework ou library
- manter follow-up opcional separado do escopo comprometido
- design de FE e BE como tracks paralelas por padrão quando ambos existem e nenhuma dependência de contrato bloqueia
- design de fan-out de verificação explicitamente
- design de fan-out de fallback para clusters prováveis de falha

---

## 15. Contrato de Specialist Agent

Esta seção se aplica a specialists de implementação, QA, segurança, game design, performance e E2E.

### 15.1 Regras Compartilhadas de Specialist

Todo specialist deve:

- permanecer dentro dos limites de role
- manter progress tracking via `TodoWrite`
- preservar simplicidade
- usar docs oficiais quando requerido
- respeitar respostas anteriores do usuário
- emitir o preflight obrigatório antes do trabalho substantivo
- avaliar paralelização antes de começar
- usar tool batching primeiro para fan-out barato
- usar fan-out de worker de mesmo role quando materialmente útil e suportado pelo ambiente
- escalar blockers em vez de adivinhar
- manter reporting de completion curto

### 15.2 Preflight Obrigatório do Specialist

Todo specialist deve emitir exatamente estas duas seções ordenadas antes do trabalho substantivo:

1. `Plano de tarefa` — os passos de alto nível que o specialist tomará
2. `Estratégia de paralelismo` — abordagem de tool batching e/ou worker de mesmo role, ou `Nenhum paralelismo aplicável a esta tarefa.`

Este contrato deve aparecer tanto:
- no contrato raiz de arquitetura
- em cada arquivo de specialist

### 15.3 Specialist de Implementação (Expert Unity Developer)

O specialist de implementação deve:

- diagnosticar antes de editar
- implementar o menor fix ou slice de feature correto
- verificar comportamento alterado diretamente
- evitar criar abstrações sem justificativa
- preservar single sources of truth

### 15.4 Specialist de QA

O specialist de QA deve:

- verificar código alterado independentemente
- rodar as lanes de teste relevantes
- priorizar funcionalidade quebrada e regressões primeiro
- dividir verificação por lane quando possível
- pedir fan-out adicional de mesmo role se muitas falhas são independentes

### 15.5 Security Reviewer

O Security Reviewer deve:

- auditar código alterado para flaws exploráveis
- revisar risco de dependência/CVE e compliance de política
- reportar findings concretos, escopo revisado, riscos residuais e gaps de cobertura

### 15.6 Game Designer

O specialist de game design deve:

- avaliar fidelidade visual, qualidade de interação e consistência de design
- usar capturas de tela ou inspeção de nível de browser quando apropriado
- reportar findings de forma sucinta e concreta
- validar game feel (hit stop, screen shake, input buffering, knockback)

### 15.7 Performance Analyst

O specialist de performance deve:

- usar Unity Profiler MCP para profiling de runtime
- analisar object pooling, GC allocation, draw calls
- reportar findings com métricas concretas e recomendações
- validar contra as metas de performance (60 FPS, tick 60Hz, 200-500 entidades)

### 15.8 Documentation Agent

O Documentation Agent deve:

- rodar por último
- nunca implementar código de produto
- atualizar apenas memória duradoura de estado atual
- atualizar docs compartilhadas quando arquitetura ou fatos estáveis do repo mudaram
- registrar melhorias de orquestração diferidas separadamente da memória duradoura do projeto

Não faça o Documentation Agent gerado depender de nenhuma superfície de memória deletada ou duplicada fora do sistema de memória do Claude Code.

---

## 16. Contrato de Hooks

A arquitetura gerada deve preservar quatro famílias de hook runtime.

### 16.1 Hooks no Claude Code

No Claude Code, hooks são configurados em `.claude/settings.json`:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Bash",
        "hooks": [
          {
            "type": "command",
            "command": "bash scripts/block-dangerous.sh \"$TOOL_INPUT\""
          }
        ]
      }
    ],
    "PostToolUse": [
      {
        "matcher": "Edit|Write",
        "hooks": [
          {
            "type": "command",
            "command": "bash scripts/format-changed.sh \"$TOOL_INPUT_FILE_PATH\""
          }
        ]
      }
    ],
    "Stop": [
      {
        "hooks": [
          {
            "type": "command",
            "command": "bash scripts/verify-completion.sh"
          }
        ]
      }
    ]
  }
}
```

### 16.2 Injeção de Contexto

Artefatos:
- `.claude/settings.json` (hook de configuração)
- `scripts/inject-context.sh`

Propósito:
- injetar contexto do projeto no início da sessão
- injetar o mesmo contexto em subagents
- reduzir trabalho repetitivo de discovery

### 16.3 Bloqueio de Comando Destrutivo

Artefatos:
- `.claude/settings.json` (hook PreToolUse)
- `scripts/block-dangerous.sh`

Propósito:
- negar comandos obviamente destrutivos antes da execução

### 16.4 Auto-Format

Artefatos:
- `.claude/settings.json` (hook PostToolUse)
- `scripts/format-changed.sh`

Propósito:
- formatar automaticamente arquivos alterados após edições

### 16.5 Stop Hook / Verificação de Completion

Artefatos:
- `.claude/settings.json` (hook Stop)
- `scripts/verify-completion.sh`

Propósito:
- pressionar-testar se a delegação aconteceu quando obviamente deveria ter acontecido
- enforce artefatos de completion em fases
- manter a lógica de close-out em um script sem estado de runtime persistido extra

### 16.6 Fases Obrigatórias do Stop-Hook

O contrato de duas fases padrão deve ser preservado a menos que o usuário queira intencionalmente um modelo de close-out diferente:

**Fase 1:**
- exigir uma tabela de resumo DoD
- exigir uma tabela de Files Changed

**Fase 2:**
- exigir a self-review investigativa curta de orquestração
- exigir a wording canônica de fallback quando nenhuma melhoria é identificada
- exigir os sete cabeçalhos numerados exatos do prompt do hook para que a verificação driven por transcript seja determinística

### 16.7 Matriz de Enforcement

A arquitetura gerada deve separar:

- hard enforcement
- pressure-based enforcement
- audit-only enforcement
- instruction-only expectations

Mapeamento recomendado:

| Comportamento | Nível |
| --- | --- |
| checks de fase de close-out | Hard |
| bloqueio de comando destrutivo | Hard |
| auto-format após edições | Hard |
| injeção de contexto | Hard |
| delegação perdida quando paralelização era claramente possível | Pressure-based |
| correção de uso de MCP/tool-server | Audit-only |
| reutilização de terminal | Instruction-only |

---

## 17. Camada de Instruções

A arquitetura gerada deve ter três camadas de instrução:

1. `CLAUDE.md` para o contrato de arquitetura
2. Seções compartilhadas em `CLAUDE.md` para fatos estáveis do repo e normas de código
3. Seções em `CLAUDE.md` (ou `.claude/agents/*.md`) para regras path-specific ou domain-specific

### Regras de Layering:

- evitar duplicar o mesmo bloco grande de regra em todo arquivo
- repetir apenas o que é necessário para aderência ao role ou correção de runtime
- manter fatos específicos do projeto fora do template genérico de arquitetura
- manter boilerplate de completion breve

---

## 18. Camada de Memória

No Claude Code, a memória vive no sistema de memória built-in: `~/.claude/projects/<project-path>/memory/`.

### 18.1 Memória Duradoura do Projeto

Use o sistema de memória do Claude Code para fatos de estado atual que ajudam trabalho futuro:

- lógica de negócio e comportamento que importa depois
- grandes contratos de codebase
- comportamento de build, test, deploy, environment ou integração
- convenções duradouras do repo que não são óbvias a partir do código

**Não armazene:**
- notas temporárias de debugging
- nits estilísticos
- fixes menores one-off
- logs de mudança histórica
- cópias duplicadas de regras de arquitetura já canônicas em outro lugar

### 18.2 Melhorias Diferidas de Orquestração

Use `doc/orchestration-suggestions.md` para melhorias de arquitetura que são intencionalmente diferidas.

Estrutura recomendada:
- agrupar pelos sete princípios canônicos de auto-auditoria
- manter entradas datadas
- manter entradas acionáveis
- remover ou reescrever sugestões stale quando implementadas ou substituídas

### 18.3 Regras de Memória

- manter apenas a verdade atual
- remover memória stale ou conflitante em vez de appendar história para sempre
- não criar nenhuma superfície de memória separada fora do sistema de memória do Claude Code
- não espalhar memória em arquivos markdown não relacionados

---

## 19. Camada de Skills

`.claude/skills/` é opcional.

Use quando:
- um comportamento de orquestração recorrente precisa de instruções locais reutilizáveis
- o repo se beneficia de capability packs especializados

Não use como depósito para regras genéricas já cobertas por:
- `CLAUDE.md`
- arquivos de agente
- instruções compartilhadas

No Claude Code, skills são invocadas via a ferramenta `Skill` e definidas em `.claude/skills/` ou configuradas como skills registradas.

---

## 20. Camada MCP / Tool-Server

Trate como opcional e environment-dependent.

### 20.1 Comportamento Obrigatório do Blueprint

Ao gerar uma nova arquitetura:
- decidir se MCP/tool servers são realmente úteis para o repo alvo
- documentar quando cada server deve e não deve ser usado
- evitar hardcoded de estratégia de secrets no blueprint genérico
- tornar provisionamento de secrets uma decisão do projeto

### 20.2 Guia de Decisão Genérico

Mantenha um guia de decisão para qualquer tool server externo configurado:

| Cenário | Quando usar | Quando NÃO usar | Owner primário |
| --- | --- | --- | --- |
| Unity MCP | manipulação de scenes, GameObjects, assets, profiling | quando CLI/git é suficiente | Expert Unity Developer, Performance Analyst |
| Browser automation | inspeção visual, E2E testing | para código backend puro | QA, Game Designer |
| Database inspection | debug de dados, queries | para desenvolvimento local simples | QA |

### 20.3 Descoberta de Settings e Capabilities

Antes de finalizar a arquitetura gerada, revise a documentação oficial mais recente do Claude Code para settings ou capabilities que podem melhorar:

- orquestração de subagents
- hooks
- handoffs
- question tools
- acesso a memória
- permissões de agente
- reutilização de terminal/sessão
- roteamento de modelo
- integração MCP/tool-server

Então forneça ao usuário uma lista dedicada de fim-de-processo:

| Setting ou capability | Benefício | Por que relevante aqui | Recomendado? | Decisão do usuário |
| --- | --- | --- | --- | --- |
| `<nome>` | `<benefício>` | `<razão>` | Sim/Não | Pendente |

Esta lista é informativa. O usuário decide o que ativar.

---

## 21. Local-Only Vs Committed Boundary

O blueprint deve forçar uma decisão deliberada:

- A arquitetura de orquestração viverá apenas localmente?
- Ou será commitada no repositório?

Se local-only:
- definir os paths local-only exatos
- definir como eles ficam fora do version control
- manter essa política fora dos docs do repo commitado a menos que intencionalmente desejado

Se committed:
- remover quaisquer instruções local-only que contradigam o modelo escolhido

Não deixe isso ambíguo na arquitetura gerada.

---

## 22. Procedimento de Bootstrap Para Um Novo Repo

Gere a nova arquitetura nesta ordem:

1. Identifique o stack alvo, runtime, e se o repo tem UI, backend, ou ambos.
2. Decida o roster final de specialists preservando a mesma topologia.
3. Escolha nomes exatos de role e mantenha-os estáveis.
4. Rascunhe `CLAUDE.md` primeiro, incluindo regras genéricas e uma seção de projeto-especifico claramente separada.
5. Crie todos os arquivos de agente com frontmatter, responsabilidades, registry, boundaries e protocolos de completion curtos.
6. Crie configs de hook e placeholders ou implementações de script.
7. Crie quaisquer arquivos de instrução scoped por path.
8. Crie a estrutura de memória.
9. Decida se skills são necessárias.
10. Decida se MCP/tool servers são necessários.
11. Decida se `.claude/settings.json` é repo-owned ou environment-owned.
12. Prefira um Stop hook driven por transcript que não requer estado de runtime persistido extra.
13. Rode uma passagem de consistência entre nomes, modelos, ferramentas e cabeçalhos de pergunta.
14. Rode uma passagem de drift comparando regras de prose contra scripts de runtime e configs de hook.
15. Produza o handoff final para o usuário, lista de settings opcionais e TODO list requerida.

Regra de geração atomicamente planejada:
- trate cada seção major acima como um discrete build step
- finalize uma seção antes de depender dela em outro lugar
- não deixe inconsistências de placeholder entre arquivos

---

## 23. Checklist de Validação

A arquitetura gerada não está pronta até que todos estes sejam verdadeiros:

- [ ] `Workflow Orchestrator` é o único roteador cross-role user-invocable.
- [ ] `Architect/Planner` existe e roda antes de implementação não-trivial.
- [ ] O formato de output do Planner inclui instruções explícitas de paralelização em nível de wave.
- [ ] O contrato do Orchestrator diz explicitamente para maximizar fan-out de mesmo role quando seguro.
- [ ] Specialists não podem fazer cross-route para outros tipos de specialist.
- [ ] Todo arquivo de specialist inclui o preflight obrigatório de duas partes.
- [ ] A wave de verificação é obrigatória após mudanças de código.
- [ ] Mudanças de UI adicionam auditoria visual/acessibilidade plus verificação browser/E2E quando relevante.
- [ ] `Documentation Agent` é sempre a última wave.
- [ ] Sistema de memória do Claude Code está configurado para memória duradoura do projeto.
- [ ] `doc/orchestration-suggestions.md` existe para melhorias diferidas de arquitetura.
- [ ] Nenhuma superfície de memória duplicada existe fora do sistema de memória do Claude Code.
- [ ] Configs de hook e scripts concordam com o contrato de close-out documentado.
- [ ] As fases do Stop hook estão documentadas em ambos `CLAUDE.md` e enforcement de runtime.
- [ ] A wording exata de fallback da auto-auditoria é preservada.
- [ ] Regras específicas do projeto estão separadas de regras genéricas de arquitetura.
- [ ] Tabelas de modelo correspondem ao frontmatter dos agentes.
- [ ] Registries de agente são internamente consistentes com o set final de roles.
- [ ] Listas de ferramentas correspondem a responsabilidades reais de role.
- [ ] Cabeçalhos de pergunta e texto de prompt são consistentes entre docs e scripts.
- [ ] O usuário recebe uma lista fim-de-processo de settings/capabilities opcionais para considerar.

---

## 24. Traps Comuns de Drift

Fique atento a estes ao gerar ou manter a arquitetura:

1. Drift de versão de modelo entre `CLAUDE.md` e frontmatter de agente.
2. Um specialist acidentalmente se tornando user-invocable.
3. O Orchestrator respondendo diretamente em vez de delegar.
4. O Planner descrevendo subtarefas mas falhando em especificar paralelização exata.
5. O Orchestrator serializando trabalho independente de mesmo role que deveria ter sido fanned out.
6. Roles de verificação faltando após mudanças de código.
7. Documentação rodando antes que verificação esteja completa.
8. Prosa de stop-hook derivando de `verify-completion.sh`.
9. Cabeçalhos de pergunta derivando entre docs e scripts de runtime.
10. Memória espalhada em arquivos markdown aleatórios em vez do sistema de memória do Claude Code.
11. Preocupações específicas de template vazando para o repo gerado.
12. Settings de ambiente específicas do projeto sendo hardcoded como regras universais de arquitetura.
13. Boilerplate de completion repetitivo crescendo em arquivos de specialist.
14. Nomes de agentes sendo renomeados em um lugar mas não em todo registry ou handoff.

---

## 25. Economias Seguras de Token Sem Enfraquecer a Arquitetura

Ao otimizar a arquitetura gerada depois, estes geralmente são seguros:

- encurtar boilerplate repetitivo de completion em arquivos de specialist
- usar uma lista compacta de uma linha de Agent Registry dentro de arquivos de specialist se a awareness for preservada
- merge summaries de platform-feature, autonomy e hook sobrepostos em uma tabela de capability/enforcement em `CLAUDE.md`
- centralizar regras grandes compartilhadas em `CLAUDE.md` e manter deltas role-locais em arquivos de agente
- manter wording exata de stop-hook em um lugar canônico e referenciá-la em outro lugar, desde que runtime e docs ainda concordem
- preferir tabelas concisas de MCP/tool-server sobre prosa narrativa repetida

**Não otimize fora:**
- o contrato de ownership de paralelização
- o contrato de preflight de specialist
- a wave de verificação independente
- a ordem de fase de close-out
- a wording exata de fallback da auto-auditoria
- a separação entre regras genéricas e específicas do projeto

---

## 26. Output Final Obrigatório Do Claude Code

Após gerar a arquitetura, o Claude Code deve fornecer:

1. Um resumo conciso do que foi criado.
2. A lista de suposições específicas do projeto que foram feitas.
3. A lista de settings/capabilities opcionais de ambiente que o usuário pode ativar.
4. Quaisquer blockers ou decisões abertas ainda necessárias.
5. Uma TODO list de pontos de atenção de execução restantes.

### Formato Obrigatório da TODO

A TODO list deve ser explícita e acionável. O item final deve ser:

- auditar a arquitetura gerada contra este blueprint e listar cada desvio, omissão ou desvio justificado

Se não houver desvios, o item de audit deve dizer isso explicitamente.

---

## 27. Tabela de Seleção de Modelos (Claude Code)

| Role | Modelo recomendado | Fallback | Justificativa |
| --- | --- | --- | --- |
| Workflow Orchestrator | `opus` | `sonnet` | coordenação complexa, raciocínio profundo |
| Architect/Planner | `opus` | `sonnet` | decomposição, design de paralelização |
| Expert Unity Developer | `sonnet` | `opus` | implementação de código, bom custo-benefício |
| Game Designer | `sonnet` | `opus` | análise de game feel, decisões de design |
| QA | `sonnet` | `haiku` | execução de testes, verificação |
| Security Reviewer | `opus` | `sonnet` | auditoria profunda, análise de segurança |
| Performance Analyst | `sonnet` | `opus` | profiling, análise de métricas |
| Documentation Agent | `sonnet` | `haiku` | escrita de docs, atualização de memória |

---

## 28. Agent Registry Completo

Todos os agentes devem conhecer este registry:

```yaml
agent_registry:
  - name: 'Workflow Orchestrator'
    type: orchestrator
    user-invocable: true
    delegates-to: ['Architect/Planner', 'Expert Unity Developer', 'Game Designer', 'QA', 'Security Reviewer', 'Performance Analyst', 'Documentation Agent']

  - name: 'Architect/Planner'
    type: planner
    user-invocable: false
    delegates-to: []  # planejamento apenas, não implementa

  - name: 'Expert Unity Developer'
    type: specialist
    user-invocable: false
    delegates-to: ['Expert Unity Developer']  # auto-delegação same-role apenas

  - name: 'Game Designer'
    type: specialist
    user-invocable: false
    delegates-to: ['Game Designer']

  - name: 'QA'
    type: specialist
    user-invocable: false
    delegates-to: ['QA']

  - name: 'Security Reviewer'
    type: specialist
    user-invocable: false
    delegates-to: ['Security Reviewer']

  - name: 'Performance Analyst'
    type: specialist
    user-invocable: false
    delegates-to: ['Performance Analyst']

  - name: 'Documentation Agent'
    type: specialist
    user-invocable: false
    delegates-to: []
```

---

## 29. Mapeamento Claude Code → Blueprint Original

Para referência, como cada conceito do blueprint original se adapta ao Claude Code:

| Conceito do Blueprint | Implementação no Claude Code |
| --- | --- |
| `AGENTS.md` | `CLAUDE.md` (este arquivo) |
| `.github/agents/*.agent.md` | `.claude/agents/*.md` + `Agent` tool com `subagent_type` |
| `.github/hooks/*.json` | `.claude/settings.json` → `hooks` section |
| `scripts/*.sh` | `scripts/*.sh` (executados pelos hooks) |
| `.github/copilot-instructions.md` | Seções em `CLAUDE.md` |
| `.github/instructions/*.instructions.md` | Seções em `CLAUDE.md` ou `.claude/agents/*.md` |
| `memories/repo/*.md` | Sistema de memória built-in do Claude Code (`~/.claude/projects/.../memory/`) |
| `memories/orchestration-suggestions.md` | `doc/orchestration-suggestions.md` |
| `.github/skills/` | `.claude/skills/` + `Skill` tool |
| `.vscode/mcp.json` | `.claude/settings.json` → `mcpServers` ou VS Code settings |
| `AskUser` tool | `AskUserQuestion` |
| `TodoWrite` | `TodoWrite` (built-in) |
| `Agent` tool | `Agent` (built-in) com `subagent_type` |
| Model routing | `model` parameter no `Agent` tool ou no frontmatter do agente |
| Worktree isolation | `EnterWorktree` / `isolation: "worktree"` no `Agent` tool |
