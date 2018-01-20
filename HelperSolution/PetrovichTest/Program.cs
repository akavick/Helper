using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPetrovich;

namespace PetrovichTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Petrovich
            {
                FirstName = "Алекс",
                LastName = "Пушкин",
                MiddleName = "Погромович",
                Gender = Gender.Male
            };

            p.InflectFirstNameTo(Case.Genitive);

        }
    }
}
