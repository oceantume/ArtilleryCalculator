using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace ArtilleryCalculator
{
    class NumpadListener : IDisposable
    {
        private const int VK_NUMPAD_0 = 96;
        private const int VK_NUMPAD_9 = 105;

        private SafeHookHandle HookHandle { get; set; }
        private Native.WindowsHookCallback HookCallback { get; set; }


        public delegate void NumpadCallback(int digit);
        public NumpadCallback Callback { get; set; }


        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        public NumpadListener()
        {
            HookCallback = new Native.WindowsHookCallback(ProcessHook);
            HookHandle = Native.SetWindowsHookEx(Native.WH_KEYBOARD_LL, HookCallback, IntPtr.Zero, 0);

            if (HookHandle.IsInvalid)
            {
                throw new Win32Exception(
                    Marshal.GetLastWin32Error(),
                    "Failed to set the Windows hook that allows to listen for numpad input.");
            }
        }

        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private int ProcessHook(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)Native.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                ProcessKeyDown(vkCode);
            }

            return Native.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private void ProcessKeyDown(int vkCode)
        {
            if (vkCode >= VK_NUMPAD_0 && vkCode <= VK_NUMPAD_9)
            {
                var digit = vkCode - VK_NUMPAD_0;
                Callback?.Invoke(digit);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        protected virtual void Dispose(bool disposing)
        {
            if (HookHandle != null && !HookHandle.IsInvalid)
            {
                HookHandle.Dispose();
            }
        }


        [SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        private class SafeHookHandle : SafeHandle
        {
            private SafeHookHandle() : base(IntPtr.Zero, true) { }

            public override bool IsInvalid => handle == IntPtr.Zero;

            protected override bool ReleaseHandle()
            {
                return Native.UnhookWindowsHookEx(handle);
            }
        }

        [SuppressUnmanagedCodeSecurity()]
        private static class Native
        {
            public const int WH_KEYBOARD_LL = 13;
            public const int WM_KEYDOWN = 0x100;

            public delegate int WindowsHookCallback(int code, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern SafeHookHandle SetWindowsHookEx(int idHook, WindowsHookCallback lpfn, IntPtr hmod, int dwThreadId);

            [DllImport("user32.dll")]
            public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        }
    }
}
