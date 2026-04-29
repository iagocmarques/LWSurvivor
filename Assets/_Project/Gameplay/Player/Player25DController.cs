using Project.Core.Tick;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Player
{
    public sealed class Player25DController : MonoBehaviour, ITickable
    {
        [SerializeField] private float moveSpeedUnitsPerSecond = 6f;

        private InputAction moveAction;

        private void Awake()
        {
            moveAction = new InputAction("Move", InputActionType.Value, expectedControlType: "Vector2");

            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");

            moveAction.AddBinding("<Gamepad>/leftStick");

            moveAction.Enable();
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
        }

        private void OnDestroy()
        {
            if (moveAction != null)
            {
                moveAction.Disable();
                moveAction.Dispose();
                moveAction = null;
            }
        }

        public void Tick(in TickContext context)
        {
            var v = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
            if (v.sqrMagnitude > 1f)
                v.Normalize();

            var delta = new Vector3(v.x, v.y, 0f) * (moveSpeedUnitsPerSecond * context.FixedDelta);
            transform.position += delta;
        }
    }
}