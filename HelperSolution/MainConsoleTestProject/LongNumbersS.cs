using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    public static class LongNumbersS
    {
        public static void Run()
        {
            //todo
        }



        public class LongNumber
        {
            private readonly int[] _arr;
            public bool IsNegative { get; set; }

            #region Constructors

            public LongNumber()
            {
                _arr = new[] { 0 };
                IsNegative = false;
            }
            public LongNumber(string number)
            {
                if (string.IsNullOrWhiteSpace(number))
                    throw new ArgumentException(@"Empty number detected");
                if (number[0] == '-')
                {
                    IsNegative = true;
                    if (number.Length == 1)
                        throw new ArgumentException(@"Empty negative number detected");
                    number = number.Substring(1);
                }
                _arr = number
                    .Select((c, i) => int.TryParse(c.ToString(), out var n) ? n : throw new ArgumentException($@"Wrong symbol at {i} position"))
                    .ToArray();
            }
            public LongNumber(string number, bool isNegative = false)
            {
                if (string.IsNullOrWhiteSpace(number))
                    throw new ArgumentException(@"Empty number detected");
                _arr = number
                    .Select((c, i) => int.TryParse(c.ToString(), out var n) ? n : throw new ArgumentException($@"Wrong symbol at {i} position"))
                    .ToArray();
                IsNegative = isNegative;
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

            public override string ToString()
            {
                return _arr.Aggregate(new StringBuilder(), (sb, i) =>
                {
                    sb.Append(i);
                    return sb;
                }).ToString();
            }

            #region Operators

            public static LongNumber operator -(LongNumber left)
            {
                return new LongNumber(left) { IsNegative = !left.IsNegative };
            }
            public static LongNumber operator +(LongNumber left, LongNumber right)
            {
                return null;
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
        }


















        public class Digit
        {
            #region Static

            private static readonly Dictionary<string, int> StrToInt = new Dictionary<string, int>
                {
                    {"0", 0},{"1", 1},{"2", 2},{"3", 3},{"4", 4},{"5", 5},{"6", 6},{"7", 7},{"8", 8},{"9", 9}
                };
            private static readonly Dictionary<char, int> CharToInt = new Dictionary<char, int>
                {
                    {'0', 0},{'1', 1},{'2', 2},{'3', 3},{'4', 4},{'5', 5},{'6', 6},{'7', 7},{'8', 8},{'9', 9}
                };
            private static readonly Dictionary<int, string> IntToStr = new Dictionary<int, string>
                {
                    {0, "0"},{1, "1"},{2, "2"},{3, "3"},{4, "4"},{5, "5"},{6, "6"},{7, "7"},{8, "8"},{9, "9"}
                };
            private static readonly Dictionary<int, char> IntToChar = new Dictionary<int, char>
                {
                    {0, '0'},{1, '1'},{2, '2'},{3, '3'},{4, '4'},{5, '5'},{6, '6'},{7, '7'},{8, '8'},{9, '9'}
                };

            public static Digit FromChar(char c) => new Digit(CharToInt[c]);
            public static Digit FromString(string s) => new Digit(StrToInt[s]);
            public static char GetChar(Digit d) => d.Char;
            public static string GetString(Digit d) => d.String;

            #endregion

            #region Fields

            private int _value;

            #endregion

            #region Constructors

            public Digit() => _value = 0;
            public Digit(int value) => Value = value;
            public Digit(char value) => Char = value;
            public Digit(string value) => String = value;

            #endregion

            #region Properties

            public char Char
            {
                get => IntToChar[_value];
                set => _value = int.Parse(value.ToString());
            }

            public string String
            {
                get => IntToStr[_value];
                set => _value = int.Parse(value);
            }

            public int Value
            {
                get => _value;
                set
                {
                    if (value < 0 || value > 9)
                        throw new ArgumentException(@"Wrong number");
                    _value = value;
                }
            }

            #endregion
        }
    }
}
