using Pollux.Verschlüsselungen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media;
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

        public void SaveAsBitmap(string path)
        {
            //Der Vergrößerungsfaktor "factor"
            const int factor = 8;

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

            //Erstelle eine Bitmap
            Bitmap bmp = new(width * factor, height * factor);

            Graphics graphics = Graphics.FromImage(bmp);
            graphics.Clear(System.Drawing.Color.Transparent);

            //Schreibe die Kanten in die Datei hinein
            foreach (KantenDarstellung i in this.visuelleKanten)
            {
                //Finde heraus, ob das Element eine Schlinge (Ellipse) oder eine ganz normale Kante (Line) ist
                if (i.Line is Line line)
                {
                    //Falls die Kante eine ganz normale Kante ist, also keine Schlinge schreibe sie als "line" hinein
                    //Finde die Attribute der Kante heraus
                    float x1 = (float)Math.Round(line.X1, 2) * factor;
                    float x2 = (float)Math.Round(line.X2, 2) * factor;
                    float y1 = (float)Math.Round(line.Y1, 2) * factor;
                    float y2 = (float)Math.Round(line.Y2, 2) * factor;
                    System.Windows.Media.Color stroke = ((SolidColorBrush)line.Stroke).Color;
                    float strokeWidth = (float)Math.Round(line.StrokeThickness, 2) * factor;
                    System.Drawing.Pen pen = new(System.Drawing.Color.FromArgb(stroke.A, stroke.R, stroke.G, stroke.B), strokeWidth);

                    //Male die Ergebnisse in die Datei
                    graphics.DrawLine(pen, x1, y1, x2, y2);
                }
                else if (i.Line is Ellipse ellipse)
                {
                    //Falls die Kante eine Schlinge ist, schreibe sie als "ellipse" hinein
                    //Finde die Attribute de Schlinge heraus
                    float x = (float)Math.Round(ellipse.Margin.Left, 2) * factor;
                    float y = (float)Math.Round(ellipse.Margin.Top, 2) * factor;
                    float widthEllipse = (float)Math.Round(ellipse.Width, 2) * factor;
                    float heightEllipse = (float)Math.Round(ellipse.Height, 2) * factor;
                    float strokeWidth = (float)Math.Round(ellipse.StrokeThickness, 2) * factor;
                    System.Windows.Media.Color stroke = ((SolidColorBrush)ellipse.Stroke).Color;
                    System.Drawing.Pen pen = new(System.Drawing.Color.FromArgb(stroke.A, stroke.R, stroke.G, stroke.B), strokeWidth);

                    //Schreibe sie in die Datei
                    graphics.DrawEllipse(pen, x, y, widthEllipse, heightEllipse);
                }
            }

            //Schreibe die Knoten und deren Label in die Datei hinein
            foreach (KnotenDarstellung i in this.visuelleKnoten)
            {
                //Finde die Attribute von dem Knoten heraus
                int x = (int)Math.Round(i.Ellipse.Margin.Left) * factor;
                int y = (int)Math.Round(i.Ellipse.Margin.Top) * factor;
                int widthEllipse = (int)Math.Round(i.Ellipse.Width) * factor;
                int heightEllipse = (int)Math.Round(i.Ellipse.Height) * factor;
                SolidColorBrush fillEllipse = (SolidColorBrush)i.Ellipse.Fill;
                System.Drawing.Color fill = System.Drawing.Color.FromArgb(fillEllipse.Color.A, fillEllipse.Color.R, fillEllipse.Color.G, fillEllipse.Color.B);
                SolidBrush brush = new(fill);

                float strokeWidth = (float)Math.Round(i.Ellipse.StrokeThickness, 2) * factor;
                System.Windows.Media.Color stroke = ((SolidColorBrush)i.Ellipse.Stroke).Color;
                System.Drawing.Pen pen = new(System.Drawing.Color.FromArgb(stroke.A, stroke.R, stroke.G, stroke.B), strokeWidth);
                System.Drawing.Rectangle rectangle = new(x, y, widthEllipse, heightEllipse);
                graphics.FillEllipse((System.Drawing.Brush)brush, rectangle);

                //Finde die Attribute von dem Label heraus
                float xLabel = (float)Math.Round(i.Label.Margin.Left, 2) * factor;
                float yLabel = (float)Math.Round(i.Label.Margin.Top, 2) * factor;
                float fontSize = (float)Math.Round(i.Label.FontSize, 2) * factor;
                string fontFamily = i.Label.FontFamily.Source;

                //Schreibe schließlich alles in die Datei
                graphics.DrawEllipse(pen, rectangle);
            }

            graphics.Save();
            bmp.Save(path);
        }
    }
}
