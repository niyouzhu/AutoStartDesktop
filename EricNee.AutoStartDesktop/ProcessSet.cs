using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EricNee.AutoStartDesktop
{
    public class ProcessSet
    {
        public Dictionary<string, Process> Processes { get; } = new Dictionary<string, Process>();


        public Process Open(string cmd, string args, out bool existing)
        {
            return Open(new ProcessStartInfo(cmd, args), out existing);
        }
        public Process Open(ProcessStartInfo processStartInfo, out bool existing)
        {
            existing = false;
            var first = Processes.FirstOrDefault(it => string.Equals(it.Value.StartInfo.FileName, processStartInfo.FileName));
            if (!first.Equals(default(KeyValuePair<string, Process>)))
            {
                existing = true;
                return first.Value;
            }
            else
            {
                var process = ProcessSet.Open(processStartInfo);
                process.EnableRaisingEvents = true;
                process.Exited += (o, e) =>
 {
     Processes.Remove(process.ProcessName);
 };
                Processes.Add(process.ProcessName, process);
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
