using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class Ref: ILesson
    {

        public async Task Run()
        {
            //var x = 5;
            //ref var xRef = ref x; // только не в async

            RunSync();
        }




        private void RunSync()
        {
            ref int tenUp(ref int pointer)
            {
                pointer += 10;

                return ref pointer;
            }

            var range = Enumerable.Range(1, 10)
                                  .ToArray();

            for (var index = 0; index < range.Length; index++)
            {
                ref var x = ref tenUp(ref range[index]);
            }

            foreach (var i in range)
            {
                Console.Write($"{i} ");
            }
        }

    }

}