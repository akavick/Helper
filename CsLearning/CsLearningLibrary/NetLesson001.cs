using System;
using System.Threading.Tasks;



namespace CsLearningLibrary
{



    public class NetLesson001 : ILesson
    {

        public async Task Run()
        {
            var uri = new Uri("http://world.episerver.com/cms/?q=pagetype");

            Console.WriteLine($"Scheme: {uri.Scheme}");
            Console.WriteLine($"Port: {uri.Port}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"Path: {uri.AbsolutePath}");
            Console.WriteLine($"Query: {uri.Query}");
        }
    }



}