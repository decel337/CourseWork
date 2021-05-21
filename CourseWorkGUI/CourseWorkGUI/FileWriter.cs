using System.IO;

namespace CourseWorkGUI
{
    public class FileWriter
    {
        public void Write(string path, double[,] matrix)
        {
            using (StreamWriter write = new StreamWriter(path))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string temp = "";
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        temp += matrix[i, j] + " ";
                    }
                    write.WriteLine(temp);
                }
            }
        }
    }
}