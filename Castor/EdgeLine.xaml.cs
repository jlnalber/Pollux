using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Castor
{
    public partial class EdgeLine : UserControl
    {
        //Members der Klasse
        private FrameworkElement uiElement1;
        public FrameworkElement UIElement1
        {
            get
            {
                return this.uiElement1;
            }
            set
            {
                this.uiElement1 = value;
                this.Redraw();
            }
        }
        private FrameworkElement uiElement2;
        public FrameworkElement UIElement2
        {
            get
            {
                return this.uiElement2;
            }
            set
            {
                this.uiElement2 = value;
                this.Redraw();
            }
        }

        private const double LoopAddition = 100;

        public EdgeLine()
        {
            InitializeComponent();

            this.uiElement1 = new();
            this.uiElement2 = new();
            this.Redraw();
        }

        public EdgeLine(FrameworkElement uiElement1, FrameworkElement uiElement2)
        {
            InitializeComponent();

            this.uiElement1 = uiElement1;
            this.uiElement2 = uiElement2;
            this.Redraw();
        }

        public void Set(FrameworkElement uiElement1, FrameworkElement uiElement2)
        {
            this.uiElement1 = uiElement1;
            this.uiElement2 = uiElement2;
            this.Redraw();
        }

        public SolidColorBrush GetStroke()
        {
            return (SolidColorBrush)this.Path.Stroke;
        }

        public void SetStroke(Color color)
        {
            this.SetStroke(new SolidColorBrush(color));
        }

        public void SetStroke(SolidColorBrush brush)
        {
            this.Path.Stroke = brush;
        }

        public double GetStrokeThickness()
        {
            return this.Path.StrokeThickness;
        }

        public void SetStrokeThicknes(double strokeThickness)
        {
            this.Path.StrokeThickness = strokeThickness;
        }

        public void Redraw()
        {
            try
            {
                if (this.uiElement1 == this.uiElement2)
                {
                    //Im Falle einer Schlinge.
                    this.Width = this.uiElement1.Width + LoopAddition / 2;
                    this.Height = this.uiElement1.Height / 2 + LoopAddition;
                    this.Margin = new Thickness(this.uiElement1.Margin.Left - LoopAddition / 4, this.uiElement1.Margin.Top - LoopAddition, this.uiElement1.Margin.Right + LoopAddition / 4, this.uiElement1.Margin.Bottom - this.uiElement1.Height / 2);
                    this.PathFigure.StartPoint = new(this.uiElement1.Margin.Left - this.Margin.Left, this.uiElement1.Margin.Top + this.uiElement1.Height / 2 - this.Margin.Top);
                    this.BezierSegment.Point1 = new(0, 2 * this.Height / 3);
                    this.BezierSegment.Point2 = new(this.Width, 2 * this.Height / 3);
                    this.BezierSegment.Point3 = new(this.uiElement1.Margin.Left + this.uiElement1.Width - this.Margin.Left, this.uiElement1.Margin.Top + this.uiElement1.Height / 2 - this.Margin.Top);
                }
                else
                {
                    //Im Falle einer normalen Kante.
                    double x1, y1, x2, y2, x3, y3, x4, y4;
                    if (Math.Abs(this.uiElement1.Margin.Left - this.uiElement2.Margin.Left) > Math.Abs(this.uiElement1.Margin.Top - this.uiElement2.Margin.Top))
                    {
                        if (this.uiElement1.Margin.Left > this.uiElement2.Margin.Left)
                        {
                            x1 = this.uiElement1.Margin.Left;
                            y1 = this.uiElement1.Margin.Top + this.uiElement1.Height / 2;
                            x4 = this.uiElement2.Margin.Left + this.uiElement2.Width;
                            y4 = this.uiElement2.Margin.Top + this.uiElement2.Height / 2;
                        }
                        else
                        {
                            x1 = this.uiElement1.Margin.Left + this.uiElement1.Width;
                            y1 = this.uiElement1.Margin.Top + this.uiElement1.Height / 2;
                            x4 = this.uiElement2.Margin.Left;
                            y4 = this.uiElement2.Margin.Top + this.uiElement2.Height / 2;
                        }

                        x2 = 2 * x1 / 3 + x4 / 3;
                        y2 = 5 * y1 / 6 + y4 / 6;
                        x3 = x1 / 3 + 2 * x4 / 3;
                        y3 = y1 / 6 + 5 * y4 / 6;
                    }
                    else
                    {
                        if (this.uiElement1.Margin.Top > this.uiElement2.Margin.Top)
                        {
                            x1 = this.uiElement1.Margin.Left + this.uiElement1.Width / 2;
                            y1 = this.uiElement1.Margin.Top;
                            x4 = this.uiElement2.Margin.Left + this.uiElement2.Width / 2;
                            y4 = this.uiElement2.Margin.Top + this.uiElement2.Height;
                        }
                        else
                        {
                            x1 = this.uiElement1.Margin.Left + this.uiElement1.Width / 2;
                            y1 = this.uiElement1.Margin.Top + this.uiElement1.Height;
                            x4 = this.uiElement2.Margin.Left + this.uiElement2.Width / 2;
                            y4 = this.uiElement2.Margin.Top;
                        }

                        x2 = 5 * x1 / 6 + x4 / 6;
                        y2 = 2 * y1 / 3 + y4 / 3;
                        x3 = x1 / 6 + 5 * x4 / 6;
                        y3 = y1 / 3 + 2 * y4 / 3;
                    }

                    this.Margin = new Thickness(x1 > x4 ? x4 : x1, (y1 > y4 ? y4 : y1), 0, 0);
                    this.Width = Math.Abs(x1 - x4);
                    this.Height = Math.Abs(y1 - y4);

                    this.PathFigure.StartPoint = new Point(x1 - this.Margin.Left, y1 - this.Margin.Top);
                    this.BezierSegment.Point1 = new Point(x2 - this.Margin.Left, y2 - this.Margin.Top);
                    this.BezierSegment.Point2 = new Point(x3 - this.Margin.Left, y3 - this.Margin.Top);
                    this.BezierSegment.Point3 = new Point(x4 - this.Margin.Left, y4 - this.Margin.Top);
                }
            }
            catch { }
        }
    }
}
