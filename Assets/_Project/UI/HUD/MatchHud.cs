using Project.Gameplay.Combat;
using TMPro;
using UnityEngine;

namespace Project.UI.HUD
{
    public sealed class MatchHud : MonoBehaviour
    {
        [System.Serializable]
        public struct PlayerSlot
        {
            public HealthBar healthBar;
            public ManaBar manaBar;
            public TextMeshProUGUI nameLabel;
        }

        [SerializeField] private PlayerSlot player1;
        [SerializeField] private PlayerSlot player2;
        [SerializeField] private TextMeshProUGUI roundLabel;

        public void BindPlayer(int index, Health health, Mana mana, string characterName)
        {
            var slot = GetSlot(index);
            slot.healthBar?.Bind(health);
            slot.manaBar?.Bind(mana);
            if (slot.nameLabel != null)
                slot.nameLabel.text = characterName ?? string.Empty;
        }

        public void SetRound(int round)
        {
            if (roundLabel != null)
                roundLabel.text = $"Round {round}";
        }

        public void SetPhase(string phase)
        {
            if (roundLabel != null)
                roundLabel.text = phase ?? string.Empty;
        }

        private PlayerSlot GetSlot(int index)
        {
            return index == 0 ? player1 : player2;
        }
    }
}
