using EricNee.AutoStartDesktop.Library;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            var appEntity = new AppEntity() { Id = Guid.NewGuid(), Args = TextBoxArgs.Text, Cmd = TextBoxCmd.Text, ProcessName = TextBoxName.Text };
            if (ImageIcon.Source != null)
            {
                appEntity.Icon = ((BitmapImage)ImageIcon.Source).ToBytes();
            }
            Business.Add(appEntity);
            MessageBox.Show("Successful!");
            this.NavigationService.Navigate(new AddAppPage(Business));
        }

        private void ButtonChooser_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaOpenFileDialog() { Multiselect = false, CheckFileExists = true, Title = "App Path" };
            if (dialog.ShowDialog() == true)
            {
                TextBoxCmd.Text = dialog.FileName;
                var icon = Icon.ExtractAssociatedIcon(TextBoxCmd.Text);
                ImageIcon.Source = icon?.ToBitmap().ToBitmapImage();
                if (string.IsNullOrWhiteSpace(TextBoxName.Text))
                {
                    TextBoxName.Text = new FileInfo(TextBoxCmd.Text).Name;
                }
            }
        }
    }
}