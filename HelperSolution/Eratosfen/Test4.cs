using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eratosfen
{
    public class Test4
    {
        public static void Run()
        {
            int sqrtMaxn = 100000; // корень из максимального значения N
            int s = 30000;
            bool[] nprime = new bool[sqrtMaxn];
            int[] primes = new int[sqrtMaxn];
            int cnt = 0;

            var sNum = 10000000;
            var n = (int)(sNum * Math.Log(sNum) + sNum * Math.Log(Math.Log(sNum)));
            var nsqrt = (int)Math.Sqrt(n + 0.0);

            nprime[0] = nprime[1] = true;
            for (var i = 2; i <= nsqrt; ++i)
            {
                if (nprime[i])
                    continue;
                primes[cnt++] = i;
                if (i * i > nsqrt)
                    continue;
                for (var j = i * i; j <= nsqrt; j += i)
                    nprime[j] = true;
            }


            var result = 0;

            for (int k = 0, maxk = n / s; k <= maxk; ++k)
            {
                var block = new bool[s];

                var start = k * s;

                for (var i = 0; i < cnt; ++i)
                {
                    var startIdx = (start + primes[i] - 1) / primes[i];
                    var j = Math.Max(startIdx, 2) * primes[i] - start;
                    for (; j < s; j += primes[i])
                        block[j] = true;
                }

                if (k == 0)
                    block[k] = block[k + 1] = true;

                for (var i = 0; i < s && start + i <= n; ++i)
                {
                    if (!block[i])
                    {
                        ++result;
                        if (result == sNum)
                        {
                            Console.WriteLine(i + start);
                        }
                    }
                }
            }
            Console.WriteLine(result);

        }
    }
}
