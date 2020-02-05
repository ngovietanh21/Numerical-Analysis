using System;
using Solution;
using System.Collections.Generic;

namespace NoiSuy_TrungTam
{
    class Gauss
    {

        public static double [] Gauss_Tien_1(double [,] Saiphan)
        {
            int n = Saiphan.GetLength(0);
            int k = 0;
            int mid = (n - 1) / 2;
            int a = 0, b = n;
            double Giaithua = 1;

            double[] Pn = new double[n];
            List<double> Heso = new List<double>();

            for (int i = 2; i < n; i++)
            {
                Giaithua *= i;
            }

            for(double i = -mid; i<=mid ; i++)
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
                    Pn[n - 2] += Saiphan[mid, mid + 1];
                }

                if (i > 1)
                {
                    double[] Xi = new double[i];

                    if (i % 2 == 0)
                    {
                        b--;
                        Heso.RemoveAt(0); 
                    }

                    if (i % 2 != 0)
                    {
                        a++;
                        Heso.RemoveAt(i);
                    }

                    for (int j = 0; j < i; j++)
                    {
                        Xi[j] = Heso[j];
                    }

                    double[] Horner = HornerPn.Nhandathuc(Xi);

                    for (int j = 0; j < Horner.Length; j++)
                    {
                        Horner[j] *= (Saiphan[a, b] / Giaithua);
                        Pn[j + k] += Horner[j];
                    }

                    Giaithua /= i;
                    k++;

                }
            }

            return Pn;
        }

        public static double[] Gauss_Lui_2(double[,] Saiphan)
        {
            int n = Saiphan.GetLength(0);
            int k = 0;
            int mid = (n - 1) / 2;
            int a = -1, b = n-1;
            double Giaithua = 1;

            double[] Pn = new double[n];
            List<double> Heso = new List<double>();

            for (int i = 2; i < n; i++)
            {
                Giaithua *= i;
            }

            for (double i = -mid; i <= mid; i++)
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
                    Pn[n - 2] += Saiphan[mid-1, mid];
                }

                if (i > 1)
                {
                    double[] Xi = new double[i];

                    if (i % 2 == 0)
                    {
                        a++;
                        Heso.RemoveAt(i);
                    }

                    if (i % 2 != 0)
                    {
                        b--;
                        Heso.RemoveAt(0);
                    }

                    for (int j = 0; j < i; j++)
                    {
                        Xi[j] = Heso[j];
                    }

                    double[] Horner = HornerPn.Nhandathuc(Xi);

                    for (int j = 0; j < Horner.Length; j++)
                    {
                        Horner[j] *= (Saiphan[a, b] / Giaithua);
                        Pn[j + k] += Horner[j];
                    }

                    Giaithua /= i;
                    k++;

                }
            }

            return Pn;
        }
    }
}
