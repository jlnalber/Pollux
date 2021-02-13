using Microsoft.Win32;
using Pollux.Graph;
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class MainWindow
    {
        //die Methoden für die verschiedenen Events
        public void CloseTab_ButtonClick(object sender, RoutedEventArgs e)
        {
            //finde das TabItem des Buttons heraus, der gerade gedrückt wurde und lösche es, falls kein Button es ausgelöst hat, entferne den aktuell geöffneten Tab
            switch (sender)
            {
                case Button ui: switch (ui.Parent) { case DockPanel dock: switch (dock.Parent) { case TabItem tab: this.CloseTab(tab); break; } break; }; break;
                default: if (this.TabControl.SelectedItem is TabItem tab1) { this.CloseTab(tab1); } break;
            }
        }

        private void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                if (sender is DockPanel dockPanel)
                {
                    if (dockPanel.Parent is TabItem tab)
                    {
                        this.CloseTab(tab);
                    }
                }
                else if (sender is TextBlock textBlock)
                {
                    if (textBlock.Parent is DockPanel dockPanel1)
                    {
                        if (dockPanel1.Parent is TabItem tab)
                        {
                            this.CloseTab(tab);
                        }
                    }
                }
            }
        }

        private void Speichern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Speichere die aktuell geöffnete Datei
                this.GetOpenConsole().Save();
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
                    TabItem tab = this.GetOpenTab();

                    //spreche die "CommandConsole" mit dem Text an und leere das Eingabe-Feld
                    this.Consoles[tab].Command(this.Inputs[tab].Text);
                    this.Inputs[tab].Text = "";
                }
            }
            catch
            {
                //spiele Error-Sound
                SystemSounds.Asterisk.Play();
            }
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

        public void KnotenHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            if (this.Graphs.Count == 0)
            {
                //Spiele Error-Sound
                SystemSounds.Asterisk.Play();
            }
            else
            {
                //Öffne ein neues "KnotenHinzufügen"-Fenster
                KnotenHinzufügen window = new(this.GetOpenGraph(), this);
                window.Show();
            }
        }

        public void KanteHinzufügen_Click(object sender, RoutedEventArgs e)
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

        public void EigenschaftenFenster_Click(object sender, RoutedEventArgs e)
        {
            //Öffne das Eigenschaften-Fenster durch die CommanConsole des geöffneten Tabs
            this.GetOpenConsole().Command("SHOW");
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //Die Shortcuts
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
                        this.KanteHinzufügen_Click(sender, new());
                    }
                    else if (e.Key == Key.N)
                    {
                        this.KnotenHinzufügen_Click(sender, new());
                    }
                }
                else if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt))
                {
                    if (e.Key == Key.S)
                    {
                        //Erstelle ein Einstellungs-Fenster "window"
                        Einstellungen window = new Einstellungen();
                        window.Show();
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
                        this.GetOpenConsole().Command("SHOW");
                    }
                    else if (e.Key == Key.S)
                    {
                        this.GetOpenConsole().Command("SAVE");
                        this.SaveOpenedFiles();
                    }
                }
            }
            catch
            {
                SystemSounds.Asterisk.Play();
            }
        }

        private void Einstellungen_Click(object sender, RoutedEventArgs e)
        {
            //Öffne ein Einstellungs-Fenster
            Einstellungen window = new();
            window.Show();
        }

        public void LöschenKnoten_Click(object sender, RoutedEventArgs e)
        {
            //Suche nach der offenen GraphDarstellung
            GraphDarstellung openGraphDarstellung = this.GetOpenGraph();

            //Lösche den Knoten, bei dem das MenuItem das Event ausgelöst hat
            for (int i = 0; i < openGraphDarstellung.GraphKnoten.Count; i++)
            {
                if (((GraphDarstellung.Knoten)openGraphDarstellung.GraphKnoten[i]).Ellipse.ContextMenu.Items.Contains(sender))
                {
                    this.GetOpenConsole().Command("REMOVE " + openGraphDarstellung.GraphKnoten[i].Name);
                    i--;
                }
            }
        }

        public void LöschenKante_Click(object sender, RoutedEventArgs e)
        {
            //Suche nach der offenen GraphDarstellung
            GraphDarstellung openGraphDarstellung = this.GetOpenGraph();

            //Lösche die Kante, bei dem das MenuItem das Event ausgelöst hat
            for (int i = 0; i < openGraphDarstellung.GraphKanten.Count; i++)
            {
                if (((GraphDarstellung.Kanten)openGraphDarstellung.GraphKanten[i]).Line is Line line)
                {
                    if (line.ContextMenu.Items.Contains(sender))
                    {
                        this.GetOpenConsole().Command("REMOVE " + openGraphDarstellung.GraphKanten[i].Name);
                        i--;
                    }
                }
                else if (((GraphDarstellung.Kanten)openGraphDarstellung.GraphKanten[i]).Line is Ellipse ellipse)
                {
                    if (ellipse.ContextMenu.Items.Contains(sender))
                    {
                        this.GetOpenConsole().Command("REMOVE " + openGraphDarstellung.GraphKanten[i].Name);
                        i--;
                    }
                }
            }
        }

        private void AlsBildSpeichern_Click(object sender, RoutedEventArgs e)
        {
            //Finde den Pfad heraus, wo die Datei gespeichert werden soll
            string filePath = "";
            try
            {
                //Erstelle einen SaveFileDialog "openFileDialog"
                SaveFileDialog openFileDialog = new();
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Scalable Vector Graphics (*.svg)|*.svg|Bitmap (*.bmp)|*.bmp|Graphics Interchange Format (*.gif)|*.gif|Exchangeable Image File Format (*.exif)|*exif|JPEG-File (*jpg)|*jpg|Portable Network Graphics (*png)|*png|Tagged Image File Format (*tiff)|*tiff";
                openFileDialog.FilterIndex = 1;
                openFileDialog.FileName = "graph.svg";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == true)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }

                if (!(filePath.EndsWith(".svg") || filePath.EndsWith(".bmp") || filePath.EndsWith(".gif") || filePath.EndsWith(".exif") || filePath.EndsWith(".jpg") || filePath.EndsWith(".png") || filePath.EndsWith(".tiff")))
                {
                    filePath += ".svg";
                }

                //Erstelle die Datei in der gewollten Datei
                if (filePath.EndsWith(".svg"))
                {
                    this.GetOpenGraph().SaveAsSVG(filePath);
                }
                else
                {
                    this.GetOpenGraph().SaveAsBitmap(filePath);
                }
            }
            catch { }
        }

        private void GraphCanvas_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            const double ScrollSpeed = 0.25;
            if (sender is Canvas graphCanvas)
            {
                //Zoom
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    const double zoomMax = 5;
                    const double zoomMin = 0.25;
                    const double zoomSpeed = 0.001;
                    double height = graphCanvas.ActualHeight;
                    double width = graphCanvas.ActualWidth;
                    Point point = e.GetPosition(graphCanvas);
                    if (graphCanvas.Children.Count != 0)
                    {
                        double top = Canvas.GetTop(graphCanvas.Children[0]);
                        double left = Canvas.GetLeft(graphCanvas.Children[0]);
                        double zoom = 1 + e.Delta * zoomSpeed;
                        try
                        {
                            zoom = e.Delta * zoomSpeed + ((ScaleTransform)graphCanvas.RenderTransform).ScaleX;
                        }
                        catch { }

                        if (zoom >= 1 && zoom < zoomMax)
                        {
                            graphCanvas.RenderTransform = new ScaleTransform(zoom, zoom, point.X, point.Y);
                        }
                        else if (zoom > zoomMin && zoom < 1)
                        {
                            graphCanvas.RenderTransform = new ScaleTransform(zoom, zoom, left, top);
                            graphCanvas.Width = width / zoom;
                            graphCanvas.Height = height / zoom;
                        }
                    }
                }

                //Horizontaler Scroll
                else if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    foreach (UIElement i in graphCanvas.Children)
                    {
                        Canvas.SetLeft(i, Canvas.GetLeft(i) + e.Delta * ScrollSpeed);
                    }
                }

                //Vertikaler Scroll
                else
                {
                    foreach (UIElement i in graphCanvas.Children)
                    {
                        Canvas.SetTop(i, Canvas.GetTop(i) + e.Delta * ScrollSpeed);
                    }
                }
            }
        }
    }
}
