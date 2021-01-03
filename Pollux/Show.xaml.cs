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
        public Graph.Graph graph;

        public Show(Graph.Graph graph)
        {
            //Erstelle das Fenster
            InitializeComponent();

            //Initialisierung der Member
            this.graph = graph;

            //Übersetze die Texte und stelle sie dar
            #region
            this.Title = graph.Name + " - " + MainWindow.resman.GetString("Eigenschaften", MainWindow.cul);
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
            #endregion

            //DataGrid
            #region
            //erstelle Elemente für das DataGrid und rechne sie aus, stelle dann das DataGrid dar
            List<Elements> list = new List<Elements>();
            list.Add(new Elements("Ist Eulersch", graph.IstEulersch.ToString()));
            list.Add(new Elements("Ist ein Baum", graph.IstBaum.ToString()));
            list.Add(new Elements("Ist Bipartit", graph.IstBipartit.ToString()));

            //Eigenschafts-Wert stimmt noch nicht ganz, wird bald überarbeitet
            //list.Add(new Elements("Ist Hamiltonsch", graph.IstHamiltonsch.ToString()));
            list.Add(new Elements("Ist Zusammenhängend", graph.IstZusammenhängend.ToString()));
            list.Add(new Elements("Komponenten", graph.AnzahlKomponenten.ToString()));
            list.Add(new Elements("Ist ein einfacher Graph", graph.EinfacherGraph.ToString()));
            list.Add(new Elements("Parallele Kanten", graph.ParalleleKanten.ToString()));
            list.Add(new Elements("Schlingen", graph.Schlingen.ToString()));
            list.Add(new Elements("Anzahl an Knoten", graph.AnzahlKnoten.ToString()));
            list.Add(new Elements("Anzahl an Kanten", graph.AnzahlKanten.ToString()));
            this.Wert.Header = MainWindow.resman.GetString("WertDataGrid", MainWindow.cul);
            this.Eigenschaft.Header = MainWindow.resman.GetString("EigenschaftDataGrid", MainWindow.cul);
            this.DataGrid.ItemsSource = list;
            #endregion

            //KnotenPicker
            #region
            //schreibe in die ComboBox, welche Ecken es alle gibt

            foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = i.Name;
                this.KnotenPicker.Items.Add(comboBoxItem);
            }
            this.KnotenPicker.SelectionChanged += KnotenPickerChanged;
            this.KnotenPicker.SelectedIndex = 0;
            #endregion

            //KantenPicker
            #region
            //schreibe in die ComboBox, welche Kanten es alle gibt

            if (graph.GraphKanten.Count == 0)
            {
                //falls es keine Kanten gibt, schreibe das anstatt von dem restlichen Inhalt von "GridKanten"
                Thickness thickness = new Thickness();//für Margin
                thickness.Left = 20;
                thickness.Top = 10;
                thickness.Bottom = 10;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = MainWindow.resman.GetString("KeineKanten", MainWindow.cul);
                textBlock.Margin = thickness;
                this.GridKanten.Children.Clear();
                this.GridKanten.Children.Add(textBlock);
            }
            else
            {
                //schreibe in die ComboBox, welche Ecken es alle gibt
                foreach (Graph.Graph.Kanten i in graph.GraphKanten)
                {
                    ComboBoxItem comboBoxItem = new ComboBoxItem();
                    comboBoxItem.Content = i.Name;
                    this.KantenPicker.Items.Add(comboBoxItem);
                }
                this.KantenPicker.SelectionChanged += KantenPickerChanged;
                this.KantenPicker.SelectedIndex = 0;
            }
            #endregion

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
        }

        //Eventhandler, falls die Combobox in "KnotenGrid" geändert wird
        private void KnotenPickerChanged(object sender, SelectionChangedEventArgs e)
        {
            //erstelle Knoten, welcher der ausgewählte Knoten ist
            Graph.Graph.Knoten knoten = this.graph.GraphKnoten[this.KnotenPicker.SelectedIndex];

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
                foreach (Graph.Graph.Kanten i in knoten.Kanten)
                {
                    ListBoxItem listBoxItem = new ListBoxItem();
                    listBoxItem.Content = i.Name;
                    this.KnotenKanten.Items.Add(listBoxItem);
                }
            }

            //Tabelle
            #region
            List<ElementsKnoten> list = new();
            for (int i = 0; i < this.graph.GraphKnoten.Count; i++)
            {
                list.Add(new ElementsKnoten(this.graph.GraphKnoten[i].Name, this.graph[this.graph.GraphKnoten.IndexOf(knoten), i]));
            }
            this.Knoten.Header = MainWindow.resman.GetString("Knoten", MainWindow.cul);
            this.Werte.Header = MainWindow.resman.GetString("Kanten", MainWindow.cul);
            this.DataGridKnoten.ItemsSource = list;
            #endregion
        }

        //Eventhandler, falls die Combobox in "KantenGrid" geändert wird
        private void KantenPickerChanged(object sender, SelectionChangedEventArgs e)
        {
            //erstelle Kante, welcher die ausgewählte Kante ist
            Graph.Graph.Kanten kante = this.graph.GraphKanten[this.KantenPicker.SelectedIndex];

            //lege die Eigenschaften fest
            this.KantenName.Text = kante.Name;
            this.KantenParent.Text = kante.Parent.Name;
            this.KantenStart.Text = kante[0].Name;
            this.KantenEnde.Text = kante[1].Name;
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
    }
}
