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
    }
}
