using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouplelessNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int max = 150;
            const int coupleless = 77;
            var r = new Random();
            var range = Enumerable.Range(1, coupleless - 1)
                .Concat(Enumerable.Range(coupleless + 1, max - coupleless - 1))
                .ToList();
            var finalRange = range
                .Concat(range)
                //.Concat(range) // tripleless
                .Concat(new[] { coupleless })
                .OrderBy(i => r.Next()) // shuffle
                .ToList();
            var result = finalRange
                .Aggregate(0, (acc, num) => acc ^ num);


            Console.WriteLine(result);
        }
    }
}
