using EricNee.AutoStartDesktop.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EricNee.AutoStartDesktop
{
    /// <summary>
    /// Interaction logic for AppIconUserControl.xaml
    /// </summary>
    public partial class AppIconUserControl : UserControl
    {
        public AppIconUserControl(AppEntity process)
        {
            Process = process;
            InitializeComponent();
        }

        public AppEntity Process { get; }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            AppName.Content = Process.ProcessName;
        }



        private void AppName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var process = ProcessHelper.OpenIfExisted(Process.Cmd);
        }
    }
}
