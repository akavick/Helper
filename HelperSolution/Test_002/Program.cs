using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_002
{
    internal class Program
    {
        private string _value = "";
        private readonly Func<int, string> _func;



        public Program() => _func = i => $"{_value}-{i}";



        public dynamic Value
        {
            get => _func;
            set => _value = value as string ?? throw new ArgumentException("value must have type of string");
        }



        private static void Main()
        {
            var prog = new Program { Value = "hello" };
            var val = prog.Value(20);

            Console.WriteLine(val);
        }
    }
}
