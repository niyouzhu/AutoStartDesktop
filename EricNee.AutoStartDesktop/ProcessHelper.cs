using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EricNee.AutoStartDesktop
{
    public class ProcessHelper
    {
        public static Process Open(ProcessStartInfo processStartInfo)
        {
            return System.Diagnostics.Process.Start(processStartInfo);
        }

        public static Process Open(string appPath)
        {
            return Open(new ProcessStartInfo(appPath));
        }

        public static Process OpenIfExisted(ProcessStartInfo processStartInfo)
        {
            var processs = System.Diagnostics.Process.GetProcessesByName(processStartInfo.FileName);
            if (processs?.Length > 0) return processs[0];
            else return Open(processStartInfo);
        }

        public static Process OpenIfExisted(string appPath)
        {
            return OpenIfExisted(new ProcessStartInfo(appPath));
        }
    }
}
