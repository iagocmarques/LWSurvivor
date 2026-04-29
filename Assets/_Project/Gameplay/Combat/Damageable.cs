using Project.Core.Tick;
using Project.Gameplay.Feedback;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class Damageable : MonoBehaviour, ICombatHurtbox
    {
        [SerializeField] private bool acceptKnockback = true;
        [SerializeField] private float knockbackScale = 1f;
        [SerializeField] private bool triggerGlobalHitStop = true;
        [SerializeField] private bool triggerScreenShake = true;

        private Health _health;
        private PlayerHsmController _hsm;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _hsm = GetComponent<PlayerHsmController>();
        }

        public bool ReceiveHit(in CombatHitInfo hit)
        {
            if (_health == null || _health.IsDead)
                return false;

            if (!_health.ApplyDamage(hit.Damage))
                return false;

            if (acceptKnockback)
            {
                var delta = hit.Knockback * knockbackScale * 0.05f;
                transform.position += new Vector3(delta.x, delta.y, 0f);
            }

            if (triggerGlobalHitStop && hit.HitStopTicks > 0)
                FixedTickSystem.RequestGlobalHitStop(hit.HitStopTicks);

            if (triggerScreenShake && hit.ScreenShakeAmplitude > 0f)
                ScreenShake2D.Instance?.Shake(hit.ScreenShakeAmplitude, Mathf.Max(0.08f, hit.HitStopTicks / 60f));

            // --- Reactive combat: notify player HSM ---
            if (_hsm != null)
            {
                // Compute attacker facing from positions
                int attackerFacing = 1;
                if (hit.Attacker != null)
                {
                    float dx = hit.Attacker.transform.position.x - transform.position.x;
                    attackerFacing = dx >= 0f ? 1 : -1;
                }

                // Derive hit flags from damage as Phase 1 heuristic
                var flags = HitFlags.None;
                if (hit.Damage >= 10)
                    flags |= HitFlags.IsHeavy;

                // Construct enriched HitResult
                var hitResult = HitResult.FromCombatHitInfo(
                    in hit,
                    attackerFacing,
                    fall: 0,           // will be wired when itr data is parsed
                    bdefend: 0,        // will be wired when itr data is parsed
                    effect: StatusEffect.None, // will be wired when itr.effect is parsed
                    flags: flags);

                // Notify the HSM
                _hsm.ApplyReactiveHit(in hitResult);
            }

            // --- Damage popup ---
            if (hit.Damage > 0)
                CombatReadabilityFx.SpawnDamagePopup(transform.position, hit.Damage);

            return true;
        }
    }
}
