
using System;

namespace CourseWorkGUI
{
    public class InversionLUP
    {
        /// <summary>
        /// Inversion matrix method LUP
        /// </summary>
        public static double[,] Inversion(double[,] matrix)
        {
            double[,] P = GetMatrixPermutation(matrix);
            (double[,] L, double[,] U) = InversionLU.Decomposition(MatrixOp.Multiply(P, matrix));
            return InversionLU.DecisionSystem(matrix, P, L, U);
        }

        /// <summary>
        /// Get matrix permutation for input matrix
        /// </summary>
        public static double[,] GetMatrixPermutation(double[,] matrix)
        {
            double[,] E = GenerateE(matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double pivot = 0;
                int iPivot = i;
                for (int j = i; j < matrix.GetLength(0); j++)
                {
                    if (Math.Abs(matrix[j, i]) > pivot)
                    {
                        pivot = Math.Abs(matrix[j, i]);
                        iPivot = j;
                    }
                }

                if (pivot == 0)
                {
                    return null;
                }

                if (i != iPivot)
                {
                    MatrixOp.SwapRow(E, i, iPivot);
                }
            }

            return E;
        }
        
        /// <summary>
        /// Generate singular matrix
        /// </summary>
        public static double[,] GenerateE(int a)
        {
            double[,] E = new double[a,a];
            for (int i = 0; i < a; i++)
            {
                E[i, i] = 1;
            }

            return E;
        }
    }
}