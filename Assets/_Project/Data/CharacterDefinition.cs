using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "CharacterDefinition_", menuName = "_Project/Data/Character Definition", order = 0)]
    public sealed class CharacterDefinition : ScriptableObject
    {
        public string id = "character.hero.01";
        public string displayName = "Hero";
        public PlayerTuning tuning;
        public AttackDefinition jab;
        public AttackDefinition launcher;
        public AttackDefinition dashAttack;
    }
}
