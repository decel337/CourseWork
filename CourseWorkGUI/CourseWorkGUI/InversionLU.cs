namespace CourseWorkGUI
{
    public class InversionLU
    {
        public static double[,] Inversion(double[,] matrix)
        {
            if (Start(matrix) != (null, null))
            {
                (double[,] L, double[,] U) = Start(matrix);
                double[,] E = InversionLUP.GenerateE(matrix.GetLength(0));
                return DecisionSystem(matrix, E, L, U);
            }
            else
            {
                return null;
            }
            
        }

        public static double[,] DecisionSystem(double[,] matrix, double[,] E, double[,] L, double[,] U)
        {
            double[,] Y = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double[,] X = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    Y[i, j] = (E[i, j] - Sum(L, Y, i, j, "u"));
                }
            }

            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                for (int i = matrix.GetLength(1) - 1; i >= 0; i--)
                {
                    X[i, j] = (Y[i, j] - Sum(U, X, i, j, "x")) / U[i, i];
                }
            }

            return X;
        }

        public static (double[,], double[,]) Start(double[,] matrix)
        {
            double[,] L = new double[matrix.GetLength(0), matrix.GetLength(1)];
            double[,] U = new double[matrix.GetLength(0), matrix.GetLength(1)];

            int k = 0;

            while (k != matrix.GetLength(0))
            {
                for (int i = 0; i < k + 1; i++)
                {
                    U[i, k] = (matrix[i, k] - Sum(L, U, i, k, "u"));
                }

                for (int i = k; i < matrix.GetLength(0); i++)
                {
                    if (U[k,k] == 0)
                    {
                        return (null,null);
                    }
                    L[i, k] = (matrix[i, k] - Sum(L, U, i, k, "l")) / U[k, k];
                }

                k += 1;
            }

            return (L, U);
        }

        private static double Sum(double[,] L, double[,] U, int point1, int point2, string op)
        {
            double result = 0;
            if (op == "u")
            {
                for (int m = 0; m < point1; m++)
                {
                    result += L[point1, m] * U[m, point2];
                }
            }

            if (op == "l")
            {
                for (int m = 0; m < point2; m++)
                {
                    result += L[point1, m] * U[m, point2];
                }
            }

            if (op == "x")
            {
                for (int i = point1; i < L.GetLength(0); i++)
                {
                    result += L[point1, i] * U[i, point2];
                }
            }

            return result;
        }
    }
}