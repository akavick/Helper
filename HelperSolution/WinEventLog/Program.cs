using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WinEventLog
{



    class Program
    {
        static void Main(string[] args)
        {
        }
    }



    public class ClsEventLog
    {
        public bool CreateLog(string strLogName)
        {
            try
            {
                EventLog.CreateEventSource(strLogName, strLogName);
                var sqlEventLog = new EventLog();

                sqlEventLog.Source = strLogName;
                sqlEventLog.Log = strLogName;

                sqlEventLog.Source = strLogName;
                sqlEventLog.WriteEntry("The " + strLogName + " was successfully initialize component.", EventLogEntryType.Information);

                return true;
            }
            catch
            {
                return false;
            }
        }



        public void WriteToEventLog(string strLogName, string strSource, string strErrDetail)
        {
            var sqlEventLog = new EventLog();

            try
            {
                if (!EventLog.SourceExists(strLogName))
                {
                    CreateLog(strLogName);
                }

                sqlEventLog.Source = strLogName;
                sqlEventLog.WriteEntry(Convert.ToString(strSource) + Convert.ToString(strErrDetail), EventLogEntryType.Information);

            }
            catch (Exception ex)
            {
                sqlEventLog.Source = strLogName;
                sqlEventLog.WriteEntry(Convert.ToString("INFORMATION: ") + Convert.ToString(ex.Message), EventLogEntryType.Information);
            }
            finally
            {
                sqlEventLog.Dispose();
                sqlEventLog = null;
            }
        }
    }



}
