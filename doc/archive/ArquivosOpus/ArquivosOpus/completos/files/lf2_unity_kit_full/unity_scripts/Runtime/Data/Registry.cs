// Assets/Game/Runtime/Data/Registry.cs
//
// Generic ID-based asset registry. Per levantamento_e_diretrizes.md §2:
//   "Authoring: ScriptableObjects.
//    Runtime: Registry por IDs (e, quando fizer sentido, Addressables/export)."
//
// Author the registry asset (Create > LF2Game > Registry) and drag your
// CharacterDefinitions / EnemyDefinitions / etc. into the list.
//
// At runtime: Registry.Find<CharacterDefinition>("bandit") -> the SO instance.
// At runtime: Registry.All<EnemyDefinition>() -> IEnumerable<EnemyDefinition>.
//
// For now this is a plain ScriptableObject with one list per type. When the
// roster grows, swap to Addressables-loaded async registries.

using System.Collections.Generic;
using UnityEngine;

namespace LF2Game.Data
{
    public interface IRegistryAsset
    {
        string Id { get; }
    }

    [CreateAssetMenu(menuName = "LF2Game/Registry", fileName = "GameRegistry")]
    public sealed class Registry : ScriptableObject
    {
        [Header("Author content here")]
        public List<CharacterDefinition> Characters = new();
        public List<EnemyDefinition>     Enemies    = new();
        public List<MoveDefinition>      Moves      = new();

        // Cached at startup.
        Dictionary<string, CharacterDefinition> _charLookup;
        Dictionary<string, EnemyDefinition>     _enemyLookup;
        Dictionary<string, MoveDefinition>      _moveLookup;

        public static Registry Active { get; private set; }

        public void Activate()
        {
            Active = this;
            _charLookup  = BuildLookup(Characters, c => c.id);
            _enemyLookup = BuildLookup(Enemies,    e => e.id);
            _moveLookup  = BuildLookup(Moves,      m => m.id);
        }

        public CharacterDefinition GetCharacter(string id)
            => _charLookup != null && _charLookup.TryGetValue(id, out var c) ? c : null;

        public EnemyDefinition GetEnemy(string id)
            => _enemyLookup != null && _enemyLookup.TryGetValue(id, out var e) ? e : null;

        public MoveDefinition GetMove(string id)
            => _moveLookup != null && _moveLookup.TryGetValue(id, out var m) ? m : null;

        static Dictionary<string, T> BuildLookup<T>(List<T> list, System.Func<T, string> idOf) where T : ScriptableObject
        {
            var d = new Dictionary<string, T>(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (item == null) continue;
                string id = idOf(item);
                if (string.IsNullOrEmpty(id)) { Debug.LogWarning($"Registry: {item.name} has empty id"); continue; }
                if (d.ContainsKey(id))         { Debug.LogWarning($"Registry: duplicate id '{id}' on {item.name}"); continue; }
                d[id] = item;
            }
            return d;
        }
    }
}
