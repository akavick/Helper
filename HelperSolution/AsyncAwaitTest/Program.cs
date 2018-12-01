using System;
using System.Threading.Tasks;





namespace AsyncAwaitTest
{

    internal class Program
    {
        private static async Task Main()
        {
            var r = A().Result;

            Console.WriteLine(r);
        }





        private static async Task<int> A()
        {
            await B();

            return 0;
        }
        private static async Task B()
        {
            await Task.Delay(500);
        }
    }

}
