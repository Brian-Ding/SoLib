using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SoLib.Common.Test
{
    [TestClass]
    public class UnionFinderTest
    {
        private readonly UnionFinder _unionFinder;

        public UnionFinderTest()
        {
            _unionFinder = new UnionFinder(5);
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
            isConnected = _unionFinder.Connected(0, 4);
            isConnected = _unionFinder.Connected(1, 4);
        }
    }
}
