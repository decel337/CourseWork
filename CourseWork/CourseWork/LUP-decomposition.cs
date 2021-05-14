using System;

namespace CourseWork
{
    public class LUP_decomp
    {
        public static double[,] Inversion(double[,] matrix)
        {
            double[,] P = GetMatrixPermutation(matrix);
            (double[,] L, double[,] U) = LU_decomp.Start(MatrixOp.Multiply(P, matrix));
            return LU_decomp.DecisionSystem(matrix, P, L, U);
        }
        public static double[,] GetMatrixPermutation(double[,] matrix)
        {
            double[,] E = MatrixOp.GenerateE(matrix.GetLength(0));
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double pivot = 0;
                int iPivot = i;
                for (int j = i; j < matrix.GetLength(0); j++)
                {
                    if (Math.Abs(matrix[j,i])> pivot)
                    {
                        pivot = Math.Abs(matrix[j, i]);
                        iPivot = j;
                    }
                }

                if (pivot == 0)
                {
                    Console.WriteLine("Singular matrix");
                }

                if (i != iPivot)
                {
                    MatrixOp.SwapRow(E, i, iPivot);
                }
            }

            return E;
        }
    }
}