using Pollux.Verschlüsselungen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public Graph.Graph graph;

        public List<GraphDarstellung.KantenDarstellung> visuelleKanten = new List<GraphDarstellung.KantenDarstellung>();
        public List<KnotenDarstellung> visuelleKnoten = new List<KnotenDarstellung>();

        public void SaveAsSVG(string path)
        {
            //Schreibe den Anfang der Datei
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");

            //Finde heraus, wie weit und wie hoch die Datei sein muss und schreibe das in sie hinein
            int width = 0;
            int height = 0;
            foreach (KnotenDarstellung i in this.visuelleKnoten)
            {
                //Anahnd der Knoten
                if (i.Ellipse.Margin.Left + i.Ellipse.Width + i.Ellipse.StrokeThickness + 10 > width)
                {
                    width = int.Parse(Math.Round(i.Ellipse.Margin.Left + i.Ellipse.Width + i.Ellipse.StrokeThickness + 10).ToString());
                }

                if (i.Ellipse.Margin.Top + i.Ellipse.Height + i.Ellipse.StrokeThickness + 10 > height)
                {
                    height = int.Parse(Math.Round(i.Ellipse.Margin.Top + i.Ellipse.Height + i.Ellipse.StrokeThickness + 10).ToString());
                }

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
            streamWriter.WriteLine("<svg width=\"" + width + "\" height=\"" + height + "\" xmlns=\"http://www.w3.org/2000/svg\">");

            //Schreibe die Kanten in die Datei hinein
            foreach (KantenDarstellung i in this.visuelleKanten)
            {
                //Finde heraus, ob das Element eine Schlinge (Ellipse) oder eine ganz normale Kante (Line) ist
                bool IsLine = true;
                Line line = new();
                Ellipse ellipse = new();
                switch (i.Line)
                {
                    case Line line1: IsLine = true; line = line1; break;
                    case Ellipse ellipse1: IsLine = false; ellipse = ellipse1; break;
                }

                if (IsLine)
                {
                    //Falls die Kante eine ganz normale Kante ist, also keine Schlinge schreibe sie als "line" hinein
                    //Finde die Attribute der Kante heraus
                    string x1 = Strings.DoubleAsString(line.X1);
                    string x2 = Strings.DoubleAsString(line.X2);
                    string y1 = Strings.DoubleAsString(line.Y1);
                    string y2 = Strings.DoubleAsString(line.Y2);
                    string stroke = Hexadezimal.BrushAsHexa(line.Stroke);
                    string strokeWidth = Strings.DoubleAsString(line.StrokeThickness);

                    //Schreibe die Ergebnisse in die Datei
                    streamWriter.WriteLine("\t<line x1=\"" + x1 + "\" y1=\"" + y1 + "\" x2=\"" + x2 + "\" y2=\"" + y2 + "\" stroke=\"" + stroke + "\" stroke-width=\"" + strokeWidth + "\"/>");
                }
                else
                {
                    //Falls die Kante eine Schlinge ist, schreibe sie als "ellipse" hinein
                    //Finde die Attribute de Schlinge heraus
                    string cx = Strings.DoubleAsString(ellipse.Margin.Left + ellipse.Width / 2);
                    string cy = Strings.DoubleAsString(ellipse.Margin.Top + ellipse.Height / 2);
                    string rx = Strings.DoubleAsString(ellipse.Width / 2);
                    string ry = Strings.DoubleAsString(ellipse.Height / 2);
                    string strokeWidth = Strings.DoubleAsString(ellipse.StrokeThickness);

                    //Schreibe sie in die Datei
                    streamWriter.WriteLine("\t<ellipse cx=\"" + cx + "\" cy=\"" + cy + "\" rx=\"" + rx + "\" ry=\"" + ry + "\" style=\"fill:" + Hexadezimal.BrushAsHexa(ellipse.Fill) + ";stroke:" + Hexadezimal.BrushAsHexa(ellipse.Stroke) + ";stroke-width:" + strokeWidth + "\"/>");
                }
            }

            //Schreibe die Knoten und deren Label in die Datei hinein
            foreach (KnotenDarstellung i in this.visuelleKnoten)
            {
                //Finde die Attribute von dem Knoten heraus
                string cx = Strings.DoubleAsString(i.Ellipse.Margin.Left + i.Ellipse.Width / 2);
                string cy = Strings.DoubleAsString(i.Ellipse.Margin.Top + i.Ellipse.Height / 2);
                string rx = Strings.DoubleAsString(i.Ellipse.Width / 2);
                string ry = Strings.DoubleAsString(i.Ellipse.Height / 2);
                string strokeWidth = Strings.DoubleAsString(i.Ellipse.StrokeThickness);

                //Finde die Attribute von dem Label heraus
                string x = Strings.DoubleAsString(i.Label.Margin.Left);
                string y = Strings.DoubleAsString(i.Label.Margin.Top);
                string fontSize = Strings.DoubleAsString(i.Label.FontSize);
                string fontFamily = i.Label.FontFamily.Source;

                //Schreibe schließlich alles in die Datei
                streamWriter.WriteLine("\t<ellipse cx=\"" + cx + "\" cy=\"" + cy + "\" rx=\"" + rx + "\" ry=\"" + ry + "\" style=\"fill:" + Hexadezimal.BrushAsHexa(i.Ellipse.Fill) + ";stroke:" + Hexadezimal.BrushAsHexa(i.Ellipse.Stroke) + ";stroke-width:" + strokeWidth + "\"/>");
                streamWriter.WriteLine("\t<text x=\"" + x + "\" y=\"" + y + "\" style=\"font-size:" + fontSize + ";font-family:" + fontFamily + "\">" + i.Label.Content.ToString() + "</text>");
            }

            //Beende die Datei noch
            streamWriter.WriteLine("</svg>");
            streamWriter.Close();
        }
    }
}
