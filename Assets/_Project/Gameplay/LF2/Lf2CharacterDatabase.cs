using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    [CreateAssetMenu(fileName = "LF2CharacterDatabase", menuName = "_Project/LF2/Character Database")]
    public sealed class Lf2CharacterDatabase : ScriptableObject
    {
        [System.Serializable]
        public struct CharacterEntry
        {
            public int id;
            public string characterName;
            public byte[] datBytes;
        }

        public List<CharacterEntry> characters = new List<CharacterEntry>();

        private Dictionary<int, Lf2CharacterData> _cache;
        private Dictionary<int, byte[]> _byteLookup;

        private void EnsureLookups()
        {
            if (_byteLookup != null)
                return;

            _byteLookup = new Dictionary<int, byte[]>();
            _cache = new Dictionary<int, Lf2CharacterData>();

            for (int i = 0; i < characters.Count; i++)
                _byteLookup[characters[i].id] = characters[i].datBytes;
        }

        public Lf2CharacterData GetCharacter(int id)
        {
            EnsureLookups();

            if (_cache.TryGetValue(id, out var cached))
                return cached;

            if (!_byteLookup.TryGetValue(id, out var bytes) || bytes == null || bytes.Length == 0)
            {
                Debug.LogWarning($"[Lf2CharacterDatabase] No data for character id {id}.");
                return null;
            }

            var data = Lf2DatRuntimeLoader.LoadFromBytes(bytes);
            _cache[id] = data;
            return data;
        }

        public bool HasCharacter(int id)
        {
            EnsureLookups();
            return _byteLookup.ContainsKey(id);
        }

        public byte[] GetDatBytes(int id)
        {
            EnsureLookups();
            return _byteLookup.TryGetValue(id, out var bytes) ? bytes : null;
        }

        public int Count => characters.Count;

        public IReadOnlyList<int> GetAllIds()
        {
            EnsureLookups();
            var ids = new List<int>(_byteLookup.Keys);
            return ids;
        }

        public void ClearCache()
        {
            _cache = null;
            _byteLookup = null;
        }
    }
}
