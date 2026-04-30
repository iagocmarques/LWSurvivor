using System.Collections.Generic;
using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "DefinitionRegistry", menuName = "_Project/Data/Definition Registry", order = 0)]
    public sealed class DefinitionRegistry : ScriptableObject
    {
        public CharacterDefinition[] characters;
        public WeaponDefinition[] weapons;

        private readonly Dictionary<int, CharacterDefinition> _charById = new Dictionary<int, CharacterDefinition>();
        private readonly Dictionary<int, WeaponDefinition> _weaponById = new Dictionary<int, WeaponDefinition>();

        public void Rebuild()
        {
            _charById.Clear();
            _weaponById.Clear();

            if (characters != null)
            {
                for (var i = 0; i < characters.Length; i++)
                {
                    var c = characters[i];
                    if (c == null || _charById.ContainsKey(c.lf2Id))
                        continue;
                    _charById.Add(c.lf2Id, c);
                }
            }

            if (weapons != null)
            {
                for (var i = 0; i < weapons.Length; i++)
                {
                    var w = weapons[i];
                    if (w == null || _weaponById.ContainsKey(w.lf2Id))
                        continue;
                    _weaponById.Add(w.lf2Id, w);
                }
            }
        }

        public bool TryGetCharacter(int lf2Id, out CharacterDefinition def) => _charById.TryGetValue(lf2Id, out def);
        public bool TryGetWeapon(int lf2Id, out WeaponDefinition def) => _weaponById.TryGetValue(lf2Id, out def);

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
