﻿using System;
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
    internal static class TaskExtensions
    {
        internal static async Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            var winner = await WhenAny(task, Delay(timeout));
            if (winner != task)
                throw new TimeoutException();
            return await task; // Извлечь результат или повторно сгенерировать исключение
        }

        internal static Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken cancelToken)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var reg = cancelToken.Register(() => tcs.TrySetCanceled());
            task.ContinueWith(ant =>
            {
                reg.Dispose();
                if (ant.IsCanceled)
                    tcs.TrySetCanceled();
                else if (ant.IsFaulted)
                    tcs.TrySetException(ant.Exception.InnerException);
                else
                    tcs.TrySetResult(ant.Result);
            });
            return tcs.Task;
        }

        internal static async Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {
            var killJoy = new TaskCompletionSource<TResult[]>();
            foreach (var task in tasks)
                task.ContinueWith(ant =>
                {
                    if (ant.IsCanceled)
                        killJoy.TrySetCanceled();
                    else if (ant.IsFaulted)
                        killJoy.TrySetException(ant.Exception.InnerException);
                });
            return await await Task.WhenAny(killJoy.Task, Task.WhenAll(tasks));
        }

    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().Run14();
            Console.ReadKey(true);
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
                for (var i = 0;i < 3;i++)
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
            for (var i = 10;i < 41;i++)
            {
                var str = string.Format($"{{0, {i}}}", $"hello#{i}");
                //var str = string.Format(string.Format("{{0, {0}}}", i), string.Format("hello#{0}", i));
                Console.WriteLine(str);
            }
        }
    }
}
