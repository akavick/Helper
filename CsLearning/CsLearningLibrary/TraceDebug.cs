using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class TraceDebug : ILesson
    {

        public async Task Run()
        {
            /*
             * 0
               Off
               Ничего не выводится
               1
               Error
               Выводятся только ошибки
               2
               Warning
               Выводятся ошибки и предупреждения
               3
               Info
               Выводятся ошибки, предупреждения и информация
               4
               Verbose
               Выводится вся информация
             */

            var ts = new TraceSwitch("MySwitch", "This switch is set via code.");

            if (System.Enum.TryParse("info", ignoreCase: true, result: out TraceLevel level))
            {
                ts.Level = level;
            }

            Trace.WriteLineIf(ts.TraceError, "Trace error");
            Trace.WriteLineIf(ts.TraceWarning, "Trace warning");
            Trace.WriteLineIf(ts.TraceInfo, "Trace information");
            Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");


            // запись в текстовый файл в папке проекта
            Trace.Listeners.Add(new TextWriterTraceListener(File.CreateText("log.txt")));

            /*
             * При отладке активизируются оба класса Debug и Trace и отображают свой вывод в области
             * OUTPUT (Вывод) или DEBUG CONSOLE (Консоль отладки). При запуске без отладки
             * отображается только вывод класса Trace. Таким образом, вы свободно можете вызывать
             * метод Debug.WriteLine по всему коду, зная, что он будет удален при компиляции
             * релизной версии вашего приложения.
             */

            // модуль записи текста буферизован, поэтому данная опция вызывает
            // Flush() для всех прослушивателей после записи
            Trace.AutoFlush = true;

            Debug.WriteLine("Debug says, I am watching!");
            Trace.WriteLine("Trace says, I am watching!");
        }
    }

}