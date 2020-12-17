using System.Collections.Generic;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public Graph.Graph graph;

        public List<GraphDarstellung.KantenDarstellung> visuelleKanten = new List<GraphDarstellung.KantenDarstellung>();
        public List<KnotenDarstellung> visuelleKnoten = new List<KnotenDarstellung>();
    }
}
