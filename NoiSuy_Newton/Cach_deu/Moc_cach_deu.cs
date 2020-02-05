using System;

namespace Moc_cach_deu
{
    class Program
    {
        static double[,] Saiphan(double[] Y)
        {
            int n = Y.Length;
            double[,] A = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                A[i, i] = Y[i];
            }

            for (int k = 0; k < n; k++)
            {
                int i = 0, j = k + 1;
                while (j != n)
                {
                    A[i, j] = A[i + 1, j] - A[i, j - 1];
                    j++;
                    i++;
                }
            }
            return A;
        }

        static double[] Newton_Tien(double[,] Saiphan)
        {
            int n = Saiphan.GetLength(0);
            double Giaithua = 1;
            int k = 0;

            double[] Pn = new double[n];

            for (int i = 2; i < n; i++)
            {
                Giaithua *= i;
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    Pn[n - 1] += Saiphan[0, 0];
                }

                if (i == 1)
                {
                    Pn[n - 2] += Saiphan[0, 1];
                }

                if (i > 1)
                {
                    double[] Xi = new double[i];

                    for (int j = 0; j < i; j++)
                    {
                        Xi[j] = j;

                    }

                    double[] Horner = HornerPn.Nhandathuc(Xi);

                    for (int j = 0; j < Horner.Length; j++)
                    {
                        Horner[j] *= (Saiphan[0, i] / Giaithua);
                        Pn[j + k] += Horner[j];
                    }
                    Giaithua /= i;
                    k++;
                }
            }

            return Pn;
        }

        static double[] Newton_Lui(double[,] Saiphan)
        {
            int n = Saiphan.GetLength(0);
            double Giaithua = 1;
            int k = 0;

            double[] Pn = new double[n];

            for (int i = 2; i < n; i++)
            {
                Giaithua *= i;
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    Pn[n - 1] += Saiphan[n-1, n-1];
                }

                if (i == 1)
                {
                    Pn[n - 2] += Saiphan[n-2, n-1];
                }

                if (i > 1)
                {
                    double[] Xi = new double[i];

                    for (int j = 0; j < i; j++)
                    {
                        Xi[j] = -j;
                    }

                    double[] Horner = HornerPn.Nhandathuc(Xi);

                    for (int j = 0; j < Horner.Length; j++)
                    {
                        Horner[j] *= (Saiphan[i, n-1] / Giaithua);
                        Pn[j + k] += Horner[j];
                    }
                    Giaithua /= i;
                    k++;
                }
            }

            return Pn;
        }

        static void Main(string[] args)
        {
           
            double X0 = 15;
            double X = 18;
            double h = 5;

            double[] Y = { 0.2588, 0.3420, 0.4226, 0.5, 0.5736, 0.6428, 0.7071 };

            double[,] A = Saiphan(Y);
            double[] B = Newton_Tien(A);
            ExportData.ExportData1D(B);
            ExportData.ExportData2D(A);
            Console.WriteLine(HornerPn.Giatridathuc(B, (X - X0) / h));
        }
    }
}
