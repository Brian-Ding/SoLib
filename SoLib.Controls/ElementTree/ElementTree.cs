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
            elementTree.Width = elementTree.SetLeft(elementTree.RootElement);
            elementTree.Height = elementTree.SetTop(elementTree.RootElement);
            elementTree.Draw(elementTree.RootElement);
            elementTree.Background = new SolidColorBrush(Colors.Azure);
        }

        private Double SetLeft(Element element, Double leftMargin = 0)
        {
            Double childWidth = 0;
            Int32 childCount = element.Children.Count;

            for (int i = 0; i < childCount; i++)
            {
                childWidth += SetLeft(element.Children[i], childWidth + leftMargin);
            }

            if (childWidth < element.Width)
            {
                element.Left = leftMargin + MARGIN;
                return element.Width + MARGIN;
            }
            else
            {
                element.Left = leftMargin + (childWidth - element.Width) / 2;
                return childWidth;
            }
        }

        private Double SetTop(Element element, Double top = 0)
        {
            Int32 childCount = element.Children.Count;
            element.Top = top;
            Double height = element.Top + element.Height;

            for (int i = 0; i < childCount; i++)
            {
                Element childElement = element.Children[i];
                height = Math.Max(height, SetTop(childElement, top + element.Height + 2 * 10));
            }

            return height;
        }

        private void Draw(Element element)
        {
            Canvas.SetLeft(element.Content, element.Left);
            Canvas.SetTop(element.Content, element.Top);
            this.Children.Add(element.Content);
            for (int i = 0; i < element.Children.Count; i++)
            {
                Draw(element.Children[i]);
            }
        }
    }
}
