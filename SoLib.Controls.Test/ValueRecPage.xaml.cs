using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SoLib.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ValueRecPage : Page
    {
        Random random;
        public ValueRecPage()
        {
            this.InitializeComponent();
            random = new Random();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //rec.Value = random.Next(250);
        }
    }
}
