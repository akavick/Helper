using System;
using System.IO;



namespace CsLearning
{

    public class PatternMatching : ILesson
    {

        public void Run()
        {
            object o = "3";
            int j = 4;

            if (o is int i)
            {
                Console.WriteLine($"{i} x {j} = {i * j}");
            }
            else
            {
                Console.WriteLine("o is not an int so it cannot multiply!");
            }



            // string path = "/Users/markjprice/Code/Chapter03"; // macOS
            string path = @"C:\Code\Chapter03"; // Windows
            Stream s = File.Open(Path.Combine(path, "file.txt"), FileMode.OpenOrCreate);

            switch (s)
            {
                case FileStream writeableFile when s.CanWrite:
                    Console.WriteLine("The stream is to a file that I can write to.");

                    break;
                case FileStream readOnlyFile:
                    Console.WriteLine("The stream is to a read-only file.");

                    break;
                case MemoryStream ms:
                    Console.WriteLine("The stream is to a memory address.");

                    break;
                default: // всегда оценивается последним,

                    // несмотря на его текущее положение
                    Console.WriteLine("The stream is some other type.");

                    break;
                case null:
                    Console.WriteLine("The stream is null.");

                    break;
            }

        }
    }

}