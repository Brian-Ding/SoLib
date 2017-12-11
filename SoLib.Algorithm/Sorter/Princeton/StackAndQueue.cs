using System;
using System.Collections;
using System.Collections.Generic;

namespace SoLib.Algorithm.Princeton
{
    public class LinkedNode<T>
    {
        public T Value { get; set; }
        public LinkedNode<T> Next { get; set; }
    }

    public class Deque<T> : IEnumerable<T>
    {
        private LinkedNode<T> _head;
        private LinkedNode<T> _tail;
        private int _size;

        public bool IsEmpty()
        {
            return _head == null;
        }

        public int Size()
        {
            return _size;
        }

        public void AddFirst(T value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }

            LinkedNode<T> addingNode = new LinkedNode<T>()
            {
                Value = value
            };

            if (_head == null)
            {
                _head = addingNode;
                _tail = addingNode;
            }
            else
            {
                addingNode.Next = _head;
                _head = addingNode;
            }
        }

        public void AddLast(T value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }

            LinkedNode<T> addingNode = new LinkedNode<T>()
            {
                Value = value
            };

            if (_head == null)
            {
                _head = addingNode;
                _tail = addingNode;
            }
            else
            {
                _tail.Next = addingNode;
                _tail = addingNode;
            }
        }

        public T RemoveFirst()
        {
            if (_head == null)
            {
                throw new NullReferenceException();
            }

            LinkedNode<T> secondNode = _head.Next;
            _head.Next = null;
            _head = secondNode;

            return _head.Value;
        }

        public T RemoveLast()
        {
            if (_tail == null)
            {
                throw new NullReferenceException();
            }

            if (_head.Next == null)
            {
                _head = null;
                _tail = null;
                return default(T);
            }
            else
            {
                LinkedNode<T> current = _head;
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }
                current.Next = null;
                _tail = current;
                return _tail.Value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}