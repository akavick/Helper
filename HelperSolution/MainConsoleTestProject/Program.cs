using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //new Program().Run1();

            Console.WriteLine("hi");

            var t = Task.Run(() =>
            {
                for (var i = 0; i < 3; i++)
                    Console.WriteLine("i");
            });

            t.GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine("phase 2");
            });

            Console.WriteLine("bye");
            Console.ReadKey(true);
        }



        private void Run1()
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for (int i = 1; i < 300000001; i++)
            //{
            //    //var r = 163156789 / i;
            //}
            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);

            //var primeNumberTask = Task.Run(() =>
            //{
            //    return Enumerable
            //        .Range(2, 3000000)
            //        .Count(n => Enumerable
            //            .Range(2, (int)Math.Sqrt(n) - 1)
            //            .All(i => n % i > 0));
            //});
            //Console.WriteLine("Task running ... ");
            //Console.WriteLine("The answer is " + primeNumberTask.Result);


            //for (var i = 0; i < 10000; i++)
            //{
            //    var x = i;
            //    //Task.Run(async () =>
            //    //{
            //    //    await Task.Delay(1000);
            //    //    Console.WriteLine(x);
            //    //});
            //    //Task.Factory.StartNew(() =>
            //    //{
            //    //    Thread.Sleep(1000);
            //    //    Console.WriteLine(x);
            //    //}, TaskCreationOptions.LongRunning);
            //}
        }

        private void Run2() => new MatrixPainter().Run();

        private void Run3() => new ChainFunction().Run();

        private void Run4() => new StaticGcTest().Run();

        private void Run5() => new ProcessTest().Run();

        private void Run6() => new Tricks().Run();

        private void Run7()
        {
            Console.WriteLine("hi");
            Task.Run(async () =>
            {
                //await Task.Yield();
                await Task.Delay(1000);
                Console.WriteLine("here");
            });
            Console.WriteLine("bye");
        }

        private void Run8(dynamic a)
        {
            var r = a?[0];
            Console.WriteLine(r);
        }

        private void Run9()
        {
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Console.WriteLine(e.Exception);
                e.SetObserved();
            };

            Task.Run(() => throw new Exception("boom!"));

            Task.Delay(1000).GetAwaiter().GetResult();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
