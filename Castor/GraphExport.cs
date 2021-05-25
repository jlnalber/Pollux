using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Thestias;

namespace Castor
{
    public partial class VisualGraph
    {
        //TODO: Export implementieren
        //Methoden, die den Graphen in eine Datei umwandeln.
        public static string TransformGraphToString(VisualGraph graph, Graph.FileMode fileMode = Graph.FileMode.GRAPHML)
        {
            if (fileMode == Graph.FileMode.POLL)
            {
                return Graph.TransformGraphToString(graph.Graph, Graph.FileMode.POLL);
            }
            else
            {
                //TODO: Mit Positionen und Aussehen exportieren, nach Standard.
                /*StringWriter stringBuilder = new();
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

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d0");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "positionx");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d1");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "positiony");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d2");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "strokeThickness");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d3");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "height");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d4");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "width");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d5");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "stroke");
                xmlWriter.WriteAttributeString("attr.type", "string");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d6");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "fill");
                xmlWriter.WriteAttributeString("attr.type", "string");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d7");
                xmlWriter.WriteAttributeString("for", "edge");
                xmlWriter.WriteAttributeString("attr.name", "stroke");
                xmlWriter.WriteAttributeString("attr.type", "string");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d8");
                xmlWriter.WriteAttributeString("for", "edge");
                xmlWriter.WriteAttributeString("attr.name", "strokeThickness");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("graph");
                xmlWriter.WriteAttributeString("id", graph.Name);
                xmlWriter.WriteAttributeString("edgedefault", "undirected");

                foreach (VisualVertex i in graph.Vertices)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", i.Vertex.Name);

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d0");
                    xmlWriter.WriteString(i.UIElement.Margin.Left.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d1");
                    xmlWriter.WriteString(i.UIElement.Margin.Top.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d2");
                    xmlWriter.WriteString(i.UIElement.StrokeThickness.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d3");
                    xmlWriter.WriteString(i.UIElement.Height.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d4");
                    xmlWriter.WriteString(i.UIElement.Width.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d5");
                    xmlWriter.WriteString(Stuff.ColorToString((SolidColorBrush)i.UIElement.Stroke));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d6");
                    xmlWriter.WriteString(Stuff.ColorToString((LinearGradientBrush)i.UIElement.Fill));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                foreach (VisualEdge i in graph.Edges)
                {
                    xmlWriter.WriteStartElement("edge");
                    xmlWriter.WriteAttributeString("id", i.Edge.Name);
                    xmlWriter.WriteAttributeString("source", i[0].Vertex.Name);
                    xmlWriter.WriteAttributeString("target", i[1].Vertex.Name);

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d7");
                    if (i.Line is Line line)
                    {
                        xmlWriter.WriteString(Stuff.ColorToString((SolidColorBrush)line.Stroke));
                    }
                    else if (i.Line is Ellipse ellipse)
                    {
                        xmlWriter.WriteString(Stuff.ColorToString((SolidColorBrush)ellipse.Stroke));
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d8");
                    if (i.Line is Line line0)
                    {
                        xmlWriter.WriteString(line0.StrokeThickness.ToString());
                    }
                    else if (i.Line is Ellipse ellipse0)
                    {
                        xmlWriter.WriteString(ellipse0.StrokeThickness.ToString());
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                return stringBuilder.ToString();*/

                return Graph.TransformGraphToString(graph.Graph, fileMode);
            }
        }

        public static void TransformGraphToFile(VisualGraph graph, string path, Graph.FileMode fileMode = Graph.FileMode.GRAPHML)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            //TransformGraphToString(graph, fileMode);
            streamWriter.WriteLine(TransformGraphToString(graph, fileMode));
            streamWriter.Close();
        }

        //Methoden, die den Graphen in ein Bild umwandeln.
        public void SaveAsSVG(string path)
        {
            //TODO: Implementieren (von Canvas zu XPS zu SVG?).
            /*//Schreibe den Anfang der Datei
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
            string defs = "\t<defs>\n";

            //Finde heraus, wie weit und wie hoch die Datei sein muss und schreibe das in sie hinein
            int width = 0;
            int height = 0;
            foreach (VisualVertex i in this.Vertices)
            {
                //Anhand der Knoten
                if (i.Ellipse.Margin.Left + i.Ellipse.Width + i.Ellipse.StrokeThickness + 10 > width)
                {
                    width = int.Parse(Math.Round(i.Ellipse.Margin.Left + i.Ellipse.Width + i.Ellipse.StrokeThickness + 10).ToString());
                }

                if (i.Ellipse.Margin.Top + i.Ellipse.Height + i.Ellipse.StrokeThickness + 10 > height)
                {
                    height = int.Parse(Math.Round(i.Ellipse.Margin.Top + i.Ellipse.Height + i.Ellipse.StrokeThickness + 10).ToString());
                }

                defs += "\t\t<linearGradient id=\"" + i.Vertex.Name + "\" gradientTransform=\"rotate(45)\">\n";
                defs += "\t\t\t<stop offset=\"0%\"  stop-color=\"" + Hexadezimal.BrushAsHexa(((LinearGradientBrush)((Ellipse)i.UIElement).Fill).GradientStops[0].Color) + "\"/>\n";
                defs += "\t\t\t<stop offset=\"100%\"  stop-color=\"" + Hexadezimal.BrushAsHexa(((LinearGradientBrush)i.Ellipse.Fill).GradientStops[1].Color) + "\"/>\n";
                defs += "\t\t</linearGradient>\n";

                //Anhand der Labels der Knoten
                if (i.Label.Margin.Left + i.Label.ActualWidth + 10 > width)
                {
                    width = int.Parse(Math.Round(i.Label.Margin.Left + i.Label.ActualWidth + 10).ToString());
                }

                if (i.Label.Margin.Top + i.Label.ActualHeight + 10 > height)
                {
                    height = int.Parse(Math.Round(i.Label.Margin.Top + i.Label.ActualHeight + 10).ToString());
                }
            }

            defs += "\t</defs>\n";

            streamWriter.WriteLine("<svg width=\"" + width + "\" height=\"" + height + "\" xmlns=\"http://www.w3.org/2000/svg\">");
            streamWriter.WriteLine(defs);

            //Schreibe die Kanten in die Datei hinein
            foreach (Kanten i in this.GraphKanten)
            {
                //Finde heraus, ob das Element eine Schlinge (Ellipse) oder eine ganz normale Kante (Line) ist

                if (i.Line is Line line)
                {
                    //Falls die Kante eine ganz normale Kante ist, also keine Schlinge schreibe sie als "line" hinein
                    //Finde die Attribute der Kante heraus
                    string x1 = Stuff.DoubleAsString(line.X1);
                    string x2 = Stuff.DoubleAsString(line.X2);
                    string y1 = Stuff.DoubleAsString(line.Y1);
                    string y2 = Stuff.DoubleAsString(line.Y2);
                    string stroke = Hexadezimal.BrushAsHexa(line.Stroke);
                    string strokeWidth = Stuff.DoubleAsString(line.StrokeThickness);

                    //Schreibe die Ergebnisse in die Datei
                    streamWriter.WriteLine("\t<line x1=\"" + x1 + "\" y1=\"" + y1 + "\" x2=\"" + x2 + "\" y2=\"" + y2 + "\" stroke=\"" + stroke + "\" stroke-width=\"" + strokeWidth + "\"/>");
                }
                else if (i.Line is Ellipse ellipse)
                {
                    //Falls die Kante eine Schlinge ist, schreibe sie als "ellipse" hinein
                    //Finde die Attribute de Schlinge heraus
                    string cx = Stuff.DoubleAsString(ellipse.Margin.Left + ellipse.Width / 2);
                    string cy = Stuff.DoubleAsString(ellipse.Margin.Top + ellipse.Height / 2);
                    string rx = Stuff.DoubleAsString(ellipse.Width / 2);
                    string ry = Stuff.DoubleAsString(ellipse.Height / 2);
                    string strokeWidth = Stuff.DoubleAsString(ellipse.StrokeThickness);

                    //Schreibe sie in die Datei
                    streamWriter.WriteLine("\t<ellipse cx=\"" + cx + "\" cy=\"" + cy + "\" rx=\"" + rx + "\" ry=\"" + ry + "\" style=\"fill:" + Hexadezimal.BrushAsHexa(ellipse.Fill) + ";stroke:" + Hexadezimal.BrushAsHexa(ellipse.Stroke) + ";stroke-width:" + strokeWidth + "\"/>");
                }
            }

            //Schreibe die Knoten und deren Label in die Datei hinein
            foreach (Knoten i in this.GraphKnoten)
            {
                //Finde die Attribute von dem Knoten heraus
                string cx = Stuff.DoubleAsString(i.Ellipse.Margin.Left + i.Ellipse.Width / 2);
                string cy = Stuff.DoubleAsString(i.Ellipse.Margin.Top + i.Ellipse.Height / 2);
                string rx = Stuff.DoubleAsString(i.Ellipse.Width / 2);
                string ry = Stuff.DoubleAsString(i.Ellipse.Height / 2);
                string strokeWidth = Stuff.DoubleAsString(i.Ellipse.StrokeThickness);

                //Finde die Attribute von dem Label heraus
                string x = Stuff.DoubleAsString(i.Label.Margin.Left);
                string y = Stuff.DoubleAsString(i.Label.Margin.Top);
                string fontSize = Stuff.DoubleAsString(i.Label.FontSize);
                string fontFamily = i.Label.FontFamily.Source;

                //Schreibe schließlich alles in die Datei
                streamWriter.WriteLine("\t<ellipse cx=\"" + cx + "\" cy=\"" + cy + "\" rx=\"" + rx + "\" ry=\"" + ry + "\" fill=\"url('#" + i.Name + "')\" style=\"stroke:" + Hexadezimal.BrushAsHexa(i.Ellipse.Stroke) + ";stroke-width:" + strokeWidth + "\"/>");
                streamWriter.WriteLine("\t<text x=\"" + x + "\" y=\"" + y + "\" style=\"font-size:" + fontSize + ";font-family:" + fontFamily + "\">" + i.Label.Content.ToString() + "</text>");
            }

            //Beende die Datei noch
            streamWriter.WriteLine("</svg>");
            streamWriter.Close();*/
        }

        public void SaveAsBitmap(string path, BitmapEncoder encoder = null)
        {
            //Canvas neu berechnen.
            var size = new Size(this.Canvas.Width, this.Canvas.Height);
            this.Canvas.Arrange(new Rect(size));
            this.Canvas.Measure(size);

            //Konvertiere den Canvas in ein Bitmap-Bild.
            var renderBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96.0D, 96.0D, PixelFormats.Pbgra32);
            renderBitmap.Render(this.Canvas);

            //Speichere das Bild ab.
            using (var os = new FileStream(path, FileMode.Create))
            {
                encoder = encoder ?? new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(os);
            }
        }
    }
}
