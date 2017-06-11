using System;

namespace SoLib.Common
{
    public class EagerUnionFinder : BaseUnionFinder
    {
        public EagerUnionFinder(int[] items) : base(items) { }

        public override bool IsConnected(int index1, int index2)
        {
            return Groups[index1] == Groups[index2];
        }

        public override void Union(int index1, int index2)
        {
            //throw new NotImplementedException();
            if (index1 == index2 && !IsConnected(index1, index2))
            {
                int item1 = Groups[index1];
                int item2 = Groups[index2];

                for (int i = 0; i < Groups.Length; i++)
                {
                    if (IsConnected(i, index2))
                    {
                        Groups[i] = item1;
                    }
                }
            }

        }
    }
}
