using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Castor
{
    public partial class EdgeLine : UserControl
    {
        //public PathCollection Points;
        private const double Addition = 10;

        //Members der Klasse
        #region
        private double x1;
        public double X1
        {
            get
            {
                return this.x1;
            }
            set
            {
                this.x1 = value;
                this.Redraw();
            }
        }
        private double y1;
        public double Y1
        {
            get
            {
                return this.y1;
            }
            set
            {
                this.y1 = value;
                this.Redraw();
            }
        }
        private double x2;
        public double X2
        {
            get
            {
                return this.x2;
            }
            set
            {
                this.x2 = value;
                this.Redraw();
            }
        }
        private double y2;
        public double Y2
        {
            get
            {
                return this.y2;
            }
            set
            {
                this.y2 = value;
                this.Redraw();
            }
        }
        #endregion

        public EdgeLine()
        {
            InitializeComponent();

            this.X1 = 0;
            this.Y1 = 0;
            this.X2 = 0;
            this.Y2 = 0;
            //this.Direction1 = Directions.FromRight;
            //this.Direction2 = Directions.FromLeft;

            this.Redraw();
            /*this.Points = new();
            this.Points.OnChange += this.Redraw;*/
        }

        public EdgeLine(double x1, double y1, double x2, double y2)
        {
            /*if (((int)direction1 + (int) direction2) % 2 == 1)
            {
                throw new InvalidSidesException();
            }*/

            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
            //this.Direction1 = direction1;
            //this.Direction2 = direction2;

            this.Redraw();
        }

        public void Set(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            //this.direction1 = direction1;
            //this.direction2 = direction2;
            this.Redraw();
        }

        public SolidColorBrush GetStroke()
        {
            return (SolidColorBrush)this.Line1.Stroke;
        }

        public void SetStroke(Color color)
        {
            this.SetStroke(new SolidColorBrush(color));
        }

        public void SetStroke(SolidColorBrush brush)
        {
            this.Line1.Stroke = brush;
            this.Line2.Stroke = brush;
            this.Line3.Stroke = brush;
        }

        public double GetStrokeThickness()
        {
            return this.Line1.StrokeThickness;
        }

        public void SetStrokeThicknes(double strokeThickness)
        {
            this.Line1.StrokeThickness = strokeThickness;
            this.Line2.StrokeThickness = strokeThickness;
            this.Line3.StrokeThickness = strokeThickness;
        }

        public void Redraw()
        {
            //TODO: implementieren
            this.Margin = new Thickness(x1 > x2 ? x2 : x1, (y1 > y2 ? y2 : y1) - Addition, 0, 0);
            this.Width = Math.Abs(x1 - x2);
            this.Height = Math.Abs(y1 - y2) + Addition;

            this.Line1.X1 = (x1 > x2 ? x2 : x1) - this.Margin.Left;
            this.Line1.Y1 = (x1 > x2 ? y2 : y1) - this.Margin.Top;
            this.Line1.X2 = 0;
            this.Line1.Y2 = 0;
            this.Line2.X1 = 0;
            this.Line2.Y1 = 0;
            this.Line2.X2 = this.Width;
            this.Line2.Y2 = 0;
            this.Line3.X1 = this.Width;
            this.Line3.Y1 = 0;
            this.Line3.X2 = (x1 > x2 ? x1 : x2) - this.Margin.Left;
            this.Line3.Y2 = (x1 > x2 ? y1 : y2) - this.Margin.Top;

            Debug.WriteLine(this.Margin.Left + " " + this.Margin.Top + " " + this.Margin.Right + " " + this.Margin.Bottom);
        }

        public enum Directions
        {
            FromLeft = 0, FromTop = 1, FromRight = 2, FromBottom = 3
        }

        public class InvalidSidesException : Exception { }

        /*
        public static Line GetLine()
        {
            Line line = new();
            return line;
        }
        public class PathCollection : IEnumerable
        {
            public delegate void Event();
            public Event OnChange;
            public PointWrapper Start;

            public PathCollection()
            {
                this.OnChange = () => { };
            }

            public void Add(Point p)
            {
                PointWrapper pw = new(p, null);
                if (this.Start == null)
                {
                    this.Start = pw;
                }
                else
                {
                    PointWrapper last = this.Start;
                    while (last.NextPointWrapper != null)
                    {
                        last = last.NextPointWrapper;
                    }
                    last.NextPointWrapper = pw;

                    OnChange();
                }
            }

            public void Remove(Point p)
            {
                PointWrapper before = this.Start;

                try
                {
                    while(before.NextPointWrapper.Point != p)
                    {
                        before = before.NextPointWrapper;
                    }
                    before.NextPointWrapper = before.NextPointWrapper.NextPointWrapper;

                    OnChange();
                }
                catch { }
            }

            public void Insert(Point p, int index)
            {
                PointWrapper last = this.Start;
                for (int i = 0; i < index; i++)
                {
                    last = last.NextPointWrapper;
                }
                last.NextPointWrapper = new PointWrapper(p, last.NextPointWrapper);

                OnChange();
            }

            public int Count
            {
                get
                {
                    int counter = 0;
                    PointWrapper last = this.Start;
                    for (; last.NextPointWrapper != null; last = last.NextPointWrapper, counter++) ;
                    return counter;
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new PathEnumerator(this);
            }

            public class PathEnumerator : IEnumerator
            {
                public PathEnumerator(PathCollection pathCollection)
                {
                    this.current = pathCollection.Start;
                    this.PathCollection = pathCollection;
                }

                private PathCollection PathCollection;
                private PointWrapper current;
                public object Current
                {
                    get
                    {
                        return this.current.Point;
                    }
                }

                public bool MoveNext()
                {
                    if (this.current == null || this.current.NextPointWrapper == null)
                    {
                        return false;
                    }

                    this.current = this.current.NextPointWrapper;
                    return true;
                }

                public void Reset()
                {
                    this.current = this.PathCollection.Start;
                }
            }

            public class PointWrapper
            {
                public Point Point;
                public PointWrapper NextPointWrapper;

                public PointWrapper(Point point, PointWrapper nextPointWrapper)
                {
                    this.Point = point;
                    this.NextPointWrapper = nextPointWrapper;
                }
            }
        }*/
    }
}
