using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    class TestStaticGcClass : IDisposable
    {
        private static readonly object Obj = new object();
        private object _object;

        public TestStaticGcClass()
        {
            _object = Obj;
        }

        public void Dispose()
        {
            _object = null;
        }
    }

    class StaticGcTest
    {

        public void Run()
        {
            var arr = new List<TestStaticGcClass>();

            for (int i = 0; i < 50000000; i++)
            {
                arr.Add(new TestStaticGcClass());
            }

            arr = null;

            //foreach (var v in arr)
            //{
            //    v.Dispose();
            //}

            Console.WriteLine("end");
            GC.Collect();
        }
    }
}
