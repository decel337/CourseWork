namespace CourseWorkGUI
{
    public class MatrixOp
    {
        /// <summary>
        /// Multiplies matrixes
        /// </summary>
        public static double[,] Multiply(double[,] matrix1, double[,] matrix2)
        {
            double[,] matrix = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int p = 0; p < matrix2.GetLength(1); p++)
                {
                    double result = 0;
                    for (int j = 0; j < matrix2.GetLength(0); j++)
                    {
                        result += matrix1[i, j] * matrix2[j, p];
                    }

                    matrix[i, p] = result;
                }
            }

            return matrix;
        }
        
        /// <summary>
        /// Addition and subtractions matrixes
        /// </summary>
        public static double[,] Add(double[,] matrix1, double[,] matrix2, string op = "+")
        {
            double[,] matrix = new double[matrix1.GetLength(0),matrix2.GetLength(1)];
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    if (op == "+")
                    {
                        matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                    else if (op == "-")
                    {
                        matrix[i, j] = matrix1[i, j] - matrix2[i, j];
                    }
                }
            }
            return matrix;
        }
        
        /// <summary>
        /// Multiply matrix on -E (singular matrix)
        /// </summary>
        public static double[,] Negative(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = -matrix[i,j];
                }
            }

            return matrix;
        }
        
        /// <summary>
        /// Swap two row in matrix
        /// </summary>
        public static void SwapRow(double[,] matrix, int row1, int row2)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }

        public static double[,] CopyMatrix(double[,] matrix1)
        {
            double[,] matrix = new double[matrix1.GetLength(0),matrix1.GetLength(0)];//copy matrix

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix[i, j] = matrix1[i, j];
                }
            }
            return matrix;
        }
    }
}