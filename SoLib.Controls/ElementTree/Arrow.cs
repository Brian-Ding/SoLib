using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Int32 _deferLevel;
        private Point _arrowStartPoint;
        private Point _arrowEndPoint;
        private Quadrant _quadrant;

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
            if (arrow.EndPoint.X >= 0 && arrow.EndPoint.Y >= 0)
            {
                arrow._quadrant = Quadrant.First;
            }
            else if (arrow.EndPoint.X < 0 && arrow.EndPoint.Y >= 0)
            {
                arrow._quadrant = Quadrant.Second;
            }
            else if (arrow.EndPoint.X < 0 && arrow.EndPoint.Y < 0)
            {
                arrow._quadrant = Quadrant.Third;
            }
            else
            {
                arrow._quadrant = Quadrant.Fourth;
            }
            arrow._arrowEndPoint = arrow.EndPoint;
            arrow.UpdateSize();
            arrow.UpdatePath();
        }

        public Arrow()
        {
            _arrowStartPoint = new Point(0, 0);
        }

        private void UpdateSize()
        {
            //this.Width = Math.Abs(Point.X);
            //this.Height = Math.Abs(Point.Y);
        }

        private void UpdatePath()
        {
            PathGeometry pathGeometry = new PathGeometry();

            // Draw line
            PathFigure lineFigure = new PathFigure() { IsClosed = false };
            PolyLineSegment lineSegment = new PolyLineSegment();
            
            lineSegment.Points.Add(ToScreenPoint(_arrowStartPoint));
            lineSegment.Points.Add(ToScreenPoint(_arrowEndPoint));
            lineFigure.Segments.Add(lineSegment);
            pathGeometry.Figures.Add(lineFigure);

            //PathFigure triangleFigure = new PathFigure() { IsClosed = true };
            //PolyLineSegment triangleSegment = new PolyLineSegment();

            //Double alpha = Math.Atan(Y / X);
            //triangleSegment.Points.Add(new Point(_arrowEndX, _arrowEndY));

            //Double beta = Math.PI + alpha - THETA;
            //Double x = RADIUS * Math.Cos(beta) + _arrowEndX;
            //Double y = RADIUS * Math.Sin(beta) + _arrowEndY;
            //triangleSegment.Points.Add(new Point(x, y));

            //beta = Math.PI + alpha + THETA;
            //x = RADIUS * Math.Cos(beta) + _arrowEndX;
            //y = RADIUS * Math.Sin(beta) + _arrowEndY;
            //triangleSegment.Points.Add(new Point(x, y));
            //triangleSegment.Points.Add(new Point(_arrowEndX, _arrowEndY));

            //triangleFigure.Segments.Add(triangleSegment);
            //pathGeometry.Figures.Add(triangleFigure);

            //InvalidateArrange();
            Data = pathGeometry;
        }

        private Point ToScreenPoint(Point point)
        {
            Point newPoint;
            switch (_quadrant)
            {
                case Quadrant.First:
                    newPoint = new Point(point.X, EndPoint.Y - point.Y);
                    break;
                case Quadrant.Second:
                    newPoint = new Point(point.X - EndPoint.X, EndPoint.Y - point.Y);
                    break;
                case Quadrant.Third:
                    newPoint = new Point(point.X - EndPoint.X, -point.Y);
                    break;
                case Quadrant.Fourth:
                    newPoint = new Point(point.X, -point.Y);
                    break;
            }

            return newPoint;
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

    internal enum Quadrant
    {
        First,
        Second,
        Third,
        Fourth
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