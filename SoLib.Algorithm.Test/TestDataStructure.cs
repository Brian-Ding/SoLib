using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoLib.Algorithm.DataStructure;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoLib.Algorithm.Test
{
    [TestClass]
    public class TestDataStructure
    {
        [TestMethod]
        public void TestStack()
        {
            Stack<Int32> stack = new Stack<int>();
            stack.Add(0);
            stack.Add(1);
            stack.Add(2);
            stack.Add(3);
            stack.Add(4);
            stack.Add(5);

            Int32 integer;
            integer = stack.Take();
            integer = stack.Take();
            integer = stack.Take();
            integer = stack.Take();
            integer = stack.Take();
            integer = stack.Take();
        }

        [TestMethod]
        public Graph TestReadGraph()
        {
            Int32[,] edges = new Int32[7, 2]
            {
                { 0, 1 },
                { 0, 4 },
                { 1, 2 },
                { 1, 3 },
                { 1, 4 },
                { 2, 3 },
                { 3, 4 }
            };
            Graph graph = new Graph(5, false);
            graph.Read(edges);
            graph.Print();
            return graph;
            graph.FindPathByBFS(2, 3);
        }

        [TestMethod]
        public void TestBFS()
        {
            Graph graph = TestReadGraph();
            graph.FindPathByBFS(2, 3);
        }

        [TestMethod]
        public void TestDFS()
        {
            Graph graph = TestReadGraph();
            graph.FindPathByDFS(2, 3);
        }


    }
}
