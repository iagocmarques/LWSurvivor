using System.Collections.Generic;
using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Gameplay.Pooling
{
    public sealed class Lf2ProjectilePool : MonoBehaviour
    {
        private static Lf2ProjectilePool _instance;
        public static Lf2ProjectilePool Instance => _instance;

        [SerializeField] private int poolSize = 32;
        [SerializeField] private GameObject projectilePrefab;

        private readonly Queue<Lf2Projectile> _pool = new Queue<Lf2Projectile>(64);
        private readonly List<Lf2Projectile> _active = new List<Lf2Projectile>(32);
        private Transform _tr;

        public int ActiveCount => _active.Count;
        public int AvailableCount => _pool.Count;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            _tr = transform;
            Prewarm();
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        private void Prewarm()
        {
            for (int i = 0; i < poolSize; i++)
            {
                var projectile = CreateInstance();
                projectile.gameObject.SetActive(false);
                _pool.Enqueue(projectile);
            }
        }

        public Lf2Projectile Rent()
        {
            Lf2Projectile projectile;

            if (_pool.Count > 0)
            {
                projectile = _pool.Dequeue();
            }
            else
            {
                projectile = CreateInstance();
            }

            _active.Add(projectile);
            return projectile;
        }

        public void Return(Lf2Projectile projectile)
        {
            if (projectile == null) return;

            projectile.gameObject.SetActive(false);
            projectile.transform.SetParent(_tr, false);
            _active.Remove(projectile);
            _pool.Enqueue(projectile);
        }

        public void ReturnAll()
        {
            for (int i = _active.Count - 1; i >= 0; i--)
            {
                var p = _active[i];
                if (p != null)
                {
                    p.gameObject.SetActive(false);
                    p.transform.SetParent(_tr, false);
                    _pool.Enqueue(p);
                }
            }

            _active.Clear();
        }

        public void TickAll()
        {
            for (int i = _active.Count - 1; i >= 0; i--)
            {
                var p = _active[i];
                if (p == null)
                {
                    _active.RemoveAt(i);
                    continue;
                }

                if (p.IsActive)
                {
                    p.Tick();
                }
                else
                {
                    Return(p);
                }
            }
        }

        private Lf2Projectile CreateInstance()
        {
            GameObject go;

            if (projectilePrefab != null)
            {
                go = Instantiate(projectilePrefab, _tr);
            }
            else
            {
                go = new GameObject("PooledProjectile");
                go.transform.SetParent(_tr, false);
                go.AddComponent<SpriteRenderer>();
            }

            go.SetActive(false);

            var projectile = go.GetComponent<Lf2Projectile>();
            if (projectile == null)
                projectile = go.AddComponent<Lf2Projectile>();

            return projectile;
        }
    }
}
