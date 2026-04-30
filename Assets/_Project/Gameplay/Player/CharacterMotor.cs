using Project.Core.Tick;
using Project.Data;
using Project.Gameplay.Rendering;
using UnityEngine;

namespace Project.Gameplay.Player
{
    /// <summary>
    /// Full 2.5D character motor. Owns all position changes.
    /// X = horizontal (walk/run/dash/knockback), Y = depth (2.5D planar), Vertical = jump/fall height.
    /// Tick-based at 60Hz via ITickSystem. No Rigidbody2D dependency.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CharacterMotor : MonoBehaviour, ITickable
    {
        [Header("Arena")]
        [SerializeField] private ArenaBounds arenaBounds;

        private CharacterMovementConfig _config;

        private float _depth;
        private float _verticalVelocity;
        private Vector2 _planarVelocity;

        private bool _isGrounded = true;
        private float _groundY;
        private bool _configured;

        private float _frameDvx;
        private float _frameDvy;
        private bool _hasFrameVelocity;

        public bool IsGrounded => _isGrounded;
        public bool IsAirborne => !_isGrounded;
        public float VerticalVelocity => _verticalVelocity;
        public float Depth => _depth;
        public Vector2 PlanarVelocity => _planarVelocity;
        public Vector3 Position => transform.position;

        public void Configure(CharacterMovementConfig config, float groundY = 0f)
        {
            _config = config;
            _groundY = groundY;
            _depth = 0f;
            _isGrounded = true;
            _configured = true;
            enabled = true;
        }

        public void Configure(float gravity, float groundY = 0f)
        {
            _config = new CharacterMovementConfig { gravity = gravity };
            _groundY = groundY;
            var pos = transform.position;
            _depth = 0f;
            _isGrounded = pos.y <= _groundY + 0.01f;
            _configured = true;
            enabled = true;
        }

        private void OnEnable() => FixedTickSystem.Register(this);
        private void OnDisable() => FixedTickSystem.Unregister(this);

        public void Tick(in TickContext context)
        {
            if (!_configured) return;

            var dt = context.FixedDelta;
            var pos = transform.position;

            if (!_isGrounded)
            {
                _verticalVelocity += _config.gravity;
                pos.y += _verticalVelocity * dt;

                if (pos.y <= _groundY)
                {
                    pos.y = _groundY;
                    _verticalVelocity = 0f;
                    _isGrounded = true;
                }
            }

            pos.x += _planarVelocity.x * dt;

            if (_isGrounded)
            {
                _depth += _planarVelocity.y * dt;
                pos.y = _groundY + _depth;
            }

            if (_hasFrameVelocity)
            {
                pos.x += _frameDvx * dt;
                _depth += _frameDvy * dt;
                if (_isGrounded)
                    pos.y = _groundY + _depth;
                _frameDvx = 0f;
                _frameDvy = 0f;
                _hasFrameVelocity = false;
            }

            if (arenaBounds != null)
                pos = arenaBounds.ClampPosition(pos);

            transform.position = pos;
        }

        /// <summary>Manual tick overload for backward compatibility. Delegates to ITickable.Tick.</summary>
        public void Tick(float dt)
        {
            Tick(new TickContext(0, dt, 0, 0, 0, 1));
        }

        public void ApplyMovementInput(Vector2 moveInput, float speedMultiplier = 1f)
        {
            _planarVelocity = moveInput * _config.walkSpeed * speedMultiplier;
        }

        public void SetPlanarVelocity(Vector2 velocity)
        {
            _planarVelocity = velocity;
        }

        public void SetHorizontalVelocity(float vx)
        {
            _planarVelocity.x = vx;
        }

        public void SetDepthVelocity(float vy)
        {
            _planarVelocity.y = vy;
        }

        public void StopMovement()
        {
            _planarVelocity = Vector2.zero;
        }

        public void Launch(float verticalSpeed)
        {
            _verticalVelocity = verticalSpeed;
            _isGrounded = false;
        }

        public void ApplyFrameVelocity(float dvx, float dvy)
        {
            _frameDvx += dvx;
            _frameDvy += dvy;
            _hasFrameVelocity = true;
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
            _depth = 0f;
        }

        public void SetGroundY(float y)
        {
            _groundY = y;
        }

        public void SetArenaBounds(ArenaBounds bounds)
        {
            arenaBounds = bounds;
        }
    }
}
