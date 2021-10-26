using System;
using System.Runtime.InteropServices;

namespace ArtilleryCalculator
{
    class TransparencyHotkeyListener : BaseWindowsHookListener
    {
        public delegate void TransparencyHotkeyCallback();
        public TransparencyHotkeyCallback Callback { get; set; }

        enum KeyType
        {
            KeyUp,
            KeyDown
        }

        const int VK_SHIFT = 0x10;
        const int VK_LSHIFT = 0xA0;
        const int VK_RSHIFT = 0xA1;
        const int VK_CONTROL = 0x11;
        const int VK_LCONTROL = 0xA2;
        const int VK_RCONTROL = 0xA3;
        const int VK_MENU = 0x12;
        const int VK_LMENU = 0xA4;
        const int VK_RMENU = 0xA5;
        const int VK_K = 0x4B;

        public TransparencyHotkeyListener() : base(Native.WH_KEYBOARD_LL) { }

        protected override void HandleHook(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)Native.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ProcessKey(vkCode, KeyType.KeyDown);
            }
            else if (code >= 0 && wParam == (IntPtr)Native.WM_KEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ProcessKey(vkCode, KeyType.KeyUp);
            }
        }

        private bool ShiftState { get; set; }
        private bool CtrlState { get; set; }
        private bool AltState { get; set; }

        private void ProcessKey(int vkCode, KeyType type)
        {
            switch (vkCode)
            {
                case VK_SHIFT:
                case VK_LSHIFT:
                case VK_RSHIFT:
                    ShiftState = type == KeyType.KeyDown;
                    break;
                case VK_CONTROL:
                case VK_LCONTROL:
                case VK_RCONTROL:
                    CtrlState = type == KeyType.KeyDown;
                    break;
                case VK_MENU:
                case VK_LMENU:
                case VK_RMENU:
                    AltState = type == KeyType.KeyDown;
                    break;
                case VK_K:
                    if (type == KeyType.KeyDown && ShiftState && CtrlState)
                        Callback();
                    break;
            }
        }
    }
}
