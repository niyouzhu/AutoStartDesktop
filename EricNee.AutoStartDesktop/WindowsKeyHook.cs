using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EricNee.AutoStartDesktop
{
    public class WindowsKeyHook : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYUP = 0x0105;
        private const int VK_MENU = 0x12;
        internal struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            int scanCode;
            public int flags;
            int time;
            int dwExtraInfo;
        }
        internal delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

        internal IntPtr Hook(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
        {

            if (lParam.vkCode == 91 || lParam.vkCode == 92)
                return (IntPtr)1;
            else
                return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, ref lParam);
        }

        private HookHandlerDelegate _hookHandler;
        private IntPtr _hookId;
        public void Intercept()
        {
            if (_hookId == IntPtr.Zero)
            {
                _hookHandler = new HookHandlerDelegate(Hook);
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    _hookId = NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, _hookHandler,
                        NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
                }
            }

        }

        public void Release()
        {
            if (_hookId != IntPtr.Zero)
            {
                NativeMethods.UnhookWindowsHookEx(_hookId);
                Marshal.Release(_hookId);
                _hookId = IntPtr.Zero;
            }
            _hookHandler = null;
        }
        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Release();
            }
        }

        #region Native methods

        [ComVisibleAttribute(false),
         System.Security.SuppressUnmanagedCodeSecurity()]
        internal class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(int idHook,
                HookHandlerDelegate lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
                IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
            public static extern short GetKeyState(int keyCode);

        }


        #endregion
    }
}
