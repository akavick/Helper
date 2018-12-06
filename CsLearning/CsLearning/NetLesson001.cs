using System;
using System.Net;
using static System.Console;



namespace CsLearning
{



    public class NetLesson001 : ILesson
    {

        public void Run()
        {
            var uri = new Uri("http://world.episerver.com/cms/?q=pagetype");

            WriteLine($"Scheme: {uri.Scheme}");
            WriteLine($"Port: {uri.Port}");
            WriteLine($"Host: {uri.Host}");
            WriteLine($"Path: {uri.AbsolutePath}");
            WriteLine($"Query: {uri.Query}");
        }
    }



}