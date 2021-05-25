using System.Collections.Generic;
using System.Linq;

namespace Thestias
{
    public partial class Graph
    {
        //Klasse für Vertex
        public class Vertex
        {
            public int Degree
            {
                get { return this.Edges.Count; }
            }
            public Graph Parent { get; set; }
            public List<Edge> Edges { get; set; }

            private string name;
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    if (this.Parent.ContainsVertex(value) || this.Parent.ContainsEdge(value) || this.Parent.Name == value)
                    {
                        throw new GraphExceptions.NameAlreadyExistsException();
                    }
                    this.name = value;
                    this.Parent.Changed(this.Parent, new(this, ChangedEventArgs.ChangedElements.Vertex, ChangedEventArgs.ChangedTypes.Renaming));
                }
            }

            public Vertex(Graph graph, List<Edge> edges, string name)
            {
                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Edges = edges;
                this.Name = name;
            }

            public Vertex(Graph graph, string name)
            {
                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Edges = new();
                this.Name = name;
            }

            public Edge this[int index]
            {
                get { return this.Edges[index]; }
                set { this.Edges[index] = value; }
            }

            //Eigenschaften der Ecke
            #region
            public bool IsIsolated
            {
                get
                {
                    return this.Degree == 0;
                }
            }

            public List<Graph.Vertex> AdjacentVertices
            {
                get
                {
                    //Liste, in die alle benachbarten Vertex kommen
                    List<Graph.Vertex> liste = new List<Vertex>();

                    foreach (Graph.Edge i in this.Edges)
                    {
                        //gehe jede Kante durch und gucke, welcher Vertex auf der anderen Seite liegt, füge diesen, falls nicht schon vorhanden, zur Liste "liste" hinzu
                        Vertex knoten = (i[0].Name == this.Name) ? (Graph.Vertex)i[1] : (Graph.Vertex)i[0];
                        if ((from n in liste where n.Name == i.Name select n).Count() == 0)
                        {
                            liste.Add(knoten);
                        }
                    }

                    //Rückgabe
                    return liste;
                }
            }
            #endregion

            //Methoden zum Vergleichen von Vertex
            #region
            public bool IsAdjacentTo(Vertex knoten)
            {
                //Überprüfe, ob der Vertex zu den benachbarten Vertex dieses Vertex gehört
                foreach (Edge i in this.Edges)
                {
                    if (i.Vertices[0] == knoten || i.Vertices[1] == knoten)
                    {
                        return true;
                    }
                }

                return false;
            }
            #endregion
        }
    }
}
