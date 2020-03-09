using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EricNee.AutoStartDesktop
{
    public class ProcessSet : HashSet<Process>
    {

        public Process Open(string cmd, string args, out bool existing)
        {
            return Open(new ProcessStartInfo(cmd, args), out existing);
        }
        public Process Open(ProcessStartInfo processStartInfo, out bool existing)
        {
            existing = false;
            var first = this.FirstOrDefault(it => string.Equals(it.StartInfo.FileName, processStartInfo.FileName, StringComparison.InvariantCultureIgnoreCase));
            if (first != null)
            {
                existing = true;
                return first;
            }
            else
            {
                var process = ProcessSet.Open(processStartInfo);
                process.EnableRaisingEvents = true;
                process.Exited += (o, e) =>
 {
     this.Remove(process);
 };
                this.Add(process);
                return process;
            }
        }

        public static Process Open(ProcessStartInfo processStartInfo)
        {
            return System.Diagnostics.Process.Start(processStartInfo);
        }

        public static Process Open(string appPath)
        {
            return Open(new ProcessStartInfo(appPath));
        }

        public static Process OpenIfExisted(ProcessStartInfo processStartInfo, out bool existing)
        {
            existing = false;
            var processs = System.Diagnostics.Process.GetProcessesByName(processStartInfo.FileName);
            if (processs?.Length > 0) { existing = true; return processs[0]; }
            else return Open(processStartInfo);
        }

        public static Process OpenIfExisted(string appPath, out bool existing)
        {
            return OpenIfExisted(new ProcessStartInfo(appPath), out existing);
        }
    }
}
