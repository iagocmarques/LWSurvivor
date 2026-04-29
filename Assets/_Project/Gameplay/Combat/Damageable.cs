using Project.Core.Tick;
using Project.Gameplay.Feedback;
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

        private void Awake()
        {
            _health = GetComponent<Health>();
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
                ScreenShake2D.Instance?.Shake(hit.ScreenShakeAmplitude, 0.08f);

            return true;
        }
    }
}
