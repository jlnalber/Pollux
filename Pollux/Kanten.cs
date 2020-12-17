namespace Pollux.Graph
{
    public partial class Graph
    {
        //Klasse für Kanten
        public class Kanten
        {
            public Graph Parent { get; set; }
            public Knoten[] Knoten { get; set; }
            public string Name { get; set; }

            public Kanten(Graph graph, Knoten[] knoten, string name)
            {
                //falls schon eine Kante mit dem gelichen Namen existiert, werfe eine Exception
                if (graph.ConatinsKanten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //Lege die Eigenschafeten fest
                this.Parent = graph;
                this.Knoten = knoten;
                this.Name = name;
            }

            public Knoten this[int index]
            {
                get { return this.Knoten[index]; }
                set { this.Knoten[index] = value; }
            }
        }
    }
}
