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
            var N = (int)(sNum * Math.Log(sNum) + sNum * Math.Log(Math.Log(sNum)));
            int? prime;
            var count = 0;

            int CACHE = 30000; // размер кэша  
            int M = (int)Math.Sqrt(N) + 1;
            var P = (int)Math.Sqrt(sNum) + 1;

            var primes = GetDivisorPrimesD(sNum, out prime); // массив простых чисел до корня из N  
            var segment = new bool[CACHE]; // вторичное решето  
            for (int I = M - 1; I < N; I += CACHE)
            {
                for (int i = 0; i < CACHE; i++)
                    segment[i] = true;


                for (int i = 0; i < P; i++)
                {
                    int h = I % primes[i];
                    int j = h > 0 ? primes[i] - h : 0;
                    for (; j < CACHE; j += primes[i])
                        segment[j] = false;
                }
                for (int i = 0; i < CACHE; i++)
                {
                    if (segment[i] && (i + I < N))
                    {
                        if(count++ == sNum)
                            Console.WriteLine(i + I); // выводим простое число на экран  
                    }
                }
            }
        }
    }
}
