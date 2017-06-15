using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoLib.Common.Test
{
    [TestClass]
    public class UnionFinderTest
    {
        private readonly BaseUnionFinder _unionFinder;

        public UnionFinderTest()
        {
            _unionFinder = new EagerUnionFinder(new int[] { 0, 1, 2, 3, 4, 5 });
            _unionFinder.Union(0, 3);
            _unionFinder.Union(3, 4);
            _unionFinder.Union(1, 2);
        }

        [TestMethod]
        public void TestUnion()
        {
            _unionFinder.Union(0, 3);
            _unionFinder.Union(3, 4);
            _unionFinder.Union(1, 2);
        }

        [TestMethod]
        public void TestConnected()
        {
            bool isConnected = false;
            isConnected = _unionFinder.IsConnected(0, 4);
            isConnected = _unionFinder.IsConnected(1, 4);
        }
    }
}
