using System;
using System.Threading.Tasks;



namespace CsLearningLibrary
{



    internal interface IMonad<out T>
    {
        IMonad<TU> Bind<TU>(Func<T, IMonad<TU>> func);

        T Value { get; }
    }



    internal class Monad<T> : IMonad<T>
    {
        public T Value { get; }



        public Monad(T val)
        {
            Value = val;
        }



        public IMonad<TU> Bind<TU>(Func<T, IMonad<TU>> func)
        {
            return 
                Value == null
                    ? new Monad<TU>(default) 
                    : func(Value);
        }
    }



    public class MonadLesson001 : ILesson
    {

        public async Task Run()
        {
            var val = 
                new Monad<int>(5)
                    .Bind(v => new Monad<double>(v + 1.0))
                    .Bind(v => new Monad<object>(v * 2))
                    .Bind(v => new Monad<double>((double) v / 3.0))
                    .Bind(v => new Monad<int>((int) v + 2))
                    .Value;

            Console.WriteLine(val);
        }
    }



}