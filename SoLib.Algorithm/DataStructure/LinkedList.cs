using System;

namespace SoLib.Algorithm.DataStructure
{
    public class LinkedNode<T>
    {
        public T Item { get; set; }
        public LinkedNode<T> Next { get; set; }

        public LinkedNode(T item)
        {
            Item = item;
        }

        public LinkedNode(T item, LinkedNode<T> next)
        {
            Item = item;
            Next = next;
        }
    }

    public class LinkedList<T> where T : IEquatable<T>
    {
        private LinkedNode<T> _head;

        public void InsertEnd(T item)
        {
            if (_head == null)
            {
                _head = new LinkedNode<T>(item);
            }
            else
            {
                LinkedNode<T> current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new LinkedNode<T>(item);
            }
        }

        public void InsertStart(T item)
        {
            if (_head == null)
            {
                _head = new LinkedNode<T>(item);
            }
            else
            {
                _head = new LinkedNode<T>(item, _head);
            }
        }

        public void Delete(T item)
        {
            if (_head != null)
            {
                if (_head.Item.Equals(item))
                {
                    _head = null;
                }
                else
                {
                    LinkedNode<T> current = _head;
                    while (current.Next != null)
                    {
                        if (current.Next.Item.Equals(item))
                        {
                            current.Next = current.Next.Next;
                            break;
                        }
                    }
                }
            }
        }

        public LinkedNode<T> Search(T item)
        {
            LinkedNode<T> current = _head;
            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return current;
                }
                else
                {
                    current = current.Next;
                }
            }

            throw new ArgumentOutOfRangeException("Does not find the item.");
        }
    }
}
