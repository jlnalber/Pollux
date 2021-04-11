using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public new class Knoten : Graph.Graph.Knoten
        {
            //Members von der Klasse
            #region
            public Ellipse Ellipse;
            public Canvas Canvas;
            public Label Label;
            internal const int LabelToRight = 40;
            internal const int LabelToTop = 20;
            private delegate void Del();
            private bool IsFocus = false;//Gibt an, ob der Knoten gerade vom Benutzer bewegt wird

            #endregion

            //Konstruktoren der Klasse
            #region
            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Canvas canvas) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = CreateVisualNode();
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;
                this.Label = CreateLabel();

                canvas.Children.Add(this.Ellipse);
                canvas.Children.Add(this.Label);
            }

            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Canvas canvas, double x, double y) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = CreateVisualNode(x, y);
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;
                this.Label = CreateLabel();

                canvas.Children.Add(this.Ellipse);
                canvas.Children.Add(this.Label);
            }

            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Ellipse ellipse, Canvas canvas) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = ellipse;
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;

                //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus
                int index = ((GraphDarstellung)this.Parent).GraphKnoten.Contains(this) ? ((GraphDarstellung)this.Parent).GraphKnoten.IndexOf(this) : ((GraphDarstellung)this.Parent).GraphKnoten.Count;
                double x = (index % 10) * 100 + 20;
                double y = index / 10 * 100 + 25;
                this.Ellipse.Margin = new(x, y, 10, 10);
                Canvas.SetZIndex(this.Ellipse, 100);
                if (this.Canvas.Children.Count != 0)
                {
                    Canvas.SetTop(this.Ellipse, Canvas.GetTop(this.Canvas.Children[0]));
                    Canvas.SetLeft(this.Ellipse, Canvas.GetLeft(this.Canvas.Children[0]));
                }
                else
                {
                    Canvas.SetTop(this.Ellipse, 0);
                    Canvas.SetLeft(this.Ellipse, 0);
                }

                this.Label = CreateLabel();

                canvas.Children.Add(this.Ellipse);
                canvas.Children.Add(this.Label);
            }

            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Ellipse ellipse, Label label, Canvas canvas) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = ellipse;
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;
                this.Label = label;

                //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus
                int index = ((GraphDarstellung)this.Parent).GraphKnoten.Contains(this) ? ((GraphDarstellung)this.Parent).GraphKnoten.IndexOf(this) : ((GraphDarstellung)this.Parent).GraphKnoten.Count;
                double x = (index % 10) * 100 + 20;
                double y = index / 10 * 100 + 25;
                this.Ellipse.Margin = new(x, y, 10, 10);
                Canvas.SetZIndex(this.Ellipse, 100);
                if (this.Canvas.Children.Count != 0)
                {
                    Canvas.SetTop(this.Ellipse, Canvas.GetTop(this.Canvas.Children[0]));
                    Canvas.SetLeft(this.Ellipse, Canvas.GetLeft(this.Canvas.Children[0]));
                }
                else
                {
                    Canvas.SetTop(this.Ellipse, 0);
                    Canvas.SetLeft(this.Ellipse, 0);
                }

                this.Label.HorizontalAlignment = HorizontalAlignment.Left;
                this.Label.VerticalAlignment = VerticalAlignment.Top;
                this.Label.Margin = new(this.Ellipse.Margin.Left + LabelToRight, this.Ellipse.Margin.Top - LabelToTop, 10, 10);
                Canvas.SetZIndex(this.Label, 100);
                if (this.Canvas.Children.Count != 0)
                {
                    Canvas.SetTop(this.Label, Canvas.GetTop(this.Canvas.Children[0]));
                    Canvas.SetLeft(this.Label, Canvas.GetLeft(this.Canvas.Children[0]));
                }
                else
                {
                    Canvas.SetTop(this.Label, 0);
                    Canvas.SetLeft(this.Label, 0);
                }

                canvas.Children.Add(this.Ellipse);
                canvas.Children.Add(this.Label);
            }
            #endregion

            public Graph.Graph.Knoten CastToKnoten(Graph.Graph graph)
            {
                return new Graph.Graph.Knoten(graph, this.Kanten, this.Name);
            }

            public Graph.Graph.Knoten CastToKnoten()
            {
                Graph.Graph graph = new();
                return new Graph.Graph.Knoten(graph, this.Kanten, this.Name);
            }

            //Lässt den Knoten verschwinden
            public bool Disappear()
            {
                try
                {
                    this.Canvas.Children.Remove(this.Ellipse);
                    this.Canvas.Children.Remove(this.Label);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            //Event-Methoden, die die Ellipsen bewegen, und deren angrenzende Kanten neu malt
            #region
            private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (e.ClickCount == 1)
                {
                    //Wenn der Knoten gedrückt wird, dann speichere ab, dass das jetzt passiert und bewege die Ellipse, und deren angrenzende Kanten
                    this.IsFocus = true;
                    this.Redraw();
                }
                else if (e.ClickCount == 2)
                {
                    //Wenn der Knoten doppelt gedrückt wird, dann öffne seine Eigenschaften
                    MainWindow.main.OpenedEigenschaftenFenster[MainWindow.main.GetOpenTab()].OpenNode(this);
                }
            }

            private void MouseMove(object sender, MouseEventArgs e)
            {
                //Falls sowohl gerade der Fokus auf ihr ist und die Maus gedrückt, bewege die Ellipse, und deren angrenzende Kanten
                if (this.IsFocus && e.LeftButton == MouseButtonState.Pressed)
                {
                    this.Redraw();
                }
                else
                {
                    //Setzte "IsFocus" auf falsch, für den Fall, dass sie nicht mehr angeklickt wird 
                    this.IsFocus = false;
                }
            }
            #endregion

            //Methode, um den Graph neu malen zu lassen
            public void Redraw(bool positionMyself = true)
            {
                if (positionMyself)
                {
                    //Platziere die Ellipse
                    Thickness thickness = new Thickness();
                    Point point = Mouse.GetPosition(this.Canvas);
                    point.X -= Canvas.GetLeft(this.Ellipse);
                    point.Y -= Canvas.GetTop(this.Ellipse);
                    thickness.Left = point.X - this.Ellipse.Width / 2;
                    thickness.Top = point.Y - this.Ellipse.Height / 2;
                    thickness.Bottom = 10;
                    thickness.Right = 10;
                    this.Ellipse.Margin = thickness;

                    //Gebe den Namen auch noch einmal an
                    this.RedrawName();
                }

                foreach (Kanten i in this.Kanten)
                {
                    //Rechne die Position der anliegenden Kanten nach und justiere sie
                    if (i.Line is Ellipse ellipse)
                    {
                        //Für den Fall, dass es eine Schlinge ist
                        //Finde das Margin des Knotens zur Schlinge heraus
                        Thickness knoten = this.Ellipse.Margin;

                        //lege die Position fest
                        ellipse.Margin = new Thickness(knoten.Left - ellipse.Width / 2 - 10, knoten.Top - 5, 10, 10);
                    }
                    else if (i.Line is Line line)
                    {
                        //Für den Fall, dass es eine ganz normale Kante ist
                        bool isFirst = i.Knoten[0] == this;

                        //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                        Thickness marginKnoten0 = ((Knoten)i.Knoten[0]).Ellipse.Margin;
                        Thickness marginKnoten1 = ((Knoten)i.Knoten[1]).Ellipse.Margin;
                        double height = this.Ellipse.Height;

                        //finde dadurch die Position heraus, wo die Kante starten und enden muss
                        const double maxDistance = 10;
                        double distance = Strings.Bigger(Math.Abs(marginKnoten0.Top - marginKnoten1.Top), Math.Abs(marginKnoten0.Left - marginKnoten1.Left)) * 0.1 + 1;
                        distance = ((distance > maxDistance) ? maxDistance : distance) + this.Ellipse.Height / 2 - 5;
                        double y = Math.Sqrt(distance * distance / (Math.Pow(Math.Abs((marginKnoten0.Left - marginKnoten1.Left) / (marginKnoten0.Top - marginKnoten1.Top)), 2) + 1));
                        double x = Math.Sqrt(distance * distance - y * y);
                        y = !(y > 0) ? 0 : y;
                        x = !(x > 0) ? 0 : x;

                        if (isFirst)
                        {
                            //schreibe diese Eigenschaften in die Linie
                            if (marginKnoten0.Top > marginKnoten1.Top)
                            {
                                line.Y1 = marginKnoten0.Top + height / 2 - y;
                                //line.Y2 = marginKnoten1.Top + height / 2 + y;
                            }
                            else
                            {
                                line.Y1 = marginKnoten0.Top + height / 2 + y;
                                //line.Y2 = marginKnoten1.Top + height / 2 - y;
                            }
                            if (marginKnoten0.Left > marginKnoten1.Left)
                            {
                                line.X1 = marginKnoten0.Left + height / 2 - x;
                                //line.X2 = marginKnoten1.Left + height / 2 + x;
                            }
                            else
                            {
                                line.X1 = marginKnoten0.Left + height / 2 + x;
                                //line.X2 = marginKnoten1.Left + height / 2 - x;
                            }

                            if (positionMyself) ((GraphDarstellung.Knoten)i.Knoten[1]).Redraw(false);
                        }
                        else
                        {
                            //schreibe diese Eigenschaften in die Linie
                            if (marginKnoten0.Top > marginKnoten1.Top)
                            {
                                //line.Y1 = marginKnoten0.Top + height / 2 - y;
                                line.Y2 = marginKnoten1.Top + height / 2 + y;
                            }
                            else
                            {
                                //line.Y1 = marginKnoten0.Top + height / 2 + y;
                                line.Y2 = marginKnoten1.Top + height / 2 - y;
                            }
                            if (marginKnoten0.Left > marginKnoten1.Left)
                            {
                                //line.X1 = marginKnoten0.Left + height / 2 - x;
                                line.X2 = marginKnoten1.Left + height / 2 + x;
                            }
                            else
                            {
                                //line.X1 = marginKnoten0.Left + height / 2 + x;
                                line.X2 = marginKnoten1.Left + height / 2 - x;
                            }

                            if (positionMyself) ((GraphDarstellung.Knoten)i.Knoten[0]).Redraw(false);
                        }
                    }
                }
            }

            //Methode, um den Namen nachzufahren
            public void RedrawName()
            {
                if (this.Label.Content.ToString() != this.Name)
                {
                    this.Label.Content = this.Name;
                }
                this.Label.Margin = new(this.Ellipse.Margin.Left + LabelToRight, this.Ellipse.Margin.Top - LabelToTop, 10, 10);
            }

            //Methode um einen Visuellen Knoten zu erstellen
            private Ellipse CreateVisualNode()
            {
                //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus
                int index = ((GraphDarstellung)this.Parent).GraphKnoten.Contains(this) ? ((GraphDarstellung)this.Parent).GraphKnoten.IndexOf(this) : ((GraphDarstellung)this.Parent).GraphKnoten.Count;

                //Rückgabe
                return CreateVisualNode((index % 10) * 100 + 20, index / 10 * 100 + 25);
            }

            private Ellipse CreateVisualNode(double x, double y)
            {
                //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus

                //Erstelle einen Knoten
                KnotenEllipse knotenEllipse = new();
                Ellipse ellipse = knotenEllipse.Ellipse;
                knotenEllipse.Content = null;

                //Mache Feinheiten an der Ellipse
                ellipse.Margin = new(x, y, 10, 10);
                Canvas.SetZIndex(ellipse, 100);
                if (this.Canvas.Children.Count != 0)
                {
                    Canvas.SetTop(ellipse, Canvas.GetTop(this.Canvas.Children[0]));
                    Canvas.SetLeft(ellipse, Canvas.GetLeft(this.Canvas.Children[0]));
                }
                else
                {
                    Canvas.SetTop(ellipse, 0);
                    Canvas.SetLeft(ellipse, 0);
                }

                //Rückgabe
                return ellipse;
            }

            //Methode, um einen Label zum Knoten zu erstellen
            private Label CreateLabel()
            {
                //Erstelle den Label
                Label label = new();

                //Mache Feinheiten an dem Label
                label.Content = this.Name;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Margin = new(this.Ellipse.Margin.Left + LabelToRight, this.Ellipse.Margin.Top - LabelToTop, 10, 10);
                Canvas.SetZIndex(label, 100);
                if (this.Canvas.Children.Count != 0)
                {
                    Canvas.SetTop(label, Canvas.GetTop(this.Canvas.Children[0]));
                    Canvas.SetLeft(label, Canvas.GetLeft(this.Canvas.Children[0]));
                }
                else
                {
                    Canvas.SetTop(label, 0);
                    Canvas.SetLeft(label, 0);
                }

                //Rückgabe
                return label;
            }
        }
    }
}
