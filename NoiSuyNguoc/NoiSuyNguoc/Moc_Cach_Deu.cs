using System;
using Solution;
using System.Collections.Generic;

namespace NoiSuyNguoc
{
    class Moc_Cach_Deu
    {
        static double[,] BangSaiphan(double[] Y)
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

                    if (A[i, j] < 0.000001)
                        A[i, j] = Math.Round(A[i, j], 4);

                    j++;
                    i++;
                    
                }
            }
            return A;
        }

        static double[] Lap_Tien(double[,] Saiphan, double Y)
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
                    Pn[n - 1] += (Y - Saiphan[0, 0]) / Saiphan[0, 1];
                }

                if (i == 1)
                {
                    continue;
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
                        Horner[j] = -Horner[j];
                        Horner[j] *= Saiphan[0, i] / (Saiphan[0, 1] * Giaithua);
                        Pn[j + k] += Horner[j];
                    }
                    Giaithua /= i;
                    k++;
                }
            }

            for(int i =0; i < n; i++)
            {
                if (Pn[i] < 0.0000001)
                    Pn[i] = Math.Round(Pn[i], 6);
            }

            return Pn;
        }

        static double[] Lap_Lui(double[,] Saiphan, double Y)
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
                    Pn[n - 1] += (Y - Saiphan[n - 1, n - 1]) / Saiphan[n - 2, n - 1];
                }

                if (i == 1)
                {
                    continue;
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
                        Horner[j] = -Horner[j];
                        Horner[j] *= Saiphan[i, n - 1] / (Saiphan[n - 2, n - 1] * Giaithua);
                        Pn[j + k] += Horner[j];
                    }
                    Giaithua /= i;
                    k++;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (Pn[i] < 0.0000001)
                    Pn[i] = Math.Round(Pn[i], 6);
            }

            return Pn;
        }

        static double[] Stirling(double[,] Saiphan, double Y)
        {
            int n = Saiphan.GetLength(0);
            int k = 0;
            int mid = (n - 1) / 2;
            int a = 0, b = n - 1;
            double Giaithua = 1;
            double t = (Saiphan[mid, mid + 1] + Saiphan[mid - 1, mid]) / 2;

            double[] Pn = new double[n];
            List<double> Heso = new List<double>();

            for (int i = 2; i < n; i++)
            {
                Giaithua *= i;
            }

            for (double i = -mid + 1; i <= mid - 1; i++)
            {
                Heso.Add(i);
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    Pn[n - 1] += (Y - Saiphan[mid, mid]) / t;
                }

                if (i == 1)
                {
                    continue;
                }

                if (i == 2)
                {
                    Pn[n - 3] += -(Saiphan[mid - 1, mid + 1] / 2) / t;
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
                            Horner[j] = -Horner[j];
                            Horner[j] *= Saiphan[a, b] / (t * Giaithua);
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
                            Horner[j] = -Horner[j];
                            Horner[j] *= (Saiphan[a, b + 1] + Saiphan[a - 1, b]) / (t * 2 * Giaithua);
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

        static void Iteration(double[] DaThucLap, int SoLanLap, double X0, double h)
        {
            double t0 = DaThucLap[DaThucLap.Length - 1];
            double t1;

            Console.WriteLine("t0 = " + t0);

            for(int i =1; i <= SoLanLap; i++)
            {
                t1 = HornerPn.Giatridathuc(DaThucLap, t0);
                Console.WriteLine("t{0} = {1}", i, t1);
                t0 = t1;
                if (i==SoLanLap)
                    Console.WriteLine("X = " + (t1 * h + X0));
            }

            // t = (X - X0) / h
            // Trong ví dụ lặp tiến X0 = 0.3 va h = 0.1
            // Trong ví dụ lặp lùi X0 = 1.38 va h = 0.01
            // Trong ví dụ lặp trung tâm X0 = 4.2 va h = 0.01
            
        }

        public static void MainCachDeu()
        {
            // Lặp tiến 
            // tìm X để Y=0.2
            /*double[] X1 = { 0.3, 0.4, 0.5, 0.6, 0.7 };
            double[] Y1 = { 0.194, 0.213, 0.231, 0.249, 0.268};

            double[,] A1 = BangSaiphan(Y1);

            double[] DaThucLap1= Lap_Tien(A1,0.2);

            Iteration(DaThucLap1, 7, 0.3, 0.1);

            ExportData.ExportData2D(A1);
            ExportData.ExportData1D(DaThucLap1);*/

            // Lăp lùi
            // tìm X để Y=0
            double[] X2 = { 16.5, 19.5, 22.5, 25.5};
            double[] Y2 = { 15824, 11454, 7292, 3631 };

            double[,] A2 = BangSaiphan(Y2);

            double[] DaThucLap2 = Lap_Lui(A2, 7222);

            Iteration(DaThucLap2, 8, 25.5, 3.5);

            ExportData.ExportData2D(A2);
            ExportData.ExportData1D(DaThucLap2);

            // Lặp trung tâm 
            // tìm X để Y=2.5
            /*double[] X3 = { 1, 1.2, 1.4, 1.6, 1.8 };
            double[] Y3 = { 1.57, 2.34, 2.75, 3.17, 4.05 };

            double[,] A3 = BangSaiphan(Y3);

            double[] DaThucLap3 = Stirling(A3, 2.5);

            Iteration(DaThucLap3, 7, 1.4, 0.2);

            ExportData.ExportData2D(A3);
            ExportData.ExportData1D(DaThucLap3);*/
        }
    }
}
