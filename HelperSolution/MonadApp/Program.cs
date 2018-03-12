using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadApp
{
    internal interface IMonad<out T>
    {
        IMonad<TU> Bind<TU>(Func<T, IMonad<TU>> func);

        T Value { get; }
    }

    internal class Monad<T> : IMonad<T>
    {
        public T Value { get; }

        public Monad(T val) => Value = val;

        public IMonad<TU> Bind<TU>(Func<T, IMonad<TU>> func) 
            => Value == null ? new Monad<TU>(default(TU)) : func(Value);
    }


    internal class Program
    {
        private static void Main()
        {
            var val = new Monad<int>(5)
                    .Bind(v => new Monad<double>(v + 1.0))
                    .Bind(v => new Monad<object>(v * 2))
                    .Bind(v => new Monad<double>((double)v / 3.0))
                    .Bind(v => new Monad<int>((int)v + 2))
                    .Value;

            Console.WriteLine(val);
        }
    }
}
