# SPEC — Combate reativo LF2-like (defender, tomar golpe, cair, levantar) + correlação com o manual de frames

**Público-alvo:** orquestrador/PM técnico + devs de gameplay.  
**Objetivo:** implementar no Unity (tick fixo 60Hz) todas as **ações reativas** estilo Little Fighter 2 (LF2) de forma **genérica e data-driven**, correlacionando diretamente com os frames do arquivo **`lf2_manual_personagens_completo.md`** (que contém `state/wait/next/bdy/itr/opoint` por frame e por personagem).

---

## 1) Escopo e definições

### 1.1 Entra no escopo
Implementar as ações reativas mínimas, com paridade funcional entre personagens:
1) **Defender (Defend)**  
2) **Tomar golpe no chão (HurtGrounded)**  
3) **Tomar golpe no ar (HurtAir / Flying)**  
4) **Cair/Deitar no chão (Lying)**  
5) **Levantar (GetUp)**  
6) **Quebra de defesa (DefendBreak)**  
7) **Stun/Faint** (opcional, mas recomendado)  
8) **Morte (Dead)** (mínimo)

Além disso:
- Aplicação de **hitstop** e **screen shake** já existentes.
- Aplicação de **status effects** (burn/freeze/blood) vindos do `itr.effect`.
- Suporte completo a **facing/mirror** (já validado no runtime).

### 1.2 Fora do escopo (por enquanto)
- Agarrões completos LF2 (Catching/Caught) *se* ainda não estiverem no MVP.
- Sistema completo de poise/armadura avançada.
- IA/estados de NPC complexos.

---

## 2) Relação direta com `lf2_manual_personagens_completo.md` (como correlacionar ações ↔ frames)

### 2.1 Regra-chave do LF2
No LF2, “ação” não é uma função; é:
1) **`state`** (categoria macro), +  
2) **timeline** (grafo `wait/next`), +  
3) **eventos por frame** (`bdy/itr/opoint/sound`), +  
4) **pontos de entrada** (normalmente `hit_*` para ações ativas).

### 2.2 Ações “ativas” (ataques, dash etc.)
**Identificação 1:1 via `hit_*`**.  
No manual, nos frames base (normalmente “standing/walking/defend”) aparecem:
- `hit_a`, `hit_j`, `hit_d`, `hit_Fa`, `hit_Fj`, `hit_Ua`, `hit_Uj`, `hit_Da`, `hit_Dj`, `hit_ja`, `hit_back`

Cada `hit_*: N` aponta para o **primeiro frame** do move. A ação é reconstruída seguindo `next` até `999` ou loop.

### 2.3 Ações “reativas” (tomar golpe, cair, defender etc.)
**Não vêm de `hit_*`**.  
Identificação se baseia em **`state`** e no **grafo `next`**.

Mapeamento “LF2 → nosso macroestado” (referência):
- `state 7`  → **Defending**
- `state 8`  → **DefendBreak**
- `state 11` → **HurtGrounded**
- `state 12` → **HurtAir**
- `state 14` → **Lying**
- `state 16` → **Faint/Stun** (se adotado)
- (variações 18/19) → burning variants (se existirem no pacote)

**Importante:** não existe um “start frame” universal por ação. É por personagem.

### 2.4 Heurística oficial para achar o “start frame” de uma ação reativa
Para cada personagem e para cada `state` reativo alvo:
1) Colete o conjunto **S** de frames com `frame.state == alvo`.
2) Para cada frame `f` em **S**, marque se algum outro frame de **S** tem `next == f.id`.
3) Os frames **não referenciados** por `next` dentro de **S** são **raízes prováveis** (start frames).
4) Se houver múltiplas raízes:
   - escolha a que tem maior `wait`, ou
   - a que possui `dvx/dvy/dvz` coerente com “impacto inicial”, ou
   - a mais próxima/mais citada em transições do resto do grafo (fallback).

Essa heurística permite gerar um `ReactiveMoveSet` automaticamente (ideal) ou guiar seleção manual.

---

## 3) Arquitetura obrigatória (3 camadas)

### 3.1 Camada 1 — FSM macro (alto nível)
Estados mínimos:
- `Grounded`
- `Attacking`
- `Defending`
- `HurtGrounded`
- `HurtAir`
- `Lying`
- `GetUp`
- `Dead`

### 3.2 Camada 2 — Move Timeline (data-driven)
Toda ação (ativa e reativa) é um **MoveDefinition**:
- `startFrameId`
- `frames[]` (expandidos via `next`)
- flags por frame: lock input, invuln, etc.

### 3.3 Camada 3 — Eventos por frame
Durante a execução do move (por tick):
- Atualiza sprite (pic)
- Aplica dvx/dvy/dvz (com mirror)
- Atualiza hurtbox (bdy) do frame
- Ativa hitboxes (itr) do frame
- Processa `opoint` (spawn)
- Toca `sound` (se usado)

---

## 4) Entry point único para dano + roteador de reações (obrigatório)

### 4.1 Estrutura mínima de `HitResult`
Padronizar um payload único ao colidir `itr` → `bdy`:
- `damage` (itr.injury)
- `knockback` (itr.dvx/dvy/dvz)
- `fall` (itr.fall) se existir
- `bdefend` (itr.bdefend) se existir
- `effect` (itr.effect) se existir (1 blood, 2 burn, 3 freeze)
- `attackerFacing` (+1 / -1)
- `hitFlags` (ex.: isProjectile, isHeavy, breaksGuard override)

### 4.2 `ApplyHit(HitResult hit)` (único ponto de entrada)
Responsabilidades:
- reduzir HP
- aplicar status effects (se aplicável)
- NÃO escolher animação aqui
- delegar para `DamageReactionRouter` escolher o move reativo

### 4.3 `DamageReactionRouter` (matriz de decisão)
Prioridade recomendada (do mais crítico ao menos):
1) `Dead` (hp <= 0)
2) `DefendBreak` (se defending e ataque quebra defesa)
3) `HurtAir` (se airborne OU knockback vertical acima de threshold)
4) `HurtGrounded` (hit normal no chão)
5) `DefendHit` (micro stun ao defender sem quebrar)

**Condição de “quebra de defesa” (default):**
- se `hit.damage` >= `GuardBreakThreshold` do defensor **OU**
- se `hit.bdefend` (do itr) excede resistência defensiva configurada.

**Condição de HurtAir (default):**
- se `hit.knockback.dvy <= -AirLaunchThreshold` (valores LF2 usam negativos para subir, então isso pode inverter no seu runtime; padronize no tick).

---

## 5) Ações reativas como moves (o que cada uma deve fazer)

> Regra: ações reativas também são MoveDefinitions (importadas ou geradas) e rodam no mesmo `MoveRunner`.

### 5.1 Defending (state 7)
- Entrada: input defend pressionado (quando permitido)
- Efeitos:
  - reduz/nega dano (dependendo do design)
  - permite “DefendHit” e “DefendBreak”
- Saída:
  - soltar botão defend → Grounded

### 5.2 DefendHit (sub-ação)
- Entrada: defend ativo + hit recebido sem quebra
- Efeitos: micro hitstun (curto), empurrão leve opcional
- Saída: volta para Defending ou Grounded

### 5.3 DefendBreak (state 8)
- Entrada: defend ativo + hit quebra defesa
- Efeitos: hitstun maior, knockback, pode derrubar (opcional)
- Saída: Grounded ou Lying (se derrubado)

### 5.4 HurtGrounded (state 11)
- Entrada: hit no chão sem launch significativo
- Efeitos:
  - lock input
  - knockback horizontal
  - se “fall/threshold” atingido → transição para Lying
- Saída: Grounded ou Lying

### 5.5 HurtAir (state 12)
- Entrada: airborne ou launch
- Efeitos:
  - lock input
  - aplicar velocidade/gravidade do seu motor (aqui é onde LF2 “voa/caindo”)
- Saída:
  - ao tocar chão → Lying

### 5.6 Lying (state 14)
- Entrada: caiu no chão
- Efeitos:
  - timer (get-up delay)
  - invulnerabilidade opcional configurável (padrão: curta)
- Saída: GetUp após timer (ou tech se existir)

### 5.7 GetUp
- Entrada: fim do timer de Lying
- Efeitos: animação curta + lock input
- Saída: Grounded

### 5.8 Dead
- Entrada: hp <= 0
- Efeitos: desabilitar inputs, physics simplificada
- Saída: nenhuma (respawn fora do escopo)

---

## 6) Requisitos de implementação (para evitar bugs clássicos)

### 6.1 Mirror/facing deve ser aplicado em tudo
Durante moves reativos também:
- hurtbox (`bdy`)
- hitbox (`itr`)
- `opoint` spawns
- deslocamentos (dvx) quando aplicados diretamente

### 6.2 Nunca usar `Time.deltaTime` na lógica core
Tudo em ticks (60Hz), inclusive timers de lying/getup.

### 6.3 Um único “motor” para movimento e gravidade
`dvx/dvy/dvz` do frame devem influenciar o `CharacterMotor`, não “teleportar transform”.

---

## 7) Entregáveis do time (o que o orquestrador deve cobrar)

### 7.1 Código
1) `HitResult` + pipeline `ApplyHit`
2) `DamageReactionRouter` com tabela de decisão e logs de debug
3) `ReactiveMoveSet` por personagem (gerado automaticamente ou configurado)
4) Integração no `MoveRunner`/FSM:
   - Defend / Hurt / Lying / GetUp / Dead

### 7.2 Dados
Para cada personagem (pelo menos 1 primeiro: Davis):
- Identificar (ou gerar) start frames para: defend/hurtground/hurtair/lying/getup
- Associar sequências via `next`

### 7.3 Debug/observabilidade (obrigatório)
Overlay/log com:
- macrostate atual
- move atual + frame atual
- último `HitResult` recebido (damage/knockback/effect)

---

## 8) Plano de execução recomendado (fases)

### Fase 1 — Vertical slice em 1 personagem (Davis)
1) Implementar Router + 4 reações: Defend, HurtGrounded, HurtAir, Lying/GetUp
2) Validar mirror left/right
3) Validar “strong hit breaks guard”

### Fase 2 — Generalizar para todos os personagens
1) Implementar geração automática de `ReactiveMoveSet` usando a heurística por `state`
2) Rodar e corrigir casos edge (múltiplas raízes por state)

### Fase 3 — Polimento
1) Faint/Stun (se quiser)
2) Ajustes de invulnerabilidade no chão, tech, etc. (se entrar no design)

---

## 9) Critérios de aceitação (QA)

### Testes manuais (mínimo)
1) **Defend:** segurar defend → ataque fraco não interrompe (ou reduz dano) e toca DefendHit.
2) **DefendBreak:** ataque forte quebra defesa → toca DefendBreak, empurra/derruba conforme regra.
3) **HurtGrounded:** tomar golpe no chão → toca recoil, trava input e volta ao controle.
4) **HurtAir:** launcher → entra HurtAir; ao cair → Lying; depois GetUp.
5) **Mirror:** repetir 1–4 virado para a esquerda e para a direita e confirmar simetria.
6) **Effect:** `effect: 2/3` aplica burn/freeze (uma vez), sem duplicar infinitamente.

### Testes automatizáveis (recomendado)
- PlayMode test com dummy attacker:
  - `DefendBlocksWeakHit`
  - `StrongHitBreaksGuard`
  - `LaunchTransitionsToHurtAirThenLyingThenGetUp`
  - `FacingLeftRightSymmetryForHitboxesAndOpoints`

---

## 10) Referências internas (arquivos do projeto)
- Manual de frames por personagem: `lf2_manual_personagens_completo.md`
- Spec do importador LF2→Unity: `spec_importador_lf2_para_unity.md`

