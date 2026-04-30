# AGENTS.md — Multi-Role Agent Architecture for Kilo Code

> Project: LF2 + Survivors (Beat 'em up 2.5D / Vampire Survivors hybrid)
> Engine: Unity 6 + URP 2D | Netcode: Host-client Steam-first
> Adapted from Claude Code architecture for **Kilo Code** (kilo-ai/kilocode CLI)

---

## 1. Purpose

This document defines:

- the canonical multi-agent orchestration topology for Kilo Code
- the rule hierarchy and source-of-truth order
- the role boundaries of each agent
- the lifecycle: planning → dispatch → verification → documentation
- the permission-driven enforcement model
- the durable memory model
- the validation and drift checks

---

## 2. Consumption Rules

When using this architecture, follow these rules exactly:

1. Be maximally detail-oriented. Do not collapse important contracts into vague summaries.
2. Preserve the architecture shape even if stack-specific names, tools, or models change.
3. Separate generic orchestration rules from project-specific rules. Do not mix them.
4. Plan and execute atomically, section by section. Do not skip and backfill later.
5. Treat each section as a deliverable. If not applicable, say why explicitly instead of omitting.
6. Do not invent unsupported platform capabilities. If environment-dependent, document it.
7. Use official Kilo Code documentation when deciding features, agents, tools, MCP support, or workspace capabilities.
8. At the end of the process, provide the user with a list of optional settings they may enable.
9. Keep completion boilerplate short in specialist agent files. Preserve rules; compress only repetitive close-out phrasing.
10. Do not create any duplicate memory surface outside `memories/`.
11. End the process with a TODO list covering remaining execution points of attention.
12. The last TODO item must require an audit comparing the generated architecture against this document, listing all mismatches.

---

## 3. Kilo Code Platform Notes

### Configuration Model

Kilo Code loads from: `~/.config/kilo/kilo.json` (global), `./kilo.json`, `.kilo/kilo.json`, and env vars (`KILO_CONFIG`, `KILO_CONFIG_CONTENT`).

### Agent System

- Agents live in `.kilo/agent/*.md` — filename (minus `.md`) is the agent name
- `mode: primary` = user-selectable (equivalent to Claude Code `user-invocable: true`)
- `mode: subagent` = callable only via `task` tool from another agent
- `mode: all` = both

### Key Tool Mappings (Claude Code → Kilo Code)

| Claude Code | Kilo Code | Purpose |
| --- | --- | --- |
| `Agent` | `task` | Subagent delegation |
| `AskUserQuestion` | `question` | Structured user questions |
| `TodoWrite` | `todowrite` | Progress tracking |
| `EnterPlanMode` | plan mode (system reminder) | Planning without implementation |

### Hooks — Kilo Code Equivalent

**Kilo Code has no native hook system.** Enforcement is via:

| Claude Code Hook | Kilo Code Equivalent |
| --- | --- |
| `PreToolUse` (block dangerous) | `permission.bash` deny patterns in `kilo.json` |
| `PostToolUse` (auto-format) | `AGENTS.md` instruction + `/format` command |
| `Stop` (verify completion) | orchestrator close-out protocol + `/verify-completion` command |
| Context injection | `instructions` field in `kilo.json` + `AGENTS.md` content |

---

## 4. What Must Stay Generic Vs Project-Specific

| Layer | Generic | Project-Specific |
| --- | --- | --- |
| Topology | Orchestrator → Planner → specialists → verification → documentation | exact stack specialist names |
| Rules | routing, verification, close-out phases, preflight, memory policy, parallelization contract | game feel rules, test caveats, build quirks |
| Files | folder layout, agent-file structure, memory folder structure | repo names, stack files, script bodies |
| Models | role-based selection strategy | exact model names and fallbacks |
| Tools | tool categories per role | exact tool identifiers per environment |
| MCP | decision framework and optionality | exact servers, secrets strategy |
| Permissions | pattern-based deny/allow/ask strategy | exact glob patterns for project paths |
| Local-only | whether boundary exists | exact paths, committed vs excluded |

---

## 5. Canonical Topology

```text
User
  └─▶ Workflow Orchestrator          (entry point, mode: primary, never implements)
        └─▶ Architect/Planner        (task decomposition, DoD, parallelization design)
              ├─▶ Expert Unity Developer     (implementation)
              ├─▶ Game Designer              (game feel, balanceamento, UX de gameplay)
              ├─▶ QA                         (tests, quality verification)
              ├─▶ Security Reviewer          (security verification)
              ├─▶ Performance Analyst        (profiling, optimization)
              └─▶ Documentation Agent        (always last)
```

### Topology Rules

- `Workflow Orchestrator` is the only user-facing entry point (`mode: primary`).
- `Architect/Planner` runs before any non-trivial implementation.
- Specialists do domain work (`mode: subagent`).
- Verification is independent from implementation.
- `Documentation Agent` is always the final wave.

### Project Adaptation (Unity/LF2)

- `Expert Unity Developer` — C#, ScriptableObjects, shaders, animations, netcode.
- `Game Designer` — game feel (hit stop, screen shake, input buffering), stat balancing, combat UX.
- `Performance Analyst` — profiling via Unity Profiler MCP, object pooling, entity optimization.
- `QA` — Unity Test Runner, unit tests, integration tests, smoke tests.
- `Security Reviewer` — code audit, dependencies, CVEs.
- `Documentation Agent` — durable project memory, shared docs updates.

---

## 6. Source-Of-Truth Order

| Priority | Artifact | Purpose |
| --- | --- | --- |
| 1 | `AGENTS.md` | canonical architecture contract and cross-role rules |
| 2 | `.kilo/agent/*.md` | role-local contracts, tools, boundaries, preflight, completion rules |
| 3 | `kilo.json` | permissions, MCP servers, model config, instruction globs |
| 4 | `.kilo/command/*.md` | executable command workflows (close-out, formatting, context injection) |
| 5 | `AGENTS.md` (project-specific sections) | stable repository facts and shared coding norms |
| 6 | `memories/repo/*.md` | durable current-state project memory |
| 7 | `memories/orchestration-suggestions.md` | deferred orchestration improvements |
| 8 | `.kilo/skill/*/SKILL.md` | optional reusable capability packs |

### Conflict Rule

- `AGENTS.md` wins over secondary documentation when prose conflicts
- `kilo.json` permission rules are enforced at runtime — they override prose
- role-local agent files refine, but do not weaken, the root architecture contract
- memory stores current truth only; it does not override architecture contracts

---

## 7. Filesystem Blueprint

```text
<repo-root>/
├─ AGENTS.md                              # canonical architecture contract
├─ kilo.json                              # permissions, MCP, model config
├─ .kilo/
│  ├─ agent/
│  │  ├─ workflow-orchestrator.md         # orchestration entry point
│  │  ├─ architect-planner.md            # planning and decomposition
│  │  ├─ expert-unity-developer.md       # Unity/C# implementation
│  │  ├─ game-designer.md                # game feel and balancing
│  │  ├─ qa-subagent.md                  # tests and verification
│  │  ├─ security-reviewer.md            # security audit
│  │  ├─ performance-analyst.md          # profiling and optimization
│  │  └─ documentation-agent.md          # documentation and memory
│  ├─ command/
│  │  ├─ verify-completion.md            # close-out verification
│  │  ├─ format.md                       # auto-format workflow
│  │  └─ inject-context.md              # context injection
│  └─ skill/                             # optional skill packs
├─ scripts/
│  ├─ inject-context.sh                   # session context injection
│  ├─ format-changed.sh                   # auto-format changed files
│  ├─ block-dangerous.sh                  # destructive command blocking reference
│  └─ verify-completion.sh                # close-out verification reference
├─ memories/
│  ├─ repo/
│  │  └─ lf2-survivors-state.md          # durable project memory
│  └─ orchestration-suggestions.md        # deferred improvements
├─ doc/
│  ├─ orchestration-suggestions.md        # legacy (moved to memories/)
│  └─ temp/
│     └─ kilo-agent-architecture-template.md
```

---

## 8. Core Architecture Contract

### 8.1 Fundamental Rules

1. Every multi-step task goes through `Workflow Orchestrator` first.
2. `Workflow Orchestrator` routes every request to a dedicated agent.
3. `Workflow Orchestrator` is the source of truth between the user request and delivered work.
4. The Orchestrator asks for clarification before delegating if the request is materially ambiguous or incomplete.
5. `Architect/Planner` runs before implementation on any non-trivial task.
6. The Orchestrator delegates through the `task` tool to the appropriate specialist.
7. Independent subtasks dispatch in parallel within the same wave (multiple `task` calls in one response).
8. Any code change triggers an independent verification wave, at minimum `QA` plus `Security Reviewer`.
9. UI changes additionally trigger `Game Designer` (visual/accessibility) and `Performance Analyst` when relevant.
10. The Orchestrator never trusts self-reports as proof of completion.
11. `Documentation Agent` is always the final wave.
12. The Orchestrator is the only cross-role router.
13. Eligible specialists may self-delegate only within their own role or inherited worker scope.
14. Simplicity beats over-engineering.
15. All agents maintain progress tracking via `todowrite`.
16. All agents know about all other agents through an Agent Registry.
17. Official documentation lookup is mandatory for planning, large implementation/refactor work, and audits.
18. Architecture changes require explicit user approval.
19. Fix root causes, not symptoms.
20. Preserve single sources of truth for reusable values and contracts.
21. Prefer existing abstractions before creating new ones.
22. Extract reusable logic only when justified.
23. The Planner uses `question` for material trade-offs or user-only blockers.
24. The Planner lists adjacent work as optional follow-up, not as assumed scope.
25. The Planner may propose a better approach, but execution follows the user's decision if the user declines.
26. Verification loops are capped at three fix → verify iterations.
27. QA prioritizes blocking failures before minor polish.
28. The orchestration self-audit follows a canonical priority order.
29. Close-out artifacts are produced only when the close-out protocol requests them; do not emit DoD summary, Files Changed, or self-review proactively.
30. Agents do not re-ask decisions already answered through `question` during the same session.
31. The Planner is the primary owner of parallelization design.
32. The Orchestrator is the primary owner of parallelization execution.
33. Specialists still assess additional fan-out opportunities during execution.
34. Orchestration-file changes require an independent consistency pass.
35. Agents verify working directory before terminal commands.
36. The self-audit fallback wording is exact and canonical.
37. Every specialist must emit a structured preflight before substantive work.
38. The Orchestrator's user-facing reporting must stay direct and concise.
39. Progress tracking uses `todowrite` for all multi-step tasks.

---

## 9. Parallelization Contract

### 9.1 Planner Responsibilities

- split work into atomic, parallel-safe subtasks
- specify wave boundaries
- specify exactly which subtasks run in parallel
- specify when multiple instances of the same agent should be launched (via multiple `task` calls)
- specify verification fan-out by lane when possible
- specify fallback fan-out if a wave is likely to return many independent failures

### 9.2 Orchestrator Responsibilities

- treat the Planner's parallelization instructions as execution requirements
- launch all independent same-wave subtasks together (batch `task` calls in one response)
- maximize safe same-role fan-out instead of serializing independent work
- prefer the highest useful number of same-role instances when work is separable
- re-fan-out verification or fixes if failures cluster into independent concerns

### 9.3 Specialist Responsibilities

- assess whether more same-role fan-out became possible after work began
- self-delegate within role when the environment allows it and the speedup is material
- otherwise report a concrete fan-out recommendation back to the Orchestrator immediately

### 9.4 Examples

- a test audit can spawn separate `QA` instances for unit, integration, smoke
- if ten failures return and they separate into independent buckets, split them into workers

### 9.5 Tool Parallelism Vs Worker Subagents

- batched reads/searches/web fetches in one turn are tool parallelism inside one agent session
- worker subagents are separate same-role executions via `task` for isolation or speed
- same-role worker use is allowed — it never grants cross-role routing authority

**In Kilo Code:**
- Tool parallelism = multiple tool calls in the same response (read, grep, glob in parallel)
- Worker subagents = multiple `task` calls with `subagent_type` set to the same specialist type

---

## 10. Orchestration Self-Audit Priority Order

1. be reliable
2. be as fast as possible on implementation through good parallelization
3. perform self-audit
4. run reliable tests on each iteration
5. write DoD with evidence and reports
6. be autonomous
7. be cost-effective

For each principle, the fallback when no issue is found must be exactly:

`Nenhuma melhoria identificada para este princípio.`

Do not paraphrase the fallback.

---

## 11. Rule Ownership Matrix

| Rule area | Primary owner | Secondary owner | Runtime owner |
| --- | --- | --- | --- |
| Cross-role routing | `Workflow Orchestrator` | `AGENTS.md` | n/a |
| Planning and DoD definition | `Architect/Planner` | `AGENTS.md` | n/a |
| Parallelization design | `Architect/Planner` | `AGENTS.md`, Planner output format | n/a |
| Parallelization execution | `Workflow Orchestrator` | specialist preflight and universal rules | close-out audit pressure |
| Specialist boundaries | agent files | `AGENTS.md` | n/a |
| Verification wave | `Workflow Orchestrator` | Planner output format | n/a |
| Close-out phases | `AGENTS.md` | Orchestrator file | `/verify-completion` command |
| Context injection | `kilo.json` instructions | `AGENTS.md` | startup |
| Destructive command blocking | `kilo.json` permissions | `AGENTS.md` | runtime permission enforcement |
| Auto-format | `AGENTS.md` instruction | `/format` command | agent adherence |
| Durable project memory | `Documentation Agent` + `memories/` | `AGENTS.md` | n/a |

---

## 12. Agent File Requirements

Every agent file under `.kilo/agent/` must contain:

- YAML frontmatter
- a role summary
- explicit responsibilities or core mandate
- an Agent Registry listing all registered roles
- role boundaries
- any shared universal rules that the role must obey
- a short completion protocol

If you rename agents, update:

- the filename (becomes agent identifier in Kilo Code)
- every registry
- Orchestrator registry
- Planner plan format
- handoffs
- any command files or scripts that mention the agent by name

### 12.1 Frontmatter Format (Kilo Code)

```yaml
---
description: '<Role summary>'
mode: subagent                   # primary | subagent | all
model: xiaomi/mimo-v2.5-pro  # optional override
steps: 25                        # max agentic iterations
hidden: false                    # hide from @ menu
color: "#FF5733"                 # optional hex color
permission:                      # optional agent-level permissions
  bash: allow
  edit:
    "Assets/**": allow
    "*": ask
---
```

### 12.2 Baseline Rules

1. Only the `Workflow Orchestrator` has `mode: primary`.
2. All specialists have `mode: subagent`.
3. The Orchestrator's system prompt lists every callable specialist by name.
4. Specialists should not be user-invocable by default.
5. Agent Registries may be a compact one-line list if all roles remain visible.
6. Completion protocols should be brief.

### 12.3 Model Selection Table

| Role | Model | Rationale |
| --- | --- | --- |
| Workflow Orchestrator | `xiaomi-token-plan-sgp/mimo-v2.5-pro` | complex coordination, deep reasoning |
| Architect/Planner | `xiaomi-token-plan-sgp/mimo-v2.5-pro` | decomposition, parallelization design |
| Expert Unity Developer | `xiaomi-token-plan-sgp/mimo-v2.5-pro` | fast, accurate implementation |
| Game Designer | `xiaomi-token-plan-sgp/mimo-v2.5-pro` | game feel analysis, design decisions |
| QA | `xiaomi-token-plan-sgp/mimo-v2.5-flash` | test execution, verification |
| Security Reviewer | `xiaomi-token-plan-sgp/mimo-v2.5-pro` | deep security analysis |
| Performance Analyst | `xiaomi-token-plan-sgp/mimo-v2.5-pro` | profiling, metrics analysis |
| Documentation Agent | `xiaomi-token-plan-sgp/mimo-v2.5-flash` | fast documentation writing |

---

## 13. Workflow Orchestrator Contract

### 13.1 Identity

- entry point for all user work
- never implements code or runs commands directly
- only cross-role router
- tracks DoD
- launches independent verification
- reports concisely

### 13.2 Required Tools

| Tool | Purpose |
| --- | --- |
| `read`, `grep`, `glob` | context visibility (read-only) |
| `task` | delegate to specialists |
| `todowrite` | progress tracking |
| `question` | user clarification |
| `websearch`, `webfetch` | official documentation lookup |
| `skill` | load optional skill packs |

The orchestrator should NOT have `edit`, `write`, or `bash` — it must never implement directly.

### 13.3 Workflow Phases

1. **Understand and clarify** — use `question` if ambiguous
2. **Plan** — delegate to `Architect/Planner` via `task` for non-trivial work
3. **Dispatch in waves** — launch specialists via `task`, maximize parallel fan-out
4. **Independent verification** — minimum `QA` + `Security Reviewer`
5. **Documentation wave** — always last via `Documentation Agent`
6. **Completion close-out** — concise user report (status, blockers, verification outcome, next step)

### 13.4 Absolute Rules

- never write code or run commands
- always delegate through `task`
- never bypass the Planner for non-trivial work
- never skip the Documentation Agent
- never trust self-reports
- use `question` when blocked or ambiguous
- respect prior `question` answers
- prefer simple root-cause fixes
- execute the Planner's parallelization plan aggressively
- maximize same-role fan-out when safe
- keep user reporting direct and token-efficient

### 13.5 Concise Reporting

The Orchestrator's final reporting should prefer:

- current status
- blockers
- verification outcome
- next decision or next step

Close-out artifacts (DoD table, Files Changed, self-review) appear only when the close-out protocol requests them.

---

## 14. Architect/Planner Contract

### 14.1 Identity

- never implements
- reads existing code and docs before planning
- decomposes into atomic subtasks
- assigns each subtask to one exact agent
- defines verifiable DoD
- flags blockers
- asks material questions when needed (via `question`)
- defines wave-level parallelization

### 14.2 Output Format

```markdown
## Plano de Tarefa: <Nome>

**Complexidade:** Simples | Média | Complexa
**Agentes:** <lista>
**Blockers:** <lista ou Nenhum>

### Wave 1 — <Nome da Fase>

**Paralelização:** <instruções exatas de lançamento same-wave>

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
- [ ] sugestões de orquestração atualizadas se aplicável
```

### 14.3 Specific Requirements

- use official docs before planning framework/library-dependent work (via `webfetch`)
- keep optional follow-up separate from committed scope
- design FE and BE as parallel tracks when possible
- design verification fan-out explicitly
- design fallback fan-out for likely failure clusters

---

## 15. Specialist Agent Contract

### 15.1 Shared Specialist Rules

Every specialist must:

- stay within role boundaries
- maintain progress tracking via `todowrite`
- preserve simplicity
- use official docs when required (via `webfetch`)
- respect prior user answers
- emit the mandatory preflight before substantive work
- assess parallelization before starting
- use tool batching first for cheap fan-out
- use same-role worker fan-out via `task` when materially useful
- escalate blockers instead of guessing
- keep completion reporting short

### 15.2 Mandatory Preflight

Every specialist must emit exactly these two ordered sections before substantive work:

1. **Plano de tarefa** — the high-level steps
2. **Estratégia de paralelismo** — tool batching and/or same-role worker approach, or `Nenhum paralelismo aplicável a esta tarefa.`

### 15.3 Implementation Specialist (Expert Unity Developer)

- diagnose before editing
- implement the smallest correct fix or feature slice
- verify changed behavior directly
- avoid creating abstractions without justification
- preserve single sources of truth

### 15.4 QA Specialist

- verify changed code independently
- run the relevant test lanes
- prioritize broken functionality and regressions first
- split verification by lane when possible

### 15.5 Security Reviewer

- audit changed code for exploitable flaws
- review dependency/CVE risk and policy compliance
- report concrete findings, scope reviewed, residual risks, and coverage gaps

### 15.6 Game Designer

- assess visual fidelity, interaction quality, accessibility, and design consistency
- validate game feel (hit stop, screen shake, input buffering, knockback)
- use screenshots or visual inspection when appropriate

### 15.7 Performance Analyst

- profile runtime behavior (memory, CPU, frame time)
- analyze object pooling, GC allocation, draw calls
- validate against performance targets (60 FPS, 60Hz tick, 200-500 entities)

### 15.8 Documentation Agent

- run last
- never implement product code
- update only durable current-state memory under `memories/`
- update `AGENTS.md` or project docs when architecture or stable repo facts changed
- record deferred orchestration improvements in `memories/orchestration-suggestions.md`

---

## 16. Enforcement Model

Kilo Code does not have a native hook system. Enforcement uses:

| Layer | Mechanism | Level |
| --- | --- | --- |
| Destructive command blocking | `kilo.json` `permission.bash` deny patterns | Hard (runtime-enforced) |
| File edit boundaries | `kilo.json` `permission.edit` glob patterns | Hard (runtime-enforced) |
| MCP tool permissions | `kilo.json` `permission` MCP key patterns | Hard / Ask (runtime-enforced) |
| Context injection | `kilo.json` `instructions` + `AGENTS.md` | Hard (startup-loaded) |
| Auto-format | `AGENTS.md` instruction + `/format` command | Instruction-only |
| Close-out verification | orchestrator prompt + `/verify-completion` | Instruction-only |
| Parallelization pressure | orchestrator/planner contracts + self-audit | Audit-only |

### 16.1 Hard-Enforced Rules (via `kilo.json` permissions)

Cannot be bypassed by the agent:

- destructive bash commands matching deny patterns
- file edits outside allowed glob patterns
- MCP tool invocations matching deny/ask patterns
- external directory access when denied

### 16.2 Instruction-Only Rules

Rely on agent adherence:

- close-out phase checks
- auto-format after edits
- preflight emission
- parallelization execution
- documentation wave ordering

### 16.3 Close-Out Protocol

At the end of any multi-agent task:

**Fase 1 — Artefatos:**
- produce a DoD summary table with evidence
- produce a Files Changed table

**Fase 2 — Self-audit:**
- review the seven canonical self-audit principles in order
- for each principle, if no improvement is found, use exactly: `Nenhuma melhoria identificada para este princípio.`
- do not paraphrase this fallback wording

---

## 17. MCP / Tool-Server Decision Guide

| Server | When to use | When NOT to use | Primary owners |
| --- | --- | --- | --- |
| Unity MCP | scene manipulation, GameObjects, assets, profiling | when CLI/git is sufficient | Expert Unity Developer, Performance Analyst |
| Browser automation | visual inspection, E2E testing | for backend-only code | QA, Game Designer |

### 17.1 MCP Configuration

MCP servers are defined in `kilo.json`. See the decision guide above for usage policies.

---

## 18. Git / Worktree Policy

- work on feature branches
- no direct commits to main/master
- architecture changes require explicit user approval
- destructive git commands are blocked via `kilo.json` permissions

---

## 19. Local-Only Boundary

This architecture is **committed** to the repository. All `.kilo/` contents, `AGENTS.md`, `kilo.json`, and `scripts/` are part of the project.

---

## 20. Build/Lint Warning Policy

- C# compiler warnings should be resolved before merge
- Unity console errors/warnings should be addressed in the same PR
- Lint/format warnings from `dotnet-format` are auto-fixed via `/format` command

---

## 21. Agent Registry

All agents must know this registry:

| Agent | Type | Delegatable |
| --- | --- | --- |
| Workflow Orchestrator | orchestrator | — (entry point) |
| Architect/Planner | planner | yes |
| Expert Unity Developer | specialist (implementation) | yes |
| Game Designer | specialist (game feel/UX) | yes |
| QA | specialist (tests) | yes |
| Security Reviewer | specialist (security) | yes |
| Performance Analyst | specialist (profiling) | yes |
| Documentation Agent | specialist (docs/memory) | yes |

Compact form: Orchestrator → Expert Unity Developer, Game Designer, QA, Security Reviewer, Performance Analyst, Documentation Agent.
