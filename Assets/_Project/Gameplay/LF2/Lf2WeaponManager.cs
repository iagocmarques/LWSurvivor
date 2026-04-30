using Project.Data;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2WeaponManager
    {
        private static readonly Collider2D[] PickupScratch = new Collider2D[8];

        private Lf2StateMachine _sm;
        private Transform _owner;

        private Lf2Weapon _equippedWeapon;
        private float _pickupRadius = 1.5f;
        private LayerMask _itemLayer;

        private int _weaponAttackHits;

        private const int DefaultWeaponAttackNeutralFrame = 90;
        private const int DefaultWeaponAttackForwardFrame = 91;
        private const int DefaultWeaponAttackBackFrame = 92;

        public bool HasWeapon => _equippedWeapon != null;
        public Lf2Weapon EquippedWeapon => _equippedWeapon;

        public void Initialize(Lf2StateMachine sm, Transform owner, LayerMask itemLayer)
        {
            _sm = sm;
            _owner = owner;
            _itemLayer = itemLayer;
        }

        public bool TryPickup()
        {
            if (_equippedWeapon != null) return false;

            var weapon = FindNearbyWeapon();
            if (weapon == null) return false;

            Equip(weapon);
            return true;
        }

        public void Throw(Vector2 direction)
        {
            if (_equippedWeapon == null) return;

            var weapon = _equippedWeapon;
            Unequip();
            weapon.Throw(direction);

            if (_sm != null)
                _sm.SetFrame(0);
        }

        public void Drop()
        {
            if (_equippedWeapon == null) return;

            _equippedWeapon.Drop();
            Unequip();

            if (_sm != null)
                _sm.SetFrame(0);
        }

        public void OnWeaponHit()
        {
            if (_equippedWeapon == null) return;

            _equippedWeapon.OnHit();
            _weaponAttackHits++;

            if (_equippedWeapon.State != Lf2WeaponState.Held)
            {
                Unequip();
                if (_sm != null)
                    _sm.SetFrame(0);
            }
        }

        public bool TryStartWeaponAttack()
        {
            if (_equippedWeapon == null) return false;
            if (_sm == null) return false;

            int frameId = ResolveWeaponAttackFrame();
            _sm.SetFrame(frameId);
            return true;
        }

        public Lf2Weapon FindNearbyWeapon()
        {
            if (_owner == null) return null;

            int count = Physics2D.OverlapCircle(_owner.position, _pickupRadius, new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = _itemLayer,
                useTriggers = true
            }, PickupScratch);

            Lf2Weapon closest = null;
            float closestDist = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                var weapon = PickupScratch[i].GetComponent<Lf2Weapon>();
                if (weapon == null || !weapon.CanPickup) continue;

                float dist = Vector2.Distance(_owner.position, PickupScratch[i].transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = weapon;
                }
            }

            return closest;
        }

        private void Equip(Lf2Weapon weapon)
        {
            _equippedWeapon = weapon;
            _weaponAttackHits = 0;
            weapon.Pickup();

            weapon.transform.SetParent(_owner);
            weapon.transform.localPosition = new Vector3(0.3f, 0f, 0f);
        }

        private void Unequip()
        {
            if (_equippedWeapon == null) return;

            _equippedWeapon.transform.SetParent(null);
            _equippedWeapon = null;
            _weaponAttackHits = 0;
        }

        private int ResolveWeaponAttackFrame()
        {
            if (_sm == null) return DefaultWeaponAttackNeutralFrame;

            var def = _equippedWeapon?.Definition;
            if (def == null)
                return DefaultWeaponAttackNeutralFrame;

            float moveX = _sm.FacingRight ? 1f : -1f;
            bool isForward = moveX > 0f;

            if (isForward && _sm.HasFrame(def.attackForwardFrame))
                return def.attackForwardFrame;
            if (!isForward && _sm.HasFrame(def.attackBackFrame))
                return def.attackBackFrame;

            return def.attackNeutralFrame;
        }

        public void Tick()
        {
            if (_equippedWeapon == null) return;

            bool isAttacking = _sm != null && _sm.CurrentState == Lf2State.Attacking;
            if (!isAttacking && _equippedWeapon.State == Lf2WeaponState.Held)
            {
                _equippedWeapon.transform.localPosition = new Vector3(0.3f, 0f, 0f);
            }
        }

        public void Cleanup()
        {
            if (_equippedWeapon != null)
            {
                Drop();
            }
        }
    }
}
