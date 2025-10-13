using System;
using System.Runtime.InteropServices;

namespace KeyboardHackerMan.Helper
{
    /// <summary>
    /// Windows API helper for low-level keyboard input simulation
    /// </summary>
    public static class WindowsApiHelper
    {
        // Input type constants
        private const int INPUT_KEYBOARD = 1;

        // Key event flags
        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const uint KEYEVENTF_KEYUP = 0x0002;
        public const uint KEYEVENTF_UNICODE = 0x0004;
        public const uint KEYEVENTF_SCANCODE = 0x0008;

        // Virtual Key Codes
        public enum VirtualKeyCode : ushort
        {
            SPACE = 0x20,
            LEFT = 0x25,
            UP = 0x26,
            RIGHT = 0x27,
            DOWN = 0x28,
            DELETE = 0x2E,
            END = 0x23,
            VK_X = 0x58
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// Sends a key press (down and up) using SendInput API
        /// </summary>
        public static bool SendKeyPress(VirtualKeyCode key, int duration = 100)
        {
            INPUT[] inputs = new INPUT[2];

            // Key down
            inputs[0] = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = (ushort)key,
                        wScan = 0,
                        dwFlags = 0,
                        time = 0,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            };

            // Key up
            inputs[1] = new INPUT
            {
                type = INPUT_KEYBOARD,
                u = new InputUnion
                {
                    ki = new KEYBDINPUT
                    {
                        wVk = (ushort)key,
                        wScan = 0,
                        dwFlags = KEYEVENTF_KEYUP,
                        time = 0,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            };

            // Send key down
            uint result = SendInput(1, new INPUT[] { inputs[0] }, Marshal.SizeOf(typeof(INPUT)));
            
            if (result != 1)
            {
                return false;
            }

            // Wait for duration
            System.Threading.Thread.Sleep(duration);

            // Send key up
            result = SendInput(1, new INPUT[] { inputs[1] }, Marshal.SizeOf(typeof(INPUT)));

            return result == 1;
        }
    }
}
