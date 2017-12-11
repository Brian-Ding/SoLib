namespace SoLib.Algorithm.UnionFinder
{
    public abstract class BaseUnionFinder
    {
        protected int[] Groups { get; set; }

        public BaseUnionFinder(int[] items)
        {
            Groups = items;
        }

        /// <summary>
        /// Connect two items.
        /// </summary>
        /// <param name="index1">Index of the first item</param>
        /// <param name="index2">Index of the second item</param>
        public abstract void Union(int index1, int index2);

        /// <summary>
        /// Check if the two items are connected.
        /// </summary>
        /// <param name="index1">Index of the first item</param>
        /// <param name="index2">Index of the second item</param>
        /// <returns></returns>
        public abstract bool IsConnected(int index1, int index2);
    }
}