using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoLib.Algorithm.Test
{
    [TestClass]
    public class TestSortUnit
    {
        private bool Verify(Int32[] output)
        {
            for (int i = 0; i < output.Length - 1; i++)
            {
                if (output[i] > output[i + 1])
                {
                    return false;
                }
            }

            return true;
        }

        [DataTestMethod]
        [DataRow(new Int32[] { 2, 8, 7, 1, 3, 5, 6, 4 })]
        public void TestQuickSort(Int32[] input)
        {
            new Sorter.Sorter().QuickSort(input, 0, input.Length - 1);
            Assert.IsTrue(Verify(input));
        }

        [DataTestMethod]
        [DataRow(new Int32[] { 2, 8, 7, 1, 3, 5, 6, 4 })]
        public void TestMergeSort(Int32[] input)
        {
            new Sorter.Sorter().MergeSort(input);
        }
    }
}