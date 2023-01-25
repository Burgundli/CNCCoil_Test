using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShapesLib
{
    public enum CommandType
    {
        Line, 
        CWArc,
        CCWArc, 
    }

    public enum ArcType 
    {
        None,
        Large, 
        Small,
    }
 
    public struct CommandParams 
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double R { get; set; }
        public ArcType ArcType { get; set; }
        public int Speed { get; set; }
        public double Volts { get; set; }
        public double PM { get; set; }
    }
    
    public class CNCNode 
    {
        private double x;
        private double y;
        private double r;

        public Path Path { get; set; }
        
        public PathSegmentCollection Segments { get; set; }

        public double X
        {
            get 
            {
                var prop = Segments.Last().GetType().GetProperty("Point");
                return ((Point)prop.GetValue(Segments.Last())).X; 
            }
            set 
            {
                var prop = Segments.Last().GetType().GetProperty("Point");
                Point point = (Point)prop.GetValue(Segments.Last());
                point.X = value;
                prop.SetValue(Segments.Last(), point); 
            }
        }
        public double Y
        {
            get 
            {
                var prop = Segments.Last().GetType().GetProperty("Point");
                return ((Point)prop.GetValue(Segments.Last())).Y;
            }
            set 
            {
                var prop = Segments.Last().GetType().GetProperty("Point");
                Point point = (Point)prop.GetValue(Segments.Last());
                point.Y = value;
                prop.SetValue(Segments.Last(), point);
            }
        }
        public double R
        {       
            get { return r; }
            set { r = value; }
        }

        public CNCNode(CommandType command, CommandParams cmdParams) 
        {
            PathGeometry geometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            Segments = new PathSegmentCollection();

            pathFigure.Segments = Segments;
            geometry.Figures.Add(pathFigure);

            Path = new Path
            {
                Data = geometry
            };
            switch (command)
            {
                case CommandType.Line:
                    Segments.Add(new LineSegment(new Point(cmdParams.X, cmdParams.Y), true)); 
                    break;
                case CommandType.CWArc:
                    Segments.Add(new ArcSegment(new Point(cmdParams.X, cmdParams.Y),new Size(cmdParams.R, cmdParams.R), 0, cmdParams.ArcType == ArcType.Large, SweepDirection.Clockwise, true));
                    break;
                case CommandType.CCWArc:
                    Segments.Add(new ArcSegment(new Point(cmdParams.X, cmdParams.Y), new Size(cmdParams.R, cmdParams.R), 0, cmdParams.ArcType == ArcType.Large, SweepDirection.Counterclockwise, true));
                    break;
                default:
                    break;
            }


            
        }


    }

    public class BezierCurveA
    {
        public BezierCurveA()
        {
            Arc arc = new Arc();
            LineGeometry line = new LineGeometry();
            LineSegment lineSegment = new LineSegment();
        }

    }
    public sealed class Arc : Shape
    {
        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register(nameof(Center), typeof(Point), typeof(Arc),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));

        public double StartAngle
        {
            get => (double)GetValue(StartAngleProperty);
            set => SetValue(StartAngleProperty, value);
        }

        // Using a DependencyProperty as the backing store for StartAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(Arc),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double EndAngle
        {
            get => (double)GetValue(EndAngleProperty);
            set => SetValue(EndAngleProperty, value);
        }

        // Using a DependencyProperty as the backing store for EndAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(Arc),
                new FrameworkPropertyMetadata(Math.PI / 2.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(Arc),
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public bool SmallAngle
        {
            get => (bool)GetValue(SmallAngleProperty);
            set => SetValue(SmallAngleProperty, value);
        }

        // Using a DependencyProperty as the backing store for SmallAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallAngleProperty =
            DependencyProperty.Register(nameof(SmallAngle), typeof(bool), typeof(Arc),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        static Arc() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(typeof(Arc)));

        protected override Geometry DefiningGeometry
        {
            get
            {
                double a0 = StartAngle < 0 ? StartAngle + 2 * Math.PI : StartAngle;
                double a1 = EndAngle < 0 ? EndAngle + 2 * Math.PI : EndAngle;

                if (a1 < a0)
                    a1 += Math.PI * 2;

                SweepDirection d = SweepDirection.Counterclockwise;
                bool large;

                if (SmallAngle)
                {
                    large = false;
                    d = (a1 - a0) > Math.PI ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
                }
                else
                    large = (Math.Abs(a1 - a0) < Math.PI);

                Point p0 = Center + new Vector(Math.Cos(a0), Math.Sin(a0)) * Radius;
                Point p1 = Center + new Vector(Math.Cos(a1), Math.Sin(a1)) * Radius;

                List<PathSegment> segments = new List<PathSegment>
            {
                new ArcSegment(p1, new Size(Radius, Radius), 0.0, large, d, true)
            };

                List<PathFigure> figures = new List<PathFigure>
            {
                new PathFigure(p0, segments, true)
                {
                    IsClosed = false
                }
            };

                return new PathGeometry(figures, FillRule.EvenOdd, null);
            }
        }

    }


    public sealed class BezierCurve : Shape
    {

        public Point Start
        {
            get => (Point)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register(nameof(Start), typeof(Point), typeof(BezierCurve),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));

        public Point End
        {
            get => (Point)GetValue(EndProperty);
            set => SetValue(EndProperty, value);
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register(nameof(End), typeof(Point), typeof(BezierCurve),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));

        public Point Control
        {
            get => (Point)GetValue(ControlProperty);
            set => SetValue(ControlProperty, value);
        }

        // Using a DependencyProperty as the backing store for Center.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ControlProperty =
            DependencyProperty.Register(nameof(Control), typeof(Point), typeof(BezierCurve),
                new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsRender));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(BezierCurve),
                new FrameworkPropertyMetadata(3.00, FrameworkPropertyMetadataOptions.AffectsRender));

        static BezierCurve() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Arc), new FrameworkPropertyMetadata(typeof(Arc)));

        protected override Geometry DefiningGeometry
        {
            get
            {
                //double a0 = StartAngle < 0 ? StartAngle + 2 * Math.PI : StartAngle;
                //double a1 = EndAngle < 0 ? EndAngle + 2 * Math.PI : EndAngle;

                //if (a1 < a0)
                //    a1 += Math.PI * 2;

                //SweepDirection d = SweepDirection.Counterclockwise;
                //bool large;

                //if (SmallAngle)
                //{
                //    large = false;
                //    d = (a1 - a0) > Math.PI ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
                //}
                //else
                //    large = (Math.Abs(a1 - a0) < Math.PI);

                //Point p0 = Center + new Vector(Math.Cos(a0), Math.Sin(a0)) * Radius;
                //Point p1 = Center + new Vector(Math.Cos(a1), Math.Sin(a1)) * Radius;

                //List<PathSegment> segments = new List<PathSegment>
                //{
                //    new ArcSegment(p1, new Size(Radius, Radius), 0.0, large, d, true)
                //};

                //List<PathFigure> figures = new List<PathFigure>
                //{
                //    new PathFigure(p0, segments, true)
                //    {
                //        IsClosed = false
                //    }
                //};

                return new PathGeometry();
            }
        }
    }
}
