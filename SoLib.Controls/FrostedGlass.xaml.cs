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
        public float BlurAmount
        {
            get { return (float)GetValue(BlurAmountProperty); }
            set { SetValue(BlurAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlurAmount.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BlurAmountProperty =
            DependencyProperty.Register("BlurAmount", typeof(float), typeof(FrostedGlass), new PropertyMetadata((float)2));


        /// <summary>
        /// Transparency, range from 0 to 255. Default value is 25.
        /// </summary>
        public byte Alpha
        {
            get { return (byte)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Alpha.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty AlphaProperty =
            DependencyProperty.Register("Alpha", typeof(byte), typeof(FrostedGlass), new PropertyMetadata((byte)25));


        /// <summary>
        /// Multiply. Default value is 1.
        /// </summary>
        public float Multiply
        {
            get { return (float)GetValue(MultiplyProperty); }
            set { SetValue(MultiplyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Multiply.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty MultiplyProperty =
            DependencyProperty.Register("Multiply", typeof(float), typeof(FrostedGlass), new PropertyMetadata((float)1));


        /// <summary>
        /// Background ratio. Default value is 0.8f.
        /// </summary>
        public float BackAmount
        {
            get { return (float)GetValue(BackAmountProperty); }
            set { SetValue(BackAmountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackAmount.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty BackAmountProperty =
            DependencyProperty.Register("BackAmount", typeof(float), typeof(FrostedGlass), new PropertyMetadata(0.8f));


        /// <summary>
        /// Foreground ratio. Default value is 0.2f.
        /// </summary>
        public float FrontAmout
        {
            get { return (float)GetValue(FrontAmoutProperty); }
            set { SetValue(FrontAmoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FrontAmout.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty FrontAmoutProperty =
            DependencyProperty.Register("FrontAmout", typeof(float), typeof(FrostedGlass), new PropertyMetadata(0.2f));



        private void FrostedGlass_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitializeFrostedGlass((float)RenderSize.Width, (float)RenderSize.Height, GlassColor, BlurAmount, Alpha, Multiply, BackAmount, FrontAmout);
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
            CompositionBackdropBrush backdropBrush = compositor.CreateHostBackdropBrush();
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
