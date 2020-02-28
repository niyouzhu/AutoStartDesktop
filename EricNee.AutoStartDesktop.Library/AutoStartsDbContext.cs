using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    public class AutoStartsDbContext : DbContext
    {
        public DbSet<AppEntity> Apps { get; set; }

        public DbSet<AdminEntity> Admin { get; set; }

        public AutoStartsDbContext() : base()
        {
            Database.EnsureCreated();
        }

        public AutoStartsDbContext(Func<DbContextOptionsBuilder> builder) : base(builder?.Invoke().Options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
