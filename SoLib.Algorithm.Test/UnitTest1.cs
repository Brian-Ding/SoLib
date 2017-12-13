using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoLib.Algorithm.Matrix;
using System;

namespace SoLib.Algorithm.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Double[,] matrix = new Double[4, 4]
            //{
            //    { 1,0.5302358,0.7561642,0.3645064 },
            //    { 0.5302358,1,0.3779162,0.4705346},
            //    { 0.7561642,0.3779162,1,0.4844589},
            //    { 0.3645064,0.4705346,0.4844589,1}
            //};
            Double[,] matrix = new Double[2, 2]
            {
                { 3,2},
                { 2,1}
            };
            new EigenSolver(matrix).Solve();
        }
    }
}
