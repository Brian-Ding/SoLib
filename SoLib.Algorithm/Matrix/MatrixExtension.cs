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

        public static Int32[] SubArray(this Int32[] array, Int32 startIndex, Int32 endIndex)
        {
            Int32[] output = new Int32[endIndex - startIndex + 1];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = array[startIndex + i];
            }

            return output;
        }
    }
}
