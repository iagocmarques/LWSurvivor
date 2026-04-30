using System;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [DisallowMultipleComponent]
    public sealed class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private bool destroyOnDeath;

        public int MaxHealth => maxHealth;
        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public event Action<Health> OnDied;
        public event Action<Health, int> OnDamaged;

        private void Awake()
        {
            CurrentHealth = Mathf.Max(1, maxHealth);
        }

        public void ResetToFull()
        {
            CurrentHealth = Mathf.Max(1, maxHealth);
        }

        public void ConfigureMaxHealth(int value, bool refill = true)
        {
            maxHealth = Mathf.Max(1, value);
            if (refill)
                CurrentHealth = maxHealth;
            else
                CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);
        }

        public bool ApplyDamage(int amount)
        {
            if (IsDead || amount <= 0)
                return false;

            CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
            OnDamaged?.Invoke(this, amount);

            if (CurrentHealth > 0)
                return true;

            OnDied?.Invoke(this);
            if (destroyOnDeath)
                Destroy(gameObject);
            return true;
        }

        public void Heal(int amount)
        {
            if (IsDead || amount <= 0) return;
            CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        }
    }
}
