using System.IO;

namespace CourseWorkGUI
{
    public class FileWriter
    {
        /// <summary>
        /// Write in file result matrix
        /// </summary>
        public static void Write(string path, double[,] matrix)
        {
            using (StreamWriter write = new StreamWriter(path))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string temp = "";
                    for (int j = 0; j < matrix.GetLength(1)-1; j++)
                    {
                        temp += matrix[i, j] + " ";
                    }

                    temp += matrix[i, matrix.GetLength(0) - 1];
                    write.WriteLine(temp);
                }
            }
        }
    }
}