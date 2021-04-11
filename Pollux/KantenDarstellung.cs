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

                ((Knoten)this.Knoten[0]).Redraw(false);
                ((Knoten)this.Knoten[1]).Redraw(false);
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
                if (!(line is Ellipse || line is Line))
                {
                    throw new GraphExceptions.UnsupportedUIElementException();
                }

                //Füge die Kante dem Canvas hinzu
                this.Canvas.Children.Add(this.Line);

                ((Knoten)this.Knoten[0]).Redraw(false);
                ((Knoten)this.Knoten[1]).Redraw(false);
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
                else
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    KantenLine kantenLine = new();
                    Line line = kantenLine.Line;
                    kantenLine.Content = null;

                    if (this.Canvas.Children.Count != 0)
                    {
                        Canvas.SetTop(line, Canvas.GetTop(this.Canvas.Children[0]));
                        Canvas.SetLeft(line, Canvas.GetLeft(this.Canvas.Children[0]));
                    }
                    else
                    {
                        Canvas.SetTop(line, 0);
                        Canvas.SetLeft(line, 0);
                    }

                    line.X1 = 0;
                    line.X2 = 0;
                    line.Y1 = 0;
                    line.Y2 = 0;

                    //Rückgabe
                    return line;
                }
            }
        }
    }
}
