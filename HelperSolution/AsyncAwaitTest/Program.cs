using System;
using System.Linq;
using System.Threading.Tasks;



namespace AsyncAwaitTest
{



    internal class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("BEGIN\n\n");

            var r = await A(1000);

            Console.WriteLine($"\n\n\n{r}");

            Console.WriteLine("\n\nEND\n\n");
        }



        private static async Task<int> A(int count)
        {
            var tasks =
                Enumerable.Range(1, count)
                          .Select(async i => await B(i))
                          .ToArray();

            await Task.WhenAll(tasks);

            return count;
        }



        private static async Task<int> B(int i)
        {
            await Task.Delay(500);
            Console.Write($"{i,-5}");

            return i;
        }
    }



}
