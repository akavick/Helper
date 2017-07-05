using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsoleTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int side = 9;

            bool F01(int n, int i, int j) => i >= j;
            bool F02(int n, int i, int j) => j < n - i;
            bool F03(int n, int i, int j) => i <= j;
            bool F04(int n, int i, int j) => j >= n - i - 1;

            bool F05(int n, int i, int j) => i <= j && j < n - i;
            bool F06(int n, int i, int j) => i <= j && j >= n - i - 1;
            bool F07(int n, int i, int j) => i >= j && j >= n - i - 1;
            bool F08(int n, int i, int j) => i >= j && j < n - i;

            bool F09(int n, int i, int j) => i > j - 1 || j >= n - i - 1;
            bool F10(int n, int i, int j) => i > j - 1 || j <= n - i - 1;
            bool F11(int n, int i, int j) => j > i - 1  || j <= n - i - 1;
            bool F12(int n, int i, int j) => j > i - 1  || j >= n - i - 1;

            bool F13(int n, int i, int j) => i <= j && j < n - i || i >= j && j >= n - i - 1;
            bool F14(int n, int i, int j) => i <= j && j >= n - i - 1 || i >= j && j < n - i;
            bool F15(int n, int i, int j) => i == j || i == n - j - 1 || i == 0 || i == n - 1 || j == 0 || j == n - 1;
            bool F16(int n, int i, int j) => i != j && i != n - j - 1;

            Draw(side, F01, F02, F03, F04);
            Draw(side, F05, F06, F07, F08);
            Draw(side, F09, F10, F11, F12);
            Draw(side, F13, F14, F15, F16);

        }

        private static void Draw(int side, params Func<int, int, int, bool>[] hits)
        {
            for (int i = 0;i < side;i++)
            {
                foreach (var hit in hits)
                {
                    for (int j = 0;j < side;j++)
                    {
                        Console.Write(hit(side, i, j) ? "* " : "  ");
                    }
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("{0}{0}", Environment.NewLine);
        }




    }
}
