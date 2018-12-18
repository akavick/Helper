using System;
using System.Threading.Tasks;



namespace CsLearningLibrary
{



    public class A
    {
        public void Execute(string message)
        {
            Console.WriteLine(message);
        }
    }



    public static class AExtensions
    {
        public static void Do(this A a)
        {
            Console.WriteLine("public static void Do(this A a)");
            a.Do("hello"); // здесь не вылетит исключение
        }
        public static void Do(this A a, string message)
        {
            Console.WriteLine("public static void Do(this A a, string message)");
            a.Execute(message);
        }
    }



    public class ExtensionsLesson001 : ILesson
    {

        public async Task Run()
        {
            A a = null;
            a.Do();
        }




    }

}