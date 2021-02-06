using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Methoden zum Vergleichen von Graphen

        public bool IstIsomorph(Graph graph)
        {
            //Vegleiche, ob die Graphen isomorph zueinander sind
            return false;
        }//Muss noch überarbeitet werden

        public bool IstTeilgraphVon(Graph graph)
        {
            //Vegleiche, ob der Graph ein Teilgraph vom anderen ist
            return false;
        }//Muss noch überarbeitet werden

        public bool IstUnterteilungVon(Graph graph)
        {
            //Untersuche, ob der Graph eine Unterteilung vom anderen ist
            return false;
        }//Muss noch überarbeitet werden

        public Graph Kopie()
        {
            //erstelle flache eine Kopie von diesem Graphen

            Graph copy = new Graph();

            foreach (Graph.Knoten i in this.GraphKnoten)
            {
                Graph.Knoten knoten = new Graph.Knoten(copy, new List<Kanten>(), i.Name);
                copy.AddKnoten(knoten);
            }

            foreach (Graph.Kanten i in this.GraphKanten)
            {
                Graph.Kanten kante = new Graph.Kanten(copy, new Knoten[2], i.Name);
                copy.AddKante(kante, copy.SucheKnoten(i[0].Name), copy.SucheKnoten(i[1].Name));
            }

            //Rückgabe
            return copy;
        }
    }
}
