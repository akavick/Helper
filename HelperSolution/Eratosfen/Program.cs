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
            var num = 1;
            var size = (int)(num * Math.Log(num) + num * Math.Log(Math.Log(num)));
            if (size < 10)
                size = 10;


            var sw = new Stopwatch();
            sw.Start();

            //var bools = new BitArray(size);
            var bools = new bool[size];

            for (var i = 2; i < size; i++)
                bools[i] = true;


            for (int i = 2, end = size + 1; i * i < end; ++i)
            {
                if (!bools[i])
                    continue;
                for (var j = i * i; j < size; j += i)
                    bools[j] = false;
            }

            for (int i = 2, n = 1; i < size; i++)
            {
                if (bools[i])
                {
                    //Console.WriteLine(i);
                    if (n++ == num)
                        Console.WriteLine(i);
                }
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
