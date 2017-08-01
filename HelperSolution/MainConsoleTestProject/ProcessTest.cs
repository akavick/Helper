using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    class ProcessTest
    {

        public void Run()
        {
            var myID = Process.GetCurrentProcess();
            var query = string.Format("SELECT ParentProcessId FROM Win32_Process");// WHERE ProcessId = {0}", myID);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            var results = search.Get().GetEnumerator();

            if (!results.MoveNext())
                throw new Exception("Huh?");

            var queryObj = results.Current;
            uint parentId = (uint)queryObj["ParentProcessId"];

            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.Length > 0)
                {
                    Console.WriteLine(p.MainWindowTitle.ToString());
                    Console.WriteLine(p.ProcessName.ToString());
                    Console.WriteLine(p.MainWindowHandle.ToString());
                    Console.WriteLine(p.PrivateMemorySize64.ToString());
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }


    }




    public class ProcessIdentityScope : IDisposable
    {
        private WindowsImpersonationContext _impersonationContext;
        private bool _disposed;

        public ProcessIdentityScope()
        {
            _impersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _impersonationContext.Undo();
                _impersonationContext.Dispose();
                _disposed = true;
            }
            else
                throw new ObjectDisposedException("ProcessIdentityScope");
        }

        #endregion
    }

}
