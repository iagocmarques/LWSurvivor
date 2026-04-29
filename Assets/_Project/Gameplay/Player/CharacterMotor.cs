using UnityEngine;

namespace Project.Gameplay.Player
{
    /// <summary>
    /// Handles gravity, vertical velocity, and grounded state for airborne characters.
    /// Extracted from PlayerHsmController to share across characters and airborne states.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class CharacterMotor : MonoBehaviour
    {
        private float _gravity;
        private float _groundY;
        private float _verticalVelocity;
        private bool _grounded = true;

        /// <summary>True when the character is at or below ground level.</summary>
        public bool IsGrounded => _grounded;

        /// <summary>Current vertical velocity (negative = falling).</summary>
        public float VerticalVelocity
        {
            get => _verticalVelocity;
            set => _verticalVelocity = value;
        }

        /// <summary>
        /// Configure motor parameters. Call from PlayerHsmController.Configure().
        /// </summary>
        /// <param name="gravity">Gravity per tick at 60 Hz.</param>
        /// <param name="groundY">Y coordinate considered ground level (default 0).</param>
        public void Configure(float gravity, float groundY = 0f)
        {
            _gravity = gravity;
            _groundY = groundY;
        }

        /// <summary>
        /// Tick the motor: apply gravity, integrate vertical velocity, clamp to ground.
        /// Call once per FixedTick (60 Hz) for airborne characters.
        /// </summary>
        /// <param name="dt">Fixed delta time.</param>
        public void Tick(float dt)
        {
            var pos = transform.position;

            // Grounded check
            if (pos.y <= _groundY)
            {
                if (!_grounded)
                {
                    // Landing
                    _verticalVelocity = 0f;
                    pos.y = _groundY;
                    transform.position = pos;
                }
                _grounded = true;
                return;
            }

            _grounded = false;

            // Gravity
            _verticalVelocity -= _gravity * (dt * 60f);

            // Integrate vertical position
            pos.y += _verticalVelocity * dt;
            transform.position = pos;
        }

        /// <summary>
        /// Set the character airborne with an initial vertical velocity.
        /// Typically called from EnterReactiveState(HurtAir).
        /// </summary>
        public void Launch(float verticalVelocity)
        {
            _verticalVelocity = verticalVelocity;
            _grounded = false;
        }
    }
}
