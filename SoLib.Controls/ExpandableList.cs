using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoLib.Controls
{
    public sealed class ExpandableList : ListView
    {
        private int _selectedIndex = -1;

        public DataTemplate ShrinkTemplate { get; set; }

        public DataTemplate ExpandTemplate { get; set; }

        public ExpandableList()
        {
            this.ItemClick += ExpandableList_ItemClick;
        }

        private void ExpandableList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_selectedIndex == this.SelectedIndex)
            {
            }
        }

        private void SwitchTempalte()
        {

        }
    }
}
