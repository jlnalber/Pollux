using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pollux
{
    partial class MainWindow
    {
        //die Methoden für die verschiedenen Events
        public void CloseTab(object sender, RoutedEventArgs e)
        {
            //Speichere alles ab
            this.SaveAll();

            //finde das TabItem des Buttons heraus, der gerade gedrückt wurde und lösche es, falls kein Button es ausgelöst hat, entferne den aktuell geöffneten Tab, 
            TabItem tabItem = new TabItem();
            switch (sender)
            {
                case System.Windows.Controls.Button ui: switch (ui.Parent) { case DockPanel dock: switch (dock.Parent) { case TabItem tab: TabControl.Items.Remove(tab); break; } break; }; break;
                default: TabControl.Items.Remove(TabControl.SelectedItem); break;
            }

            //Gucke, ob in dem Element "OpenedFiles" sich ein nicht geöffneter Tab befindet, wenn ja, dann entferne es aus "OpenedFiles"
            #region
            List<TabItem> save = new List<TabItem>(); //Hier werden die Tabs gespeichert, die später aus "OpenedFiles" entfernt werden muss; würde man diese direkt entfernen, so gibt es ein Error...
            Dictionary<TabItem, string>.KeyCollection tabs = OpenedFiles.Keys;
            foreach (TabItem i in tabs)
            {
                if (!TabControl.Items.Contains(i))
                {
                    save.Add(i);
                }
            }

            //Lösche jetzt sie Tabs aus "OpenedFiles"
            foreach (TabItem i in save)
            {
                OpenedFiles.Remove(i);
            }
            #endregion

            //Speichere die noch offenen Tabs ab
            this.SaveOpenedFiles();

            //Falls keine Tabs mehr übrig sind, dann schließe komplette App;
            if (TabControl.Items.Count == 0)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void Speichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Speichere die aktuell geöffnete Datei
                TabItem tabItem = new TabItem();

                switch (TabControl.SelectedItem)
                {
                    case TabItem item: tabItem = item; break;
                }

                this.Consoles[tabItem].Save();
            }
            catch { }
        }

        private void AlleSpeichern_Click(object sender, RoutedEventArgs e)
        {
            //Speicher alle Tabs
            this.SaveAll();
        }

        private void Neu_Click(object sender, RoutedEventArgs e)
        {
            //öffne das Fenster, um Datei zu erstellen
            GraphAuswahl fenster = new GraphAuswahl(GraphAuswahl.State.CreateNewFile, this);
            fenster.Show();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //öffne das Fenster, um Datei auszuwählen, die man öffnen möchte
            GraphAuswahl fenster = new GraphAuswahl(GraphAuswahl.State.Open, this);
            fenster.Show();
        }

        private void AllesSchließen_Click(object sender, RoutedEventArgs e)
        {
            //schließe die ganze App
            System.Windows.Application.Current.Shutdown();
        }

        private void KeyDown_ConsoleInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //wird ausgelöst, wenn man was im Eingabe-Feld eine Taste drückt
            try
            {
                //falls Enter gedrückt wird, dann spreche 
                if (e.Key == Key.Return)
                {
                    //finde den geöffneten Tab heraus
                    TabItem tab = GetOpenTab();

                    //spreche die "CommandConsole" mit dem Text an und leere das Eingabe-Feld
                    this.Consoles[tab].Command(this.Inputs[tab].Text);
                    this.Inputs[tab].Text = "";
                }
            }
            catch (Exception f)
            {
                //spiele Error-Sound
                MessageBox.Show(f.Message);
                SystemSounds.Asterisk.Play();
            }
        }

        private void Graph_HasToBeRedrawn(object sender, RoutedEventArgs e)
        {
            this.DrawGraph();
        }

        private void HilfeDatei_Click(object sender, RoutedEventArgs e)
        {
            //Diese Methode wird ausgeführt, wenn das MenuItem "HilfeDatei" geklickt wird
            string path = "";
            try
            {
                //Öffne die Hilfe-Datei im Verzeichnis "Hilfe" mit dem Namen "Hilfe.pdf" mit dem Standardprogramm
                if (Files.Contains("Pollux.exe"))
                {
                    path = Files.Substring(0, Files.LastIndexOf(@"\Pollux.exe")) + @"\Hilfe\Hilfe.pdf";
                }
                else
                {
                    path = Files + @"Hilfe\Hilfe.pdf";
                }
                //System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(@"\Hilfe\Hilfe.pdf"));
                new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(path)
                    {
                        UseShellExecute = true
                    }
                }.Start();
                //System.Diagnostics.Process.Start(path);
            }
            catch (Exception f)
            {
                System.Windows.MessageBox.Show(f.Message + path);
            }
        }

        private void KnotenHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            if (this.Graphs.Count == 0)
            {
                //Spiele Error-Sound
                SystemSounds.Asterisk.Play();
            }
            else
            {
                //Öffne ein neues "KantenHinzufügen"-Fenster
                KnotenHinzufügen window = new(this.GetOpenGraph(), this);
                window.Show();
            }
        }

        private void KanteHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            if (this.Graphs.Count == 0)
            {
                //Spiele Error-Sound
                SystemSounds.Asterisk.Play();
            }
            else
            {
                //Öffne ein neues "KantenHinzufügen"-Fenster
                KanteHinzufügen window = new(this.GetOpenGraph(), this);
                window.Show();
            }
        }

        private void NewWindow(object sender, RoutedEventArgs e)
        {
            //Erstelle ein neues "MainWindow"
            MainWindow mainWindow = new();
            mainWindow.Show();
        }

        private void EigenschaftenFenster_Click(object sender, RoutedEventArgs e)
        {
            //Öffne das Eigenschaften-Fenster durch die CommanConsole des geöffneten Tabs
            this.GetOpenConsole().Command("SHOW");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
                {
                    if (e.Key == Key.S)
                    {
                        this.SaveAll();
                        this.SaveOpenedFiles();
                    }
                    else if (e.Key == Key.E)
                    {
                        KanteHinzufügen_Click(sender, new());
                    }
                    else if (e.Key == Key.N)
                    {
                        KnotenHinzufügen_Click(sender, new());
                    }
                }
                else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
                {
                    if (e.Key == Key.N)
                    {
                        GraphAuswahl graphAuswahl = new(GraphAuswahl.State.CreateNewFile, this);
                        graphAuswahl.Show();
                    }
                    else if (e.Key == Key.O)
                    {
                        GraphAuswahl graphAuswahl = new(GraphAuswahl.State.Open, this);
                        graphAuswahl.Show();
                    }
                    else if (e.Key == Key.OemPlus)
                    {
                        //Erstelle ein neues "MainWindow"
                        MainWindow mainWindow = new();
                        mainWindow.Show();
                    }
                    else if (e.Key == Key.E)
                    {
                        Pollux.Show show = new(GetOpenGraph());
                        show.Show();
                    }
                    else if (e.Key == Key.S)
                    {
                        this.SaveOpenFile();
                        this.SaveOpenedFiles();
                    }
                }
            }
            catch
            {
                SystemSounds.Asterisk.Play();
            }
        }
    }
}
