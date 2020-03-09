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
            ((Library.AppSettings)GridAppSettings.DataContext).CultureName = ((KeyValuePair<string, string>)ComboBoxCulture.SelectedItem).Key;
            Business.UpdateSettings((Library.AppSettings)GridAppSettings.DataContext);
            MessageBox.Show("Successful!");
        }

        private Dictionary<string, string> _cultures;
        public Dictionary<string, string> Cultures
        {
            get
            {
                if (_cultures == null)
                {
                    _cultures = new Dictionary<string, string>();
                    _cultures.Add("neutral", "English");
                    _cultures.Add("zh-CN", "简体中文");
                }
                return _cultures;
            }
        }

      
        private void ComboBoxCulture_Loaded(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Library.AppSettings)GridAppSettings.DataContext);
            if (string.IsNullOrWhiteSpace(dataContext.CultureName))
            {
                ComboBoxCulture.SelectedIndex = 0;
            }
            else
                ComboBoxCulture.SelectedItem = Cultures.Single(it => it.Key == ((Library.AppSettings)GridAppSettings.DataContext).CultureName);

        }
    }
}
