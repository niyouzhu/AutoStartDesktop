using EricNee.AutoStartDesktop.Library;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace EricNee.AutoStartDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        internal static string ConnectionString { get; } = $"DataSource={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoStartDesktop.db")};";
        private Business Business { get; } = new Business(new DataAccessor(() =>
        {
            var builder = new DbContextOptionsBuilder().UseSqlite(ConnectionString);
            return builder;
        }));
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Business.Init();
            AppMagician = new AppMagician(Business.GetAppSettings());
            AppMagician.Magic();
        }



        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppMagician.Dispose();
        }

        internal static AppMagician AppMagician { get; private set; }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
            System.Diagnostics.Trace.WriteLine($"{e.Exception?.ToString()}{Environment.NewLine}{e.Exception?.InnerException?.ToString()}{Environment.NewLine}");
        }

        internal static ProcessSet ProcessSet { get; } = new ProcessSet();

    }
}
