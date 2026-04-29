using Project.Core.Tick;
using Project.Gameplay.Combat;
using Project.Gameplay.Visual;
using UnityEngine;

namespace Project.Gameplay.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Damageable))]
    public sealed class EnemyAgent : MonoBehaviour, ITickable
    {
        [SerializeField] private EnemyDefinition definition;

        private Transform _target;
        private Health _health;
        private Vector2 _steerDir;
        private float _nextThinkAt;
        private EnemySpawnerDirector _ownerSpawner;
        private float _touchCooldown;
        public int NetId { get; private set; }
        private SpriteRenderer _sprite;
        private Color _baseTint = Color.white;
        private float _flashLeft;
        private Lf2EnemySpriteAnimator _lf2Animator;

        public float XpDrop => definition != null ? definition.xpDrop : 1f;
        public string DefinitionId => definition != null ? definition.id : "enemy.unknown";

        public void Initialize(int netId, EnemyDefinition def, Transform target, EnemySpawnerDirector ownerSpawner)
        {
            NetId = netId;
            definition = def;
            _target = target;
            _ownerSpawner = ownerSpawner;
            _nextThinkAt = 0f;
            _steerDir = Vector2.zero;
            _touchCooldown = 0f;

            EnsureHealth();
            _health.ResetToFull();
            ApplyVisualFromDefinition();
        }

        private void Awake()
        {
            EnsureHealth();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
            EnsureHealth();
            _health.OnDied += OnDied;
            _health.OnDamaged += OnDamaged;
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
            if (_health != null)
            {
                _health.OnDied -= OnDied;
                _health.OnDamaged -= OnDamaged;
            }
        }

        public void Tick(in TickContext context)
        {
            if (definition == null || _target == null || _health == null || _health.IsDead)
                return;

            if (_touchCooldown > 0f)
                _touchCooldown -= context.FixedDelta;

            var now = (float)context.UnscaledNow;
            if (now >= _nextThinkAt)
            {
                var toTarget = (Vector2)(_target.position - transform.position);
                _steerDir = toTarget.sqrMagnitude > 0.0001f ? toTarget.normalized : Vector2.zero;
                _nextThinkAt = now + Mathf.Max(0.05f, definition.thinkInterval);
            }

            var delta = (Vector3)(_steerDir * (definition.moveSpeed * context.FixedDelta));
            transform.position += delta;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (definition == null || _touchCooldown > 0f)
                return;

            if (!other.TryGetComponent<ICombatHurtbox>(out var hurt))
                return;

            var toVictim = (Vector2)(other.transform.position - transform.position);
            if (toVictim.sqrMagnitude < 0.0001f)
                toVictim = Vector2.right;

            var hit = new CombatHitInfo(
                gameObject,
                Mathf.RoundToInt(definition.touchDamage),
                toVictim.normalized * 2f,
                CombatAttackId.None,
                1,
                0.05f,
                false);

            if (hurt.ReceiveHit(in hit))
                _touchCooldown = 0.4f;
        }

        private void OnDied(Health _)
        {
            CombatReadabilityFx.SpawnKillPopup(transform.position, definition != null ? definition.displayName : "enemy");
            _ownerSpawner?.NotifyEnemyDied(this);
        }

        private void OnDamaged(Health _, int amount)
        {
            _flashLeft = definition != null ? definition.hitFlashSeconds : 0.06f;
            CombatReadabilityFx.SpawnDamagePopup(transform.position, amount);
        }

        private void EnsureHealth()
        {
            _health = GetComponent<Health>();
            if (_health == null)
                return;

            _health.ConfigureMaxHealth(definition != null ? definition.maxHealth : 20, true);
        }

        private void ApplyVisualFromDefinition()
        {
            if (definition == null)
                return;

            if (_sprite == null)
                _sprite = GetComponent<SpriteRenderer>();

            if (_sprite != null)
            {
                var sp = Lf2VisualLibrary.GetEnemySprite(definition.id);
                if (sp != null)
                    _sprite.sprite = sp;

                _baseTint = definition.tint;
                _sprite.color = _baseTint;
            }

            if (_lf2Animator == null)
                _lf2Animator = GetComponent<Lf2EnemySpriteAnimator>();
            if (_lf2Animator == null)
                _lf2Animator = gameObject.AddComponent<Lf2EnemySpriteAnimator>();
            _lf2Animator.Configure(definition.id);

            transform.localScale = Vector3.one * Mathf.Max(0.25f, definition.scale);
        }

        private void Update()
        {
            if (_sprite == null)
                return;

            if (_flashLeft > 0f)
            {
                _flashLeft -= Time.deltaTime;
                _sprite.color = Color.Lerp(_baseTint, Color.white, 0.85f);
            }
            else
            {
                _sprite.color = _baseTint;
            }
        }
    }
}
