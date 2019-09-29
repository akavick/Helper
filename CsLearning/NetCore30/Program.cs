using System;
using System.Text.Json;



namespace NetCore30
{
    class Program
    {
        private static void Main()
        {
            Index i1 = 3;  // number 3 from beginning
            Index i2 = ^4; // number 4 from end
            int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Console.WriteLine($"{a[i1]}, {a[i2]}"); // "3, 6"

            var slice = a[i1..i2]; // { 3, 4, 5 }

            Console.WriteLine($"{0.3 - 0.2}");
            Console.WriteLine($"{Math.Round(0.3 - 0.2, 1)}");
            Console.WriteLine($"{Math.BitIncrement(0.3 - 0.2)}");
            Console.WriteLine($"{Math.BitIncrement(0.0) == Double.Epsilon}");

            var instance = new
            {
                FirstName = "John",
                LastName = "Smith",
                Age = 18
            };

            Console.WriteLine(JsonSerializer.Serialize(instance));
        }




    }



    public class JsonPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public static JsonPerson Parse(string json) =>
            System.Text.Json.JsonSerializer.Deserialize<JsonPerson>(json);
    }
}
