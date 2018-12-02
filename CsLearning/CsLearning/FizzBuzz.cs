using System;
using System.Linq;



namespace CsLearning
{

    public class FizzBuzz : ILesson
    {

        public void Run()
        {
            Enumerable.Range(1, 100)
                      .ToList()
                      .ForEach(n =>
                      {
                          var canBeDividedByThree = n % 3 == 0;
                          var canBeDividedByFive = n % 5 == 0;
                          var output = "";

                          if (!canBeDividedByThree && !canBeDividedByFive)
                          {
                              output = $"{n}";
                          }
                          else
                          {
                              if (canBeDividedByThree)
                              {
                                  output = "fizz";
                              }

                              if (canBeDividedByFive)
                              {
                                  output += "buzz";
                              }
                          }

                          Console.Write($"{output} ");
                      });
        }
    }

}