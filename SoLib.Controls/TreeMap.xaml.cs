using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    /// <summary>
    /// A control that visualize data's connection
    /// </summary>
    public sealed partial class TreeMap : UserControl
    {
        /// <summary>
        /// TreeMap constructor
        /// </summary>
        public TreeMap()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Width of each data
        /// </summary>
        public double WidthUnit
        {
            get { return (double)GetValue(WidthUnitProperty); }
            set { SetValue(WidthUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthUnit.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty WidthUnitProperty =
            DependencyProperty.Register("WidthUnit", typeof(double), typeof(TreeMap), new PropertyMetadata(50.0));

        /// <summary>
        /// Height of each data
        /// </summary>
        public double HeightUnit
        {
            get { return (double)GetValue(HeightUnitProperty); }
            set { SetValue(HeightUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightUnit.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty HeightUnitProperty =
            DependencyProperty.Register("HeightUnit", typeof(double), typeof(TreeMap), new PropertyMetadata(50.0));

        /// <summary>
        /// Horizontal gap between each data
        /// </summary>
        public double WidthGap
        {
            get { return (double)GetValue(WidthGapProperty); }
            set { SetValue(WidthGapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthGap.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty WidthGapProperty =
            DependencyProperty.Register("WidthGap", typeof(double), typeof(TreeMap), new PropertyMetadata(20.0));

        /// <summary>
        /// Vertical gap between each data
        /// </summary>
        public double HeightGap
        {
            get { return (double)GetValue(HeightGapProperty); }
            set { SetValue(HeightGapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightGap.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty HeightGapProperty =
            DependencyProperty.Register("HeightGap", typeof(double), typeof(TreeMap), new PropertyMetadata(30.0));

        /// <summary>
        /// Visibility of ConnectionLine
        /// </summary>
        public bool ConnectionLine
        {
            get { return (bool)GetValue(ConnectionLineProperty); }
            set { SetValue(ConnectionLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConnectionLine.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ConnectionLineProperty =
            DependencyProperty.Register("ConnectionLine", typeof(bool), typeof(TreeMap), new PropertyMetadata(false));

        /// <summary>
        /// DataSource for TreeMap control
        /// </summary>
        public List<IData> DataSource
        {
            get { return (List<IData>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSource.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(List<IData>), typeof(TreeMap), new PropertyMetadata(0, OnDataSourceChanged));

        private static void OnDataSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            (d as TreeMap).Draw();
        }

        private double FindWidth(IData data)
        {
            double width = 0;

            foreach (var child in DataSource)
            {
                if (child.ParentID == data.ID)
                {
                    width += FindWidth(child);
                    width += WidthGap;
                }
            }

            if (width == 0)
            {
                width = WidthUnit;
            }
            else
            {
                width -= WidthGap;
            }

            return width;
        }

        private double FindHeight()
        {
            int level = 0;

            foreach (var data in DataSource)
            {
                if (data.Level > level)
                {
                    level = data.Level;
                }
            }

            return (level + 1) * HeightUnit + level * HeightGap;
        }

        private void LevelData(IData data)
        {
            foreach (var child in DataSource)
            {
                if (child.ParentID == data.ID)
                {
                    child.Level = data.Level + 1;
                    LevelData(child);
                }
            }
        }

        private IData FindTopData()
        {
            foreach (var data in DataSource)
            {
                if (data.ParentID == Guid.Empty)
                {
                    data.Level = 0;
                    LevelData(data);
                    return data;
                }
            }

            return null;
        }

        private void PlaceData()
        {
            for (int i = 0; i < FindMaxLevel() + 1; i++)
            {
                List<IData> dataList = FindData(i);

                for (int j = 0; j < dataList.Count; j++)
                {
                    dataList[j].Top = i * (HeightUnit + HeightGap);
                    dataList[j].Left = (FindWidth(FindTopData()) - dataList.Count * WidthUnit) / (dataList.Count + 1) * (j + 1) + WidthUnit * j;
                }
            }
        }

        private void Draw()
        {
            Canvas canvas = new Canvas()
            {
                Width = FindWidth(FindTopData()),
                Height = FindHeight()
            };

            PlaceData();

            foreach (var data in this.DataSource)
            {
                DrawData(data, canvas);
                DrawLine(data, canvas);
            }

            this.Content = canvas;
        }

        private void DrawData(IData data, Canvas canvas)
        {
            UIElement uielement = (data.DataContent as UIElement).DeepClone();

            Canvas.SetTop(uielement, data.Top);
            Canvas.SetLeft(uielement, data.Left);
            canvas.Children.Add(uielement);
        }

        private void DrawLine(IData data, Canvas canvas)
        {
            if (ConnectionLine)
            {
                foreach (var child in DataSource)
                {
                    if (child.ParentID == data.ID)
                    {
                        Line line = new Line()
                        {
                            X1 = data.Left + (data.DataContent as FrameworkElement).Width / 2,
                            X2 = child.Left + (data.DataContent as FrameworkElement).Width / 2,
                            Y1 = data.Top + (data.DataContent as FrameworkElement).Height + HeightGap / 20,
                            Y2 = child.Top - HeightGap / 20,
                            Stroke = new SolidColorBrush(Colors.Black),
                            StrokeThickness = 1
                        };

                        canvas.Children.Add(line);
                    }
                }
            }
        }

        private int FindMaxLevel()
        {
            int maxLevel = 0;

            foreach (var data in DataSource)
            {
                if (data.Level > maxLevel)
                {
                    maxLevel = data.Level;
                }
            }

            return maxLevel;
        }

        private List<IData> FindData(int level)
        {
            return DataSource.FindAll(d => d.Level == level);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal static class UIElementExtensions
    {
        internal static T DeepClone<T>(this T source) where T : UIElement
        {

            T result;

            // Get the type
            Type type = source.GetType();

            // Create an instance
            result = Activator.CreateInstance(type) as T;

            CopyProperties<T>(source, result, type);

            DeepCopyChildren<T>(source, result);

            return result;
        }

        private static void DeepCopyChildren<T>(T source, T result) where T : UIElement
        {
            // Deep copy children.
            Panel sourcePanel = source as Panel;
            if (sourcePanel != null)
            {
                Panel resultPanel = result as Panel;
                if (resultPanel != null)
                {
                    foreach (UIElement child in sourcePanel.Children)
                    {
                        // RECURSION!
                        UIElement childClone = DeepClone(child);
                        resultPanel.Children.Add(childClone);
                    }
                }
            }
        }

        private static void CopyProperties<T>(T source, T result, Type type) where T : UIElement
        {
            // Copy all properties.

            IEnumerable<PropertyInfo> properties = type.GetRuntimeProperties();

            foreach (var property in properties)
            {
                if (property.Name != "Name") // do not copy names or we cannot add the clone to the same parent as the original.
                {
                    if ((property.CanWrite) && (property.CanRead))
                    {
                        object sourceProperty = property.GetValue(source);

                        UIElement element = sourceProperty as UIElement;
                        if (element != null)
                        {
                            UIElement propertyClone = element.DeepClone();
                            property.SetValue(result, propertyClone);
                        }
                        else
                        {
                            try
                            {
                                property.SetValue(result, sourceProperty);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}
