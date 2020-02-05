using System;


namespace VienQuanh
{
    class Program
    {
        static double[][] MatrixCreate(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        static double[][] MatrixSum(double[][] a, double[][] b, int rows, int cols)
        {
            double[][] result = MatrixCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = a[i][j] + b[i][j];

            return result;
        }

        static double[][] MatrixSubtr(double[][] a, double[][] b, int rows, int cols)
        {
            double[][] result = MatrixCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = a[i][j] - b[i][j];

            return result;
        }

        static double[][] MatrixProduct(double[][] matrixA, double[][] matrixB)
        {
            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            if (aCols != bRows)
                throw new Exception("Ma tran khong hop le");
            double[][] result = MatrixCreate(aRows, bCols);
            for (int i = 0; i < aRows; ++i)
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
            return result;
        }

        static void Display(double[][] a, int rows, int cols)
        {
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    if (j == cols - 1) Console.WriteLine("{0}\t", Math.Round(a[i][j],5));
                    else Console.Write("{0},\t", Math.Round(a[i][j], 5));
        }

        static double[][] Slide(double[][] a, int n)
        {
            double[][] result = MatrixCreate(n - 1, n - 1);
            for (int i = 0; i < n - 1; ++i)
                for (int j = 0; j < n - 1; ++j)
                    result[i][j] = a[i][j];


            return result;
        }

        static double[][] MatrixInverse(double[][] a, int n)
        {
            double[][] result = MatrixCreate(n, n);
            if (n == 1)
            {
                result[0][0] = 1 / a[0][0];
                return result;
            }
            if (n == 2)
            {
                double m = a[0][0] * a[1][1] - a[0][1] * a[1][0];
                result[0][0] = a[1][1] / m;
                result[0][1] = -a[0][1] / m;
                result[1][0] = -a[1][0] / m;
                result[1][1] = a[0][0] / m;
                return result;
            }
            else
            {
                var b11 = MatrixCreate(n - 1, n - 1);
                var b12 = MatrixCreate(n - 1, 1);
                var b21 = MatrixCreate(1, n - 1);
                var b22 = MatrixCreate(1, 1);

                var a11 = MatrixCreate(n - 1, n - 1);
                var a12 = MatrixCreate(n - 1, 1);
                var a21 = MatrixCreate(1, n - 1);
                var a22 = MatrixCreate(1, 1);

                a11 = Slide(a, n);
                for (int i = 0; i < n - 1; ++i)
                {
                    a12[i][0] = a[i][n - 1];
                    a21[0][i] = a[n - 1][i];
                }
                a22[0][0] = a[n - 1][n - 1];

                var X = MatrixCreate(n - 1, 1);
                var Y = MatrixCreate(1, n - 1);
                var theta = MatrixCreate(1, 1);

                var ia11 = MatrixCreate(n - 1, n - 1);
                ia11 = MatrixInverse(a11, n - 1);

                Console.WriteLine("\nMoi lan vien quanh");
                Display(ia11, n-1, n-1);

                X = MatrixProduct(ia11, a12);
                Y = MatrixProduct(a21, ia11);
                theta = MatrixSubtr(a22, MatrixProduct(Y, a12), 1, 1);


                var mX = MatrixCreate(n - 1, 1);
                var mY = MatrixCreate(1, n - 1);
                for (int i = 0; i < n - 1; ++i)
                {
                    mX[i][0] = -X[i][0];
                    mY[0][i] = -Y[0][i];
                }

                var itheta = MatrixCreate(1, 1);
                itheta = MatrixInverse(theta, 1);
               
                b11 = MatrixSum(ia11, MatrixProduct(MatrixProduct(X, itheta), Y), n - 1, n - 1);
                b12 = MatrixProduct(mX, itheta);
                b21 = MatrixProduct(itheta, mY);
                b22 = itheta;

                for (int i = 0; i < n - 1; ++i)
                {
                    for (int j = 0; j < n - 1; ++j)
                    {
                        result[i][j] = b11[i][j];
                    }
                    result[n - 1][i] = b21[0][i];
                    result[i][n - 1] = b12[i][0];
                    result[n - 1][n - 1] = b22[0][0];
                }

                return result;
            }
        }

        static void Main(string[] args)
        {
            /*double[,] a = {
                   {1, 4, 1, 3},
                   {0, -1, 3, -1},
                   {3, 1, 0, 2},
                   {1, -2, 5, 1},
                   };*/

            double[,] a = {
                   {211, 22, -13, 24, 15, -26,17,28 },
                   {22,433,24,35,26,37,28,-39},
                   {33,-24,235,-26,37,28,-39,20},
                   {14,45,26,247,38,49,40,-41},
                   {-55,16,57,28,259,30,-51,42},
                   {46,27,-48,39,40,261,41,73},
                   {27,-58,29,70,-21,42,223,34},
                   {38,59,60,-71,82 ,-93,24,215},
                   };


            int n = (int)Math.Sqrt(a.Length);

            var A = MatrixCreate(n, n);
            var iA = MatrixCreate(n, n);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    A[i][j] = a[i, j];
                }
            }

            Console.WriteLine("\nMa tran vua nhap: ");
            Display(A, n, n);

            iA = MatrixInverse(A, n);
            Console.WriteLine("\nMa tran nghich dao: ");
            Display(iA, n, n);
        }
    }
}
