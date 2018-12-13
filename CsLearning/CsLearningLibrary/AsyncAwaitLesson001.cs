using System;
using System.Linq;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class AsyncAwaitLesson001 : ILesson
    {

        public async Task Run()
        {
            var r = await DoCycle(1000);
            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Count: {r}");
        }



        private static async Task<int> DoCycle(int count)
        {
            var tasks =
                Enumerable.Range(1, count)
                          .Select(async i => await DoSingle(i))
                          //.Select(DoSingle)
                          .ToArray();

            var c = await Task.WhenAll(tasks);

            return c.Length;
        }



        private static async Task<int> DoSingle(int i)
        {
            //await Task.Delay(5000);

            await Task.Run(() =>
            {
                for (var j = 0; j < 100000000; j++) {};
            });

            Console.Write($"{i,-5}");

            return i;
        }
    }

}