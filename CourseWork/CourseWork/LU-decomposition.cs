namespace CourseWork
{
    public class LU_decomp
    {
        public static (double[,],double[,]) Start(double[,] matrix)
        {
            double[,] L = new double[matrix.GetLength(0),matrix.GetLength(1)];
            double[,] U = new double[matrix.GetLength(0),matrix.GetLength(1)];

            int k = 0;

            while(k != matrix.GetLength(0))
            {
                for (int i = 0; i < k+1; i++)
                {
                    U[i, k] = (matrix[i, k] - Sum(L, U, i, k, "u"));
                }
                
                for (int i = k; i < matrix.GetLength(0); i++)
                {

                    L[i, k] = (matrix[i, k] - Sum(L, U, i, k, "l"))/ U[k, k];
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

            return result;
        }
    }
}