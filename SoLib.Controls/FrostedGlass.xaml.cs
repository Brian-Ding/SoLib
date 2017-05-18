using Microsoft.Graphics.Canvas.Effects;
using System.Numerics;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SoLib.Controls
{
    public sealed partial class FrostedGlass : UserControl
    {
        public FrostedGlass()
        {
            this.InitializeComponent();
            this.SizeChanged += FrostedGlass_SizeChanged;
        }


        /// <summary>
        /// Color of the frosted glass. Default value is White.
        /// </summary>
        public Color GlassColor
        {
            get { return (Color)GetValue(GlassColorProperty); }
            set { SetValue(GlassColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GlassColor.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty GlassColorProperty =
            DependencyProperty.Register("GlassColor", typeof(Color), typeof(FrostedGlass), new PropertyMetadata(Colors.White));


        /// <summary>
        /// How blur the glass will be. Default value is 2.
        /// </summary>
        public double BlurAmount
        {
            get { return (double)GetValue(BlurAmountProperty); }
            set { SetValue(BlurAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlurAmount.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BlurAmountProperty =
            DependencyProperty.Register("BlurAmount", typeof(double), typeof(FrostedGlass), new PropertyMetadata(10.0));


        /// <summary>
        /// Transparency, range from 0 to 255. Default value is 25.
        /// </summary>
        public int Alpha
        {
            get { return (int)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Alpha.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty AlphaProperty =
            DependencyProperty.Register("Alpha", typeof(int), typeof(FrostedGlass), new PropertyMetadata(25));


        /// <summary>
        /// Multiply. Default value is 1.
        /// </summary>
        public double Multiply
        {
            get { return (double)GetValue(MultiplyProperty); }
            set { SetValue(MultiplyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiply.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty MultiplyProperty =
            DependencyProperty.Register("Multiply", typeof(double), typeof(FrostedGlass), new PropertyMetadata(1.0));


        /// <summary>
        /// Background ratio. Default value is 0.8f.
        /// </summary>
        public double BackAmount
        {
            get { return (double)GetValue(BackAmountProperty); }
            set { SetValue(BackAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackAmount.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BackAmountProperty =
            DependencyProperty.Register("BackAmount", typeof(double), typeof(FrostedGlass), new PropertyMetadata(0.8));


        /// <summary>
        /// Foreground ratio. Default value is 0.2f.
        /// </summary>
        public double FrontAmout
        {
            get { return (double)GetValue(FrontAmoutProperty); }
            set { SetValue(FrontAmoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FrontAmout.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty FrontAmoutProperty =
            DependencyProperty.Register("FrontAmout", typeof(double), typeof(FrostedGlass), new PropertyMetadata(0.2));



        public bool Hosted
        {
            get { return (bool)GetValue(HostedProperty); }
            set { SetValue(HostedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hosted.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty HostedProperty =
            DependencyProperty.Register("Hosted", typeof(bool), typeof(FrostedGlass), new PropertyMetadata(true));



        private void FrostedGlass_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitializeFrostedGlass((float)RenderSize.Width, (float)RenderSize.Height, GlassColor, (float)BlurAmount, (byte)Alpha, (float)Multiply, (float)BackAmount, (float)FrontAmout);
        }

        private void InitializeFrostedGlass(float width, float height, Color color, float blurAmout, byte alpha, float multiply, float backAmout, float frontAmout)
        {
            Visual hostedVisual = ElementCompositionPreview.GetElementVisual(glassGrid);
            Compositor compositor = hostedVisual.Compositor;
            GaussianBlurEffect blurEffect = new GaussianBlurEffect()
            {
                BlurAmount = blurAmout,
                BorderMode = EffectBorderMode.Hard,
                Source = new ArithmeticCompositeEffect()
                {
                    MultiplyAmount = multiply,
                    Source1Amount = backAmout,
                    Source2Amount = frontAmout,
                    Source1 = new CompositionEffectSourceParameter("BackBrush"),
                    Source2 = new ColorSourceEffect()
                    {
                        Color = Color.FromArgb(alpha, color.R, color.G, color.B)
                    }
                }
            };

            CompositionEffectFactory factory = compositor.CreateEffectFactory(blurEffect);
            CompositionBackdropBrush backdropBrush;
            if (Hosted)
            {
                backdropBrush = compositor.CreateHostBackdropBrush();
            }
            else
            {
                backdropBrush = compositor.CreateBackdropBrush();
            }
            CompositionEffectBrush brush = factory.CreateBrush();
            brush.SetSourceParameter("BackBrush", backdropBrush);

            SpriteVisual glassVisual = compositor.CreateSpriteVisual();
            glassVisual.Size = new Vector2()
            {
                X = width,
                Y = height
            };
            glassVisual.Brush = brush;
            ElementCompositionPreview.SetElementChildVisual(glassGrid, glassVisual);
        }
    }
}
