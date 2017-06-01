using System;
using System.Collections;
using System.Collections.Generic;

namespace SoLib.Common
{
    public class LinkedListNode<T>
    {
        /// <summary>
        /// Value of this node.
        /// </summary>
        /// <returns></returns>
        public T Value { get; set; }

        /// <summary>
        /// Construct a node with specified value.
        /// </summary>
        /// <param name="value"></param>
        public LinkedListNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// The next node in the linked list. (null if last node.)
        /// </summary>
        /// <returns></returns>
        public LinkedListNode<T> Next{get;set;}
    }

    public class ListList<T> : ICollection<T>
    {
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}