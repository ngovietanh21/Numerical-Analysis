using System;
using System.IO;

namespace Graeffe
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] d = File.ReadAllLines(@"Untitled 8.applescript");
            int n = 9;
            int k = n;
            int m = 0;

            double[] x1 = new double[n];
            double[] x2 = new double[n];
            double[] c = { 1, -484, 75538, -4350202, -77596110, 38743766888, -5043770223112, 212852129071840, 0.00000001 };
            double[] a = new double[n + 1];
            double[] b = new double[n + 1];

            for (int i = 0; i < n; i++)
            {
                //c[i] = Convert.ToDouble(d[i]);
                Console.WriteLine("Hệ số của x^{0}= {1}", --k, c[i]);
                a[i] = c[i];
            }
            Console.WriteLine("\nNghiệm của phương trình là: ");


        SQUARE:
            b[0] = Math.Pow(a[0], 2);
            for (int i = 1; i <= n; i++)
            {
                int j = 1;
                b[i] = Math.Pow(a[i], 2);
                while ((i + j <= n) && (j <= i))
                {
                    b[i] = b[i] + Math.Pow(-1, j) * 2 * a[i - j] * a[i + j];
                    j = j + 1;
                }
            }


            m = m + 1;
            if (m == 4) goto ROOT;
            for (int i = 0; i <= n; i++) a[i] = b[i];
            goto SQUARE;


        ROOT:
            Console.WriteLine();
            k = 1;
            for (int i = 0; i < n; i++)
            {
                x1[i] = Math.Pow(Math.Abs(b[i + 1] / b[i]), Math.Pow(2, -m));
                x2[i] = -x1[i];

                double f = 0;
                for (int j = 0; j < n; j++) f = f * x1[i] + c[j];
                if (Math.Abs(f) < 0.1)
                    Console.WriteLine("X{0}= {1}", k++, x1[i]);

                f = 0;
                for (int j = 0; j < n; j++) f = f * x2[i] + c[j];
                if (Math.Abs(f) < 0.1)
                    Console.WriteLine("X{0}= {1}", k++, x2[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0}|\t", x1[i]);

            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0}|\t", x2[i]);
            }
        }
    }
}
