using System.Windows;
using System.Windows.Controls;
using Thestias;

namespace Castor
{
    public partial class VisualGraph
    {
        //Methoden zum hinzufügen, entfernen von Vertices/Edges
        //Methoden zum Hinzufügen von Vertices
        #region
        public VisualVertex AddVertex(VisualVertex visualVertex)
        {
            //Visuelle Darstellung
            Canvas.SetZIndex(visualVertex.UIElement, 100);
            this.AddUIElementToCanvas(visualVertex.UIElement);

            //Knoten hinzufügen
            this.Vertices.Add(visualVertex);
            visualVertex.Graph = this;

            //Vertex zum Thestias.Graph hinzufügen
            this.Graph.IsEditable = true;
            this.Graph.AddVertex(visualVertex.Vertex);
            this.Graph.IsEditable = false;

            //Events hinzufügen
            this.Canvas.MouseMove += visualVertex.UIElement_MouseMove;

            //Rückgabe
            return visualVertex;
        }

        public VisualVertex AddVertex(string name, FrameworkElement uiElement)
        {
            return this.AddVertex(new VisualVertex(new Graph.Vertex(this.Graph, name), this, uiElement));
        }

        public VisualVertex AddVertex(FrameworkElement uiElement)
        {
            return this.AddVertex(this.GetValidVertexName(), uiElement);
        }

        public VisualVertex AddVertex(Graph.Vertex vertex)
        {
            return this.AddVertex(new VisualVertex(vertex, this));
        }

        public VisualVertex AddVertex(string name)
        {
            return this.AddVertex(new Graph.Vertex(this.Graph, name));
        }

        public VisualVertex AddVertex(Graph.Vertex vertex, double x, double y)
        {
            //Erstelle den VisualVertex
            VisualVertex visualVertex = new VisualVertex(vertex, this);
            visualVertex.UIElement.Margin = new Thickness(x, y, 0, 0);

            //Rückgabe
            return this.AddVertex(visualVertex);
        }

        public VisualVertex AddVertex(string name, double x, double y)
        {
            //Erstelle den VisualVertex
            VisualVertex visualVertex = new VisualVertex(new Graph.Vertex(this.Graph, name), this);
            visualVertex.UIElement.Margin = new Thickness(x, y, 0, 0);

            //Rückgabe
            return this.AddVertex(visualVertex);
        }

        public VisualVertex AddVertex(double x, double y)
        {
            return this.AddVertex(this.GetValidVertexName(), x, y);
        }

        public VisualVertex AddVertex()
        {
            return this.AddVertex(this.GetValidVertexName());
        }
        #endregion

        //Methoden zum Hinzufügen von Edges
        #region
        public VisualEdge AddEdge(VisualEdge visualEdge, VisualVertex endpoint1, VisualVertex endpoint2)
        {
            //Visuelle Darstellung
            Canvas.SetZIndex(visualEdge.Line, 0);
            this.AddUIElementToCanvas(visualEdge.Line);

            //Füge die Edge zum Graph hinzu.
            this.Edges.Add(visualEdge);
            visualEdge.Graph = this;
            visualEdge.Vertices = new VisualVertex[2] { endpoint1, endpoint2 };

            //Edge zu den Vertices hinzufügen.
            endpoint1.Edges.Add(visualEdge);
            endpoint2.Edges.Add(visualEdge);

            //Füge die Edge zum Thestias.Graph hinzu.
            this.Graph.IsEditable = true;
            this.Graph.AddEdge(visualEdge.Edge, endpoint1.Vertex, endpoint2.Vertex);
            this.Graph.IsEditable = false;

            //Die Edge im Canvas malen (eventuell Überarbeitung, da vielleicht nur einmal nötig).
            endpoint1.Redraw(false);
            endpoint2.Redraw(false);

            //Male die Kante.
            visualEdge.Line.Redraw();

            //Rückgabe
            return visualEdge;
        }

        public VisualEdge AddEdge(Graph.Edge edge, VisualVertex endpoint1, VisualVertex endpoint2)
        {
            return this.AddEdge(new VisualEdge(edge, this, new VisualVertex[2]), endpoint1, endpoint2);
        }

        public VisualEdge AddEdge(string name, VisualVertex endpoint1, VisualVertex endpoint2)
        {
            return this.AddEdge(new Graph.Edge(this.Graph, new Graph.Vertex[2] { endpoint1.Vertex, endpoint2.Vertex }, name), endpoint1, endpoint2);
        }

        public VisualEdge AddEdge(string name, string endpoint1, string endpoint2)
        {
            if (endpoint1 == endpoint2)
            {
                //Falls es eine Schlinge ist, suche nur einmal nach dem Knoten.
                VisualVertex vertex = this.SearchVertex(endpoint1);
                return this.AddEdge(name, vertex, vertex);
            }
            else
            {
                //Falls es eine normale Kante ist, einfach Rückgabe.
                return this.AddEdge(name, this.SearchVertex(endpoint1), this.SearchVertex(endpoint2));
            }
        }

        public VisualEdge AddEdge(VisualVertex endpoint1, VisualVertex endpoint2)
        {
            return this.AddEdge(this.GetValidEdgeName(), endpoint1, endpoint2);
        }

        public VisualEdge AddEdge(string endpoint1, string endpoint2)
        {
            return this.AddEdge(this.GetValidEdgeName(), endpoint1, endpoint2);
        }
        #endregion

        //Methoden zum Entfernen von Vertices und Edges
        #region
        public VisualVertex RemoveVertex(VisualVertex vertex)
        {
            //Entferne die Edges des Vertex.
            while (vertex.Edges.Count != 0)
            {
                this.RemoveEdge(vertex.Edges[0]);
            }
            vertex.Graph = null;
            vertex.Edges.Clear();
            this.Vertices.Remove(vertex);

            //Entferne den Vertex aus dem Canvas.
            try
            {
                //TODO: Label implementieren
                this.Canvas.Children.Remove(vertex.UIElement);

                this.Canvas.MouseMove -= vertex.UIElement_MouseMove;
            }
            catch { }

            //Entferne den Vertex aus dem Thestias.Graph.
            this.Graph.IsEditable = true;
            this.Graph.RemoveVertex(vertex.Vertex);
            this.Graph.IsEditable = false;

            //Rückgabe
            return vertex;
        }

        public VisualVertex RemoveVertex(string name)
        {
            return this.RemoveVertex(this.SearchVertex(name));
        }

        public VisualEdge RemoveEdge(VisualEdge edge)
        {
            //Entferne die Edge aus ihren Vertices.
            edge.Vertices[0].Edges.Remove(edge);
            edge.Vertices[1].Edges.Remove(edge);

            //Führe Änderungen an der Edge aus.
            edge.Graph = null;
            this.Edges.Remove(edge);
            edge.Vertices = new VisualVertex[2];

            //Entferne die Edge aus dem Canvas.
            try
            {
                this.Canvas.Children.Remove(edge.Line);
            }
            catch { }

            //Entferne die Edge aus dem Thestias.Graph.
            this.Graph.IsEditable = true;
            this.Graph.RemoveEdge(edge.Edge);
            this.Graph.IsEditable = false;

            //Rückgabe
            return edge;
        }

        public VisualEdge RemoveEdge(string name)
        {
            return this.RemoveEdge(this.SearchEdge(name));
        }
        #endregion

        public void AddUIElementToCanvas(FrameworkElement uiElement)
        {
            if (this.Canvas.Children.Count != 0)
            {
                Canvas.SetTop(uiElement, Canvas.GetTop(this.Canvas.Children[0]));
                Canvas.SetLeft(uiElement, Canvas.GetLeft(this.Canvas.Children[0]));
            }
            else
            {
                Canvas.SetTop(uiElement, 0);
                Canvas.SetLeft(uiElement, 0);
            }
            this.Canvas.Children.Add(uiElement);
        }
    }
}
