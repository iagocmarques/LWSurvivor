using UnityEngine;

namespace Project.Core.Input
{
    /// <summary>
    /// Abstraction layer for input that works with both legacy Input Manager
    /// and the new Input System package. Use this instead of UnityEngine.Input directly.
    /// </summary>
    public static class Lf2Input
    {
#if ENABLE_INPUT_SYSTEM
        public static bool GetKeyDown(KeyCode key)
        {
            var kb = UnityEngine.InputSystem.Keyboard.current;
            if (kb == null) return false;

            return key switch
            {
                KeyCode.A => kb.aKey.wasPressedThisFrame,
                KeyCode.B => kb.bKey.wasPressedThisFrame,
                KeyCode.C => kb.cKey.wasPressedThisFrame,
                KeyCode.D => kb.dKey.wasPressedThisFrame,
                KeyCode.E => kb.eKey.wasPressedThisFrame,
                KeyCode.F => kb.fKey.wasPressedThisFrame,
                KeyCode.G => kb.gKey.wasPressedThisFrame,
                KeyCode.H => kb.hKey.wasPressedThisFrame,
                KeyCode.I => kb.iKey.wasPressedThisFrame,
                KeyCode.J => kb.jKey.wasPressedThisFrame,
                KeyCode.K => kb.kKey.wasPressedThisFrame,
                KeyCode.L => kb.lKey.wasPressedThisFrame,
                KeyCode.M => kb.mKey.wasPressedThisFrame,
                KeyCode.N => kb.nKey.wasPressedThisFrame,
                KeyCode.O => kb.oKey.wasPressedThisFrame,
                KeyCode.P => kb.pKey.wasPressedThisFrame,
                KeyCode.Q => kb.qKey.wasPressedThisFrame,
                KeyCode.R => kb.rKey.wasPressedThisFrame,
                KeyCode.S => kb.sKey.wasPressedThisFrame,
                KeyCode.T => kb.tKey.wasPressedThisFrame,
                KeyCode.U => kb.uKey.wasPressedThisFrame,
                KeyCode.V => kb.vKey.wasPressedThisFrame,
                KeyCode.W => kb.wKey.wasPressedThisFrame,
                KeyCode.X => kb.xKey.wasPressedThisFrame,
                KeyCode.Y => kb.yKey.wasPressedThisFrame,
                KeyCode.Z => kb.zKey.wasPressedThisFrame,
                KeyCode.Return => kb.enterKey.wasPressedThisFrame,
                KeyCode.Escape => kb.escapeKey.wasPressedThisFrame,
                KeyCode.Space => kb.spaceKey.wasPressedThisFrame,
                KeyCode.UpArrow => kb.upArrowKey.wasPressedThisFrame,
                KeyCode.DownArrow => kb.downArrowKey.wasPressedThisFrame,
                KeyCode.LeftArrow => kb.leftArrowKey.wasPressedThisFrame,
                KeyCode.RightArrow => kb.rightArrowKey.wasPressedThisFrame,
                KeyCode.F1 => kb.f1Key.wasPressedThisFrame,
                KeyCode.F2 => kb.f2Key.wasPressedThisFrame,
                KeyCode.F3 => kb.f3Key.wasPressedThisFrame,
                KeyCode.F4 => kb.f4Key.wasPressedThisFrame,
                KeyCode.F5 => kb.f5Key.wasPressedThisFrame,
                KeyCode.F6 => kb.f6Key.wasPressedThisFrame,
                KeyCode.F7 => kb.f7Key.wasPressedThisFrame,
                KeyCode.F8 => kb.f8Key.wasPressedThisFrame,
                KeyCode.F9 => kb.f9Key.wasPressedThisFrame,
                KeyCode.F10 => kb.f10Key.wasPressedThisFrame,
                KeyCode.F11 => kb.f11Key.wasPressedThisFrame,
                KeyCode.F12 => kb.f12Key.wasPressedThisFrame,
                KeyCode.Semicolon => kb.semicolonKey.wasPressedThisFrame,
                _ => false,
            };
        }
#else
        public static bool GetKeyDown(KeyCode key)
        {
            return UnityEngine.Input.GetKeyDown(key);
        }
#endif
    }
}
