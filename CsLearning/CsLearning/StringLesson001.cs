using System;



namespace CsLearning
{

    public static class StringExtensions
    {
        public static bool IsNullOrWhitespace(this string str) => str == null || string.IsNullOrWhiteSpace(str);

        public static bool IsNullOrEmpty(this string str) => str == null || string.IsNullOrEmpty(str);
    }



    public class StringLesson001 : ILesson
    {
        public void Run()
        {
            string str = null;

            Console.WriteLine($"{str.IsNullOrWhitespace()}");
        }
    }

}