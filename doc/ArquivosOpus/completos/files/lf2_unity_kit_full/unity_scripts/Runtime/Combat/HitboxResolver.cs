// Assets/Game/Runtime/Combat/HitboxResolver.cs
//
// Per-tick singleton that resolves hits between attackers and victims.
//
// Flow per tick:
//   1. Attackers (PlayerFSM, Projectiles) call SubmitAttack() with their
//      current frame's hitboxes (in world coords).
//   2. Victims (PlayerFSM, SwarmEnemy) call SubmitVictim() with their hurtbox
//      (in world coords).
//   3. After all submissions, ResolveAll() iterates pairs, applies damage,
//      requests hit feel, and clears the per-tick lists.
//
// MVP simplifications (clearly marked as TODO for post-MVP):
//   - Hurtbox is a single AABB per character, not per-frame from FrameDefinition.bdy[].
//   - Depth tolerance is a fixed lane thickness (DEPTH_TOLERANCE).
//   - No team-vs-team filtering yet — players can hit players. Add a "team" field
//     to attackers/victims when adding versus modes.
//   - Each attacker can hit each victim at most once per move (tracked by id).

using System.Collections.Generic;
using LF2Game.Data;
using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Combat
{
    public interface IHitVictim
    {
        Vector3 GetWorldPos();
        Rect    GetHurtboxWorldRect();
        float   GetDepth();
        int     GetVictimId();
        void    TakeHit(in HitboxDefinition hitbox, Vector3 sourcePos);
    }

    public sealed class HitboxResolver : MonoBehaviour, ITickable
    {
        public const float DEPTH_TOLERANCE = 0.6f; // "lane thickness" per the doc

        public static HitboxResolver Instance { get; private set; }

        struct AttackSubmission
        {
            public int    attackerId;
            public int    moveInstanceId; // unique per move start; resets on new move
            public Rect   worldRect;
            public float  depth;
            public Vector3 sourcePos;
            public HitboxDefinition hb;
        }

        readonly List<AttackSubmission> _attacks  = new();
        readonly List<IHitVictim>       _victims  = new();
        // pair (attackerId, moveInstanceId, victimId) -> already hit?
        readonly HashSet<long>          _hitOnce  = new();

        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        void OnDestroy() { if (Instance == this) Instance = null; }

        void OnEnable()  { TickRunner.Instance?.Register(this); }
        void OnDisable() { TickRunner.Instance?.Unregister(this); }

        public void Tick(int tick)
        {
            // Resolve runs LAST in the tick (after attackers and victims submit).
            // Make sure HitboxResolver is registered after them, or use a higher
            // priority list. For MVP we rely on submission happening before this
            // ITickable runs by virtue of registration order in BootstrapInstaller.
            ResolveAll();
        }

        public void SubmitAttack(int attackerId, int moveInstanceId, Rect worldRect, float depth, Vector3 sourcePos, in HitboxDefinition hb)
        {
            if (hb.kind != HitboxKind.Strike && hb.kind != HitboxKind.Grab && hb.kind != HitboxKind.Projectile) return;
            _attacks.Add(new AttackSubmission {
                attackerId = attackerId, moveInstanceId = moveInstanceId,
                worldRect = worldRect, depth = depth, sourcePos = sourcePos, hb = hb
            });
        }

        public void SubmitVictim(IHitVictim v) => _victims.Add(v);

        /// <summary>Reset hit-once tracking when a new move starts (so the same attacker can hit again).</summary>
        public void ClearHitOnceFor(int attackerId, int moveInstanceId)
        {
            // Conservative approach: drop entries by scanning. For 4 players + 500 swarm
            // this stays small enough.
            var toRemove = new List<long>();
            foreach (long key in _hitOnce)
            {
                int aid = (int)(key >> 40) & 0xFFFF;
                int mid = (int)(key >> 24) & 0xFFFF;
                if (aid == (attackerId & 0xFFFF) && mid == (moveInstanceId & 0xFFFF)) toRemove.Add(key);
            }
            foreach (long k in toRemove) _hitOnce.Remove(k);
        }

        void ResolveAll()
        {
            for (int a = 0; a < _attacks.Count; a++)
            {
                ref var atk = ref System.Runtime.InteropServices.CollectionsMarshal.AsSpan(_attacks)[a];
                for (int v = 0; v < _victims.Count; v++)
                {
                    var victim = _victims[v];
                    int vid = victim.GetVictimId();
                    if (vid == atk.attackerId) continue; // can't hit yourself

                    // Depth check (lane thickness).
                    if (Mathf.Abs(victim.GetDepth() - atk.depth) > DEPTH_TOLERANCE) continue;

                    // AABB check.
                    if (!atk.worldRect.Overlaps(victim.GetHurtboxWorldRect())) continue;

                    long key = ((long)(atk.attackerId & 0xFFFF) << 40)
                             | ((long)(atk.moveInstanceId & 0xFFFF) << 24)
                             | (long)(vid & 0xFFFFFF);
                    if (_hitOnce.Contains(key)) continue;
                    _hitOnce.Add(key);

                    victim.TakeHit(atk.hb, atk.sourcePos);
                }
            }

            _attacks.Clear();
            _victims.Clear();
        }
    }
}
