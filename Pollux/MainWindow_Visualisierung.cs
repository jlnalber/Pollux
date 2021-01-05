﻿using System;
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
                tabControl.SelectionChanged += this.Graph_HasToBeRedrawn;

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
                TextBox consoleInput = new();
                consoleInput.KeyDown += this.KeyDown_ConsoleInput;
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
                //DockPanel "dockPanel", in dem sich alle Elemente befinden werden
                DockPanel dockPanel = new DockPanel();

                //Grid "grid" für die Eigenschaften (wird später zugewiesen)
                Grid grid = new();

                //erstelle Canvas für den Graphen
                Canvas graphCanvas = new Canvas();
                DockPanel.SetDock(graphCanvas, Dock.Left);

                //lege Eigenschaften für ihn fest
                //MenuItems
                //MenuItem zur Bearbeitung von Graph
                MenuItem menuItem1 = new();
                menuItem1.Header = resman.GetString("GraphBearbeiten", cul);

                //MenuItem zum Hinzufügen von Kanten
                MenuItem menuItem2 = new();
                menuItem2.Header = resman.GetString("KanteHinzufügen", cul);
                menuItem2.Icon = " + ";
                menuItem2.Click += this.KanteHinzufügen_Click;

                //MenuItem zum Hinzufügen von Knoten
                MenuItem menuItem3 = new();
                menuItem3.Header = resman.GetString("KnotenHinzufügen", cul);
                menuItem3.Icon = " + ";
                menuItem3.Click += this.KnotenHinzufügen_Click;

                //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                menuItem1.Items.Add(menuItem2);
                menuItem1.Items.Add(menuItem3);

                //MenuItem zum Öffnen des Eiganschaften-Fensters
                MenuItem menuItem4 = new();
                menuItem4.Click += this.EigenschaftenFenster_Click;
                menuItem4.Header = resman.GetString("EigenschaftenFenster", cul);

                //Füge die MenuItems hinzu
                graphCanvas.ContextMenu = new ContextMenu();
                graphCanvas.ContextMenu.Items.Add(menuItem1);
                graphCanvas.ContextMenu.Items.Add(menuItem4);

                //Eigene Darstellung
                graphCanvas.Background = Brushes.White;
                graphCanvas.Margin = new Thickness(5, 5, 5, 5);

                //füge ihn zum TabItem hinzu
                tabGraph.Content = dockPanel;
                #endregion

                //erstelle einen neuen Tab
                tab = this.AddNewTab(path.Substring(path.LastIndexOf(@"\") + 1), tabControl);

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
                    System.Windows.MessageBox.Show(resman.GetString("FehlermeldungBeschädigteDatei", cul));
                }

                //Erstelle einen GraphDarstellung "graphDarstellung"
                GraphDarstellung graphDarstellung = new();
                graphDarstellung.graph = graph;

                //erstelle die CommandConsole zum Graph
                CommandConsole commandConsole = new CommandConsole(graph, graphDarstellung, consoleOutput, path, this, tab);
                #endregion

                //Erstelle weitere visuelle Elemente
                #region
                Show show = new Show(graphDarstellung, commandConsole);
                grid = show.ContentGrid;
                show.Content = new Grid();
                grid.Width = 400;
                DockPanel.SetDock(grid, Dock.Right);
                dockPanel.Children.Add(grid);
                show.Close();

                dockPanel.Children.Add(graphCanvas);
                #endregion

                //stelle die Knoten des Graphen im Canvas "graphCanvas" dar, die Kanten werden später noch dargestellt durch ein Event
                #region
                //erstelle Liste für die Knoten
                List<GraphDarstellung.KnotenDarstellung> knotenDarstellung = new List<GraphDarstellung.KnotenDarstellung>();
                graphDarstellung.visuelleKnoten = knotenDarstellung;

                //erstelle die Liste für die Kanten
                List<GraphDarstellung.KantenDarstellung> kantenDarstellung = new List<GraphDarstellung.KantenDarstellung>();
                graphDarstellung.visuelleKanten = kantenDarstellung;
                #endregion

                //speichere in der Liste ab, dass diese Datei gerade geöffnet ist und speichere auch ab, welche TextBox hier der Output ist, und welche Input, sowie die Commands
                #region
                this.OpenedFiles.Add(tab, path);
                this.Outputs.Add(tab, consoleOutput);
                this.Inputs.Add(tab, consoleInput);
                this.Consoles.Add(tab, commandConsole);
                this.Canvases.Add(tab, graphCanvas);
                this.Graphs.Add(tab, graph);
                this.GraphDarstellungen.Add(tab, graphDarstellung);
                this.OpenedEigenschaftenFenster.Add(tab, show);
                this.OpenedEigenschaftenFensterGrid.Add(tab, grid);
                #endregion

                //Finalisierung
                #region
                this.TabControl.SelectedIndex = this.TabControl.Items.Count - 1;
                this.SaveOpenedFiles();
                this.SaveAll();
                #endregion
            }
            catch (Exception e)
            {
                //Mache einen ErrorSound, zeige die Error-Nachricht und entferne den Tab
                SystemSounds.Asterisk.Play();
                #region
                this.TabControl.Items.Remove(tab);
                this.OpenedFiles.Remove(tab);
                this.Outputs.Remove(tab);
                this.Inputs.Remove(tab);
                this.Consoles.Remove(tab);
                this.Canvases.Remove(tab);
                this.Graphs.Remove(tab);
                this.GraphDarstellungen.Remove(tab);
                this.OpenedEigenschaftenFenster.Remove(tab);
                this.OpenedEigenschaftenFensterGrid.Remove(tab);
                System.Windows.MessageBox.Show(e.Message);
                #endregion
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
            GraphDarstellung graphDarstellung = this.GetOpenGraphDarstellung();
            Graph.Graph graph = graphDarstellung.graph;
            List<GraphDarstellung.KantenDarstellung> kantenDarstellung = graphDarstellung.visuelleKanten;
            List<GraphDarstellung.KnotenDarstellung> knotenDarstellung = graphDarstellung.visuelleKnoten;

            //Lese die Farben in den Einstellungen nach
            SolidColorBrush knoten_FarbeFilling = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B));
            SolidColorBrush knoten_FarbeBorder = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeBorder.A, Properties.Settings.Default.Knoten_FarbeBorder.R, Properties.Settings.Default.Knoten_FarbeBorder.G, Properties.Settings.Default.Knoten_FarbeBorder.B));
            SolidColorBrush kanten_FarbeBorder = new(Color.FromArgb(Properties.Settings.Default.Kante_FarbeBorder.A, Properties.Settings.Default.Kante_FarbeBorder.R, Properties.Settings.Default.Kante_FarbeBorder.G, Properties.Settings.Default.Kante_FarbeBorder.B));

            //Lese die Höhen und die Thickness in den Einstellungen nach
            double knoten_Height = Properties.Settings.Default.Knoten_Höhe;
            double knoten_Width = Properties.Settings.Default.Knoten_Breite;
            double knoten_Border_Thickness = Properties.Settings.Default.Knoten_Border_Thickness;
            double kanten_Thickness = Properties.Settings.Default.Kanten_Thickness;

            //entferne die alten "KnotenDarstellung"
            List<GraphDarstellung.KnotenDarstellung> entfernteKnoten = new();
            foreach (GraphDarstellung.KnotenDarstellung i in knotenDarstellung)
            {
                if (!graph.ContainsKnoten(i.Knoten))
                {
                    entfernteKnoten.Add(i);
                }
                else
                {
                    //Fahre noch mal die Farbe nach
                    i.Ellipse.Fill = knoten_FarbeFilling;
                    i.Ellipse.Stroke = knoten_FarbeBorder;

                    //und gebe noch einmal die Dicke, Höhe und Breite an
                    if (i.Ellipse.Width != knoten_Width | i.Ellipse.Height != knoten_Height | i.Ellipse.StrokeThickness != knoten_Border_Thickness)
                    {
                        i.Ellipse.Width = knoten_Width;
                        i.Ellipse.Height = knoten_Height;
                        i.Ellipse.StrokeThickness = knoten_Border_Thickness;
                    }

                    //Gebe den Namen auch noch einmal an
                    if (i.Label.Content.ToString() != i.Knoten.Name)
                    {
                        i.Label.Content = i.Knoten.Name;
                    }
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
                    knoten.Ellipse.Fill = knoten_FarbeFilling;
                    knoten.Ellipse.Stroke = knoten_FarbeBorder;
                    knoten.Ellipse.StrokeThickness = knoten_Border_Thickness;
                    knoten.Ellipse.Height = knoten_Height;
                    knoten.Ellipse.Width = knoten_Width;
                    knoten.Ellipse.Margin = new((index % 10) * 100 + 10, Convert.ToInt32(index / 10) * 100 + 10, 10, 10);
                    knoten.Ellipse.Cursor = System.Windows.Input.Cursors.Hand;

                    //Mache Feinheiten an dem Label
                    label.Content = i.Name;
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Top;
                    label.Margin = new(knoten.Ellipse.Margin.Left + GraphDarstellung.KnotenDarstellung.LabelToRight, knoten.Ellipse.Margin.Top - GraphDarstellung.KnotenDarstellung.LabelToTop, 10, 10);

                    //Füge ein ContextMenu hinzu
                    #region
                    //ContextMenu "contextMenu"
                    ContextMenu contextMenu = new ContextMenu();
                    knoten.Ellipse.ContextMenu = contextMenu;

                    //MenuItem zum Löschen des Knoten
                    MenuItem löschen = new MenuItem();
                    löschen.Header = resman.GetString("LöschenKnoten", cul);
                    löschen.Icon = " - ";
                    löschen.Click += this.LöschenKnoten_Click;

                    //MenuItem zur Bearbeitung von Graph
                    MenuItem menuItem1 = new();
                    menuItem1.Header = resman.GetString("GraphBearbeiten", cul);

                    //MenuItem zum Hinzufügen von Kanten
                    MenuItem menuItem2 = new();
                    menuItem2.Header = resman.GetString("KanteHinzufügen", cul);
                    menuItem2.Icon = " + ";
                    menuItem2.Click += this.KanteHinzufügen_Click;

                    //MenuItem zum Hinzufügen von Knoten
                    MenuItem menuItem3 = new();
                    menuItem3.Header = resman.GetString("KnotenHinzufügen", cul);
                    menuItem3.Icon = " + ";
                    menuItem3.Click += this.KnotenHinzufügen_Click;

                    //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                    menuItem1.Items.Add(menuItem2);
                    menuItem1.Items.Add(menuItem3);

                    //MenuItem zum Öffnen des Eiganschaften-Fensters
                    MenuItem menuItem4 = new();
                    menuItem4.Click += this.EigenschaftenFenster_Click;
                    menuItem4.Header = resman.GetString("EigenschaftenFenster", cul);

                    //Füge alle MenuItems zum ContextMenu "contextMenu" hinzu
                    contextMenu.Items.Add(löschen);
                    contextMenu.Items.Add(new Separator());
                    contextMenu.Items.Add(menuItem1);
                    contextMenu.Items.Add(menuItem4);
                    #endregion
                }
            }

            //richte die Position der Kanten neu aus
            List<GraphDarstellung.KantenDarstellung> entfernteKanten = new();
            foreach (GraphDarstellung.KantenDarstellung i in kantenDarstellung)
            {
                if (graph.ContainsKanten(i.Kante))
                {
                    //Gucke, ob das Element "i.Line" eine Line ist oder eine Ellipse, also eine ganz normale Kante darstellt oder eine Schlinge
                    Line line = new Line();
                    Ellipse ellipse = new Ellipse();
                    bool isLine = true;
                    switch (i.Line)
                    {
                        case Ellipse ellipse1: ellipse = ellipse1; isLine = false; break;
                        case Line line1: line = line1; isLine = true; break;
                    }

                    //Rechne die Position nach und justiere sie
                    if (!isLine)
                    {
                        //Für den Fall, dass es eine Schlinge ist
                        //Finde das Margin des Knotens zur Schlinge heraus
                        Thickness knoten = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Kante.Knoten[0])].Ellipse.Margin;

                        //lege die Position fest
                        ellipse.Margin = new Thickness(knoten.Left - ellipse.Width / 2 - 10, knoten.Top - 5, 10, 10);

                        //Fahre die Farbe nach
                        ellipse.Stroke = kanten_FarbeBorder;

                        //Gebe noch einmal die Dicke der Border an
                        ellipse.StrokeThickness = kanten_Thickness;
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

                        //Fahre die Farbe nach
                        line.Stroke = kanten_FarbeBorder;

                        //Gebe noch einmal die Dicke der Border an
                        line.StrokeThickness = kanten_Thickness;

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
            //porvisorische Liste, damit die Knoten nach der Errstellung der Kanten im Canvas "graphCanvas" eingefügt werden können
            List<GraphDarstellung.KantenDarstellung> neueKanten = new();
            foreach (Graph.Graph.Kanten i in graph.GraphKanten)
            {
                //Finde heraus, ob zu dieser Kante "i" schon eine visuelle Darstellung existiert
                bool exists = false;
                foreach (GraphDarstellung.KantenDarstellung n in kantenDarstellung)
                {
                    if (n.Kante == i)
                    {
                        exists = true;
                    }
                }

                if (!exists)
                {
                    if (i.Knoten[0] == i.Knoten[1])
                    {
                        //erstelle die Linie, die nachher dargestellt werden soll
                        Ellipse ellipse = new Ellipse();

                        //Erstelle die visuelle Kante "kanten"
                        GraphDarstellung.KantenDarstellung kante = new GraphDarstellung.KantenDarstellung() { Kante = i, Line = ellipse };

                        //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                        Thickness marginKnoten = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Knoten[0])].Ellipse.Margin;

                        //lege eine Konstante für die Höhe fest
                        const int height = 40;

                        //Lege noch andere Werte der Linie fest
                        ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        ellipse.VerticalAlignment = VerticalAlignment.Bottom;
                        ellipse.Stroke = kanten_FarbeBorder;
                        ellipse.StrokeThickness = kanten_Thickness;
                        ellipse.Fill = Brushes.Transparent;
                        ellipse.Height = height;
                        ellipse.Width = height;

                        //lege die Position fest
                        ellipse.Margin = new Thickness(marginKnoten.Left - ellipse.Width / 2 - 10, marginKnoten.Top - 5, 10, 10);

                        //füge die Linie zu der Liste "kantenDarstellung" hinzu
                        kantenDarstellung.Add(kante);

                        //Füge es zur Liste "neueKanten" hinuz
                        neueKanten.Add(kante);

                        //Füge ein ContextMenu hinzu
                        #region
                        //ContextMenu "contextMenu"
                        ContextMenu contextMenu = new ContextMenu();
                        ellipse.ContextMenu = contextMenu;

                        //MenuItem zum Löschen des Knoten
                        MenuItem löschen = new MenuItem();
                        löschen.Header = resman.GetString("LöschenKante", cul);
                        löschen.Icon = " - ";
                        löschen.Click += this.LöschenKante_Click;

                        //MenuItem zur Bearbeitung von Graph
                        MenuItem menuItem1 = new();
                        menuItem1.Header = resman.GetString("GraphBearbeiten", cul);

                        //MenuItem zum Hinzufügen von Kanten
                        MenuItem menuItem2 = new();
                        menuItem2.Header = resman.GetString("KanteHinzufügen", cul);
                        menuItem2.Icon = " + ";
                        menuItem2.Click += this.KanteHinzufügen_Click;

                        //MenuItem zum Hinzufügen von Knoten
                        MenuItem menuItem3 = new();
                        menuItem3.Header = resman.GetString("KnotenHinzufügen", cul);
                        menuItem3.Icon = " + ";
                        menuItem3.Click += this.KnotenHinzufügen_Click;

                        //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                        menuItem1.Items.Add(menuItem2);
                        menuItem1.Items.Add(menuItem3);

                        //MenuItem zum Öffnen des Eiganschaften-Fensters
                        MenuItem menuItem4 = new();
                        menuItem4.Click += this.EigenschaftenFenster_Click;
                        menuItem4.Header = resman.GetString("EigenschaftenFenster", cul);

                        //Füge alle MenuItems zum ContextMenu "contextMenu" hinzu
                        contextMenu.Items.Add(löschen);
                        contextMenu.Items.Add(new Separator());
                        contextMenu.Items.Add(menuItem1);
                        contextMenu.Items.Add(menuItem4);
                        #endregion
                    }
                    else
                    {
                        //erstelle die Linie, die nachher dargestellt werden soll
                        Line line = new Line();

                        //Erstelle die visuelle Kante "kanten"
                        GraphDarstellung.KantenDarstellung kante = new GraphDarstellung.KantenDarstellung() { Kante = i, Line = line };

                        //Finde die Margins der Knoten heraus, mit dem die Kante verbunden ist
                        Thickness marginKnoten0 = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Knoten[0])].Ellipse.Margin;
                        Thickness marginKnoten1 = knotenDarstellung[graph.GraphKnoten.IndexOf(i.Knoten[1])].Ellipse.Margin;
                        double height = knoten_Height;

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
                        line.Stroke = kanten_FarbeBorder;
                        line.StrokeThickness = kanten_Thickness;

                        //füge die Linie zu der Liste "kantenDarstellung" hinzu
                        kantenDarstellung.Add(kante);

                        //Füge es zur Liste "neueKanten" hinzu
                        neueKanten.Add(kante);

                        //Füge ein ContextMenu hinzu
                        #region
                        //ContextMenu "contextMenu"
                        ContextMenu contextMenu = new ContextMenu();
                        line.ContextMenu = contextMenu;

                        //MenuItem zum Löschen des Knoten
                        MenuItem löschen = new MenuItem();
                        löschen.Header = resman.GetString("LöschenKante", cul);
                        löschen.Icon = " - ";
                        löschen.Click += this.LöschenKante_Click;

                        //MenuItem zur Bearbeitung von Graph
                        MenuItem menuItem1 = new();
                        menuItem1.Header = resman.GetString("GraphBearbeiten", cul);

                        //MenuItem zum Hinzufügen von Kanten
                        MenuItem menuItem2 = new();
                        menuItem2.Header = resman.GetString("KanteHinzufügen", cul);
                        menuItem2.Icon = " + ";
                        menuItem2.Click += this.KanteHinzufügen_Click;

                        //MenuItem zum Hinzufügen von Knoten
                        MenuItem menuItem3 = new();
                        menuItem3.Header = resman.GetString("KnotenHinzufügen", cul);
                        menuItem3.Icon = " + ";
                        menuItem3.Click += this.KnotenHinzufügen_Click;

                        //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
                        menuItem1.Items.Add(menuItem2);
                        menuItem1.Items.Add(menuItem3);

                        //MenuItem zum Öffnen des Eiganschaften-Fensters
                        MenuItem menuItem4 = new();
                        menuItem4.Click += this.EigenschaftenFenster_Click;
                        menuItem4.Header = resman.GetString("EigenschaftenFenster", cul);

                        //Füge alle MenuItems zum ContextMenu "contextMenu" hinzu
                        contextMenu.Items.Add(löschen);
                        contextMenu.Items.Add(new Separator());
                        contextMenu.Items.Add(menuItem1);
                        contextMenu.Items.Add(menuItem4);
                        #endregion
                    }
                }
            }

            //stelle Kanten im Canvas "graphCanvas" dar
            foreach (GraphDarstellung.KantenDarstellung i in neueKanten)
            {
                graphCanvas.Children.Add(i.Line);
            }

            //stelle Knoten im Canvas "graphCanvas" dar
            foreach (GraphDarstellung.KnotenDarstellung i in neueKnoten)
            {
                graphCanvas.Children.Add(i.Ellipse);
                graphCanvas.Children.Add(i.Label);
            }
            #endregion
        }

        public void DrawGraph()
        {
            this.DrawGraph(this.GetOpenCanvas());
        }

        public TabItem AddNewTab<T>(string header, T content)
        {
            //Füge neuen Tab hinzu
            TabItem tab1 = new TabItem();

            //Füge Content hinzu
            tab1.Content = content;

            //erstelle Header
            System.Windows.Controls.DockPanel headerPanel = new System.Windows.Controls.DockPanel();

            //Füge TextBlock zum Header hinzu
            TextBlock Title = new TextBlock();
            Title.Text = header;
            headerPanel.Children.Add(Title);

            //Füge Button zum Schließen zum Header hinzu
            System.Windows.Controls.Button closeButton = new();
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
            closeButton.Click += this.CloseTab;
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

        public void AktualisiereEigenschaftenFenster(TabItem tabItem)
        {
            //Aktualisiere das Eigenschaften-Fenster
            this.OpenedEigenschaftenFenster[tabItem].AktualisiereGrid();
        }

        public void AktualisiereEigenschaftenFenster()
        {
            //Aktualisiere das Eigenschaften-Fenster, die Daten + Rückgabe
            this.AktualisiereEigenschaftenFenster(this.GetOpenTab());
        }
    }
}
