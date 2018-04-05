using System;
using System.Linq;

namespace MontyHall
{
    internal class Door
    {
        public bool Value { get; set; }
        public bool Opened { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            const int iterations = 10000;
            var r = new Random();
            var result = Enumerable.Range(1, iterations)
                                   .Select(i =>
                                   {
                                       const int cnt = 1000;
                                       var doors = Enumerable.Range(0, cnt)
                                                             .Select(index => new Door())
                                                             .ToList();
                                       doors[r.Next(cnt)].Value = true;
                                       var choice = doors[r.Next(cnt)];
                                       doors.Where(d => d.Value == false && d != choice)
                                            .ToList()
                                            .ForEach(d => d.Opened = true);
                                       if (choice.Value)
                                       {
                                           doors.Where(d => d.Value == false)
                                                .ElementAt(r.Next(cnt - 1)).Opened = false;
                                       }

                                       var c = choice;
                                       choice = doors.Single(d => d.Opened == false && d != c);
                                       return choice.Value;
                                   })
                                   .Count(val => val) * 1.0m / iterations;
            Console.WriteLine(result * 100);
        }
    }
}
