using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    public sealed partial class ValueRec : UserControl
    {
        public ValueRec()
        {
            this.InitializeComponent();
        }



        public FrameworkElement Header
        {
            get { return (FrameworkElement)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(FrameworkElement), typeof(ValueRec), new PropertyMetadata(0));



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ValueRec), new PropertyMetadata(100.0));



        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(ValueRec), new PropertyMetadata(0));




        public double RecHeight
        {
            get { return (double)GetValue(RecHeightProperty); }
            set { SetValue(RecHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecHeightProperty =
            DependencyProperty.Register("RecHeight", typeof(double), typeof(ValueRec), new PropertyMetadata(30.0));




        public double WidthRatio
        {
            get { return (double)GetValue(WidthRatioProperty); }
            set { SetValue(WidthRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthRatioProperty =
            DependencyProperty.Register("WidthRatio", typeof(double), typeof(ValueRec), new PropertyMetadata(1.0));

        private double GetWidth()
        {
            return WidthRatio * Value;
        }
    }
}
