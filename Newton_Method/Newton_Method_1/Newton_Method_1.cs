using System;

namespace Newton_Method_1
{
    class Program
    {
        static double f(double x)
        {
            return Math.Log(x)-1;
        }

        static void Main(string[] args)
        {
            double a=2, e=0.000001, x0, x1, err, m = 1/3;
            x0 = a;
            int n = 1;
            do 
            {
                x1 = x0 - f(x0)*x0;
                err = Math.Abs(f(x1)) / m;
                Console.WriteLine("No{0}.   X= {1}    F(x)= {2}", n, x1, f(x1));
                n++;
                x0 = x1;

            } while (err >= e);

            Console.WriteLine("Nghiem la: " + x1);
        }
    }
}
