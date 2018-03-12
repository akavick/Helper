using System;
using System.Globalization;

namespace MainConsoleTestProject
{
    internal class ChainFunction
    {
        public void Run()
        {
            //var value1 = Sum(3)(2)(1)();
            //var value2 = Sum(3)(3)(1)(0)();
            var value3 = Sum(3)(2)(0)(20)(100)()()(5);
            var value4 = Sum2(1)(0)(4)(15)();
            //var value5 = Sum(0)(0)();
            //var value6 = Sum(0)();
            var value7 = Sum2();

            //Print(value1);
            //Print(value2);
            Print(value3);
            Print(value4);
            //Print(value5);
            //Print(value6);
            Print(value7);

        }

        private static void Print(object value)
        {
            switch (value)
            {
                case null:
                    value = "null";
                    break;
                case SumDeleg v:
                    value = v(null, true);
                    break;
            }

            Console.WriteLine(value);
        }


        #region Extended

        internal delegate dynamic SumDeleg(int? x = null, bool stop = false);
        private static dynamic Sum(int? value = null)
        {
            dynamic InnerFun(int? val = null, bool stop = false)
            {
                value += val ?? 0;
                return stop ? (dynamic)value : (SumDeleg)InnerFun;
            }

            return value is null ? null : (SumDeleg)InnerFun;
        }

        #endregion


        #region Classic

        private delegate dynamic SumDeleg2(int? x = null);
        private static dynamic Sum2(int? value = null)
        {
            dynamic InnerFun(int? val = null)
            {
                value += val ?? 0;
                return val is null ? (dynamic)value : (SumDeleg2)InnerFun;
            }

            return value is null ? null : (SumDeleg2)InnerFun;
        }

        #endregion
    }
}