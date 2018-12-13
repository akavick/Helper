using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;



namespace CsLearningLibrary
{



    public class ImmutableCollections : ILesson
    {

        public async Task Run()
        {
            var cities = new List<string> { "Moscow, Minsk" };
            var immutableCities = cities.ToImmutableList();
            var newList = immutableCities.Add("Rio");

            Console.Write("Immutable cities:");

            foreach (var city in immutableCities)
            {
                Console.Write($" {city}");
            }

            Console.WriteLine();
            Console.Write("New cities:");

            foreach (var city in newList)
            {
                Console.Write($" {city}");
            }

            Console.WriteLine();
        }
    }



}