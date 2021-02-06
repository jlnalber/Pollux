using System.Collections.Generic;
using System.Windows.Controls;

namespace Pollux.Graph
{
    public partial class GraphDarstellung : Graph
    {
        //Members
        public Canvas Canvas;
        public new List<GraphDarstellung.Knoten> GraphKnoten { get; set; }
        public new List<GraphDarstellung.Kanten> GraphKanten { get; set; }

        //Konstruktoren
        #region
        public GraphDarstellung(List<GraphDarstellung.Knoten> graphKnoten, List<GraphDarstellung.Kanten> graphKanten, int[,] liste, string name) : base()
        {
            this.GraphKnoten = graphKnoten;
            this.GraphKanten = graphKanten;
            this.Liste = liste;
            this.Name = name;
            this.Canvas = new Canvas();
        }

        public GraphDarstellung(List<GraphDarstellung.Knoten> graphKnoten, List<GraphDarstellung.Kanten> graphKanten, int[,] liste, string name, Canvas canvas) : base()
        {
            this.GraphKnoten = graphKnoten;
            this.GraphKanten = graphKanten;
            this.Liste = liste;
            this.Name = name;
            this.Canvas = canvas;
        }

        public GraphDarstellung() : base()
        {
            this.GraphKnoten = new List<Knoten>();
            this.GraphKanten = new List<Kanten>();
            this.Liste = new int[0, 0];
            this.Name = "GRAPH";
            this.Canvas = new Canvas();
        }
        #endregion
    }
}
