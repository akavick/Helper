using System;

namespace MainConsoleTestProject
{
    internal class MatrixPainter
    {
        private const int N = 13;
        private int I { get; set; }
        private int J { get; set; }

        public void Run()
        {
            bool Quadrat(int offset = 0)
            {
                var o1 = offset;
                var o2 = N - offset - 1;
                return (I == o1 || I == o2) && J >= o1 && J <= o2 || (J == o1 || J == o2) && I >= o1 && I <= o2;
            }
            bool DiaQuadrat() => I == N - J + N / 2 - 1 || I == J - N / 2 || I == N - J - N / 2 - 1 || I == J + N / 2;
            bool Cross() => I == N / 2 || J == N / 2 || I == (N - 1) / 2 || J == (N - 1) / 2;
            bool DiaCross() => I == J || I == N - J - 1;
            bool Circle(double outerMod = 0, double innerMod = 0, bool equals = false, bool filled = false)
            {
                var r = (N - innerMod) / 2.0 - outerMod;
                const double ic = (N - 1) / 2.0;
                const double jc = (N - 1) / 2.0;
                var val = (I - ic) * (I - ic) + (J - jc) * (J - jc);
                var qr = r * r;
                if (filled)
                    return equals ? val <= qr : val < qr;
                var abs = Math.Abs(val - qr);
                return equals ? abs <= N : abs < N;
            }

            bool F01() => I >= J;
            bool F02() => I < N - J;
            bool F03() => I <= J;
            bool F04() => I >= N - J - 1;
            bool F05() => Cross() || DiaCross();

            bool F06() => I <= J && I < N - J;
            bool F07() => I <= J && I >= N - J - 1;
            bool F08() => I >= J && I >= N - J - 1;
            bool F09() => I >= J && I < N - J;
            bool F10() => F06() || F08();

            bool F11() => I > J - 1 || I >= N - J - 1;
            bool F12() => I > J - 1 || I <= N - J - 1;
            bool F13() => I < J + 1 || I <= N - J - 1;
            bool F14() => I < J + 1 || I >= N - J - 1;
            bool F15() => F07() || F09();

            bool F16() => DiaQuadrat() || Circle(N / 2.9, equals: false, filled: true) || Quadrat(N / 5);
            bool F17() => Quadrat() || Quadrat(N / 5) || Quadrat(N / 3);
            bool F18() => !Circle(0, 1) && !Circle(N / 2.0) || Cross();
            bool F19() => Circle(N / 3.3, equals: false, filled: true) || Circle(0, 1);
            bool F20() => I >= N - J + N / 2 - 1 || I < J - N / 2 + 1 || I <= N - J - N / 2 - 1 || I >= J + N / 2;

            Draw(F01, F02, F03, F04, F05);
            Draw(F06, F07, F08, F09, F10);
            Draw(F11, F12, F13, F14, F15);
            Draw(F16, F17, F18, F19, F20);
        }

        private void Draw(params Func<bool>[] hits)
        {
            for (I = 0;I < N;I++)
            {
                foreach (var hit in hits)
                {
                    for (J = 0;J < N;J++)
                    {
                        Console.Write(hit() ? "* " : "  ");
                    }
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}