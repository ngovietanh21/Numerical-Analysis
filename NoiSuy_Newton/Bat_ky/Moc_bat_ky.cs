using System;

namespace Moc_bat_ky
{
    class Program
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

        static double[,] SaiPhan_ThemMoc(double[,] Tysaiphan, double[] X, double Xn, double Yn)
        {
            int n = Tysaiphan.GetLength(1);
            double[,] A = new double[n + 1, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j >= i) A[i, j] = Tysaiphan[i, j];
                  
                }
            }
            A[n, n] = Yn;
            for(int i = n - 1; i >= 0; i--)
            {
                A[i,n] = (A[i + 1, n] - A[i, n - 1]) / (Xn - X[i]);
            }
            return A;
        }

        static double[] Da_thuc_Newton(double[,] Tysaiphan, double[] X)
        {
            int n = X.Length;
            double[] Pn = new double[n];
            int k = 0;

            for(int i = n-1; i >= 0; i--)
            {
                if (i == 0)
                {
                    Pn[n - 1] += Tysaiphan[0, 0];
                }

                if (i == 1)
                {
                    Pn[n - 1] -= Tysaiphan[0, 1] * X[0];
                    Pn[n - 2] += Tysaiphan[0, 1]; 
                }

                if (i > 1)
                {
                    double[] Xi = new double[i];

                    for (int j = 0; j < i; j++)
                    {
                        Xi[j] = X[j];

                    }

                    double[] Horner = HornerPn.Nhandathuc(Xi);

                    for (int j = 0; j < Horner.Length; j++)
                    {
                        Horner[j] *= Tysaiphan[0, i];
                        Pn[j+k] += Horner[j];
                    }
                    k++;
                }

               
            }

            return Pn;
        }

        static void Main(string[] args)
        {
            double[] X = { 3631      ,  7292     ,  11454   ,    15824   ,    21126 };
            double[] Y = { 25.5000  , 22.5000 ,  19.5000  , 16.5000  , 13.5000 };

            double[,] A = Tysaiphan(X, Y);
            double[] B = Da_thuc_Newton(A, X);
            double[,] C = SaiPhan_ThemMoc(A, X, 50, 1000);

            ExportData.ExportData2D(C);
            ExportData.ExportData1D(B);
            Console.WriteLine(HornerPn.Giatridathuc(B, 7222));
          
        }
    }
}
