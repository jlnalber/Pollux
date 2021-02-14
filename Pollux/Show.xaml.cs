using Pollux.Graph;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für Show.xaml
    /// </summary>
    public partial class Show : Window
    {
        public GraphDarstellung Graph;
        public CommandConsole CommandConsole;

        public Show(GraphDarstellung graph, CommandConsole commandConsole)
        {
            //Erstelle das Fenster
            this.InitializeComponent();

            //Initialisierung der Member
            this.Graph = graph;
            this.CommandConsole = commandConsole;

            //Übersetze die Texte und stelle sie dar
            #region
            this.Title = this.Graph.Name + " - " + MainWindow.resman.GetString("Eigenschaften", MainWindow.cul);
            this.Eigenschaft.Header = MainWindow.resman.GetString("Eigenschaft", MainWindow.cul);
            this.Wert.Header = MainWindow.resman.GetString("Wert", MainWindow.cul);
            this.KnotenPickerText.Text = MainWindow.resman.GetString("KnotenPickerText", MainWindow.cul);
            this.KantenPickerText.Text = MainWindow.resman.GetString("KantenPickerText", MainWindow.cul);
            this.KnotenNameText.Text = MainWindow.resman.GetString("KnotenNameText", MainWindow.cul);
            this.KnotenParentText.Text = MainWindow.resman.GetString("KnotenParentText", MainWindow.cul);
            this.KnotenGradText.Text = MainWindow.resman.GetString("KnotenGradText", MainWindow.cul);
            this.KnotenKantenText.Text = MainWindow.resman.GetString("KnotenKantenText", MainWindow.cul);
            this.KantenNameText.Text = MainWindow.resman.GetString("KantenNameText", MainWindow.cul);
            this.KantenParentText.Text = MainWindow.resman.GetString("KantenParentText", MainWindow.cul);
            this.KantenStartText.Text = MainWindow.resman.GetString("KantenStartText", MainWindow.cul);
            this.KantenEndeText.Text = MainWindow.resman.GetString("KantenEndeText", MainWindow.cul);
            this.KantenTab.Header = MainWindow.resman.GetString("KantenTab_Header", MainWindow.cul);
            this.KnotenTab.Header = MainWindow.resman.GetString("KnotenTab_Header", MainWindow.cul);
            this.GraphTab.Header = MainWindow.resman.GetString("GraphTab_Header", MainWindow.cul);
            this.UmbennenKnoten.Content = MainWindow.resman.GetString("UmbennenKnoten", MainWindow.cul);
            this.UmbennenKanten.Content = MainWindow.resman.GetString("UmbennenKanten", MainWindow.cul);
            #endregion

            //Darstellung
            #region
            //DataGrid
            this.Wert.Header = MainWindow.resman.GetString("WertDataGrid", MainWindow.cul);
            this.Eigenschaft.Header = MainWindow.resman.GetString("EigenschaftDataGrid", MainWindow.cul);

            //KnotenPicker
            this.KnotenPicker.SelectionChanged += this.KnotenPickerChanged;
            this.KnotenPicker.SelectedIndex = 0;

            //KantenPicker
            this.KantenPicker.SelectionChanged += this.KantenPickerChanged;
            this.KantenPicker.SelectedIndex = 0;

            //Lasse das Grid aktualisieren
            this.AktualisiereGrid();

            /*
            //Grid-Table
            #region
            //füge genügend "Columns" und "Rows" zum "Table" hinzu

            for (int i = 0; i <= graph.Liste.GetLength(0); i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                this.Table.ColumnDefinitions.Add(columnDefinition);
            }
            for (int i = 0; i <= graph.Liste.GetLength(1); i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                this.Table.RowDefinitions.Add(rowDefinition);
            }

            //stelle die Namen der Knoten im "Table" dar
            foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
            {
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = i.Name + ":";
                TextBlock textBlock2 = new TextBlock();
                textBlock2.Text = i.Name + ":";

                Thickness thickness = new Thickness();
                thickness.Left = 10;
                thickness.Right = 10;
                thickness.Top = 10;
                thickness.Bottom = 10;

                textBlock1.Padding = thickness;
                textBlock2.Padding = thickness;

                Grid.SetRow(textBlock1, 0);
                Grid.SetColumn(textBlock1, graph.GraphKnoten.IndexOf(i) + 1);

                Grid.SetRow(textBlock2, graph.GraphKnoten.IndexOf(i) + 1);
                Grid.SetColumn(textBlock2, 0);

                this.Table.Children.Add(textBlock1);
                this.Table.Children.Add(textBlock2);
            }

            //stelle die Liste im "Table" dar, sie gibt an wieviele Kanten die Knoten jeweils verbinden
            for (int i = 0; i < graph.Liste.GetLength(0); i++)
            {
                for (int f = 0; f < graph.Liste.GetLength(1); f++)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = graph.Liste[i, f].ToString();
                    Thickness thickness = new Thickness();
                    thickness.Left = 10;
                    thickness.Right = 10;
                    thickness.Top = 10;
                    thickness.Bottom = 10;
                    textBlock.Padding = thickness;
                    Grid.SetColumn(textBlock, f + 1);
                    Grid.SetRow(textBlock, i + 1);
                    this.Table.Children.Add(textBlock);
                }
            }
            #endregion*/
            #endregion
        }

        //Eventhandler, falls die Combobox in "KnotenGrid" geändert wird
        private void KnotenPickerChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //erstelle Knoten, welcher der ausgewählte Knoten ist
                GraphDarstellung.Knoten knoten = this.Graph.GraphKnoten[this.KnotenPicker.SelectedIndex];

                //lege die Eigenschaften fest
                this.KnotenName.Text = knoten.Name;
                this.KnotenParent.Text = knoten.Parent.Name;
                this.KnotenGrad.Text = knoten.Grad.ToString();

                //mache die Listbox "KnotenKanten"
                if (knoten.Grad == 0)
                {
                    //verstecke die Liste
                    this.KnotenKanten.Visibility = Visibility.Hidden;
                    this.KnotenKantenText.Visibility = Visibility.Hidden;

                    //leere die Liste
                    this.KnotenKanten.Items.Clear();
                }
                else
                {
                    //mache es sichtbar
                    this.KnotenKanten.Visibility = Visibility.Visible;
                    this.KnotenKantenText.Visibility = Visibility.Visible;

                    //leere die Liste
                    this.KnotenKanten.Items.Clear();

                    //füge die Namen der Kanten zur Liste hinzu
                    foreach (GraphDarstellung.Kanten i in knoten.Kanten)
                    {
                        ListBoxItem listBoxItem = new ListBoxItem();
                        listBoxItem.Content = i.Name;
                        this.KnotenKanten.Items.Add(listBoxItem);
                    }
                }

                //Tabelle
                #region
                int position = this.Graph.GraphKnoten.IndexOf(knoten);
                List<ElementsKnoten> list = new();
                for (int i = 0; i < this.Graph.GraphKnoten.Count; i++)
                {
                    int contentListe = this.Graph[position, i];
                    if (contentListe != 0)
                    {
                        list.Add(new ElementsKnoten(this.Graph.GraphKnoten[i].Name, contentListe));
                    }
                }
                this.Knoten.Header = MainWindow.resman.GetString("Knoten", MainWindow.cul);
                this.Werte.Header = MainWindow.resman.GetString("Kanten", MainWindow.cul);
                this.DataGridKnoten.ItemsSource = list;
                #endregion
            }
            catch { }
        }

        //Eventhandler, falls die Combobox in "KantenGrid" geändert wird
        private void KantenPickerChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //erstelle Kante, welcher die ausgewählte Kante ist
                GraphDarstellung.Kanten kante = this.Graph.GraphKanten[this.KantenPicker.SelectedIndex];

                //lege die Eigenschaften fest
                this.KantenName.Text = kante.Name;
                this.KantenParent.Text = kante.Parent.Name;
                this.KantenStart.Text = kante[0].Name;
                this.KantenEnde.Text = kante[1].Name;
            }
            catch { }
        }

        //private Klasse um die Eigenschaften des Graphens im "DataGrid" darzustellen
        private record Elements
        {
            public string Eigenschaft { get; set; }
            public string Wert { get; set; }
            public Elements(string eigenschaft, string wert)
            {
                this.Eigenschaft = eigenschaft;
                this.Wert = wert;
            }
        }

        private record ElementsKnoten
        {
            public string Node { get; set; }
            public int Edge { get; set; }
            public ElementsKnoten(string node, int edge)
            {
                this.Node = node;
                this.Edge = edge;
            }
        }

        private void KnotenName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Falls sich der Text in der TexBox "KnotenName" ändert
            if (this.KnotenName.Text != this.GetSelectedKnoten().Name)
            {
                this.UmbennenKnoten.Visibility = Visibility.Visible;
            }
            else
            {
                this.UmbennenKnoten.Visibility = Visibility.Hidden;
            }
        }

        private void UmbennenKnoten_Click(object sender, RoutedEventArgs e)
        {
            //Falls der Knoten umbenannt wird
            this.CommandConsole.Command("RENAME " + this.GetSelectedKnoten().Name + " TO " + this.KnotenName.Text);
            this.UmbennenKnoten.Visibility = Visibility.Hidden;
        }

        private void KantenName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Falls sich der Text in der TexBox "KantenName" ändert
            if (this.KantenName.Text != this.GetSelectedKante().Name)
            {
                this.UmbennenKanten.Visibility = Visibility.Visible;
            }
            else
            {
                this.UmbennenKanten.Visibility = Visibility.Hidden;
            }
        }

        private void UmbennenKanten_Click(object sender, RoutedEventArgs e)
        {
            //Falls die Kante umbenannt wird
            this.CommandConsole.Command("RENAME " + this.GetSelectedKante().Name + " TO " + this.KantenName.Text);
            this.UmbennenKanten.Visibility = Visibility.Hidden;
        }

        public GraphDarstellung.Knoten GetSelectedKnoten()
        {
            string[] vs = this.KnotenPicker.SelectedItem.ToString().Split(' ');
            return this.Graph.SucheKnoten(vs[vs.Length - 1]);
        }

        public GraphDarstellung.Kanten GetSelectedKante()
        {
            string[] vs = this.KantenPicker.SelectedItem.ToString().Split(' ');
            return this.Graph.SucheKanten(vs[vs.Length - 1]);
        }

        public void AktualisiereGrid()
        {
            //Aktualisiere das Fenster

            //DataGrid
            #region
            //erstelle Elemente für das DataGrid und rechne sie aus, stelle dann das DataGrid dar
            List<Elements> list = new List<Elements>();
            list.Add(new Elements("Ist Eulersch", this.Graph.IstEulersch.ToString()));
            list.Add(new Elements("Ist ein Baum", this.Graph.IstBaum.ToString()));
            list.Add(new Elements("Ist Bipartit", this.Graph.IstBipartit.ToString()));

            //Eigenschafts-Wert stimmt noch nicht ganz, wird bald überarbeitet
            //list.Add(new Elements("Ist Hamiltonsch", graph.IstHamiltonsch.ToString()));
            list.Add(new Elements("Ist Zusammenhängend", this.Graph.IstZusammenhängend.ToString()));
            list.Add(new Elements("Komponenten", this.Graph.AnzahlKomponenten.ToString()));
            list.Add(new Elements("Ist ein einfacher Graph", this.Graph.EinfacherGraph.ToString()));
            list.Add(new Elements("Parallele Kanten", this.Graph.ParalleleKanten.ToString()));
            list.Add(new Elements("Schlingen", this.Graph.Schlingen.ToString()));
            list.Add(new Elements("Anzahl an Knoten", this.Graph.AnzahlKnoten.ToString()));
            list.Add(new Elements("Anzahl an Kanten", this.Graph.AnzahlKanten.ToString()));
            this.DataGrid.ItemsSource = list;
            #endregion

            //KnotenPicker
            #region
            //schreibe in die ComboBox, welche Ecken es alle gibt
            this.KnotenContent.Children.Clear();//Leere das StackPanel "KnotenContent"
            if (this.Graph.GraphKnoten.Count == 0)
            {
                //falls es keine Knoten gibt, schreibe das anstatt von dem restlichen Inhalt von "KnotenContent"
                Thickness thickness = new Thickness();//für Margin
                thickness.Left = 20;
                thickness.Top = 10;
                thickness.Bottom = 10;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = MainWindow.resman.GetString("KeineKnoten", MainWindow.cul);
                textBlock.Margin = thickness;
                this.KnotenContent.Children.Add(textBlock);
            }
            else
            {
                //Aktualisiere "KnotenPicker"
                this.KnotenPicker.Items.Clear();
                foreach (GraphDarstellung.Knoten i in this.Graph.GraphKnoten)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem();
                    comboBoxItem.Content = i.Name;
                    this.KnotenPicker.Items.Add(comboBoxItem);
                }
                this.KnotenPicker.SelectedIndex = 0;

                //Füge wieder alle Elemente zum StackPanel "KnotenContent" hinzu
                this.KnotenContent.Children.Add(this.KnotenPickerText);
                this.KnotenContent.Children.Add(this.KnotenPicker);

                this.KnotenContent.Children.Add(this.KnotenNameText);
                this.KnotenContent.Children.Add(this.KnotenName);
                this.KnotenContent.Children.Add(this.UmbennenKnoten);

                this.KnotenContent.Children.Add(this.KnotenParentText);
                this.KnotenContent.Children.Add(this.KnotenParent);

                this.KnotenContent.Children.Add(this.KnotenGradText);
                this.KnotenContent.Children.Add(this.KnotenGrad);

                this.KnotenContent.Children.Add(this.KnotenKantenText);
                this.KnotenContent.Children.Add(this.KnotenKanten);

                this.KnotenContent.Children.Add(this.DataGridKnoten);
            }
            #endregion

            //KantenPicker
            #region
            //schreibe in die ComboBox, welche Kanten es alle gibt
            this.GridKanten.Children.Clear();//Leere das Grid "GridKanten"
            if (this.Graph.GraphKanten.Count == 0)
            {
                //falls es keine Kanten gibt, schreibe das anstatt von dem restlichen Inhalt von "GridKanten"
                Thickness thickness = new Thickness();//für Margin
                thickness.Left = 20;
                thickness.Top = 10;
                thickness.Bottom = 10;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = MainWindow.resman.GetString("KeineKanten", MainWindow.cul);
                textBlock.Margin = thickness;
                this.GridKanten.Children.Add(textBlock);
            }
            else
            {
                //schreibe in die ComboBox, welche Ecken es alle gibt
                this.KantenPicker.Items.Clear();
                foreach (GraphDarstellung.Kanten i in this.Graph.GraphKanten)
                {
                    ComboBoxItem comboBoxItem = new();
                    comboBoxItem.Content = i.Name;
                    this.KantenPicker.Items.Add(comboBoxItem);
                }
                this.KantenPicker.SelectedIndex = 0;

                //Füge wieder alle Elemente zum Grid "GridKanten" hinzu
                this.GridKanten.Children.Add(this.KantenPickerText);
                this.GridKanten.Children.Add(this.KantenPicker);

                this.GridKanten.Children.Add(this.KantenNameText);
                this.GridKanten.Children.Add(this.KantenName);
                this.GridKanten.Children.Add(this.UmbennenKanten);

                this.GridKanten.Children.Add(this.KantenParentText);
                this.GridKanten.Children.Add(this.KantenParent);

                this.GridKanten.Children.Add(this.KantenStartText);
                this.GridKanten.Children.Add(this.KantenStart);

                this.GridKanten.Children.Add(this.KantenEndeText);
                this.GridKanten.Children.Add(this.KantenEnde);
            }
            #endregion
        }

        //Methoden, um die Eigenschaften von Kanten und Knoten herauszufinden
        #region
        public void OpenNode(string name)
        {
            //Gehe zu dem Tab und wähle den Knoten aus
            this.TabControl.SelectedIndex = 1;
            this.KnotenPicker.SelectedIndex = this.Graph.GraphKnoten.IndexOf(this.Graph.SucheKnoten(name));
        }

        public void OpenNode(GraphDarstellung.Knoten knoten)
        {
            //Gehe zu dem Tab und wähle den Knoten aus
            this.TabControl.SelectedIndex = 1;
            this.KnotenPicker.SelectedIndex = this.Graph.GraphKnoten.IndexOf(knoten);
        }

        public void OpenEdge(string name)
        {
            //Gehe zu dem Tab und wähle die Kante aus
            this.TabControl.SelectedIndex = 2;
            this.KantenPicker.SelectedIndex = this.Graph.GraphKanten.IndexOf(this.Graph.SucheKanten(name));
        }

        public void OpenEdge(GraphDarstellung.Kanten kante)
        {
            //Gehe zu dem Tab und wähle die Kante aus
            this.TabControl.SelectedIndex = 2;
            this.KantenPicker.SelectedIndex = this.Graph.GraphKanten.IndexOf(kante);
        }
        #endregion
    }
}
