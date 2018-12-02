using System;



namespace CsLearning
{

    public class ToBase64 : ILesson
    {

        public void Run()
        {
            /*
             При необходимости сохранить или передать бинарный объект лучше не отправлять 
             его в виде необработанных битов, ведь вы не знаете, какие ошибки могут возникнуть 
             при интерпретации битов, например, сетевым протоколом, ответственным за их передачу, 
             или другой операционной системой, считывающей и сохраняющей объект.
             Самый безопасный способ — конвертировать бинарный объект в строку безопасных символов. 
             Программисты называют этот подход кодировкой Base64.
             Для типа Convert реализовано два метода: ToBase64String и FromBase64String, 
             которые выполнят конвертацию за вас.
             */

            // выделить массив из 128 байт
            var binaryObject = new byte[128];
            // заполнить массив случайными байтами
            new Random().NextBytes(binaryObject);

            Console.WriteLine($"Binary Object as bytes:{Environment.NewLine}");

            foreach (var b in binaryObject)
            {
                Console.Write($"{b:D3} ");
            }

            Console.WriteLine(Environment.NewLine);

            foreach (var b in binaryObject)
            {
                Console.Write($"{b:X2} ");
            }

            Console.WriteLine(Environment.NewLine);

            foreach (var b in binaryObject)
            {
                Console.Write($"{Convert.ToString(b, 2).PadLeft(8, '0')} ");
            }

            Console.WriteLine(Environment.NewLine);

            // преобразовать в строку Base64
            var encoded = Convert.ToBase64String(binaryObject);

            Console.WriteLine($"Binary Object as Base64:{Environment.NewLine}{Environment.NewLine}{encoded}");
        }
    }

}