using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace CsLearningLibrary
{
    public class IEnumerableLesson001 : ILesson
    {
        public async Task Run()
        {
            var iEnumerable = GetIEnumerable().Where(i => i > 1).Select(i => i);

            Console.WriteLine(iEnumerable);


        }



        IEnumerable<int> GetIEnumerable()
        {
            Console.WriteLine("METHOD WAS EXECUTED");

            var array = new[] {1, 2, 3};

            return array;
        }




    }
}
