using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pollux.Graph
{
    public partial class GraphDarstellung
    {
        //Methoden, die den Graphen in ein Bild umwandelt

        public void SaveAsSVG(string path)
        {
            //Schreibe den Anfang der Datei
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
            string defs = "\t<defs>\n";

            //Finde heraus, wie weit und wie hoch die Datei sein muss und schreibe das in sie hinein
            int width = 0;
            int height = 0;
            foreach (Knoten i in this.GraphKnoten)
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

                defs += "\t\t<linearGradient id=\"" + i.Name + "\" gradientTransform=\"rotate(45)\">\n";
                defs += "\t\t\t<stop offset=\"0%\"  stop-color=\"" + Hexadezimal.BrushAsHexa(((LinearGradientBrush)i.Ellipse.Fill).GradientStops[0].Color) + "\"/>\n";
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
                    string x1 = Strings.DoubleAsString(line.X1);
                    string x2 = Strings.DoubleAsString(line.X2);
                    string y1 = Strings.DoubleAsString(line.Y1);
                    string y2 = Strings.DoubleAsString(line.Y2);
                    string stroke = Hexadezimal.BrushAsHexa(line.Stroke);
                    string strokeWidth = Strings.DoubleAsString(line.StrokeThickness);

                    //Schreibe die Ergebnisse in die Datei
                    streamWriter.WriteLine("\t<line x1=\"" + x1 + "\" y1=\"" + y1 + "\" x2=\"" + x2 + "\" y2=\"" + y2 + "\" stroke=\"" + stroke + "\" stroke-width=\"" + strokeWidth + "\"/>");
                }
                else if (i.Line is Ellipse ellipse)
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
            foreach (Knoten i in this.GraphKnoten)
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
                streamWriter.WriteLine("\t<ellipse cx=\"" + cx + "\" cy=\"" + cy + "\" rx=\"" + rx + "\" ry=\"" + ry + "\" fill=\"url('#" + i.Name + "')\" style=\"stroke:" + Hexadezimal.BrushAsHexa(i.Ellipse.Stroke) + ";stroke-width:" + strokeWidth + "\"/>");
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
            const int padding = factor * 10;

            //Finde heraus, wie weit und wie hoch die Datei sein muss und schreibe das in sie hinein
            int width = 0;
            int height = 0;
            foreach (Knoten i in this.GraphKnoten)
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
            Bitmap bmp = new(width * factor + padding * 2, height * factor + padding * 2);

            Graphics graphics = Graphics.FromImage(bmp);
            graphics.Clear(System.Drawing.Color.Transparent);

            //Schreibe die Kanten in die Datei hinein
            foreach (Kanten i in this.GraphKanten)
            {
                //Finde heraus, ob das Element eine Schlinge (Ellipse) oder eine ganz normale Kante (Line) ist
                if (i.Line is Line line)
                {
                    //Falls die Kante eine ganz normale Kante ist, also keine Schlinge schreibe sie als "line" hinein
                    //Finde die Attribute der Kante heraus
                    float x1 = (float)Math.Round(line.X1, 2) * factor + padding;
                    float x2 = (float)Math.Round(line.X2, 2) * factor + padding;
                    float y1 = (float)Math.Round(line.Y1, 2) * factor + padding;
                    float y2 = (float)Math.Round(line.Y2, 2) * factor + padding;
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
                    float x = (float)Math.Round(ellipse.Margin.Left, 2) * factor + padding;
                    float y = (float)Math.Round(ellipse.Margin.Top, 2) * factor + padding;
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
            foreach (Knoten i in this.GraphKnoten)
            {
                //Finde die Attribute von dem Knoten heraus
                int x = (int)Math.Round(i.Ellipse.Margin.Left) * factor + padding;
                int y = (int)Math.Round(i.Ellipse.Margin.Top) * factor + padding;
                int widthEllipse = (int)Math.Round(i.Ellipse.Width) * factor;
                int heightEllipse = (int)Math.Round(i.Ellipse.Height) * factor;
                LinearGradientBrush fillEllipse = (LinearGradientBrush)i.Ellipse.Fill;
                System.Drawing.Color fill1 = System.Drawing.Color.FromArgb(fillEllipse.GradientStops[0].Color.A, fillEllipse.GradientStops[0].Color.R, fillEllipse.GradientStops[0].Color.G, fillEllipse.GradientStops[0].Color.B);
                System.Drawing.Color fill2 = System.Drawing.Color.FromArgb(fillEllipse.GradientStops[1].Color.A, fillEllipse.GradientStops[1].Color.R, fillEllipse.GradientStops[1].Color.G, fillEllipse.GradientStops[1].Color.B);

                float strokeWidth = (float)Math.Round(i.Ellipse.StrokeThickness, 2) * factor;
                System.Windows.Media.Color stroke = ((SolidColorBrush)i.Ellipse.Stroke).Color;
                System.Drawing.Pen pen = new(System.Drawing.Color.FromArgb(stroke.A, stroke.R, stroke.G, stroke.B), strokeWidth);
                System.Drawing.Rectangle rectangle = new(x, y, widthEllipse, heightEllipse);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new(rectangle, fill1, fill2, 45);
                graphics.FillEllipse((System.Drawing.Brush)brush, rectangle);

                //Finde die Attribute von dem Label heraus
                float xLabel = (float)Math.Round(i.Label.Margin.Left, 2) * factor + padding;
                float yLabel = (float)Math.Round(i.Label.Margin.Top, 2) * factor + padding;
                float fontSize = (float)Math.Round(i.Label.FontSize, 2) * factor;
                string fontFamily = i.Label.FontFamily.Source;

                //Schreibe schließlich alles in die Datei
                graphics.DrawEllipse(pen, rectangle);
                graphics.DrawString(i.Label.Content.ToString(), new Font(fontFamily, fontSize), new SolidBrush(System.Drawing.Color.Black), xLabel, yLabel);
            }

            graphics.Save();
            bmp.Save(path);
        }
    }
}
