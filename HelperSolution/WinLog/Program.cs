using System;
using System.Diagnostics;

namespace WinLog
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Run("AM Scheduler", "Application", "Sample Event from AM Scheduler");
                Run("AM Application", "Application", "Sample Event from AM Application");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(Environment.NewLine +  "Press any key to exit...");
            Console.ReadKey(false);
        }

        private static void Run(string sSource, string sLog, string sEvent)
        {
            if (EventLog.SourceExists(sSource))
                EventLog.DeleteEventSource(sSource);
            EventLog.CreateEventSource(sSource, sLog);
            EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Warning, 234);
        }
    }
}
