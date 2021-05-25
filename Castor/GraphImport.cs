using System.Collections.Generic;
using System.IO;
using System.Xml;
using Thestias;

namespace Castor
{
    public partial class VisualGraph
    {
        //TODO: Import implementieren
        public static VisualGraph TransformFileToVisualGraph(string path, Graph.FileMode fileMode = Graph.FileMode.GRAPHML)
        {
            try
            {
                if (fileMode == Graph.FileMode.POLL)
                {
                    //Initialisiere Variablen
                    List<string> file = new List<string>();//Eine Liste für die ausgelesene Datei
                    VisualGraph graph = new VisualGraph();
                    graph.AutoReloadProperties = false;

                    //auslesen der Datei
                    //lese die Datei aus
                    StreamReader reader = new StreamReader(path);
                    while (!reader.EndOfStream)
                    {
                        file.Add(reader.ReadLine());
                    }
                    reader.Close();

                    //nutze die Informationen aus der Datei, um den Graphen zu vervollständigen
                    #region
                    //Suche nach dem Namen
                    int positionName = file.IndexOf("[NAME]");
                    string name = file[positionName + 1];

                    //Suche nach den Knoten
                    int positionKnoten = file.IndexOf("[NODES]");
                    for (int i = positionKnoten + 1; file[i] != "[/NODES]"; ++i)
                    {
                        graph.AddVertex(file[i]);
                    }

                    //Suche nach Kanten
                    int positionKanten = file.IndexOf("[EDGES]");
                    for (int i = positionKanten + 1; file[i] != "[/EDGES]"; ++i)
                    {
                        string[] liste = file[i].Split('\t');
                        graph.AddEdge(liste[0], liste[1], liste[2]);
                    }
                    #endregion

                    //Mache AutoReloadProperties wieder an.
                    graph.AutoReloadProperties = true;

                    //Rückgabe
                    return graph;
                }
                else
                {
                    //TODO: implementieren mit Aussehen (nach Standard).
                    /*XmlDocument xmlReader = new();
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

                    VisualGraph graph = new(nodeNames, name, canvas);

                    foreach (XmlElement i in nodes)
                    {
                        foreach (XmlElement n in i.ChildNodes)
                        {
                            if (n.Name == "data" && n.HasAttribute("key") && i.HasAttribute("id"))
                            {
                                string keyID = n.GetAttribute("key");

                                XmlElement key = (from XmlElement k in keys where k.GetAttribute("id") == keyID select k).First();

                                if (key.HasAttribute("for") && key.HasAttribute("attr.name") && key.GetAttribute("for") == "node")
                                {
                                    switch (key.GetAttribute("attr.name"))
                                    {
                                        case "positionx":
                                            VisualGraph.Knoten knoten = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            Thickness margin = knoten.Ellipse.Margin;
                                            margin.Left = double.Parse(n.InnerText);
                                            knoten.Ellipse.Margin = margin;
                                            knoten.RedrawName(); break;
                                        case "positiony":
                                            VisualGraph.Knoten knoten0 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            Thickness margin0 = knoten0.Ellipse.Margin;
                                            margin0.Top = double.Parse(n.InnerText);
                                            knoten0.Ellipse.Margin = margin0;
                                            knoten0.RedrawName(); break;
                                        case "strokeThickness":
                                            VisualGraph.Knoten knoten1 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten1.Ellipse.StrokeThickness = double.Parse(n.InnerText);
                                            knoten1.RedrawName(); break;
                                        case "height":
                                            VisualGraph.Knoten knoten2 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten2.Ellipse.Height = double.Parse(n.InnerText);
                                            knoten2.RedrawName(); break;
                                        case "width":
                                            VisualGraph.Knoten knoten3 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten3.Ellipse.Width = double.Parse(n.InnerText);
                                            knoten3.RedrawName(); break;
                                        case "stroke":
                                            VisualGraph.Knoten knoten4 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten4.Ellipse.Stroke = Stuff.StringToColor(n.InnerText);
                                            knoten4.RedrawName(); break;
                                        case "fill":
                                            VisualGraph.Knoten knoten5 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten5.Ellipse.Fill = Stuff.StringToColor(n.InnerText);
                                            knoten5.RedrawName(); break;
                                    }
                                }
                            }
                        }
                    }

                    XmlNodeList edges = xmlReader.GetElementsByTagName("edge");

                    foreach (XmlElement i in edges)
                    {
                        if (i.HasAttribute("id") && i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            graph.AddKante(i.GetAttribute("id"), graph.SucheKnoten(i.GetAttribute("source")), graph.SucheKnoten(i.GetAttribute("target")));
                        }
                        else if (i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            int suffix = 0;
                            for (; graph.SucheKanten("EDGE" + suffix) != null; ++suffix) ;
                            graph.AddKante("EDGE" + suffix, graph.SucheKnoten(i.GetAttribute("source")), graph.SucheKnoten(i.GetAttribute("target")));
                        }

                        foreach (XmlElement n in i.ChildNodes)
                        {
                            if (n.Name == "data" && n.HasAttribute("key") && i.HasAttribute("id"))
                            {
                                string keyID = n.GetAttribute("key");

                                XmlElement key = (from XmlElement k in keys where k.GetAttribute("id") == keyID select k).First();

                                if (key.HasAttribute("for") && key.HasAttribute("attr.name") && key.GetAttribute("for") == "edge")
                                {
                                    switch (key.GetAttribute("attr.name"))
                                    {
                                        case "strokeThickness":
                                            VisualGraph.Kanten kanten = graph.SucheKanten(i.GetAttribute("id").ToUpper());
                                            if (kanten.Line is Line line0)
                                            {
                                                line0.StrokeThickness = double.Parse(n.InnerText);
                                            }
                                            else if (kanten.Line is Ellipse ellipse0)
                                            {
                                                ellipse0.StrokeThickness = double.Parse(n.InnerText);
                                            }
                                            break;
                                        case "stroke":
                                            VisualGraph.Kanten kanten0 = graph.SucheKanten(i.GetAttribute("id").ToUpper());
                                            if (kanten0.Line is Line line1)
                                            {
                                                line1.Stroke = Stuff.StringToColor(n.InnerText);
                                            }
                                            else if (kanten0.Line is Ellipse ellipse1)
                                            {
                                                ellipse1.Stroke = Stuff.StringToColor(n.InnerText);
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    return graph;*/

                    XmlDocument xmlReader = new();
                    StreamReader streamReader = new(path);
                    string xml = streamReader.ReadToEnd();
                    streamReader.Close();
                    xmlReader.LoadXml(xml);

                    XmlElement graphXml = (XmlElement)xmlReader.GetElementsByTagName("graph")[0];
                    string name = graphXml.HasAttribute("id") ? graphXml.GetAttribute("id").ToUpper() : "GRAPH";

                    VisualGraph graph = new(new(new(), new(), new int[0, 0], name));
                    graph.AutoReloadProperties = false;

                    XmlNodeList keys = xmlReader.GetElementsByTagName("key");

                    XmlNodeList nodes = xmlReader.GetElementsByTagName("node");

                    foreach (XmlElement i in nodes)
                    {
                        if (i.HasAttribute("id"))
                        {
                            graph.AddVertex(i.GetAttribute("id"));
                        }
                        else
                        {
                            graph.AddVertex();
                        }
                    }

                    XmlNodeList edges = xmlReader.GetElementsByTagName("edge");

                    foreach (XmlElement i in edges)
                    {
                        if (i.HasAttribute("id") && i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            graph.AddEdge(i.GetAttribute("id"), i.GetAttribute("source"), i.GetAttribute("target"));
                        }
                        else if (i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            graph.AddEdge(i.GetAttribute("source"), i.GetAttribute("target"));
                        }
                    }

                    //Mache AutoReloadProperties wieder an.
                    graph.AutoReloadProperties = true;

                    return graph;
                }
            }
            catch
            {
                return new VisualGraph();
            }
        }
    }
}
