using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public enum Lf2ItemType
    {
        Milk,
        Beer,
        Chicken,
        Dumpling,
        Weapon,
    }

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class Lf2Item : MonoBehaviour
    {
        [SerializeField] private Lf2ItemType itemType;
        [SerializeField] private int healAmount = 50;
        [SerializeField] private int mpRestoreAmount = 50;
        [SerializeField] private Lf2WeaponType weaponType;

        private bool _consumed;

        public Lf2ItemType ItemType => itemType;
        public Lf2WeaponType WeaponType => weaponType;

        public void Setup(Lf2ItemType type)
        {
            itemType = type;
            _consumed = false;
        }

        public void SetupWeapon(Lf2WeaponType type)
        {
            itemType = Lf2ItemType.Weapon;
            weaponType = type;
            _consumed = false;
        }

        public void ApplyTo(GameObject target)
        {
            if (_consumed || target == null) return;
            _consumed = true;

            switch (itemType)
            {
                case Lf2ItemType.Milk:
                {
                    var health = target.GetComponent<Health>();
                    if (health != null)
                        health.Heal(healAmount);
                    break;
                }

                case Lf2ItemType.Beer:
                {
                    var mana = target.GetComponent<Mana>();
                    if (mana != null)
                        mana.Restore(mpRestoreAmount);
                    break;
                }

                case Lf2ItemType.Chicken:
                {
                    var health = target.GetComponent<Health>();
                    if (health != null)
                        health.Heal(healAmount * 2);
                    break;
                }

                case Lf2ItemType.Dumpling:
                {
                    var health = target.GetComponent<Health>();
                    if (health != null)
                        health.Heal(healAmount / 2);
                    var mana = target.GetComponent<Mana>();
                    if (mana != null)
                        mana.Restore(mpRestoreAmount / 2);
                    break;
                }

                case Lf2ItemType.Weapon:
                {
                    Lf2Weapon.SpawnWeapon(weaponType, target.transform.position);
                    break;
                }
            }

            Destroy(gameObject);
        }
    }
}
