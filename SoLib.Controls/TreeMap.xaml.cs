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

        }

        private double FindWidth(IData data)
        {
            double width = 0;

            foreach (var item in DataSource)
            {
                if (item.ParentID == data.ID)
                {
                    width += 70;
                }
            }

            if (width > 0)
            {
                width -= 20;
            }
            else
            {
                width = 50;
            }

            return width;
        }

        private void DrawLabel()
        {
            foreach (var data in DataSource)
            {

            }
        }
    }
}
