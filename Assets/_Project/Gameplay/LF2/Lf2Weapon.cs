using Project.Data;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public enum Lf2WeaponType
    {
        Stick = 100,
        Hoe = 101,
        Knife = 120,
        BaseballBat = 121,
        Stone = 150,
        WoodenBox = 151,
        IceSword = 213,
    }

    public enum Lf2WeaponState
    {
        OnGround,
        Held,
        Thrown,
        Broken,
    }

    public sealed class Lf2Weapon : MonoBehaviour
    {
        [SerializeField] private Lf2WeaponType weaponType;
        [SerializeField] private int durability;
        [SerializeField] private int throwDamage = 10;
        [SerializeField] private float throwSpeed = 8f;

        private WeaponDefinition _definition;
        private Lf2WeaponState _state;
        private int _currentHp;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _pickupCollider;

        private Vector2 _throwVelocity;
        private float _throwLifetime;
        private const float MaxThrowLifetime = 3f;

        public Lf2WeaponType WeaponType => weaponType;
        public Lf2WeaponState State => _state;
        public int CurrentHp => _currentHp;
        public bool CanPickup => _state == Lf2WeaponState.OnGround;
        public int ThrowDamage => _definition != null ? _definition.throwDamage : throwDamage;
        public WeaponDefinition Definition => _definition;
        public Lf2WeaponCategory Category => _definition != null ? _definition.category : Lf2WeaponCategory.Light;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _pickupCollider = GetComponent<Collider2D>();
            _currentHp = durability;
            _state = Lf2WeaponState.OnGround;
        }

        public void Setup(Lf2WeaponType type, int dur, Sprite sprite)
        {
            weaponType = type;
            durability = dur;
            _currentHp = dur;

            if (sprite != null && _spriteRenderer != null)
                _spriteRenderer.sprite = sprite;

            SetState(Lf2WeaponState.OnGround);
        }

        public void Setup(WeaponDefinition def)
        {
            _definition = def;
            if (def == null) return;

            weaponType = (Lf2WeaponType)def.lf2Id;
            durability = def.hp;
            _currentHp = def.hp;
            throwDamage = def.throwDamage;
            throwSpeed = def.throwSpeed;

            if (def.sprite != null && _spriteRenderer != null)
                _spriteRenderer.sprite = def.sprite;

            SetState(Lf2WeaponState.OnGround);
        }

        public void Pickup()
        {
            if (_state != Lf2WeaponState.OnGround) return;
            SetState(Lf2WeaponState.Held);
        }

        public void Throw(Vector2 direction)
        {
            if (_state != Lf2WeaponState.Held) return;

            SetState(Lf2WeaponState.Thrown);
            float speed = _definition != null ? _definition.throwSpeed : throwSpeed;
            _throwVelocity = direction.normalized * speed;
            _throwLifetime = 0f;
        }

        public void OnHit()
        {
            if (_state != Lf2WeaponState.Held) return;

            _currentHp--;
            if (_currentHp <= 0)
                Break();
        }

        public void Drop()
        {
            if (_state != Lf2WeaponState.Held) return;
            SetState(Lf2WeaponState.OnGround);
        }

        private void Break()
        {
            SetState(Lf2WeaponState.Broken);
            Destroy(gameObject, 0.1f);
        }

        private void SetState(Lf2WeaponState newState)
        {
            _state = newState;

            if (_pickupCollider != null)
                _pickupCollider.enabled = newState == Lf2WeaponState.OnGround || newState == Lf2WeaponState.Thrown;

            if (_spriteRenderer != null)
                _spriteRenderer.enabled = newState != Lf2WeaponState.Broken;
        }

        private void FixedUpdate()
        {
            if (_state != Lf2WeaponState.Thrown) return;

            _throwLifetime += Time.fixedDeltaTime;
            if (_throwLifetime >= MaxThrowLifetime)
            {
                Drop();
                return;
            }

            transform.position += (Vector3)_throwVelocity * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_state != Lf2WeaponState.Thrown) return;

            if (other.TryGetComponent<Project.Gameplay.Combat.ICombatHurtbox>(out var hurtbox))
            {
                int dmg = _definition != null ? _definition.throwDamage : throwDamage;
                var knockback = _throwVelocity.normalized * 3f * Lf2StateMachine.PixelToUnit;
                var hitInfo = new Project.Gameplay.Combat.CombatHitInfo(
                    gameObject,
                    dmg,
                    knockback,
                    Project.Gameplay.Combat.CombatAttackId.None,
                    2,
                    0.1f,
                    false,
                    0
                );
                hurtbox.ReceiveHit(in hitInfo);
                Break();
            }
        }

        public static Lf2Weapon SpawnWeapon(Lf2WeaponType type, Vector3 position, Transform parent = null)
        {
            var go = new GameObject($"Weapon_{type}");
            go.transform.position = position;
            if (parent != null) go.transform.SetParent(parent);

            go.layer = LayerMask.NameToLayer("Items");

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 5;

            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(0.5f, 0.5f);

            var weapon = go.AddComponent<Lf2Weapon>();

            int dur = type switch
            {
                Lf2WeaponType.Stick => 100,
                Lf2WeaponType.Hoe => 101,
                Lf2WeaponType.Knife => 120,
                Lf2WeaponType.BaseballBat => 100,
                Lf2WeaponType.Stone => 150,
                _ => 100
            };

            weapon.Setup(type, dur, null);
            return weapon;
        }

        public static Lf2Weapon SpawnWeapon(WeaponDefinition def, Vector3 position, Transform parent = null)
        {
            var go = new GameObject($"Weapon_{def.displayName ?? def.lf2Id.ToString()}");
            go.transform.position = position;
            if (parent != null) go.transform.SetParent(parent);

            go.layer = LayerMask.NameToLayer("Items");

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 5;

            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(0.5f, 0.5f);

            var weapon = go.AddComponent<Lf2Weapon>();
            weapon.Setup(def);
            return weapon;
        }
    }
}
