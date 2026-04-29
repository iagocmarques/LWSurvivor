# Prompts para desenvolvimento do MVP (TRAE Code)

> **Objetivo**: você copia/cola estes prompts aqui no TRAE Code (esta plataforma) para eu gerar **código C# completo + instruções no Unity Editor** e irmos construindo o MVP do jogo “LF2 + Survivors”.
>
> **Stack travada**: Unity 6 + URP 2D + C# + Input System + Addressables (mais tarde) + Steamworks (lobbies/invites) + transporte via SteamNetworkingSockets/SDR (mais tarde) + host-client autoritativo.
>
> **Regras do fluxo**:
> 1) Sempre cole primeiro o **PROMPT MESTRE** (abaixo) em uma conversa/sessão nova.
> 2) Depois execute os prompts na ordem (0, 1, 2…).
> 3) Após cada etapa, você me responde com **“OK, passou”** ou cola o erro/print.
> 4) Eu só avanço quando você confirmar que testou.

---

## PROMPT MESTRE (cole sempre no início)

**Prompt:**
> Você é Tech Lead sênior de jogos indie 2D multiplayer na Steam. Vamos construir o MVP “LF2 + Survivors” em **Unity 6 + URP 2D**, **tick fixo 60Hz**, **data-driven**, **pooling agressivo**.  
> Regras:
> 1) Sempre entregar **arquivos C# completos** (prontos para colar), com **nome do arquivo** e **caminho** em `Assets/_Project/...`.
> 2) Sempre incluir **Passos no Unity Editor** (menus/inspector) quando necessário.
> 3) Sempre terminar com: **Critérios de aceitação** + **Como testar**.
> 4) Assuma decisões técnicas sem perguntar (sou leigo), a menos que bloqueie 100%.
> 5) Não proponha trocar engine/stack.

---

## Prompt 0 — Setup do projeto (Unity Editor) + estrutura base

**Prompt:**
> Me dê um passo-a-passo exato para criar um projeto Unity 6 2D com **URP 2D**, configurar o **Input System**, e criar a estrutura de pastas `Assets/_Project/{Core,Gameplay,Data,UI,Art,Audio,Net,Tools,Tests}`.  
> Depois disso, gere os scripts mínimos para uma cena `Bootstrap` que carrega `Game` e mostra um HUD de debug com FPS.

---

## Prompt 1 — Tick fixo 60Hz (base da simulação)

**Prompt:**
> Gere o sistema de tick fixo 60Hz desacoplado do Update. Quero:
> - `FixedTickSystem` (60Hz), com clamp anti-espiral  
> - interface `ITickable` e registro/unregistro  
> - contador de tick e delta fixo  
> - overlay debug mostrando tick e drift  
> Entregue os arquivos completos e como plugar isso na cena `Bootstrap`.

---

## Prompt 2 — Movimento 2.5D (LF2-style) + depth sorting por profundidade

**Prompt:**
> Gere o controller do player para movimento 2.5D (X horizontal, Y profundidade), usando Input System (teclado + gamepad).  
> Quero depth sorting por Y (quanto maior Y, mais atrás), com solução robusta.  
> Entregue scripts + instruções de componentes no GameObject do Player.

---

## Prompt 3 — FSM do player (Idle/Move/Dash/Attack/Hitstun)

**Prompt:**
> Gere uma FSM hierárquica simples para o player com estados: Idle/Move, Dash, Attack, Hitstun.  
> Quero: dash com cooldown e invuln curta; hitstun com knockback.  
> Parametrização via `PlayerTuning` (ScriptableObject).  
> Entregue código + como configurar 1 asset de tuning.

---

## Prompt 4 — Input Buffering + 3 golpes (combo mínimo)

**Prompt:**
> Implemente input buffering (8 frames) e três ataques: jab, launcher, dash attack.  
> Preciso de:
> - buffer com debug (últimos inputs)  
> - regras de cancel/encadeamento simples  
> - ataque dispara hitbox em frames definidos  
> Entregue código e como testar com um dummy.

---

## Prompt 5 — Hitbox/Hurtbox data-driven v0 (por frame)

**Prompt:**
> Crie um sistema data-driven de hitboxes por frame:
> - estrutura de dados (ScriptableObject) com frames e retângulos  
> - runtime que ativa hitbox por tick  
> - um “editor simples” dentro do Unity (Tool/Custom Inspector) para editar retângulos por frame  
> Entregue tudo do jeito mais simples que funcione.

---

## Prompt 6 — Damage pipeline + hit stop + screen shake

**Prompt:**
> Implemente o pipeline de dano: `Health`, `Damageable`, `Hitbox`, `Hurtbox`.  
> Ao acertar: dano, knockback, hit stop (attacker/victim), screen shake (leve).  
> Tudo parametrizável pela hitbox.  
> Entregue scripts + valores default bons.

---

## Prompt 7 — Hordas + pooling + AI “barata”

**Prompt:**
> Quero stress test de horda: spawner que chega a 200 inimigos em 2 min.  
> Requisitos:
> - pooling genérico  
> - AI seek player com think 10–20Hz (separado do movimento)  
> - contador de entidades e FPS  
> Entregue scripts + como configurar prefabs.

---

## Prompt 8 — Loop de run + XP + level up (Survivors v0)

**Prompt:**
> Faça o loop mínimo de run: 10 min, XP gems, level up com escolha 1 de 3 upgrades.  
> Quero 10 upgrades simples (stats e 1 ataque automático) data-driven.  
> Entregue scripts + ScriptableObjects + uma UI simples de escolha.

---

## Prompt 9 — Data-driven “de verdade” (registries + IDs)

**Prompt:**
> Consolide data-driven: `CharacterDefinition`, `EnemyDefinition`, `UpgradeDefinition` com IDs e um Registry.  
> Quero que adicionar conteúdo novo seja duplicar asset e preencher números.  
> Gere também um `Docs/data_schema.md` explicando.

---

## Prompt 10 — Steam Lobbies/Invites (sem gameplay online ainda)

**Prompt:**
> Integre Steamworks para: inicialização, overlay, criar/entrar em lobby, aceitar convite.  
> Faça um menu Host/Join e mostre membros do lobby.  
> Entregue passo-a-passo + código (com `#if UNITY_STANDALONE` etc quando fizer sentido).

---

## Prompt 11 — Netcode MVP: 2 players (host-client)

**Prompt:**
> Implemente netcode MVP host-client:
> - host autoritativo  
> - cliente envia inputs por tick  
> - prediction + reconciliação do player local  
> - interpolação do player remoto  
> (sem horda ainda)  
> Entregue scripts e explique claramente onde plugar.

---

## Prompt 12 — Netcode da horda: snapshots compactos

**Prompt:**
> Agora replique horda via snapshots compactos 10–20Hz (não “network object por inimigo”).  
> Host manda array (id, tipo, pos quantizada, hp, flags), cliente mantém pool visual e interpola.  
> Entregue código e limites de banda/CPU esperados.

---

## Prompt 13 — Robustez (rejoin, host quit, progresso parcial)

**Prompt:**
> Implementar robustez mínima para evitar reviews negativas de co-op:
> - Rejoin: se cliente cair, consegue voltar ao lobby e ressincronizar run (snapshot full)  
> - Se host sair: encerrar run com UX clara e salvar progresso parcial (meta-currency) localmente  
> - Mensagens de erro amigáveis e logs  
> Entregue scripts e checklist de testes.

---

## Prompt 14 — Build/QA (CI, crash reporting, analytics mínimos)

**Prompt:**
> Preparar infraestrutura básica:
> - Git + Git LFS (guideline do que vai pra LFS)  
> - GitHub Actions: build Windows e gerar artefato  
> - Crash reporting (Sentry ou equivalente) por define/flag  
> - Analytics leve: RunStart/RunEnd, escolhas de upgrade, disconnect reason  
> Entregue arquivos e passos.

