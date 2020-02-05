using System;
using System.Text;
using System.Numerics;

namespace CholeskyInverse
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
                    s += A[i, j].ToString("f3") + "\t";
                }
                sb.AppendLine(s);
            }

            Console.WriteLine("{0}\t", sb);
        }

        static void CholeskyInverse(Complex[,] A)
        {
            int n = (int)Math.Sqrt(A.Length);
            Complex[,] S = new Complex[n, n];
            Complex[,] iS = new Complex[n, n];
            Complex[,] iA = new Complex[n, n];

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
            Console.WriteLine("S: ");
            PrintArray(S);

            //Tìm ma trận nghịch đảo của ma trận tam giác dưới
            for (int i = n - 1; i >= 0; i--)
            {
                iS[i, i] = 1.0 / S[i, i];

                for (int j = i + 1; j < n; j++)
                    for (int k = i + 1; j < n; j++)
                        iS[j, i] += iS[j, k] * S[k, i];

                for (int j = i + 1; j < n; j++)
                    iS[j, i] = -iS[i, i] * iS[j, i];
            }
            Console.WriteLine("\nS^(-1): ");
            PrintArray(iS);

            //Tính tích hai ma trận S^(-1) và iS^(-1)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    iA[i, j] = 0;

                    for (int k = 0; k < n; k++)
                        iA[i, j] += iS[k, i] * iS[k, j];
                }
            }

            Console.WriteLine("\nMa tran nghich dao cua A: ");
            PrintArray(iA);
        }


        static void Main(string[] args)
        {
            /*Complex[,] A =
            {
                {1, 3, -2},
                {3, 4, -5},
                {-2, -5, 3},
            };*/

            Complex[,] A =
            {
                {1, 2, 3, 4 },
                {2, 1, 2, 3 },
                {3, 2, 1, 2 },
                {4, 3, 2, 1 },
            };

            /*Complex[,] A =
            {
                { 1 ,4, 1, 3},
                { 0, -1, 3, -1},
                { 3, 1, 0, 2},
                { 1, -2, 5, 1},
            };*/

            /*Complex[,] A ={
                  {4.9, 1.0, 0.1, 1.1},
                  { 1.0, 6.4, 1.2, 0.2},
                  { 0.1, 1.2, 3.6, 1.1},
                  { 1.1, 0.2, 1.1, 6.4},
                  };*/

            /*Complex[,] A = {
                  {13, 14, 6, 4},
                  { 8, -1, 13, 9},
                  { 6, 7, 3, 2},
                  { 9, 5, 16, 11},
                  };*/


            CholeskyInverse(A);
        }
    }
}
