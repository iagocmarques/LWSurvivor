#if false
using Project.Core.Tick;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [DisallowMultipleComponent]
    public sealed class AutoPulseAura : MonoBehaviour, ITickable
    {
        [SerializeField] private float radius = 2.2f;
        [SerializeField] private int damage = 3;
        [SerializeField] private float intervalSeconds = 0.45f;
        [SerializeField] private LayerMask enemyMask = ~0;

        private readonly Collider2D[] _hits = new Collider2D[128];
        private float _cooldown;
        private int _baseDamage;
        private float _baseInterval;
        private float _baseRadius;
        private PlayerRuntimeStats _stats;

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
            _cooldown = 0f;
            _baseDamage = damage;
            _baseInterval = intervalSeconds;
            _baseRadius = radius;
            _stats = GetComponent<PlayerRuntimeStats>();
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
        }

        public void Configure(int pulseDamage, float pulseInterval, float pulseRadius)
        {
            damage = Mathf.Max(1, pulseDamage);
            intervalSeconds = Mathf.Max(0.05f, pulseInterval);
            radius = Mathf.Max(0.2f, pulseRadius);

            _baseDamage = damage;
            _baseInterval = intervalSeconds;
            _baseRadius = radius;
        }

        public void Tick(in TickContext context)
        {
            _cooldown -= context.FixedDelta;
            if (_cooldown > 0f)
                return;

            var damageNow = _baseDamage;
            var intervalNow = _baseInterval;
            var radiusNow = _baseRadius;
            if (_stats != null)
            {
                damageNow = Mathf.Max(1, Mathf.RoundToInt(_baseDamage * _stats.DamageMultiplier));
                intervalNow = Mathf.Max(0.05f, _baseInterval / Mathf.Max(0.1f, _stats.PulseRateMultiplier));
                radiusNow = Mathf.Max(0.2f, _baseRadius * _stats.PulseRadiusMultiplier);
            }

            _cooldown = intervalNow;
            var filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = enemyMask,
                useTriggers = true
            };
            var count = Physics2D.OverlapCircle((Vector2)transform.position, radiusNow, filter, _hits);
            for (var i = 0; i < count; i++)
            {
                var c = _hits[i];
                if (c == null)
                    continue;

                if (!c.TryGetComponent<ICombatHurtbox>(out var hurt))
                    continue;

                var dir = ((Vector2)c.transform.position - (Vector2)transform.position).normalized;
                if (dir.sqrMagnitude < 0.001f)
                    dir = Vector2.right;

                var hit = new CombatHitInfo(gameObject, damageNow, dir * 1.5f, CombatAttackId.None, 1, 0.04f, false);
                hurt.ReceiveHit(in hit);
            }
        }
    }
}
#endif
