using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Newtonsoft.Json;



namespace CsLearningLibrary
{
    public class MyJsonSerializableClass
    {
        public string Title { get; set; }
        public DateTime DateOfCreation { get; set; }
        public HashSet<MyJsonSerializableClass> Children { get; set; }
        public decimal Value { get; set; }
    }



    public class JsonSerialization : ILesson
    {

        public async Task Run()
        {
            var list = await GetListAsync();
            var path = Path.Combine(Environment.CurrentDirectory, "list.json");

            using (var stream = File.CreateText(path))
            {
                var js = new JsonSerializer();
                js.Serialize(stream, list);
            }
            
            Console.WriteLine($"Written {new FileInfo(path).Length} bytes of JSON to {path}");
            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(path));
            Console.WriteLine();
            Console.WriteLine();

            using (var stream = File.OpenText(path))
            {
                var js = new JsonSerializer();
                var loadedList = (List<MyJsonSerializableClass>)js.Deserialize(stream, typeof(List<MyJsonSerializableClass>));

                foreach (var item in loadedList)
                {
                    Console.WriteLine($"{item.Title} has {item.Children?.Count ?? 0} children.");
                }
            }
        }



        private async Task<List<MyJsonSerializableClass>> GetListAsync()
        {
            await Task.CompletedTask;

            return new List<MyJsonSerializableClass>
            {
                new MyJsonSerializableClass
                {
                    Title = "Alice",
                    DateOfCreation = new DateTime(1974, 3, 14),
                    Value = 1.0m
                },
                new MyJsonSerializableClass
                {
                    Title = "Bob",
                    DateOfCreation = new DateTime(1969, 11, 23),
                    Value = 4.0m
                },
                new MyJsonSerializableClass
                {
                    Title = "Charlie",
                    DateOfCreation = new DateTime(1964, 5, 4),
                    Children = new HashSet<MyJsonSerializableClass>
                    {
                        new MyJsonSerializableClass
                        {
                            Title = "Sally",
                            DateOfCreation = new DateTime(1990, 7, 12),
                            Value = 14.4m
                        }
                    },
                    Value = 21.0m
                }
            };
        }

    }

}