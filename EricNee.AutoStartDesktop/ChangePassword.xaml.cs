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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public Business Business { get; }
        public ChangePassword(Business business)
        {
            Business = business;
            InitializeComponent();
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordNew.Password != PasswordNew2.Password)
            {
                MessageBox.Show(Properties.Resources.PasswordNotSame);
                return;
            }
            var admin = Business.UpdateAdmin(new ChangedPasswordBusinessModel() { NewPassword = PasswordNew.Password, OldPassword = PasswordOld.Password });
            MessageBox.Show(Properties.Resources.PasswordIsChanged);
        }
    }
}
