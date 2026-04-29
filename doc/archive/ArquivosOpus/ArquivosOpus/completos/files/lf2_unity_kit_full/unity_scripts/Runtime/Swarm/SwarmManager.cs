// Assets/Game/Runtime/Swarm/SwarmManager.cs
//
// Lightweight horde controller for the "Survivors" layer of the game. Designed
// to handle 200-500 enemies per the doc's §3 perf targets.
//
// Per levantamento_e_diretrizes.md §3.3:
//   "Movimento/colisão: 60Hz
//    'Think' de AI: 10–20Hz
//    Replicação de swarm: 10–20Hz (delta/quantização)"
//
// Design choices for cheapness:
//   - Enemies are stored in a struct array, NOT GameObjects. Cheap to iterate.
//   - Each enemy's visual is a pooled GameObject we sync from the struct in LateUpdate.
//   - Movement runs every tick (60Hz). AI "think" runs every N ticks (configurable, ~12Hz).
//   - Enemies hit players via SubmitAttack with a single contact-damage hitbox.

using System.Collections.Generic;
using LF2Game.Combat;
using LF2Game.Data;
using LF2Game.Movement;
using LF2Game.Pooling;
using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Swarm
{
    public struct SwarmInstance
    {
        public bool Alive;
        public float HpRemaining;
        public Position2_5D Pos;
        public Vector2 Vel;            // x = X-vel, y = Depth-vel
        public int    LastThinkTick;
        public int    AnimFrame;
        public int    EnemyDefIndex;   // index into SwarmManager.Defs
        public GameObject Visual;       // pooled
    }

    public sealed class SwarmManager : MonoBehaviour, ITickable, IHitVictim
    {
        public static SwarmManager Instance { get; private set; }

        [Header("Setup")]
        public List<EnemyDefinition> Defs = new();
        public GameObject EnemyVisualPrefab;       // simple SpriteRenderer prefab
        public Transform  Target;                  // usually player

        [Header("Capacity")]
        public int MaxEnemies = 500;

        [Header("Hurtbox (per-enemy AABB)")]
        public Vector2 HurtboxSize   = new(0.5f, 1.4f);
        public Vector2 HurtboxOffset = new(0f, 0.7f);

        SwarmInstance[] _instances;
        PrefabPool _visualPool;

        // We use a single shared "victim id" range starting at SWARM_VICTIM_BASE,
        // adding the slot index. Since HitboxResolver uses int ids, this works
        // up to ~16M enemies which is way more than we'll ever have.
        const int SWARM_VICTIM_BASE = 1_000_000;

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            _instances = new SwarmInstance[MaxEnemies];
        }

        void Start()
        {
            if (EnemyVisualPrefab == null)
            {
                Debug.LogError("SwarmManager: EnemyVisualPrefab not assigned");
                return;
            }
            _visualPool = new PrefabPool(EnemyVisualPrefab, transform, prewarm: 50);
        }

        void OnDestroy() { if (Instance == this) Instance = null; }

        void OnEnable()  { TickRunner.Instance?.Register(this); }
        void OnDisable() { TickRunner.Instance?.Unregister(this); }

        // ---------- spawn / despawn ----------
        public int Spawn(int defIndex, Position2_5D pos)
        {
            if (defIndex < 0 || defIndex >= Defs.Count || Defs[defIndex] == null) return -1;
            int slot = FindFreeSlot();
            if (slot < 0) return -1;

            ref var inst = ref _instances[slot];
            inst.Alive = true;
            inst.HpRemaining = Defs[defIndex].maxHp;
            inst.Pos = pos;
            inst.Vel = Vector2.zero;
            inst.LastThinkTick = 0;
            inst.AnimFrame = 0;
            inst.EnemyDefIndex = defIndex;
            if (inst.Visual == null)
            {
                inst.Visual = _visualPool.Spawn(pos.ToWorld(), Quaternion.identity);
            }
            else
            {
                inst.Visual.SetActive(true);
            }
            return slot;
        }

        public void Despawn(int slot)
        {
            ref var inst = ref _instances[slot];
            if (!inst.Alive) return;
            inst.Alive = false;
            if (inst.Visual != null) _visualPool.Despawn(inst.Visual);
            inst.Visual = null;
        }

        int FindFreeSlot()
        {
            for (int i = 0; i < _instances.Length; i++)
                if (!_instances[i].Alive) return i;
            return -1;
        }

        // ---------- tick ----------
        public void Tick(int tick)
        {
            if (HitFeel.IsHitstopActive) return;
            if (Target == null) return;

            Vector3 tWorld = Target.position;

            for (int i = 0; i < _instances.Length; i++)
            {
                ref var inst = ref _instances[i];
                if (!inst.Alive) continue;
                var def = Defs[inst.EnemyDefIndex];

                // Think (low Hz).
                int thinkInterval = Mathf.Max(1, Mathf.RoundToInt(TickRunner.TICK_RATE / def.thinkHz));
                if (tick - inst.LastThinkTick >= thinkInterval)
                {
                    inst.LastThinkTick = tick;
                    Think(ref inst, def, tWorld);
                }

                // Movement (60Hz).
                inst.Pos.X     += inst.Vel.x * TickRunner.TICK_DT;
                inst.Pos.Depth += inst.Vel.y * TickRunner.TICK_DT;

                // Submit hurtbox (so player attacks can hit us).
                HitboxResolver.Instance?.SubmitVictim(new SwarmVictim(this, i));

                // Submit contact-damage hitbox (always-on, 1 unit reach).
                if (def.contactDamage > 0f)
                {
                    var contact = new HitboxDefinition
                    {
                        kind = HitboxKind.Strike,
                        damage = def.contactDamage,
                        knockback = new Vector2(2.5f, 2.5f),
                        hitstunTicks = 12, hitstopTicks = 4,
                    };
                    Vector3 worldCtr = inst.Pos.ToWorld();
                    Rect contactRect = new(worldCtr.x - 0.4f, worldCtr.y + 0.3f, 0.8f, 0.8f);
                    HitboxResolver.Instance?.SubmitAttack(
                        SWARM_VICTIM_BASE + i, 1, contactRect, inst.Pos.Depth, worldCtr, contact);
                }
            }
        }

        static void Think(ref SwarmInstance inst, EnemyDefinition def, Vector3 targetWorld)
        {
            // Convert target world to logical coords (rough — assumes target is on ground).
            float tx = targetWorld.x;
            // Pure direct chase (sufficient for MVP)
            float dx = tx - inst.Pos.X;
            float dd = (-targetWorld.y / Position2_5D.DEPTH_FACTOR) - inst.Pos.Depth;
            float mag = Mathf.Sqrt(dx*dx + dd*dd);
            if (mag < 0.1f) { inst.Vel = Vector2.zero; return; }
            inst.Vel = new Vector2(dx / mag, dd / mag) * def.walkSpeed;
        }

        void LateUpdate()
        {
            // Sync visuals every render frame (with interpolation we'd lerp; for
            // swarm this is good enough — they don't need sub-pixel smoothness).
            for (int i = 0; i < _instances.Length; i++)
            {
                ref var inst = ref _instances[i];
                if (!inst.Alive || inst.Visual == null) continue;
                inst.Visual.transform.position = inst.Pos.ToWorld();
                var sr = inst.Visual.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingOrder = inst.Pos.ToSortingOrder();
                    var def = Defs[inst.EnemyDefIndex];
                    if (def.frames != null && def.frames.Length > 0)
                    {
                        // pick a walk frame at animFps
                        float t = TickRunner.Instance != null ? TickRunner.Instance.SimTime : Time.time;
                        int idx = (int)(t * def.animFps) % Mathf.Max(1, def.walkFrames.Length);
                        int spriteIdx = def.walkFrames[idx];
                        if (spriteIdx >= 0 && spriteIdx < def.frames.Length)
                            sr.sprite = def.frames[spriteIdx];
                    }
                }
            }
        }

        // ---------- victim bridge ----------
        // Each enemy slot is exposed as an IHitVictim via this proxy.
        sealed class SwarmVictim : IHitVictim
        {
            readonly SwarmManager _mgr;
            readonly int _slot;
            public SwarmVictim(SwarmManager m, int s) { _mgr = m; _slot = s; }

            public Vector3 GetWorldPos()         => _mgr._instances[_slot].Pos.ToWorld();
            public float   GetDepth()            => _mgr._instances[_slot].Pos.Depth;
            public int     GetVictimId()         => SWARM_VICTIM_BASE + _slot;
            public Rect    GetHurtboxWorldRect()
            {
                Vector3 w = _mgr._instances[_slot].Pos.ToWorld();
                return new Rect(
                    w.x + _mgr.HurtboxOffset.x - _mgr.HurtboxSize.x * 0.5f,
                    w.y + _mgr.HurtboxOffset.y - _mgr.HurtboxSize.y * 0.5f,
                    _mgr.HurtboxSize.x, _mgr.HurtboxSize.y);
            }
            public void TakeHit(in HitboxDefinition hb, Vector3 sourcePos)
                => _mgr.OnSlotHit(_slot, hb, sourcePos);
        }

        // SwarmManager itself implements IHitVictim only as a fallback;
        // not actually used in submission (we submit SwarmVictim instances).
        public Vector3 GetWorldPos() => transform.position;
        public float   GetDepth()    => 0f;
        public int     GetVictimId() => -1;
        public Rect    GetHurtboxWorldRect() => default;
        public void    TakeHit(in HitboxDefinition hb, Vector3 sourcePos) { }

        void OnSlotHit(int slot, in HitboxDefinition hb, Vector3 sourcePos)
        {
            ref var inst = ref _instances[slot];
            if (!inst.Alive) return;
            inst.HpRemaining -= hb.damage;
            HitFeel.RequestHitstop(hb.hitstopTicks);
            HitFeel.RequestShake(0.1f);
            // Knockback (simplified).
            Vector3 dir = inst.Pos.ToWorld() - sourcePos;
            float sx = dir.x >= 0f ? 1f : -1f;
            inst.Vel = new Vector2(sx * hb.knockback.x * 0.5f, 0f);
            if (inst.HpRemaining <= 0f) Despawn(slot);
        }
    }
}
