using System;

namespace Secant_Method
{
    class Program
    {
        static double f(double x)
        {
            return x * x * x - 0.2* x * x-0.2*x - 1.2;
        }

        static void Main(string[] args)
        {
            double a, b ,e,x0,x1,err,m=2.4;
            //m= min cua abs f'
            int n = 1;
            Console.Write("a= ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b= ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("e= ");
            e = Convert.ToDouble(Console.ReadLine());
            x0 = b;
            do
            {
                x1 = x0 - (f(x0) * (x0 - a) / (f(x0) - f(a)));
                err = Math.Abs(f(x1)) / m;
                Console.WriteLine("No{0}.   X= {1}    F(x)= {2}", n, x1, f(x1));
                n++;
                x0 = x1;

            } while (err>=e);

            Console.WriteLine("Nghiem la: " + x1);
        }
    }
}
