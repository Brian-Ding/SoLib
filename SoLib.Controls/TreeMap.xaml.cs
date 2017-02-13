using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TreeMap : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeMap()
        {
            this.InitializeComponent();
        }

        private readonly double unitWidth = 50;
        private readonly double unitHeight = 50;
        private readonly double gapWidth = 20;
        private readonly double gapHeight = 30;

        /// <summary>
        /// 
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
            TreeMap treeMap = d as TreeMap;
            Canvas canvas = new Canvas()
            {
                Width = treeMap.FindWidth(treeMap.FindTopData()),
                Height = treeMap.FindHeight()
            };
            treeMap.PlaceData();
            foreach (var data in treeMap.DataSource)
            {
                treeMap.DrawData(data, canvas);
            }

            treeMap.Content = canvas;
        }

        private double FindWidth(IData data)
        {
            double width = 0;

            foreach (var child in DataSource)
            {
                if (child.ParentID == data.ID)
                {
                    width += 20;
                    width += FindWidth(child);
                }
            }

            if (width == 0)
            {
                width = 50;
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

            return (level + 1) * 50 + level * 30;
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
                    if (i == 0)
                    {
                        dataList[j].Top = 0;
                    }
                    else
                    {
                        dataList[j].Top = i * 50 + (i - 1) * 30;
                    }
                    dataList[j].Left = (FindWidth(FindTopData()) - dataList.Count * unitWidth) / (dataList.Count + 1) * (j + 1) + unitWidth * j;
                }
            }
        }

        private void DrawData(IData data, Canvas canvas)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = data.ID.ToString(),
                MaxWidth = 30
            };

            Canvas.SetTop(textBlock, data.Top);
            Canvas.SetLeft(textBlock, data.Left);
            canvas.Children.Add(textBlock);
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
}
