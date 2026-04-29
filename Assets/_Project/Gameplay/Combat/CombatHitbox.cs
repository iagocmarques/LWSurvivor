using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Hitbox física 2D (trigger) com detecção manual por tick para controle total no tick fixo.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CombatHitbox : MonoBehaviour
    {
        private static readonly Collider2D[] OverlapScratch = new Collider2D[32];

        private BoxCollider2D _box;
        private GameObject _owner;
        private LayerMask _hurtMask;
        private int _damage;
        private Vector2 _knockback;
        private CombatAttackId _attackId;
        private int _hitStopTicks;
        private float _screenShakeAmplitude;
        private bool _isGrab;
        private readonly HashSet<Collider2D> _hitThisActivation = new HashSet<Collider2D>();
        private bool _armed;

        private void Awake()
        {
            _box = GetComponent<BoxCollider2D>();
            if (_box == null)
                _box = gameObject.AddComponent<BoxCollider2D>();

            _box.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Hitbox");
            if (gameObject.layer < 0)
                gameObject.layer = 0;
        }

        public void ConfigureForPool()
        {
            if (_box == null)
                Awake();
        }

        public void Arm(
            GameObject owner,
            LayerMask hurtMask,
            int damage,
            Vector2 knockback,
            CombatAttackId attackId,
            int hitStopTicks,
            float screenShakeAmplitude,
            bool isGrab)
        {
            _owner = owner;
            _hurtMask = hurtMask;
            _damage = damage;
            _knockback = knockback;
            _attackId = attackId;
            _hitStopTicks = hitStopTicks;
            _screenShakeAmplitude = screenShakeAmplitude;
            _isGrab = isGrab;
            _hitThisActivation.Clear();
            _armed = true;
            gameObject.SetActive(true);
        }

        public void Disarm()
        {
            _armed = false;
            _owner = null;
            _hitThisActivation.Clear();
            gameObject.SetActive(false);
        }

        public void SetWorldPose(Vector2 center, Vector2 halfExtents, float angleDeg)
        {
            transform.SetPositionAndRotation(new Vector3(center.x, center.y, 0f), Quaternion.Euler(0f, 0f, angleDeg));
            if (_box == null)
                Awake();

            _box.offset = Vector2.zero;
            _box.size = halfExtents * 2f;
        }

        public void TickOverlap()
        {
            if (!_armed || _owner == null || _box == null)
                return;

            var filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = _hurtMask,
                useTriggers = true
            };

            var count = Physics2D.OverlapBox(
                _box.bounds.center,
                _box.bounds.size,
                transform.eulerAngles.z,
                filter,
                OverlapScratch);
            for (var i = 0; i < count; i++)
            {
                var col = OverlapScratch[i];
                if (col == null)
                    continue;

                if (col.attachedRigidbody != null && col.attachedRigidbody.gameObject == _owner)
                    continue;

                if (col.gameObject == _owner)
                    continue;

                if (!_hitThisActivation.Add(col))
                    continue;

                if (!col.TryGetComponent<ICombatHurtbox>(out var hurt))
                    continue;

                var info = new CombatHitInfo(
                    _owner,
                    _damage,
                    _knockback,
                    _attackId,
                    _hitStopTicks,
                    _screenShakeAmplitude,
                    _isGrab);
                hurt.ReceiveHit(in info);
            }
        }
    }
}
