using System;

namespace Linear_System
{
    class Program
    {
        static void Cholesky(double[,] A,double[] B)
        {
            int n = B.Length;
            double[,] S = new double[n, n];
            double[] Y = new double[n];
            double[] X = new double[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    double sum = 0;

                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                            sum += Math.Pow(S[j, k], 2);
                        S[j, j] = Math.Sqrt(A[j, j] - sum);
                    }

                    else
                    {
                        for (int k = 0; k < j; k++)
                            sum += S[i, k] * S[j, k];
                        S[i, j] = (A[i, j] - sum) / S[j, j];
                    }
                }
            }

            Console.WriteLine("[CHOLESKY]:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("{0}\t", Math.Round(S[i, j], 4));
                Console.Write("\n");
            }

            //Y
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                    sum += S[i, j] * Y[j];
                Y[i] = (B[i] - sum) / S[i, i];
            }
            Console.WriteLine("\n[Y]:");
            for (int i = 0; i < n; i++) Console.Write("{0}\t", Math.Round(Y[i], 4));

            //X
            for (int i = n-1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = n-1; j > i; j--)
                    sum += S[j, i] * X[j];
                X[i] = (Y[i] - sum) / S[i, i];
            }
            Console.WriteLine("\n\n[X]:");
            for (int i = 0; i < n; i++) Console.Write("{0}\t", Math.Round(X[i], 4));
        }

        static void LU(double[,] A, double[] B)
        {
            int n = B.Length;
            double[,] L = new double[n, n];
            double[,] U = new double[n, n];
            double[] X = new double[n];
            double[] Y = new double[n];

            for (int i = 0; i < n; i++)
            {
                // Upper
                for (int j = i; j < n; j++)
                {

                    double sum = 0;

                    for (int k = 0; k < i; k++)
                        sum += L[i,k] * U[k,j];

                    U[i,j] = A[i,j] - sum;
                }

                // Lower
                for (int j = i; j < n; j++)
                {
                    if (i == j)
                        L[i,i] = 1;
                    else
                    {
                    
                        double sum = 0;

                        for (int k = 0; k < i; k++)
                            sum += L[j,k] * U[k,i];

                        L[j,i] = (A[j,i] - sum) / U[i,i];
                    }
                }
            }

            Console.WriteLine("PHAN TACH LU");
            Console.WriteLine("[L]:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("{0}\t", Math.Round(L[i, j], 4));
                Console.Write("\n");
            }

            Console.WriteLine("\n[U]:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("{0}\t", Math.Round(U[i, j], 4));
                Console.Write("\n");
            }

            //Y
            for (int i = 0; i < n; i++)
            {
                Y[i] = B[i];
                for (int j = 0; j < i; j++)
                {
                    Y[i] -= L[i, j] * Y[j];
                }
            }
            Console.WriteLine("\n[Y]:");
            for (int i = 0; i < n; i++) Console.Write("{0}\t", Math.Round(Y[i], 4));

            //X
            for (int i = n - 1; i >= 0; i--)
            {
                X[i] = Y[i];
                for (int j = i + 1; j < n; j++)
                {
                    X[i] -= U[i,j] * X[j];
                }
                X[i] /= U[i,i];
            }

            Console.WriteLine("\n\n[X]:");
            for (int i = 0; i < n; i++) Console.Write("{0}\t", Math.Round(X[i], 4));
        }

        static void Main()
        {
            /*
             double[,] A = {
                   {1, 3, -2, 0, -2},
                   {3, 4, -5, 1, -3},
                   {-2, -5, 3, -2, 2},
                   {0, 1, -2, 5, 3},
                   {-2, -3, 2, 3, 4},
                   };
             double[] B = { 0.5, 5.4, 5.0, 7.5, 3.3};
             */

            /*
            double[,] A = {
                   {20.5, 1.7, -3.2, 2.1, 9.23,-3.52 },
                   {2.5, 37.1, 5.2, 2.8, 7.23, -5.52},
                   {11.3, 2.7, -38.2, 4.1, -7.58, 4.25},
                   {8.4, -4.6, -6.5, 52.1, 1.43, 15.26},
                   {42.7, -36.9, -42.7, 61.1, 2.43, -35.26},
                   {9.2, -1, 35, -2, 14.73,5.64 },
                   };
            double[] B = { 21.41,27.11,14.17,52.49,56.72,18.57};
            */

            double[,] A =
            {
                { 1.7 , 0.2, 0.1, 0.3},
                { 0.2, 1.9, -0.1, 0.2},
                { 0.1, -0.1, 1.3, -0.4},
                { 0.3, 0.2, -0.4, 4.1},
            };
            double[] B = { 1.6, 0.3, -1.2, 0.7 };


            LU(A, B);
            Cholesky(A, B);
        }
    }
}