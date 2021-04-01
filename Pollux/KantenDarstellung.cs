using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public new class Kanten : Graph.Graph.Kanten
        {
            //Members der Klasse
            public UIElement Line;
            public Canvas Canvas;

            public Kanten(GraphDarstellung graph, Knoten[] knoten, string name, Canvas canvas) : base(graph, knoten, name)
            {
                //falls schon eine Kante mit dem gelichen Namen existiert, werfe eine Exception
                if (graph.ContainsKanten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //Lege die Eigenschaften fest
                this.Parent = graph;
                this.Knoten = knoten;
                this.Name = name;
                this.Canvas = canvas;
                this.Line = CreateVisualEdge();

                //Füge die Kante dem Canvas hinzu
                this.Canvas.Children.Add(this.Line);
            }

            public Kanten(GraphDarstellung graph, Knoten[] knoten, string name, UIElement line, Canvas canvas) : base(graph, knoten, name)
            {
                //falls schon eine Kante mit dem gelichen Namen existiert, werfe eine Exception
                if (graph.ContainsKanten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //Lege die Eigenschaften fest
                this.Parent = graph;
                this.Knoten = knoten;
                this.Name = name;
                this.Canvas = canvas;
                if (line is Line line1)
                {
                    this.Line = line1;

                    //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten0 = ((Knoten)this.Knoten[0]).Ellipse.Margin;
                    Thickness marginKnoten1 = ((Knoten)this.Knoten[1]).Ellipse.Margin;
                    double height = ((Knoten)this.Knoten[0]).Ellipse.Height;

                    //finde dadurch die Position heraus, wo die Kante starten und enden muss
                    const double maxDistance = 20;
                    double distance = Strings.Bigger(Math.Abs(marginKnoten0.Top - marginKnoten1.Top), Math.Abs(marginKnoten0.Left - marginKnoten1.Left)) * 0.1 + 1;
                    distance = (distance > maxDistance) ? maxDistance : distance;
                    double y = Math.Sqrt(distance * distance / (Math.Pow(Math.Abs((marginKnoten0.Left - marginKnoten1.Left) / (marginKnoten0.Top - marginKnoten1.Top)), 2) + 1));
                    double x = Math.Sqrt(distance * distance - y * y);
                    y = !(y > 0) ? 0 : y;
                    x = !(x > 0) ? 0 : x;

                    //schreibe diese Eigenschaften in die Linie
                    if (marginKnoten0.Top > marginKnoten1.Top)
                    {
                        line1.Y1 = marginKnoten0.Top + height / 2 - y;
                        line1.Y2 = marginKnoten1.Top + height / 2 + y;
                    }
                    else
                    {
                        line1.Y1 = marginKnoten0.Top + height / 2 + y;
                        line1.Y2 = marginKnoten1.Top + height / 2 - y;
                    }
                    if (marginKnoten0.Left > marginKnoten1.Left)
                    {
                        line1.X1 = marginKnoten0.Left + height / 2 - x;
                        line1.X2 = marginKnoten1.Left + height / 2 + x;
                    }
                    else
                    {
                        line1.X1 = marginKnoten0.Left + height / 2 + x;
                        line1.X2 = marginKnoten1.Left + height / 2 - x;
                    }
                    Canvas.SetTop(line1, 0);
                    Canvas.SetLeft(line1, 0);
                }
                else if (line is Ellipse ellipse)
                {
                    this.Line = ellipse;

                    //Finde die Margins des Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten = ((Knoten)this.Knoten[0]).Ellipse.Margin;
                    if (this.Canvas.Children.Count != 0)
                    {
                        Canvas.SetTop(this.Line, Canvas.GetTop(this.Canvas.Children[0]));
                        Canvas.SetLeft(this.Line, Canvas.GetLeft(this.Canvas.Children[0]));
                    }
                    else
                    {
                        Canvas.SetTop(this.Line, 0);
                        Canvas.SetLeft(this.Line, 0);
                    }

                    //lege die Position fest
                    ellipse.Margin = new Thickness(marginKnoten.Left - ellipse.Width / 2 - 10, marginKnoten.Top - 5, 10, 10);
                }
                else
                {
                    throw new GraphExceptions.UnsupportedUIElementException();
                }

                //Füge die Kante dem Canvas hinzu
                this.Canvas.Children.Add(this.Line);
            }

            public Graph.Graph.Kanten CastToKanten(Graph.Graph graph)
            {
                return new Graph.Graph.Kanten(graph, this.Knoten, Name);
            }

            public Graph.Graph.Kanten CastToKanten()
            {
                return new Graph.Graph.Kanten(new(), this.Knoten, Name);
            }

            public bool Disappear()
            {
                try
                {
                    this.Canvas.Children.Remove(this.Line);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private UIElement CreateVisualEdge()
            {
                if (this.Knoten[0] == this.Knoten[1])
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    KantenEllipse kantenEllipse = new();
                    Ellipse ellipse = kantenEllipse.Ellipse;
                    kantenEllipse.Content = null;

                    //Finde die Margins des Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten = ((Knoten)this.Knoten[0]).Ellipse.Margin;
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

                    //lege die Position fest
                    ellipse.Margin = new Thickness(marginKnoten.Left - ellipse.Width / 2 - 10, marginKnoten.Top - 5, 10, 10);

                    //Rückgabe
                    return ellipse;
                }
                else
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    KantenLine kantenLine = new();
                    Line line = kantenLine.Line;
                    kantenLine.Content = null;

                    //Erstelle die visuelle Kante "kanten"

                    //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten0 = ((Knoten)this.Knoten[0]).Ellipse.Margin;
                    Thickness marginKnoten1 = ((Knoten)this.Knoten[1]).Ellipse.Margin;
                    double height = ((Knoten)this.Knoten[0]).Ellipse.Height;

                    //finde dadurch die Position heraus, wo die Kante starten und enden muss
                    const double maxDistance = 20;
                    double distance = Strings.Bigger(Math.Abs(marginKnoten0.Top - marginKnoten1.Top), Math.Abs(marginKnoten0.Left - marginKnoten1.Left)) * 0.1 + 1;
                    distance = (distance > maxDistance) ? maxDistance : distance;
                    double y = Math.Sqrt(distance * distance / (Math.Pow(Math.Abs((marginKnoten0.Left - marginKnoten1.Left) / (marginKnoten0.Top - marginKnoten1.Top)), 2) + 1));
                    double x = Math.Sqrt(distance * distance - y * y);
                    y = !(y > 0) ? 0 : y;
                    x = !(x > 0) ? 0 : x;

                    //schreibe diese Eigenschaften in die Linie
                    if (marginKnoten0.Top > marginKnoten1.Top)
                    {
                        line.Y1 = marginKnoten0.Top + height / 2 - y;
                        line.Y2 = marginKnoten1.Top + height / 2 + y;
                    }
                    else
                    {
                        line.Y1 = marginKnoten0.Top + height / 2 + y;
                        line.Y2 = marginKnoten1.Top + height / 2 - y;
                    }
                    if (marginKnoten0.Left > marginKnoten1.Left)
                    {
                        line.X1 = marginKnoten0.Left + height / 2 - x;
                        line.X2 = marginKnoten1.Left + height / 2 + x;
                    }
                    else
                    {
                        line.X1 = marginKnoten0.Left + height / 2 + x;
                        line.X2 = marginKnoten1.Left + height / 2 - x;
                    }
                    Canvas.SetTop(line, 0);
                    Canvas.SetLeft(line, 0);

                    //Rückgabe
                    return line;
                }
            }

            public void Eigenschaften_Click(object sender, RoutedEventArgs e)
            {
                //Methode, wenn das MenuItem "eigenschaften" geklickt wurde
                MainWindow.main.OpenedEigenschaftenFenster[MainWindow.main.GetOpenTab()].OpenEdge(this);
            }
        }
    }
}
