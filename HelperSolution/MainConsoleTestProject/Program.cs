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

            Console.WriteLine("hi");

            MAsync();

            Console.WriteLine("bye");

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
