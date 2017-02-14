using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SoLib.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreeMapPage : Page
    {
        public TreeMapPage()
        {
            this.InitializeComponent();
            this.Loaded += TreeMapPage_Loaded;
        }

        private void TreeMapPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<IData> temp = new List<IData>();
            temp.Add(new TreeMapModel()
            {
                ParentID = Guid.Empty,
                ID = Guid.NewGuid(),
                DataContent = new TextBlock() { Text = "AA" }
            });
            temp.Add(new TreeMapModel()
            {
                ParentID = temp[0].ID,
                ID = Guid.NewGuid(),
                DataContent = new TextBlock() { Text = "BB" }
            });
            temp.Add(new TreeMapModel()
            {
                ParentID = temp[0].ID,
                ID = Guid.NewGuid(),
                DataContent = new TextBlock() { Text = "CC" }
            });

            treeMap.DataSource = temp;
        }
    }

    public class TreeMapModel : IData
    {
        public Guid ParentID { get; set; }

        public Guid ID { get; set; }

        public string Relation { get; set; }

        public double Top { get; set; }

        public double Left { get; set; }

        public int Level { get; set; }

        public object DataContent { get; set; }
    }
}
