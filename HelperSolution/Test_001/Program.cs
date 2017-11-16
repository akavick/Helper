using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_001
{
    internal sealed class Int
    {
        private int _value;

        public int MyProperty
        {
            get => _value;
            set => _value = value;
        }

        private Int(int value) => _value = value;


        public static implicit operator int(Int i) => i._value;
        public static implicit operator Int(int i) => new Int(i);
        public static implicit operator string(Int i) => i._value.ToString();

        public static implicit operator Func<Int, string>(Int i) => x => x._value.ToString();
    }

    internal class Program
    {
        private Int MyProperty { get; set; } = 0;

        private static void Main()
        {
            var prog = new Program();
            var x = ((Func<Int, string>)prog.MyProperty)(20);
            Console.WriteLine(x);
        }
    }

}
