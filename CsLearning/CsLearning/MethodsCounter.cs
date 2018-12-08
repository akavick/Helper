using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;



namespace CsLearning
{

    public class MethodsCounter : ILesson
    {
        public async Task Run()
        {
            // перебор сборок, на которые ссылается приложение
            foreach (var r in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                // загрузка сборки для чтения данных
                var a = Assembly.Load(new AssemblyName(r.FullName));

                // перебор всех типов в сборке
                // объявление переменной для подсчета методов
                var methodCount = a.DefinedTypes.Sum(t => t.GetMethods().Length);

                // вывод количества типов и их методов
                Console.WriteLine($"{a.DefinedTypes.Count():N0} types " +
                                  $"with {methodCount:N0} methods in {r.Name} assembly.");
            }
        }
    }

}