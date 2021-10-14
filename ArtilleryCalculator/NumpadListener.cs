using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace ArtilleryCalculator
{
    class NumpadListener : BaseWindowsHookListener
    {
        private const int VK_NUMPAD_0 = 96;
        private const int VK_NUMPAD_9 = 105;

        public delegate void NumpadCallback(int digit);
        public NumpadCallback Callback { get; set; }

        public NumpadListener() : base(Native.WH_KEYBOARD_LL) { }

        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        protected override void HandleHook(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)Native.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ProcessKeyDown(vkCode);
            }
        }

        private void ProcessKeyDown(int vkCode)
        {
            if (vkCode >= VK_NUMPAD_0 && vkCode <= VK_NUMPAD_9)
            {
                var digit = vkCode - VK_NUMPAD_0;
                Callback?.Invoke(digit);
            }
        }
    }
}
