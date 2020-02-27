using System;
using System.Collections.Generic;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{
    public class AdminBusinessModel
    {
        public string Password { get; set; }
        public AppSettings AppSettings { get; set; }

        public AdminEntity ToEntity()
        {
            return new AdminEntity() { Password = Password, Settings = AppSettings.ToFormatString() };
        }
    }
}
