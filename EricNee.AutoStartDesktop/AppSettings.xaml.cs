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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class AppSettings : Page
    {
        public Business Business { get; }
        public AppSettings(Business business)
        {
            Business = business;
            InitializeComponent();
            GridAppSettings.DataContext = Business.GetAppSettings();
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            Business.UpdateSettings((Library.AppSettings)GridAppSettings.DataContext);
            MessageBox.Show("Successful!");
        }
    }
}
