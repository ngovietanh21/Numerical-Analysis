using System;

namespace Danilevsky
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
                    Console.Write(A[i, j] + "\t");
                    if (j == n - 1) Console.WriteLine();
                }
            }
        }

        static double[] Graeffe(double[] d)
        {
            int n = d.Length;
            int k = n - 1;
            int m = 0;

            double[] x1 = new double[n];
            double[] x2 = new double[n];
            double[] x = new double[n];
            double[] c = new double[n];
            double[] a = new double[n + 1];
            double[] b = new double[n + 1];

            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                c[i] = Convert.ToDouble(d[i]);
                Console.WriteLine("Hệ số của λ^{0}= {1}", k, c[i]);
                a[i] = c[i];
                k = k - 1;
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
            if (m == 5) goto ROOT;
            for (int i = 0; i <= n; i++) a[i] = b[i];
            goto SQUARE;


        ROOT:
            for (int i = 0; i < n; i++)
            {
                x1[i] = Math.Pow(Math.Abs(b[i + 1] / b[i]), Math.Pow(2, -m));
                x2[i] = -x1[i];

                double f = 0;
                for (int j = 0; j < n; j++) f = f * x1[i] + c[j];
                if (Math.Abs(f) < 0.1) x[i] = x1[i];

                f = 0;
                for (int j = 0; j < n; j++) f = f * x2[i] + c[j];
                if (Math.Abs(f) < 0.1) x[i] = x2[i];
            }

            return x;
        }

        static double[,] MultiMatrix(double[,] A, double[,] B)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[,] C = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <  n; j++)
                {
                    C[i, j] = 0;

                    for (int k = 0; k < n; k++)
                        C[i, j] += A[i, k] * B[k, j];
                }
            }
            return C;
        }

        static double[] MultiMatrix2(double[,] A, double[] B)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[] C = new double[n];
            for (int i = 0; i < n; i++)
            {
                C[i] = 0;
                for (int j = 0; j < n; j++)
                    C[i] += A[i, j] * B[j]; 
            }
            return C;
        }

        static void Danilevsky(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[] poly = new double[n + 1];
            double[,] M = new double[n, n];
            double[,] iM = new double[n, n];
            double[,] B = new double[n, n];
            double[,] B1 = new double[n, n];
            double[,] V = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) B1[i, j] = 1;
                    else B1[i, j] = 0;
                }
            }

            for (int k = n-2; k >= 0; k--)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i != k)
                        {
                            if (i == j) M[i, j] = iM[i, j] = 1;
                            else M[i, j] = iM[i, j] = 0;
                        }
                        else
                        {
                            iM[i, j] = A[k + 1, j];
                            if (j == k) M[i, j] = 1 / A[k + 1, k];
                            else M[i, j] = -A[k + 1, j] / A[k + 1, k];
                        }
                    }
                }
                Console.WriteLine("M: ");
                Print(M);
                Console.WriteLine("\niM: ");
                Print(iM);

                B = MultiMatrix(A, M);
                A = MultiMatrix(iM, B);
                B = MultiMatrix(B1, M);
                B1 = B;

                Console.WriteLine("\nA: ");
                Print(A);
                if (k==0)
                {
                    Console.WriteLine("\nB: ");
                    Print(B);
                }
                Console.WriteLine("---------------");
            }


            for (int i = 0; i < n + 1; ++i)
            {
                if (i == 0) poly[i] = Math.Pow(-1, n) * 1;
                else poly[i] = Math.Pow(-1, n) * -1 * A[0, i - 1];
            }

            double[] lambda = Graeffe(poly);
            //double[] lambda = { -2, 2, 3 };
            for (int i = 0; i < n; i++)
            {
                double[] X = new double[n];
                Console.WriteLine("\nλ{0} = {1}", i + 1, lambda[i]);
                Console.Write("Y{0} = ", i + 1);
                for (int j = 0; j < n; j++)
                {
                    V[i, j] = Math.Pow(lambda[i], n - j - 1);
                    Console.Write("{0}    ", V[i, j]);
                    X[j] = V[i, j];
                }
                Console.WriteLine();

                //Tìm vector riêng bằng ma trận B cuối nhân với ma trận cột Y
                poly = MultiMatrix2(B, X);
                Console.Write("X{0} =  ", i + 1);
                for (int j = 0; j < n; j++)
                    Console.Write("{0}   ", poly[j]);

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
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

            /*double[,] A =
            {
                { 4, 2, 2 },
                { 2, 5, 1 },
                { 2, 1, 6 },
            };*/

            /*double[,] A =
            {
                {20, 14, 0, 0 },
                {14, 10, 0, 0 },
                {0, 0, 0, 0 },
                {0, 0, 0, 0 },
            };*/

            /*double[,] A =
            {
                {5.1, 1.1, 1.0},
                {1.1, 6.1, 1.1},
                {1.0, 1.1, 5.1},
            };*/

            Danilevsky(A);
        }
    }
}
