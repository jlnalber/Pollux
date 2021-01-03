using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Methoden zum Suchen von Kanten und Knoten in einem Graphen

        public Graph.Knoten SucheKnoten(string name)
        {
            //suche nach dem Knoten mit dem Namen
            Graph.Knoten knoten = new Graph.Knoten(this, new List<Kanten>(), "");

            //Gucke, ob ein Knoten diesen Namen hat, speichere es in "knoten" ab
            foreach (Graph.Knoten i in this.GraphKnoten)
            {
                if (i.Name == name)
                {
                    knoten = i;
                }
            }

            //Rückgabe
            return knoten;
        }

        public Graph.Kanten SucheKanten(string name)
        {
            //suche nach dem Kanten mit dem Namen
            Graph.Kanten kante = new Graph.Kanten(this, new Knoten[2], "");

            //Gucke, ob eine Kanten diesen Namen hat, speichere es in "kante" ab
            foreach (Graph.Kanten i in this.GraphKanten)
            {
                if (i.Name == name)
                {
                    kante = i;
                }
            }

            //Rückgabe
            return kante;
        }

        public bool ContainsKnoten(string name)
        {
            //durchsuche die Liste "GraphKnoten", ob schon ein Knoten mit diesem Namen existiert
            foreach (Graph.Knoten i in this.GraphKnoten)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKnoten(Graph.Knoten knoten)
        {
            return this.GraphKnoten.Contains(knoten);
        }

        public bool ContainsKanten(string name)
        {
            //durchsuche die Liste "GraphKanten", ob schon eine Kante mit diesem Namen existiert
            foreach (Graph.Kanten i in this.GraphKanten)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKanten(Graph.Kanten kante)
        {
            return this.GraphKanten.Contains(kante);
        }

        //Indexer
        public int this[int x, int y]
        {
            get
            {
                return this.Liste[x, y];
            }
            set
            {
                this.Liste[x, y] = value;
            }
        }

        public Knoten this[int index]
        {
            get { return this.GraphKnoten[index]; }
        }

        public Knoten this[string name]
        {
            get { return SucheKnoten(name); }
        }
    }
}
