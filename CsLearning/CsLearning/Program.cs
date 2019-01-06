using System;
using System.Threading.Tasks;

using CsLearningLibrary;



namespace CsLearning
{

    public class Program
    {
        private static readonly string _nl = Environment.NewLine;



        private static async Task Main(string[] args)
        {
            Console.WriteLine($"START{_nl}");

            await GetLesson<RefStruct>().Run();

            Console.WriteLine($"{_nl}FINISH{_nl}");
            Console.ReadKey(false);
        }



        private static ILesson /*T*/ GetLesson<T>()
            where T : ILesson, new()
        {
            return new T();
        }
    }

}
