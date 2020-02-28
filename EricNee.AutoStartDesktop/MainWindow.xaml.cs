using EricNee.AutoStartDesktop.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Business Business { get; } = new Business(new DataAccessor(() =>
        {
            var builder = new DbContextOptionsBuilder().UseSqlite(App.ConnectionString);
            return builder;
        }));


        private Clock Clock { get; } = new Clock();
        private Library.AppSettings AppSettings { get; set; }
        public MainWindow()
        {
            AppSettings = Business.GetAppSettings();
            InitializeComponent();
        }

        public void Refresh()
        {
            AppSettings = Business.GetAppSettings();
            App.AppMagician.AppSettings = AppSettings;
            App.AppMagician.Magic();
        }
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminWindow(Business);
            adminWindow.Closed += (o, e2) =>
            {
                Refresh();
                GridApps_Loaded(null, null);
            };
            adminWindow.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (AppSettings.DisabledAltF4)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void GridApps_Loaded(object sender, RoutedEventArgs e)
        {
            GridApps.Children.Clear();
            var appLayout = new AppLayoutUserControl(Business.GetApps());
            GridApps.Children.Add(appLayout);
            Grid.SetColumn(appLayout, 0);
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            Clock.Interval += (args) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    LabelTime.Content = args.Now.ToString("MM-dd HH:mm:ss dddd");
                });
            };
            await Clock.Start();
        }
    }
}
