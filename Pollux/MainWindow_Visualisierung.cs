﻿using Castor;
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Thestias;

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
                //erstelle den Graphen, indem die Datei ausgelesen wird
                VisualGraph graph = new();
                try
                {
                    //Öffne die Datei
                    Graph.FileMode fileMode = path.EndsWith(".poll") ? Graph.FileMode.POLL : Graph.FileMode.GRAPHML;
                    graph = VisualGraph.TransformFileToVisualGraph(path, fileMode);
                }
                catch
                {
                    //Gebe eine Fehlermeldung aus
                    MessageBox.Show(Resman.GetString("FehlermeldungBeschädigteDatei", Cul));

                    //Gebe eine Nachricht aus
                    DisplayMessage(Resman.GetString("DateiNichtGeöffnetNachricht", Cul) + path);
                }

                //Darstellung und Einstellungen des VisualGraphs "graph".
                graph.ShowProperties = true;

                //füge ihn zum TabItem hinzu
                tabGraph.Content = graph;
                #endregion

                //erstelle einen neuen Tab
                tab = this.AddNewTab(path.Substring(path.LastIndexOf(@"\") + 1), tabControl);

                //fokusiere auf das Eingabe-Feld
                consoleInput.Focus();
                #endregion

                //erstelle die CommandConsole zum Graph
                CommandConsole commandConsole = new CommandConsole(graph, consoleOutput, path, this, tab);

                //speichere in der Liste ab, dass diese Datei gerade geöffnet ist und speichere auch ab, welche TextBox hier der Output ist, und welche Input, sowie die Commands
                #region
                this.OpenedFiles.Add(tab, path);
                this.Outputs.Add(tab, consoleOutput);
                this.Inputs.Add(tab, consoleInput);
                this.Consoles.Add(tab, commandConsole);
                this.Graphs.Add(tab, graph);
                #endregion

                //Finalisierung
                #region
                this.TabControl.SelectedIndex = this.TabControl.Items.Count - 1;
                this.SaveOpenedFiles();

                //Gebe eine Nachricht aus
                DisplayMessage(Resman.GetString("DateiGeöffnetNachricht", Cul) + path);
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
                this.Graphs.Remove(tab);
                MessageBox.Show(e.Message);

                //Gebe eine Nachricht aus
                DisplayMessage(Resman.GetString("DateiNichtGeöffnetNachricht", Cul) + path);
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
            closeButton.ToolTip = Resman.GetString("TabSchließen", Cul);
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

        public void DisplayMessageFromResman(string stringName)
        {
            //Stelle die Nachricht rechts unten im TextBlock "ProgramTextBlock" in der entsprechenden Sprache dar
            DisplayMessage(Resman.GetString(stringName, Cul));
        }

        public void DisplayMessage(string message)
        {
            //Stelle die Nachricht rechts unten im TextBlock "ProgramTextBlock" dar
            this.ProgramTextBlock.Text = message;
        }
    }
}
