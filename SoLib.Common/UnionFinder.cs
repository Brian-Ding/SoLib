namespace SoLib.Common
{
    public class UnionFinder
    {
        private int[] IDs { get; set; }

        public UnionFinder(int count)
        {
            IDs = new int[count];
            for (int i = 0; i < count; i++)
            {
                IDs[i] = i;
            }
        }

        // Union is too slow.
        //public void Union(int a, int b)
        //{
        //    if (a != b && IDs[a] != IDs[b])
        //    {
        //        int IDa = IDs[a];
        //        int IDb = IDs[b];

        //        for (int i = 0; i < IDs.Length; i++)
        //        {
        //            IDs[i] = IDs[i] == IDa ? IDa : IDs[i];
        //        }
        //    }
        //}

        //public bool Connected(int a, int b)
        //{
        //    return IDs[a] == IDs[b];
        //}

        public void Union(int a, int b)
        {
            IDs[a] = b;
        }

        private int FindRoot(int a)
        {
            int index = a;
            while (IDs[index] != index)
            {
                index = IDs[index];
            }

            return index;
        }

        public bool Connected(int a, int b)
        {
            return FindRoot(a) == FindRoot(b);
        }
    }
}
