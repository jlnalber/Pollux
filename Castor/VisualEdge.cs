using Thestias;

namespace Castor
{
    public partial class VisualEdge
    {
        public VisualGraph Graph;
        public Graph.Edge Edge;
        public VisualVertex[] Vertices;
        public EdgeLine Line;

        public VisualEdge(Graph.Edge edge, VisualGraph graph, VisualVertex[] vertices)
        {
            //Lege die Members fest.
            this.Line = new();
            this.Graph = graph;
            this.Edge = edge;
            this.Vertices = vertices;
        }

        public VisualEdge(VisualGraph graph, VisualVertex[] vertices, string name)
        {
            //Lege die Members fest.
            this.Line = new();
            this.Graph = graph;
            try
            {
                this.Edge = new(graph.Graph, new Graph.Vertex[] { vertices[0].Vertex, vertices[1].Vertex }, name);
            }
            catch
            {
                this.Edge = new(graph.Graph, new Graph.Vertex[2], name);
            }
            this.Vertices = vertices;
        }

        public VisualVertex this[int index]
        {
            get
            {
                return this.Vertices[index];
            }
            set
            {
                this.Vertices[index] = value;
            }
        }
    }
}
