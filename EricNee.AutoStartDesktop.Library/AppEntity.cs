using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EricNee.AutoStartDesktop.Library
{

    public class AppEntity
    {
        public string Cmd { get; set; }

        public string ProcessName { get; set; }
        public byte[] Icon { get; set; }

        [Key]
        public Guid Id { get; set; }

        public string LogonName { get; set; }

        public string Args { get; set; }
    }
}
