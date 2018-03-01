using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflTest001
{

    internal class MyClass
    {
        public int Prop { get; set; }
    }

    internal class Program
    {
        private static void Main()
        {
            var cl = new MyClass();
            var obj = new object();
            obj = new ObjectDisposedException("hi");

            Console.WriteLine(obj.GetType().Name);


        }
    }
}
