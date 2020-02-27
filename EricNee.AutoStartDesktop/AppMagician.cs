using GlobalHotKey;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EricNee.AutoStartDesktop
{
    public class AppMagician : IDisposable
    {

        //public KeyboardHook KeyboardHook { get; } = new KeyboardHook(KeyboardHook.Parameters.AllowAltTab);
        public WindowsKeyHook WindowsKeyHook { get; } = new WindowsKeyHook();
        public Taskbar Taskbar { get; } = new Taskbar();
        private bool _disposed;

        public Library.AppSettings AppSettings { get; set; }
        public AppMagician(Library.AppSettings appSettings)
        {
            AppSettings = appSettings;
        }
        public void Magic()
        {
            if (AppSettings.DisabledWindowsKey)
                WindowsKeyHook.Intercept();
            else
                WindowsKeyHook.Release();
            if (AppSettings.HiddenTaskbar)
                Taskbar.Hide();
            else
                Taskbar.Show();
        }
        public void Dispose()
        {
            if (!_disposed)
            {
                //KeyboardHook.Dispose();
                WindowsKeyHook.Dispose();
                Taskbar.Dispose();
                //GlobalEvent.Dispose();
                //Manager.Dispose();
                _disposed = true;
            }
        }
    }
}
