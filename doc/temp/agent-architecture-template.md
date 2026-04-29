# Agent Architecture Blueprint Template

Use this blueprint to create a repo-local multi-agent orchestration system with the same structure and operating model as the reference architecture, while keeping the generated result project-agnostic.

This document is intentionally detailed. It is not a compact summary. It is a build specification for an LLM that must generate a full architecture from scratch without silently dropping contracts.

---

## 1. Purpose

This blueprint defines:

- the canonical orchestration topology
- the required files and folders
- the rule hierarchy and source-of-truth order
- the agent role boundaries
- the planning, dispatch, verification, and documentation lifecycle
- the hook-driven close-out contract
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
7. Use official documentation when deciding current platform features, hooks, subagent behavior, model options, MCP/tool-server support, or workspace capabilities.
8. Research the most recent environment settings and optional capabilities that could improve orchestration quality, parallelization, visibility, safety, or autonomy. Do not silently enable them in the generated repo unless the user explicitly wants that.
9. At the end of the generation process, provide the user with a dedicated list of optional settings and capabilities they may choose to enable, including what each one improves and why it is relevant.
10. Keep completion boilerplate short inside specialist agent files. Preserve role rules; compress only repetitive close-out phrasing.
11. Do not create any duplicate memory surface outside `memories/`.
12. End the generation process with a TODO list covering all remaining execution points of attention.
13. The last TODO item must explicitly require an audit that compares the generated architecture against this blueprint and lists any mismatches.

---

## 3. What Must Stay Generic Vs What Must Be Project-Specific

| Layer | Must be generic in the blueprint | Must become project-specific when instantiated |
| --- | --- | --- |
| Topology | Orchestrator -> Planner -> specialists -> verification -> documentation | exact stack specialist names if the target repo is not web/UI based |
| Rules | routing, verification, stop-hook phases, preflight, memory policy, parallelization contract | product-specific UI rules, test caveats, build quirks, domain policies |
| Files | folder layout, hook layout, agent-file structure, memory folder structure | repo names, stack files, concrete script bodies |
| Models | role-based selection strategy | exact current model names and fallbacks |
| Tools | tool categories per role | exact tool identifiers available in the target environment |
| MCP / tool servers | decision framework and optionality | exact servers, secrets strategy, project-specific integrations |
| Local-only policy | whether a local-only boundary exists | exact paths and whether the architecture is committed or excluded |

Rule: if a fact would only be true for one project, it belongs in the generated repo's project-specific overlay, not in the generic architecture contract.

---

## 4. Canonical Topology

Default role topology:

```text
User
  └─▶ Workflow Orchestrator          (entry point, never implements)
        └─▶ Architect/Planner        (task decomposition, DoD, parallelization design)
              ├─▶ Expert <Stack> Developer   (implementation)
              ├─▶ UX/UI Designer             (visual/accessibility audit, if relevant)
              ├─▶ QA                         (tests, quality verification)
              ├─▶ Security Reviewer          (security verification)
              ├─▶ Playwright Tester / E2E Tester (browser or E2E verification, if relevant)
              └─▶ Documentation Agent        (always last)
```

Topology rules:

- `Workflow Orchestrator` is the only user-facing entry point.
- `Architect/Planner` runs before any non-trivial implementation.
- Specialists do domain work.
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

## 5. Canonical Source-Of-Truth Order

The generated architecture should make this hierarchy explicit:

| Priority | Artifact | Purpose |
| --- | --- | --- |
| 1 | `AGENTS.md` | canonical architecture contract and cross-role rules |
| 2 | `.github/agents/*.agent.md` | role-local contracts, tools, boundaries, preflight, completion rules |
| 3 | `.github/hooks/*.json` + `scripts/*.sh` | executable runtime enforcement |
| 4 | `.github/copilot-instructions.md` | stable repository facts and shared coding norms |
| 5 | `.github/instructions/*.instructions.md` | path-scoped or domain-scoped implementation rules |
| 6 | `memories/repo/*.md` | durable current-state project memory |
| 7 | `memories/orchestration-suggestions.md` | deferred orchestration improvements |
| 8 | `.github/skills/` | optional reusable capability packs |
| 9 | `.vscode/mcp.json`, `.vscode/settings.json`, `.vscode/extensions.json` | environment and tooling overlay; environment-dependent |
Conflict rule:

- runtime enforcement wins over prose when a hook blocks execution
- `AGENTS.md` wins over secondary documentation when prose conflicts
- role-local agent files refine, but do not weaken, the root architecture contract
- memory stores current truth only; it does not override architecture contracts

---

## 6. Filesystem Blueprint

Create this structure, adjusting only names that are genuinely stack-specific:

```text
<repo-root>/
├─ AGENTS.md
├─ .github/
│  ├─ agents/
│  │  ├─ workflow-orchestrator.agent.md
│  │  ├─ architect-planner.agent.md
│  │  ├─ expert-<stack>-developer.agent.md
│  │  ├─ ux-ui-designer.agent.md              # omit only if the product has no UI surface
│  │  ├─ qa-subagent.agent.md
│  │  ├─ security-reviewer.agent.md
│  │  ├─ playwright-tester.agent.md           # rename only if another E2E tool is canonical
│  │  └─ documentation-agent.agent.md
│  ├─ hooks/
│  │  ├─ context.json
│  │  ├─ format.json
│  │  ├─ security.json
│  │  └─ orchestration-review.json
│  ├─ instructions/
│  │  └─ <path-or-domain>.instructions.md
│  ├─ skills/
│  │  └─ <optional-skill>/
│  └─ copilot-instructions.md
├─ .vscode/
│  ├─ mcp.json                  # optional, if MCP/tool servers are used
│  ├─ settings.json             # optional, project-owned only if needed
│  └─ extensions.json           # optional
├─ scripts/
│  ├─ inject-context.sh
│  ├─ format-changed.sh
│  ├─ block-dangerous.sh
│  └─ verify-completion.sh
├─ memories/
│  ├─ repo/
│  │  └─ <repo-name>-state.md
│  └─ orchestration-suggestions.md
```

Do not create:

- any other memory file outside `memories/`
- hidden duplicate copies of the architecture contract in random markdown files

---

## 7. Root `AGENTS.md` Required Structure

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
- the second half describes runtime enforcement and environment usage without scattering the same hook/capability facts across multiple sections
- project-specific rules stay visibly separate from the generic architecture

---

## 8. Core Architecture Contract

The generated `AGENTS.md` must represent these rules explicitly.

1. Every multi-step task goes through `Workflow Orchestrator` first.
2. `Workflow Orchestrator` routes every request to a dedicated agent.
3. `Workflow Orchestrator` is the source of truth between the user request and delivered work.
4. The Orchestrator asks for clarification before delegating if the request is materially ambiguous or incomplete.
5. `Architect/Planner` runs before implementation on any non-trivial task.
6. The Orchestrator delegates through the agent tool to the appropriate specialist for the task.
7. Independent subtasks dispatch in parallel within the same wave.
8. Any code change triggers an independent verification wave, at minimum `QA` plus `Security Reviewer`.
9. Any UI change additionally triggers a visual/accessibility audit plus browser/E2E verification.
10. The Orchestrator never trusts self-reports as proof of completion.
11. `Documentation Agent` is always the final wave.
12. The Orchestrator is the only cross-role router.
13. Eligible specialists may self-delegate only within their own role or inherited worker scope.
14. Simplicity beats over-engineering.
15. All agents maintain progress tracking.
16. All agents know about all other agents through an Agent Registry.
17. Official documentation lookup is mandatory for planning, large implementation/refactor work, and audits/reviews involving framework or library contracts.
18. Architecture changes require explicit user approval.
19. Fix root causes, not symptoms.
20. Preserve single sources of truth for reusable values and contracts.
21. Prefer existing abstractions before creating new ones.
22. Extract reusable logic only when justified.
23. The Planner uses the question tool for material trade-offs or user-only blockers.
24. The Planner lists adjacent work as optional follow-up, not as assumed scope.
25. The Planner may propose a better approach, but execution follows the user's decision if the user declines.
26. Verification loops are capped at three fix -> verify iterations.
27. QA prioritizes blocking failures before minor polish.
28. The orchestration self-audit follows a canonical priority order.
29. Close-out artifacts are produced only when the Stop hook requests them; do not emit the DoD summary table, Files Changed table, or orchestration self-review proactively.
30. Agents reuse open terminals when the environment exposes stable terminal reuse.
31. Agents do not re-ask decisions already answered through question tools during the same session.
32. The Planner is the primary owner of parallelization design.
33. The Orchestrator is the primary owner of parallelization execution.
34. Specialists still assess additional fan-out opportunities during execution.
35. Orchestration-file changes require an independent consistency pass.
36. Agents verify working directory before terminal commands.
37. The self-audit fallback wording is exact and canonical.
38. Every specialist must emit a structured preflight before substantive work.
39. The Orchestrator's user-facing reporting must stay direct and concise.

### 8.1 Parallelization Contract

This is the most important coordination rule after routing.

Planner responsibilities:

- split work into atomic, parallel-safe subtasks
- specify wave boundaries
- specify exactly which subtasks run in parallel
- specify when multiple instances of the same agent should be launched
- specify verification fan-out by lane when possible
- specify fallback fan-out if a wave is likely to return many independent failures

Orchestrator responsibilities:

- treat the Planner's parallelization instructions as execution requirements
- launch all independent same-wave subtasks together
- maximize safe same-role fan-out instead of serializing independent work
- prefer the highest useful number of same-role instances when work is separable
- re-fan-out verification or fixes if failures cluster into independent concerns

Specialist responsibilities:

- assess whether more same-role fan-out became possible after work began
- self-delegate within role when the environment allows it and the speedup is material
- otherwise report a concrete fan-out recommendation back to the Orchestrator immediately

Example:

- a test audit can spawn separate `QA` instances for unit, integration, smoke, and E2E-adjacent non-browser checks
- a browser verification wave can split `Playwright Tester` work by independent flows
- if ten failures return and they separate into independent buckets, do not serialize them by default; split them into workers or ask the Orchestrator to do so

### 8.2 Tool Parallelism Vs Worker Subagents

The generated docs should explain the difference clearly:

- batched reads/searches/web fetches in one turn are tool parallelism inside one agent session
- worker subagents are separate same-role executions created for isolation or speed
- same-role worker use is allowed only if the active environment supports it
- same-role worker use never grants cross-role routing authority

---

## 9. Orchestration Self-Audit Priority Order

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

## 10. Rule Ownership Matrix

Make rule ownership explicit so future drift is easy to spot.

| Rule area | Primary owner | Secondary owner | Runtime owner |
| --- | --- | --- | --- |
| Cross-role routing | `Workflow Orchestrator` | `AGENTS.md` | n/a |
| Planning and DoD definition | `Architect/Planner` | `AGENTS.md` | n/a |
| Parallelization design | `Architect/Planner` | `AGENTS.md`, Planner output format | n/a |
| Parallelization execution | `Workflow Orchestrator` | specialist preflight and universal rules | Stop-hook audit pressure |
| Specialist boundaries | agent files | `AGENTS.md` | n/a |
| Verification wave | `Workflow Orchestrator` | Planner output format | n/a |
| Close-out phases | `AGENTS.md` | Orchestrator file | `verify-completion.sh` |
| Context injection | hook docs | `AGENTS.md` | `inject-context.sh` |
| Destructive command blocking | hook docs | `AGENTS.md` | `block-dangerous.sh` |
| Auto-format | hook docs | `AGENTS.md` | `format-changed.sh` |
| Durable project memory | `Documentation Agent` + `memories/` | `copilot-instructions.md` | n/a |

---

## 11. Agent File Requirements

Every agent file must contain:

- YAML frontmatter
- a role summary
- explicit responsibilities or core mandate
- an Agent Registry listing all registered roles
- role boundaries
- any shared universal rules that the role must obey
- a short completion protocol

If you rename agents, update:

- frontmatter `name`
- every registry
- Orchestrator registry
- Planner plan format
- handoffs
- any hook messages or script expectations that mention the agent by name

### 11.1 Common Frontmatter Shape

Use this as the baseline:

```yaml
---
name: '<Exact Agent Name>'
description: '<Role summary>'
argument-hint: '[how the orchestrator should call this role]'
tools: ['<tool>', '<tool>']
agents: ['<Exact Agent Name>']           # omit only for the Orchestrator, which lists every callable specialist
model: ['<Primary Model>', '<Fallback Model>']
handoffs:
  - label: '<Optional handoff label>'
    agent: '<Optional target agent>'
    prompt: '<Optional handoff prompt>'
    send: false
user-invocable: true|false
disable-model-invocation: true
---
```

Baseline rules:

1. Only the `Workflow Orchestrator` is user-invocable.
2. The Orchestrator lists every callable specialist in `agents`.
3. Specialists should not be user-invocable by default.
4. Same-role self-delegating specialists may include the `agent` tool, but that does not permit cross-role calls.
5. Specialist Agent Registries may be a compact one-line list if all roles remain visible.
6. Completion protocols should be brief. Preserve substance, not repetitive handoff prose.

---

## 12. `Workflow Orchestrator` Contract

The Orchestrator file is the heart of the runtime architecture.

### 12.1 Required Identity

- entry point for all user work
- never implements
- only cross-role router
- tracks DoD
- launches independent verification
- reports concisely

### 12.2 Required Tools

At minimum:

- search/read tools
- `agent`
- todo tracking
- official-doc fetching capability
- question tool if the platform supports structured user questions

Optional:

- any non-implementation read-only helpers needed for orchestration visibility

### 12.3 Required Workflow Sections

The file should contain these workflow phases in order:

1. Understand and clarify
2. Plan
3. Dispatch in waves
4. Independent verification
5. Documentation wave
6. Completion close-out

Exact stop-phase wording does not need to be repeated verbatim in every orchestration file if `AGENTS.md` and the runtime hook contract are already canonical. The Orchestrator may reference that canonical wording instead of duplicating it.

### 12.4 Absolute Rules The Orchestrator Must Carry

- never write code or run commands
- always delegate
- never bypass the Planner for non-trivial work
- never skip the Documentation Agent
- never trust self-reports
- ask when blocked or ambiguous
- respect prior question-tool answers
- prefer simple root-cause fixes
- execute the Planner's parallelization plan aggressively
- maximize same-role fan-out when safe
- keep user reporting direct and token-efficient

### 12.5 Concise Reporting Requirement

The Orchestrator's final user-facing reporting should prefer:

- current status
- blockers
- verification outcome
- next decision or next step

Avoid long narrative summaries unless the user explicitly asks for them.

Hook-enforced close-out artifacts such as the DoD summary table, Files Changed table, and orchestration self-review should appear only when the Stop hook blocks stop and requests them.

The Orchestrator's MCP guidance may also be kept as a concise decision table instead of long duplicated prose, as long as each configured server still documents:

- when to use it
- when not to use it
- the primary role owners

---

## 13. `Architect/Planner` Contract

The Planner is responsible for decomposition quality and explicit parallelization design.

### 13.1 Required Identity

- never implements
- reads existing code and docs before planning
- decomposes into atomic subtasks
- assigns each subtask to one exact agent
- defines verifiable DoD
- flags blockers
- asks material questions when needed
- defines wave-level parallelization

### 13.2 Required Planner Output

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

**Agent:** QA + Security Reviewer (+ UX/UI Designer + E2E role when relevant)
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

### 13.3 Planner-Specific Requirements

- use official docs before planning framework- or library-dependent work
- keep optional follow-up separate from committed scope
- design FE and BE as parallel tracks by default when both exist and no contract dependency blocks it
- design verification fan-out explicitly
- design fallback fan-out for likely failure clusters

---

## 14. Specialist Agent Contract

This section applies to implementation, QA, security, UX/UI, and E2E specialists.

### 14.1 Shared Specialist Rules

Every specialist must:

- stay within role boundaries
- maintain progress tracking
- preserve simplicity
- use official docs when required
- respect prior user answers
- emit the mandatory preflight before substantive work
- assess parallelization before starting
- use tool batching first for cheap fan-out
- use same-role worker fan-out when materially useful and environment-supported
- escalate blockers instead of guessing
- keep completion reporting short

### 14.2 Mandatory Specialist Preflight

Every specialist must emit exactly these two ordered sections before substantive work:

1. `Task plan` — the high-level steps the specialist will take
2. `Parallelism strategy` — tool batching and/or same-role worker approach, or `No parallelism applicable to this task.`

This contract should appear both:

- in the root architecture contract
- in each specialist file

### 14.3 Implementation Specialist

The implementation specialist should:

- diagnose before editing
- implement the smallest correct fix or feature slice
- verify changed behavior directly
- avoid creating abstractions without justification
- preserve single sources of truth

### 14.4 QA Specialist

The QA specialist should:

- verify changed code independently
- run the relevant test lanes
- prioritize broken functionality and regressions first
- split verification by lane when possible
- ask for additional same-role fan-out if many failures are independent

### 14.5 Security Reviewer

The Security Reviewer should:

- audit changed code for exploitable flaws
- review dependency/CVE risk and policy compliance
- report concrete findings, scope reviewed, residual risks, and coverage gaps

### 14.6 UX/UI Designer

The UX/UI specialist should:

- assess visual fidelity, interaction quality, accessibility, and design consistency
- use screenshots or browser-level inspection when appropriate
- report findings succinctly and concretely

### 14.7 E2E / Playwright Tester

The browser/E2E specialist should:

- validate user-facing flows
- debug failing browser flows when needed
- split independent flow clusters into parallel same-role work when beneficial

### 14.8 Documentation Agent

The Documentation Agent should:

- run last
- never implement product code
- update only durable current-state memory
- update shared docs when architecture or stable repo facts changed
- record deferred orchestration improvements separately from durable project memory

Do not make the generated Documentation Agent depend on any deleted or duplicate memory surface outside `memories/`.

---

## 15. Hook Contract

The generated architecture should preserve four runtime hook families.

### 15.1 Context Injection

Artifacts:

- `.github/hooks/context.json`
- `scripts/inject-context.sh`

Purpose:

- inject project context at session start
- inject the same context into subagents
- reduce repeated discovery work

### 15.2 Destructive Command Blocking

Artifacts:

- `.github/hooks/security.json`
- `scripts/block-dangerous.sh`

Purpose:

- deny obviously destructive commands before execution

### 15.3 Auto-Format

Artifacts:

- `.github/hooks/format.json`
- `scripts/format-changed.sh`

Purpose:

- automatically format changed files after edits

### 15.4 Stop Hook / Completion Verification

Artifacts:

- `.github/hooks/orchestration-review.json`
- `scripts/verify-completion.sh`

Purpose:

- pressure-test whether delegation happened when it obviously should have
- enforce completion artifacts in phases
- keep the close-out logic in one script without extra persisted runtime state

### 15.5 Required Stop-Hook Phases

The default two-phase contract should be preserved unless the user intentionally wants a different close-out model:

Phase 1:

- require a DoD summary table
- require a Files Changed table

Phase 2:

- require the short investigative orchestration self-review
- require the canonical fallback wording when no improvement is identified
- require the exact seven numbered headings from the hook prompt so transcript-driven verification is deterministic

### 15.6 Enforcement Matrix

The generated docs should separate:

- hard enforcement
- pressure-based enforcement
- audit-only enforcement
- instruction-only expectations

Recommended mapping:

| Behavior | Level |
| --- | --- |
| close-out phase checks | Hard |
| destructive command blocking | Hard |
| auto-format after edits | Hard |
| context injection | Hard |
| missed delegation when parallelization was clearly plausible | Pressure-based |
| MCP/tool-server usage correctness | Audit-only |
| terminal reuse | Instruction-only |

---

## 16. Instructions Layer

The generated repo should have three instruction layers:

1. `AGENTS.md` for the architecture contract
2. `.github/copilot-instructions.md` for stable repository-wide facts and coding norms
3. `.github/instructions/*.instructions.md` for path-specific or domain-specific rules

Layering rules:

- avoid duplicating the same large rule block in every file
- repeat only what is necessary for role adherence or runtime correctness
- keep project-specific facts out of the generic architecture template
- keep completion boilerplate brief

---

## 17. Memory Layer

Memory now lives only under `memories/`.

### 17.1 Durable Project Memory

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

### 17.2 Deferred Orchestration Improvements

Use `memories/orchestration-suggestions.md` for architecture improvements that are intentionally deferred.

Recommended structure:

- group by the canonical seven self-audit principles
- keep entries dated
- keep entries actionable
- remove or rewrite stale suggestions when implemented or superseded

### 17.3 Memory Rules

- keep only the current truth
- remove stale or conflicting memory instead of appending history forever
- do not create any separate memory surface outside `memories/`
- do not scatter memory across unrelated markdown files

---

## 18. Skills Layer

`.github/skills/` is optional.

Use it when:

- a recurring orchestration behavior needs reusable local instructions
- the repo benefits from specialized capability packs

Do not use it as a dumping ground for generic rules already covered by:

- `AGENTS.md`
- agent files
- shared instructions

---

## 19. MCP / Tool-Server Layer

Treat this as optional and environment-dependent.

### 19.1 Required Blueprint Behavior

When generating a new architecture:

- decide whether MCP/tool servers are actually useful for the target repo
- document when each server should and should not be used
- avoid hardcoding a secrets strategy in the generic blueprint
- make secret provisioning a project decision

### 19.2 Generic Decision Guide

Keep a decision guide for any configured external tool server:

- browser automation / render inspection
- browser runtime debugging
- backend or database inspection
- project-specific external integration

For each, document:

- when to use it
- when not to use it
- primary role owners

A concise table is preferred when the same guidance already exists canonically elsewhere.

### 19.3 Settings And Capability Discovery Requirement

Before finalizing the generated architecture, the LLM should review the latest official platform documentation for settings or capabilities that may improve:

- subagent orchestration
- hooks
- handoffs
- question tools
- memory access
- agent permissions
- terminal/session reuse
- model routing
- MCP/tool-server integration

Then provide the user with a dedicated end-of-process list:

| Setting or capability | Benefit | Why relevant here | Recommended? | User decision |
| --- | --- | --- | --- | --- |
| `<name>` | `<benefit>` | `<reason>` | Yes/No | Pending |

This list is informational. The user decides what to enable.

---

## 20. Local-Only Vs Committed Boundary

The blueprint should force a deliberate decision:

- Will the orchestration architecture live only locally?
- Or will it be committed into the repository?

If local-only:

- define the exact local-only paths
- define how they stay out of version control
- keep that policy out of committed repo docs unless intentionally desired

If committed:

- remove any local-only instructions that would contradict the chosen model

Do not leave this ambiguous in the generated architecture.

---

## 21. Bootstrap Procedure For A New Repo

Generate the new architecture in this order:

1. Identify the target stack, runtime, and whether the repo has UI, backend, or both.
2. Decide the final specialist roster while preserving the same topology.
3. Choose exact role names and keep them stable.
4. Draft `AGENTS.md` first, including generic rules and a clearly separated project-specific section.
5. Create all agent files with frontmatter, responsibilities, registry, boundaries, and short completion protocols.
6. Create hook configs and script placeholders or implementations.
7. Create `.github/copilot-instructions.md`.
8. Create any path-scoped instruction files.
9. Create the `memories/` structure.
10. Decide whether skills are needed.
11. Decide whether MCP/tool servers are needed.
12. Decide whether `.vscode/settings.json` is repo-owned or environment-owned.
13. Prefer a transcript-driven Stop hook that does not require extra persisted runtime state.
14. Run a consistency pass across names, models, tools, and question headers.
15. Run a drift pass comparing prose rules against runtime scripts and hook configs.
16. Produce the final user handoff, optional-settings list, and required TODO list.

Atomically planned generation rule:

- treat each major section above as a discrete build step
- finish one section before relying on it elsewhere
- do not leave placeholder inconsistencies between files

---

## 22. Validation Checklist

The generated architecture is not done until all of these are true:

- [ ] `Workflow Orchestrator` is the only user-invocable cross-role router.
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
- [ ] hook configs and scripts agree with the documented close-out contract.
- [ ] the Stop hook phases are documented in both `AGENTS.md` and runtime enforcement.
- [ ] the exact self-audit fallback wording is preserved.
- [ ] project-specific rules are separated from generic architecture rules.
- [ ] model tables match agent frontmatter.
- [ ] agent registries are internally consistent with the final role set.
- [ ] tool lists match actual role responsibilities.
- [ ] question headers and prompt text are consistent across docs and scripts.
- [ ] the user receives an end-of-process list of optional settings/capabilities to consider.

---

## 23. Common Drift Traps

Watch for these when generating or maintaining the architecture:

1. Model-version drift between `AGENTS.md` and agent frontmatter.
2. A specialist accidentally becoming user-invocable.
3. The Orchestrator answering directly instead of delegating.
4. The Planner describing subtasks but failing to specify exact parallelization.
5. The Orchestrator serializing independent same-role work that should have been fanned out.
6. Verification roles missing after code changes.
7. Documentation running before verification is complete.
8. Stop-hook prose drifting away from `verify-completion.sh`.
9. Question headers drifting between docs and runtime scripts.
10. Memory spread across random markdown files instead of `memories/`.
11. Template-specific concerns leaking into the generated repo.
12. Project-specific environment settings being hardcoded as universal architecture rules.
13. Repetitive completion boilerplate growing across specialist files.
14. Agent names being renamed in one place but not every registry or handoff.

---

## 24. Safe Token Savings Without Weakening The Architecture

When optimizing the generated architecture later, these are usually safe:

- shorten repetitive completion boilerplate in specialist files
- use a compact one-line Agent Registry inside specialist files if awareness is preserved
- merge overlapping platform-feature, autonomy, and hook summaries into one capability/enforcement table in `AGENTS.md`
- centralize large shared rules in `AGENTS.md` and keep role-local deltas in agent files
- keep exact stop-hook wording in one canonical place and reference it elsewhere, as long as runtime and docs still agree
- prefer concise MCP/tool-server tables over repeated narrative prose

Do not optimize away:

- the parallelization ownership contract
- the specialist preflight contract
- the independent verification wave
- the close-out phase order
- the exact self-audit fallback wording
- the separation between generic and project-specific rules

---

## 25. Required Final Output From The LLM

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
