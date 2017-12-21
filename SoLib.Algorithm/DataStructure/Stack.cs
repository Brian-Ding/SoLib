using System;

namespace SoLib.Algorithm.DataStructure
{
    public class Stack<T>
    {
        private T[] _items;

        public Int32 Count { get; private set; }

        public Stack()
        {
            _items = new T[1];
            Count = 0;
        }

        public void Add(T item)
        {
            Count++;

            if (Count > _items.Length)
            {
                T[] temp = new T[_items.Length * 2];
                _items.CopyTo(temp, 0);
                _items = temp;
            }

            _items[Count - 1] = item;
        }

        public T Take()
        {
            T item = _items[--Count];

            if (Count < _items.Length / 2)
            {
                T[] temp = new T[_items.Length / 2];
                Copy(_items, temp, 0, temp.Length - 1);
                _items = temp;
            }

            return item;
        }

        private void Copy(T[] from, T[] to, Int32 startIndex, Int32 endIndex)
        {
            if (startIndex > from.Length - 1 || endIndex > from.Length - 1 || endIndex < startIndex || endIndex - startIndex + 1 > to.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                to[i - startIndex] = from[i];
            }
        }
    }
}
