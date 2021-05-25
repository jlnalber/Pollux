//Datei mit Konstruktor und Members von der Klasse
using System.Collections.Generic;

namespace Thestias
{
    public partial class Graph
    {
        //Members dieses Graphen
        public virtual List<Vertex> Vertices { get; set; }
        public virtual List<Edge> Edges { get; set; }
        public int[,] List { get; set; }
        public bool IsEditable = true;

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.ContainsVertex(value) || this.ContainsEdge(value))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }
                this.name = value;
                this.Changed(this, new(this, ChangedEventArgs.ChangedElements.Graph, ChangedEventArgs.ChangedTypes.Renaming));
            }
        }

        //Konstruktor 1
        public Graph()
        {
            this.Vertices = new List<Vertex>();
            this.Edges = new List<Edge>();
            this.List = new int[0, 0];
            this.Name = "GRAPH";
        }

        //Konstruktor 2
        public Graph(List<Vertex> vertices, List<Edge> edges, int[,] list, string name)
        {
            this.Vertices = vertices;
            this.Edges = edges;
            this.List = list;
            this.Name = name;
        }

        public Graph(string[] verticesNames)
        {
            this.Name = "GRAPH";
            this.Edges = new();
            this.Vertices = new();
            this.List = new int[verticesNames.Length, verticesNames.Length];
            foreach (string i in verticesNames)
            {
                this.Vertices.Add(new Vertex(this, new(), i.ToUpper()));
            }
        }

        public Graph(HashSet<string> verticesNames)
        {
            this.Name = "GRAPH";
            this.Edges = new();
            this.Vertices = new();
            this.List = new int[verticesNames.Count, verticesNames.Count];
            foreach (string i in verticesNames)
            {
                this.Vertices.Add(new Vertex(this, new(), i.ToUpper()));
            }
        }

        public Graph(string[] verticesNames, string name)
        {
            this.Name = name;
            this.Edges = new();
            this.Vertices = new();
            this.List = new int[verticesNames.Length, verticesNames.Length];
            foreach (string i in verticesNames)
            {
                this.Vertices.Add(new Vertex(this, new(), i.ToUpper()));
            }
        }

        public Graph(HashSet<string> verticesNames, string name)
        {
            this.Name = name;
            this.Edges = new();
            this.Vertices = new();
            this.List = new int[verticesNames.Count, verticesNames.Count];
            foreach (string i in verticesNames)
            {
                this.Vertices.Add(new Vertex(this, new(), i.ToUpper()));
            }
        }
    }
}
