using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class GraphDarstellung
    {
        //Methoden zum Vergleichen von Graphen

        public bool IstIsomorph(GraphDarstellung graph)
        {
            //Vegleiche, ob die Graphen isomorph zueinander sind
            return false;
        }//Muss noch überarbeitet werden

        public bool IstTeilgraphVon(GraphDarstellung graph)
        {
            //Vegleiche, ob der Graph ein Teilgraph vom anderen ist
            return false;
        }//Muss noch überarbeitet werden

        public bool IstUnterteilungVon(GraphDarstellung graph)
        {
            //Untersuche, ob der Graph eine Unterteilung vom anderen ist
            return false;
        }//Muss noch überarbeitet werden

        public new GraphDarstellung Kopie()
        {
            //erstelle flache eine Kopie von diesem Graphen

            GraphDarstellung copy = new(new List<Knoten>(), new List<Kanten>(), new int[0, 0], this.Name, new System.Windows.Controls.Canvas());

            foreach (Knoten i in this.GraphKnoten)
            {
                Knoten knoten = new Knoten(copy, new List<Kanten>(), i.Name, copy.Canvas);
                copy.AddKnoten(knoten);
            }

            foreach (Kanten i in this.GraphKanten)
            {
                Knoten knoten0 = copy.SucheKnoten(i[0].Name);
                Knoten knoten1 = copy.SucheKnoten(i[1].Name);
                Kanten kante = new Kanten(copy, new Knoten[2] { knoten0, knoten1 }, i.Name, copy.Canvas);
                copy.AddKante(kante, knoten0, knoten1);
            }

            //Rückgabe
            return copy;
        }

        //Die Operatoren
        public static bool operator ==(GraphDarstellung graphDarstellung1, GraphDarstellung graphDarstellung2)
        {
            return graphDarstellung1.IstIsomorph(graphDarstellung2);
        }

        public static bool operator !=(GraphDarstellung graphDarstellung1, GraphDarstellung graphDarstellung2)
        {
            return !graphDarstellung1.IstIsomorph(graphDarstellung2);
        }
    }
}
