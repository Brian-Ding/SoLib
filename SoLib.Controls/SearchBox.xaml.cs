using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    public sealed partial class SearchBox : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public SearchBox()
        {
            this.InitializeComponent();
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string SearchTip
        {
            get { return (string)GetValue(SearchTipProperty); }
            set { SetValue(SearchTipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchTip.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty SearchTipProperty =
            DependencyProperty.Register("SearchTip", typeof(string), typeof(SearchBox), new PropertyMetadata("Search"));

        /// <summary>
        /// 
        /// </summary>
        public Thickness Thickness
        {
            get { return (Thickness)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(Thickness), typeof(SearchBox), new PropertyMetadata(new Thickness(0, 0, 0, 1)));
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<SearchEventArgs> Search;

        private void Search_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.searchTxt.Text) && Search != null)
            {
                SearchEventArgs args = new SearchEventArgs()
                {
                    Query = this.searchTxt.Text
                };

                EventHandler<SearchEventArgs> temp = Search;
                temp(this, args);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SearchEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string Query { get; set; }
    }
}
