using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Player
{
    /// <summary>
    /// LF2 two-player input scheme. P1: WASD move, J/K/L actions. P2: Arrows move, Numpad 1/0/2 actions.
    /// Each instance holds its own InputAction bindings — no cross-talk between players.
    /// Reads input once per FixedTick and exposes a snapshot struct.
    /// </summary>
    public sealed class Lf2InputScheme : System.IDisposable
    {
        private InputAction moveAction;
        private InputAction attackAction;
        private InputAction jumpAction;
        private InputAction defendAction;

        public Lf2InputScheme(int playerIndex = 0)
        {
            if (playerIndex == 1)
            {
                moveAction = new InputAction("P2Move", InputActionType.Value, expectedControlType: "Vector2");
                moveAction.AddCompositeBinding("2DVector")
                    .With("Up", "<Keyboard>/upArrow")
                    .With("Down", "<Keyboard>/downArrow")
                    .With("Left", "<Keyboard>/leftArrow")
                    .With("Right", "<Keyboard>/rightArrow");
                moveAction.AddBinding("<Gamepad>/leftStick");
                moveAction.Enable();

                attackAction = new InputAction("P2Attack", InputActionType.Button);
                attackAction.AddBinding("<Keyboard>/numpad1");
                attackAction.AddBinding("<Gamepad>/buttonWest");
                attackAction.Enable();

                jumpAction = new InputAction("P2Jump", InputActionType.Button);
                jumpAction.AddBinding("<Keyboard>/numpad0");
                jumpAction.AddBinding("<Gamepad>/buttonSouth");
                jumpAction.Enable();

                defendAction = new InputAction("P2Defend", InputActionType.Button);
                defendAction.AddBinding("<Keyboard>/numpad2");
                defendAction.AddBinding("<Gamepad>/buttonEast");
                defendAction.Enable();
            }
            else
            {
                moveAction = new InputAction("Move", InputActionType.Value, expectedControlType: "Vector2");
                moveAction.AddCompositeBinding("2DVector")
                    .With("Up", "<Keyboard>/w")
                    .With("Down", "<Keyboard>/s")
                    .With("Left", "<Keyboard>/a")
                    .With("Right", "<Keyboard>/d");
                moveAction.AddBinding("<Gamepad>/leftStick");
                moveAction.Enable();

                attackAction = new InputAction("Attack", InputActionType.Button);
                attackAction.AddBinding("<Keyboard>/j");
                attackAction.AddBinding("<Gamepad>/buttonWest");
                attackAction.Enable();

                jumpAction = new InputAction("Jump", InputActionType.Button);
                jumpAction.AddBinding("<Keyboard>/k");
                jumpAction.AddBinding("<Gamepad>/buttonSouth");
                jumpAction.Enable();

                defendAction = new InputAction("Defend", InputActionType.Button);
                defendAction.AddBinding("<Keyboard>/l");
                defendAction.AddBinding("<Gamepad>/buttonEast");
                defendAction.Enable();
            }
        }

        public struct Lf2InputState
        {
            public Vector2 MoveDir;
            public bool AttackPressed;
            public bool AttackHeld;
            public bool JumpPressed;
            public bool JumpHeld;
            public bool DefendHeld;
            public bool MovePressed;
        }

        public Lf2InputState Read()
        {
            var raw = moveAction != null ? moveAction.ReadValue<Vector2>() : Vector2.zero;
            if (raw.sqrMagnitude > 1f) raw.Normalize();

            return new Lf2InputState
            {
                MoveDir = raw,
                AttackPressed = attackAction != null && attackAction.WasPressedThisFrame(),
                AttackHeld = attackAction != null && attackAction.IsPressed(),
                JumpPressed = jumpAction != null && jumpAction.WasPressedThisFrame(),
                JumpHeld = jumpAction != null && jumpAction.IsPressed(),
                DefendHeld = defendAction != null && defendAction.IsPressed(),
                MovePressed = raw.sqrMagnitude > 0.0001f
            };
        }

        public void Dispose()
        {
            moveAction?.Disable(); moveAction?.Dispose();
            attackAction?.Disable(); attackAction?.Dispose();
            jumpAction?.Disable(); jumpAction?.Dispose();
            defendAction?.Disable(); defendAction?.Dispose();
        }
    }
}
