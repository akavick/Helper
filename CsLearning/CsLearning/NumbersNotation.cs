using System;



namespace CsLearning
{

    public class NumbersNotation : ILesson
    {

        public void Run()
        {
            var decimalNotation = 2_000_000;                                // два миллиона
            var binaryNotation = 0b0001_1110_1000_0100_1000_0000;           // два миллиона
            var hexadecimalNotation = 0x001E_8480;                          // два миллиона
            Console.WriteLine($"{decimalNotation == binaryNotation}");      // => true
            Console.WriteLine($"{decimalNotation == hexadecimalNotation}"); // => true
        }
    }

}