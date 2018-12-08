using System;
using System.Linq;
using System.Threading.Tasks;



namespace CsLearning
{

    public class FizzBuzz : ILesson
    {

        public async Task Run()
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