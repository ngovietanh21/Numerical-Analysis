using System;

namespace InverseMatrix
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

        static double NORM_INFINITY_MATRIX(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[] X = new double[n];
            double max;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    X[i] += Math.Abs(A[i, j]);
            }

            max = X[0];
            for (int i = 1; i < n; i++)
            {
                if (max < X[i]) max = X[i];
            }
            return max;
        }

        static double NORM_ONE_MATRIX(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[] X = new double[n];
            double max;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    X[i] += Math.Abs(A[j, i]);
            }

            max = X[0];
            for (int i = 1; i < n; i++)
            {
                if (max < X[i]) max = X[i];
            }
            return max;
        }

        static double NORM_EUCLIDE_MATRIX(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double Sum = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Sum += A[i, j] * A[i, j];
            }
            Math.Sqrt(Sum);

            return Sum;
        }

        static void JacobiInverse(double[,] A, double [,] X)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[,] X_NEW = new double[n, n];
            double[,] ERR = new double[n, n];
            double[,] E = new double[n, n];
            double[,] G = new double[n, n];
            double q0, q1, q2, x;

            for (int i = 0; i < n; i++) E[i, i] = 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    G[i, j] = E[i, j] - A[i, j] * X[i, j];
            }

            // Tính Norm G trước rồi quyết định chọn NORM để tính sai số
            q0 = NORM_INFINITY_MATRIX(G);
            /*q1 = NORM_ONE_MATRIX(G);
            q2 = NORM_EUCLIDE_MATRIX(G);
            Console.WriteLine("||G||0 = " + q0);
            Console.WriteLine("||G||1 = " + q1);
            Console.WriteLine("||G||2 = " + q2);*/

            x = NORM_INFINITY_MATRIX(X); ;
            //x = NORM_ONE_MATRIX(X);
            //x = NORM_EUCLIDE_MATRIX(X);

            /*if (q0 < 1 && q1 < 1 && q2 < 1)
            {
                Console.WriteLine("Không thoả mãn điều kiện hội tụ, chọn lại X0");
            }*/

            Console.Write("So lan lap: ");
            int ite = Convert.ToInt32(Console.ReadLine());
            for (int k = 1; k <= ite; k++)
            {
                double err;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        X_NEW[i, j] = X[i, j] * (2*E[i, j] - A[i, j] * X[i, j]);

                        ERR[i, j] = X_NEW[i, j] - X[i,j];
                    }
                }
                Console.WriteLine("No." + k);
                Print(X_NEW);

                err = NORM_INFINITY_MATRIX(ERR);
                //err = NORM_ONE_MATRIX(ERR);
                //err = NORM_EUCLIDE_MATRIX(ERR);

                Console.WriteLine("||A^(-1) - X({0})|| <= {1}", k, x/(1-q0) * Math.Pow(q0, Math.Pow(2,k)));

                //if (err <= x / (1 - q0) * Math.Pow(q0, Math.Pow(2, k)) break;

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        X[i, j] = X_NEW[i, j];
                Console.WriteLine();
            }
        }

        static void CholeskyInverse(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[,] S = new double[n, n];
            double[,] iS = new double[n, n];
            double[,] iA = new double[n, n];

            Console.WriteLine("Ma tran A: ");
            Print(A);

            //Tìm ma trận tam giác dưới
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
            Console.WriteLine("\nMa tran S: ");
            Print(S);

            //Tìm ma trận nghịch đảo của ma trận tam giác dưới
            for (int i = n-1; i >= 0; i--)
            {
                iS[i, i] = 1 / S[i, i];

                for (int j = i + 1; j < n; j++)
                    for (int k = i + 1; j < n; j++)
                        iS[j,i] += iS[j,k] * S[k,i];

                for (int j = i + 1; j < n; j++)
                    iS[j, i] = -iS[i, i] * iS[j, i];
            }
            Console.WriteLine("\nS^(-1): ");
            Print(iS);

            //Tính tích hai ma trận S^(-1) và iS^(-1)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    iA[i,j] = 0;

                    for (int k = 0; k < n; k++)
                        iA[i,j] += iS[k,i] * iS[k,j];
                }
            }

            Console.WriteLine("\nMa tran nghich dao cua A: ");
            Print(iA);
        }

        static void GaussJordanInverse(double[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            double[,] E = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) E[i, j] = 1;
                    else E[i, j] = 0;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double temp = A[j, i] / A[i, i];
                        for (int k = 0; k < n; k++)
                        {
                            A[j, k] -= A[i, k] * temp;
                            E[j, k] -= E[i, k] * temp;
                        }
                    }
                }
                Console.WriteLine("-------------------");
                Console.WriteLine("E: ");
                Print(E);
                Console.WriteLine("\nA:");
                Print(A);
            }

            for (int i = 0; i < n; i++)
            {
                double temp = A[i, i];
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = A[i, j] / temp;
                    E[i, j] = E[i, j] / temp;
                }
                Console.WriteLine("-------------------");
                Console.WriteLine("\nE");
                Print(E);
                Console.WriteLine("\nA");
                Print(A);
            }
        }

        static void Main(string[] args)
        {
            /*double[,] A = {
                   {1, 4, 1, 3},
                   {0, -1, 3, -1},
                   {3, 1, 0, 2},
                   {1, -2, 5, 1},
                   };*/

            /*double[,] A = {
                  {4.9, 1.0, 0.1, 1.1},
                  { 1.0, 6.4, 1.2, 0.2},
                  { 0.1, 1.2, 3.6, 1.1},
                  { 1.1, 0.2, 1.1, 6.4},
                  };*/

            /*double[,] A = {
                  {13, 14, 6, 4},
                  { 8, -1, 13, 9},
                  { 6, 7, 3, 2},
                  { 9, 5, 16, 11},
                  };*/

            /*double[,] A =
            {
                {50, 107, 36},
                {25, 54, 20},
                {31, 66, 21},
            };*/

            double[,] A = {
                   {211, 22, -13, 24, 15, -26,17,28 },
                   {22,433,24,35,26,37,28,-39},
                   {33,-24,235,-26,37,28,-39,20},
                   {14,45,26,247,38,49,40,-41},
                   {-55,16,57,28,259,30,-51,42},
                   {46,27,-48,39,40,261,41,73},
                   {27,-58,29,70,-21,42,223,34},
                   {38,59,60,-71,82 ,-93,24,215},
                   };

            CholeskyInverse(A);
            //GaussJordanInverse(A);

        }
    }
}
