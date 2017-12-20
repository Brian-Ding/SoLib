using SoLib.Algorithm.Matrix;
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
                QuickSort(input, startIndex, dividerIndex - 1);
                QuickSort(input, dividerIndex + 1, endIndex);
            }
        }

        private Int32 Partition(Int32[] input, Int32 startIndex, Int32 endIndex)
        {
            Int32 dividerIndex = startIndex - 1;
            Int32 pivot = input[endIndex];
            Int32 temp;

            for (Int32 i = startIndex; i < endIndex; i++)
            {
                if (input[i] <= pivot)
                {
                    dividerIndex++;
                    temp = input[dividerIndex];
                    input[dividerIndex] = input[i];
                    input[i] = temp;
                }
            }
            dividerIndex++;
            temp = input[dividerIndex];
            input[dividerIndex] = input[endIndex];
            input[endIndex] = temp;

            return dividerIndex;
        }

        public void MergeSort(Int32[] input)
        {
            Merge(input.SubArray(0, (Int32)Math.Floor((Double)input.Length / 2)), input.SubArray((Int32)Math.Floor((Double)input.Length / 2) + 1, input.Length - 1));
        }

        private Int32[] Merge(Int32[] input1, Int32[] input2)
        {
            Int32[] output = new Int32[input1.Length + input2.Length];
            Int32 index1 = 0, index2 = 0;

            for (int i = 0; i < output.Length; i++)
            {
                if (input1[index1] < input2[index2])
                {
                    output[i] = input1[index1];
                    index1++;
                }
                else
                {
                    output[i] = input2[index2];
                    index2++;
                }
            }

            return output;
        }
    }
}