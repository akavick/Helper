using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static System.Linq.Enumerable;
using static System.Threading.Tasks.Task;

namespace MainConsoleTestProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new ChainFunction().Run();
            
            
            
            //new Program().Run17();
            //LongNumbersS.Run();
            Console.ReadKey(true);
        }





        private void Run17()
        {
            long x = 1;
            var y = x >> 190;
        }



        private void Run16()
        {
            var number = 2;
            var currentPrime = 3;
            var primes = new List<int> { currentPrime };

            while (number < 20)
            {
                currentPrime += 2;

                //for (var i = 0;;i++)
                //{
                //    var divisorPrime = primes[i];

                //    if (currentPrime % divisorPrime == 0)
                //        break;

                //    if (divisorPrime * divisorPrime < currentPrime)
                //        continue;

                //    number++;
                //    primes.Add(currentPrime);
                //    break;
                //}

                if (!primes
                        .TakeWhile(divisorPrime => currentPrime % divisorPrime != 0)
                        .Any(divisorPrime => divisorPrime * divisorPrime >= currentPrime))
                    continue;
                number++;
                primes.Add(currentPrime);
            }

        }









        private void StringByHalfReplase()
        {
            while (true)
            {
                var s = Console.ReadLine();
                //var result = s?.Substring(s.Length % 2 == 0 ? s.Length / 2 : s.Length / 2 + 1) + s?.Substring(0, s.Length % 2 == 0 ? s.Length / 2 : s.Length / 2 + 1);

                var result = new string(s?.Where((c, i) => i % 3 != 0).ToArray());

                Console.WriteLine(result);
            }
        }



        private void Run15()
        {
            var signal = new ManualResetEvent(false);
            var х = 0;

            new Thread(() =>
            {
                х++;
                signal.Set();
            })
            .Start();

            signal.WaitOne();

            Console.WriteLine(х); // 1 (всегда)
        }





        private async void Run14()
        {
            var src = new CancellationTokenSource();
            var toc = src.Token;

            var t = Task.Run(async () =>
            {
                await Task.Delay(5000);
                Console.WriteLine("OOps!");
                return 10;
            })
            .WithCancellation(toc);

            await Task.Run(async () =>
            {
                await Task.Delay(2000);
                src.Cancel();
                t.Dispose();
            });


            Console.WriteLine();
        }



        private async void Run13()
        {

            var tasks = Range(1, 5).Select(async i =>
            {
                var time = i * 1000;
                await Delay(time);
                return time / 1000.0;
            });

            //var result = await await WhenAny(tasks);
            var result = (await WhenAll(tasks)).Sum();

            Console.WriteLine(result);
        }



        private void Run12()
        {
            var sc = SynchronizationContext.Current;


            Console.WriteLine(sc?.ToString() ?? "null");

            var timer = new System.Timers.Timer(5000) { AutoReset = false };

            var tcs = new TaskCompletionSource<int>();

            new Thread(() =>
            {
                Thread.Sleep(2000);
                tcs.SetResult(42);
            })
            { IsBackground = true }
            .Start();

            var task = tcs.Task; // "Подчиненная" задача

            Console.WriteLine(task.Result); // 42

        }































        private void Run1()
        {
            try
            {
                throw null;
            }
            catch
            {

            }



            var nextPotentialPrime = 15;

            var locker = new object();
            long GetNextPotentialPrime()
            {
                var next = Interlocked.Add(ref nextPotentialPrime, 2);

                return next;
            }

            Console.WriteLine(GetNextPotentialPrime());

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
            //    Task.Run(async () =>
            //    {
            //        await Task.Yield();
            //        //Thread.Sleep(1000);
            //        //await Task.Delay(1000);
            //        Console.WriteLine(x);
            //    });
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
            Run(async () =>
            {
                //await Task.Yield();
                await Delay(1000);
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

            Run(() => throw new Exception("boom!"));

            Delay(1000).GetAwaiter().GetResult();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Run10()
        {
            Console.WriteLine("hi");

            var t = Run(() =>
            {
                for (var i = 0; i < 3; i++)
                    Console.WriteLine("i");
            });

            t.GetAwaiter().OnCompleted(() =>
            {
                Console.WriteLine("phase 2");
            });

            Console.WriteLine("bye");
        }


        private void Run11()
        {
            for (var i = 10; i < 41; i++)
            {
                var str = string.Format($"{{0, {i}}}", $"hello#{i}");
                //var str = string.Format(string.Format("{{0, {0}}}", i), string.Format("hello#{0}", i));
                Console.WriteLine(str);
            }
        }




    }
}
