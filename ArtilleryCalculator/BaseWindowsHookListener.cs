using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace ArtilleryCalculator
{
    abstract class BaseWindowsHookListener : IDisposable
    {
        private SafeHookHandle HookHandle { get; set; }
        private Native.WindowsHookCallback HookCallback { get; set; }

        [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
        protected BaseWindowsHookListener(int hookId)
        {
            HookCallback = new Native.WindowsHookCallback(ProcessHook);
            HookHandle = Native.SetWindowsHookEx(hookId, HookCallback, IntPtr.Zero, 0);

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
            HandleHook(code, wParam, lParam);
            return Native.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        protected abstract void HandleHook(int code, IntPtr wParam, IntPtr lParam);

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
        protected class SafeHookHandle : SafeHandle
        {
            private SafeHookHandle() : base(IntPtr.Zero, true) { }

            public override bool IsInvalid => handle == IntPtr.Zero;

            protected override bool ReleaseHandle()
            {
                return Native.UnhookWindowsHookEx(handle);
            }
        }

        [SuppressUnmanagedCodeSecurity()]
        protected static class Native
        {
            public const int WH_KEYBOARD_LL = 13;
            public const int WM_KEYDOWN = 0x100;

            public const int WH_MOUSE_LL = 14;
            public const int WM_LBUTTONDOWN = 0x201;

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
