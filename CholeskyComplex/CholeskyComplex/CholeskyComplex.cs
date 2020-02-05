using System;
using System.Text;
using System.Numerics;

namespace CholeskyComplex
{
    class Program
    {
        static void PrintArray(Complex[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                string s = "";
                for (int j = 0; j < n; j++)
                {
                    s += A[i, j].ToString("f3")+ "\t";
                }
                sb.AppendLine(s);
            }

            Console.WriteLine("{0}\t",sb);
        }

        static void Print(Complex[] A)
        {
            int n = A.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++)
            {

                string s = A[i].ToString("f3");
                sb.AppendLine(s);
                if (i==n-1) Console.Write("{0}", sb);
            }
        }

        static void  Cholesky(Complex[,] A, Complex[] B)
        {
            int n = (int)Math.Sqrt(A.Length);
            Complex[,] S = new Complex[n, n];
            Complex[] Y = new Complex[n];
            Complex[] X = new Complex[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Complex sum = 0;

                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                            sum += S[j, k] * S[j, k];
                        S[j, j] = Complex.Sqrt(A[j, j] - sum);
                    }

                    else
                    {
                        for (int k = 0; k < j; k++)
                            sum += S[i, k] * S[j, k];
                        S[i, j] = (A[i, j] - sum) / S[j, j];
                    }
                }
            }
            Console.WriteLine("Cholesky");
            PrintArray(S);

            for (int i = 0; i < n; i++)
            {
                Complex sum = 0;
                for (int j = 0; j < i; j++)
                    sum += S[i, j] * Y[j];
                Y[i] = (B[i] - sum) / S[i, i];
            }

            Console.WriteLine("Y:");
            Print(Y);

            for (int i = n - 1; i >= 0; i--)
            {
                Complex sum = 0;
                for (int j = n - 1; j > i; j--)
                    sum += S[j, i] * X[j];
                X[i] = (Y[i] - sum) / S[i, i];
            }

            Console.WriteLine("X:");
            Print(X);
        }

        static void Main()
        {

            Complex[,] A =
            {
                {1,3,-2,0,-2},
                {3,4,-5,1,-3},
                {-2,-5,3,-2,2},
                {0,1,-2,5,3},
                {-2,-3,2,3,4 }
            };
            Complex[] B = { 0.5, 5.4, 5.0, 7.5, 3.3 };

            /*Complex[,] A =
            {
                { 5 ,3, 2, 1},
                { 3, 6, 1, 2},
                { 2, 1, 5, 1},
                { 1, 2, 1, 6},

            };
            Complex[] B = { 1, 4, 7, 2 };*/

            /*Complex[,] A = {
                   {3.5, 1.0, 0.1, -0.1},
                   {3.5, 1.0, 0.1, -0.1},
                   {3.5, 1.0, 0.1, -0.1},
                   {-0.1, 0.2, -0.2, 2.1},
                   };
            Complex[] B = { 0.2, 0.8, 5.9, -2.3 };*/

            Cholesky(A, B);
        }
    }
}
