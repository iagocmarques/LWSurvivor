using System.Collections.Generic;
using Project.Gameplay.Enemies;
using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "DefinitionRegistry", menuName = "_Project/Data/Definition Registry", order = 0)]
    public sealed class DefinitionRegistry : ScriptableObject
    {
        public CharacterDefinition[] characters;
        public EnemyDefinition[] enemies;
        public UpgradeDefinition[] upgrades;

        private readonly Dictionary<string, CharacterDefinition> _charById = new Dictionary<string, CharacterDefinition>();
        private readonly Dictionary<string, EnemyDefinition> _enemyById = new Dictionary<string, EnemyDefinition>();
        private readonly Dictionary<string, UpgradeDefinition> _upgradeById = new Dictionary<string, UpgradeDefinition>();

        public void Rebuild()
        {
            _charById.Clear();
            _enemyById.Clear();
            _upgradeById.Clear();

            if (characters != null)
            {
                for (var i = 0; i < characters.Length; i++)
                {
                    var c = characters[i];
                    if (c == null || string.IsNullOrWhiteSpace(c.id) || _charById.ContainsKey(c.id))
                        continue;
                    _charById.Add(c.id, c);
                }
            }

            if (enemies != null)
            {
                for (var i = 0; i < enemies.Length; i++)
                {
                    var e = enemies[i];
                    if (e == null || string.IsNullOrWhiteSpace(e.id) || _enemyById.ContainsKey(e.id))
                        continue;
                    _enemyById.Add(e.id, e);
                }
            }

            if (upgrades != null)
            {
                for (var i = 0; i < upgrades.Length; i++)
                {
                    var u = upgrades[i];
                    if (u == null || string.IsNullOrWhiteSpace(u.id) || _upgradeById.ContainsKey(u.id))
                        continue;
                    _upgradeById.Add(u.id, u);
                }
            }
        }

        public bool TryGetCharacter(string id, out CharacterDefinition def) => _charById.TryGetValue(id, out def);
        public bool TryGetEnemy(string id, out EnemyDefinition def) => _enemyById.TryGetValue(id, out def);
        public bool TryGetUpgrade(string id, out UpgradeDefinition def) => _upgradeById.TryGetValue(id, out def);

        private void OnEnable()
        {
            Rebuild();
        }

        private void OnValidate()
        {
            Rebuild();
        }
    }
}
