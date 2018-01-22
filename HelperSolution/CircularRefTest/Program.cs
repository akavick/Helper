using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularRefTest
{
    internal class A
    {
        private int[] _arr = new int[10000000];
        private B _b;

        public B B
        {
            get => _b;
            set => _b = value;
        }

        public int[] Arr
        {
            get => _arr;
            set => _arr = value;
        }
    }

    internal class B
    {
        private int[] _arr = new int[10000000];
        private A _a;

        public A A
        {
            get => _a;
            set => _a = value;
        }

        public int[] Arr
        {
            get => _arr;
            set => _arr = value;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                var a = new A();
                var b = new B();
                a.B = b;
                b.A = a;
            }
        }
    }
}
