// Assets/Game/Runtime/Pooling/ObjectPool.cs
//
// Generic object pool. Per levantamento_e_diretrizes.md §3.2:
//   "Object pooling agressivo (inimigos, projéteis, pickups, VFX)."
//
// Two variants:
//   - GenericPool<T> for plain C# objects (no GameObject).
//   - PrefabPool for GameObjects with a poolable prefab. Uses SetActive
//     instead of Instantiate/Destroy for hot-loop spawning.
//
// Usage (PrefabPool):
//   var pool = new PrefabPool(enemyPrefab, parent: enemiesRoot, prewarm: 100);
//   var go = pool.Spawn(pos, rot);
//   ...
//   pool.Despawn(go);

using System.Collections.Generic;
using UnityEngine;

namespace LF2Game.Pooling
{
    public sealed class GenericPool<T> where T : class, new()
    {
        readonly Stack<T> _free = new();
        readonly System.Action<T> _onRent;
        readonly System.Action<T> _onReturn;

        public GenericPool(int prewarm = 0,
                           System.Action<T> onRent = null,
                           System.Action<T> onReturn = null)
        {
            _onRent = onRent;
            _onReturn = onReturn;
            for (int i = 0; i < prewarm; i++) _free.Push(new T());
        }

        public T Rent()
        {
            T t = _free.Count > 0 ? _free.Pop() : new T();
            _onRent?.Invoke(t);
            return t;
        }

        public void Return(T t)
        {
            if (t == null) return;
            _onReturn?.Invoke(t);
            _free.Push(t);
        }

        public int FreeCount => _free.Count;
    }

    public sealed class PrefabPool
    {
        readonly GameObject _prefab;
        readonly Transform  _parent;
        readonly Stack<GameObject> _free = new();
        readonly HashSet<GameObject> _live = new();

        public PrefabPool(GameObject prefab, Transform parent, int prewarm = 0)
        {
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < prewarm; i++)
            {
                var go = Object.Instantiate(_prefab, _parent);
                go.SetActive(false);
                _free.Push(go);
            }
        }

        public GameObject Spawn(Vector3 pos, Quaternion rot)
        {
            GameObject go;
            if (_free.Count > 0) go = _free.Pop();
            else                 go = Object.Instantiate(_prefab, _parent);
            go.transform.SetPositionAndRotation(pos, rot);
            go.SetActive(true);
            _live.Add(go);
            return go;
        }

        public void Despawn(GameObject go)
        {
            if (go == null || !_live.Contains(go)) return;
            _live.Remove(go);
            go.SetActive(false);
            go.transform.SetParent(_parent, worldPositionStays: false);
            _free.Push(go);
        }

        public int LiveCount => _live.Count;
        public int FreeCount => _free.Count;

        public void DespawnAll()
        {
            foreach (var go in _live) { go.SetActive(false); _free.Push(go); }
            _live.Clear();
        }
    }
}
