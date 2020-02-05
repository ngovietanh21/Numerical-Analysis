using System;

namespace Gauss_GaussJordan
{
    class Program
    {
        static void Print(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    Console.Write("{0}\t", Math.Round(A[i, j],3));
                    if (j == n - 1) Console.Write("|");
                }
                Console.Write("\n");
            }
        }

        static void Gauss(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[] X = new double[n];

            for (int i = 0; i < n; i++)
            {
                Print(A);
                Console.WriteLine();

                if (A[i,i] == 0 )
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (A[j, i] != 0)
                        {
                            for (int k = 0; k < n + 1; k++)
                            {
                                double temp = A[i, k];
                                A[i, k] = A[j, k];
                                A[j, k] = temp;
                            }
                            break;
                        }
                    }
                }

                for (int j = i + 1; j < n; j++)
                {
                    double temp = A[j, i] / A[i, i];
                    for (int k = 0; k <= n; k++)
                        A[j, k] = A[j, k] - temp * A[i, k];
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                X[i] = A[i, n];

                for (int j = i + 1; j < n; j++)
                    X[i] = X[i] - A[i, j] * X[j];

                X[i] = X[i] / A[i, i];
            }


            Console.WriteLine("\nNghiệm của phương trình là: ");
            for (int i = 0; i < n; i++)
                Console.Write("x{0}= {1}\n", i + 1, X[i]);
        }

        static void GaussJordan(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
                Print(A);

                if (A[i, i] == 0)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        if (A[j, i] != 0)
                        {
                            for (int k = 0; k < n + 1; k++)
                            {
                                double tem = A[i, k];
                                A[i, k] = A[j, k];
                                A[j, k] = tem;
                            }
                            break;
                        }
                    }
                }

                double temp = A[i, i];

                for (int j = 0; j < n + 1; j++)
                {
                    A[i, j] = A[i, j] / temp;
                }

                for (int k = 0; k < n; k++)
                {
                    temp = A[k, i];
                    if (i != k)
                    {
                        for (int j = 0; j < n + 1; j++)
                        {
                            A[k, j] = A[k, j] - (temp * A[i, j]);
                        }
                    }
                }

            }
            Console.WriteLine("\nGAUSS JORDAN");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n + 1; j++)
                {
                    Console.Write("{0}\t", A[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Main()
        {
            /*double[,] A = {
                   {20.5, 1.7, -3.2, 2.1, 9.23,-3.52, 21.41},
                   {2.5, 37.1, 5.2, 2.8, 7.23, -5.52, 27.11},
                   {11.3, 2.7, -38.2, 4.1, -7.58, 4.25, 14.17},
                   {8.4, -4.6, -6.5, 52.1, 1.43, 15.26, 52.49},
                   {42.7, -36.9, -42.7, 61.1, 2.43, -35.26, 56.72},
                    {9.2, -1, 35, -2, 14.73,5.64, 18.57},
                   };*/

            double[,] A = {
                   {-1, 2, 0, 4},
                   {2, -3, 0, 1},
                   {-1, 3, 0, 2},
                   };

            /*double[,] A = {
                   {1, 1, 1, 1, 5},
                   {2, 2, -1, 3, 10},
                   {-2, -1, 1, 1, 3},
                   {2, 3, 4, 2, 11},
                   };*/


            /*double[,] A = {
                   {208, 30, 1, 1, -2180},
                   {178, 22, 2, 0, -1704},
                   {192, 18, 3, 0, -1656},
                   {242, 20, 4, 0, -1992},
                   };*/

            Gauss(A);
            //GaussJordan(A);
        }
    }
}