using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    public class DataAccessor : IDisposable
    {
        public DataAccessor(AutoStartsDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private AutoStartsDbContext DbContext { get; set; }
        public IEnumerable<AppEntity> GetApps()
        {
            return DbContext.Apps.ToList();
        }


        public bool ExistsAdmin()
        {
            return DbContext.Admin.Any();
        }

        public AdminEntity AddAdmin(AdminEntity admin)
        {
            var rt = DbContext.Admin.Add(admin);
            DbContext.SaveChanges();
            return rt.Entity;
        }

        public AdminEntity GetAdmin()
        {
            return DbContext.Admin.First();
        }

        public AppEntity Add(AppEntity app)
        {
            var rt = DbContext.Apps.Add(app);
            DbContext.SaveChanges();
            return rt.Entity;
        }

        public AppEntity Delete(AppEntity app)
        {
            var existing = DbContext.Apps.Single(it => it.Id == app.Id);
            var rt = DbContext.Apps.Remove(existing);
            return rt.Entity;
        }

        public IEnumerable<AppEntity> Delete(IEnumerable<AppEntity> apps)
        {
            var rt = new List<AppEntity>(apps.Count());
            foreach (var app in apps)
            {
                var existing = DbContext.Apps.Single(it => it.Id == app.Id);
                rt.Add(DbContext.Apps.Remove(existing).Entity);
            }
            DbContext.SaveChanges();
            return rt;
        }


        public AdminEntity Update(AdminEntity admin)
        {
            var rt = DbContext.Admin.Single();
            rt.Password = admin.Password;
            rt.Settings = admin.Settings;
            DbContext.SaveChanges();
            return rt;
        }

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed && DbContext != null)
            {
                _disposed = true;
                DbContext.Dispose();
                DbContext = null;
            }
        }
    }
}
