using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    public class AutoStartsDbContext : DbContext
    {
        public DbSet<AppEntity> Apps { get; set; }

        public DbSet<AdminEntity> Admin { get; set; }


        public AutoStartsDbContext(Func<DbContextOptionsBuilder> builder) : base(builder().Options)
        {
            this.Database.EnsureCreated();
        }

    }
}
