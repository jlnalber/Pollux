namespace Thestias
{
    public partial class Graph
    {
        //Klasse für Edge
        public class Edge
        {
            public Graph Parent { get; set; }
            public Vertex[] Vertices { get; set; }

            private string name;
            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    if (this.Parent.ContainsVertex(value) || this.Parent.ContainsEdge(value) || this.Parent.Name == value)
                    {
                        throw new GraphExceptions.NameAlreadyExistsException();
                    }
                    this.name = value;
                    this.Parent.Changed(this.Parent, new(this, ChangedEventArgs.ChangedElements.Edge, ChangedEventArgs.ChangedTypes.Renaming));
                }
            }

            public Edge(Graph graph, Vertex[] vertices, string name)
            {
                //falls schon eine Kante mit dem gelichen Namen existiert, werfe eine Exception
                if (graph.ContainsEdge(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //Lege die Eigenschafeten fest
                this.Parent = graph;
                this.Vertices = vertices;
                this.Name = name;
            }

            public Vertex this[int index]
            {
                get { return this.Vertices[index]; }
                set { this.Vertices[index] = value; }
            }
        }
    }
}
