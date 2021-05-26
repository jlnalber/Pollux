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

        private EdgeTypes edgeType;
        public EdgeTypes EdgeType
        {
            get
            {
                return this.edgeType;
            }
            set
            {
                this.SetVisibility(Visibility.Collapsed);
                this.edgeType = value;
                switch (value)
                {
                    case EdgeTypes.NormalEdge: this.Line1.Visibility = Visibility.Visible; break;
                    case EdgeTypes.StraightEdge: this.Line1.Visibility = Visibility.Visible; this.Line2.Visibility = Visibility.Visible; this.Line3.Visibility = Visibility.Visible; break;
                    case EdgeTypes.BezierEdge: this.Path.Visibility = Visibility.Visible; break;
                }

                this.Redraw();
            }
        }

        private const double LoopAddition = 100;

        public EdgeLine()
        {
            InitializeComponent();

            this.EdgeType = EdgeTypes.NormalEdge;
            this.uiElement1 = new();
            this.uiElement2 = new();
            this.Redraw();
        }

        public EdgeLine(FrameworkElement uiElement1, FrameworkElement uiElement2)
        {
            InitializeComponent();

            this.EdgeType = EdgeTypes.NormalEdge;
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
            this.Line1.Stroke = brush;
            this.Line2.Stroke = brush;
            this.Line3.Stroke = brush;
        }

        public double GetStrokeThickness()
        {
            return this.Path.StrokeThickness;
        }

        public void SetStrokeThicknes(double strokeThickness)
        {
            this.Path.StrokeThickness = strokeThickness;
            this.Line1.StrokeThickness = strokeThickness;
            this.Line2.StrokeThickness = strokeThickness;
            this.Line3.StrokeThickness = strokeThickness;
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
                    
                    //Mache die den "Path" sichtbar.
                    this.SetVisibility(Visibility.Collapsed);
                    this.Path.Visibility = Visibility.Visible;
                }
                else if (this.EdgeType == EdgeTypes.NormalEdge)
                {
                    double x1, y1, x2, y2;
                    x1 = this.UIElement1.Margin.Left + this.UIElement1.Width / 2;
                    y1 = this.UIElement1.Margin.Top + this.UIElement1.Height / 2;
                    x2 = this.UIElement2.Margin.Left + this.UIElement2.Width / 2;
                    y2 = this.UIElement2.Margin.Top + this.UIElement2.Height / 2;

                    this.Margin = new Thickness(x1 > x2 ? x2 : x1, (y1 > y2 ? y2 : y1), 0, 0);
                    this.Width = Math.Abs(x1 - x2);
                    this.Height = Math.Abs(y1 - y2);

                    this.Line1.X1 = x1 - this.Margin.Left;
                    this.Line1.Y1 = y1 - this.Margin.Top;
                    this.Line1.X2 = x2 - this.Margin.Left;
                    this.Line1.Y2 = y2 - this.Margin.Top;
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

                        x2 = (x1 + x4) / 2;
                        y2 = y1;
                        x3 = (x1 + x4) / 2;
                        y3 = y4;
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

                        x2 = x1;
                        y2 = (y1 + y4) / 2;
                        x3 = x4;
                        y3 = (y1 + y4) / 2;
                    }

                    this.Margin = new Thickness(x1 > x4 ? x4 : x1, (y1 > y4 ? y4 : y1), 0, 0);
                    this.Width = Math.Abs(x1 - x4);
                    this.Height = Math.Abs(y1 - y4);

                    if (this.EdgeType == EdgeTypes.BezierEdge)
                    {
                        this.PathFigure.StartPoint = new Point(x1 - this.Margin.Left, y1 - this.Margin.Top);
                        this.BezierSegment.Point1 = new Point(x2 - this.Margin.Left, y2 - this.Margin.Top);
                        this.BezierSegment.Point2 = new Point(x3 - this.Margin.Left, y3 - this.Margin.Top);
                        this.BezierSegment.Point3 = new Point(x4 - this.Margin.Left, y4 - this.Margin.Top);
                    }
                    else if (this.EdgeType == EdgeTypes.StraightEdge)
                    {
                        this.Line1.X1 = x1 - this.Margin.Left;
                        this.Line1.Y1 = y1 - this.Margin.Top;
                        this.Line1.X2 = x2 - this.Margin.Left;
                        this.Line1.Y2 = y2 - this.Margin.Top;
                        this.Line2.X1 = x2 - this.Margin.Left;
                        this.Line2.Y1 = y2 - this.Margin.Top;
                        this.Line2.X2 = x3 - this.Margin.Left;
                        this.Line2.Y2 = y3 - this.Margin.Top;
                        this.Line3.X1 = x3 - this.Margin.Left;
                        this.Line3.Y1 = y3 - this.Margin.Top;
                        this.Line3.X2 = x4 - this.Margin.Left;
                        this.Line3.Y2 = y4 - this.Margin.Top;
                    }
                }
            }
            catch { }
        }

        private void SetVisibility(Visibility visibility)
        {
            this.Path.Visibility = visibility;
            this.Line1.Visibility = visibility;
            this.Line2.Visibility = visibility;
            this.Line3.Visibility = visibility;
        }

        public enum EdgeTypes
        {
            NormalEdge, StraightEdge, BezierEdge
        }

        private enum Arrows
        {
            NoArrow, ArrowToUIElement1, ArrowToUIElement2, BothArrows
        }
    }
}
