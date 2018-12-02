using System;





namespace CsLearning
{

    public class Program
    {
        private static readonly string _nl = Environment.NewLine;





        private static void Main(string[] args)
        {
            Console.WriteLine($"START{_nl}");
            Run();
            Console.WriteLine($"{_nl}FINISH{_nl}");
            Console.ReadKey(false);
        }





        private static void Run()
        {
            GetLesson<TraceDebug>().Run();
        }





        private static ILesson /*T*/ GetLesson<T>()
            where T : ILesson, new()
        {
            return new T();
        }
    }

}
