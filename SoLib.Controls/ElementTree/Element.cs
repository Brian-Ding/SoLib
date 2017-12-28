using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        public IList<Element> Children { get; set; }

        internal Double Width { get; set; }
        internal Double Height { get; set; }
        internal Double Left { get; set; }
        internal Double Top { get; set; }

        public Element(Guid id, FrameworkElement content, Object tooltip)
        {
            ID = id;
            Content = content;
            ToolTipService.SetToolTip(Content, tooltip);
            Children = new List<Element>();
        }

        public Element(Guid id, FrameworkElement content, Object tooltip, IList<Element> children)
        {
            ID = id;
            Content = content;
            ToolTipService.SetToolTip(Content, tooltip);
            Children = children;
        }
    }
}
