using System;

namespace SoLib.Algorithms.UnionFinder
{
    public class LazyUnionFinder : BaseUnionFinder
    {
        public LazyUnionFinder(int[] items) : base(items) { }

        public override bool IsConnected(int index1, int index2)
        {
            return FindRoot(index1) == FindRoot(index2);
        }

        public override void Union(int index1, int index2)
        {
            int root1 = FindRoot(index1);
            int root2 = FindRoot(index2);
            if (root1 != root2)
            {
                Groups[root1] = root2;
            }
        }

        protected virtual int FindRoot(int index)
        {
            if (index == Groups[index])
            {
                return index;
            }
            else
            {
                Groups[index] = Groups[Groups[index]];
                return FindRoot(Groups[index]);
            }
        }
    }
}