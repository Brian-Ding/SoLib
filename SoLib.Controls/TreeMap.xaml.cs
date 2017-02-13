using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    public sealed partial class TreeMap : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public TreeMap()
        {
            this.InitializeComponent();
        }


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
                if (data.ChildrenIDs.Contains(child.ID))
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

        private void PlaceData(IData data)
        {
            if (data.Level == 0)
            {
                data.Top = 0;
            }
            else
            {
                data.Top = data.Level * 50 + (data.Level - 1) * 30;
            }

            double left = 0;

        }
    }
}
