using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExtensionsNs
{
    public static class Extensions
    {
        public static TOut With<TIn, TOut>(this TIn self, Func<TIn, TOut> func) 
            where TIn : class 
            where TOut : class
        {
            return self == null ? null : func(self);
        }
    }
}


namespace MainConsoleTestProject
{
    using ExtensionsNs;
    class A
    {
        public B B { get; set; }
    }

    class B
    {
        public C C { get; set; }
    }

    class C
    {
        public object O { get; set; }
    }
    public class Tricks
    {

        public void Run()
        {
            var a = new A {B = new B()};
            var r = a
                .With(x => x.B)
                .With(x => x.C)
                .With(x => x.O)
                .With(x => x.GetType());
            
            Console.WriteLine(r);
        }
    }
}
