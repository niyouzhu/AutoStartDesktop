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
            LabelName.Content = Process.ProcessName;
            ImageIcon.Source = Process.Icon?.ToBitmapImage();
        }



        private void LabelName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClickIcon();
        }

        private void ClickIcon()
        {
            bool existing;
            var process = App.ProcessSet.Open(Process.Cmd, Process.Args, out existing);
            if (existing)
            {
                MessageBox.Show("该应用已经在运行，按住键盘 Alt + Tab 键切换并找到它");
            }
        }

        private Brush BackGroundBrush = new SolidColorBrush(Color.FromArgb(System.Drawing.Color.Silver.A, System.Drawing.Color.Silver.R, System.Drawing.Color.Silver.G, System.Drawing.Color.Silver.B));

        private void GridAppIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            GridAppIcon.Background = BackGroundBrush;
        }

        private void GridAppIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            GridAppIcon.Background = null;
        }

        private void ImageIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClickIcon();
        }
    }
}
