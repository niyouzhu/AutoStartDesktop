using EricNee.AutoStartDesktop.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptionsBuilder)
        {
            services.AddDbContext<AutoStartsDbContext>(dbContextOptionsBuilder, ServiceLifetime.Transient);
            services.AddTransient(typeof(DataAccessor), it =>
            {
                return new DataAccessor(it.GetRequiredService<AutoStartsDbContext>());
            });
            services.AddTransient(typeof(Business), it =>
            {
                return new Business(it.GetRequiredService<DataAccessor>());
            });
            return services;
        }
    }
}
