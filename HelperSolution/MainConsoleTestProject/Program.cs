using System;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //new MatrixPainter().Run();
            //new ChainFunction().Run();
            //new StaticGcTest().Run();
            //new ProcessTest().Run();
            //new Tricks().Run();

            //Console.WriteLine("hi");
            //MAsync();
            //Console.WriteLine("bye");


            Console.WriteLine(DateTime.Parse("01.17.2017"));



            Console.ReadKey(true);
        }



        private static async void MAsync()
        {
            //await Task.Yield();
            await Task.Delay(1000);
            Console.WriteLine("here");
        }
    }
}
