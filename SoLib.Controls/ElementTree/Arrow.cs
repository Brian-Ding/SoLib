using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SoLib.Controls.ElementTree
{
    public sealed class Arrow : Path
    {
        private const Double THETA = 15 * Math.PI / 180;
        private Double _radius = 40;

        private Int32 _deferLevel;
        private Point _endPoint;



        public Point EndPoint
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }
        private static readonly DependencyProperty PointProperty =
            DependencyProperty.Register(nameof(EndPoint), typeof(Point), typeof(Arrow), new PropertyMetadata(new Point(0, 0), OnPointChanged));

        private static void OnPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Arrow arrow = d as Arrow;
            //arrow._radius = 0.05 * Math.Sqrt(arrow.EndPoint.X * arrow.EndPoint.X + arrow.EndPoint.Y * arrow.EndPoint.Y);
            arrow._endPoint = new Point(Math.Abs(arrow.EndPoint.X), Math.Abs(arrow.EndPoint.Y));
            arrow.UpdateSize();
            arrow.UpdatePath();
            arrow.Transform();
        }

        private void UpdateSize()
        {
            this.Width = Math.Abs(EndPoint.X) + 30;
            this.Height = Math.Abs(EndPoint.Y) + 30;
        }

        private void UpdatePath()
        {
            PathGeometry pathGeometry = new PathGeometry();

            //// Draw line
            //PathFigure lineFigure = new PathFigure() { IsClosed = false };
            //PolyLineSegment lineSegment = new PolyLineSegment();

            //lineSegment.Points.Add(_endPoint);
            //lineFigure.Segments.Add(lineSegment);
            //pathGeometry.Figures.Add(lineFigure);

            // Draw triangle
            Double away = 10;
            Point arrowEnd = new Point(_endPoint.X + away, _endPoint.Y + away * _endPoint.Y / _endPoint.X);
            PathFigure triangleFigure = new PathFigure() { IsClosed = true };
            PolyLineSegment triangleSegment = new PolyLineSegment();

            Double alpha = Math.Atan(arrowEnd.Y / arrowEnd.X);

            Double beta1 = alpha - THETA;
            Double x1 = arrowEnd.X - _radius * Math.Cos(beta1);
            Double y1 = arrowEnd.Y - _radius * Math.Sin(beta1);

            Double beta2 = alpha + THETA;
            Double x2 = arrowEnd.X - _radius * Math.Cos(beta2);
            Double y2 = arrowEnd.Y - _radius * Math.Sin(beta2);

            triangleSegment.Points.Add(new Point((x1 + x2) / 2, (y1 + y2) / 2));
            triangleSegment.Points.Add(new Point(x1, y1));
            triangleSegment.Points.Add(arrowEnd);
            triangleSegment.Points.Add(new Point(x2, y2));
            triangleSegment.Points.Add(new Point((x1 + x2) / 2, (y1 + y2) / 2));


            triangleFigure.Segments.Add(triangleSegment);
            pathGeometry.Figures.Add(triangleFigure);

            //InvalidateArrange();
            Data = pathGeometry;
        }

        private void Transform()
        {
            TransformGroup transformGroup = new TransformGroup();
            if (EndPoint.X >= 0 && EndPoint.Y >= 0)
            {
                transformGroup.Children.Add(new ScaleTransform() { ScaleY = -1 });
            }
            else if (EndPoint.X < 0 && EndPoint.Y >= 0)
            {
                transformGroup.Children.Add(new RotateTransform() { Angle = 180 });
            }
            else if (EndPoint.X < 0 && EndPoint.Y < 0)
            {
                transformGroup.Children.Add(new ScaleTransform() { ScaleX = -1 });
            }
            this.RenderTransform = transformGroup;
        }

        //public IDisposable DeferRefresh()
        //{
        //    _deferLevel++;
        //    return new DeferHelper(this);
        //}

        //internal void EndDefer()
        //{
        //    if (_deferLevel > 0)
        //    {
        //        _deferLevel--;
        //    }

        //    if (_deferLevel == 0)
        //    {
        //        UpdatePath();
        //    }
        //}
    }

    //internal class DeferHelper : IDisposable
    //{
    //    private Arrow _path;

    //    public DeferHelper(Arrow path)
    //    {
    //        _path = path;
    //    }

    //    public void Dispose()
    //    {
    //        GC.SuppressFinalize(this);
    //        if (_path != null)
    //        {
    //            _path.EndDefer();
    //            _path = null;
    //        }
    //    }
    //}
}