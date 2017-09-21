using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eratosfen
{
    class Test1
    {
        public static void Run()
        {
            int size = 1000000;
            int limit = (int)Math.Sqrt(size) + 1;

            int[] primes = new int[size / 32];

            void Sieve()
            {
                for (int i = 0; i < size / 32; i++)
                    primes[i] = 0xffff;
                primes[0] &= ~(1 << (1 % 32));
                for (int i = 2; i <= limit; i++)
                    if ((primes[i / 32] & (1 << (i % 32))) != 0)
                        for (int j = 2 * i; j < size; j += i)
                            primes[j / 32] &= ~(1 << (j % 32));

            }

            bool IsPrime(int n)
            {
                return (primes[n / 32] & (1 << (n % 32))) != 0;
            }
        }



    }
}
