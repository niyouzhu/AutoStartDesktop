using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EricNee.AutoStartDesktop.Library
{

    public class ClockEventArgs : EventArgs
    {
        public DateTime Now { get; internal set; }
    }
    public class Clock : IDisposable
    {

        public string CurrentTime { get; }
        public Task<string> ToFormatString()
        {
            return Task.FromResult(DateTime.Now.ToString("MM-dd HH:mm:ss"));
        }

        public event Action<ClockEventArgs> Interval;

        private Timer Timer { get; set; }

        public Task Start()
        {
            Timer = new Timer(obj =>
            {
                obj = new ClockEventArgs() { Now = DateTime.Now };
                Interval?.Invoke((ClockEventArgs)obj);
            }, null, 0, 1000);
            return Task.CompletedTask;
        }
        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed && Timer != null)
            {
                Timer.Dispose();
                Timer = null;
                _disposed = true;
            }
        }
    }
}
