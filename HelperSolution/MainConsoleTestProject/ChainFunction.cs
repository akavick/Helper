using System;

namespace MainConsoleTestProject
{
    internal class ChainFunction
    {
        public void Run()
        {
            var value1 = Sum(3)(2)(1)();
            var value2 = Sum(3)(3)(1)(0)();
            var value3 = Sum(0)(2)(0)(20)(100)();
            var value4 = Sum(1)(0)(4)(15)();
            var value5 = Sum(0)(0)();
            var value6 = Sum(0)();
            var value7 = Sum();

            Print(value1);
            Print(value2);
            Print(value3);
            Print(value4);
            Print(value5);
            Print(value6);
            Print(value7);
        }

        private static void Print(object value)
        {
            Console.WriteLine(value ?? "null");
        }


        private delegate dynamic SumDeleg(int? x = null);

        private static dynamic Sum(int? value = null)
        {
            if (value == null)
                return null;

            dynamic InnerFun(int? val = null)
            {
                if (val == null)
                    return value;
                value += val;
                return (SumDeleg)InnerFun;
            }

            return (SumDeleg)InnerFun;
        }



    }
}