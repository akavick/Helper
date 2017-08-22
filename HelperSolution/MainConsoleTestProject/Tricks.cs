using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MainConsoleTestProject
{
    #region NullTest

    using ExtensionsNs;

    namespace ExtensionsNs
    {
        public static class Extensions
        {
            public static TOut With<TIn, TOut>(this TIn self, Func<TIn, TOut> func)
                where TIn : class
                where TOut : class
                => self == null ? null : func(self);

            public static void Alert<T>(this T self)
                where T : class
            {
                if (self == null)
                    Console.WriteLine("object is null");
            }

        }
    }

    internal class A
    {
        public B B { get; set; }
    }

    internal class B
    {
        public C C { get; set; }
    }

    internal class C
    {
        public object O { get; set; }
    }

    #endregion



    public class Tricks
    {
        #region NullTest

        private void NullTest()
        {
            var a = new A { B = new B() };
            var r = a
                .With(x => x.B)
                .With(x => x.C) // не вылетит
                .With(x => x.O)
                .With(x => x.GetType());

            Console.WriteLine(r);

            object obj = null;
            obj.Alert(); // всё равно не вылетит
        }

        #endregion



        public void Run()
        {
            //NullTest();
            MathTest001();





        }

        private interface ICla { }

        private struct Str : ICla { }



        #region Math

        private void MathTest001()
        {
            //var r = new Random();
            //const int n = 100000000;
            //var arr1 = new int[n];
            //var arr2 = new int[n];
            //for (var i = 0; i < n; i++)
            //{
            //    arr1[i] = r.Next();
            //    arr2[i] = r.Next();
            //}

            var sw = new Stopwatch();
            sw.Start();

            //for (var i = 0; i < n; i++)
            //{
            //    //var v = Min1(arr1[i], arr2[i]);
            //    //var v = Min2(arr1[i], arr2[i]);
            //    //var v = Min3(arr1[i], arr2[i]);
            //    //var v = Math.Min(arr1[i], arr2[i]);
            //    //var v = arr1[i] < arr2[i] ? arr1[i] : arr2[i];
            //}

            //for (var i = 0; i < n; i++)
            //{
            //    //var v = arr1[i] / 3;
            //    var v = (int)((arr1[i] * 0xAAAAAAAB) >> 33);
            //}

            //int cnt = 0;
            //for (double i = 10000000000000000.0; i < 10000000000000002.0; i++) // нужен алгоритм кэхана
            //{
            //    cnt++;
            //    if (cnt > 99)
            //    {
            //        Console.WriteLine("Warning!");
            //        break;
            //    }
            //}

            //раскрутка цикла : arr[i]+arr[i+1]+arr[i+2]+arr[i+3]

            //var a = Math.Round(0.5);
            //var b = Math.Round(1.5);
            //var c = Math.Round(0.5, MidpointRounding.AwayFromZero);
            //var d = Math.Round(1.5, MidpointRounding.AwayFromZero);
            //Console.WriteLine(a + " " + b + " " + c + " " + d); // использовать перегузку с MidpointRounding.AwayFromZero !!!!!!!!
            //Console.WriteLine(false | null);

            int[] arr = { 4, 3, 7, 1, 0, 4, 8 };
            Console.WriteLine(arr.Sum());
            Console.WriteLine(SumArrGt(arr, 4));


            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        private int Min1(int a, int b) => a < b ? a : b;
        private int Min2(int a, int b) => a & ((a - b) >> 31) | b & (~(a - b) >> 31); //nuts
        private int Min3(int a, int b) => Math.Min(a, b);



        private int SumArrGt(int[] arr, int gt)
        {
            int sum = 0;
            int n = arr.Length;
            for (int i = 0;i < n;i++)
            {
                int t = (arr[i] - gt) >> 31;
                sum += ~t & arr[i];
            }

            return sum;
        }

        #endregion











    }
}
