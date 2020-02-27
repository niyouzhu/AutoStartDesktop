using EricNee.AutoStartDesktop.Library;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EricNee.AutoStartDesktop
{
    /// <summary>
    /// Interaction logic for AddAppPage.xaml
    /// </summary>
    public partial class AddAppPage : Page
    {
        public Business Business { get; }

        public AddAppPage(Business business)
        {
            Business = business;
            InitializeComponent();
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            Business.Add(new AppEntity() { Id = Guid.NewGuid(), Cmd = TextBoxCmd.Text, ProcessName = TextBoxName.Text });
            MessageBox.Show("Successful!");
            this.NavigationService.Navigate(new AddAppPage(Business));
        }
    }
}