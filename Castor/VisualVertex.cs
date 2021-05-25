using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Thestias;

namespace Castor
{
    public partial class VisualVertex
    {
        //TODO: Label implementieren
        public VisualGraph Graph;
        public Graph.Vertex Vertex;
        public FrameworkElement UIElement;
        public bool IsFocus = false;
        public List<VisualEdge> Edges;

        //Konstruktoren
        #region
        public VisualVertex(Graph.Vertex vertex, VisualGraph graph)
        {
            //Member festlegen
            this.Graph = graph;
            this.Vertex = vertex;
            this.UIElement = new VertexEllipse(graph, this);
            this.Edges = new();//Eventuell nach den Edges des Thestias.Vertex suchen und hier deren VisualEdges einfügen.
            this.UIElement.MouseMove += this.UIElement_MouseMove;
            this.UIElement.MouseDown += this.UIElement_MouseDown;
        }

        public VisualVertex(Graph.Vertex vertex, VisualGraph graph, FrameworkElement uiElement)
        {
            //Member festlegen
            this.Graph = graph;
            this.Vertex = vertex;
            this.UIElement = uiElement;
            this.Edges = new();//Eventuell nach den Edges des Thestias.Vertex suchen und hier deren VisualEdges einfügen.
            this.UIElement.MouseMove += this.UIElement_MouseMove;
            this.UIElement.MouseDown += this.UIElement_MouseDown;

            //Lege die Position fest.
            int index = this.Graph.Vertices.Contains(this) ? this.Graph.Vertices.IndexOf(this) : this.Graph.Vertices.Count;
            this.UIElement.Margin = new Thickness((index % 10) * 100 + 20, index / 10 * 100 + 25, 0, 0);
        }

        public VisualVertex(Graph.Vertex vertex, VisualGraph graph, double x, double y)
        {
            //Member festlegen
            this.Graph = graph;
            this.Vertex = vertex;
            this.UIElement = new VertexEllipse(graph, this);
            this.UIElement.Margin = new Thickness(x, y, 0, 0);
            this.Edges = new();//Eventuell nach den Edges des Thestias.Vertex suchen und hier deren VisualEdges einfügen.
            this.UIElement.MouseMove += this.UIElement_MouseMove;
            this.UIElement.MouseDown += this.UIElement_MouseDown;
        }

        public VisualVertex(Graph.Vertex vertex, VisualGraph graph, FrameworkElement uiElement, double x, double y)
        {
            //Member festlegen
            this.Graph = graph;
            this.Vertex = vertex;
            this.UIElement = uiElement;
            this.UIElement.Margin = new Thickness(x, y, 0, 0);
            this.Edges = new();//Eventuell nach den Edges des Thestias.Vertex suchen und hier deren VisualEdges einfügen.
            this.UIElement.MouseMove += this.UIElement_MouseMove;
            this.UIElement.MouseDown += this.UIElement_MouseDown;
        }
        #endregion

        public void Redraw(bool positionMyself = true)
        {
            if (this.Graph.CanMoveVertices)
            {
                if (positionMyself)
                {
                    //Platziere die Ellipse
                    Thickness thickness = new Thickness();
                    Point point = Mouse.GetPosition(this.Graph.Canvas);
                    point.X -= Canvas.GetLeft(this.UIElement);
                    point.Y -= Canvas.GetTop(this.UIElement);
                    thickness.Left = point.X - this.UIElement.Width / 2;
                    thickness.Top = point.Y - this.UIElement.Height / 2;
                    thickness.Bottom = 10;
                    thickness.Right = 10;
                    this.UIElement.Margin = thickness;

                    //TODO: Label implementieren.
                    //Gebe den Namen auch noch einmal an
                    //this.RedrawName();

                    Debug.WriteLine(this.UIElement.Margin.Left + " " + this.UIElement.Margin.Top + " " + this.UIElement.Margin.Right + " " + this.UIElement.Margin.Bottom);
                    Debug.WriteLine(point.X + " " + point.Y + " " + this.UIElement.Margin.Right + " " + this.UIElement.Margin.Bottom);
                }

                foreach (VisualEdge i in this.Edges)
                {
                    //Eventuell auf eine Methode in KantenLine.xaml.cs auslagern.
                    i.Line.Set(i[0].UIElement.Margin.Left + i[0].UIElement.Width / 2, i[0].UIElement.Margin.Top, i[1].UIElement.Margin.Left + i[1].UIElement.Width / 2, i[1].UIElement.Margin.Top/*, EdgeLine.Directions.FromLeft, EdgeLine.Directions.FromLeft*/);


                    //Für den alten Code: in Pollux oder älteren Versionen davon.
                }
            }
        }

        //Events
        #region
        public void UIElement_MouseDown(object sender, MouseButtonEventArgs e)
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
                this.Graph.PropertiesGrid.OpenNode(this);
            }
        }

        public void UIElement_MouseMove(object sender, MouseEventArgs e)
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

        public void DeleteVertex_Click(object sender, RoutedEventArgs e)
        {
            //Versuche den Knoten zu entfernen.
            try
            {
                this.Graph.RemoveVertex(this);
            }
            catch { }
        }

        public void OpenProperties_Click(object sender, RoutedEventArgs e)
        {
            //Versuch die Eigenschaften des Knotens zu öffnen.
            try
            {
                this.Graph.PropertiesGrid.OpenNode(this);
            }
            catch { }
        }
        #endregion
    }
}
