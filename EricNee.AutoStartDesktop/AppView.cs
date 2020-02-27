using EricNee.AutoStartDesktop.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EricNee.AutoStartDesktop
{
    public class AppView : AppEntity
    {
        public bool Checked { get; set; }

        public AppView() { }
        public AppView(AppEntity app)
        {
            Checked = false; Cmd = app.Cmd; Id = app.Id; Icon = app.Icon; LogonName = app.LogonName; ProcessName = app.ProcessName;
        }

        public static IEnumerable<AppView> ConvertTo(IEnumerable<AppEntity> apps)
        {
            var rt = new System.Collections.ObjectModel.ObservableCollection<AppView>();
            //var rt = new List<AppView>();
            foreach (var item in apps)
            {
                rt.Add(new AppView(item));
            }
            return rt;
        }
    }
}
