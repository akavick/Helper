using System;
using System.Threading.Tasks;



namespace CsLearningLibrary
{

    public class NumbersNotation : ILesson
    {

        public async Task Run()
        {
            var decimalNotation = 2_000_000;                                // два миллиона
            var binaryNotation = 0b0001_1110_1000_0100_1000_0000;           // два миллиона
            var hexadecimalNotation = 0x001E_8480;                          // два миллиона
            Console.WriteLine($"{decimalNotation == binaryNotation}");      // => true
            Console.WriteLine($"{decimalNotation == hexadecimalNotation}"); // => true

            Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}.");
            Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range {double.MinValue:N0} to {double.MaxValue:N0}.");
            Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range {decimal.MinValue:N0} to {decimal.MaxValue:N0}.");
        }
    }

}