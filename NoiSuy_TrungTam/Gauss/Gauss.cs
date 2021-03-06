﻿using System;
using Solution;
using System.Collections.Generic;

namespace NoiSuy_TrungTam
{
    class Gauss
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

        static void Main()
        {
            // Nội suy trung tâm nên phải lấy cả 2 phía và lấy số lẻ mới chạy được
            // Nội suy tại X = 1.7489 nên phải tính giá trị tại 1.7489-1.75/0.01 = -0.11
            // Dùng Gauss tiến khi t nằm trong khoảng 0 đến 0.5
            // Dùng Gauss lui khi t nằm trong khoảng -0.5 đến 0
            // Dùng Gauss tốt nhất khi gần 0, nếu dương thì tiến, nếu âm thì lùi 
            double[] X = { 1.72, 1.73, 1.74, 1.75, 1.76, 1.77, 1.78 };
            double[] Y = { 5.5845, 5.6406, 5.6973, 5.7546, 5.81244, 5.87085, 5.92986 };
            double[,] A = Saiphan(Y);
            double[] B = Gauss_Tien_1(A);
            double[] C = Gauss_Lui_2(A);
            ExportData.ExportData2D(A);
            ExportData.ExportData1D(C);
            Console.WriteLine(HornerPn.Giatridathuc(B, -0.11));
            Console.WriteLine(HornerPn.Giatridathuc(C, -0.11));
        }
    }
}
