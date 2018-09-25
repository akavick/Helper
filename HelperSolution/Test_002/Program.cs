using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test_002
{
    internal class Program
    {
        private string _value = "";
        private readonly Func<int, string> _func;



        public Program() => _func = i => $"{_value}-{i}";



        public dynamic Value
        {
            get => _func;
            set => _value = value as string ?? throw new ArgumentException("value must have type of string");
        }



        private static void Main()
        {
            //var prog = new Program { Value = "hello" };
            //var val = prog.Value(20);

            //Console.WriteLine(val);

            //var n = new Number();
            //Console.WriteLine(n.DigitalRoot(456));

            Console.WriteLine(SpinWords("Hey wollef sroirraw"));
        }


        public static string SpinWords(string sentence)
        {
            //return 
            //    sentence.Split(' ')
            //            .Select((s, i) => (i == 0 ? "" : " ") + (s.Length > 4 ? $"{string.Concat(str.Reverse())}": $"{s}"))
            //            .Aggregate(new StringBuilder(), (sb, str) => sb.Append(str))
            //            .ToString();

            return Regex.Replace(sentence, @"\b\S{5,}\b", x => string.Concat(x.Value.Reverse()));

            return string.Join(" ", sentence.Split(' ')
                                            .Select(str => str.Length >= 5 ? string.Concat(str.Reverse()) : str));
        }
    }



    public class Number
    {
        public int DigitalRoot(long n)
        {
            if (n / 10 == 0)
            {
                return (int)n;
            }

            long sum = 0;

            while (n / 10 != 0)
            {
                sum += n % 10;
                n /= 10;
            }

            return DigitalRoot(sum + n);
        }
    }
}
