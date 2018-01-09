using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace SoLib.Controls.ElementTree
{
    public sealed class ElementTree : Canvas
    {
        private const Double VERTICALMARGIN = 10;

        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(nameof(Stroke), typeof(Color), typeof(ElementTree), new PropertyMetadata(Colors.Black));



        public Color Fill
        {
            get { return (Color)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(nameof(Fill), typeof(Color), typeof(ElementTree), new PropertyMetadata(Colors.Transparent));



        public Double ArrowThickness
        {
            get { return (Double)GetValue(ArrowThicknessProperty); }
            set { SetValue(ArrowThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArrowThickness.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ArrowThicknessProperty =
            DependencyProperty.Register(nameof(ArrowThickness), typeof(Double), typeof(ElementTree), new PropertyMetadata(2.0));



        public Double Distance
        {
            get { return (Double)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Distance.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register(nameof(Distance), typeof(Double), typeof(ElementTree), new PropertyMetadata(100.0));



        public Double Margin
        {
            get { return (Double)GetValue(MarginProperty); }
            set { SetValue(MarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Margin.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty MarginProperty =
            DependencyProperty.Register(nameof(Margin), typeof(Double), typeof(ElementTree), new PropertyMetadata(10.0));



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
            elementTree.DrawArrow(elementTree.RootElement);
        }

        private Double SetLeft(Element element, Double initialLeft = 0)
        {
            Double childWidth = 0;
            Int32 childCount = element.Children.Count;

            for (int i = 0; i < childCount; i++)
            {
                childWidth += SetLeft(element.Children[i], childWidth + initialLeft);
            }

            if (childWidth < element.Width)
            {
                element.Left = initialLeft + Margin;
                return element.Width + Margin;
            }
            else
            {
                element.Left = initialLeft + (childWidth - element.Width) / 2;
                return childWidth;
            }
        }

        private Double SetTop(Element element, Double top = 0)
        {
            element.Top = top;
            Int32 childCount = element.Children.Count;
            Double height = element.Top + element.Height;

            for (int i = 0; i < childCount; i++)
            {
                Element childElement = element.Children[i];
                height = Math.Max(height, SetTop(childElement, top + element.Height + 2 * VERTICALMARGIN + Distance));
            }

            return height;
        }

        private void Draw(Element element)
        {
            SetLeft(element.Content, element.Left);
            SetTop(element.Content, element.Top);
            Children.Add(element.Content);
            for (int i = 0; i < element.Children.Count; i++)
            {
                Draw(element.Children[i]);
            }
        }

        private void DrawArrow(Element element)
        {
            for (int i = 0; i < element.Children.Count; i++)
            {
                Arrow arrow = new Arrow()
                {
                    Stroke = new SolidColorBrush(Stroke),
                    Fill = new SolidColorBrush(Fill),
                    StrokeThickness = ArrowThickness,
                    EndPoint = new Point()
                    {
                        X = element.Children[i].Left + element.Children[i].Width / 2 - element.Left - element.Width / 2,
                        //Y = element.Top + element.Height + 2 * Margin - element.Children[i].Top
                        Y = -Distance
                    }
                };
                SetLeft(arrow, element.Left + element.Width / 2);
                SetTop(arrow, element.Top + element.Height + VERTICALMARGIN);
                Children.Add(arrow);

                DrawArrow(element.Children[i]);
            }
        }
    }
}
