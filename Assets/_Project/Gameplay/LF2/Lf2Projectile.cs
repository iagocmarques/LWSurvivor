using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2Projectile : MonoBehaviour
    {
        private Lf2StateMachine _sm;
        private Lf2ItrProcessor _itrProcessor;
        private Transform _tr;
        private bool _active;
        private int _lifetimeTicks;
        private int _maxLifetimeTicks;
        private bool _facingRight;

        private static readonly Vector2 ArenaMin = new(-20f, -10f);
        private static readonly Vector2 ArenaMax = new(20f, 10f);

        public bool IsActive => _active;
        public Lf2StateMachine Sm => _sm;

        public void OnProjectileHit()
        {
            if (_active)
                Deactivate();
        }

        private void Awake()
        {
            _tr = transform;
            _sm = new Lf2StateMachine();
            _itrProcessor = new Lf2ItrProcessor();
            _sm.SetItrProcessor(_itrProcessor);
            _sm.SetAlwaysProcessItr(true);
            _sm.SetOwnerTransform(_tr);
            _sm.Gravity = 0f;
        }

        public void Activate(Lf2CharacterData data, Vector2 position, Vector2 velocity,
            bool facingRight, LayerMask hurtMask, LayerMask projectileMask = default, int maxLifetimeTicks = 300)
        {
            _tr.position = position;
            _facingRight = facingRight;
            _lifetimeTicks = 0;
            _maxLifetimeTicks = maxLifetimeTicks;
            _active = true;

            _sm.Initialize(data, position.y);
            _sm.SetFacingRight(facingRight);
            _sm.SetFrame(0);
            _sm.SetVelocityDirect(velocity);

            _itrProcessor.Initialize(_sm, _tr, hurtMask);
            _itrProcessor.SetOnHit(OnProjectileHit);
            if (projectileMask.value != 0)
                _itrProcessor.SetProjectileMask(projectileMask);

            UpdateFacingScale();
            gameObject.SetActive(true);
        }

        public void Tick()
        {
            if (!_active) return;

            _lifetimeTicks++;
            if (_lifetimeTicks >= _maxLifetimeTicks)
            {
                Deactivate();
                return;
            }

            if (_sm.CurrentFrame == null)
            {
                Deactivate();
                return;
            }

            _sm.Tick();

            var pos = _tr.position;
            pos.x += _sm.Velocity.x;
            pos.y += _sm.Velocity.y;
            _tr.position = pos;

            if (pos.x < ArenaMin.x || pos.x > ArenaMax.x ||
                pos.y < ArenaMin.y || pos.y > ArenaMax.y)
            {
                Deactivate();
                return;
            }

            if (_sm.IsFinished)
                Deactivate();
        }

        private void UpdateFacingScale()
        {
            var scale = _tr.localScale;
            scale.x = Mathf.Abs(scale.x) * (_facingRight ? 1f : -1f);
            _tr.localScale = scale;
        }

        private void Deactivate()
        {
            _active = false;
            gameObject.SetActive(false);
        }
    }
}
