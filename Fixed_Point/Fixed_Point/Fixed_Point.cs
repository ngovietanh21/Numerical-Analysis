using System;

namespace Fixed_Point
{
    class Program
    {
        static double F(double x)
        {
            return Math.Cos(x) - 3 * x + 1;
        }

        static double G(double x)
        {
            return (1 + Math.Cos(x)) / 3;
        }

        static void Main(string[] args)
        {
            int step = 1, N =10;
            double x0=1,x1, e=0.0001;
            Console.WriteLine("step\tx0\tf(x0)\tx1\tf(x1)");
            do
            {
                x1 = G(x0);
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", step, x0, F(x0), x1, F(x1));
                step = step + 1;
                if (step > N)
                {
                    Console.Write("Khong hoi tu");
                    break;
                }
                x0 = x1;
            } while (Math.Abs(F(x1)) > e);

            Console.WriteLine("Nghiem la: " + x1);
        }
    }
}
