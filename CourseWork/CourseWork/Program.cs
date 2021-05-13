using System;
using CourseWork;
namespace CourseWork
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix1 = new double[,] {{1, 2, 3}, {1, 4, 5}};
            double[,] matrix2 = new double[,] {{1}, {4}, {5}};
            double[,] matrix = MatrixOperation.Multiply(matrix1, matrix2);
            
        }
    }
}