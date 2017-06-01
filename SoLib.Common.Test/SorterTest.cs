using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        }
    }
}
