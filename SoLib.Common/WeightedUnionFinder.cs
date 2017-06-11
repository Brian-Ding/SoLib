using System;

namespace SoLib.Common
{
    public class WeightedUnionFinder : LazyUnionFinder
    {
        protected int[] Size { get; set; }

        public WeightedUnionFinder(int[] items) : base(items)
        {
            Size = new int[items.Length];
            for (int i = 0; i < Size.Length; i++)
            {
                Size[i] = 1;
            }
        }

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
                if (Size[root1] >= Size[root2])
                {
                    Groups[root2] = root1;
                    Size[root1] += Size[root2];
                }
            }
        }
    }
}