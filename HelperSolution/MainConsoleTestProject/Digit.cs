using System;
using System.Collections.Generic;

namespace MainConsoleTestProject
{

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
