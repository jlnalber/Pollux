using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Thestias
{
    public partial class Graph
    {
        public static Graph TransformFileToGraph(string path, FileMode fileMode = FileMode.GRAPHML)
        {
            try
            {
                if (fileMode == FileMode.POLL)
                {
                    //Initialisiere Variablen
                    List<string> file = new List<string>();//Eine Liste für die ausgelesene Datei

                    //auslesen der Datei
                    #region
                    //lese die Datei aus
                    StreamReader reader = new StreamReader(path);
                    while (!reader.EndOfStream)
                    {
                        file.Add(reader.ReadLine());
                    }
                    reader.Close();
                    #endregion

                    //nutze die Informationen aus der Datei, um den Graphen zu vervollständigen
                    #region
                    //Suche nach dem Namen
                    int positionName = file.IndexOf("[NAME]");
                    string name = file[positionName + 1];

                    //Suche nach den Knoten
                    int positionKnoten = file.IndexOf("[NODES]");
                    HashSet<string> knotenNamen = new();
                    for (int i = positionKnoten + 1; file[i] != "[/NODES]"; ++i)
                    {
                        knotenNamen.Add(file[i]);
                    }

                    Graph graph = new(knotenNamen, name);

                    //Suche nach Kanten
                    int positionKanten = file.IndexOf("[EDGES]");
                    for (int i = positionKanten + 1; file[i] != "[/EDGES]"; ++i)
                    {
                        string[] liste = file[i].Split('\t');
                        graph.AddEdge(new Graph.Edge(graph, new Graph.Vertex[2], liste[0]), graph.SearchVertex(liste[1]), graph.SearchVertex(liste[2]));
                    }
                    #endregion

                    //Rückgabe
                    return graph;
                }
                else
                {
                    XmlDocument xmlReader = new();
                    StreamReader streamReader = new(path);
                    string xml = streamReader.ReadToEnd();
                    streamReader.Close();
                    xmlReader.LoadXml(xml);

                    XmlNodeList keys = xmlReader.GetElementsByTagName("key");

                    XmlNodeList nodes = xmlReader.GetElementsByTagName("node");

                    HashSet<string> nodeNames = new();
                    foreach (XmlElement i in nodes)
                    {
                        if (i.HasAttribute("id"))
                        {
                            nodeNames.Add(i.GetAttribute("id"));
                        }
                        else
                        {
                            int suffix = 0;
                            for (; nodeNames.Contains("NODE" + suffix); ++suffix) ;
                            nodeNames.Add("NODE" + suffix);
                        }
                    }

                    XmlElement graphXml = (XmlElement)xmlReader.GetElementsByTagName("graph")[0];
                    string name = graphXml.HasAttribute("id") ? graphXml.GetAttribute("id").ToUpper() : "GRAPH";

                    Graph graph = new(nodeNames, name);

                    XmlNodeList edges = xmlReader.GetElementsByTagName("edge");

                    foreach (XmlElement i in edges)
                    {
                        if (i.HasAttribute("id") && i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            graph.AddEdge(i.GetAttribute("id"), graph.SearchVertex(i.GetAttribute("source")), graph.SearchVertex(i.GetAttribute("target")));
                        }
                        else if (i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            int suffix = 0;
                            for (; graph.SearchEdge("EDGE" + suffix) != null; ++suffix) ;
                            graph.AddEdge("EDGE" + suffix, graph.SearchVertex(i.GetAttribute("source")), graph.SearchVertex(i.GetAttribute("target")));
                        }
                    }

                    return graph;
                }
            }
            catch
            {
                return new();
            }
        }

        public enum FileMode
        {
            POLL,
            GRAPHML
        }
    }
}
