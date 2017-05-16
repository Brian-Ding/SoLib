using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    public sealed partial class ValueRec : UserControl
    {
        private readonly Compositor _compositor;

        public ValueRec()
        {
            this.InitializeComponent();
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
        }



        public FrameworkElement Header
        {
            get { return (FrameworkElement)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(FrameworkElement), typeof(ValueRec), new PropertyMetadata(0));



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                if (Value != value)
                {
                    StartAnimation(value);
                }
                SetValue(ValueProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(ValueRec), new PropertyMetadata(0.0));



        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(ValueRec), new PropertyMetadata(0));




        public double RecHeight
        {
            get { return (double)GetValue(RecHeightProperty); }
            set { SetValue(RecHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecHeight.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty RecHeightProperty =
            DependencyProperty.Register("RecHeight", typeof(double), typeof(ValueRec), new PropertyMetadata(30.0));



        private void StartAnimation(double value)
        {
            float ratio = (float)this.Value / (float)value;
            Visual recVisual = ElementCompositionPreview.GetElementVisual(rec);
            Visual txtVisual = ElementCompositionPreview.GetElementVisual(txt);
            recVisual.CenterPoint = new Vector3(0, 0.5f, 0);
            recVisual.Scale = new Vector3(ratio, 1, 0);
            txtVisual.Opacity = 0;

            CompositionEasingFunction cubicBezierEasingFunction = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.5f, 0f), new Vector2(0.3f, 0.9f));
            Vector3KeyFrameAnimation scaleAnimation = _compositor.CreateVector3KeyFrameAnimation();
            scaleAnimation.InsertKeyFrame(1f, new Vector3(1, 1, 0), cubicBezierEasingFunction);
            scaleAnimation.Duration = TimeSpan.FromMilliseconds(1250);
            scaleAnimation.DelayTime = TimeSpan.FromMilliseconds(100);

            ScalarKeyFrameAnimation offsetAnimation = _compositor.CreateScalarKeyFrameAnimation();
            //offsetAnimation.InsertKeyFrame(0f, 10);
            offsetAnimation.InsertKeyFrame(1f, (float)(10 + value), cubicBezierEasingFunction);
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(1250);
            offsetAnimation.DelayTime = TimeSpan.FromMilliseconds(100);

            ScalarKeyFrameAnimation opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            //opacityAnimation.InsertKeyFrame(0.2f, 0, cubicBezierEasingFunction);
            opacityAnimation.InsertKeyFrame(1f, 1, cubicBezierEasingFunction);
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(1250);
            opacityAnimation.DelayTime = TimeSpan.FromMilliseconds(100);

            recVisual.StartAnimation(nameof(recVisual.Scale), scaleAnimation);
            txtVisual.StartAnimation("Offset.X", offsetAnimation);
            txtVisual.StartAnimation(nameof(txtVisual.Opacity), opacityAnimation);
        }

    }
}
