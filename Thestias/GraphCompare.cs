using System;
using System.Collections.Generic;

namespace Thestias
{
    public partial class Graph
    {
        //Methoden zum Vergleichen von Graphen

        public bool IstIsomorph(Graph graph)
        {
            //Vegleiche, ob die Graphen isomorph zueinander sind
            throw new NotImplementedException();
            return false;
        }//Muss noch überarbeitet werden

        public bool IstTeilgraphVon(Graph graph)
        {
            //Vegleiche, ob der Graph ein Teilgraph vom anderen ist
            throw new NotImplementedException();
            return false;
        }//Muss noch überarbeitet werden

        public bool IstUnterteilungVon(Graph graph)
        {
            //Untersuche, ob der Graph eine Unterteilung vom anderen ist
            throw new NotImplementedException();
            return false;
        }//Muss noch überarbeitet werden

        public Graph Kopie()
        {
            //erstelle flache eine Kopie von diesem Graphen

            Graph copy = new Graph();

            foreach (Graph.Vertex i in this.Vertices)
            {
                Graph.Vertex knoten = new Graph.Vertex(copy, new List<Edge>(), i.Name);
                copy.AddVertex(knoten);
            }

            foreach (Graph.Edge i in this.Edges)
            {
                Graph.Edge kante = new Graph.Edge(copy, new Vertex[2], i.Name);
                copy.AddEdge(kante, copy.SearchVertex(i[0].Name), copy.SearchVertex(i[1].Name));
            }

            //Rückgabe
            return copy;
        }

        public static bool operator ==(Graph graph1, Graph graph2)
        {
            return graph1.IstIsomorph(graph2);
        }

        public static bool operator !=(Graph graph1, Graph graph2)
        {
            return !graph1.IstIsomorph(graph2);
        }

        #region
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        #endregion
    }
}
