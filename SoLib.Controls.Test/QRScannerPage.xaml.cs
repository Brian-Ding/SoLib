using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SoLib.Controls.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QRScannerPage : Page
    {
        public QRScannerPage()
        {
            this.InitializeComponent();
        }

        private void QRScanner_QRCodeFound(object sender, QRCodeFoundEventArgs e)
        {
            result.Text = e.Result.Text;
        }
    }
}
