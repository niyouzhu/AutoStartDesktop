using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    public class DataAccessor
    {
        public DataAccessor(Func<DbContextOptionsBuilder> optionsBudiler)
        {
            DbContextOptionsBuilder = optionsBudiler;
        }
        public Func<DbContextOptionsBuilder> DbContextOptionsBuilder { get; }
        public IEnumerable<AppEntity> GetApps()
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                return dbContext.Apps.ToList();
            }
        }


        public bool ExistsAdmin()
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                return dbContext.Admin.Any();
            }
        }

        public AdminEntity AddAdmin(AdminEntity admin)
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                var rt = dbContext.Admin.Add(admin);
                dbContext.SaveChanges();
                return rt.Entity;
            }
        }

        public AdminEntity GetAdmin()
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                return dbContext.Admin.First();
            }
        }

        public AppEntity Add(AppEntity app)
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                var rt = dbContext.Apps.Add(app);
                dbContext.SaveChanges();
                return rt.Entity;
            }
        }

        public AppEntity Delete(AppEntity app)
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                var existing = dbContext.Apps.Find(app.Id);
                var rt = dbContext.Apps.Remove(existing);
                return rt.Entity;
            }
        }

        public IEnumerable<AppEntity> Delete(IEnumerable<AppEntity> apps)
        {
            var rt = new List<AppEntity>(apps.Count());
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                foreach (var app in apps)
                {
                    var existing = dbContext.Apps.Find(app.Id);
                    rt.Add(dbContext.Apps.Remove(existing).Entity);
                }
                dbContext.SaveChanges();
                return rt;
            }
        }


        public AdminEntity Update(AdminEntity admin)
        {
            using (var dbContext = new AutoStartsDbContext(DbContextOptionsBuilder))
            {
                var rt = dbContext.Admin.Single();
                rt.Password = admin.Password;
                rt.Settings = admin.Settings;
                dbContext.SaveChanges();
                return rt;
            }
        }
    }
}
