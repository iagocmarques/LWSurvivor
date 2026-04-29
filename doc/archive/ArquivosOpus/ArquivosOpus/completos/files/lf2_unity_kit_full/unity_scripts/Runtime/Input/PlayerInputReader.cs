// Assets/Game/Runtime/Input/PlayerInputReader.cs
//
// Reads keyboard input every frame, samples held axes, and feeds press events
// into an InputBuffer for the FSM to consume.
//
// Per levantamento_e_diretrizes.md §4:
//   "Buffer global ~8 frames (configurável)."
//
// MVP NOTE: uses legacy UnityEngine.Input. For production we'll switch to the
// new Input System for rebinding and gamepad support — but legacy avoids
// shipping an InputActionAsset in this kit. The InputBuffer below is reusable
// regardless of how presses are sourced.

using System.Collections.Generic;
using LF2Game.Data;
using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Input
{
    /// <summary>8-frame press buffer. Tracks "did this fire recently?" per action.</summary>
    public sealed class InputBuffer
    {
        public int BufferTicks = 8;

        readonly Dictionary<InputAction, int> _pressedAt  = new();
        readonly Dictionary<InputAction, int> _consumedAt = new();
        int _currentTick;

        public void Tick(int tick) => _currentTick = tick;

        public void NotePress(InputAction action) => _pressedAt[action] = _currentTick;

        /// <summary>True if action was pressed within window AND not yet consumed.</summary>
        public bool Peek(InputAction action)
        {
            if (!_pressedAt.TryGetValue(action, out int pressTick)) return false;
            if (_consumedAt.TryGetValue(action, out int consTick) && consTick >= pressTick) return false;
            return _currentTick - pressTick <= BufferTicks;
        }

        /// <summary>Consume the buffered press if any. Returns true once.</summary>
        public bool Consume(InputAction action)
        {
            if (!Peek(action)) return false;
            _consumedAt[action] = _currentTick;
            return true;
        }

        public void Clear()
        {
            _pressedAt.Clear();
            _consumedAt.Clear();
        }
    }

    public sealed class PlayerInputReader : MonoBehaviour, ITickable
    {
        [Header("Bindings (legacy Input)")]
        public KeyCode KeyAttack  = KeyCode.A;
        public KeyCode KeyDefense = KeyCode.S;
        public KeyCode KeyJump    = KeyCode.D;
        public KeyCode KeyLeft    = KeyCode.LeftArrow;
        public KeyCode KeyRight   = KeyCode.RightArrow;
        public KeyCode KeyUp      = KeyCode.UpArrow;
        public KeyCode KeyDown    = KeyCode.DownArrow;

        /// <summary>Held movement axis. X = horizontal, Y = depth.</summary>
        public Vector2 MoveAxis { get; private set; }
        public bool AttackHeld  { get; private set; }
        public bool DefenseHeld { get; private set; }

        public readonly InputBuffer Buffer = new();

        void OnEnable()  { TickRunner.Instance?.Register(this); }
        void OnDisable() { TickRunner.Instance?.Unregister(this); }

        void Update()
        {
            float x = (UnityEngine.Input.GetKey(KeyRight) ? 1f : 0f)
                     - (UnityEngine.Input.GetKey(KeyLeft)  ? 1f : 0f);
            float y = (UnityEngine.Input.GetKey(KeyUp)    ? 1f : 0f)
                     - (UnityEngine.Input.GetKey(KeyDown)  ? 1f : 0f);
            MoveAxis = new Vector2(x, y);

            AttackHeld  = UnityEngine.Input.GetKey(KeyAttack);
            DefenseHeld = UnityEngine.Input.GetKey(KeyDefense);

            // Note presses immediately on the frame they happen — the buffer's
            // tick counter only matures on the next tick, but the timestamp is
            // the latest known tick which is fine.
            if (UnityEngine.Input.GetKeyDown(KeyAttack))  Buffer.NotePress(InputAction.Attack);
            if (UnityEngine.Input.GetKeyDown(KeyDefense)) Buffer.NotePress(InputAction.Defense);
            if (UnityEngine.Input.GetKeyDown(KeyJump))    Buffer.NotePress(InputAction.Jump);
        }

        public void Tick(int tick) => Buffer.Tick(tick);
    }
}
