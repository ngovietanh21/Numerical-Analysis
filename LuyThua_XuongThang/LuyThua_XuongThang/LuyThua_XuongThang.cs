using System;

namespace LuyThua_XuongThang
{
    class Program
    {
        static void Print(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0}\t", Math.Round(A[i, j], 4));
                }
                Console.Write("\n");
            }
        }

        static double MAX(double[] A)
        {
            int n = A.Length;
            double max = A[0];
            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(max) < Math.Abs(A[i]))
                    max = A[i];
            }
            return max;
        }

        static void Main(string[] args)
        {
            /*double[,] A =
            {
                {4, 2, 2},
                {2, 5, 1},
                {2, 1, 6},
            };*/

            /*double[,] A =
            {
                {1, 2, 3, 4 },
                {2, 1, 2, 3 },
                {3, 2, 1, 2 },
                {4, 3, 2, 1 },
            };*/

            double[,] A = {
                   {11, 22, -13, 24, 15, -26,17,28 },
                   {22,233,24,35,26,37,28,-39},
                   {33,-24,35,-26,37,28,-39,20},
                   {14,45,26,47,38,49,40,-41},
                   {-55,16,57,28,59,30,-51,42},
                   {46,27,-48,39,40,61,41,73},
                   {27,-58,29,70,-21,42,23,34},
                   {38,59,60,-71,82 ,-93,24,15},
                   };

            double[] X = { 1, 1, 1, 1, 1, 1,1,1};

            int n = X.Length;
            double eps = 0.0001, λ0 = 0, λ = 0;
            double[] Y = new double[n];
            double[] C = new double[n];
            double[] Lambda = new double[n]; // Mảng chứa giá trị riêng
            double[,] vv = new double[n, n]; // Mảng chứa vector riêng

            for (int l = 0; l < n; l++)
            {
                //for (int i = 0; i < n; i++)
                    //X[i] = 1;

                for (int k = 1; k <= 1000; k++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Y[i] = 0;

                        for (int j = 0; j < n; j++)
                            Y[i] += A[i, j] * X[j];
                    }

                    for (int i = 0; i < n; i++)
                    {
                        C[i] = Y[i] / X[i];
                        X[i] = Y[i] / Y[n - 1];  //Tinh vector riêng
                    }

                    λ = MAX(C); // Tính giá trị riêng

                    if (k==1 || k==2 || k==3 || k==25 || k==26 || k==27)
                    {
                        Console.WriteLine("-----------{0}--------{1} ", k, λ);
                    }

                    if (Math.Abs(λ0 - λ) < eps) {
                        Console.WriteLine("-----------{0}--------",k);
                    break; }

                    λ0 = λ;
                }

                Lambda[l] = λ; // Lưu giá trị riêng
                Console.WriteLine("λ{0}= {1}\t", l+1, λ);
                Console.WriteLine("Vec to rieng: ");
                for (int i = 0; i < n; i++)
                    Console.Write("{0}\t", X[i]);
                Console.WriteLine("\n");

                /*--------------Xuống thang tìm ma trận A ---------------*/

                double ps = 0;
                for (int i = 0; i < n; i++)
                {
                    ps += X[i] * X[i];
                    vv[l, i] = X[i]; //Lưu vector riêng
                }

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        A[i, j] -= (λ / ps) * X[i] * X[j];
            }
        }
    }
}
