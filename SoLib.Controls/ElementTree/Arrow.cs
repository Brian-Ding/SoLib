using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SoLib.Controls.ElementTree
{
    public sealed class Arrow : Path
    {
        private const Double THETA = 15 * Math.PI / 180;
        private const Double RADIUS = 20;

        private Double _endX;
        private Double _endY;

        public Double EndX
        {
            get { return (Double)GetValue(EndXProperty); }
            set { SetValue(EndXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndX.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty EndXProperty =
            DependencyProperty.Register("EndX", typeof(Double), typeof(Arrow), new PropertyMetadata(0, OnEndXChanged));

        private static void OnEndXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }



        public Double EndY
        {
            get { return (Double)GetValue(EndYProperty); }
            set { SetValue(EndYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndY.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty EndYProperty =
            DependencyProperty.Register("EndY", typeof(Double), typeof(Arrow), new PropertyMetadata(0, OnEndYChanged));

        private static void OnEndYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Arrow()
        {
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePath();
        }

        private void UpdatePath()
        {
            PathGeometry pathGeometry = new PathGeometry();

            PathFigure lineFigure = new PathFigure() { IsClosed = true };
            PolyLineSegment lineSegment = new PolyLineSegment();
            lineSegment.Points.Add(new Point(10, 10));
            lineSegment.Points.Add(new Point(EndX, EndY));
            lineFigure.Segments.Add(lineSegment);
            pathGeometry.Figures.Add(lineFigure);

            PathFigure triangleFigure = new PathFigure() { IsClosed = true };
            PolyLineSegment triangleSegment = new PolyLineSegment();

            Double alpha = Math.Atan(EndY / EndX);
            triangleSegment.Points.Add(new Point(EndX, EndY));

            Double beta = Math.PI + alpha - THETA;
            Double x = RADIUS * Math.Cos(beta) + EndX;
            Double y = RADIUS * Math.Sin(beta) + EndY;
            triangleSegment.Points.Add(new Point(x, y));

            beta = Math.PI + alpha + THETA;
            x = RADIUS * Math.Cos(beta) + EndX;
            y = RADIUS * Math.Sin(beta) + EndY;
            triangleSegment.Points.Add(new Point(x, y));
            triangleSegment.Points.Add(new Point(EndX, EndY));

            triangleFigure.Segments.Add(triangleSegment);
            pathGeometry.Figures.Add(triangleFigure);

            //InvalidateArrange();
            Data = pathGeometry;
        }
    }
}
