using System;

namespace NoiSuyNguoc
{
    class Moc_Bat_Ky
    {
        static double[,] Tysaiphan(double[] X, double[] Y)
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
                    A[i, j] = (A[i + 1, j] - A[i, j - 1]) / (X[j] - X[i]);
                    j++;
                    i++;
                }
            }
            return A;
        }

    }
}
