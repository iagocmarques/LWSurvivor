using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Dummy de teste visual para receber dano via Damageable/Health.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Damageable))]
    public sealed class DamageableDummy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color hurtFlash = new Color(1f, 0.35f, 0.35f, 1f);
        [SerializeField] private float hurtFlashSeconds = 0.08f;

        private Color _baseColor;
        private float _flashLeft;
        private Health _health;

        private void Reset()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            if (sprite == null)
                sprite = GetComponent<SpriteRenderer>();

            if (sprite != null)
                _baseColor = sprite.color;

            _health = GetComponent<Health>();
            if (_health != null)
                _health.OnDamaged += OnDamaged;

            var hurt = LayerMask.NameToLayer("Hurtbox");
            if (hurt >= 0)
                gameObject.layer = hurt;

            EnsureCollider();
        }

        private void TickVisual(float dt)
        {
            if (_flashLeft > 0f)
            {
                _flashLeft -= dt;
                if (sprite != null)
                    sprite.color = hurtFlash;
            }
            else if (sprite != null)
            {
                sprite.color = _baseColor;
            }
        }

        private void Update()
        {
            TickVisual(Time.deltaTime);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.OnDamaged -= OnDamaged;
        }

        private void EnsureCollider()
        {
            var box = GetComponent<BoxCollider2D>();
            if (box == null)
            {
                box = gameObject.AddComponent<BoxCollider2D>();
                box.size = new Vector2(0.9f, 1.2f);
            }

            box.isTrigger = true;
        }

        private void OnDamaged(Health _, int __)
        {
            _flashLeft = hurtFlashSeconds;
        }
    }
}
