using System.Collections.Generic;
using System.Windows.Controls;

namespace Pollux
{
    public partial class GraphDarstellung : Graph.Graph
    {
        //Members
        public Canvas Canvas = new Canvas();
        public new List<Knoten> GraphKnoten { get; set; }
        public new List<Kanten> GraphKanten { get; set; }

        //Konstruktoren
        #region
        public GraphDarstellung(List<GraphDarstellung.Knoten> graphKnoten, List<GraphDarstellung.Kanten> graphKanten, int[,] liste, string name) : base()
        {
            this.GraphKnoten = graphKnoten;
            this.GraphKanten = graphKanten;
            this.Liste = liste;
            this.Name = name;
            this.Canvas.MouseDown += Canvas_MouseDown;
        }

        public GraphDarstellung(List<GraphDarstellung.Knoten> graphKnoten, List<GraphDarstellung.Kanten> graphKanten, int[,] liste, string name, Canvas canvas) : base()
        {
            this.GraphKnoten = graphKnoten;
            this.GraphKanten = graphKanten;
            this.Liste = liste;
            this.Name = name;
            this.Canvas = canvas;
            this.Canvas.MouseDown += Canvas_MouseDown;
        }

        public GraphDarstellung() : base()
        {
            this.GraphKnoten = new List<Knoten>();
            this.GraphKanten = new List<Kanten>();
            this.Liste = new int[0, 0];
            this.Name = "GRAPH";
            this.Canvas.MouseDown += Canvas_MouseDown;
        }
        #endregion

        public Graph.Graph CastToGraph()
        {
            Graph.Graph graph = new Graph.Graph();
            graph = new Graph.Graph(this.GraphKnoten.ConvertAll((x) => x.CastToKnoten(graph)), this.GraphKanten.ConvertAll((x) => x.CastToKanten(graph)), this.Liste, this.Name);
            return graph;
        }
    }
}
