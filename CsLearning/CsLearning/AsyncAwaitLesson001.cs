using System;
using System.Linq;
using System.Threading.Tasks;



namespace CsLearning
{

    public class AsyncAwaitLesson001 : ILesson
    {

        public async Task Run()
        {
            var r = await DoCycle(1000);

            Console.WriteLine();
        }



        private static async Task<int> DoCycle(int count)
        {
            var tasks =
                Enumerable.Range(1, count)
                          //.Select(async i => await DoSingle(i))
                          .Select(DoSingle)
                          .ToArray();

            await Task.WhenAll(tasks);

            return count;
        }



        private static async Task<int> DoSingle(int i)
        {
            await Task.Delay(500);
            Console.Write($"{i,-5}");

            return i;
        }
    }

}