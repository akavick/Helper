using System;
using System.Linq;

namespace MontyHall
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int iterations = 10000;
            var r = new Random();
            var result1 = Enumerable
                         .Range(1, iterations)
                         .Select(i =>
                         {
                             const int cnt = 1000;
                             var prizeDoorNumber = r.Next(cnt);
                             var selectedDoorNumber = r.Next(cnt);
                             var doors = Enumerable
                                         .Range(0, cnt)
                                         .Select(index => (Value: index == prizeDoorNumber, Opened: false, Number: index))
                                         .ToArray();
                             var closedUnselectedDoors = doors.Where(door => door.Value == false && door.Number != doors[selectedDoorNumber].Number);

                             foreach (var door in closedUnselectedDoors)
                             {
                                 doors[door.Number].Opened = true;
                             }

                             if (doors[selectedDoorNumber].Value)
                             {
                                 var loseDoorsNumbers = doors.Where(d => d.Value == false)
                                                             .Select(d => d.Number)
                                                             .ToArray();

                                 doors[r.Next(loseDoorsNumbers.Length)].Opened = false;
                             }

                             var choice = doors.Single(door => door.Opened == false && door.Number != doors[selectedDoorNumber].Number);

                             return choice.Value;
                         })
                         .Count(val => val) * 1.0m / iterations;

            Console.WriteLine(result1 * 100);
        }
    }
}
