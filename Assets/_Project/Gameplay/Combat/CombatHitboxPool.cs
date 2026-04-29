using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Pool estático de hitboxes (sem prefab obrigatório — cria sob demanda).
    /// </summary>
    public static class CombatHitboxPool
    {
        private const int InitialCapacity = 16;
        private static readonly Stack<CombatHitbox> Pool = new Stack<CombatHitbox>(InitialCapacity);
        private static Transform _root;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetDomain()
        {
            Pool.Clear();
            _root = null;
        }

        private static void EnsureRoot()
        {
            if (_root != null)
                return;

            var go = new GameObject("[CombatHitboxPool]");
            Object.DontDestroyOnLoad(go);
            _root = go.transform;
        }

        public static CombatHitbox Rent()
        {
            EnsureRoot();

            if (Pool.Count > 0)
            {
                var h = Pool.Pop();
                h.gameObject.SetActive(true);
                h.transform.SetParent(_root, false);
                return h;
            }

            var go = new GameObject("PooledHitbox");
            go.transform.SetParent(_root, false);
            var hit = go.AddComponent<CombatHitbox>();
            hit.ConfigureForPool();
            return hit;
        }

        public static void Return(CombatHitbox hit)
        {
            if (hit == null)
                return;

            hit.Disarm();
            hit.transform.SetParent(_root, false);
            Pool.Push(hit);
        }
    }
}
