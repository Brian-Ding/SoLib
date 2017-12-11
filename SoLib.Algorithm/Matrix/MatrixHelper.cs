using System;

namespace SoLib.Algorithm.Matrix
{
    public static class MatrixExtension
    {
        public static String Print(this Double[,] matrix)
        {
            String content = String.Empty;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    content += matrix[i, j].ToString() + "\t";
                }

                content += "\n";
            }

            return content;
        }
    }
}
