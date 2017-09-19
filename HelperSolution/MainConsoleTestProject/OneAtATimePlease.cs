using System;
using System.Threading;

namespace MainConsoleTestProject
{
    internal class OneAtATimePlease
    {
        public void Run()
        {
            // Назначение объекту Mutex имени делает его доступным на уровне всего компьютера.
            // Используйте имя, являющееся уникальным для вашей компании и приложения
            // (например, включите в него URL компании) .
            using (var mutex = new Mutex(true, "VeryLongAndUniqueMutexName"))
            {
                // Ожидать несколько секунд, если возникло соперничество; в этом случае
                // другой экземпляр прог раммы все еше находится в процессе завершения.
                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Another iпstance of the арр is running. Буе!");
                    // Выполняется другой экземпляр прог раммы
                    return;
                }
                try
                {
                    RunProgram();
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        static void RunProgram()
        {
            Console.WriteLine("Runпiпg. Press Enter to exit");
            // Программа выполняется; нажмите Enter для завершения
            Console.ReadLine();
        }
    }
}