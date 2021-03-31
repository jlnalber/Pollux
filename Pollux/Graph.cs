//Datei mit Konstruktor und Members von der Klasse
using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Members dieses Graphen
        public virtual List<Knoten> GraphKnoten { get; set; }
        public virtual List<Kanten> GraphKanten { get; set; }
        public int[,] Liste { get; set; }
        public string Name { get; set; }

        //Konstruktor 1
        public Graph()
        {
            this.GraphKnoten = new List<Knoten>();
            this.GraphKanten = new List<Kanten>();
            this.Liste = new int[0, 0];
            this.Name = "GRAPH";
        }

        //Konstruktor 2
        public Graph(List<Knoten> graphKnoten, List<Kanten> graphKanten, int[,] liste, string name)
        {
            this.GraphKnoten = graphKnoten;
            this.GraphKanten = graphKanten;
            this.Liste = liste;
            this.Name = name;
        }
    }
}
