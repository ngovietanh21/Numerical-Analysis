using System;

namespace Jacobi_GaussSeldel
{
    class Program
    {
        static double[,] Alpha;
        static double[] Beta;
        static double qi,q1,q2,Lambda;
        static int n;

        static double NORM_INFINITY_VECTOR(double[] A)
        {
            int n = A.Length;
            double max = A[0];
            for (int i = 1; i < n; i++)
            {
                if (Math.Abs(max) < Math.Abs(A[i]))
                    max = A[i];
            }
            return Math.Abs(max);
        }

        static double NORM_ONE_VECTOR(double[] A)
        {
            int n = A.Length;
            double Sum = 0;
            for (int i = 0; i < n; i++)
                Sum += Math.Abs(A[i]);

            return Sum;
        }

        static double NORM_EUCLIDE_VECTOR(double[] A)
        {
            int n = A.Length;
            double Sum = 0;
            for (int i = 0; i < n; i++)
                Sum += A[i] * A[i];

            Sum = Math.Sqrt(Sum);

            return Sum;
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

        static void Jacobi()
        {
            Console.WriteLine("JACOBI THEO CHUẨN");
            double[] X = new double[n];
            double[] X_OLD = new double[n];
            double[] Y = new double[n];

            Console.Write("X(0):\n");
            for (int i = 0; i < n; i++)
                Console.Write("{0}\t", X[i]);

            //Bắt đầu quá trình lặp Jacobi
            Console.Write("\nSo lan lap: ");
            int ite = Convert.ToInt32(Console.ReadLine());

            for (int k = 1; k <= ite; k++)
            {
                double Err;
                Console.WriteLine("\nNo.{0}", k);
                for (int i = 0; i < n; i++)
                {
                    double Sum = 0;
                    for (int j = 0; j < n; j++)
                        Sum += Alpha[i, j] * X[j];

                    X[i] = Sum + Beta[i];

                    Y[i] = X[i] - X_OLD[i];

                    X_OLD[i] = X[i];

                    Console.Write("x{0}= {1}\t", i + 1, Math.Round(X[i],6));
                }
                //Nếu dùng chuẩn một thì đánh giá sai số ở đây 
                Console.WriteLine("\n||X({0}) - X({1})|| = {2}", k, k - 1, NORM_ONE_VECTOR(Y));
                Err = q1 / (1 - q1) * NORM_ONE_VECTOR(Y);
                Console.WriteLine("||X({0}) - X*|| <= {1}", k, Err);

                //Nếu dùng chuẩn vô cùng thì đánh giá sai số ở đây
                /*Console.WriteLine("\n||X({0}) - X({1})|| = {2}", k, k - 1, NORM_INFINITY_VECTOR(Y));
                Err = qi / (1 - qi) * NORM_INFINITY_VECTOR(Y);
                Console.WriteLine("||X({0}) - X*|| <= {1}", k, Err);*/
            }
        }

        static void GaussSeldel()
        {
            Console.WriteLine("GAUSS SEIDEL");
            //Khai báo biến
            double[] X = new double[n];
            double[] X_OLD = new double[n];
            double[] Y = new double[n];

            Console.Write("X(0):\n");
            for (int i = 0; i < n; i++)
                Console.Write("{0}\t", X[i]);

            //Bắt đầu quá trình lặp Seidel
            Console.Write("\nSo lan lap: ");
            int ite = Convert.ToInt32(Console.ReadLine());

            for (int k = 1; k <= ite; k++)
            {
                double Err;
                Console.WriteLine("\nNo.{0}", k);
                for (int i = 0; i < n; i++)
                {
                    double Sum1 = 0;
                    for (int j = 0; j < i; j++)
                        Sum1 += Alpha[i, j] * X[j];

                    double Sum2 = 0;
                    for (int j = i + 1; j < n; j++)
                        Sum2 += Alpha[i, j] * X_OLD[j];

                    X[i] = Sum1 + Sum2 + Beta[i];

                    Y[i] = X[i] - X_OLD[i];

                    X_OLD[i] = X[i];

                    Console.Write("x{0}= {1}\t ", i + 1, Math.Round(X[i],6));
                }
                Console.WriteLine("\n||X({0}) - X({1})|| = {2}", k, k - 1, NORM_INFINITY_VECTOR(Y));

                Err = Lambda / (1 - Lambda) * NORM_INFINITY_VECTOR(Y);
                Console.WriteLine("||X({0}) - X*|| <= {1}", k, Err);
            }
        }

        static void Main(string[] args)
        {

            /*double[,] A = {
                   { 10, -1, 2, 0 },
                    { -1, 11, -1, 3},
                    { 2, -1, 10, -1},
                    { 0, 3, -1, 8},
                   };
            double[] B = { 6, 25, -11, 15 };*/

           /*double[,] A = {
                   {10, 1, 1},
                    { 2, 10, 1},
                    { 2, 2, 10},
                   };
            double[] B = { 12, 13, 14};*/

           double[,] A = {
                   { 10, -1, 2, -3 },
                    { -1, 10, -1, 2},
                    { 2, 3, 20, -1},
                    { 3, 2, 1, 20},
                   };
            double[] B = { 0, 5, -10, 15};

            /*double[,] A = {
                   { 4, -1, 0, -1, 0, 0},
                    { -1, 4, -1, 0, -1, 0},
                    { 0, -1, 4, 0, 0, -1},
                    { -1, 0, 0, 1, -1, 0},
                    { 0, -1, 0, -1, 4, -1},
                    { 0, 0, -1, 0, -1, 4},
                   };
            double[] B = { 0, 5, 0, 6, -2, 6};*/

            n = B.Length;
            Alpha = new double[n, n];
            Beta = new double[n];
            double[] Y = new double[n];

            //Tính ma trận Alpha và Beta
            for (int i = 0; i < n; i++)
            {
                Beta[i] = B[i] / A[i, i];

                for (int j = 0; j < n; j++)
                {
                    if (i != j) Alpha[i, j] = -A[i, j] / A[i, i];
                    if (i == j) Alpha[i, j] = 0;
                }
            }
                
            //In ra ma trận Alpha và Beta
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0}\t ", Math.Round(Alpha[i, j], 3));
                    if (j == n - 1)
                    {
                        Console.Write("|");
                        Console.Write(Beta[i]);
                    }
                }
                Console.WriteLine();
            }

            //Tính hệ số co λ của ma trận Alpha
            for (int i = 0; i < n; i++)
            {
                double Sum1 = 0,
                        Sum2 = 0;

                for (int j = i; j < n; j++)
                    Sum1 += Math.Abs(Alpha[i, j]);

                for (int j = 0; j < i; j++)
                    Sum2 += Math.Abs(Alpha[i, j]);

                Y[i] = Sum1 / (1 - Sum2);
            }

            Lambda = NORM_INFINITY_VECTOR(Y);
            qi = NORM_INFINITY_MATRIX(Alpha);
            q1 = NORM_ONE_MATRIX(Alpha);
            q2 = NORM_EUCLIDE_MATRIX(Alpha);
            Console.WriteLine("\n||α||(∞)= {0}\n||α||(1)= {1}\n||α||(2)= {0}\n   λ= {3}", qi,q1,q2,Lambda);
            Jacobi();
        }
    }
}
