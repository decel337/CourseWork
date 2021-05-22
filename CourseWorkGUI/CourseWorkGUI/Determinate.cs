using System;
using System.Windows.Media;

namespace CourseWorkGUI
{
    public class Determinate
    {
        public static bool IsZero(double[,] matrix)
        {
            double det = 1;

            const double EPS = 1E-9;

            int n = matrix.GetLength(0);

            for (int i=0; i<n; ++i) 
            {
                int k = i;

                for (int j = i + 1; j < n; ++j)
                {
                    if (Math.Abs(matrix[j, i]) > Math.Abs(matrix[k, i]))
                    {
                        k = j;
                    }
                }

                if (Math.Abs(matrix[k,i]) < EPS)
                {
                    return true;
                }

                MatrixOp.SwapRow(matrix, i, k);

                if (i != k)
                {
                    det = -det;
                }

                det *= matrix[i,i];

                for (int j = i + 1; j < n; ++j)
                {
                    matrix[i, j] /= matrix[i, i];
                }

                for (int j = 0; j < n; ++j)
                {
                    if ((j != i) && (Math.Abs(matrix[j, i]) > EPS))
                    {
                        for (k = i + 1; k < n; ++k)
                        {
                            matrix[j, k] -= matrix[i, k] * matrix[j, i];
                        }
                    }
                }
            }
            return false;
        }
    }
}