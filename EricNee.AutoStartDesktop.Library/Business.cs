using System;
using System.Collections.Generic;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    public class Business
    {
        public DataAccessor DataAccessor { get; }
        public Business(DataAccessor dataAccessor)
        {
            DataAccessor = dataAccessor;
        }

        public bool Verify(string password)
        {
            var correct = DataAccessor.GetAdmin().Password;
            if (password == correct) return true;
            return false;
        }

        public IEnumerable<AppEntity> GetApps()
        {
            return DataAccessor.GetApps();
        }

        public AppEntity Add(AppEntity app)
        {
            return DataAccessor.Add(app);
        }

        public AppEntity Remove(AppEntity app)
        {
            return DataAccessor.Delete(app);
        }
        public IEnumerable<AppEntity> Remove(IEnumerable<AppEntity> apps)
        {
            return DataAccessor.Delete(apps);
        }
        public bool ExistsAdmin()
        {
            return DataAccessor.ExistsAdmin();
        }

        public AdminBusinessModel AddDefaultAdmin()
        {
            var admin = new AdminBusinessModel() { Password = "123456", AppSettings = new AppSettings() { DisabledAltF4 = true, DisabledWindowsKey = true, HiddenTaskbar = true } };
            DataAccessor.AddAdmin(admin.ToEntity());
            return admin;
        }

        public void Init()
        {
            if (!ExistsAdmin())
            {
                Add(new AppEntity() { Cmd = "notepad", ProcessName = "Notepad" });
                AddDefaultAdmin();
            }
        }

        public AdminBusinessModel GetAdmin()
        {
            var admin = DataAccessor.GetAdmin();
            return new AdminBusinessModel() { AppSettings = AppSettings.ToObject(admin.Settings), Password = admin.Password };
        }

        public AppSettings GetAppSettings()
        {
            return GetAdmin().AppSettings;
        }

        public AppSettings UpdateSettings(AppSettings appSettings)
        {
            var existing = GetAdmin();
            DataAccessor.Update(new AdminEntity() { Settings = appSettings.ToFormatString(), Password = existing.Password });
            return appSettings;
        }
        public ChangedPasswordBusinessModel UpdateAdmin(ChangedPasswordBusinessModel password)
        {
            var admin = GetAdmin();
            if (admin.Password == password.OldPassword)
            {
                DataAccessor.Update(new AdminEntity() { Password = password.NewPassword, Settings = admin.AppSettings.ToFormatString() });
                return password;
            }
            throw new Exception("Old password is incorrect!");
        }
    }
}
