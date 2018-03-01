using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyMorph
{
    abstract class A
    {
        public abstract void M();
    }

    class B : A
    {
        public override void M()
        {
            Console.WriteLine("B");
        }
    }

    class C : B
    {
        public override void M()
        {
            Console.WriteLine("C");
        }
    }

    class D : C
    {
        public override void M()
        {
            base.M();
            Console.WriteLine("D");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A obj1 = new B();
            A obj2 = new C();
            B obj3 = new C();
            A obj4 = new D();
            var list = new [] { obj1, obj2, obj3, obj4 };
            foreach (var a in list)
            {
                a.M();
            }
        }
    }
}
