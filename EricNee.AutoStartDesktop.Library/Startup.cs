using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EricNee.AutoStartDesktop.Library
{
    public class Startup
    {
        public Startup(string appDirectory)
        {
            AppDirectory = appDirectory;
        }

        public string AppDirectory { get; }

        public void ConfigureServices()
        {
            string connectionString = $"DataSource={Path.Combine(AppDirectory, "AutoStartDesktop.db")};";
            Services.AddBusiness(it => it.UseSqlite(connectionString));
            ServiceProvider = ServiceProviderFactory.CreateServiceProvider(Services);
        }

        public IServiceProvider ServiceProvider { get; private set; }

        private IServiceProviderFactory<IServiceCollection> ServiceProviderFactory = new DefaultServiceProviderFactory();
        private IServiceCollection Services { get; } = new ServiceCollection();

    }
}
