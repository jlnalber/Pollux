namespace Castor
{
    public partial class VisualGraph
    {
        //Methoden zum Suchen von Kanten und Knoten in einem Graphen

        public VisualVertex SearchVertex(string name)
        {
            name = name.ToUpper();

            //Gucke, ob ein Knoten diesen Namen hat, Rückgabe.
            foreach (VisualVertex i in this.Vertices)
            {
                if (i.Vertex.Name == name)
                {
                    return i;
                }
            }

            //Ansonsten
            return null;
        }

        public virtual VisualEdge SearchEdge(string name)
        {
            name = name.ToUpper();

            //Gucke, ob eine Kanten diesen Namen hat, Rückgabe.
            foreach (VisualEdge i in this.Edges)
            {
                if (i.Edge.Name == name)
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

            //durchsuche die Liste "GraphKnoten", ob schon ein Knoten mit diesem Namen existiert
            foreach (VisualVertex i in this.Vertices)
            {
                if (i.Vertex.Name == name)
                {
                    return true;
                }
            }

            //Ansonsten
            return false;
        }

        public virtual bool ContainsVertex(VisualVertex knoten)
        {
            return this.Vertices.Contains(knoten);
        }

        public virtual bool ContainsEdge(string name)
        {
            name = name.ToUpper();

            //durchsuche die Liste "GraphKanten", ob schon eine Kante mit diesem Namen existiert
            foreach (VisualEdge i in this.Edges)
            {
                if (i.Edge.Name == name)
                {
                    return true;
                }
            }

            //Ansonsten
            return false;
        }

        public virtual bool ContainsEdge(VisualEdge kante)
        {
            return this.Edges.Contains(kante);
        }

        //Indexer
        public int this[int x, int y]
        {
            get
            {
                return this.Graph.List[x, y];
            }
            set
            {
                this.Graph.List[x, y] = value;
            }
        }

        public VisualVertex this[int index]
        {
            get { return this.Vertices[index]; }
        }

        public VisualVertex this[string name]
        {
            get { return this.SearchVertex(name); }
        }

        public string GetValidVertexName()
        {
            int number = this.Vertices.Count;
            for (; this.SearchVertex("NODE" + number) != null; number++) ;
            return "NODE" + number;
        }

        public string GetValidEdgeName()
        {
            int number = this.Edges.Count;
            for (; this.SearchEdge("EDGE" + number) != null; number++) ;
            return "EDGE" + number;
        }
    }
}
