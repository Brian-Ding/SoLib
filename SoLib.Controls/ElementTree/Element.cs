using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SoLib.Controls.ElementTree
{
    public sealed class Element
    {
        public Guid ID { get; set; }
        private FrameworkElement _content;
        public FrameworkElement Content
        {
            get => _content;
            set
            {
                _content = value;
                Width = _content.Width;
                Height = _content.Height;
            }
        }
        public FrameworkElement Description { get; set; }
        public IList<Element> Children { get; set; }

        internal Double Width { get; set; }
        internal Double Height { get; set; }
        internal Double Left { get; set; }
        internal Double Top { get; set; }

        public Element(Guid id, FrameworkElement content, FrameworkElement description, IList<Element> children)
        {
            ID = id;
            Content = content;
            Description = description;
            Children = children == null ? new List<Element>() : children;
        }
    }
}
