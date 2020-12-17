using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für GraphAuswahl.xaml
    /// </summary>
    public partial class GraphAuswahl : Window
    {
        //Liste enthält alle paths zu den zuletzt geöffneten Dateien
        protected static List<string> paths = new List<string>();

        //delegate, um später Daten zu übertragen
        private delegate void Del(string path);

        //Das MainWindow, welches dann auch den Graphen darstellen soll
        MainWindow MainWindow;

        public GraphAuswahl(State state, MainWindow mainWindow)
        {
            //erstelle das Fenster
            InitializeComponent();

            //Lege die Members fest
            #region
            this.MainWindow = mainWindow;
            #endregion

            //Übersetzen
            #region
            //lege den Titel und weiteren Text je nach Kultur fest
            this.Title = MainWindow.resman.GetString("GraphAuswahlHeader", MainWindow.cul);
            LetzteDateiHeader.Header = MainWindow.resman.GetString("LetzteDateien", MainWindow.cul);
            NeueDateiErstellenHeader.Header = MainWindow.resman.GetString("ErstelleNeueDatei", MainWindow.cul);
            NeueDateiName.Text = MainWindow.resman.GetString("NeueDateiName", MainWindow.cul);
            NeueDateiSpeicherort.Text = MainWindow.resman.GetString("Speicherort", MainWindow.cul);
            Erstellen.Content = MainWindow.resman.GetString("Erstelle", MainWindow.cul);
            DateiÖffnen.Header = MainWindow.resman.GetString("DateiÖffnen", MainWindow.cul);
            DateiSpeicherortText.Text = MainWindow.resman.GetString("Speicherort", MainWindow.cul);
            Öffnen.Content = MainWindow.resman.GetString("Öffnen", MainWindow.cul);
            #endregion

            //stelle die zuletzt geöffneten Dateien in der ListBox "LetzteDatei" dar
            #region
            //Lese die Datei aus, die alle paths von den zuletzt verwendeten Dateien enthält, falls die Datei nicht existiert, dann erstelle eine neuen Ordner mit einer solchen Datei
            try
            {
                /*StreamWriter streamWriter = new StreamWriter(Environment.CurrentDirectory + @"\Data\recentFiles.txt");
                //streamWriter.WriteLine(@"C:\example\example.poll");
                streamWriter.Close();*/

                //lese die Datei aus und speicher es in dem Member "paths"
                StreamReader streamReader = new StreamReader(Environment.CurrentDirectory + @"\Data\recentFiles.txt");
                while (!streamReader.EndOfStream)
                {
                    paths.Add(streamReader.ReadLine());
                }
                streamReader.Close();
            }
            catch
            {
                //erstelle Ordner
                DirectoryInfo di = Directory.CreateDirectory(Environment.CurrentDirectory + @"\Data");

                //erstelle Datei in diesem Ordner
                StreamWriter streamWriter = new StreamWriter(Environment.CurrentDirectory + @"\Data\recentFiles.txt");
                streamWriter.Close();
            }

            //Lösche alle Duplicates aus paths
            List<string> copy = new List<string>();
            foreach (string i in paths)
            {
                copy.Add(i);
            }
            paths.Clear();
            foreach (string i in copy)
            {
                if (!paths.Contains(i))
                {
                    paths.Add(i);
                }
            }

            //gehe die Liste durch und füge zur ListBox ListBoxItems aus, die beim doppelt klicken einen neuen Tab mit dieser Datei im MainWindow öffnet
            foreach (string i in paths)
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
                textBlock2.Text = MainWindow.resman.GetString("Weg", MainWindow.cul) + ": " + i;
                dock.Children.Add(textBlock2);

                //lege Layout für Textblöcke fest
                DockPanel.SetDock(textBlock1, Dock.Top);
                DockPanel.SetDock(textBlock2, Dock.Bottom);

                //erstelle ListBoxItem, füge das DockPanel hinzu und füge es zur ListBox hinzu; lege außerdem fest, dass LIstBoxItem_Click ausgeführt wird, wenn das ListBoxItem doppelt gedrückt wird
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = dock;
                listBoxItem.Background = Brushes.White;
                listBoxItem.MouseDoubleClick += ListBoxItem_Click;
                LetzteDatei.Items.Add(listBoxItem);
            }
            #endregion

            //öffne das gewollte Element laut dem State "state"
            #region
            if (state == State.All)
            {
                //öffne alle TreeViewItems
                DateiÖffnen.IsExpanded = true;
                LetzteDateiHeader.IsExpanded = true;
                NeueDateiErstellenHeader.IsExpanded = true;
            }
            else if (state == State.CreateNewFile)
            {
                //öffne das TreeViewItem "NeueDateiErstellenHeader" zum Erstellen einer neuen Datei
                NeueDateiErstellenHeader.IsExpanded = true;
            }
            else if (state == State.Open)
            {
                //öffne das TreeViewItem "LetzteDateiHeader" zum Öffnen einer letztlich geöffneten Datei und das TreeViewItem "DateiÖffnen" zum Öffnen einer schon vorhandenen Datei
                DateiÖffnen.IsExpanded = true;
                LetzteDateiHeader.IsExpanded = true;
            }
            else if (state == State.OpenFile)
            {
                //öffne das TreeViewItem "DateiÖffnen" zum Öffnen einer schon vorhandenen Datei
                DateiÖffnen.IsExpanded = true;
            }
            else if (state == State.OpenRecentFile)
            {
                //öffne das TreeViewItem "LetzteDateiHeader" zum Öffnen einer letztlich geöffneten Datei
                LetzteDateiHeader.IsExpanded = true;
            }
            #endregion
        }

        private void ListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            //führe die Methode "OpenFile" in der MainWindow-Klasse aus (aber über den anderen Thread, da sonst kein Tab entstehen kann, deshalb auch mit delegate...)
            Del handler = MainWindow.OpenFile;
            Dispatcher.BeginInvoke(handler, paths[LetzteDatei.Items.IndexOf(LetzteDatei.SelectedItem)]);

            //schließe das Fenster
            this.Close();
        }

        private void Durchsuchen_Click(object sender, RoutedEventArgs e)
        {
            //erstelle einen FolderBrowserDialog und zeige den SelectedPath in der TextBox
            try
            {
                SaveFileDialog saveFileDialog = new();
                if (saveFileDialog.ShowDialog() == true)
                {
                    Speicherort.Text = saveFileDialog.FileName;
                }
            }
            catch
            {
            }
        }

        private void Erstellen_Click(object sender, RoutedEventArgs e)
        {
            //erstelle eine neue Datei und öffne diese, wenn das nicht möglich ist, dann mache einen Fehlersound und mache das Textfeld rot
            string path = Speicherort.Text + @"\" + Name.Text + ".poll";
            try
            {
                //schreibe eine "leere" Datei
                StreamWriter streamWriter1 = new StreamWriter(path);
                streamWriter1.WriteLine("[NAME]");
                streamWriter1.WriteLine(Name.Text.ToUpper());
                streamWriter1.WriteLine("[/NAME]");
                streamWriter1.WriteLine("[NODES]");
                streamWriter1.WriteLine("[/NODES]");
                streamWriter1.WriteLine("[EDGES]");
                streamWriter1.WriteLine("[/EDGES]");
                streamWriter1.Close();

                //führe die Methode "OpenFile" in der MainWindow-Klasse aus (aber über den anderen Thread, da sonst kein Tab entstehen kann, deshalb auch mit delegate...)
                Del handler = MainWindow.OpenFile;
                Dispatcher.BeginInvoke(handler, path);

                //Füge die gerade erstellte Datei zu der recentFiles-Datei und zu "paths" hinzu
                StreamWriter streamWriter2 = new StreamWriter(Environment.CurrentDirectory + @"\Data\recentFiles.txt");
                streamWriter2.WriteLine(path);
                streamWriter2.Close();
                paths.Add(path);

                //schließe das Fenster
                this.Close();
            }
            catch
            {
                //dann mache einen Fehlersound und mache das Textfeld rot
                SystemSounds.Asterisk.Play();
                Speicherort.BorderBrush = Brushes.Red;
            }
        }

        private void DateiDurchsuchen_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "poll files (*.poll)|*.poll";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
                DateiSpeicherort.Text = filePath;
            }
            catch
            {
            }
        }

        private void Öffnen_Click(object sender, RoutedEventArgs e)
        {
            string path = DateiSpeicherort.Text;
            try
            {
                //führe die Methode "OpenFile" in der MainWindow-Klasse aus (aber über den anderen Thread, da sonst kein Tab entstehen kann, deshalb auch mit delegate...)
                Del handler = MainWindow.OpenFile;
                Dispatcher.BeginInvoke(handler, path);

                //Füge die Datei zu der recentFiles-Datei und zu "paths" hinzu, wenn sie noch nicht da ist
                if (!paths.Contains(path))
                {
                    StreamWriter streamWriter2 = new StreamWriter(Environment.CurrentDirectory + @"\Data\recentFiles.txt");
                    streamWriter2.WriteLine(path);
                    streamWriter2.Close();
                    paths.Add(path);
                }

                //schließe Fenster
                this.Close();
            }
            catch
            {
                //dann mache einen Fehlersound und mache das Textfeld rot
                SystemSounds.Asterisk.Play();
                DateiSpeicherort.BorderBrush = Brushes.Red;
            }
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
