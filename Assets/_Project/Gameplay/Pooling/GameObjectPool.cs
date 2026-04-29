using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Pooling
{
    public sealed class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int prewarmCount = 16;

        private readonly Stack<GameObject> _stack = new Stack<GameObject>(128);

        public int CountInactive => _stack.Count;

        private void Awake()
        {
            if (prefab == null)
                return;

            for (var i = 0; i < prewarmCount; i++)
                _stack.Push(CreateInstance());
        }

        public void Configure(GameObject prefabRef, int prewarm)
        {
            prefab = prefabRef;
            prewarmCount = Mathf.Max(0, prewarm);
        }

        public GameObject Rent(Vector3 position)
        {
            var go = _stack.Count > 0 ? _stack.Pop() : CreateInstance();
            go.transform.SetPositionAndRotation(position, Quaternion.identity);
            go.SetActive(true);
            return go;
        }

        public void Return(GameObject go)
        {
            if (go == null)
                return;

            go.SetActive(false);
            go.transform.SetParent(transform, false);
            _stack.Push(go);
        }

        private GameObject CreateInstance()
        {
            var go = Instantiate(prefab, transform);
            go.SetActive(false);
            return go;
        }
    }
}
