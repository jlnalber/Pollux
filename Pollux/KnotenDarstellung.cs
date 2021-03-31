using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public new class Knoten : Graph.Graph.Knoten
        {
            //Members von der Klasse
            #region
            public Ellipse Ellipse;
            public Canvas Canvas;
            public Label Label;
            internal const int LabelToRight = 40;
            internal const int LabelToTop = 20;
            private delegate void Del();
            private bool IsFocus = false;//Gibt an, ob der Knoten gerade vom Benutzer bewegt wird

            #endregion

            //Konstruktoren der Klasse
            #region
            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Canvas canvas) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = CreateVisualNode();
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;
                this.Label = CreateLabel();

                canvas.Children.Add(this.Ellipse);
                canvas.Children.Add(this.Label);
            }

            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Canvas canvas, double x, double y) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = CreateVisualNode(x, y);
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;
                this.Label = CreateLabel();

                canvas.Children.Add(this.Ellipse);
                canvas.Children.Add(this.Label);
            }

            public Knoten(GraphDarstellung graph, List<Kanten> kanten, string name, Ellipse ellipse, Label label, Canvas canvas) : base(graph, name)
            {
                //falls schon ein Knoten mit so einem Namen existiert, werfe eine Exception
                if (graph.ContainsKnoten(name))
                {
                    throw new GraphExceptions.NameAlreadyExistsException();
                }

                //lege die Eigenschaften fest
                this.Parent = graph;
                this.Kanten = kanten.ConvertAll((x) => x.CastToKanten());
                this.Name = name;

                this.Canvas = canvas;
                this.Ellipse = ellipse;
                this.Ellipse.MouseMove += this.MouseMove;
                this.Canvas.MouseMove += this.MouseMove;
                this.Ellipse.MouseDown += Ellipse_MouseDown;
                this.Label = label;

                canvas.Children.Add(ellipse);
                canvas.Children.Add(label);
            }
            #endregion

            public Graph.Graph.Knoten CastToKnoten(Graph.Graph graph)
            {
                return new Graph.Graph.Knoten(graph, this.Kanten, this.Name);
            }

            public Graph.Graph.Knoten CastToKnoten()
            {
                Graph.Graph graph = new();
                return new Graph.Graph.Knoten(graph, this.Kanten, this.Name);
            }

            //Lässt den Knoten verschwinden
            public bool Disappear()
            {
                try
                {
                    this.Canvas.Children.Remove(this.Ellipse);
                    this.Canvas.Children.Remove(this.Label);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            //Event-Methoden, die die Ellipsen bewegen, und deren angrenzende Kanten neu malt
            #region
            private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
            {
                if (e.ClickCount == 1)
                {
                    //Wenn der Knoten gedrückt wird, dann speichere ab, dass das jetzt passiert und bewege die Ellipse, und deren angrenzende Kanten
                    this.IsFocus = true;
                    this.Redraw();
                }
                else if (e.ClickCount == 2)
                {
                    //Wenn der Knoten doppelt gedrückt wird, dann öffne seine Eigenschaften
                    MainWindow.main.OpenedEigenschaftenFenster[MainWindow.main.GetOpenTab()].OpenNode(this);
                }
            }

            private void MouseMove(object sender, MouseEventArgs e)
            {
                //Falls sowohl gerade der Fokus auf ihr ist und die Maus gedrückt, bewege die Ellipse, und deren angrenzende Kanten
                if (this.IsFocus && e.LeftButton == MouseButtonState.Pressed)
                {
                    this.Redraw();
                }
                else
                {
                    //Setzte "IsFocus" auf falsch, für den Fall, dass sie nicht mehr angeklickt wird 
                    this.IsFocus = false;
                }
            }
            #endregion

            //Methode, um den Graph neu malen zu lassen
            public void Redraw()
            {
                //Platziere die Ellipse
                Thickness thickness = new Thickness();
                Point point = Mouse.GetPosition(this.Canvas);
                point.X -= Canvas.GetLeft(this.Ellipse);
                point.Y -= Canvas.GetTop(this.Ellipse);
                thickness.Left = point.X - this.Ellipse.Width / 2;
                thickness.Top = point.Y - this.Ellipse.Height / 2;
                thickness.Bottom = 10;
                thickness.Right = 10;
                this.Ellipse.Margin = thickness;

                //Gebe den Namen auch noch einmal an
                if (this.Label.Content.ToString() != this.Name)
                {
                    this.Label.Content = this.Name;
                }
                this.Label.Margin = new(this.Ellipse.Margin.Left + LabelToRight, this.Ellipse.Margin.Top - LabelToTop, 10, 10);

                foreach (Kanten i in this.Kanten)
                {
                    //Rechne die Position der anliegenden Kanten nach und justiere sie
                    if (i.Line is Ellipse ellipse)
                    {
                        //Für den Fall, dass es eine Schlinge ist
                        //Finde das Margin des Knotens zur Schlinge heraus
                        Thickness knoten = this.Ellipse.Margin;

                        //lege die Position fest
                        ellipse.Margin = new Thickness(knoten.Left - ellipse.Width / 2 - 10, knoten.Top - 5, 10, 10);
                    }
                    else if (i.Line is Line line)
                    {
                        //Für den Fall, dass es eine ganz normale Kante ist
                        //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                        Thickness marginKnoten0 = ((Knoten)i.Knoten[0]).Ellipse.Margin;
                        Thickness marginKnoten1 = ((Knoten)i.Knoten[1]).Ellipse.Margin;
                        double height = this.Ellipse.Height;

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
                    }
                }
            }

            //Methode, um den Namen nachzufahren
            public void RedrawName()
            {
                if (this.Label.Content.ToString() != this.Name)
                {
                    this.Label.Content = this.Name;
                }
            }

            //Methode um einen Visuellen Knoten zu erstellen
            private Ellipse CreateVisualNode()
            {
                //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus
                int index = ((GraphDarstellung)this.Parent).GraphKnoten.Contains(this) ? ((GraphDarstellung)this.Parent).GraphKnoten.IndexOf(this) : ((GraphDarstellung)this.Parent).GraphKnoten.Count;

                //Rückgabe
                return CreateVisualNode((index % 10) * 100 + 20, index / 10 * 100 + 25);
            }

            private Ellipse CreateVisualNode(double x, double y)
            {
                //Lese die Farben in den Einstellungen nach
                LinearGradientBrush knoten_FarbeFilling = new();
                if (Properties.Settings.Default.Transition)
                {
                    knoten_FarbeFilling = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B), Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling2.A, Properties.Settings.Default.Knoten_FarbeFilling2.R, Properties.Settings.Default.Knoten_FarbeFilling2.G, Properties.Settings.Default.Knoten_FarbeFilling2.B), 45);
                }
                else
                {
                    knoten_FarbeFilling = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B), Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B), 45);
                }
                SolidColorBrush knoten_FarbeBorder = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeBorder.A, Properties.Settings.Default.Knoten_FarbeBorder.R, Properties.Settings.Default.Knoten_FarbeBorder.G, Properties.Settings.Default.Knoten_FarbeBorder.B));

                //Lese die Höhen und die Thickness in den Einstellungen nach
                double knoten_Height = Properties.Settings.Default.Knoten_Höhe;
                double knoten_Width = Properties.Settings.Default.Knoten_Breite;
                double knoten_Border_Thickness = Properties.Settings.Default.Knoten_Border_Thickness;

                //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus
                int index = this.Parent.GraphKnoten.Contains(this) ? this.Parent.GraphKnoten.IndexOf(this) : this.Parent.GraphKnoten.Count;

                //Erstelle einen Knoten
                Ellipse ellipse = new();

                //Mache Feinheiten an der Ellipse
                ellipse.Fill = knoten_FarbeFilling;
                ellipse.Stroke = knoten_FarbeBorder;
                ellipse.StrokeThickness = knoten_Border_Thickness;
                ellipse.Height = knoten_Height;
                ellipse.Width = knoten_Width;
                ellipse.Margin = new(x, y, 10, 10);
                ellipse.Cursor = Cursors.Hand;
                ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                ellipse.VerticalAlignment = VerticalAlignment.Top;
                Canvas.SetZIndex(ellipse, 100);
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

                //Füge ein ContextMenu hinzu
                #region
                //ContextMenu "contextMenu"
                ContextMenu contextMenu = new ContextMenu();
                ellipse.ContextMenu = contextMenu;

                //MenuItem zum Anzeigen seiner Eigenschaften
                MenuItem eigenschaften = new();
                eigenschaften.Header = MainWindow.resman.GetString("EigenschaftenKnoten", MainWindow.cul);
                eigenschaften.Click += Eigenschaften_Click;

                //MenuItem zum Löschen des Knoten
                MenuItem löschen = new MenuItem();
                löschen.Header = MainWindow.resman.GetString("LöschenKnoten", MainWindow.cul);
                löschen.Icon = " - ";
                löschen.Click += MainWindow.main.LöschenKnoten_Click;

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
                return ellipse;
            }

            //Methode, die ausgelöst wird, wenn das MenuItem "eigenschaften" gedrückt wird
            private void Eigenschaften_Click(object sender, RoutedEventArgs e)
            {
                //Falls das MenuItem "eigenschaften" geklickt wurde
                MainWindow.main.OpenedEigenschaftenFenster[MainWindow.main.GetOpenTab()].OpenNode(this);
            }

            //Methode, um einen Label zum Knoten zu erstellen
            private Label CreateLabel()
            {
                //Erstelle den Label
                Label label = new();

                //Mache Feinheiten an dem Label
                label.Content = this.Name;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Margin = new(this.Ellipse.Margin.Left + LabelToRight, this.Ellipse.Margin.Top - LabelToTop, 10, 10);
                Canvas.SetZIndex(label, 100);
                if (this.Canvas.Children.Count != 0)
                {
                    Canvas.SetTop(label, Canvas.GetTop(this.Canvas.Children[0]));
                    Canvas.SetLeft(label, Canvas.GetLeft(this.Canvas.Children[0]));
                }
                else
                {
                    Canvas.SetTop(label, 0);
                    Canvas.SetLeft(label, 0);
                }

                return label;
            }
        }
    }
}
