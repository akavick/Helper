using System;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    internal class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }



        public void Deconstruct(out string name, out int age)
        {
            name = this.Name;
            age = this.Age;
        }
    }



    public class Deconstruction : ILesson
    {

        public async Task Run()
        {
            var person = new Person
            {
                Name = "Tom",
                Age = 33
            };

            var (name, age) = person;

            Console.WriteLine(name); // Tom
            Console.WriteLine(age);  // 33
        }

    }

}