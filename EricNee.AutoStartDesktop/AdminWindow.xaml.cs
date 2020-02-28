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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private bool Logged { get; set; }
        public Business Business { get; }
        public AdminWindow(Business business)
        {
            Business = business;
            InitializeComponent();
        }


        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            var passwordPage = new PasswordPage(Business);
            passwordPage.Callback += (logined) =>
            {
                if (logined) { Logged = true; LabelLogonInfo.Content = "Logged in"; }
                else { Logged = false; LabelLogonInfo.Content = "Not logged in"; }
            };
            FrameAdmin.Navigate(passwordPage);
        }

        private void ButtonAddApp_Click(object sender, RoutedEventArgs e)
        {
            if (Logged)
            {
                FrameAdmin.Navigate(new AddAppPage(Business));
            }
            else
            {
                MessageBox.Show("Log in First!");
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            if (Logged)
            {
                if (MessageBox.Show("The action will cause the program to exit, OK?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    Application.Current.Shutdown();
            }
            else
                MessageBox.Show("Log in First!");
        }

        private void ButtonUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            if (Logged)
            {
                FrameAdmin.Navigate(new ChangePassword(Business));
            }
            else
            {
                MessageBox.Show("Log in First!");
            }
        }

        private void ButtonDeleteApp_Click(object sender, RoutedEventArgs e)
        {
            if (Logged)
            {
                FrameAdmin.Navigate(new DeleteApp(Business));
            }
            else
            {
                MessageBox.Show("Log in First!");
            }
        }

        private void ButtonAppSettings_Click(object sender, RoutedEventArgs e)
        {
            if (Logged)
            {
                FrameAdmin.Navigate(new AppSettings(Business));
            }
            else
            {
                MessageBox.Show("Log in First!");
            }
        }
    }
}
