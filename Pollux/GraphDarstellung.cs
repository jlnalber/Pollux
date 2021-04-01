using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

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

        public GraphDarstellung(string[] knotenNamen, Canvas canvas) : base()
        {
            this.Name = "GRAPH";
            this.Canvas = canvas;
            this.GraphKanten = new();
            this.GraphKnoten = new();
            this.Liste = new int[knotenNamen.Length, knotenNamen.Length];
            foreach (string i in knotenNamen)
            {
                this.GraphKnoten.Add(new Knoten(this, new(), i.ToUpper(), this.Canvas));
            }
        }

        public GraphDarstellung(List<string> knotenNamen, Canvas canvas) : base()
        {
            this.Name = "GRAPH";
            this.Canvas = canvas;
            this.GraphKanten = new();
            this.GraphKnoten = new();
            this.Liste = new int[knotenNamen.Count, knotenNamen.Count];
            foreach (string i in knotenNamen)
            {
                this.GraphKnoten.Add(new Knoten(this, new(), i.ToUpper(), this.Canvas));
            }
        }

        public GraphDarstellung(string[] knotenNamen, string name, Canvas canvas) : base()
        {
            this.Name = name;
            this.Canvas = canvas;
            this.GraphKanten = new();
            this.GraphKnoten = new();
            this.Liste = new int[knotenNamen.Length, knotenNamen.Length];
            foreach (string i in knotenNamen)
            {
                this.GraphKnoten.Add(new Knoten(this, new(), i.ToUpper(), this.Canvas));
            }
        }

        public GraphDarstellung(List<string> knotenNamen, string name, Canvas canvas) : base()
        {
            this.Name = name;
            this.Canvas = canvas;
            this.GraphKanten = new();
            this.GraphKnoten = new();
            this.Liste = new int[knotenNamen.Count, knotenNamen.Count];

            Ellipse ellipse = (new KnotenEllipse()).Ellipse;

            foreach (string i in knotenNamen)
            {
                Ellipse ellipse2 = Strings.CopyEllipse(ellipse);
                ellipse2.ContextMenu = KnotenEllipse.GetContextMenu();
                this.GraphKnoten.Add(new Knoten(this, new(), i.ToUpper(), ellipse2, this.Canvas));
            }
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
