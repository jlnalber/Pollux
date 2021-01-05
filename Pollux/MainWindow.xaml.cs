using Pollux.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Windows;
using System.Windows.Controls;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Konstruktor und Destruktor
        #region
        public MainWindow()
        {
            //Initialisiere dieses Fenster
            InitializeComponent();

            if (main == null)
            {
                //Initialisiere die statischen Member
                #region
                //setze das MainWindow auf dieses Fenster
                main = this;

                //füge das aktuelle Directory hinzu
                AppDirectory = Environment.GetCommandLineArgs()[0].Replace("Pollux.dll", "");
                AppDirectory = AppDirectory.Replace("Pollux.exe", "");
                Files = Environment.GetCommandLineArgs()[0].Replace("bin\\Debug\\net5.0-windows\\Pollux.dll", "");
                Files = Files.Replace("bin\\Debug\\net5.0-windows\\Pollux.exe", "");
                Files = Files.Replace("bin\\Release\\net5.0-windows\\Pollux.dll", "");
                Files = Files.Replace("bin\\Release\\net5.0-windows\\Pollux.exe", "");
                Files = Files.Replace("Pollux.exe", "");
                Files = Files.Replace("Pollux.dll", "");

                //initialisiere Resource und rufe die Kultur ab
                resman = new ResourceManager(typeof(Resources));
                cul = CultureInfo.CurrentUICulture;
                //cul = new CultureInfo("en");
                //cul = new CultureInfo("fr");
                #endregion
            }

            //Initialisiere die Members
            #region
            this.OpenedFiles = new Dictionary<TabItem, string>();
            this.Outputs = new Dictionary<TabItem, System.Windows.Controls.TextBox>();
            this.Inputs = new Dictionary<TabItem, System.Windows.Controls.TextBox>();
            this.Consoles = new Dictionary<TabItem, CommandConsole>();
            this.Canvases = new Dictionary<TabItem, Canvas>();
            this.GraphDarstellungen = new Dictionary<TabItem, GraphDarstellung>();
            this.Graphs = new Dictionary<TabItem, Graph.Graph>();
            this.Headers = new Dictionary<TabItem, TextBlock>();
            this.OpenedEigenschaftenFenster = new Dictionary<TabItem, Show>();
            this.OpenedEigenschaftenFensterGrid = new Dictionary<TabItem, Grid>();
            #endregion

            //Mache verschiedene Darstellungen
            #region
            //Füge "Icons" hinzu
            this.Schließen.Icon = " x ";
            this.AllesSchließen.Icon = " x ";
            this.NeuesFenster.Icon = " + ";
            this.HilfeDatei.Icon = " ? ";
            this.Neu.Icon = " + ";
            this.KanteHinzufügen.Icon = " + ";
            this.KnotenHinzufügen.Icon = " + ";

            //setze die Texte auf die jeweils richtige Sprache
            #region
            this.Title = resman.GetString("Title", cul);
            this.Datei.Header = resman.GetString("Datei", cul);
            //this.Ansicht.Header = resman.GetString("Ansicht", cul);
            this.Fenster.Header = resman.GetString("Fenster", cul);
            this.Hilfe.Header = resman.GetString("Hilfe", cul);
            this.Bearbeiten.Header = resman.GetString("Bearbeiten", cul);
            this.Speichern.Header = resman.GetString("Speichern", cul);
            this.AlleSpeichern.Header = resman.GetString("AlleSpeichern", cul);
            this.AlsSVGSpeichern.Header = resman.GetString("SaveAsSVG", cul);
            this.Neu.Header = resman.GetString("Neu", cul);
            this.Einstellungen.Header = resman.GetString("Einstellungen", cul);
            this.Schließen.Header = resman.GetString("Schließen", cul);
            this.AllesSchließen.Header = resman.GetString("AlleSchließen", cul);
            this.Öffnen.Header = resman.GetString("Öffnen", cul);
            this.HilfeDatei.Header = resman.GetString("Hilfe", cul);
            this.GraphBearbeiten.Header = resman.GetString("GraphBearbeiten", cul);
            this.KanteHinzufügen.Header = resman.GetString("KanteHinzufügen", cul);
            this.KnotenHinzufügen.Header = resman.GetString("KnotenHinzufügen", cul);
            this.EinstellungsFenster.Header = resman.GetString("Einstellungen", cul);
            //this.ZwischenTabsSpringen.Header = resman.GetString("ZwischenTabsSpringen", cul);
            this.NeuesFenster.Header = resman.GetString("NeuesFenster", cul);
            this.EigenschaftenFenster.Header = resman.GetString("EigenschaftenFenster", cul);
            #endregion

            #endregion

            //Öffne die Tabs
            #region
            //Lese die Einstellung "OpenedFiles" aus, die alle paths von den zuletzt verwendeten Dateien enthält; falls die Datei nicht existiert, lasse sie aus
            List<string> paths = new List<string>();
            string[] pathsAsString = Settings.Default.OpenedFiles.Split("\n");
            foreach (string i in pathsAsString)
            {
                try
                {
                    StreamReader streamReader = new StreamReader(i);
                    streamReader.Close();
                    paths.Add(i);
                }
                catch { }
            }

            //gucke nach, ob das Programm ausgeführt wurde, weil eine Datei angeklickt wurde, füge zu paths hinzu
            string[] vs = Environment.GetCommandLineArgs();
            if (vs.Length > 1)
            {
                int counter = 0;
                foreach (string i in vs)
                {
                    if (counter != 0 && i.Contains(".poll"))
                    {
                        paths.Add(i);
                    }
                    counter++;
                }
            }

            //Erstelle die Tabs; öffne die Dateien, die beim letzten Schließen des Programms offen waren
            if (paths.Count == 0)
            {
                //Experimenteller Browser
                #region
                /*
                //füge einen neuen WebBrowser "webBrowser" hinzu, um die HTML-Datei anzuzeigen
                WebBrowser webBrowser = new WebBrowser();
                webBrowser.Navigate(@"file:\" + Files + @"HTML\HTMLWelcomePage.html");
                webBrowser.Margin = new Thickness(5);
                webBrowser.HorizontalAlignment = HorizontalAlignment.Left;
                webBrowser.VerticalAlignment = VerticalAlignment.Top;

                //Füge den WebBrowser "webBrowser"
                AddNewTab<WebBrowser>(resman.GetString("Willkommen", cul), webBrowser);*/
                #endregion

                //Füge neuen Tab hinzu mit WillkommensContent, falls keine Datei in der Setting "OpenedFiles" liegt
                Grid grid = new();

                //Erstelle den ersten TextBlock
                TextBlock tb1 = new TextBlock();
                tb1.Text = resman.GetString("Willkommenstext", cul);
                tb1.FontSize = 23;
                tb1.HorizontalAlignment = HorizontalAlignment.Left;
                tb1.VerticalAlignment = VerticalAlignment.Top;
                tb1.Margin = new(20, 50, 10, 10);
                grid.Children.Add(tb1);

                //Erstelle den zweiten TextBlock
                TextBlock tb2 = new TextBlock();
                tb2.Text = resman.GetString("Willkommenstext2", cul);
                tb2.FontSize = 15;
                tb2.HorizontalAlignment = HorizontalAlignment.Left;
                tb2.VerticalAlignment = VerticalAlignment.Top;
                tb2.Margin = new(20, 100, 10, 10);
                grid.Children.Add(tb2);

                //Button zum Datei öffnen
                Button button1 = new();
                button1.Click += Open_Click;
                button1.Content = resman.GetString("GraphÖffnen", cul);
                button1.Padding = new(10);
                button1.HorizontalAlignment = HorizontalAlignment.Left;
                button1.VerticalAlignment = VerticalAlignment.Top;
                button1.Margin = new(20, 150, 10, 10);
                grid.Children.Add(button1);

                //Button zum Datei erstellen
                Button button2 = new();
                button2.Click += Neu_Click;
                button2.Content = resman.GetString("GraphErstellen", cul);
                button2.Padding = new(10);
                button2.HorizontalAlignment = HorizontalAlignment.Left;
                button2.VerticalAlignment = VerticalAlignment.Top;
                button2.Margin = new(20, 200, 10, 10);
                grid.Children.Add(button2);

                //Button zum neuen Fenster öffnen
                Button button3 = new();
                button3.Click += NewWindow;
                button3.Content = resman.GetString("NeuesFenster", cul);
                button3.Padding = new(10);
                button3.HorizontalAlignment = HorizontalAlignment.Left;
                button3.VerticalAlignment = VerticalAlignment.Top;
                button3.Margin = new(20, 250, 10, 10);
                grid.Children.Add(button3);

                //Button zum Hilfe-Datei öffnen
                Button button4 = new();
                button4.Click += HilfeDatei_Click;
                button4.Content = resman.GetString("HilfeDateiÖffnen", cul);
                button4.Padding = new(10);
                button4.HorizontalAlignment = HorizontalAlignment.Left;
                button4.VerticalAlignment = VerticalAlignment.Top;
                button4.Margin = new(20, 300, 10, 10);
                grid.Children.Add(button4);

                //Füge den Tab zum Fenster hinzu
                AddNewTab("Pollux", grid);
            }
            else
            {
                //öffne jede Datei in der Liste "paths", welche jede Datei in der Setting "OpenedFiles" repräsentiert
                foreach (string path in paths)
                {
                    OpenFile(path);
                }
            }
            #endregion
        }

        ~MainWindow()
        {
            //abspeicheren, welche Dateien alle geöffnet sind
            SaveOpenedFiles();

            //speichere die Dateien noch schnell ab
            SaveAll();

            if (main == this)
            {
                //Schließe alle weiteren Fenster
                System.Windows.Application.Current.Shutdown();
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
