namespace Thestias
{
    public partial class Graph
    {
        //Methoden zum Suchen von Edge und Vertex in einem Graphen

        public virtual Graph.Vertex SearchVertex(string name)
        {
            name = name.ToUpper();

            //Gucke, ob ein Vertex diesen Namen hat, speichere es in "knoten" ab
            foreach (Graph.Vertex i in this.Vertices)
            {
                if (i.Name == name)
                {
                    return i;
                }
            }

            //Rückgabe
            return null;
        }

        public virtual Graph.Edge SearchEdge(string name)
        {
            name = name.ToUpper();

            //Gucke, ob eine Edge diesen Namen hat, speichere es in "kante" ab
            foreach (Graph.Edge i in this.Edges)
            {
                if (i.Name == name)
                {
                    return i;
                }
            }

            //Ansonsten
            return null;
        }

        public virtual bool ContainsVertex(string name)
        {
            name = name.ToUpper();

            //durchsuche die Liste "GraphKnoten", ob schon ein Vertex mit diesem Namen existiert
            foreach (Graph.Vertex i in this.Vertices)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool ContainsVertex(Graph.Vertex vertex)
        {
            return this.Vertices.Contains(vertex);
        }

        public virtual bool ContainsEdge(string name)
        {
            name = name.ToUpper();

            //durchsuche die Liste "GraphKanten", ob schon eine Kante mit diesem Namen existiert
            foreach (Graph.Edge i in this.Edges)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool ContainsEdge(Graph.Edge kante)
        {
            return this.Edges.Contains(kante);
        }

        //Indexer
        public int this[int x, int y]
        {
            get
            {
                return this.List[x, y];
            }
            set
            {
                this.List[x, y] = value;
            }
        }

        public Vertex this[int index]
        {
            get { return this.Vertices[index]; }
        }

        public Vertex this[string name]
        {
            get { return this.SearchVertex(name); }
        }
    }
}
