using System.IO;
using System.Text;
using System.Xml;

namespace Thestias
{
    public partial class Graph
    {
        public static string TransformGraphToString(Graph graph, FileMode fileMode = FileMode.GRAPHML)
        {
            if (fileMode == FileMode.POLL)
            {
                //Intialisiere die Variable "file"
                string file = "";

                //speichere den Namen vom Graphen
                file += "[NAME]\n";
                file += graph.Name + "\n";
                file += "[/NAME]\n";

                //speichere die Knoten ab
                file += "[NODES]\n";
                foreach (Graph.Vertex i in graph.Vertices)
                {
                    file += i.Name + "\n";
                }
                file += "[/NODES]\n";

                //speichere die Kanten ab
                file += "[EDGES]\n";
                foreach (Graph.Edge i in graph.Edges)
                {
                    string str = "";
                    foreach (Graph.Vertex f in i.Vertices)
                    {
                        str += "\t" + f.Name;
                    }
                    file += i.Name + str + "\n";
                }
                file += "[/EDGES]\n";

                //Rückgabe
                return file;
            }
            else
            {
                StringWriter stringBuilder = new();
                XmlWriterSettings xmlWriterSetting = new();
                xmlWriterSetting.Encoding = Encoding.UTF8;
                xmlWriterSetting.Indent = true;
                xmlWriterSetting.NewLineChars = "\n";
                xmlWriterSetting.OmitXmlDeclaration = false;
                XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSetting);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("graphml", "http://graphml.graphdrawing.org/xmlns");
                xmlWriter.WriteAttributeString("xmlns", "xsi", "", "http://www.w3.org/2001/XMLSchema-instance");
                xmlWriter.WriteAttributeString("xsi", "schemaLocation", null, "http://graphml.graphdrawing.org/xmlns http://graphml.graphdrawing.org/xmlns/1.1/graphml.xsd");

                xmlWriter.WriteStartElement("graph");
                xmlWriter.WriteAttributeString("id", graph.Name);
                xmlWriter.WriteAttributeString("edgedefault", "undirected");

                foreach (Graph.Vertex i in graph.Vertices)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", i.Name);
                    xmlWriter.WriteEndElement();
                }

                foreach (Graph.Edge i in graph.Edges)
                {
                    xmlWriter.WriteStartElement("edge");
                    xmlWriter.WriteAttributeString("id", i.Name);
                    xmlWriter.WriteAttributeString("source", i.Vertices[0].Name);
                    xmlWriter.WriteAttributeString("target", i.Vertices[1].Name);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                return stringBuilder.ToString();
            }
        }

        public static void TransformGraphToFile(Graph graph, string path, FileMode fileMode = FileMode.GRAPHML)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine(TransformGraphToString(graph, fileMode));
            streamWriter.Close();
        }
    }
}
