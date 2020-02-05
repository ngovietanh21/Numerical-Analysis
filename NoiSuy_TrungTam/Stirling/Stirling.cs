using System;
using Solution;
using System.Collections.Generic;

namespace NoiSuy_TrungTam
{
    class Stirling
    {
        static double[,] SaiPhan(double[] Y)
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

        static double [] DaThucStirling(double [,] Saiphan)
        {
            int n = Saiphan.GetLength(0);
            int k = 0;
            int mid = (n - 1) / 2;
            int a = 0, b = n - 1;
            double Giaithua = 1;

            double[] Pn = new double[n];
            List<double> Heso = new List<double>();

            for (int i = 2; i < n; i++)
            {
                Giaithua *= i;
            }

            for (double i = - mid + 1; i <= mid - 1; i++)
            {
                Heso.Add(i);
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    Pn[n - 1] += Saiphan[mid, mid];
                }

                if (i == 1)
                {
                    Pn[n - 2] += (Saiphan[mid, mid + 1] + Saiphan[mid - 1, mid])/2 ;
                }

                if (i == 2)
                {
                    Pn[n - 3] += Saiphan[mid - 1, mid + 1] / 2;
                }

                if (i > 2)
                {
                    if (i % 2 == 0)
                    {
                        Heso.Add(0);

                        double[] Xi = Heso.ToArray();
                        double[] Horner = HornerPn.Nhandathuc(Xi);
                        for (int j = 0; j < Horner.Length; j++)
                        {
                            Horner[j] *= Saiphan[a, b] / Giaithua;
                            Pn[j + k] += Horner[j];
                        }
                        Heso.RemoveAt(Heso.Count - 1);
                    }

                    if (i % 2 != 0)
                    {
                        double[] Xi = Heso.ToArray();
                        double[] Horner = HornerPn.Nhandathuc(Xi);
                        a++;
                        b--;
                        for (int j = 0; j < Horner.Length; j++)
                        {
                            Horner[j] *= (Saiphan[a, b + 1] + Saiphan[a - 1, b]) / (2 * Giaithua);
                            Pn[j + k] += Horner[j];
                        }
                        Heso.RemoveAt(0);
                        Heso.RemoveAt(Heso.Count - 1);
                    }
                    Giaithua /= i;
                    k++;
                }
            }

            return Pn;
        }

        static void Main(string[] args)
        {
            // Nội suy trung tâm nên phải lấy cả 2 phía và lấy số lẻ mới chạy được
            // Nội suy tại X = 1.7489 nên phải tính giá trị tại 1.7489-1.75/0.01 = -0.11
            // Dùng Stirling khi t nằm trong khoảng -0.5 đến 0.5
            // Dùng Stirling tốt nhất khi t nằm trong khoảng -0.25 đến 0.25
            double[] X = { 75, 76, 77, 78, 79, 80, 81, 82, 83, 84 };
            double[] Y = { 2.76806, 2.83267, 2.90256, 2.97857, 3.06173, 3.15339, 3.25530 };
            double[,] A = SaiPhan(Y);
            ExportData.ExportData2D(A);
            double[] B = DaThucStirling(A);
            ExportData.ExportData1D(B);
            Console.WriteLine(HornerPn.Giatridathuc(B, 0.5));
        }
    }
}
