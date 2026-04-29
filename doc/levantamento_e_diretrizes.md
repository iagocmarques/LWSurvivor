## Levantamento e diretrizes técnico-estratégicas (MVP “LF2 + Survivors”)

Este documento consolida as **decisões, justificativas e diretrizes** para minimizar risco e acelerar o MVP, mantendo tudo pronto para expansão pós-launch.

---

## 1) Visão do projeto (resumo executivo)

**Conceito**: beat ’em up 2D com movimento em plano com profundidade (2.5D), combate técnico tipo LF2 (combos, dash, agarrões, projéteis) + loop de runs e progressão tipo Vampire Survivors (hordas, upgrades e fusões durante a run, meta-progressão entre runs).

**Multiplayer**: co-op online 2–4p, **host-client** (um jogador autoritativo).

**Plataforma**: Steam-first.

**Filosofia**: MVP enxuto, arquitetura data-driven, preparada para expansão contínua.

---

## 2) Stack definitiva (travada)

### Engine e render
- **Unity 6 + URP 2D**
- 2D lights/particles/shaders (polish “moderno”, sem perder leitura)

### Netcode (Steam-first)
- **Host-client autoritativo**
- Transporte recomendado: **SteamNetworkingSockets + Steam Datagram Relay (SDR)** (Steamworks)

### Dados / expansibilidade
- Authoring: **ScriptableObjects**
- Runtime: Registry por IDs (e, quando fizer sentido, Addressables/export)

### Infra
- Git + **Git LFS** para assets
- CI (GitHub Actions) para builds
- Crash reporting (ex.: Sentry)
- Analytics leve (balanceamento + rede)

---

## 3) Diretrizes de performance (pilares técnicos)

### Metas não-negociáveis
- **60 FPS** como padrão.
- **Tick fixo 60Hz** para simulação (base do netcode e consistência do combate).
- Capacidade de lidar com **200–500 entidades** (inimigos/projéteis/VFX leves) em picos.

### Estratégia para “muitas entidades”
1) **Object pooling agressivo** (inimigos, projéteis, pickups, VFX).
2) **Evitar overhead por entidade** (AI e render precisam ser baratos).
3) Separar frequências:
   - Movimento/colisão: 60Hz
   - “Think” de AI: 10–20Hz
   - Replicação de swarm: 10–20Hz (delta/quantização)

---

## 4) Diretrizes de game feel que impactam código (decidir cedo)

### Input buffering
- Buffer global ~8 frames (configurável).
- Regras de prioridade e janelas de cancel definidas cedo (evita refatorar conteúdo depois).

### Hit stop / screen shake / knockback
Tudo deve ser **parametrizável por golpe** (dados na hitbox), não hardcoded.

### Máquina de estados do player
FSM hierárquica simples (Idle/Move/Dash/Attack/Hitstun/Dead).

### 2.5D (profundidade estilo LF2)
Player/inimigos se movem em **X/Y**, onde `Y` é “profundidade”.  
Ordenação visual por Y e colisões/hitboxes com tolerância de profundidade (“lane thickness”).

---

## 5) Arquitetura data-driven (expansão pós-launch)

### Princípio
**Conteúdo = dados; código = runtime.**  
Adicionar personagem/inimigo/upgrade deve ser duplicar um asset e ajustar números/frames.

### Estruturas sugeridas (MVP)
1) `CharacterDefinition`
   - stats base
   - moveset (lista de moves)
   - animações (referências)
2) `MoveDefinition`
   - condições de entrada
   - duração em ticks
   - janelas de cancel
   - eventos por frame/tick (spawn hitbox, impulso, projétil)
3) `HitboxDefinition` por frame
   - retângulos/polígonos simples, dano, stun, knockback, hitstop, flags (ex.: grab)
4) `EnemyDefinition`
   - hp, speed, peso/poise, tabela de drop
5) `UpgradeDefinition`
   - tags
   - modificadores (add/mult/override)
   - condições de evolução/fusão

### Event Bus
UI/Audio/Analytics devem ouvir eventos, não depender diretamente de classes de gameplay.

---

## 6) Netcode (diretrizes e escolhas)

### Modelo escolhido
**Host-client autoritativo** (host = “server”).

### Por que não rollback no MVP
Rollback é ótimo para PvP competitivo, mas adiciona complexidade alta (re-sim, correção visual, buffers).  
Para co-op 2–4p com parte “survivors-like”, é custo alto cedo demais.

### Objetivo de latência (prático)
- Bom: 30–80ms  
- Jogável: 80–140ms (com prediction)  
- Ruim: 140–200ms (degradar suavemente: mais interpolação, menos exigência de “frame perfect”)  

### O que é autoritativo no host
- RNG seed + spawns
- HP/estado dos inimigos e confirmação de hits
- timers/boss AI

### O que pode ser previsto no client
- Movimento do próprio player
- Início de animações/ataques (para responsividade), com reconciliação

### Replicação do “swarm”
**Não replicar cada inimigo como “NetworkObject”.**  
Enviar snapshots compactos (arrays com id/tipo/pos quantizada/hp/flags) a 10–20Hz, com delta e re-sync completo em checkpoints.

### NAT traversal “sem dor”
**Usar SteamNetworkingSockets + SDR** para relay e reduzir dor de NAT/porta/DoS.

---

## 7) Escopo recomendado do MVP

### Conteúdo mínimo sugerido
- Personagens: **4**
- Mapas: **2**
- Inimigos: **12–18** + **2 bosses**
- Upgrades: **25–35** base + **6–10** evoluções/fusões
- Run: **15–20 min** (MVP pode começar em 10 min e subir)
- Meta-progressão: **12–20** unlocks persistentes

### Não entra no MVP (mas arquitetura deve suportar)
- PvP/Versus
- Campanha/história
- Editor robusto de conteúdo
- Workshop
- Cross-play
- Remapping avançado
- Replays

---

## 8) Pipeline de arte (diretrizes)

### Workflow
Aseprite → spritesheets + JSON (tags/pivots) → atlases na engine.

### “Bonito mas fiel”
Uso moderado de:
- 2D lights (URP 2D)
- partículas (hit sparks, dust, trails)
- hit stop e shake (parametrizados por golpe)

### Otimização visual
sprite batching/atlasing + pooling de VFX/projéteis + culling simples.

---

## 9) Roadmap macro (1–3 pessoas)

1) **Protótipo jogável**: core combat + 1 personagem + 1 inimigo + stress de horda (200).
2) **Vertical slice**: 1 run completa + upgrades + boss placeholder + pipeline de dados.
3) **Alpha fechado**: multiplayer (lobby/invite + movimento/ataque 2p).
4) **Steam Playtest/Beta**: otimização, balance com dados, robustez de rede.
5) **Demo Next Fest**: demo polida (tratar como mini-lançamento).

---

## 10) Principais riscos e mitigação

1) **Performance 500 entidades + VFX**
   - pooling, culling, AI think 10–20Hz, profiling cedo
2) **Swarm replication pesada**
   - snapshots compactos + quantização + delta + 10–20Hz
3) **Desync em transições**
   - checkpoints com re-sync full + logs + testes de packet loss
4) **Host quit**
   - UX clara + salvar progresso parcial; host migration só pós-launch
5) **Balance combinatório**
   - telemetria pick/win + limites de sinergia + ajustes frequentes
6) **Ferramentas de hitbox virarem gargalo**
   - começar simples e evoluir; manter dados versionados
7) **Escopo explodir**
   - gates por fase, Definition of Done e prioridades rígidas
8) **Risco de IP (LF2)**
   - não copiar sprites/nomes/moves específicos; “inspirado”, não clonado

---

## 11) As 5 decisões mais caras de reverter (antes de codar)

1) Engine: Unity 6 + URP 2D  
2) Transporte de rede: SteamNetworkingSockets/SDR  
3) Resolução base/PPU (definir cedo e congelar)  
4) Schema de dados (moves/hitboxes/upgrades)  
5) Tick fixo 60Hz (simulação e rede em ticks)
