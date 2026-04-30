using System;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [DisallowMultipleComponent]
    public sealed class Mana : MonoBehaviour
    {
        [SerializeField] private int maxMana = 100;

        public int MaxMana => maxMana;
        public int CurrentMana { get; private set; }

        public event Action<Mana> OnManaChanged;

        private void Awake()
        {
            CurrentMana = maxMana;
        }

        public void ConfigureMaxMana(int value, bool refill = true)
        {
            maxMana = Mathf.Max(0, value);
            if (refill)
                CurrentMana = maxMana;
            else
                CurrentMana = Mathf.Min(CurrentMana, maxMana);
            OnManaChanged?.Invoke(this);
        }

        public bool Spend(int amount)
        {
            if (amount <= 0 || CurrentMana < amount)
                return false;

            CurrentMana -= amount;
            OnManaChanged?.Invoke(this);
            return true;
        }

        public void Restore(int amount)
        {
            if (amount <= 0) return;
            CurrentMana = Mathf.Min(CurrentMana + amount, maxMana);
            OnManaChanged?.Invoke(this);
        }

        public void ResetToFull()
        {
            CurrentMana = maxMana;
            OnManaChanged?.Invoke(this);
        }
    }
}
