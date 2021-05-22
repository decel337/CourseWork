namespace CourseWorkGUI
{
    public class InversionEdging
    {
        public static double[,] Inversion(double[,] matrix)
        {
            double[,] P = InversionLUP.GetMatrixPermutation(matrix);
            
            matrix = MatrixOp.Multiply(P, matrix);
            
            double[,] matrixInversion = new double[,] {{1 / matrix[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1]}};
            
            for (int i = 2; i < matrix.GetLength(0)+1; i++)
            {
                matrixInversion = InversionMiniMatrix(matrix, i, matrixInversion);
            }

            return MatrixOp.Multiply(matrixInversion, P);
        }

        private static double[,] InversionMiniMatrix(double[,] matrix, int i, double[,] inv)
        {
            double[,] matrixInversion = new double[i,i];//4
            double[,] matrix1 = new double[,] {{matrix[matrix.GetLength(0) - i, matrix.GetLength(0) - i]}};
            double[,] matrix2 = new double[1, i - 1];
            double[,] matrix3 = new double[i - 1, 1];
            for (int j = matrix.GetLength(0) - i+1; j < matrix.GetLength(0); j++)
            {
                matrix2[0, j-1 + i - matrix.GetLength(0)] = matrix[matrix.GetLength(0) - i, j];
                matrix3[j-1 + i - matrix.GetLength(0), 0] = matrix[j, matrix.GetLength(0) - i];
            }
            double[,] A = new double[,] {{1/(MatrixOp.Add(matrix1, MatrixOp.Multiply(MatrixOp.Multiply(matrix2, inv), matrix3), "-")[0,0])}};
            double[,] B = MatrixOp.Negative(MatrixOp.Multiply(MatrixOp.Multiply(A, matrix2), inv));
            double[,] C = MatrixOp.Negative(MatrixOp.Multiply(MatrixOp.Multiply(inv, matrix3), A));
            double[,] D = MatrixOp.Add(inv, MatrixOp.Multiply(MatrixOp.Multiply(inv, matrix3), B), "-");
            matrixInversion[0, 0] = A[0, 0];
            for (int j = 1; j < i; j++)
            {
                matrixInversion[0, j] = B[0, j - 1];
                matrixInversion[j, 0] = C[j - 1, 0];
            }

            for (int j = 0; j < D.GetLength(0); j++)
            {
                for (int k = 0; k < D.GetLength(1); k++)
                {
                    matrixInversion[j+1, k+1] = D[j, k];
                }
            }
            return matrixInversion;
        }
    }
}