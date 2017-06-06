using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace SoLib.Common.Test
{
    [TestClass]
    public class SorterTest
    {
        [TestMethod]
        public void TestInsertionSort()
        {
            int[] input = new int[5] { 2, 5, 3, 4, 1 };
            Sorter.InsertionSort(ref input);
            Debug.WriteLine("==== TestInsertionSort ====");
            foreach (int num in input)
            {
                Debug.Write(num);
            }
            Debug.WriteLine(Environment.NewLine + Environment.NewLine);
        }
    }
}
