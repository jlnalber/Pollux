using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pollux
{
    public static class Stuff
    {
        //Konstante Alphabet mit ihrer get-Methode
        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Alphabet { get => alphabet; }

        public const string hexabet = "0123456789ABCDEF";

        public static int Count(string str, char item)
        {
            //erstelle einen Counter, der die Anzahl der des gesuchten Zeichens in der string zählt
            int counter = 0;

            //geht die Liste durch; entspricht das Zeichen dem gesuchten Zeichen, so erhöhe den Counter
            foreach (char i in str)
            {
                if (i == item)
                {
                    ++counter;
                }
            }

            //Rückgabe
            return counter;
        }

        public static decimal CountAnteil(string str, char item)
        {
            //speichert ab, wie oft das Zeichen in der string vorhanden ist, und gibt diesen Wert durch die Länge des strings zurück (Anteil)
            decimal dec = Count(str, item);
            return dec / str.Length;
        }

        public static char HäufigstesZeichen(string str)
        {
            //erstelle Listen
            List<char> list = new List<char>();
            List<int> listInt = new List<int>();

            //füge zur list hinzu, welche Zeichen alle in der string str enthalten sind und füge die Anzahl zur listInt hinzu
            foreach (char i in str.ToUpper())
            {
                if (!list.Contains(i))
                {
                    list.Add(i);
                    listInt.Add(Count(str, i));
                }
            }

            //sucht die größte Zahl in listInt und gibt das dazugehörige Zeichen zurück
            int biggest = 0;
            foreach (int i in listInt)
            {
                if (i > biggest)
                {
                    biggest = i;
                }
            }

            //Rückgabe
            return list[listInt.IndexOf(biggest)];
        }

        public static char HäufigsterBuchstabe(string text)
        {
            //Variablen initialisieren und verändern
            string str = "";
            string alphabet = Stuff.Alphabet;
            text = text.ToUpper();

            //geht sie string durch, falls das Zeichen im Alphabet enthalten ist, so füge es zur anderen string hinzu
            foreach (char i in text)
            {
                if (alphabet.Contains(i))
                {
                    str += i;
                }
            }

            //Rückgabe
            return HäufigstesZeichen(str);
        }

        public static int IndexOfBiggestNumber(List<int> list)
        {
            //lege Variablen für den Index und die zwischenzeitlich größte Zahl fest
            int index = 0;
            int biggestNumber = 0;

            //gehe die Liste durch und gucke, ob die aktuelle Zahl größer ist als die ursprünglich größte. Wenn ja, speichere ihren Index ab
            for (int i = 0; i < list.Count(); ++i)
            {
                if (list[i] > biggestNumber)
                {
                    biggestNumber = list[i];
                    index = i;
                }
            }

            //Gebe den Index der größten Zahl zurück
            return index;
        }

        public static int IndexOfBiggestNumber(int[] arr)
        {
            return Array.IndexOf(arr, IndexOfBiggestNumber(arr));
        }

        public static int BiggestNumber(int[] arr)
        {
            int biggest = 0;

            foreach (int i in arr)
            {
                if (i > biggest)
                {
                    biggest = i;
                }
            }

            return biggest;
        }

        public static int ContainsHowOften(string text, string item)
        {
            //Zähler
            int howOften = 0;

            //solange das item noch in der Substring drin ist, erhöhe den Zähler; danach wird immer die Substring so abgeändert, dass alles bis zum nach dem Vorkommen des Items gelöscht wird
            while (text.Contains(item))
            {
                ++howOften;

                text = text.Substring(text.IndexOf(item) + item.Length);
            }

            //Rückgabe
            return howOften;
        }

        public static List<int> Distanzen(string text, string item, int howOften)
        {
            //deklariere Listen
            List<int> distanzen = new List<int>();//enthält später von allen items die Distanz zueinander
            List<int> positionen = new List<int>();//enthält nachher alle Positionen der items

            string substr = text;//hier wird einfach eine Substring von "text" abgespeichert

            //rechne von jedem Vorkommen des "item"'s die Postion aus und speichere sie in der Liste "positionen"
            for (int i = 0; i < howOften; ++i)
            {
                positionen.Add(substr.IndexOf(item) + text.Length - substr.Length);//rechne die Position auf den ganzen text bezogen aus
                substr = substr.Substring(substr.IndexOf(item) + item.Length);//Mache die substr bis nach dem Vorkommen des "item"'s
            }

            //Rechne von jedem, zu jedem anderen item die Distanz aus und speichere sie in "distanzen" ab (wird später zurückgegeben)
            foreach (int i in positionen)
            {
                foreach (int f in SubList(positionen, positionen.IndexOf(i) + 1))
                {
                    distanzen.Add(f - i);
                }
            }

            //Rückgabe
            return distanzen;
        }

        public static List<int> SubList(List<int> list, int index)
        {
            //Methode erstellt eine Subliste vom gegebenen Index aus
            List<int> newList = new List<int>();//neue Subliste
            int counter = 0;//Zähler

            //geht die Liste durch
            foreach (int i in list)
            {
                //falls der index eingetreten ist, ab dem man die Subliste möchte, dann füge das Element hinzu
                if (counter >= index)
                {
                    newList.Add(i);
                }
                ++counter;
            }

            //Rückgabe
            return newList;
        }

        public static string EntferneZeichen(string text)
        {
            string str = "";
            foreach (char i in text)
            {
                if (Alphabet.Contains(i))
                {
                    str += i;
                }
            }
            return str;
        }

        public static string Hochstellen(double numbers)
        {
            string str = numbers.ToString();
            string solution = "";
            foreach (char i in str)
            {
                switch (i)
                {
                    case ('0'): solution += "\u2070"; break;
                    case ('1'): solution += "\u00b9"; break;
                    case ('2'): solution += "²"; break;
                    case ('3'): solution += "³"; break;
                    case ('4'): solution += "\u2074"; break;
                    case ('5'): solution += "\u2075"; break;
                    case ('6'): solution += "\u2076"; break;
                    case ('7'): solution += "\u2077"; break;
                    case ('8'): solution += "\u2078"; break;
                    case ('9'): solution += "\u2079"; break;
                    case ('-'): solution += "\u207b"; break;
                    case (' '): solution += " "; break;
                    case ('.'): solution += "\u2e33"; break;
                }
            }
            return solution;
        }

        public static string DoubleAsString(double number)
        {
            return number.ToString().Replace(',', '.');
        }

        public static double Bigger(double number1, double number2)
        {
            if (number1 > number2)
            {
                return number1;
            }
            else if (number2 > number1)
            {
                return number2;
            }
            return number1;
        }

        public static int ToNumber(string text)
        {
            return int.Parse(ToNumberAsString(text));
        }

        public static double ToDoubleEN(string text)
        {
            return double.Parse(ToDoubleAsStringEN(text));
        }

        public static double ToDoubleDE(string text)
        {
            return double.Parse(ToDoubleAsStringDE(text));
        }

        public static double ToDouble(string text)
        {
            return double.Parse(ToDoubleAsString(text));
        }

        public static string ToNumberAsString(string text)
        {
            string numbers = "0123456789";
            string newText = "";
            foreach (char i in text)
            {
                if (numbers.Contains(i)) newText += i;
            }

            return newText;
        }

        public static string ToDoubleAsStringEN(string text)
        {
            string numbers = "0123456789";
            string newText = "";
            foreach (char i in text)
            {
                if (i == '.' && !newText.Contains('.'))
                {
                    newText += '.';
                }
                else if (numbers.Contains(i)) newText += i;
            }

            return newText;
        }

        public static string ToDoubleAsStringDE(string text)
        {
            string numbers = "0123456789";
            string newText = "";
            foreach (char i in text)
            {
                if (i == ',' && !newText.Contains('.'))
                {
                    newText += '.';
                }
                else if (numbers.Contains(i)) newText += i;
            }

            return newText;
        }

        public static string ToDoubleAsString(string text)
        {
            string numbers = "0123456789";
            string newText = "";
            foreach (char i in text)
            {
                if ((i == '.' || i == ',') && !newText.Contains('.'))
                {
                    newText += '.';
                }
                else if (numbers.Contains(i)) newText += i;
            }

            return newText;
        }

        public static Line CopyLine(Line line)
        {
            Line line1 = new();
            line1.X1 = line.X1;
            line1.X2 = line.X2;
            line1.Y1 = line.Y1;
            line1.Y2 = line.Y2;
            line1.Cursor = line.Cursor;
            line1.StrokeThickness = line.StrokeThickness;
            line1.Stroke = CopySolidColorBrush((SolidColorBrush)line.Stroke);
            if (line.Fill is LinearGradientBrush)
            {
                line1.Fill = CopyLinearGradientBrush((LinearGradientBrush)line.Fill);
            }
            else
            {
                line1.Fill = CopySolidColorBrush((SolidColorBrush)line.Fill);
            }
            line1.VerticalAlignment = line.VerticalAlignment;
            line1.HorizontalAlignment = line.HorizontalAlignment;
            line1.Margin = new(line.Margin.Left, line.Margin.Top, line.Margin.Right, line.Margin.Bottom);
            //line1.ContextMenu = CopyContextMenu(line.ContextMenu);
            return line1;
        }

        public static Ellipse CopyEllipse(Ellipse ellipse)
        {
            Ellipse ellipse1 = new();
            ellipse1.Width = ellipse.Width;
            ellipse1.Height = ellipse.Height;
            ellipse1.Margin = new(ellipse.Margin.Left, ellipse.Margin.Top, ellipse.Margin.Right, ellipse.Margin.Bottom);
            ellipse1.HorizontalAlignment = ellipse.HorizontalAlignment;
            ellipse1.VerticalAlignment = ellipse.VerticalAlignment;
            ellipse1.Cursor = ellipse.Cursor;
            Canvas.SetTop(ellipse1, Canvas.GetTop(ellipse));
            Canvas.SetLeft(ellipse1, Canvas.GetLeft(ellipse));
            Canvas.SetZIndex(ellipse1, Canvas.GetZIndex(ellipse));
            ellipse1.StrokeThickness = ellipse.StrokeThickness;
            //ellipse1.ContextMenu = CopyContextMenu(ellipse.ContextMenu);
            ellipse1.Stroke = CopySolidColorBrush((SolidColorBrush)ellipse.Stroke);
            if (ellipse.Fill is LinearGradientBrush)
            {
                ellipse1.Fill = CopyLinearGradientBrush((LinearGradientBrush)ellipse.Fill);
            }
            else
            {
                ellipse1.Fill = CopySolidColorBrush((SolidColorBrush)ellipse.Fill);
            }
            return ellipse1;
        }

        public static SolidColorBrush CopySolidColorBrush(SolidColorBrush solidColorBrush)
        {
            return new(solidColorBrush.Color);
        }

        public static LinearGradientBrush CopyLinearGradientBrush(LinearGradientBrush linearGradientBrush)
        {
            LinearGradientBrush linearGradientBrush1 = new();
            linearGradientBrush1.StartPoint = linearGradientBrush.StartPoint;
            linearGradientBrush1.EndPoint = linearGradientBrush.EndPoint;
            foreach (var i in linearGradientBrush.GradientStops)
            {
                linearGradientBrush1.GradientStops.Add(new(i.Color, i.Offset));
            }
            return linearGradientBrush1;
        }

        public static ContextMenu CopyContextMenu(ContextMenu contextMenu)
        {
            ContextMenu contextMenu1 = new();
            foreach (var i in contextMenu.Items)
            {
                if (i is MenuItem mi)
                {
                    contextMenu1.Items.Add(CopyMenuItem(mi));
                }
                else if (i is Separator)
                {
                    contextMenu1.Items.Add(new Separator());
                }
            }
            return contextMenu1;
        }

        public static MenuItem CopyMenuItem(MenuItem menuItem)
        {
            MenuItem menuItem1 = new();
            menuItem1.Icon = menuItem.Icon;
            menuItem1.Header = menuItem.Header;
            foreach (var i in menuItem.Items)
            {
                if (i is MenuItem mi)
                {
                    menuItem1.Items.Add(CopyMenuItem(mi));
                }
                else if (i is Separator)
                {
                    menuItem1.Items.Add(new Separator());
                }
            }
            //menuItem1.Click += menuItem.Click[0];
            return menuItem1;
        }

        public static string ColorToString(SolidColorBrush solidColorBrush)
        {
            string str = "#";
            str += hexabet[solidColorBrush.Color.R / 16];
            str += hexabet[solidColorBrush.Color.R % 16];
            str += hexabet[solidColorBrush.Color.G / 16];
            str += hexabet[solidColorBrush.Color.G % 16];
            str += hexabet[solidColorBrush.Color.B / 16];
            str += hexabet[solidColorBrush.Color.B % 16];
            str += hexabet[solidColorBrush.Color.A / 16];
            str += hexabet[solidColorBrush.Color.A % 16];
            return str;
        }

        public static string ColorToString(LinearGradientBrush linearGradientBrush)
        {
            if (linearGradientBrush.GradientStops.Count == 1)
            {
                string str = "#";
                str += hexabet[linearGradientBrush.GradientStops[0].Color.R / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.R % 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.G / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.G % 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.B / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.B % 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.A / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.A % 16];
                return str;
            }
            else if (linearGradientBrush.GradientStops.Count == 2)
            {
                string str = "#";
                str += hexabet[linearGradientBrush.GradientStops[0].Color.R / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.R % 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.G / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.G % 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.B / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.B % 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.A / 16];
                str += hexabet[linearGradientBrush.GradientStops[0].Color.A % 16];
                str += "-";
                str += hexabet[linearGradientBrush.GradientStops[1].Color.R / 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.R % 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.G / 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.G % 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.B / 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.B % 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.A / 16];
                str += hexabet[linearGradientBrush.GradientStops[1].Color.A % 16];
                return str;
            }
            else
            {
                return "#000000";
            }
        }

        public static Brush StringToColor(string color)
        {
            if (color.Length == 9)
            {
                byte r = (byte)(hexabet.IndexOf(color[1]) * 16 + hexabet.IndexOf(color[2]));
                byte g = (byte)(hexabet.IndexOf(color[3]) * 16 + hexabet.IndexOf(color[4]));
                byte b = (byte)(hexabet.IndexOf(color[5]) * 16 + hexabet.IndexOf(color[6]));
                byte a = (byte)(hexabet.IndexOf(color[7]) * 16 + hexabet.IndexOf(color[8]));

                return new SolidColorBrush(Color.FromArgb(a, r, g, b));
            }
            else if (color.Length == 7)
            {
                byte r = (byte)(hexabet.IndexOf(color[1]) * 16 + hexabet.IndexOf(color[2]));
                byte g = (byte)(hexabet.IndexOf(color[3]) * 16 + hexabet.IndexOf(color[4]));
                byte b = (byte)(hexabet.IndexOf(color[5]) * 16 + hexabet.IndexOf(color[6]));

                return new SolidColorBrush(Color.FromRgb(r, g, b));
            }
            else if (color.Length == 18)
            {
                byte r1 = (byte)(hexabet.IndexOf(color[1]) * 16 + hexabet.IndexOf(color[2]));
                byte g1 = (byte)(hexabet.IndexOf(color[3]) * 16 + hexabet.IndexOf(color[4]));
                byte b1 = (byte)(hexabet.IndexOf(color[5]) * 16 + hexabet.IndexOf(color[6]));
                byte a1 = (byte)(hexabet.IndexOf(color[7]) * 16 + hexabet.IndexOf(color[8]));

                byte r2 = (byte)(hexabet.IndexOf(color[10]) * 16 + hexabet.IndexOf(color[11]));
                byte g2 = (byte)(hexabet.IndexOf(color[12]) * 16 + hexabet.IndexOf(color[13]));
                byte b2 = (byte)(hexabet.IndexOf(color[14]) * 16 + hexabet.IndexOf(color[15]));
                byte a2 = (byte)(hexabet.IndexOf(color[16]) * 16 + hexabet.IndexOf(color[17]));

                LinearGradientBrush linearGradientBrush = new(Color.FromArgb(a1, r1, g1, b1), Color.FromArgb(a2, r2, g2, b2), 45.0);
                return linearGradientBrush;
            }
            else if (color.Length == 14)
            {
                byte r1 = (byte)(hexabet.IndexOf(color[1]) * 16 + hexabet.IndexOf(color[2]));
                byte g1 = (byte)(hexabet.IndexOf(color[3]) * 16 + hexabet.IndexOf(color[4]));
                byte b1 = (byte)(hexabet.IndexOf(color[5]) * 16 + hexabet.IndexOf(color[6]));

                byte r2 = (byte)(hexabet.IndexOf(color[8]) * 16 + hexabet.IndexOf(color[9]));
                byte g2 = (byte)(hexabet.IndexOf(color[10]) * 16 + hexabet.IndexOf(color[11]));
                byte b2 = (byte)(hexabet.IndexOf(color[12]) * 16 + hexabet.IndexOf(color[13]));

                LinearGradientBrush linearGradientBrush = new(Color.FromRgb(r1, g1, b1), Color.FromRgb(r2, g2, b2), 45.0);
                return linearGradientBrush;
            }
            else
            {
                return new SolidColorBrush();
            }
        }

        public static string GetDirectory(string filePath)
        {
            return filePath.Substring(0, filePath.LastIndexOf("\\") + 1); ;
        }
    }
}
