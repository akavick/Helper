using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eratosfen
{
    class OptimizedCached
    {
        private static int[] GetDivisorPrimesD(int searchingNumber, out int? prime)
        {
            // Максимальный делитель
            var maxDivisorPrime = (int)Math.Sqrt(int.MaxValue);
            var currentDivisorPrime = 3;
            var divisorPrimes = new List<int> { 2, currentDivisorPrime };

            // Заполняем массив делителей
            var count = divisorPrimes.Count;
            while ((currentDivisorPrime += 2) < maxDivisorPrime)
            {
                var i = 0;

                while (count < searchingNumber)
                {
                    var divisorPrime = divisorPrimes[++i];

                    if (currentDivisorPrime % divisorPrime == 0)
                        break;

                    if (divisorPrime * divisorPrime < currentDivisorPrime)
                        continue;

                    divisorPrimes.Add(currentDivisorPrime);
                    ++count;
                    break;
                }
            }


            if (count >= searchingNumber)
            {
                prime = divisorPrimes[searchingNumber - 1];
                return null;
            }

            prime = null;
            return divisorPrimes.ToArray();
        }


        public static void Run()
        {
            var sNum = 10000000;
            var n = (int)(sNum * Math.Log(sNum) + sNum * Math.Log(Math.Log(sNum)));
            var count = 0;

            var cache = 30000; // размер кэша  
            var m = (int)Math.Sqrt(n) + 1;
            var p = (int)Math.Sqrt(sNum) + 1;

            var primes = GetDivisorPrimesD(sNum, out var _); // массив простых чисел до корня из N  
            var segment = new bool[cache]; // вторичное решето  
            for (var I = m - 1; I < n; I += cache)
            {
                for (var i = 0; i < cache; i++)
                    segment[i] = true;


                for (var i = 0; i < p; i++)
                {
                    var h = I % primes[i];
                    var j = h > 0 ? primes[i] - h : 0;
                    for (; j < cache; j += primes[i])
                        segment[j] = false;
                }
                for (var i = 0; i < cache; i++)
                {
                    if (segment[i] && (i + I < n))
                    {
                        if(count++ == sNum)
                            Console.WriteLine(i + I); // выводим простое число на экран  
                    }
                }
            }
        }
    }
}
