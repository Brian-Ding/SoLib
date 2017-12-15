using System;

namespace SoLib.Algorithm.Sorter
{
    public class Sorter
    {
        /// <summary>
        /// Use for array which does not contain too many items.
        /// </summary>
        /// <param name="input">Input array to be sorted.</param>
        /// <param name="nondecreasing">Sorting order, default to nondecreasing</param>
        public void InsertionSort(int[] input, bool nondecreasing = true)
        {
            bool loop;
            int key;
            for (int i = 0; i < input.Length; i++)
            {
                key = input[i];
                // insert the key into sorted sequence input[0]...input[i-1]
                int j = i - 1;
                loop = nondecreasing ? j >= 0 && input[j] > key : j >= 0 && input[j] < key;
                while (loop)
                {
                    // set next number in the array to be this one.
                    input[j + 1] = input[j];
                    // move to the last number.
                    j--;
                }
                input[j + 1] = key;
            }
        }

        public void QuickSort(Int32[] input, Int32 startIndex, Int32 endIndex)
        {
            if (startIndex < endIndex)
            {
                Int32 dividerIndex = Partition(input, startIndex, endIndex);
            }
        }

        private Int32 Partition(Int32[] input, Int32 startIndex, Int32 endIndex)
        {
            Int32 dividerIndex = startIndex - 1;
            Int32 pivot = input[endIndex];

            for (Int32 i = startIndex; startIndex < endIndex; i++)
            {
                if (input[i] <= pivot)
                {
                    dividerIndex++;
                    Int32 temp  = input[dividerIndex];
                    input[dividerIndex] = input[i];
                    input[dividerIndex] = temp;
                }
            }

            return dividerIndex;
        }
    }
}