using System.Collections.Generic;

namespace SoLib.Common
{
    public class Node<T> : NotifyItem
    {
        private T _data;
        public T Data
        {
            get
            {
                return _data;
            }
            private set
            {
                SetValue(ref _data, value);
            }
        }

        private List<Node<T>> _children;
        public List<Node<T>> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<Node<T>>();
                }

                return _children;
            }
            private set
            {
                SetValue(ref _children, value);
            }
        }
    }
}
