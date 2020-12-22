using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class MainWindow
    {
        //Methoden zur Darstellung der Dateien
        public void OpenFile(string path)
        {
            TabItem tab = new TabItem();
            try
            {
                //erstelle die visuellen Elemente für den Tab
                #region
                //erstelle allgemeines TabControl
                TabControl tabControl = new();
                tabControl.TabStripPlacement = Dock.Bottom;
                tabControl.Background = Brushes.White;

                //erstelle zwei Tabs
                //der Tab für die visuelle Darstellung des Graphen
                TabItem tabGraph = new TabItem();
                tabGraph.Header = "Graph";
                tabGraph.Background = Brushes.White;
                tabControl.Items.Add(tabGraph);
                tabControl.SelectionChanged += Graph_HasToBeRedrawn;

                //der Tab für die Console
                TabItem tabConsole = new TabItem();
                tabConsole.Header = "Console";
                tabConsole.Background = Brushes.White;
                tabControl.Items.Add(tabConsole);


                //erstelle die Elemente für die Konsole
                #region
                //erstelle DockPanel, in das alle visuellen Elemente für die Konsole hereinkommen
                DockPanel dockPanelConsole = new DockPanel();
                dockPanelConsole.LastChildFill = true;

                //Thickness (margin und padding) für Ausgabe-Konsole
                Thickness outputThickness = new Thickness();
                outputThickness.Left = 5;
                outputThickness.Right = 5;
                outputThickness.Top = 5;
                outputThickness.Bottom = 5;

                //Konsolen-Ausgabe (große TextBox)
                System.Windows.Controls.TextBox consoleOutput = new System.Windows.Controls.TextBox();
                consoleOutput.IsReadOnly = true;
                consoleOutput.Margin = outputThickness;
                consoleOutput.Padding = outputThickness;
                consoleOutput.FontFamily = new FontFamily("Consolas");
                consoleOutput.FontSize = 15;

                //Thickness (margin und padding) für Eingabe-Konsole
                Thickness inputThickness = new Thickness();
                inputThickness.Top = 5;
                inputThickness.Bottom = 5;
                inputThickness.Left = 5;
                inputThickness.Right = 5;

                //Konsolen-Eingabe (kleine TextBox)
                System.Windows.Controls.TextBox consoleInput = new System.Windows.Controls.TextBox();
                consoleInput.KeyDown += KeyDown_ConsoleInput;
                consoleInput.AcceptsReturn = false;
                consoleInput.Padding = inputThickness;
                consoleInput.Margin = inputThickness;
                consoleInput.FontFamily = new FontFamily("Consolas");
                consoleInput.FontSize = 15;

                //setze die "Dock"-Eigenschaft für die Elemente
                DockPanel.SetDock(consoleOutput, Dock.Top);
                DockPanel.SetDock(consoleInput, Dock.Bottom);

                //füge die Elemente zum Dockpanel hinzu
                dockPanelConsole.Children.Add(consoleInput);
                dockPanelConsole.Children.Add(consoleOutput);

                //Füge das Dockpanel zum TabItem "tabConsole" hinzu
                tabConsole.Content = dockPanelConsole;
                #endregion


                //erstelle die Elemente für die Graph-Visualisierung
                #region
                //erstelle Canvas für den Graphen
                Canvas graphCanvas = new Canvas();

                //lege Eigenschaften für ihn fest
                //MenuItems
                #region
                //MenuItem zur Bearbeitung von Graph
                System.Windows.Controls.MenuItem menuItem1 = new System.Windows.Controls.MenuItem();
                menuItem1.Header = resman.GetString("GraphBearbeiten", cul);

                //MenuItem zum Hinzufügen von Kanten
                System.Windows.Controls.MenuItem menuItem2 = new System.Windows.Controls.MenuItem();
                menuItem2.Header = resman.GetString("KanteHinzufügen", cul);
                menuItem2.Icon = " + ";
                menuItem2.Click += KanteHinzufügen_Click;

                //MenuItem zum Hinzufügen von Knoten
                System.Windows.Controls.MenuItem menuItem3 = new System.Windows.Controls.MenuItem();
                menuItem3.Header = resman.GetString("KnotenHinzufügen", cul);
                menuItem3.Icon = " + ";
                menuItem3.Click += KnotenHinzufügen_Click;

                //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                menuItem1.Items.Add(menuItem2);
                menuItem1.Items.Add(menuItem3);

                //MenuItem zum Öffnen des Eiganschaften-Fensters
                MenuItem menuItem4 = new();
                menuItem4.Click += EigenschaftenFenster_Click;
                menuItem4.Header = resman.GetString("EigenschaftenFenster", cul);

                //Füge die MenuItems hinzu
                graphCanvas.ContextMenu = new System.Windows.Controls.ContextMenu();
                graphCanvas.ContextMenu.Items.Add(menuItem1);
                graphCanvas.ContextMenu.Items.Add(menuItem4);
                #endregion

                //Eigene Darstellung
                #region
                graphCanvas.Background = Brushes.White;
                graphCanvas.Margin = new Thickness(5, 5, 5, 5);
                #endregion

                //füge ihn zum TabItem hinzu
                tabGraph.Content = graphCanvas;
                #endregion

                //erstelle einen neuen Tab
                tab = AddNewTab<System.Windows.Controls.TabControl>(path.Substring(path.LastIndexOf(@"\") + 1), tabControl);

                //fokusiere auf das Eingabe-Feld
                consoleInput.Focus();
                #endregion

                //erstelle neuen Graph und füge eine CommandConsole hinzu
                #region
                //erstelle den Graphen, indem die Datei ausgelesen wird
                Graph.Graph graph = new();
                try
                {
                    graph = CommandConsole.TransformFileToGraph(path);
                }
                catch
                {
                    MessageBox.Show(resman.GetString("FehlermeldungBeschädigteDatei", cul));
                }

                //erstelle die CommandConsole zum Graph
                CommandConsole commandConsole = new CommandConsole(graph, consoleOutput, path, this, tab);
                #endregion

                //stelle die Knoten des Graphen im Canvas "graphCanvas" dar, die Kanten werden später noch dargestellt durch ein Event
                #region
                //erstelle Liste für die Knoten
                List<GraphDarstellung.KnotenDarstellung> knotenDarstellung = new List<GraphDarstellung.KnotenDarstellung>();

                //erstelle die Liste für die Kanten
                List<GraphDarstellung.KantenDarstellung> kantenDarstellung = new List<GraphDarstellung.KantenDarstellung>();
                #endregion

                //speichere in der Liste ab, dass diese Datei gerade geöffnet ist und speichere auch ab, welche TextBox hier der Output ist, und welche Input, sowie die Commands
                #region
                this.OpenedFiles.Add(tab, path);
                this.Outputs.Add(tab, consoleOutput);
                this.Inputs.Add(tab, consoleInput);
                this.Consoles.Add(tab, commandConsole);
                this.Canvases.Add(tab, graphCanvas);
                this.Graphs.Add(tab, new GraphDarstellung() { graph = graph, visuelleKanten = kantenDarstellung, visuelleKnoten = knotenDarstellung });//erstelle einen neuen "GraphDarstellungen" und gebe ihm den Graphen und die visuellen Kanten und Knoten hinzu
                #endregion

                //Finalisierung
                #region
                this.TabControl.SelectedIndex = this.TabControl.Items.Count - 1;
                SaveOpenedFiles();
                SaveAll();
                #endregion
            }
            catch (Exception e)
            {
                //Mache einen ErrorSound, zeige die Error-Nachricht und entferne den Tab
                SystemSounds.Asterisk.Play();
                this.TabControl.Items.Remove(tab);
                MessageBox.Show(e.Message);
            }
        }

        public void DrawGraph(Canvas graphCanvas)
        {
            /*
             * Diese Methode soll die Kanten im Canvas nachfahren.
             * Dazu muss sie:
             * 1. Graph herausfinden
             * 2. die Kanten erstellen
             * 3. die Kanten darstellen
             */

            #region
            //finde den Graphen heraus und lege andere Variablen fest
            GraphDarstellung graphDarstellung = GetOpenGraphDarstellung();
            Graph.Graph graph = graphDarstellung.graph;
            List<GraphDarstellung.KantenDarstellung> kantenDarstellung = graphDarstellung.visuelleKanten;
            List<GraphDarstellung.KnotenDarstellung> knotenDarstellung = graphDarstellung.visuelleKnoten;

            //entferne die alten "KnotenDarstellung"
            List<GraphDarstellung.KnotenDarstellung> entfernteKnoten = new();
            foreach (GraphDarstellung.KnotenDarstellung i in knotenDarstellung)
            {
                if (!graph.ContainsKnoten(i.Knoten))
                {
                    entfernteKnoten.Add(i);
                }
            }
            foreach (GraphDarstellung.KnotenDarstellung i in entfernteKnoten)
            {
                knotenDarstellung.Remove(i);
                graphCanvas.Children.Remove(i.Ellipse);
                graphCanvas.Children.Remove(i.Label);
            }

            //erstelle die neuen "KnotenDarstellung"

            //porvisorische Liste, damit die Knoten nach der Errstellung der Kanten im Canvas "graphCanvas" eingefügt werden können
            List<GraphDarstellung.KnotenDarstellung> neueKnoten = new();
            foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
            {
                //Finde heraus, ob zu diesem Knoten "i" schon eine visuelle Darstellung existiert
                bool exists = false;
                foreach (GraphDarstellung.KnotenDarstellung n in knotenDarstellung)
                {
                    if (n.Knoten == i)
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    //Finde den Index (für die Position) von dem Knoten in "graph.GraphKnoten" heraus
                    int index = graph.GraphKnoten.IndexOf(i);

                    //Erstelle einen Label und einen KnotenDarstellung
                    Label label = new();
                    GraphDarstellung.KnotenDarstellung knoten = new(i, new Ellipse(), label, graphCanvas);

                    //Füge die KnotenDarstellung "knoten" zu Listen hinzu
                    neueKnoten.Add(knoten);
                    knotenDarstellung.Add(knoten);

                    //Mache Feinheiten an der Ellipse
                    knoten.Ellipse.Fill = new SolidColorBrush(Color.FromRgb(Pollux.Properties.Settings.Default.KnotenFarbe.R, Pollux.Properties.Settings.Default.KnotenFarbe.G, Pollux.Properties.Settings.Default.KnotenFarbe.B));
                    knoten.Ellipse.Stroke = Brushes.Black;
                    knoten.Ellipse.StrokeThickness = 2;
                    knoten.Ellipse.Width = 30;
                    knoten.Ellipse.Height = 30;
                    knoten.Ellipse.Margin = new((index % 15) * 100 + 10, Convert.ToInt32(index / 15) * 100 + 10, 10, 10);

                    //Mache Feinheiten an dem Label
                    label.Content = i.Name;
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Top;
                    label.Margin = new(knoten.Ellipse.Margin.Left + GraphDarstellung.KnotenDarstellung.LabelToRight, knoten.Ellipse.Margin.Top - GraphDarstellung.KnotenDarstellung.LabelToTop, 10, 10);
                }
            }

            //richte die Position der neuen Kanten neu aus
            List<GraphDarstellung.KantenDarstellung> entfernteKanten = new();
            foreach (GraphDarstellung.KantenDarstellung i in kantenDarstellung)
            {
                if (graph.ContainsKanten(i.Kante))
                {
                    //Gucke, ob das Element "i.Line" eine Line ist oder eine Ellipse, also eine ganz normale Kante darstellt oder eine Schlinge
                    Line line = new Line() { Fill = Brushes.Transparent };
                    Ellipse ellipse = new Ellipse() { Fill = Brushes.Transparent };
                    switch (i.Line)
                    {
                        case Ellipse ellipse1: ellipse = ellipse1; break;
                        case Line line1: line = line1; break;
                    }

                    //Rechne die Position nach und justiere sie
                    if (line.Fill == Brushes.Transparent)
                    {
                        //Für den Fall, dass es eine Schlinge ist
                        //Finde das Margin des Knotens zur Schlinge heraus
                        Thickness knoten = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Kante.Knoten[0])].Ellipse.Margin;

                        //lege die Position fest
                        ellipse.Margin = new Thickness(knoten.Left - ellipse.Width / 2 - 10, knoten.Top - 5, 10, 10);

                        graphCanvas.Children.Add(ellipse);
                    }
                    else
                    {
                        //Für den Fall, dass es eine ganz normale Kante ist
                        //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                        Thickness marginKnoten0 = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Kante.Knoten[0])].Ellipse.Margin;
                        Thickness marginKnoten1 = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Kante.Knoten[1])].Ellipse.Margin;
                        double height = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Kante.Knoten[0])].Ellipse.Height;

                        //finde dadurch die Position heraus, wo die Kante starten und enden muss
                        double x = (marginKnoten1.Left - marginKnoten0.Left) * 0.1;
                        x = (x > 30) ? 30 : (x < -30) ? -30 : x;
                        double y = (marginKnoten1.Top - marginKnoten0.Top) * 0.1;
                        y = (y > 30) ? 30 : (y < -30) ? -30 : y;

                        //schreibe diese Eigenschaften in die Linie
                        line.X1 = marginKnoten0.Left + height / 2 + x;
                        line.Y1 = marginKnoten0.Top + height / 2 + y;
                        line.X2 = marginKnoten1.Left + height / 2 - x;
                        line.Y2 = marginKnoten1.Top + height / 2 - y;
                    }
                }
                else
                {
                    //Falls einer die Kante gar nicht mehr zum Graphen hinzu gehört, entferne sie, indem sie zu der Liste "entfernteKanten" hinzugefügt wird
                    entfernteKanten.Add(i);
                }
            }

            foreach (GraphDarstellung.KantenDarstellung i in entfernteKanten)
            {
                //Falls einer die Kante gar nicht mehr zum Graphen hinzu gehört, entferne sie aus dem Canvas "graphCanvas" und aus dem GraphDarstellung
                graphCanvas.Children.Remove(i.Line);
                kantenDarstellung.Remove(i);
            }

            //erstelle die neuen Kanten
            for (int i = kantenDarstellung.Count; i < graph.GraphKanten.Count; i++)
            {
                Pollux.Graph.Graph.Kanten kante = graph.GraphKanten[i];
                if (kante.Knoten[0] == kante.Knoten[1])
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    Ellipse ellipse = new Ellipse();

                    //Erstelle die visuelle Kante "kanten"
                    GraphDarstellung.KantenDarstellung kanten = new GraphDarstellung.KantenDarstellung() { Kante = kante, Line = ellipse };

                    //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten = knotenDarstellung[graph.GraphKnoten.IndexOf(kante.Knoten[0])].Ellipse.Margin;

                    //lege eine Konstante für die Höhe fest
                    const int height = 40;

                    //Lege noch andere Werte der Linie fest
                    ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    ellipse.VerticalAlignment = VerticalAlignment.Bottom;
                    ellipse.Stroke = Brushes.Black;
                    ellipse.StrokeThickness = 2;
                    ellipse.Fill = Brushes.Transparent;
                    ellipse.Height = height;
                    ellipse.Width = height;

                    //lege die Position fest
                    ellipse.Margin = new Thickness(marginKnoten.Left - ellipse.Width / 2 - 10, marginKnoten.Top - 5, 10, 10);

                    //füge die Linie zu der Liste "kantenDarstellung" hinzu
                    kantenDarstellung.Add(kanten);

                    //Füge es zum Canvas hinzu
                    graphCanvas.Children.Add(kanten.Line);
                }
                else
                {
                    //erstelle die Linie, die nachher dargestellt werden soll
                    Line line = new Line();

                    //Erstelle die visuelle Kante "kanten"
                    GraphDarstellung.KantenDarstellung kanten = new GraphDarstellung.KantenDarstellung() { Kante = kante, Line = line };

                    //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                    Thickness marginKnoten0 = knotenDarstellung[graph.GraphKnoten.IndexOf(kante.Knoten[0])].Ellipse.Margin;
                    Thickness marginKnoten1 = knotenDarstellung[graph.GraphKnoten.IndexOf(kante.Knoten[1])].Ellipse.Margin;
                    double height = knotenDarstellung[graph.GraphKnoten.IndexOf(kante.Knoten[0])].Ellipse.Height;

                    //finde dadurch die Position heraus, wo die Kante starten und enden muss
                    double x = (marginKnoten1.Left - marginKnoten0.Left) * 0.1;
                    x = (x > 30) ? 30 : (x < -30) ? -30 : x;
                    double y = (marginKnoten1.Top - marginKnoten0.Top) * 0.1;
                    y = (y > 30) ? 30 : (y < -30) ? -30 : y;

                    //schreibe diese Eigenschaften in die Linie
                    line.X1 = marginKnoten0.Left + height / 2 + x;
                    line.Y1 = marginKnoten0.Top + height / 2 + y;
                    line.X2 = marginKnoten1.Left + height / 2 - x;
                    line.Y2 = marginKnoten1.Top + height / 2 - y;

                    //Lege noch andere Werte der Linie fest
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.VerticalAlignment = VerticalAlignment.Bottom;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 2;

                    //füge die Linie zu der Liste "kantenDarstellung" hinzu
                    kantenDarstellung.Add(kanten);

                    //Füge es zum Canvas hinzu
                    graphCanvas.Children.Add(kanten.Line);
                }
            }

            //stelle Knoten im Canvas "graphCanvas" dar; Kanten müssen nicht dargestellt werden, da eben schon passiert
            foreach (GraphDarstellung.KnotenDarstellung i in neueKnoten)
            {
                graphCanvas.Children.Add(i.Ellipse);
                graphCanvas.Children.Add(i.Label);
            }
            #endregion
        }

        public void DrawGraph()
        {
            DrawGraph(GetOpenCanvas());
        }

        public TabItem AddNewTab<T>(string header, T content)
        {
            //Füge neuen Tab hinzu
            TabItem tab1 = new TabItem();

            //Füge Content hinzu
            tab1.Content = content;

            //erstelle Header
            DockPanel headerPanel = new DockPanel();

            //Füge TextBlock zum Header hinzu
            TextBlock Title = new TextBlock();
            Title.Text = header;
            headerPanel.Children.Add(Title);

            //Füge Button zum Schließen zum Header hinzu
            Button closeButton = new();
            closeButton.Content = "x";
            closeButton.Background = Brushes.Transparent;
            closeButton.BorderBrush = Brushes.Transparent;
            closeButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            closeButton.VerticalAlignment = VerticalAlignment.Top;
            closeButton.Width = 20;
            closeButton.Height = 20;
            closeButton.ToolTip = resman.GetString("TabSchließen", cul);
            Thickness thickness = new Thickness();
            thickness.Top = 0;
            thickness.Right = 0;
            thickness.Left = 8;
            closeButton.Margin = thickness;
            closeButton.FontSize = 10;
            closeButton.Click += CloseTab;
            headerPanel.Children.Add(closeButton);

            //Eigenschaften für das TabItem "tab1"
            tab1.Header = headerPanel;
            tab1.Background = Brushes.White;

            //Füge den Header zum Element "Headers" hinzu
            this.Headers.Add(tab1, Title);

            //Füge Tab zu "TabControl" hinzu
            this.TabControl.Items.Add(tab1);

            //Rückgabe von diesem Tab
            return tab1;
        }
    }
}
