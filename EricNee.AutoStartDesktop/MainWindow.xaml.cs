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
            var builder = new DbContextOptionsBuilder().UseSqlCe("DataSource=db.sdf");
            return builder;
        }));

        private Library.AppSettings AppSettings { get; set; }
        public MainWindow()
        {
            Business.Init();
            AppSettings = Business.GetAppSettings();
            AppMagician = new AppMagician(AppSettings);
            InitializeComponent();
        }

        public void Refresh()
        {
            AppSettings = Business.GetAppSettings();
            AppMagician.AppSettings = AppSettings;
            AppMagician.Magic();
        }
        private AppMagician AppMagician { get; set; }

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

        private void Window_Initialized(object sender, EventArgs e)
        {
            AppMagician.Magic();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            AppMagician.Dispose();
        }




    }
}
