﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Thestias;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für GraphAuswahl.xaml
    /// </summary>
    public partial class GraphAuswahl : Window
    {
        //Liste enthält alle paths zu den zuletzt geöffneten Dateien
        protected static List<string> Paths = new List<string>();

        //delegate, um später Daten zu übertragen
        private delegate void Del(string path);

        //Das MainWindow, welches dann auch den Graphen darstellen soll
        private MainWindow MainWindow;

        public GraphAuswahl(State state, MainWindow mainWindow)
        {
            //erstelle das Fenster
            this.InitializeComponent();

            //Lege die Members fest
            #region
            this.MainWindow = mainWindow;
            #endregion

            //Übersetzen
            #region
            //lege den Titel und weiteren Text je nach Kultur fest
            this.Title = MainWindow.Resman.GetString("GraphAuswahlHeader", MainWindow.Cul);
            this.LetzteDateiHeader.Header = MainWindow.Resman.GetString("LetzteDateien", MainWindow.Cul);
            this.NeueDateiErstellenHeader.Header = MainWindow.Resman.GetString("ErstelleNeueDatei", MainWindow.Cul);
            this.NeueDateiName.Text = MainWindow.Resman.GetString("NeueDateiName", MainWindow.Cul);
            this.NeueDateiSpeicherort.Text = MainWindow.Resman.GetString("Speicherort", MainWindow.Cul);
            this.Erstellen.Content = MainWindow.Resman.GetString("Erstelle", MainWindow.Cul);
            this.DateiÖffnen.Header = MainWindow.Resman.GetString("DateiÖffnen", MainWindow.Cul);
            this.DateiSpeicherortText.Text = MainWindow.Resman.GetString("Speicherort", MainWindow.Cul);
            this.Öffnen.Content = MainWindow.Resman.GetString("Öffnen", MainWindow.Cul);
            this.TemplateText.Text = MainWindow.Resman.GetString("Template", MainWindow.Cul);
            this.NothingTemplate_Header.Text = MainWindow.Resman.GetString("NothingTemplate_Header", MainWindow.Cul);
            this.NothingTemplate_Text.Text = MainWindow.Resman.GetString("NothingTemplate_Text", MainWindow.Cul);
            this.CircleTemplate_Header.Text = MainWindow.Resman.GetString("CircleTemplate_Header", MainWindow.Cul);
            this.CircleTemplate_Text.Text = MainWindow.Resman.GetString("CircleTemplate_Text", MainWindow.Cul);
            this.CircleTemplate_KnotenText.Text = MainWindow.Resman.GetString("CircleTemplate_KnotenText", MainWindow.Cul);
            this.VieleckTemplate_Header.Text = MainWindow.Resman.GetString("VieleckTemplate_Header", MainWindow.Cul);
            this.VieleckTemplate_Text.Text = MainWindow.Resman.GetString("VieleckTemplate_Text", MainWindow.Cul);
            this.VieleckTemplate_KnotenText.Text = MainWindow.Resman.GetString("VieleckTemplate_KnotenText", MainWindow.Cul);
            this.VollständigesVieleckTemplate_Header.Text = MainWindow.Resman.GetString("VollständigesVieleckTemplate_Header", MainWindow.Cul);
            this.VollständigesVieleckTemplate_Text.Text = MainWindow.Resman.GetString("VollständigesVieleckTemplate_Text", MainWindow.Cul);
            this.VollständigesVieleckTemplate_KnotenText.Text = MainWindow.Resman.GetString("VollständigesVieleckTemplate_KnotenText", MainWindow.Cul);
            this.BipartiterGraphTemplate_Header.Text = MainWindow.Resman.GetString("BipartiterGraphTemplate_Header", MainWindow.Cul);
            this.BipartiterGraphTemplate_Text.Text = MainWindow.Resman.GetString("BipartiterGraphTemplate_Text", MainWindow.Cul);
            this.BipartiterGraphTemplate_Knoten1Text.Text = MainWindow.Resman.GetString("BipartiterGraphTemplate_Knoten1Text", MainWindow.Cul);
            this.BipartiterGraphTemplate_Knoten2Text.Text = MainWindow.Resman.GetString("BipartiterGraphTemplate_Knoten2Text", MainWindow.Cul);
            this.BaumTemplate_Header.Text = MainWindow.Resman.GetString("BaumTemplate_Header", MainWindow.Cul);
            this.BaumTemplate_Text.Text = MainWindow.Resman.GetString("BaumTemplate_Text", MainWindow.Cul);
            this.BaumTemplate_StufenText.Text = MainWindow.Resman.GetString("BaumTemplate_StufenText", MainWindow.Cul);
            this.BaumTemplate_VerzweigungenText.Text = MainWindow.Resman.GetString("BaumTemplate_VerzweigungenText", MainWindow.Cul);
            #endregion

            //stelle die zuletzt geöffneten Dateien in der ListBox "LetzteDatei" dar
            #region
            //Lese die Einstellung "RecentFiles" aus, die alle paths von den zuletzt geöffneten Dateien enthält; falls die Datei nicht existiert, lasse sie aus
            string[] pathsAsString = Properties.Settings.Default.OpenedFiles.Split("\n");
            foreach (string i in pathsAsString)
            {
                if (File.Exists(i))
                {
                    Paths.Add(i);
                }
            }

            //Lösche alle Duplicates aus paths
            List<string> copy = new List<string>();
            foreach (string i in Paths)
            {
                copy.Add(i);
            }
            Paths.Clear();
            foreach (string i in copy)
            {
                if (!Paths.Contains(i))
                {
                    Paths.Add(i);
                }
            }

            //gehe die Liste durch und füge zur ListBox ListBoxItems aus, die beim doppelt klicken einen neuen Tab mit dieser Datei im MainWindow öffnet
            foreach (string i in Paths)
            {
                //neues DockPanel (kommt in LIstBoxItems)
                DockPanel dock = new DockPanel();

                //Textblock; enthält Dateiname
                TextBlock textBlock1 = new TextBlock();
                textBlock1.FontSize = 20;
                textBlock1.Text = i.Substring(i.LastIndexOf(@"\") + 1);
                dock.Children.Add(textBlock1);

                //zweiter Textblock, der den Dateipfad enthält
                TextBlock textBlock2 = new TextBlock();
                textBlock2.FontSize = 15;
                textBlock2.Text = MainWindow.Resman.GetString("Weg", MainWindow.Cul) + ": " + i;
                dock.Children.Add(textBlock2);

                //lege Layout für Textblöcke fest
                DockPanel.SetDock(textBlock1, Dock.Top);
                DockPanel.SetDock(textBlock2, Dock.Bottom);

                //erstelle ListBoxItem, füge das DockPanel hinzu und füge es zur ListBox hinzu; lege außerdem fest, dass LIstBoxItem_Click ausgeführt wird, wenn das ListBoxItem doppelt gedrückt wird
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = dock;
                listBoxItem.Background = Brushes.White;
                listBoxItem.MouseDoubleClick += this.ListBoxItem_Click;
                this.LetzteDatei.Items.Add(listBoxItem);
            }
            #endregion

            //öffne das gewollte Element laut dem State "state"
            #region
            if (state == State.All)
            {
                //öffne alle TreeViewItems
                this.DateiÖffnen.IsExpanded = true;
                this.LetzteDateiHeader.IsExpanded = true;
                this.NeueDateiErstellenHeader.IsExpanded = true;
            }
            else if (state == State.CreateNewFile)
            {
                //öffne das TreeViewItem "NeueDateiErstellenHeader" zum Erstellen einer neuen Datei
                this.NeueDateiErstellenHeader.IsExpanded = true;
            }
            else if (state == State.Open)
            {
                //öffne das TreeViewItem "LetzteDateiHeader" zum Öffnen einer letztlich geöffneten Datei und das TreeViewItem "DateiÖffnen" zum Öffnen einer schon vorhandenen Datei
                this.DateiÖffnen.IsExpanded = true;
                this.LetzteDateiHeader.IsExpanded = true;
            }
            else if (state == State.OpenFile)
            {
                //öffne das TreeViewItem "DateiÖffnen" zum Öffnen einer schon vorhandenen Datei
                this.DateiÖffnen.IsExpanded = true;
            }
            else if (state == State.OpenRecentFile)
            {
                //öffne das TreeViewItem "LetzteDateiHeader" zum Öffnen einer letztlich geöffneten Datei
                this.LetzteDateiHeader.IsExpanded = true;
            }
            #endregion
        }

        private void ListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Paths[this.LetzteDatei.SelectedIndex]))
            {
                //führe die Methode "OpenFile" in der MainWindow-Klasse aus (aber über den anderen Thread, da sonst kein Tab entstehen kann, deshalb auch mit delegate...)
                Del handler = this.MainWindow.OpenFile;
                this.Dispatcher.BeginInvoke(handler, Paths[this.LetzteDatei.SelectedIndex]);

                //schließe das Fenster
                this.Close();
            }
        }

        private void Durchsuchen_Click(object sender, RoutedEventArgs e)
        {
            //erstelle einen FolderBrowserDialog und zeige den SelectedPath in der TextBox
            try
            {
                SaveFileDialog saveFileDialog = new();
                saveFileDialog.Filter = "Graph Markup Language (*.graphml) | *.graphml|Pollux Graph (*.poll) | *.poll";
                saveFileDialog.FileName = (this.NameDatei.Text == "") ? "graph.graphml" : this.NameDatei.Text + ".graphml";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == true)
                {
                    this.Speicherort.Text = saveFileDialog.FileName;
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void Erstellen_Click(object sender, RoutedEventArgs e)
        {
            //erstelle eine neue Datei und öffne diese, wenn das nicht möglich ist, dann mache einen Fehlersound und mache das Textfeld rot
            string path = this.Speicherort.Text;
            try
            {
                //Falls das Directory gar nicht existiert, werfe einen Fehler.
                if (!Directory.Exists(Stuff.GetDirectory(path)))
                {
                    throw new DirectoryNotFoundException();
                }

                //Lege den Namen fest
                string name = this.NameDatei.Text == "" ? "GRAPH" : this.NameDatei.Text.ToUpper();

                //Finde den FileMode heraus
                Graph.FileMode fileMode = path.EndsWith(".poll") ? Graph.FileMode.POLL : Graph.FileMode.GRAPHML;

                //Erstelle den Graphen bzw. seine Datei
                #region
                if (this.TemplateListBox.SelectedItem == this.NothingTemplate)
                {
                    //schreibe eine "leere" Datei
                    StreamWriter streamWriter1 = new StreamWriter(path);
                    streamWriter1.WriteLine(Graph.TransformGraphToString(new Graph(new(), new(), new int[0, 0], name), fileMode));
                    streamWriter1.Close();
                }
                else if (this.TemplateListBox.SelectedItem == this.CircleTemplate)
                {
                    //Erstelle den Graphen
                    Graph graph = Graph.GraphTemplates.Kreis(int.Parse(this.CircleTemplate_Knoten.Text));
                    graph.Name = name;

                    //schreibe eine neue Datei für den Graphen
                    StreamWriter streamWriter1 = new StreamWriter(path);
                    streamWriter1.WriteLine(Graph.TransformGraphToString(graph, fileMode));
                    streamWriter1.Close();
                }
                else if (this.TemplateListBox.SelectedItem == this.VieleckTemplate)
                {
                    //Erstelle den Graphen
                    Graph graph = Graph.GraphTemplates.Vieleck(int.Parse(this.VieleckTemplate_Knoten.Text));
                    graph.Name = name;

                    //schreibe eine neue Datei für den Graphen
                    StreamWriter streamWriter1 = new StreamWriter(path);
                    streamWriter1.WriteLine(Graph.TransformGraphToString(graph, fileMode));
                    streamWriter1.Close();
                }
                else if (this.TemplateListBox.SelectedItem == this.VollständigesVieleckTemplate)
                {
                    //Erstelle den Graphen
                    Graph graph = Graph.GraphTemplates.VollständigesVieleck(int.Parse(this.VollständigesVieleckTemplate_Knoten.Text));
                    graph.Name = name;

                    //schreibe eine neue Datei für den Graphen
                    StreamWriter streamWriter1 = new StreamWriter(path);
                    streamWriter1.WriteLine(Graph.TransformGraphToString(graph, fileMode));
                    streamWriter1.Close();
                }
                else if (this.TemplateListBox.SelectedItem == this.BipartiterGraphTemplate)
                {
                    //Erstelle den Graphen
                    Graph graph = Graph.GraphTemplates.VollständigerBipartiterGraph(int.Parse(this.BipartiterGraphTemplate_Knoten1.Text), int.Parse(this.BipartiterGraphTemplate_Knoten2.Text));
                    graph.Name = name;

                    //schreibe eine neue Datei für den Graphen
                    StreamWriter streamWriter1 = new StreamWriter(path);
                    streamWriter1.WriteLine(Graph.TransformGraphToString(graph, fileMode));
                    streamWriter1.Close();
                }
                else if (this.TemplateListBox.SelectedItem == this.BaumTemplate)
                {
                    //Erstelle den Graphen
                    Graph graph = Graph.GraphTemplates.Baum(int.Parse(this.BaumTemplate_Stufen.Text), int.Parse(this.BaumTemplate_Verzweigungen.Text));
                    graph.Name = name;

                    //schreibe eine neue Datei für den Graphen
                    StreamWriter streamWriter1 = new StreamWriter(path);
                    streamWriter1.WriteLine(Graph.TransformGraphToString(graph, fileMode));
                    streamWriter1.Close();
                }
                #endregion

                //führe die Methode "OpenFile" in der MainWindow-Klasse aus (aber über den anderen Thread, da sonst kein Tab entstehen kann, deshalb auch mit delegate...)
                Del handler = this.MainWindow.OpenFile;
                this.Dispatcher.BeginInvoke(handler, path);

                //Füge die Datei zu der Setting "RecentFiles" und zu "paths" hinzu, wenn sie noch nicht da ist
                if (!Properties.Settings.Default.RecentFiles.Contains(path))
                {
                    Properties.Settings.Default.RecentFiles += path + "\n";
                    Properties.Settings.Default.Save();
                    Paths.Add(path);
                }

                //schließe das Fenster
                this.Close();
            }
            catch
            {
                //MessageBox.Show(t.Message);
                //dann mache einen Fehlersound und mache das Textfeld rot
                SystemSounds.Asterisk.Play();
                this.Speicherort.BorderBrush = Brushes.Red;
            }
        }

        private void DateiDurchsuchen_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Graph Markup Language (*.graphml) | *.graphml|Pollux Graph (*.poll) | *.poll";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
                this.DateiSpeicherort.Text = filePath;
            }
            catch
            {
            }
        }

        private void Öffnen_Click(object sender, RoutedEventArgs e)
        {
            string path = this.DateiSpeicherort.Text;
            try
            {
                //Falls die Datei oder das Directory nicht existiert, werfe einen Fehler.
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException();
                }
                if (!Directory.Exists(Stuff.GetDirectory(path)))
                {
                    throw new DirectoryNotFoundException();
                }

                //führe die Methode "OpenFile" in der MainWindow-Klasse aus (aber über den anderen Thread, da sonst kein Tab entstehen kann, deshalb auch mit delegate...)
                Del handler = this.MainWindow.OpenFile;
                this.Dispatcher.BeginInvoke(handler, path);

                //Füge die Datei zu der Setting "RecentFiles" und zu "paths" hinzu, wenn sie noch nicht da ist
                if (!Properties.Settings.Default.RecentFiles.Contains(path))
                {
                    Properties.Settings.Default.RecentFiles += path + "\n";
                    Properties.Settings.Default.Save();
                    Paths.Add(path);
                }

                //schließe Fenster
                this.Close();
            }
            catch
            {
                //dann mache einen Fehlersound und mache das Textfeld rot
                SystemSounds.Asterisk.Play();
                this.DateiSpeicherort.BorderBrush = Brushes.Red;
            }
        }

        private void DateiSpeicherort_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Öffnen_Click(sender, e);
            }
        }

        private void Speicherort_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Erstellen_Click(sender, e);
            }
        }

        private void Escape(object sender, RoutedEventArgs e)
        {
            //Schließe das Fenster.
            this.Close();
        }

        //Die verschiedenen Zustände die dem Konstruktor übergeben werden
        public enum State
        {
            OpenRecentFile = 0,
            OpenFile = 1,
            CreateNewFile = 2,
            Open = 3,
            All = 4
        }
    }
}
