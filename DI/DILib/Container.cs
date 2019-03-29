using System;

namespace DILib
{
    public class Container
    {
        
        
        
        
        public void RegisterSingleton<T, TImpl>()
        {

        }

        public void Register<T, TImpl>()
        {

        }

        public T Resolve<T>(T t)
        {
            return t;
        }
    }
}
