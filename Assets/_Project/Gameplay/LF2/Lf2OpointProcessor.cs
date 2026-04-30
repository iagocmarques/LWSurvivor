using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2OpointProcessor
    {
        private const int MaxProjectiles = 32;

        private readonly Lf2Projectile[] _pool = new Lf2Projectile[MaxProjectiles];
        private Transform _parent;
        private LayerMask _hurtMask;
        private LayerMask _projectileMask;
        private int _projectileLayer = -1;
        private Dictionary<int, Lf2CharacterData> _projectileDataMap;

        public void Initialize(Transform parent, LayerMask hurtMask, Dictionary<int, Lf2CharacterData> projectileDataMap,
            int projectileLayer = -1)
        {
            _parent = parent;
            _hurtMask = hurtMask;
            _projectileLayer = projectileLayer;
            _projectileMask = projectileLayer >= 0 ? (1 << projectileLayer) : 0;
            _projectileDataMap = projectileDataMap;

            for (int i = 0; i < MaxProjectiles; i++)
            {
                var go = new GameObject($"Projectile_{i}");
                go.transform.SetParent(parent, false);
                go.SetActive(false);

                if (_projectileLayer >= 0)
                    go.layer = _projectileLayer;

                var col = go.AddComponent<BoxCollider2D>();
                col.isTrigger = true;
                col.size = new Vector2(0.5f, 0.5f);

                _pool[i] = go.AddComponent<Lf2Projectile>();
            }
        }

        public void ProcessOpoints(Lf2FrameData frame, Vector2 shooterPosition, bool shooterFacingRight)
        {
            if (frame?.Opoints == null || frame.Opoints.Length == 0) return;

            for (int i = 0; i < frame.Opoints.Length; i++)
                SpawnProjectile(frame.Opoints[i], shooterPosition, shooterFacingRight);
        }

        public void TickAll()
        {
            for (int i = 0; i < MaxProjectiles; i++)
            {
                if (_pool[i].IsActive)
                    _pool[i].Tick();
            }
        }

        private void SpawnProjectile(Lf2OpointData opoint, Vector2 shooterPosition, bool shooterFacingRight)
        {
            if (_projectileDataMap == null || !_projectileDataMap.TryGetValue(opoint.Oid, out var data))
                return;

            var projectile = GetInactive();
            if (projectile == null)
            {
                Debug.LogWarning($"[Lf2OpointProcessor] Pool exhausted (max={MaxProjectiles}), cannot spawn oid={opoint.Oid}");
                return;
            }

            bool facingRight = opoint.Facing == 0 ? shooterFacingRight : !shooterFacingRight;

            float scale = Lf2StateMachine.PixelToUnit;
            var offset = new Vector2(
                opoint.Position.x * scale * (shooterFacingRight ? 1f : -1f),
                opoint.Position.y * scale
            );
            var position = shooterPosition + offset;

            var velocity = new Vector2(
                opoint.Velocity.x * (facingRight ? 1f : -1f) * scale,
                opoint.Velocity.y * scale
            );

            projectile.Activate(data, position, velocity, facingRight, _hurtMask, _projectileMask);
        }

        private Lf2Projectile GetInactive()
        {
            for (int i = 0; i < MaxProjectiles; i++)
            {
                if (!_pool[i].IsActive)
                    return _pool[i];
            }
            return null;
        }
    }
}
