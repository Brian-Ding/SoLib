using System;

namespace SoLib.Algorithm.DataStructure
{
    public class BinaryTreeNode<T>
    {
        public T Item { get; private set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode(T item)
        {
            Item = item;
        }
    }

    public class BinaryTree<T> where T : IEquatable<T>
    {
        public BinaryTreeNode<T> Root { get; protected set; }

        public BinaryTree(BinaryTreeNode<T> root)
        {
            Root = root;
        }

        public virtual void Add(BinaryTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        public virtual BinaryTreeNode<T> Search(T item)
        {
            return Search(Root, item);
        }

        protected virtual BinaryTreeNode<T> Search(BinaryTreeNode<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Item.Equals(item))
            {
                return node;
            }
            else
            {
                BinaryTreeNode<T> result = Search(node.Left, item);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return Search(node.Right, item);
                }
            }

        }
    }
}
