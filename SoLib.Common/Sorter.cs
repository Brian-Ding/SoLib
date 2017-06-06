namespace SoLib.Common
{
    public class Sorter
    {
        /// <summary>
        /// Use for array which does not contain too many items.
        /// </summary>
        /// <param name="input"></param>
        public static void InsertionSort(ref int[] input)
        {
            int key;
            for (int i = 0; i < input.Length; i++)
            {
                key = input[i];
                // insert the key into sorted sequence input[0]...input[i-1]
                int j = i - 1;
                while (j >= 0 && input[j] > key)
                {
                    // set next number in the array to be this one.
                    input[j + 1] = input[j];
                    // move to the last number.
                    j--;
                }
                input[j + 1] = key;
            }
        }
    }
}