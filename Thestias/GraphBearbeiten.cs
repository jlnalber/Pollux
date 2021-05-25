using System.Collections.Generic;

namespace Thestias
{
    public partial class Graph
    {
        //Methoden zum hinzufügen, entfernen von Vertex/Edge
        //Methoden zum Hinzufügen von Vertex
        #region
        public virtual Graph.Vertex AddVertex(Vertex vertex)
        {
            if (!this.IsEditable)
            {
                throw new GraphExceptions.GraphIsNotEditableException();
            }

            //Erstelle den Vertex
            //Lege den Namen fest
            vertex.Name = vertex.Name.ToUpper();

            //Füge den Vertex dem Graphen hinzu
            this.Vertices.Add(vertex);
            vertex.Parent = this;

            //Füge den Vertex der Liste "Liste" des Graphen hinzu
            int[,] copy = new int[this.List.GetLength(0) + 1, this.List.GetLength(1) + 1];
            for (int i = 0; i < this.List.GetLength(0); ++i)
            {
                for (int f = 0; f < this.List.GetLength(1); ++f)
                {
                    copy[i, f] = this.List[i, f];
                }
            }
            this.List = copy;

            //Event triggern.
            this.Changed(this, new(vertex, ChangedEventArgs.ChangedElements.Vertex, ChangedEventArgs.ChangedTypes.Addition));

            //Rückgabe
            return vertex;
        }

        public virtual Graph.Vertex AddVertex(string name)
        {
            Vertex knote = new Vertex(this, new List<Edge>(), name.ToUpper());
            return this.AddVertex(knote);
        }

        public virtual Graph.Vertex AddVertex()
        {
            return this.AddVertex("NODE" + (this.Vertices.Count + 1).ToString());
        }
        #endregion

        //Methode zum Hinzufügen von Edge
        #region
        public virtual Graph.Edge AddEdge(Edge edge, Vertex endpoint1, Vertex endpoint2)
        {
            if (!this.IsEditable)
            {
                throw new GraphExceptions.GraphIsNotEditableException();
            }

            //Erstelle die Kante
            //Füge den Namen hinzu
            edge.Name = edge.Name.ToUpper();

            //Füge dem Graphen die Kante hinzu
            edge.Parent = this;
            this.Edges.Add(edge);

            //Setze die Vertex zu den Start und Endknoten der Kante
            edge.Vertices[0] = endpoint1;
            edge.Vertices[1] = endpoint2;

            //Füge die Kante den angegebenen Vertex hinzu
            endpoint1.Edges.Add(edge);
            endpoint2.Edges.Add(edge);

            //Erhöhe die Zahl an der Matrix
            ++this.List[this.Vertices.IndexOf(endpoint1), this.Vertices.IndexOf(endpoint2)];
            ++this.List[this.Vertices.IndexOf(endpoint2), this.Vertices.IndexOf(endpoint1)];

            //Event triggern.
            this.Changed(this, new(edge, ChangedEventArgs.ChangedElements.Edge, ChangedEventArgs.ChangedTypes.Addition));

            //Rückgabe
            return edge;
        }

        public virtual Graph.Edge AddEdge(string name, Vertex endpoint1, Vertex endpoint2)
        {
            Edge kante = new Edge(this, new Vertex[2], name.ToUpper());
            return this.AddEdge(kante, endpoint1, endpoint2);
        }

        public virtual Graph.Edge AddEdge(Vertex endpoint1, Vertex endpoint2)
        {
            return this.AddEdge("EDGE" + (this.Edges.Count + 1).ToString(), endpoint1, endpoint2);
        }
        #endregion

        //Methoden zum Entfernen von Vertex und Edge
        #region
        public virtual Graph.Vertex RemoveVertex(Graph.Vertex vertex)
        {
            if (!this.IsEditable)
            {
                throw new GraphExceptions.GraphIsNotEditableException();
            }

            //Entferne alle zugehörigen Edge aus dem Graphen
            while (vertex.Edges.Count != 0)
            {
                this.RemoveEdge(vertex.Edges[0]);
            }
            vertex.Parent = null;
            vertex.Edges.Clear();

            //Bearbeite die Liste "List"
            int position = this.Vertices.IndexOf(vertex);
            int[,] copy = new int[this.List.GetLength(0) - 1, this.List.GetLength(1) - 1];
            for (int i = 0; i < this.List.GetLength(0); ++i)
            {
                for (int f = 0; f < this.List.GetLength(1); ++f)
                {
                    if (i != position && f != position)
                    {
                        copy[(i < position) ? i : i - 1, (f < position) ? f : f - 1] = this.List[i, f];
                    }
                }
            }
            this.List = copy;

            //Entferne den Vertex "knoten" aus der Liste "GraphKnoten"
            this.Vertices.Remove(vertex);

            //Event triggern.
            this.Changed(this, new(vertex, ChangedEventArgs.ChangedElements.Vertex, ChangedEventArgs.ChangedTypes.Removal));

            //Rückgabe
            return vertex;
        }

        public virtual Graph.Edge RemoveEdge(Graph.Edge edge)
        {
            if (!this.IsEditable)
            {
                throw new GraphExceptions.GraphIsNotEditableException();
            }

            //Entferne den Graph aus der Kante
            edge.Parent = null;

            //Bearbeite die Liste "Liste"
            --this.List[this.Vertices.IndexOf(edge.Vertices[0]), this.Vertices.IndexOf(edge.Vertices[1])];
            --this.List[this.Vertices.IndexOf(edge.Vertices[1]), this.Vertices.IndexOf(edge.Vertices[0])];

            //Entferne die Kante aus ihren Vertex
            edge.Vertices[0].Edges.Remove(edge);
            edge.Vertices[1].Edges.Remove(edge);

            //Lösche die Vertex aus der Kante
            edge[0] = null;
            edge[1] = null;

            //Entferne die Kante aus der Liste "GraphKanten" im Graphen.
            this.Edges.Remove(edge);

            //Event triggern.
            this.Changed(this, new(edge, ChangedEventArgs.ChangedElements.Edge, ChangedEventArgs.ChangedTypes.Removal));

            //Rückgabe
            return edge;
        }
        #endregion
    }
}
