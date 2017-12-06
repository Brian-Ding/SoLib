using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SoLib.Controls.ElementTree
{
    public sealed class ElementTree : Canvas
    {
        private const Double MARGIN = 5;

        public Element RootElement
        {
            get { return (Element)GetValue(RootElementProperty); }
            set { SetValue(RootElementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RootElement.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty RootElementProperty =
            DependencyProperty.Register(nameof(RootElement), typeof(Element), typeof(ElementTree), new PropertyMetadata(null, OnRootElementChanged));

        private static void OnRootElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ElementTree elementTree = d as ElementTree;
            elementTree.Children.Clear();
            elementTree.Width = elementTree.SetPosition(elementTree.RootElement, 0, 0);
            elementTree.Height = elementTree.Draw(elementTree.RootElement);
            elementTree.Background = new SolidColorBrush(Colors.Azure);
        }

        private Double SetPosition(Element element, Double left, Double top)
        {
            Double width = 0;
            Double childCount = element.Children.Count;

            for (int i = 0; i < childCount; i++)
            {
                Element child = element.Children[i];
                Double childWidth = SetPosition(child, left + width, top + child.Height);
                child.Left = left + width + (childWidth - child.Width) / 2;
                child.Top = top + element.Height;
                width += childWidth;
            }

            width += width == 0 ? element.Width : 0;
            element.Left = left + (width - element.Width) / 2;
            element.Top = top;

            return width;
        }

        private Double Draw(Element element)
        {
            Double height = 0;

            Children.Add(element.Content);
            SetLeft(element.Content, element.Left);
            SetTop(element.Content, element.Top);
            height = Math.Max(height, (element.Top + element.Height));

            for (int i = 0; i < element.Children.Count; i++)
            {
                height = Math.Max(height, Draw(element.Children[i]));
            }

            return height;
        }
    }
}
