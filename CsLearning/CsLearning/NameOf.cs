using System;
using System.Threading.Tasks;



namespace CsLearning
{

    public class NameOf : ILesson
    {
        public async Task Run()
        {
            var heightInMetres = 1.88;

            Console.WriteLine($"The variable {nameof(heightInMetres)} has the value {heightInMetres}.");

            Console.WriteLine($"{nameof(DoSomething)}");
        }



        private void DoSomething()
        {

        }
    }

}