using System;

namespace SoLib.Algorithms.UnionFinder
{
    public class Percolater : WeightedUnionFinder
    {
        public Percolater(int[] items) : base(items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = -1;
            }

            int width = (int)Math.Sqrt(items.Length - 2);
            for (int i = 0; i < width; i++)
            {
                Union(i + 1, 0);
            }

            for (int i = items.Length; i > items.Length - width; i++)
            {
                Union(i, items.Length - 1);
            }
        }

        public void Reveal(int index)
        {
            Groups[index] = index;
        }

        public override bool IsConnected(int index1, int index2)
        {
            return base.IsConnected(index1, index2) && FindRoot(index1) != -1;
        }

        public bool IsPercolated()
        {
            return IsConnected(0, Groups.Length - 1);
        }

    }
}