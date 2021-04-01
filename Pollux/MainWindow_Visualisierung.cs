using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                //tabControl.SelectionChanged += this.Graph_HasToBeRedrawn;

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

                //FontFamily "font"
                FontFamily font = new(Properties.Settings.Default.FontConsole);

                //Konsolen-Ausgabe (große TextBox)
                System.Windows.Controls.TextBox consoleOutput = new System.Windows.Controls.TextBox();
                consoleOutput.IsReadOnly = true;
                consoleOutput.Margin = outputThickness;
                consoleOutput.Padding = outputThickness;
                consoleOutput.FontFamily = font;
                consoleOutput.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                consoleOutput.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                consoleOutput.FontSize = Properties.Settings.Default.FontSizeConsole;

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
                consoleInput.FontFamily = font;
                consoleInput.FontSize = Properties.Settings.Default.FontSizeConsole;

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
                Grid dockPanel = new Grid();
                ColumnDefinition columnDefinition0 = new ColumnDefinition();
                columnDefinition0.Width = new GridLength(2, GridUnitType.Star);
                ColumnDefinition columnDefinition1 = new ColumnDefinition();
                columnDefinition1.Width = new GridLength(525);
                columnDefinition1.MaxWidth = 1000;
                columnDefinition1.MinWidth = 200;
                dockPanel.ColumnDefinitions.Add(columnDefinition0);
                dockPanel.ColumnDefinitions.Add(columnDefinition1);
                dockPanel.Background = Brushes.White;

                GridSplitter gridSplitter = new();
                gridSplitter.HorizontalAlignment = HorizontalAlignment.Left;
                gridSplitter.VerticalAlignment = VerticalAlignment.Stretch;
                gridSplitter.ShowsPreview = false;
                gridSplitter.Width = 6;
                gridSplitter.ResizeDirection = GridResizeDirection.Columns;
                gridSplitter.Background = Brushes.Transparent;
                Grid.SetColumn(gridSplitter, 1);

                //Grid "grid" für die Eigenschaften (wird später zugewiesen)
                Grid grid = new();

                //erstelle Canvas für den Graphen
                Grid gridAroundCanvas = new Grid();
                Canvas graphCanvas = new Canvas();
                gridAroundCanvas.Children.Add(graphCanvas);
                Grid.SetColumn(gridAroundCanvas, 0);
                Grid.SetRow(gridAroundCanvas, 0);

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

                //Wenn Maus-Rad bewegt wird
                graphCanvas.MouseWheel += GraphCanvas_MouseWheel;

                //Eigene Darstellung
                graphCanvas.Background = Brushes.White;
                gridAroundCanvas.Background = Brushes.Transparent;
                graphCanvas.Margin = new Thickness(0);
                gridAroundCanvas.Margin = new Thickness(5);
                graphCanvas.ClipToBounds = true;
                gridAroundCanvas.ClipToBounds = true;

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
                GraphDarstellung graph = new(new List<GraphDarstellung.Knoten>(), new List<GraphDarstellung.Kanten>(), new int[0, 0], "GRAPH");
                try
                {
                    //Öffne die Datei
                    graph = CommandConsole.TransformFileToGraphDarstellung(path, graphCanvas);
                }
                catch
                {
                    //Gebe eine Fehlermeldung aus
                    MessageBox.Show(resman.GetString("FehlermeldungBeschädigteDatei", cul));

                    //Gebe eine Nachricht aus
                    DisplayMessage(resman.GetString("DateiNichtGeöffnetNachricht", cul) + path);
                }

                //erstelle die CommandConsole zum Graph
                CommandConsole commandConsole = new CommandConsole(graph, consoleOutput, path, this, tab);
                #endregion

                //Erstelle weitere visuelle Elemente
                #region
                Show show = new Show(graph, commandConsole);
                grid = show.ContentGrid;
                grid.Margin = new Thickness(gridSplitter.Width / 2, grid.Margin.Top, grid.Margin.Right, grid.Margin.Bottom);
                grid.MaxWidth = 1000;
                show.Content = new Grid();
                Grid.SetRow(grid, 0);
                Grid.SetColumn(grid, 1);
                dockPanel.Children.Add(grid);
                show.Close();

                dockPanel.Children.Add(gridAroundCanvas);
                dockPanel.Children.Add(gridSplitter);
                #endregion

                //speichere in der Liste ab, dass diese Datei gerade geöffnet ist und speichere auch ab, welche TextBox hier der Output ist, und welche Input, sowie die Commands
                #region
                this.OpenedFiles.Add(tab, path);
                this.Outputs.Add(tab, consoleOutput);
                this.Inputs.Add(tab, consoleInput);
                this.Consoles.Add(tab, commandConsole);
                this.Canvases.Add(tab, graphCanvas);
                this.Graphs.Add(tab, graph);
                this.OpenedEigenschaftenFenster.Add(tab, show);
                this.OpenedEigenschaftenFensterGrid.Add(tab, grid);
                #endregion

                //Finalisierung
                #region
                this.TabControl.SelectedIndex = this.TabControl.Items.Count - 1;
                this.SaveOpenedFiles();

                //Gebe eine Nachricht aus
                DisplayMessage(resman.GetString("DateiGeöffnetNachricht", cul) + path);
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
                this.OpenedEigenschaftenFenster.Remove(tab);
                this.OpenedEigenschaftenFensterGrid.Remove(tab);
                MessageBox.Show(e.Message);

                //Gebe eine Nachricht aus
                DisplayMessage(resman.GetString("DateiNichtGeöffnetNachricht", cul) + path);
                #endregion
            }
        }

        public TabItem AddNewTab<T>(string header, T content)
        {
            //Füge neuen Tab hinzu
            TabItem tab1 = new TabItem();

            //Füge Content hinzu
            tab1.Content = content;

            //erstelle Header
            DockPanel headerPanel = new DockPanel();
            headerPanel.MouseDown += Panel_MouseDown;

            //Füge TextBlock zum Header hinzu
            TextBlock title = new TextBlock();
            title.MouseDown += Panel_MouseDown;
            title.Text = header;
            headerPanel.Children.Add(title);

            //Füge Button zum Schließen zum Header hinzu
            Button closeButton = new();
            closeButton.Content = "x";
            closeButton.Background = Brushes.Transparent;
            closeButton.BorderBrush = Brushes.Transparent;
            closeButton.HorizontalAlignment = HorizontalAlignment.Right;
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
            closeButton.Click += this.CloseTab_ButtonClick;
            headerPanel.Children.Add(closeButton);

            //Eigenschaften für das TabItem "tab1"
            tab1.Header = headerPanel;
            tab1.Background = Brushes.White;

            //Füge den Header zum Element "Headers" hinzu
            this.Headers.Add(tab1, title);

            //Füge Tab zu "TabControl" hinzu
            this.TabControl.Items.Add(tab1);

            //Rückgabe von diesem Tab
            return tab1;
        }

        public void AktualisiereEigenschaftenFenster(TabItem tabItem)
        {
            //Aktualisiere das Eigenschaften-Fenster
            this.OpenedEigenschaftenFenster[tabItem].AktualisiereGridAsync();
        }

        public void AktualisiereEigenschaftenFenster()
        {
            //Aktualisiere das Eigenschaften-Fenster, die Daten + Rückgabe
            this.AktualisiereEigenschaftenFenster(this.GetOpenTab());
        }

        public void CloseTab(TabItem tab)
        {
            //Speichere den Graphen des Tabs ab, falls er überhaupt einen Graph enthält
            if (this.Consoles.ContainsKey(tab))
            {
                this.Consoles[tab].Save();
            }

            //Entferne den Tab aus dem TabControl "TabControl"
            this.TabControl.Items.Remove(tab);

            //Gucke, ob in dem Element "OpenedFiles" sich ein nicht geöffneter Tab befindet, wenn ja, dann entferne es aus "OpenedFiles"
            #region
            List<TabItem> save = new List<TabItem>(); //Hier werden die Tabs gespeichert, die später aus "OpenedFiles" entfernt werden muss; würde man diese direkt entfernen, so gibt es ein Error...
            Dictionary<TabItem, string>.KeyCollection tabs = this.OpenedFiles.Keys;
            foreach (TabItem i in tabs)
            {
                if (!this.TabControl.Items.Contains(i))
                {
                    save.Add(i);
                }
            }

            //Lösche jetzt die Tabs aus "OpenedFiles"
            foreach (TabItem i in save)
            {
                this.OpenedFiles.Remove(i);
            }
            #endregion

            //Speichere die noch offenen Tabs ab
            this.SaveOpenedFiles();

            //Gebe eine Nachricht aus
            this.DisplayMessageFromResman("TabGeschlossenNachricht");

            //Falls keine Tabs mehr übrig sind, dann schließe komplette App;
            if (this.TabControl.Items.Count == 0)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public void SetScrollX(double x, Canvas graphCanvas)
        {
            //Methode, um horizontal zu scrollen
            foreach (UIElement i in graphCanvas.Children)
            {
                Canvas.SetLeft(i, x);
            }
        }

        public void SetScrollY(double y, Canvas graphCanvas)
        {
            //Methode, um vertikal zu scrollen
            foreach (UIElement i in graphCanvas.Children)
            {
                Canvas.SetTop(i, y);
            }
        }

        public void SetZoom(double zoom, Point point, Canvas graphCanvas)
        {
            //Initialisierung
            const double zoomMax = 5;
            const double zoomMin = 0.25;
            double height = graphCanvas.ActualHeight;
            double width = graphCanvas.ActualWidth;

            //Zoome herein oder heraus
            if (zoom >= 1 && zoom < zoomMax)
            {
                graphCanvas.RenderTransform = new ScaleTransform(zoom, zoom, point.X, point.Y);
            }
            else if (zoom > zoomMin && zoom < 1)
            {
                graphCanvas.RenderTransform = new ScaleTransform(zoom, zoom, 0, 0);
                graphCanvas.Width = width / zoom;
                graphCanvas.Height = height / zoom;
            }
        }

        public void DisplayMessageFromResman(string stringName)
        {
            //Stelle die Nachricht rechts unten im TextBlock "ProgramTextBlock" in der entsprechenden Sprache dar
            DisplayMessage(resman.GetString(stringName, cul));
        }

        public void DisplayMessage(string message)
        {
            //Stelle die Nachricht rechts unten im TextBlock "ProgramTextBlock" dar
            this.ProgramTextBlock.Text = message;
        }
    }
}
