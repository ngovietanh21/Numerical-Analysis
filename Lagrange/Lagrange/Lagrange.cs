using System;

namespace Lagrange
{
    class MainClass
    {
        static double Giatridathuc(double[] Pn, double c)
        {
            double f = Pn[0];
            int n = Pn.Length;
            for (int i = 1; i < n; i++)
            {
                f = f * c + Pn[i];
            }
            return f;
        }

        static double[] Chiadathua(double[] TuSo, double c)
        {
            int n = TuSo.Length;
            double[] Kq = new double[n];
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    Kq[i] = TuSo[i];
                }
                else
                {
                    Kq[i] = Kq[i - 1] * c + TuSo[i];
                }

            }

            return Kq;
        }

        static double[] Nhandathuc(double[] Mocnoisuy)
        {
            int n = Mocnoisuy.Length;
            double[] multi = new double[n + 1];
            double[] temp = new double[n + 1];

            for (int k = 0; k < n; k++)
            {
                if (k == 0)
                {
                    multi[0] = temp[0] = 1;
                    multi[1] = temp[1] = -Mocnoisuy[0];
                }
                else
                {
                    for (int i = 1; i <= k + 1; i++)
                    {
                        multi[i] = temp[i] - Mocnoisuy[k] * temp[i - 1];
                    }

                    for (int i = 1; i <= k + 1; i++)
                    {
                        temp[i] = multi[i];
                    }
                }
            }

            return multi;
        }

        static double[] Congdathua(double[] a, double [] b)
        {
            int n = a.Length;
            double[] c = new double[n];

            for(int i = 0; i < n; i++)
            {
                c[i] = a[i] + b[i];
            }

            return c;
        }

        static double[] Noi_Suy_Lagrange(double[] X, double[] Y) 
		{
            int n = X.Length;
            double[] multi = Nhandathuc(X);
            double[] a = new double[n + 1];
            double[] b = new double[n];

            for (int k = 0; k < n; k++)
            {
                double Sum = 1;

                for (int i = 0; i < n; i++)
                {
                    if (i != k) Sum *= (X[k] - X[i]);
                }

                Sum = Y[k] / Sum;

                for (int i = 0; i < multi.Length; i++)
                {
                    a[i] = multi[i] * Sum;
                }

                a = Chiadathua(a, X[k]);
                b = Congdathua(b, a);
            }

            return b;
        }

        static void Print_Array(double[] A)
        {
            for(int i = 0; i < A.Length; i++)
            {
                Console.Write("{0}\n", A[i]);
            }
        }

        public static void Main(string[] args)
        {
            double[] Y = { 1, 1.2, 1.4, 1.6, 1.8 };
            double[] X = { 1.57, 2.34, 2.75, 3.17, 4.05 };

            double[] Lagrange = Noi_Suy_Lagrange(X, Y);
            Print_Array(Lagrange);
            Console.WriteLine();
            Console.WriteLine(Giatridathuc(Lagrange, 2.5));
        }
    }
}
