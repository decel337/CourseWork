using System.Globalization;
using System.IO;

namespace CourseWorkGUI
{
    public class FileReader
    {
        public static double[,] Read(string path)
        {
            double[,] matrix = new double[1, 1];
            using (StreamReader read = new StreamReader(path))
            {
                string temp = read.ReadLine();
                string[] tempArr = temp.Split(' ');
                matrix = new double[tempArr.Length, tempArr.Length];
            }
            
            using (StreamReader read = new StreamReader(path))
            {
                double num = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string temp = read.ReadLine();
                    string[] tempArr = temp.Split(' ');
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (double.TryParse(tempArr[j],NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                        {
                            matrix[i, j] = num;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

            }

            return matrix;
        }
    }
}