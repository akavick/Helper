using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    public static class LongNumbersS
    {
        public static void Run()
        {
            //var list = new List<int> {1, 2, 3, 4, 5};
            //list.InsertRange(1, list.GetRange(2, 2));
            //foreach (var i in list)
            //    Console.Write(i + " ");

            var x = default(int);
            Console.WriteLine(x);

            ////todo
            ////var ln1 = new LongNumber("915978976987696791597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099689678687967909");
            ////var ln2 = new LongNumber("9191597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790991597897698769679689678687967909915978976987696796896786879679099159789769876967968967868796790909");

            //var ln1 = new LongNumber("18000");
            //var ln2 = new LongNumber("9000");


            //var sw = new Stopwatch();
            //sw.Start();

            //var result = ln1 + ln2;

            //sw.Stop();
            //Console.WriteLine(sw.Elapsed);
            //Console.WriteLine(result);
        }



        public class LongNumber
        {
            private int[] _arr;
            public bool IsNegative { get; set; }

            public override string ToString() => _arr.Aggregate(new StringBuilder(), (sb, i) =>
            {
                sb.Append(i);
                return sb;
            }).ToString();

            public void SetDefault()
            {
                _arr = new[] { 0 };
                IsNegative = false;
            }

            #region Constructors



            public LongNumber() => SetDefault();



            public LongNumber(string number, bool isNegative = false)
            {
                if (String.IsNullOrWhiteSpace(number))
                    throw new ArgumentException(@"Empty number detected");
                IsNegative = isNegative;
                _arr = number
                    .Select((c, i) => Int32.TryParse(c.ToString(), out var n) ? n : throw new ArgumentException($@"Wrong symbol at {i} position"))
                    .SkipWhile(val => val == 0)
                    .ToArray();
                if (_arr.Length == 0)
                    SetDefault();
            }



            public LongNumber(IReadOnlyList<int> arr, bool isNegative = false)
            {
                if (arr.Count == 0)
                    throw new ArgumentException(@"Empty number detected");
                _arr = new int[arr.Count];
                for (var i = 0; i < arr.Count; i++)
                {
                    if (arr[i] < 0 || arr[i] > 9)
                        throw new ArgumentException($@"Wrong number at {i} position");
                    _arr[i] = arr[i];
                }
                IsNegative = isNegative;
            }



            public LongNumber(LongNumber other)
            {
                IsNegative = other.IsNegative;
                _arr = new int[other._arr.Length];
                for (var i = 0; i < _arr.Length; i++)
                    _arr[i] = other._arr[i];
            }



            #endregion

            #region Operators



            public static bool operator <(LongNumber left, LongNumber right)
            {
                if (ReferenceEquals(left, right))
                    return false;
                //доделать негативы
                if (left._arr.Length != right._arr.Length)
                    return left._arr.Length < right._arr.Length;

                for (var i = 0; i < left._arr.Length; i++)
                {
                    if (left._arr[i] != right._arr[i])
                        return left._arr[i] < right._arr[i];
                }

                return false;
            }



            public static bool operator >(LongNumber left, LongNumber right)
            {
                return true;
            }



            public static bool operator <=(LongNumber left, LongNumber right)
            {
                return true;
            }



            public static bool operator >=(LongNumber left, LongNumber right)
            {
                return true;
            }



            public static bool operator ==(LongNumber left, LongNumber right)
            {
                return true;
            }



            public static bool operator !=(LongNumber left, LongNumber right)
            {
                return true;
            }



            public static LongNumber operator -(LongNumber left) => new LongNumber(left) { IsNegative = !left.IsNegative };



            public static LongNumber operator +(LongNumber left, LongNumber right)
            {
                //доделать негативы
                if (left._arr.Length > right._arr.Length)
                    Swap(ref left, ref right);

                var leftIndex = left._arr.Length - 1;
                var rightIndex = right._arr.Length - 1;
                var memory = false;
                var result = new List<int>();

                while (rightIndex >= 0)
                {
                    var rightDigit = right._arr[rightIndex];
                    if (memory)
                        rightDigit++;
                    var leftDigit = leftIndex >= 0 ? left._arr[leftIndex] : 0;

                    var resultDigit = leftDigit + rightDigit;
                    memory = resultDigit > 9;
                    if (memory)
                        resultDigit -= 10;
                    result.Add(resultDigit);
                    leftIndex--;
                    rightIndex--;
                }

                if (memory)
                    result.Add(1);

                result.Reverse();
                return new LongNumber(result);
            }



            public static LongNumber operator -(LongNumber left, LongNumber right)
            {
                return null;
            }



            public static LongNumber operator *(LongNumber left, LongNumber right)
            {
                return null;
            }




            public static LongNumber operator /(LongNumber left, LongNumber right)
            {
                return null;
            }



            #endregion

            #region Helpers



            private static void Swap<T>(ref T left, ref T right)
            {
                var temp = left;
                left = right;
                right = temp;
            }



            #endregion
        }
    }
}
