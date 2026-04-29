# Agent Architecture Blueprint Template — Kilo Code Edition

Use this blueprint to create a repo-local multi-agent orchestration system with the same structure and operating model as the reference architecture, adapted for **Kilo Code** (kilo-ai/kilocode CLI), while keeping the generated result project-agnostic.

This document is intentionally detailed. It is not a compact summary. It is a build specification for an LLM that must generate a full architecture from scratch without silently dropping contracts.

**Origin:** This is an adaptation of the Claude Code Agent Architecture Blueprint Template, mapped 1:1 to Kilo Code's configuration model: agents, commands, skills, permissions, MCP servers, and instruction files.

---

## 1. Purpose

This blueprint defines:

- the canonical orchestration topology
- the required files and folders (adapted to `.kilo/` layout)
- the rule hierarchy and source-of-truth order
- the agent role boundaries
- the planning, dispatch, verification, and documentation lifecycle
- the permission-driven enforcement model (replacing hooks)
- the durable memory model
- the validation and drift checks required after generation

Use it when you want a new repository to inherit the same operating model, not merely similar prose.

---

## 2. Blueprint Consumption Rules For The LLM

When using this blueprint to generate a new architecture, follow these rules exactly:

1. Be maximally detail-oriented. Do not collapse important contracts into vague summaries.
2. Preserve the architecture shape even if stack-specific names, tools, or models change.
3. Separate generic orchestration rules from project-specific rules. Do not mix them.
4. Plan and generate the architecture atomically, section by section. Do not skip ahead and backfill later.
5. Treat every section in this blueprint as a deliverable. If a section is not applicable, say why explicitly instead of omitting it.
6. Do not invent unsupported platform capabilities. If a capability depends on the host environment, document it as environment-dependent.
7. Use official documentation when deciding current platform features, agent behavior, model options, MCP support, or workspace capabilities.
8. Research the most recent environment settings and optional capabilities that could improve orchestration quality, parallelization, visibility, safety, or autonomy. Do not silently enable them in the generated repo unless the user explicitly wants that.
9. At the end of the generation process, provide the user with a dedicated list of optional settings and capabilities they may choose to enable, including what each one improves and why it is relevant.
10. Keep completion boilerplate short inside specialist agent files. Preserve role rules; compress only repetitive close-out phrasing.
11. Do not create any duplicate memory surface outside `memories/`.
12. End the generation process with a TODO list covering all remaining execution points of attention.
13. The last TODO item must explicitly require an audit that compares the generated architecture against this blueprint and lists any mismatches.

---

## 3. Kilo Code Platform Reference

This section documents the Kilo Code capabilities relevant to multi-agent orchestration. All subsequent sections assume this model.

### 3.1 Configuration Model

Kilo Code loads configuration from multiple sources (low-to-high precedence):

- `~/.config/kilo/kilo.json` (global)
- Project `./kilo.json`, `./kilo.jsonc`, `.kilo/kilo.json`
- Environment variables: `KILO_CONFIG`, `KILO_CONFIG_DIR`, `KILO_CONFIG_CONTENT`

Key config fields for orchestration:

```jsonc
{
  "model": "provider/model",          // default model
  "small_model": "provider/model",    // model for titles/summaries
  "default_agent": "agent-name",      // default primary agent
  "instructions": ["**/*.md"],        // additional instruction files
  "permission": { ... },              // tool permission matrix
  "mcp": { ... },                     // MCP server definitions
  "skills": { ... },                  // skill paths/URLs
  "plugin": [...]                     // plugin specifiers
}
```

### 3.2 Agent System

Agents live in `.kilo/agent/*.md` (or `agents/`). The filename (minus `.md`) becomes the agent identifier.

Agent frontmatter shape:

```yaml
---
description: 'When to use this agent'
mode: primary          # primary | subagent | all
model: xiaomi/mimo-v2.5-pro  # optional override
steps: 25              # max agentic iterations
hidden: false          # hide from @ menu (subagent only)
color: "#FF5733"       # hex or theme name
permission:            # optional, agent-level permissions
  bash: allow
  edit:
    "src/**": allow
    "*": ask
---
System prompt for this agent.
```

**Mode values:**
- `primary` = selectable as main agent by the user (equivalent to Claude Code `user-invocable: true`)
- `subagent` = only callable via `task` tool from another agent (equivalent to Claude Code `user-invocable: false`)
- `all` = both primary and subagent

### 3.3 Built-In Tools

| Kilo Code Tool | Claude Code Equivalent | Purpose |
| --- | --- | --- |
| `read` | `Read` | Read file contents |
| `edit` | `Edit` | String replacement in files |
| `write` | `Write` | Write/overwrite files |
| `glob` | `Glob` | File pattern matching |
| `grep` | `Grep` | Content search (regex) |
| `bash` | `Bash` | Execute shell commands |
| `task` | `Agent` | Launch a subagent |
| `question` | `AskUserQuestion` | Structured user questions |
| `todowrite` | `TodoWrite` | Create/manage task list |
| `todoread` | — | Read task list state |
| `webfetch` | `WebFetch` | Fetch URL content |
| `websearch` | `WebSearch` | Web search |
| `codesearch` | — | Code-specific search |
| `skill` | — | Load a skill file |
| `plan_exit` | — | Signal plan completion |
| `lsp` | — | Language server integration |
| `kilo_local_recall` | — | Search/read past conversations |

### 3.4 Task Delegation

The `task` tool is the Kilo Code equivalent of Claude Code's `Agent` tool. It launches a subagent with a detailed prompt:

```jsonc
{
  "description": "Short task description",
  "prompt": "Detailed autonomous instructions...",
  "subagent_type": "explore" | "general"  // agent type selection
}
```

Multiple `task` calls can be made in parallel in the same response, enabling wave-based parallelization.

### 3.5 Commands

Commands are `.kilo/command/*.md` files. The filename (minus `.md`) becomes the command name invoked via `/name`. They can route to specific agents, override models, and accept arguments (`$1`–`$N`, `$ARGUMENTS`).

### 3.6 Skills

Skills are `SKILL.md` files inside `.kilo/skill/<name>/` or `.kilo/skills/<name>/`. They provide reusable capability packs loaded via the `skill` tool.

### 3.7 Hooks — Kilo Code Equivalent

**Kilo Code does not have a native hook system** (no PreToolUse, PostToolUse, Stop). The following equivalents must be used:

| Claude Code Hook | Kilo Code Equivalent |
| --- | --- |
| `PreToolUse` (block dangerous) | `permission.bash` with `deny` patterns in `kilo.json` |
| `PostToolUse` (auto-format) | Command `/format` or explicit formatting instruction in `AGENTS.md` |
| `Stop` (verify completion) | Command `/verify-completion` or inline close-out protocol in orchestrator agent |
| Context injection | `instructions` field in `kilo.json` or `AGENTS.md` system prompt content |

When generating the architecture, you MUST document how each enforcement mechanism is implemented in Kilo Code's model.

---

## 4. What Must Stay Generic Vs What Must Be Project-Specific

| Layer | Must be generic in the blueprint | Must become project-specific when instantiated |
| --- | --- | --- |
| Topology | Orchestrator → Planner → specialists → verification → documentation | exact stack specialist names if the target repo is not web/UI based |
| Rules | routing, verification, close-out phases, preflight, memory policy, parallelization contract | product-specific UI rules, test caveats, build quirks, domain policies |
| Files | folder layout, agent-file structure, memory folder structure | repo names, stack files, concrete script bodies |
| Models | role-based selection strategy | exact current model names and fallbacks |
| Tools | tool categories per role | exact tool identifiers available in the target environment |
| MCP / tool servers | decision framework and optionality | exact servers, secrets strategy, project-specific integrations |
| Permissions | pattern-based deny/allow/ask strategy | exact glob patterns for project-specific paths |
| Local-only policy | whether a local-only boundary exists | exact paths and whether the architecture is committed or excluded |

Rule: if a fact would only be true for one project, it belongs in the generated repo's project-specific overlay, not in the generic architecture contract.

---

## 5. Canonical Topology

Default role topology:

```text
User
  └─▶ Workflow Orchestrator          (entry point, mode: primary, never implements)
        └─▶ Architect/Planner        (task decomposition, DoD, parallelization design)
              ├─▶ Expert <Stack> Developer   (implementation)
              ├─▶ UX/UI Designer             (visual/accessibility audit, if relevant)
              ├─▶ QA                         (tests, quality verification)
              ├─▶ Security Reviewer          (security verification)
              ├─▶ Performance Analyst        (performance profiling, if relevant)
              ├─▶ E2E Tester                 (browser or E2E verification, if relevant)
              └─▶ Documentation Agent        (always last)
```

Topology rules:

- `Workflow Orchestrator` is the only user-facing entry point (`mode: primary`).
- `Architect/Planner` runs before any non-trivial implementation.
- Specialists do domain work (`mode: subagent`).
- Verification is independent from implementation.
- `Documentation Agent` is always the final wave.

If the target repo is not a web UI repo:

- keep the orchestration shape
- rename stack-specific specialists as needed
- preserve role boundaries
- preserve the planner/orchestrator parallelization contract
- preserve the independent verification wave
- preserve the final documentation wave

---

## 6. Canonical Source-Of-Truth Order

The generated architecture should make this hierarchy explicit:

| Priority | Artifact | Purpose |
| --- | --- | --- |
| 1 | `AGENTS.md` | canonical architecture contract and cross-role rules |
| 2 | `.kilo/agent/*.md` | role-local contracts, tools, boundaries, preflight, completion rules |
| 3 | `kilo.json` | permissions, MCP servers, model config, instruction globs |
| 4 | `.kilo/command/*.md` | executable command workflows (close-out, formatting, context injection) |
| 5 | `AGENTS.md` (instructions section) | stable repository facts and shared coding norms |
| 6 | `.kilo/skill/*/SKILL.md` | optional reusable capability packs |
| 7 | `memories/repo/*.md` | durable current-state project memory |
| 8 | `memories/orchestration-suggestions.md` | deferred orchestration improvements |

Conflict rule:

- `AGENTS.md` wins over secondary documentation when prose conflicts
- `kilo.json` permission rules are enforced at runtime — they override prose
- role-local agent files refine, but do not weaken, the root architecture contract
- memory stores current truth only; it does not override architecture contracts

---

## 7. Filesystem Blueprint

Create this structure, adjusting only names that are genuinely stack-specific:

```text
<repo-root>/
├─ AGENTS.md
├─ kilo.json
├─ .kilo/
│  ├─ agent/
│  │  ├─ workflow-orchestrator.md
│  │  ├─ architect-planner.md
│  │  ├─ expert-<stack>-developer.md
│  │  ├─ ux-ui-designer.md                  # omit only if the product has no UI surface
│  │  ├─ qa-subagent.md
│  │  ├─ security-reviewer.md
│  │  ├─ performance-analyst.md             # optional, if performance is a concern
│  │  ├─ e2e-tester.md                      # rename only if another E2E tool is canonical
│  │  └─ documentation-agent.md
│  ├─ command/
│  │  ├─ verify-completion.md               # close-out verification workflow
│  │  ├─ format.md                          # auto-format workflow
│  │  ├─ inject-context.md                  # context injection workflow
│  │  └─ <optional-commands>.md
│  ├─ skill/
│  │  └─ <optional-skill>/
│  │     └─ SKILL.md
│  └─ plans/                                # plan files (auto-managed)
├─ scripts/
│  ├─ block-dangerous.sh                    # reference script for destructive command patterns
│  ├─ format-changed.sh                     # formatting script (invoked by /format command)
│  ├─ inject-context.sh                     # context injection (invoked by /inject-context command)
│  └─ verify-completion.sh                  # close-out verification (invoked by /verify-completion command)
├─ memories/
│  ├─ repo/
│  │  └─ <repo-name>-state.md
│  └─ orchestration-suggestions.md
```

Do not create:

- any other memory file outside `memories/`
- hidden duplicate copies of the architecture contract in random markdown files

---

## 8. `kilo.json` Required Structure

The generated `kilo.json` must contain:

```jsonc
{
  "$schema": "https://app.kilo.ai/config.json",
  "model": "<default-model>",               // e.g. "xiaomi/mimo-v2.5-pro"
  "small_model": "<small-model>",           // for titles/summaries
  "default_agent": "workflow-orchestrator",  // orchestrator is default
  "instructions": [                          // additional instruction globs
    "AGENTS.md",
    "**/*.instructions.md"
  ],
  "permission": {
    // Destructive command blocking (replaces PreToolUse hook)
    "bash": {
      "rm -rf *": "deny",
      "git push --force *": "deny",
      "mkfs.*": "deny",
      "*": "ask"                             // default: ask for all bash
    },
    // File editing boundaries
    "edit": {
      "src/**": "allow",
      "*.lock": "deny",
      "*": "ask"
    },
    // External directory access
    "external_directory": "deny"
  },
  "mcp": {
    // Project-specific MCP servers here
  },
  "skills": {
    "paths": [".kilo/skill"]
  }
}
```

### 8.1 Permission Strategy

The `permission` field in `kilo.json` is the primary enforcement mechanism. It replaces Claude Code's hook system.

**Permission actions:**
- `"allow"` — auto-approve, no user interaction
- `"ask"` — prompt user for approval
- `"deny"` — block silently or with error

**Enforcement mapping:**

| Behavior | Kilo Code Implementation |
| --- | --- |
| Destructive command blocking | `permission.bash` with deny patterns |
| Auto-format after edits | `AGENTS.md` instruction + `/format` command |
| Context injection | `instructions` field in `kilo.json` + AGENTS.md content |
| Close-out verification | orchestrator agent system prompt + `/verify-completion` command |
| MCP/tool-server usage | `permission` patterns for MCP tools (e.g. `"mcp__server__tool": "ask"`) |
| File edit boundaries | `permission.edit` with glob patterns |

### 8.2 MCP Permission Patterns

MCP tools use permission keys in the format `{server}_{tool}`. Glob patterns are supported:

```jsonc
{
  "permission": {
    "unity_*": "ask",                    // require approval for all Unity MCP tools
    "unity_ReadConsole": "allow",        // auto-approve safe read-only tool
    "unity_DeleteAsset": "deny"          // block dangerous tool
  }
}
```

---

## 9. Root `AGENTS.md` Required Structure

The generated root `AGENTS.md` should contain, in this order:

1. Title and short repository identifier
2. High-level architecture diagram
3. Core Principles
4. Project-Specific Rules
5. Model Selection table
6. Platform capabilities and enforcement table
7. Close-out phases
8. Hard-enforced vs audit-only matrix
9. MCP or tool-server decision guide
10. Git / worktree policy
11. Local-only boundary policy, if used
12. Build/lint warning policy, if relevant

Why this order matters:

- the first half establishes the operating contract
- the second half describes runtime enforcement and environment usage without scattering the same capability facts across multiple sections
- project-specific rules stay visibly separate from the generic architecture

---

## 10. Core Architecture Contract

The generated `AGENTS.md` must represent these rules explicitly.

1. Every multi-step task goes through `Workflow Orchestrator` first.
2. `Workflow Orchestrator` routes every request to a dedicated agent.
3. `Workflow Orchestrator` is the source of truth between the user request and delivered work.
4. The Orchestrator asks for clarification before delegating if the request is materially ambiguous or incomplete.
5. `Architect/Planner` runs before implementation on any non-trivial task.
6. The Orchestrator delegates through the `task` tool to the appropriate specialist for the task.
7. Independent subtasks dispatch in parallel within the same wave (multiple `task` calls in one response).
8. Any code change triggers an independent verification wave, at minimum `QA` plus `Security Reviewer`.
9. Any UI change additionally triggers a visual/accessibility audit plus browser/E2E verification.
10. The Orchestrator never trusts self-reports as proof of completion.
11. `Documentation Agent` is always the final wave.
12. The Orchestrator is the only cross-role router.
13. Eligible specialists may self-delegate only within their own role or inherited worker scope.
14. Simplicity beats over-engineering.
15. All agents maintain progress tracking via `todowrite`.
16. All agents know about all other agents through an Agent Registry section in their file.
17. Official documentation lookup is mandatory for planning, large implementation/refactor work, and audits/reviews involving framework or library contracts.
18. Architecture changes require explicit user approval.
19. Fix root causes, not symptoms.
20. Preserve single sources of truth for reusable values and contracts.
21. Prefer existing abstractions before creating new ones.
22. Extract reusable logic only when justified.
23. The Planner uses the `question` tool for material trade-offs or user-only blockers.
24. The Planner lists adjacent work as optional follow-up, not as assumed scope.
25. The Planner may propose a better approach, but execution follows the user's decision if the user declines.
26. Verification loops are capped at three fix → verify iterations.
27. QA prioritizes blocking failures before minor polish.
28. The orchestration self-audit follows a canonical priority order.
29. Close-out artifacts are produced only when the close-out protocol requests them; do not emit the DoD summary table, Files Changed table, or orchestration self-review proactively.
30. Agents do not re-ask decisions already answered through `question` tools during the same session.
31. The Planner is the primary owner of parallelization design.
32. The Orchestrator is the primary owner of parallelization execution.
33. Specialists still assess additional fan-out opportunities during execution.
34. Orchestration-file changes require an independent consistency pass.
35. Agents verify working directory before terminal commands.
36. The self-audit fallback wording is exact and canonical.
37. Every specialist must emit a structured preflight before substantive work.
38. The Orchestrator's user-facing reporting must stay direct and concise.

### 10.1 Parallelization Contract

This is the most important coordination rule after routing.

**Planner responsibilities:**

- split work into atomic, parallel-safe subtasks
- specify wave boundaries
- specify exactly which subtasks run in parallel
- specify when multiple instances of the same agent should be launched (via multiple `task` calls)
- specify verification fan-out by lane when possible
- specify fallback fan-out if a wave is likely to return many independent failures

**Orchestrator responsibilities:**

- treat the Planner's parallelization instructions as execution requirements
- launch all independent same-wave subtasks together (batch `task` calls in one response)
- maximize safe same-role fan-out instead of serializing independent work
- prefer the highest useful number of same-role instances when work is separable
- re-fan-out verification or fixes if failures cluster into independent concerns

**Specialist responsibilities:**

- assess whether more same-role fan-out became possible after work began
- self-delegate within role when the environment allows it and the speedup is material
- otherwise report a concrete fan-out recommendation back to the Orchestrator immediately

Example:

- a test audit can spawn separate `QA` instances for unit, integration, smoke, and E2E-adjacent non-browser checks
- a browser verification wave can split `E2E Tester` work by independent flows
- if ten failures return and they separate into independent buckets, do not serialize them by default; split them into workers or ask the Orchestrator to do so

### 10.2 Tool Parallelism Vs Worker Subagents

The generated docs should explain the difference clearly:

- batched reads/searches/web fetches in one turn are tool parallelism inside one agent session (multiple tool calls in a single response)
- worker subagents are separate same-role executions created via `task` tool for isolation or speed
- same-role worker use is allowed only if the environment supports it
- same-role worker use never grants cross-role routing authority

---

## 11. Orchestration Self-Audit Priority Order

The generated architecture should keep this canonical order:

1. be reliable
2. be as fast as possible on implementation through good parallelization
3. perform self-audit
4. run reliable tests on each iteration
5. write DoD with evidence and reports
6. be autonomous
7. be cost-effective

For each principle, the fallback when no issue is found must be exactly:

`No improvement identified for this principle.`

Do not paraphrase the fallback.

---

## 12. Rule Ownership Matrix

Make rule ownership explicit so future drift is easy to spot.

| Rule area | Primary owner | Secondary owner | Runtime owner |
| --- | --- | --- | --- |
| Cross-role routing | `Workflow Orchestrator` | `AGENTS.md` | n/a |
| Planning and DoD definition | `Architect/Planner` | `AGENTS.md` | n/a |
| Parallelization design | `Architect/Planner` | `AGENTS.md`, Planner output format | n/a |
| Parallelization execution | `Workflow Orchestrator` | specialist preflight and universal rules | close-out command audit pressure |
| Specialist boundaries | agent files | `AGENTS.md` | n/a |
| Verification wave | `Workflow Orchestrator` | Planner output format | n/a |
| Close-out phases | `AGENTS.md` | Orchestrator file | `/verify-completion` command |
| Context injection | `kilo.json` instructions | `AGENTS.md` | startup |
| Destructive command blocking | `kilo.json` permissions | `AGENTS.md` | runtime permission enforcement |
| Auto-format | `AGENTS.md` instruction | `/format` command | agent adherence |
| Durable project memory | `Documentation Agent` + `memories/` | `AGENTS.md` | n/a |

---

## 13. Agent File Requirements

Every agent file must contain:

- YAML frontmatter
- a role summary
- explicit responsibilities or core mandate
- an Agent Registry listing all registered roles
- role boundaries
- any shared universal rules that the role must obey
- a short completion protocol

If you rename agents, update:

- frontmatter (filename becomes agent name in Kilo Code)
- every registry
- Orchestrator registry
- Planner plan format
- handoffs
- any command files or scripts that mention the agent by name

### 13.1 Common Frontmatter Shape

Use this as the baseline:

```yaml
---
description: '<Role summary — shown when selecting agents>'
mode: subagent                   # primary | subagent | all
model: provider/model            # optional, override kilo.json default
steps: 25                        # max agentic iterations (tune per role)
hidden: false                    # hide from @ menu (subagent only)
color: "#FF5733"                 # hex or theme name (optional)
permission:                      # optional, agent-level permission overrides
  bash: allow
  edit:
    "src/**": allow
    "*": ask
---
```

Baseline rules:

1. Only the `Workflow Orchestrator` has `mode: primary` (or `mode: all`).
2. All specialists have `mode: subagent`.
3. The Orchestrator's system prompt lists every callable specialist by name.
4. Specialists should not be user-invocable by default.
5. Agent Registries may be a compact one-line list if all roles remain visible.
6. Completion protocols should be brief. Preserve substance, not repetitive handoff prose.

**Naming convention:** The filename (minus `.md`) is the agent identifier. Use kebab-case: `workflow-orchestrator.md`, `architect-planner.md`, `expert-unity-developer.md`.

### 13.2 Model Selection Strategy

Use this as a template:

| Role | Primary Model | Rationale |
| --- | --- | --- |
| Workflow Orchestrator | `xiaomi/mimo-v2.5-pro` | best routing and coordination |
| Architect/Planner | `xiaomi/mimo-v2.5-pro` | best decomposition quality |
| Expert <Stack> Developer | `xiaomi/mimo-v2.5-pro` | fast, accurate implementation |
| UX/UI Designer | `xiaomi/mimo-v2.5-pro` | fast visual assessment |
| QA | `xiaomi/mimo-v2.5-flash` | fast test execution |
| Security Reviewer | `xiaomi/mimo-v2.5-pro` | best security analysis |
| Performance Analyst | `xiaomi/mimo-v2.5-pro` | fast profiling analysis |
| E2E Tester | `xiaomi/mimo-v2.5-flash` | fast browser interaction |
| Documentation Agent | `xiaomi/mimo-v2.5-flash` | fast documentation writing |

These models should be set in each agent's frontmatter `model` field. The `kilo.json` `model` field serves as the default fallback.

---

## 14. `Workflow Orchestrator` Contract

The Orchestrator file is the heart of the runtime architecture.

### 14.1 Required Identity

- entry point for all user work
- never implements
- only cross-role router
- tracks DoD
- launches independent verification
- reports concisely

### 14.2 Required Tools

The orchestrator system prompt should reference these Kilo Code tools:

| Tool | Purpose |
| --- | --- |
| `read`, `grep`, `glob` | search/read for understanding requests |
| `task` | delegate to specialists (primary delegation mechanism) |
| `todowrite` | track DoD and progress |
| `question` | ask user for clarification when blocked or ambiguous |
| `websearch`, `webfetch` | official documentation lookup |
| `skill` | load optional skill packs |

The orchestrator should NOT have `edit`, `write`, or `bash` tools (or they should be denied via `permission` in frontmatter) — it must never implement directly.

### 14.3 Required Workflow Sections

The orchestrator system prompt should contain these workflow phases in order:

1. **Understand and clarify** — read the request, use `question` if materially ambiguous
2. **Plan** — delegate to `Architect/Planner` via `task` for any non-trivial work
3. **Dispatch in waves** — launch specialists via `task`, maximize parallel fan-out
4. **Independent verification** — minimum `QA` + `Security Reviewer`; add role-specific verifiers when relevant
5. **Documentation wave** — always last via `Documentation Agent`
6. **Completion close-out** — concise user report (status, blockers, verification outcome, next step)

### 14.4 Absolute Rules The Orchestrator Must Carry

- never write code or run commands directly
- always delegate through `task`
- never bypass the Planner for non-trivial work
- never skip the Documentation Agent
- never trust self-reports as proof of completion
- use `question` when blocked or ambiguous
- respect prior `question` answers
- prefer simple root-cause fixes
- execute the Planner's parallelization plan aggressively
- maximize same-role fan-out when safe
- keep user reporting direct and token-efficient

### 14.5 Concise Reporting Requirement

The Orchestrator's final user-facing reporting should prefer:

- current status
- blockers
- verification outcome
- next decision or next step

Avoid long narrative summaries unless the user explicitly asks for them.

Close-out artifacts such as the DoD summary table, Files Changed table, and orchestration self-review should appear only when the close-out protocol or `/verify-completion` command requests them.

---

## 15. `Architect/Planner` Contract

The Planner is responsible for decomposition quality and explicit parallelization design.

### 15.1 Required Identity

- never implements
- reads existing code and docs before planning
- decomposes into atomic subtasks
- assigns each subtask to one exact agent
- defines verifiable DoD
- flags blockers
- asks material questions when needed (via `question`)
- defines wave-level parallelization

### 15.2 Required Planner Output

The generated Planner file should include a structured output format similar to this:

```markdown
## Task Plan: <Name>

**Complexity:** Simple | Medium | Complex
**Agents:** <list>
**Blockers:** <list or None>

### Wave 1 — <Phase Name>

**Parallelization:** <exact same-wave launch instructions, same-agent instance counts, fallback split if the wave may return many independent failures>

#### Subtask 1.1 — <Name>
**Agent:** <Exact Agent Name>
**Depends on:** none
**Files:** <list>
**Instruction:** <precise instruction>
**DoD:**
- [ ] <criterion>

#### Subtask 1.2 — <Name> ⇄ parallel with 1.1
**Agent:** <Exact Agent Name>
**Depends on:** none
**Files:** <list>
**Instruction:** <precise instruction>
**DoD:**
- [ ] <criterion>

### Wave N-1 — Verification

**Agent:** QA + Security Reviewer (+ UX/UI Designer + E2E Tester + Performance Analyst when relevant)
**Depends on:** all implementation waves
**Parallelization:** <explicit lane split, for example QA x3 by lane>
**DoD:**
- [ ] <criterion>

### Wave N — Documentation

**Agent:** Documentation Agent
**Depends on:** all previous waves
**DoD:**
- [ ] relevant memory updated if durable state changed
- [ ] orchestration suggestions updated if a deferred improvement should be recorded
- [ ] shared instructions updated if warranted
```

### 15.3 Planner-Specific Requirements

- use official docs before planning framework- or library-dependent work (via `webfetch`)
- keep optional follow-up separate from committed scope
- design FE and BE as parallel tracks by default when both exist and no contract dependency blocks it
- design verification fan-out explicitly
- design fallback fan-out for likely failure clusters

---

## 16. Specialist Agent Contract

This section applies to implementation, QA, security, UX/UI, E2E, performance, and documentation specialists.

### 16.1 Shared Specialist Rules

Every specialist must:

- stay within role boundaries
- maintain progress tracking via `todowrite`
- preserve simplicity
- use official docs when required (via `webfetch`)
- respect prior user answers
- emit the mandatory preflight before substantive work
- assess parallelization before starting
- use tool batching first for cheap fan-out (multiple tool calls in one response)
- use same-role worker fan-out via `task` when materially useful
- escalate blockers instead of guessing
- keep completion reporting short

### 16.2 Mandatory Specialist Preflight

Every specialist must emit exactly these two ordered sections before substantive work:

1. `Task plan` — the high-level steps the specialist will take
2. `Parallelism strategy` — tool batching and/or same-role worker approach, or `No parallelism applicable to this task.`

This contract should appear both:

- in the root architecture contract (`AGENTS.md`)
- in each specialist file

### 16.3 Implementation Specialist

The implementation specialist should:

- diagnose before editing
- implement the smallest correct fix or feature slice
- verify changed behavior directly
- avoid creating abstractions without justification
- preserve single sources of truth

### 16.4 QA Specialist

The QA specialist should:

- verify changed code independently
- run the relevant test lanes
- prioritize broken functionality and regressions first
- split verification by lane when possible
- ask for additional same-role fan-out if many failures are independent

### 16.5 Security Reviewer

The Security Reviewer should:

- audit changed code for exploitable flaws
- review dependency/CVE risk and policy compliance
- report concrete findings, scope reviewed, residual risks, and coverage gaps

### 16.6 UX/UI Designer

The UX/UI specialist should:

- assess visual fidelity, interaction quality, accessibility, and design consistency
- use screenshots or browser-level inspection when appropriate
- report findings succinctly and concretely

### 16.7 E2E Tester

The browser/E2E specialist should:

- validate user-facing flows
- debug failing browser flows when needed
- split independent flow clusters into parallel same-role work when beneficial

### 16.8 Performance Analyst

The Performance specialist should:

- profile runtime behavior (memory, CPU, frame time, etc.)
- identify bottlenecks, leaks, and regression risks
- report metrics, findings, optimization recommendations, and estimated impact

### 16.9 Documentation Agent

The Documentation Agent should:

- run last
- never implement product code
- update only durable current-state memory under `memories/`
- update `AGENTS.md` or project docs when architecture or stable repo facts changed
- record deferred orchestration improvements in `memories/orchestration-suggestions.md` separately from durable project memory

Do not make the generated Documentation Agent depend on any deleted or duplicate memory surface outside `memories/`.

---

## 17. Enforcement Model

The generated architecture replaces Claude Code's hook-based enforcement with Kilo Code's permission + command + instruction model.

### 17.1 Enforcement Layers

| Layer | Mechanism | Enforcement Level |
| --- | --- | --- |
| Destructive command blocking | `kilo.json` `permission.bash` deny patterns | Hard (runtime-enforced) |
| File edit boundaries | `kilo.json` `permission.edit` glob patterns | Hard (runtime-enforced) |
| MCP tool permissions | `kilo.json` `permission` MCP key patterns | Hard / Ask (runtime-enforced) |
| Context injection | `kilo.json` `instructions` field + AGENTS.md content | Hard (startup-loaded) |
| Auto-format | `AGENTS.md` instruction + `/format` command | Instruction-only (agent adherence) |
| Close-out verification | orchestrator system prompt + `/verify-completion` command | Instruction-only (agent adherence) |
| Parallelization pressure | orchestrator/planner contracts + self-audit | Audit-only |
| Terminal reuse | `AGENTS.md` instruction | Instruction-only |

### 17.2 Hard-Enforced Rules (via `kilo.json` permissions)

These cannot be bypassed by the agent:

- destructive bash commands matching deny patterns
- file edits outside allowed glob patterns
- MCP tool invocations matching deny/ask patterns
- external directory access when denied

### 17.3 Instruction-Only Rules (via `AGENTS.md` and agent files)

These rely on agent adherence:

- close-out phase checks
- auto-format after edits
- preflight emission before substantive work
- parallelization execution
- documentation wave ordering

### 17.4 Close-Out Protocol

The orchestrator and all specialists must follow this close-out protocol at the end of any multi-agent task. This replaces Claude Code's Stop hook.

**Phase 1 — Artifacts:**

- produce a DoD summary table with evidence
- produce a Files Changed table

**Phase 2 — Self-audit:**

- review the seven canonical self-audit principles in order
- for each principle, if no improvement is found, use exactly: `No improvement identified for this principle.`
- do not paraphrase this fallback wording

This protocol is defined in `AGENTS.md` and reinforced in the orchestrator's system prompt. A `/verify-completion` command may optionally enforce it.

---

## 18. Commands Layer

Commands in `.kilo/command/` provide reusable workflows. Generate these commands:

### 18.1 `/verify-completion`

**File:** `.kilo/command/verify-completion.md`

```yaml
---
description: Verify task completion artifacts and self-audit
agent: workflow-orchestrator
---
```

Content should enforce the two-phase close-out protocol: DoD table + Files Changed table + seven-principle self-audit with exact fallback wording.

### 18.2 `/format`

**File:** `.kilo/command/format.md`

```yaml
---
description: Auto-format changed files
---
```

Content should invoke `scripts/format-changed.sh` or equivalent formatting logic for the project's stack.

### 18.3 `/inject-context`

**File:** `.kilo/command/inject-context.md`

```yaml
---
description: Inject project context into the current session
---
```

Content should output or reference the project's canonical context block (equivalent to `scripts/inject-context.sh`).

### 18.4 Project-Specific Commands

Add any stack-specific commands as needed. Examples:

- `/build` — run the project build
- `/test` — run the test suite
- `/deploy` — deployment workflow

---

## 19. Instructions Layer

The generated repo should have three instruction layers:

1. `AGENTS.md` for the architecture contract
2. `AGENTS.md` (project-specific section) for stable repository facts and coding norms
3. `kilo.json` `instructions` globs for path-specific or domain-specific rules (e.g. `"**/*.instructions.md"`)

Layering rules:

- avoid duplicating the same large rule block in every file
- repeat only what is necessary for role adherence or runtime correctness
- keep project-specific facts out of the generic architecture template
- keep completion boilerplate brief

---

## 20. Memory Layer

Memory lives only under `memories/`.

### 20.1 Durable Project Memory

Use `memories/repo/*.md` for current-state facts that help future work:

- business logic and behavior that matters later
- major codebase contracts
- build, test, deploy, environment, or integration behavior
- durable repo conventions that are not already obvious from code

Do not store:

- temporary debugging notes
- stylistic nits
- minor one-off fixes
- historical change logs
- duplicate copies of architecture rules already canonical elsewhere

### 20.2 Deferred Orchestration Improvements

Use `memories/orchestration-suggestions.md` for architecture improvements that are intentionally deferred.

Recommended structure:

- group by the canonical seven self-audit principles
- keep entries dated
- keep entries actionable
- remove or rewrite stale suggestions when implemented or superseded

### 20.3 Memory Rules

- keep only the current truth
- remove stale or conflicting memory instead of appending history forever
- do not create any separate memory surface outside `memories/`
- do not scatter memory across unrelated markdown files

### 20.4 Kilo Code Memory Integration

Kilo Code also provides `kilo_local_recall` for searching and reading past conversations within the project. The Documentation Agent may use this tool to:

- recall how previous changes were implemented
- find session transcripts relevant to current work
- avoid re-documenting facts already captured in prior sessions

---

## 21. Skills Layer

`.kilo/skill/` is optional.

Use it when:

- a recurring orchestration behavior needs reusable local instructions
- the repo benefits from specialized capability packs

Do not use it as a dumping ground for generic rules already covered by:

- `AGENTS.md`
- agent files
- `kilo.json` instructions

### 21.1 Skill Structure

Each skill is a directory with a `SKILL.md` file:

```
.kilo/skill/
└─ <skill-name>/
   ├─ SKILL.md
   └─ <optional-scripts-or-assets>
```

`SKILL.md` frontmatter:

```yaml
---
name: <skill-name>
description: <what this skill does>
---
Skill instructions in markdown.
```

Skills are loaded via the `skill` tool from within any agent's system prompt or runtime execution.

---

## 22. MCP / Tool-Server Layer

Treat this as optional and environment-dependent.

### 22.1 Required Blueprint Behavior

When generating a new architecture:

- decide whether MCP/tool servers are actually useful for the target repo
- document when each server should and should not be used
- avoid hardcoding a secrets strategy in the generic blueprint
- make secret provisioning a project decision

### 22.2 Generic Decision Guide

Keep a decision guide for any configured external tool server in `kilo.json`:

- browser automation / render inspection
- browser runtime debugging
- backend or database inspection
- project-specific external integration

For each, document in `AGENTS.md`:

- when to use it
- when not to use it
- primary role owners

A concise table is preferred when the same guidance already exists canonically elsewhere.

### 22.3 MCP Server Configuration

MCP servers are defined in `kilo.json`:

```jsonc
{
  "mcp": {
    "server-name": {
      "type": "local",                    // or "remote"
      "command": ["node", "server.js"],   // for local
      "url": "https://mcp.example.com",   // for remote
      "environment": { "KEY": "value" },
      "headers": { "Authorization": "Bearer ..." },
      "enabled": true,
      "timeout": 10000
    }
  }
}
```

### 22.4 Settings And Capability Discovery Requirement

Before finalizing the generated architecture, the LLM should review the latest official Kilo Code documentation for settings or capabilities that may improve:

- agent orchestration
- permissions
- commands
- question tools
- memory access
- model routing
- MCP/tool-server integration
- skill loading
- plan mode

Then provide the user with a dedicated end-of-process list:

| Setting or capability | Benefit | Why relevant here | Recommended? | User decision |
| --- | --- | --- | --- | --- |
| `<name>` | `<benefit>` | `<reason>` | Yes/No | Pending |

This list is informational. The user decides what to enable.

---

## 23. Local-Only Vs Committed Boundary

The blueprint should force a deliberate decision:

- Will the orchestration architecture live only locally?
- Or will it be committed into the repository?

If local-only:

- define the exact local-only paths (typically `.kilo/` contents)
- define how they stay out of version control (`.gitignore`)
- keep that policy out of committed repo docs unless intentionally desired

If committed:

- remove any local-only instructions that would contradict the chosen model

Do not leave this ambiguous in the generated architecture.

---

## 24. Bootstrap Procedure For A New Repo

Generate the new architecture in this order:

1. Identify the target stack, runtime, and whether the repo has UI, backend, or both.
2. Decide the final specialist roster while preserving the same topology.
3. Choose exact role names and keep them stable.
4. Draft `AGENTS.md` first, including generic rules and a clearly separated project-specific section.
5. Create `kilo.json` with permissions, MCP servers, model config, and instruction globs.
6. Create all agent files under `.kilo/agent/` with frontmatter, responsibilities, registry, boundaries, and short completion protocols.
7. Create command files under `.kilo/command/` for close-out, formatting, and context injection.
8. Create `scripts/` with shell script implementations for formatting, context injection, and verification.
9. Create the `memories/` structure.
10. Decide whether skills are needed under `.kilo/skill/`.
11. Decide whether MCP/tool servers are needed.
12. Run a consistency pass across names, models, tools, and question headers.
13. Run a drift pass comparing prose rules against `kilo.json` permissions and agent frontmatter.
14. Produce the final user handoff, optional-settings list, and required TODO list.

Atomically planned generation rule:

- treat each major section above as a discrete build step
- finish one section before relying on it elsewhere
- do not leave placeholder inconsistencies between files

---

## 25. Validation Checklist

The generated architecture is not done until all of these are true:

- [ ] `Workflow Orchestrator` is the only agent with `mode: primary`.
- [ ] `Architect/Planner` exists and runs before non-trivial implementation.
- [ ] the Planner output format includes explicit wave-level parallelization instructions.
- [ ] the Orchestrator contract explicitly says to maximize same-role fan-out when safe.
- [ ] specialists may not cross-route to other specialist types.
- [ ] every specialist file includes the mandatory two-part preflight.
- [ ] the verification wave is mandatory after code changes.
- [ ] UI changes add visual/accessibility audit plus browser/E2E verification when relevant.
- [ ] `Documentation Agent` is always the last wave.
- [ ] `memories/repo/*.md` exists for durable project memory.
- [ ] `memories/orchestration-suggestions.md` exists for deferred architecture improvements.
- [ ] no duplicate memory surface exists outside `memories/`.
- [ ] `kilo.json` permission patterns agree with the documented enforcement model.
- [ ] the close-out protocol is documented in both `AGENTS.md` and the orchestrator system prompt.
- [ ] the exact self-audit fallback wording is preserved.
- [ ] project-specific rules are separated from generic architecture rules.
- [ ] model tables match agent frontmatter.
- [ ] agent registries are internally consistent with the final role set.
- [ ] tool lists match actual role responsibilities.
- [ ] the user receives an end-of-process list of optional settings/capabilities to consider.

---

## 26. Common Drift Traps

Watch for these when generating or maintaining the architecture:

1. Model-version drift between `AGENTS.md` and agent frontmatter.
2. A specialist accidentally having `mode: primary` or `mode: all`.
3. The Orchestrator answering directly instead of delegating via `task`.
4. The Planner describing subtasks but failing to specify exact parallelization.
5. The Orchestrator serializing independent same-role work that should have been fanned out.
6. Verification roles missing after code changes.
7. Documentation running before verification is complete.
8. Close-out protocol prose drifting between `AGENTS.md` and agent files.
9. Permission patterns in `kilo.json` not matching the documented enforcement matrix.
10. Memory spread across random markdown files instead of `memories/`.
11. Template-specific concerns leaking into the generated repo.
12. Project-specific environment settings being hardcoded as universal architecture rules.
13. Repetitive completion boilerplate growing across specialist files.
14. Agent filenames changed but registries not updated.
15. `kilo.json` `default_agent` not pointing to the orchestrator.

---

## 27. Safe Token Savings Without Weakening The Architecture

When optimizing the generated architecture later, these are usually safe:

- shorten repetitive completion boilerplate in specialist files
- use a compact one-line Agent Registry inside specialist files if awareness is preserved
- merge overlapping platform-feature, autonomy, and hook summaries into one capability/enforcement table in `AGENTS.md`
- centralize large shared rules in `AGENTS.md` and keep role-local deltas in agent files
- keep exact close-out wording in one canonical place and reference it elsewhere
- prefer concise MCP/tool-server tables over repeated narrative prose

Do not optimize away:

- the parallelization ownership contract
- the specialist preflight contract
- the independent verification wave
- the close-out phase order
- the exact self-audit fallback wording
- the separation between generic and project-specific rules

---

## 28. Required Final Output From The LLM

After generating the architecture, the LLM should provide:

1. a concise summary of what was created
2. the list of project-specific assumptions that were made
3. the list of optional environment settings/capabilities the user may enable
4. any open blockers or decisions still needed
5. a TODO list of remaining execution points of attention

### Required TODO Shape

The TODO list should be explicit and actionable. The final item must be:

- audit the generated architecture against this blueprint and list every mismatch, omission, or justified deviation

If there are no deviations, the audit item should say so explicitly.

---

## Appendix A: Claude Code → Kilo Code Quick Reference

| Claude Code | Kilo Code | Notes |
| --- | --- | --- |
| `.claude/agents/*.md` | `.kilo/agent/*.md` | filename = agent name |
| `CLAUDE.md` | `AGENTS.md` | root instruction file |
| `.claude/settings.json` hooks | `kilo.json` permissions + commands | no native hook system |
| `.claude/settings.local.json` permissions | `kilo.json` permission field | same pattern model |
| `user-invocable: true` | `mode: primary` | |
| `user-invocable: false` | `mode: subagent` | |
| `Agent` tool | `task` tool | subagent delegation |
| `AskUserQuestion` | `question` tool | structured user questions |
| `TodoWrite` | `todowrite` tool | progress tracking |
| `EnterPlanMode` | plan mode (system reminder) | read-only + plan file |
| `Agent()` in tools list | `task` in tool access | implicit availability |
| `Skill()` tool | `skill` tool | load SKILL.md files |
| `WebFetch`/`WebSearch` | `webfetch`/`websearch` | |
| `Read`/`Edit`/`Write` | `read`/`edit`/`write` | |
| `Glob`/`Grep` | `glob`/`grep` | |
| `Bash` | `bash` | |
| `.claude/skills/` | `.kilo/skill/*/SKILL.md` | different structure |
| PreToolUse hook | `permission.bash` deny | runtime-enforced |
| PostToolUse hook | `/format` command + AGENTS.md instruction | instruction-only |
| Stop hook | `/verify-completion` command + orchestrator prompt | instruction-only |
| `scripts/*.sh` | `scripts/*.sh` (same) + `.kilo/command/*.md` | scripts still work, commands add structure |

## Appendix B: Complete Agent File Example (Kilo Code)

Below is a complete example of how a specialist agent file should look in Kilo Code:

**File:** `.kilo/agent/expert-unity-developer.md`

````yaml
---
description: 'Implementation in Unity 6 / C# / URP 2D. Diagnosis, code, ScriptableObjects, shaders, animations, netcode.'
mode: subagent
model: xiaomi/mimo-v2.5-pro
steps: 30
hidden: false
permission:
  bash: allow
  edit:
    "Assets/**/*.cs": allow
    "*.lock": deny
    "*": ask
  read: allow
  glob: allow
  grep: allow
  webfetch: allow
  websearch: allow
  todowrite: allow
  task: allow
---
````

```markdown
# Expert Unity Developer

## Identity

- Implementation specialist for Unity 6 + URP 2D.
- Diagnoses before editing.
- Implements the smallest correct fix or feature slice.
- Verifies changed behavior directly.
- Avoids creating abstractions without justification.
- Preserves single sources of truth.

## Agent Registry

Orchestrator → Expert Unity Developer, Game Designer, QA, Security Reviewer, Performance Analyst, Documentation Agent.

## Mandatory Preflight

Before any substantive work:

1. **Task plan** — high-level steps.
2. **Parallelism strategy** — tool batching and/or same-role worker approach, or `No parallelism applicable to this task.`

## Boundaries

- Focuses only on Unity/C# implementation.
- Does not design game feel (→ Game Designer).
- Does not run independent verification tests (→ QA).
- Does not profile performance (→ Performance Analyst).
- Does not audit security (→ Security Reviewer).
- Does not update durable project memory (→ Documentation Agent).

## Rules

- Diagnose before editing: read relevant code, understand context, then modify.
- Implement the smallest correct fix: no over-engineer, no unrequested features.
- Verify changed behavior: run whatever is possible to confirm the change works.
- Preserve ScriptableObjects as source of truth for gameplay data.
- Respect project conventions: assembly definitions, namespaces, folder structure.
- Use object pooling for frequently created/destroyed entities.
- Keep simulation tick at 60Hz for netcode and combat consistency.

## Completion Protocol

- Report: what was changed, what was verified, any residual risks.
- Keep it brief.
```

---

## Appendix C: Complete `kilo.json` Example

```jsonc
{
  "$schema": "https://app.kilo.ai/config.json",
  "model": "xiaomi/mimo-v2.5-pro",
  "small_model": "xiaomi/mimo-v2.5-flash",
  "default_agent": "workflow-orchestrator",
  "instructions": [
    "AGENTS.md",
    "**/*.instructions.md"
  ],
  "permission": {
    "bash": {
      "rm -rf /": "deny",
      "rm -rf ~": "deny",
      "rm -rf *": "deny",
      "mkfs.*": "deny",
      "dd if=* of=/dev/*": "deny",
      "git push --force origin main": "deny",
      "git push --force origin master": "deny",
      "shutdown": "deny",
      "reboot": "deny",
      "*": "ask"
    },
    "edit": {
      "*.lock": "deny",
      "*.asset": "ask",
      "*": "ask"
    },
    "external_directory": "deny"
  },
  "mcp": {
    "user-unity-mcp": {
      "type": "remote",
      "url": "http://localhost:23163",
      "headers": {
        "Authorization": "Bearer ${UNITY_MCP_TOKEN}"
      },
      "enabled": true
    }
  },
  "skills": {
    "paths": [".kilo/skill"]
  }
}
```
