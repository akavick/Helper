using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;



namespace CsLearningLibrary
{
    public class MyXmlSerializableClass
    {
        [XmlAttribute("Name")]
        public string Title { get; set; }
        [XmlElement("Date")]
        public DateTime DateOfCreation { get; set; }
        public HashSet<MyXmlSerializableClass> Children { get; set; }
        public decimal Value { get; set; }
    }



    public class XmlSerialization : ILesson
    {

        public async Task Run()
        {
            var list = await GetListAsync();
            var path = Path.Combine(Environment.CurrentDirectory, "list.xml");

            using (var stream = File.Create(path))
            {
                var xs = new XmlSerializer(typeof(List<MyXmlSerializableClass>));
                xs.Serialize(stream, list);
            }

            Console.WriteLine($"Written {new FileInfo(path).Length} bytes of XML to {path}");
            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(path));
            Console.WriteLine();
            Console.WriteLine();

            using (var stream = File.Open(path, FileMode.Open))
            {
                var xs = new XmlSerializer(typeof(List<MyXmlSerializableClass>));
                var loadedList = (List<MyXmlSerializableClass>)xs.Deserialize(stream);

                foreach (var item in loadedList)
                {
                    Console.WriteLine($"{item.Title} has {item.Children.Count} children.");
                }
            }
        }



        private async Task<List<MyXmlSerializableClass>> GetListAsync()
        {
            await Task.CompletedTask;

            return new List<MyXmlSerializableClass>
            {
                new MyXmlSerializableClass
                {
                    Title = "Alice",
                    DateOfCreation = new DateTime(1974, 3, 14),
                    Value = 1.0m
                },
                new MyXmlSerializableClass
                {
                    Title = "Bob",
                    DateOfCreation = new DateTime(1969, 11, 23),
                    Value = 4.0m
                },
                new MyXmlSerializableClass
                {
                    Title = "Charlie",
                    DateOfCreation = new DateTime(1964, 5, 4),
                    Children = new HashSet<MyXmlSerializableClass>
                    {
                        new MyXmlSerializableClass
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