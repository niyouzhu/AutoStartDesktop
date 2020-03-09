using EricNee.AutoStartDesktop.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace EricNee.AutoStartDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {



        internal static Startup MainStartup { get; private set; } = new Startup(AppDomain.CurrentDomain.BaseDirectory);

        protected override void OnStartup(StartupEventArgs e)
        {
            MainStartup.ConfigureServices();
            base.OnStartup(e);
            using (var business = MainStartup.ServiceProvider.GetRequiredService<Business>())
            {
                business.Init();
                var appSettings = business.GetAppSettings();
                AppMagician = new AppMagician(appSettings);
                if (!string.IsNullOrWhiteSpace(appSettings.CultureName) && appSettings.CultureName != "neutral")
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(appSettings.CultureName);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(appSettings.CultureName);
                }
                AppMagician.Magic();
            }
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
