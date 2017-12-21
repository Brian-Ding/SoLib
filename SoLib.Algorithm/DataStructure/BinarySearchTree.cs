using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoLib.Algorithm.DataStructure
{
    public class BinarySearchTree<T> : BinaryTree<T> where T : IEquatable<T>
    {
        public BinarySearchTree(BinaryTreeNode<T> root) : base(root) { }

        public override void Add(BinaryTreeNode<T> node)
        {
            if (Root == null)
            {
                Root = node;
            }
        }
    }
}
