using System.Collections.Generic;
using System.Linq;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Klasse für Knoten
        public class Knoten
        {
            public int Grad
            {
                get { return this.Kanten.Count; }
            }
            public Graph Parent { get; set; }
            public List<Kanten> Kanten { get; set; }
            public string Name { get; set; }

            public Knoten(Graph graph, List<Kanten> kanten, string name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten;
                this.Name = name;
            }

            public Knoten(Graph graph, string name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = new();
                this.Name = name;
            }

            public Kanten this[int index]
            {
                get { return this.Kanten[index]; }
                set { this.Kanten[index] = value; }
            }

            //Eigenschaften der Ecke
            #region
            public bool IstIsolierteEcke
            {
                get
                {
                    return this.Grad == 0;
                }
            }

            public List<Graph.Knoten> BenachbarteKnoten
            {
                get
                {
                    //Liste, in die alle benachbarten Knoten kommen
                    List<Graph.Knoten> liste = new List<Knoten>();

                    foreach (Graph.Kanten i in this.Kanten)
                    {
                        //gehe jede Kante durch und gucke, welcher Knoten auf der anderen Seite liegt, füge diesen, falls nicht schon vorhanden, zur Liste "liste" hinzu
                        Knoten knoten = (i[0].Name == this.Name) ? (Graph.Knoten)i[1] : (Graph.Knoten)i[0];
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

            //Methoden zum Vergleichen von Knoten
            #region
            public bool IstBenachbart(Knoten knoten)
            {
                //Überprüfe, ob der Knoten zu den benachbarten Knoten dieses Knoten gehört
                foreach (Kanten i in this.Kanten)
                {
                    if (i.Knoten[0] == knoten || i.Knoten[1] == knoten)
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
