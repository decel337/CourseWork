using System;
using CourseWork;
namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixOp operation = new MatrixOp();
            double[,] matrix1 = new double[,] {{2, 4, 2}, {1, 8, 3}, {9, 3, 1}};//LU
            double[,] matrix3 = new double[,] {{1, 2, 0}, {0, 0, 1}, {1, 1, 9}};//LUP
            double[,] matrix2 = new double[,] {{1}, {4}, {5}};
            (double[,] L, double[,] U) = LU_decomp.Start(matrix3);
            MatrixOp.Print(L);
            Console.WriteLine();
            MatrixOp.Print(U);
        }
    }
}