using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atkin
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            var limit = 180000000/*int.Parse(Console.ReadLine())*/;
            var primes = AtkinSieve.GetPrimesUpTo(limit);

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            // Вывод последовательности
            for (int number = 2, n = 1; number <= limit; ++number)
            {
                if (primes[number])
                    //Console.Write("{0} ", number);
                {
                    ++n;
                    if (n == 10000000)
                        Console.WriteLine(number);
                }
            }
            //Console.WriteLine();
        }
    }



















    public static class AtkinSieve
    {
        public static BitArray GetPrimesUpTo(int limit)
        {
            var sieve = new BitArray(limit + 1);
            //var sieve = new bool[limit + 1];
            // Предварительное просеивание
            for (long x2 = 1L, dx2 = 3L; x2 < limit; x2 += dx2, dx2 += 2L)
            {
                for (long y2 = 1L, dy2 = 3L; y2 < limit; y2 += dy2, dy2 += 2L)
                {
                    // n = 4x² + y²
                    var n = (x2 << 2) + y2;
                    if (n <= limit && (n % 12L == 1L || n % 12L == 5L))
                        sieve[(int)n] ^= true;

                    // n = 3x² + y²
                    n -= x2;
                    if (n <= limit && n % 12L == 7L)
                        sieve[(int)n] ^= true;

                    // n = 3x² - y² (при x > y)
                    if (x2 <= y2)
                        continue;

                    n -= y2 << 1;

                    if (n <= limit && n % 12L == 11L)
                        sieve[(int)n] ^= true;
                }
            }
            // Все числа, кратные квадратам, помечаются как составные
            var r = 5;
            for (long r2 = r * r, dr2 = (r << 1) + 1L; r2 < limit; ++r, r2 += dr2, dr2 += 2L)
            {
                if (!sieve[r])
                    continue;
                for (var mr2 = r2; mr2 < limit; mr2 += r2)
                    sieve[(int)mr2] = false;
            }
            // Числа 2 и 3 — заведомо простые
            if (limit > 2)
                sieve[2] = true;
            if (limit > 3)
                sieve[3] = true;
            return sieve;
            //return new BitArray(sieve);
        }
    }
}
