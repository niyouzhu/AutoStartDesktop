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
    /// Interaction logic for DeleteApp.xaml
    /// </summary>
    public partial class DeleteApp : Page
    {

        public DeleteApp(Business business)
        {
            InitializeComponent();
            Business = business;
            DataGridApps.DataContext = AppView.ConvertTo(Business.GetApps());
        }

        public Business Business { get; }

        private void ButtonDeletion_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (IEnumerable<AppView>)DataGridApps.DataContext;
            Business.Remove(dataContext.Where(it => it.Checked == true));
            DataGridApps.DataContext = AppView.ConvertTo(Business.GetApps());
            MessageBox.Show("Successful!");
        }
    }
}
