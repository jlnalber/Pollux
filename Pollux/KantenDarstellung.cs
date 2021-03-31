using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public new class Kanten : Graph.Graph.Kanten
        {
            //Members der Klasse
            public UIElement Line;
            public Canvas Canvas;

            public Kanten(GraphDarstellung graph, Knoten[] knoten, string name, Canvas canvas) : base(graph, knoten, name)
            {
                //falls schon eine Kante mit dem gelichen Namen existiert, werfe eine Exception
                if (graph.ContainsKanten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //Lege die Eigenschaften fest
                this.Parent = graph;
                this.Knoten = knoten;
                this.Name = name;
                this.Canvas = canvas;
                this.Line = CreateVisualEdge();

                //Füge die Kante dem Canvas hinzu
                this.Canvas.Children.Add(this.Line);
            }

            public Graph.Graph.Kanten CastToKanten(Graph.Graph graph)
            {
                return new Graph.Graph.Kanten(graph, this.Knoten, Name);
            }

            public Graph.Graph.Kanten CastToKanten()
            {
                return new Graph.Graph.Kanten(new(), this.Knoten, Name);
            }

            public bool Disappear()
            {
                try
                {
                    this.Canvas.Children.Remove(this.Line);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private UIElement CreateVisualEdge()
            {

                //Lese die Einstellungen aus
                #region
                //Lese die Farben in den Einstellungen nach
                SolidColorBrush kanten_FarbeBorder = new(Color.FromArgb(Properties.Settings.Default.Kante_FarbeBorder.A, Properties.Settings.Default.Kante_FarbeBorder.R, Properties.Settings.Default.Kante_FarbeBorder.G, Properties.Settings.Default.Kante_FarbeBorder.B));

                //Lese die Höhen und die Thickness in den Einstellungen nach
                double knoten_Height = Properties.Settings.Default.Knoten_Höhe;
                double kanten_Thickness = Properties.Settings.Default.Kanten_Thickness;
                #endregion

                if (this.Knoten[0] == this.Knoten[1])
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    Ellipse ellipse = new Ellipse();

                    //Finde die Margins des Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten = ((Knoten)this.Knoten[0]).Ellipse.Margin;

                    //lege eine Konstante für die Höhe fest
                    const int height = 40;

                    //Lege noch andere Werte der Linie fest
                    ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                    ellipse.VerticalAlignment = VerticalAlignment.Bottom;
                    ellipse.Stroke = kanten_FarbeBorder;
                    ellipse.StrokeThickness = kanten_Thickness;
                    ellipse.Fill = Brushes.Transparent;
                    ellipse.Height = height;
                    ellipse.Width = height;
                    if (this.Canvas.Children.Count != 0)
                    {
                        Canvas.SetTop(ellipse, Canvas.GetTop(this.Canvas.Children[0]));
                        Canvas.SetLeft(ellipse, Canvas.GetLeft(this.Canvas.Children[0]));
                    }
                    else
                    {
                        Canvas.SetTop(ellipse, 0);
                        Canvas.SetLeft(ellipse, 0);
                    }

                    //lege die Position fest
                    ellipse.Margin = new Thickness(marginKnoten.Left - ellipse.Width / 2 - 10, marginKnoten.Top - 5, 10, 10);

                    //Füge ein ContextMenu hinzu
                    #region
                    //ContextMenu "contextMenu"
                    ContextMenu contextMenu = new ContextMenu();
                    ellipse.ContextMenu = contextMenu;

                    //MenuItem zum Löschen des Knoten
                    MenuItem löschen = new MenuItem();
                    löschen.Header = MainWindow.resman.GetString("LöschenKante", MainWindow.cul);
                    löschen.Icon = " - ";
                    löschen.Click += MainWindow.main.LöschenKante_Click;

                    //MenuItem zur Bearbeitung von Graph
                    MenuItem menuItem1 = new();
                    menuItem1.Header = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul);

                    //MenuItem zum Hinzufügen von Kanten
                    MenuItem menuItem2 = new();
                    menuItem2.Header = MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
                    menuItem2.Icon = " + ";
                    menuItem2.Click += MainWindow.main.KanteHinzufügen_Click;

                    //MenuItem zum Hinzufügen von Knoten
                    MenuItem menuItem3 = new();
                    menuItem3.Header = MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
                    menuItem3.Icon = " + ";
                    menuItem3.Click += MainWindow.main.KnotenHinzufügen_Click;

                    //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                    menuItem1.Items.Add(menuItem2);
                    menuItem1.Items.Add(menuItem3);

                    //MenuItem zum Öffnen des Eiganschaften-Fensters
                    MenuItem menuItem4 = new();
                    menuItem4.Click += MainWindow.main.EigenschaftenFenster_Click;
                    menuItem4.Header = MainWindow.resman.GetString("EigenschaftenFenster", MainWindow.cul);

                    //Füge alle MenuItems zum ContextMenu "contextMenu" hinzu
                    contextMenu.Items.Add(löschen);
                    contextMenu.Items.Add(new Separator());
                    contextMenu.Items.Add(menuItem1);
                    contextMenu.Items.Add(menuItem4);
                    #endregion

                    return ellipse;
                }
                else
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    Line line = new Line();

                    //Erstelle die visuelle Kante "kanten"

                    //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten0 = ((Knoten)this.Knoten[0]).Ellipse.Margin;
                    Thickness marginKnoten1 = ((Knoten)this.Knoten[1]).Ellipse.Margin;
                    double height = ((Knoten)this.Knoten[0]).Ellipse.Height;

                    //finde dadurch die Position heraus, wo die Kante starten und enden muss
                    const double maxDistance = 20;
                    double distance = Strings.Bigger(Math.Abs(marginKnoten0.Top - marginKnoten1.Top), Math.Abs(marginKnoten0.Left - marginKnoten1.Left)) * 0.1 + 1;
                    distance = (distance > maxDistance) ? maxDistance : distance;
                    double y = Math.Sqrt(distance * distance / (Math.Pow(Math.Abs((marginKnoten0.Left - marginKnoten1.Left) / (marginKnoten0.Top - marginKnoten1.Top)), 2) + 1));
                    double x = Math.Sqrt(distance * distance - y * y);
                    y = !(y > 0) ? 0 : y;
                    x = !(x > 0) ? 0 : x;

                    //schreibe diese Eigenschaften in die Linie
                    if (marginKnoten0.Top > marginKnoten1.Top)
                    {
                        line.Y1 = marginKnoten0.Top + height / 2 - y;
                        line.Y2 = marginKnoten1.Top + height / 2 + y;
                    }
                    else
                    {
                        line.Y1 = marginKnoten0.Top + height / 2 + y;
                        line.Y2 = marginKnoten1.Top + height / 2 - y;
                    }
                    if (marginKnoten0.Left > marginKnoten1.Left)
                    {
                        line.X1 = marginKnoten0.Left + height / 2 - x;
                        line.X2 = marginKnoten1.Left + height / 2 + x;
                    }
                    else
                    {
                        line.X1 = marginKnoten0.Left + height / 2 + x;
                        line.X2 = marginKnoten1.Left + height / 2 - x;
                    }
                    Canvas.SetTop(line, 0);
                    Canvas.SetLeft(line, 0);

                    //Lege noch andere Werte der Linie fest
                    line.HorizontalAlignment = HorizontalAlignment.Left;
                    line.VerticalAlignment = VerticalAlignment.Bottom;
                    line.Stroke = kanten_FarbeBorder;
                    line.StrokeThickness = kanten_Thickness;

                    //Füge ein ContextMenu hinzu
                    #region
                    //ContextMenu "contextMenu"
                    ContextMenu contextMenu = new ContextMenu();
                    line.ContextMenu = contextMenu;

                    //MenuItem zum Öffnen der Eigenschaften
                    MenuItem eigenschaften = new();
                    eigenschaften.Header = MainWindow.resman.GetString("EigenschaftenKante", MainWindow.cul);
                    eigenschaften.Click += Eigenschaften_Click;

                    //MenuItem zum Löschen des Knoten
                    MenuItem löschen = new MenuItem();
                    löschen.Header = MainWindow.resman.GetString("LöschenKante", MainWindow.cul);
                    löschen.Icon = " - ";
                    löschen.Click += MainWindow.main.LöschenKante_Click;

                    //MenuItem zur Bearbeitung von Graph
                    MenuItem menuItem1 = new();
                    menuItem1.Header = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul);

                    //MenuItem zum Hinzufügen von Kanten
                    MenuItem menuItem2 = new();
                    menuItem2.Header = MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
                    menuItem2.Icon = " + ";
                    menuItem2.Click += MainWindow.main.KanteHinzufügen_Click;

                    //MenuItem zum Hinzufügen von Knoten
                    MenuItem menuItem3 = new();
                    menuItem3.Header = MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
                    menuItem3.Icon = " + ";
                    menuItem3.Click += MainWindow.main.KnotenHinzufügen_Click;

                    //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                    menuItem1.Items.Add(menuItem2);
                    menuItem1.Items.Add(menuItem3);

                    //MenuItem zum Öffnen des Eiganschaften-Fensters
                    MenuItem menuItem4 = new();
                    menuItem4.Click += MainWindow.main.EigenschaftenFenster_Click;
                    menuItem4.Header = MainWindow.resman.GetString("EigenschaftenFenster", MainWindow.cul);

                    //Füge alle MenuItems zum ContextMenu "contextMenu" hinzu
                    contextMenu.Items.Add(eigenschaften);
                    contextMenu.Items.Add(löschen);
                    contextMenu.Items.Add(new Separator());
                    contextMenu.Items.Add(menuItem1);
                    contextMenu.Items.Add(menuItem4);
                    #endregion

                    //Rückgabe
                    return line;
                }
            }

            private void Eigenschaften_Click(object sender, RoutedEventArgs e)
            {
                //Methode, wenn das MenuItem "eigenschaften" geklickt wurde
                MainWindow.main.OpenedEigenschaftenFenster[MainWindow.main.GetOpenTab()].OpenEdge(this);
            }
        }
    }
}
