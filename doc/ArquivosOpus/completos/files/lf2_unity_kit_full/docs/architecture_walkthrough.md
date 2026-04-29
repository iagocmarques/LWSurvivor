# Architecture walkthrough

> Como as peças do `unity_scripts/Runtime/` se conectam. Leia depois do `lf2_assets_guide.md`.

---

## Visão geral em 1 diagrama

```
                    ┌──────────────────────┐
                    │     TickRunner       │  60Hz, drives everything
                    │   (singleton GO)     │
                    └──────────┬───────────┘
                               │ ITickable.Tick(int)
        ┌──────────────────────┼─────────────────────────────┐
        │                      │                             │
        ▼                      ▼                             ▼
┌───────────────┐    ┌───────────────────┐         ┌────────────────────┐
│PlayerInput    │    │   PlayerFSM       │         │   SwarmManager     │
│Reader         │    │  (per player)     │         │  (one for all      │
│ feeds buffer  │    │  reads input,     │         │   horde enemies)   │
└───────┬───────┘    │  drives sprite,   │         │                    │
        │            │  submits hits     │         │  submits hits too  │
        │            └─────────┬─────────┘         └─────────┬──────────┘
        │                      │                             │
        │                      ▼                             ▼
        │              ┌───────────────────────────────────────┐
        │              │       HitboxResolver                   │
        │              │   pairwise check, applies damage       │
        └─────────────►│   + HitFeel.RequestHitstop / Shake     │
                       └───────────────────────────────────────┘
                                       │
                                       ▼
                              ┌──────────────────┐
                              │  TakeHit on each  │
                              │  IHitVictim       │
                              └──────────────────┘

Visual side (per render frame):
  CharacterController2_5D.LateUpdate
    -> interpolates between previous tick and current tick (Alpha 0..1)
    -> sets transform.position from Position2_5D (X, Depth, Height)
    -> sets sortingOrder from Depth
    -> sets sprite.flipX from FacingLeft

CameraShake.LateUpdate
    -> reads HitFeel.SampleShakeMagnitude(), offsets the Main Camera
```

---

## Por que cada peça existe (e o que substituiria)

### `TickRunner` + `ITickable`
- **Por quê**: `levantamento_e_diretrizes.md` §3.1 manda tick fixo 60Hz. Sem isso, o combat feel varia com framerate, e o netcode futuro fica impossível.
- **Substituiria com FixedUpdate?** Não — FixedUpdate tá preso à física da Unity, e por padrão é 50Hz. Trocar pra 60 é fácil mas perde controle determinístico do ordering de subscribers, que importa quando inserir host-authority gate ou rollback.

### `PlayerInputReader` + `InputBuffer`
- **Por quê**: `levantamento_e_diretrizes.md` §4 manda buffer global de ~8 frames. Sem isso, jogadores vão "perder" inputs apertados perto da janela de cancel, e o combat feel vira engessado.
- **Substituiria com Input System novo?** Sim, em produção. Por enquanto o legacy `Input.GetKey` tá ali pra evitar shipar um InputActionAsset no kit.

### `Position2_5D` + `CharacterController2_5D`
- **Por quê**: §4 manda 2.5D estilo LF2 (X horizontal, Y profundidade, Z altura). Unity Transform mapeia tudo num plano 2D + sortingOrder.
- **Substituiria com Rigidbody2D?** Não — não temos física rígida. Combat resolution é AABB+depth tolerance no `HitboxResolver`. Rigidbody traria CCD, layering e overhead que não precisamos.
- **Por que separar logical Position e world Transform?** Render-side interpolation. Sem isso, num monitor de 144Hz o sprite anda em "passos" de 60Hz e fica trêmulo.

### `MoveDefinition` + `FrameDefinition` + `HitboxDefinition`
- **Por quê**: §5 manda data-driven. Adicionar um novo move = duplicar um asset, não escrever código.
- **Por que NÃO mapeamos 1:1 dos `.dat` do LF2?** Ver `lf2_assets_guide.md` §5.3.

### `PlayerFSM`
- **Por quê**: §4 manda FSM hierárquica simples (Idle/Move/Dash/Attack/Hitstun/Dead).
- **Por que não Animator/StateMachineBehaviour?** O Mecanim é amigável pra animação, ruim pra gameplay logic determinística. Cancels, frame-perfect timings, network syncing — tudo fica forçado em StateMachineBehaviour. FSM em código é mais leve e menos misterioso.

### `HitboxResolver`
- **Por quê**: hits precisam ser resolvidos num único ponto pra: (a) determinismo (mesma ordem todo tick), (b) host authority no netcode, (c) easy debug ("quem bateu em quem nesse tick?").
- **Substituiria com OnTriggerEnter2D?** Não — Unity collider events disparam fora do tick e em ordem indeterminada.

### `HitFeel` (hitstop + shake)
- **Por quê**: §4 manda parametrizado por golpe. `causesKnockdown`/`hitstopTicks`/`knockback` ficam em `HitboxDefinition`.
- **Por que `IsHitstopActive` no lugar de `Time.timeScale = 0`?** Time.timeScale congela TUDO incluindo UI e VFX que a gente quer continuar rodando. Hitstop só precisa pausar a simulação de combat.

### `ObjectPool` + `PrefabPool`
- **Por quê**: §3.2 manda pooling agressivo. Sem isso, GC trava o framerate com 200 inimigos morrendo.
- **Substituiria com `UnityEngine.Pool`?** Pode-se. O pool nosso é levinho e sem allocs internos. Em produção, qualquer um serve.

### `SwarmManager`
- **Por quê**: §3.3 manda Movement 60Hz + AI think 10-20Hz. Sem split de frequência, 500 inimigos pensando 60x/s consomem CPU pra nada.
- **Por que struct array em vez de N MonoBehaviours?** §3.2 manda "evitar overhead por entidade". Cada MonoBehaviour ativa custa ~3-5KB e GameObject Update overhead. Struct array é praticamente livre.

### `Registry`
- **Por quê**: §2 manda lookup por ID. Sem registry, código fica cheio de `Resources.Load("Bandit")` (e Resources é deprecated).
- **Substituiria com Addressables?** Sim, quando ficar com 50+ characters. Pra MVP, registry SO simples basta.

### `BootstrapInstaller`
- **Por quê**: precisa de uma única entrypoint que garante a ordem: TickRunner antes de tudo, HitboxResolver depois das submissões.
- **Substituiria com Zenject/VContainer?** DI pode-se introduzir depois. No MVP é overkill.

### `PixelPerfectCameraSetup`
- **Por quê**: pixel art borrado é o erro #1 em jogos 2D na Unity. URP + Pixel Perfect Camera resolve, mas só se configurada certo.

---

## Fluxo de um soco do começo ao fim

Vamos rastrear: jogador aperta `A` no Bandit, o soco conecta num inimigo do swarm.

### Frame N (render)
1. `PlayerInputReader.Update()` detecta `Input.GetKeyDown(A)` → `Buffer.NotePress(InputAction.Attack)`. Timestamp = `TickRunner.CurrentTick`.

### Tick X (60Hz)
1. `TickRunner.Update()` acumula deltaTime, dispara `Step()`.
2. **Subscribers em ordem**:
   1. `PlayerInputReader.Tick()` — atualiza tick counter do buffer.
   2. `PlayerFSM.Tick()`:
      - State é `Idle`, chama `TickFreeMovement()`.
      - `Buffer.Consume(Attack)` retorna true.
      - `TryStartMoveFor(Attack)` encontra `Bandit_Punch1` com `validEntryStates: [Idle, Walk]` → `StartMove()`.
      - `StartMove` define `_move = Punch1`, `_frameIndex = 0`, `_state = Attack`.
      - `EnterFrame(frame[0])` (windup): `_frameTicksRemaining = 4`. Sem hitboxes (windup).
   3. `SwarmManager.Tick()` itera os 200 inimigos, submete hurtboxes pra cada um.
   4. `HitboxResolver.Tick()` — `_attacks` está vazia (windup), nada a resolver.

### Tick X+4 (active frame)
- `PlayerFSM.Tick()`:
  - State é `Attack`, chama `TickActiveMove()`.
  - `_frameTicksRemaining` decrementou pra 0, entra em `frame[1]` (active).
  - `EnterFrame(frame[1])` chama `SubmitFrameHitboxes`.
  - `HitboxResolver.SubmitAttack(entityId, moveInstanceId, worldRect, depth, sourcePos, hb)`.
- `HitboxResolver.Tick()`:
  - Pra cada attack, pra cada victim, checa AABB overlap + depth tolerance.
  - Encontra um inimigo do swarm dentro do rect → `victim.TakeHit(hb, sourcePos)`.
- Inimigo recebe hit:
  - `SwarmVictim.TakeHit()` → `SwarmManager.OnSlotHit()`.
  - `HpRemaining -= 8`, `HitFeel.RequestHitstop(4)`, `HitFeel.RequestShake(0.1)`.
  - Knockback: `inst.Vel = (sx * 3.5 * 0.5, 0)` (swarm gets reduced knockback).

### Tick X+5 a X+8 (hitstop active)
- `HitFeel.IsHitstopActive` retorna true → `PlayerFSM.Tick()` early-return, `SwarmManager.Tick()` early-return.
- Visualmente: tudo congelado por 4 ticks (~67ms). É o "punch" do hit feel.

### Tick X+9 (hitstop ends)
- Simulação retoma. Inimigo morre se hp<=0 → `Despawn(slot)`. Visual sumido.

### Tick X+9 em diante (recovery)
- `PlayerFSM` continua nos frames de recovery do Punch1.
- Se o jogador apertar `A` durante essa janela e `cancelOnAttack: Punch2` estiver setado, o soco enca­deia.

---

## Coisas que ainda NÃO estão implementadas (próximos sprints)

- **Defense move**: o input/buffer já existe; falta autorar o move com hurtboxes especiais.
- **Block/parry mechanic**: vai ser uma flag no `HitboxDefinition` ("blockable") + state `Blocking` no FSM.
- **Projectiles**: estrutura semelhante ao SwarmEnemy, com TTL e visual lerp. Reutiliza o `HitboxResolver`.
- **Stages/StageDefinition**: o backgrounds estão prontos (PNGs com alpha). Falta um `StageDefinition` SO + parallax controller.
- **Save / meta-progression**: o `levantamento_e_diretrizes.md` §7 fala em "12-20 unlocks persistentes". Vai ser um SO de unlocks + serializador JSON.
- **Netcode**: o tick fixo + submissão de hitboxes via singleton já preparam o terreno. Falta integrar Steamworks.NET + Snapshot serialization.
- **Telemetria**: hooks vazios pra começar logging.

---

## Decisões de design que valem revisar

Estas estão no MVP por simplicidade, mas dá pra reabrir conversa:

1. **`HitFeel` é singleton estático.** Funciona pra single player. Pra multiplayer, hitstop pode ser por-jogador (pra não congelar o jogo dos outros). Mantenho global pelo MVP.
2. **Hurtbox é uma única AABB por personagem.** O `FrameDefinition.hurtboxes[]` existe na data layer mas o FSM ainda não consulta. Adicionar quando começar a importar movimentos especiais (e.g. dash com invuln frames).
3. **SwarmEnemy não tem moves.** Só "andar até o player + dano de contato". Pra bosses ou mid-tier, use `CharacterDefinition` + `PlayerFSM`-like (talvez extraia uma `ActorFSM` base).
4. **Combat ignora altura (Z) na resolução.** Se o jogador pula e o soco fica no ar, ainda atinge inimigo no chão. Adicionar Z check em `HitboxResolver` quando a vertical importar.
