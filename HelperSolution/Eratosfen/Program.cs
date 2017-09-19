using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eratosfen
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.MaxValue;
            var searched = 10000000;

            var sw = new Stopwatch();
            sw.Start();

            var bools = new BitArray(n);

            for (var i = 2; i < n; i++)
                bools[i] = true;

            var counter = int.MaxValue - 2;
            var ranToStop = false;

            // Обработка
            for (int i = 2, end = n + 1; i * i < end; ++i)
            {
                if (!bools[i])
                    continue;
                for (var j = i * i; j < n; j += i)
                {
                    bools[j] = false;
                    --counter;
                    //if (n - counter == searched)
                    //    ranToStop = true;
                }
            }


            sw.Stop();

            Console.WriteLine(sw.Elapsed);

            // Повторный вывод
            for (int i = 2, num = 1; i < n; i++)
            {
                if (bools[i])
                {
                    num++;
                    if (num == searched)
                        Console.WriteLine(i);
                }
            }
        }
    }
}
