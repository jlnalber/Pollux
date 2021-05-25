using Castor.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Thestias;

namespace Castor
{
    /// <summary>
    /// Interaktionslogik für KantenEllipse.xaml
    /// </summary>
    public partial class Show : UserControl
    {
        public bool AllowSounds = true;
        private VisualGraph graph;
        public VisualGraph Graph
        {
            get
            {
                return graph;
            }
            set
            {
                this.graph = value;
                this.AktualisiereGrid();
            }
        }

        public Show()
        {
            InitializeComponent();

            if (VisualGraph.Resman == null)
            {
                //initialisiere Resource und rufe die Kultur ab
                VisualGraph.Resman = new ResourceManager(typeof(Resources));
                VisualGraph.Cul = CultureInfo.CurrentUICulture;
                //cul = new CultureInfo("en");
                //cul = new CultureInfo("fr");
            }

            //Übersetze die Texte und stelle sie dar
            #region
            this.Eigenschaft.Header = VisualGraph.Resman.GetString("Eigenschaft", VisualGraph.Cul);
            this.Wert.Header = VisualGraph.Resman.GetString("Wert", VisualGraph.Cul);
            this.KnotenPickerText.Text = VisualGraph.Resman.GetString("KnotenPickerText", VisualGraph.Cul);
            this.KantenPickerText.Text = VisualGraph.Resman.GetString("KantenPickerText", VisualGraph.Cul);
            this.KnotenNameText.Text = VisualGraph.Resman.GetString("KnotenNameText", VisualGraph.Cul);
            this.KnotenParentText.Text = VisualGraph.Resman.GetString("KnotenParentText", VisualGraph.Cul);
            this.KnotenGradText.Text = VisualGraph.Resman.GetString("KnotenGradText", VisualGraph.Cul);
            this.KnotenKantenText.Text = VisualGraph.Resman.GetString("KnotenKantenText", VisualGraph.Cul);
            this.KantenNameText.Text = VisualGraph.Resman.GetString("KantenNameText", VisualGraph.Cul);
            this.KantenParentText.Text = VisualGraph.Resman.GetString("KantenParentText", VisualGraph.Cul);
            this.KantenStartText.Text = VisualGraph.Resman.GetString("KantenStartText", VisualGraph.Cul);
            this.KantenEndeText.Text = VisualGraph.Resman.GetString("KantenEndeText", VisualGraph.Cul);
            this.KantenTab.Header = VisualGraph.Resman.GetString("KantenTab_Header", VisualGraph.Cul);
            this.KnotenTab.Header = VisualGraph.Resman.GetString("KnotenTab_Header", VisualGraph.Cul);
            this.GraphTab.Header = VisualGraph.Resman.GetString("GraphTab_Header", VisualGraph.Cul);
            this.UmbennenKnoten.Content = VisualGraph.Resman.GetString("UmbennenKnoten", VisualGraph.Cul);
            this.UmbennenKanten.Content = VisualGraph.Resman.GetString("UmbennenKanten", VisualGraph.Cul);
            this.KnotenLöschen.Content = VisualGraph.Resman.GetString("KnotenLöschenContent", VisualGraph.Cul);
            this.KanteLöschen.Content = VisualGraph.Resman.GetString("KanteLöschenContent", VisualGraph.Cul);

            this.Knoten_Design_Text.Header = VisualGraph.Resman.GetString("Knoten_Design_Text", VisualGraph.Cul);

            this.Knoten_DesignFilling_Text.Text = VisualGraph.Resman.GetString("Knoten_DesignFilling_Text", VisualGraph.Cul);

            this.Knoten_DesignFilling2_CheckBox.Content = VisualGraph.Resman.GetString("Knoten_DesignFilling2_CheckBox", VisualGraph.Cul);

            this.Knoten_DesignBorder_Text.Text = VisualGraph.Resman.GetString("Knoten_DesignBorder_Text", VisualGraph.Cul);

            this.Knoten_DesignSizes_Text.Text = VisualGraph.Resman.GetString("Knoten_DesignSizes_Text", VisualGraph.Cul);
            this.Slider_Knoten_Size_Text.Text = VisualGraph.Resman.GetString("Slider_Knoten_Size_Text", VisualGraph.Cul);
            this.Slider_Knoten_SizeStroke_Text.Text = VisualGraph.Resman.GetString("Slider_Knoten_SizeStroke_Text", VisualGraph.Cul);

            this.Kanten_Design_Text.Header = VisualGraph.Resman.GetString("Kanten_Design_Text", VisualGraph.Cul);
            this.Kanten_DesignBorder_Text.Text = VisualGraph.Resman.GetString("Kanten_DesignBorder_Text", VisualGraph.Cul);

            this.Kanten_DesignSizes_Text.Text = VisualGraph.Resman.GetString("Kanten_DesignSizes_Text", VisualGraph.Cul);
            this.Slider_Kanten_SizeStroke_Text.Text = VisualGraph.Resman.GetString("Slider_Kanten_SizeStroke_Text", VisualGraph.Cul);
            #endregion

            //Darstellung
            #region
            //DataGrid
            this.Wert.Header = VisualGraph.Resman.GetString("WertDataGrid", VisualGraph.Cul);
            this.Eigenschaft.Header = VisualGraph.Resman.GetString("EigenschaftDataGrid", VisualGraph.Cul);

            //KnotenPicker
            this.KnotenPicker.SelectionChanged += this.KnotenPickerChanged;
            this.KnotenPicker.SelectedIndex = 0;

            //KantenPicker
            this.KantenPicker.SelectionChanged += this.KantenPickerChanged;
            this.KantenPicker.SelectedIndex = 0;

            this.KnotenFilling.ColorChanged += this.Vertex_ValuesChanged;
            this.KnotenFilling2.ColorChanged += this.Vertex_ValuesChanged;
            this.KnotenBorder.ColorChanged += this.Vertex_ValuesChanged;

            this.KantenBorder.ColorChanged += this.Edge_ValuesChanged;
            #endregion
        }

        public Show(VisualGraph graph)
        {
            //Erstelle das Fenster
            InitializeComponent();

            if (VisualGraph.Resman == null)
            {
                //initialisiere Resource und rufe die Kultur ab
                VisualGraph.Resman = new ResourceManager(typeof(Resources));
                VisualGraph.Cul = CultureInfo.CurrentUICulture;
                //cul = new CultureInfo("en");
                //cul = new CultureInfo("fr");
            }

            //Übersetze die Texte und stelle sie dar
            #region
            this.Eigenschaft.Header = VisualGraph.Resman.GetString("Eigenschaft", VisualGraph.Cul);
            this.Wert.Header = VisualGraph.Resman.GetString("Wert", VisualGraph.Cul);
            this.KnotenPickerText.Text = VisualGraph.Resman.GetString("KnotenPickerText", VisualGraph.Cul);
            this.KantenPickerText.Text = VisualGraph.Resman.GetString("KantenPickerText", VisualGraph.Cul);
            this.KnotenNameText.Text = VisualGraph.Resman.GetString("KnotenNameText", VisualGraph.Cul);
            this.KnotenParentText.Text = VisualGraph.Resman.GetString("KnotenParentText", VisualGraph.Cul);
            this.KnotenGradText.Text = VisualGraph.Resman.GetString("KnotenGradText", VisualGraph.Cul);
            this.KnotenKantenText.Text = VisualGraph.Resman.GetString("KnotenKantenText", VisualGraph.Cul);
            this.KantenNameText.Text = VisualGraph.Resman.GetString("KantenNameText", VisualGraph.Cul);
            this.KantenParentText.Text = VisualGraph.Resman.GetString("KantenParentText", VisualGraph.Cul);
            this.KantenStartText.Text = VisualGraph.Resman.GetString("KantenStartText", VisualGraph.Cul);
            this.KantenEndeText.Text = VisualGraph.Resman.GetString("KantenEndeText", VisualGraph.Cul);
            this.KantenTab.Header = VisualGraph.Resman.GetString("KantenTab_Header", VisualGraph.Cul);
            this.KnotenTab.Header = VisualGraph.Resman.GetString("KnotenTab_Header", VisualGraph.Cul);
            this.GraphTab.Header = VisualGraph.Resman.GetString("GraphTab_Header", VisualGraph.Cul);
            this.UmbennenKnoten.Content = VisualGraph.Resman.GetString("UmbennenKnoten", VisualGraph.Cul);
            this.UmbennenKanten.Content = VisualGraph.Resman.GetString("UmbennenKanten", VisualGraph.Cul);
            this.KnotenLöschen.Content = VisualGraph.Resman.GetString("KnotenLöschenContent", VisualGraph.Cul);
            this.KanteLöschen.Content = VisualGraph.Resman.GetString("KanteLöschenContent", VisualGraph.Cul);

            this.Knoten_Design_Text.Header = VisualGraph.Resman.GetString("Knoten_Design_Text", VisualGraph.Cul);

            this.Knoten_DesignFilling_Text.Text = VisualGraph.Resman.GetString("Knoten_DesignFilling_Text", VisualGraph.Cul);

            this.Knoten_DesignFilling2_CheckBox.Content = VisualGraph.Resman.GetString("Knoten_DesignFilling2_CheckBox", VisualGraph.Cul);

            this.Knoten_DesignBorder_Text.Text = VisualGraph.Resman.GetString("Knoten_DesignBorder_Text", VisualGraph.Cul);

            this.Knoten_DesignSizes_Text.Text = VisualGraph.Resman.GetString("Knoten_DesignSizes_Text", VisualGraph.Cul);
            this.Slider_Knoten_Size_Text.Text = VisualGraph.Resman.GetString("Slider_Knoten_Size_Text", VisualGraph.Cul);
            this.Slider_Knoten_SizeStroke_Text.Text = VisualGraph.Resman.GetString("Slider_Knoten_SizeStroke_Text", VisualGraph.Cul);

            this.Kanten_Design_Text.Header = VisualGraph.Resman.GetString("Kanten_Design_Text", VisualGraph.Cul);
            this.Kanten_DesignBorder_Text.Text = VisualGraph.Resman.GetString("Kanten_DesignBorder_Text", VisualGraph.Cul);

            this.Kanten_DesignSizes_Text.Text = VisualGraph.Resman.GetString("Kanten_DesignSizes_Text", VisualGraph.Cul);
            this.Slider_Kanten_SizeStroke_Text.Text = VisualGraph.Resman.GetString("Slider_Kanten_SizeStroke_Text", VisualGraph.Cul);
            #endregion

            //Darstellung
            #region
            //DataGrid
            this.Wert.Header = VisualGraph.Resman.GetString("WertDataGrid", VisualGraph.Cul);
            this.Eigenschaft.Header = VisualGraph.Resman.GetString("EigenschaftDataGrid", VisualGraph.Cul);

            //KnotenPicker
            this.KnotenPicker.SelectionChanged += this.KnotenPickerChanged;
            this.KnotenPicker.SelectedIndex = 0;

            //KantenPicker
            this.KantenPicker.SelectionChanged += this.KantenPickerChanged;
            this.KantenPicker.SelectedIndex = 0;

            this.KnotenFilling.ColorChanged += this.Vertex_ValuesChanged;
            this.KnotenFilling2.ColorChanged += this.Vertex_ValuesChanged;
            this.KnotenBorder.ColorChanged += this.Vertex_ValuesChanged;

            this.KantenBorder.ColorChanged += this.Edge_ValuesChanged;

            //Lasse das Grid aktualisieren und weise den Graphen zu.
            this.Graph = graph;
            #endregion
        }

        private void KnotenPickerChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //erstelle Knoten, welcher der ausgewählte Knoten ist
                VisualVertex knoten = this.GetSelectedKnoten();

                //lege die Eigenschaften fest
                this.KnotenName.Text = knoten.Vertex.Name;
                this.KnotenParent.Text = knoten.Vertex.Parent.Name;
                int grad = knoten.Vertex.Degree;
                this.KnotenGrad.Text = grad.ToString();

                //mache die Listbox "KnotenKanten"
                if (grad == 0)
                {
                    //verstecke die Liste
                    this.KnotenKanten.Visibility = Visibility.Collapsed;
                    this.KnotenKantenText.Visibility = Visibility.Collapsed;

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
                    foreach (Graph.Edge i in knoten.Vertex.Edges)
                    {
                        ListBoxItem listBoxItem = new ListBoxItem();
                        listBoxItem.Content = i.Name;
                        this.KnotenKanten.Items.Add(listBoxItem);
                    }
                }

                //Design des Knoten
                #region
                if (knoten.UIElement is VertexEllipse knotenEllipse)
                {
                    //Rufe die Daten ab
                    LinearGradientBrush brushNode = (LinearGradientBrush)knotenEllipse.Ellipse.Fill;
                    Color color1 = brushNode.GradientStops[0].Color;
                    Color color2 = brushNode.GradientStops[1].Color;
                    SolidColorBrush brushStrokeNode = (SolidColorBrush)knotenEllipse.Ellipse.Stroke;
                    double height = knotenEllipse.Ellipse.Height;
                    double strokeThickness = knotenEllipse.Ellipse.StrokeThickness;

                    //Synchronisiere die Slider mit den Farben
                    this.KnotenFilling.SetColor(color1);
                    this.KnotenFilling2.SetColor(color2);
                    this.KnotenBorder.SetSolidColorBrush(brushStrokeNode);

                    this.Knoten_DesignFilling2_CheckBox.IsChecked = !(color1.A == color2.A && color1.R == color2.R && color1.G == color2.G && color1.B == color2.B);

                    //Synchronisiere die Slider mit der Dicke und der Größe
                    this.Slider_Knoten_Size.Value = height;
                    this.Slider_Knoten_SizeStroke.Value = strokeThickness;

                    //Mache alle Elemente sichtbar.
                    this.SetKnotenVisibility(Visibility.Visible);
                }
                else
                {
                    this.SetKnotenVisibility(Visibility.Collapsed);
                }
                #endregion

                //Tabelle
                #region
                this.KnotenContent.Children.Remove(this.DataGridKnoten);//Entferne das DataGrid "DataGridKnoten", um sicherzustellen, dass es auch wirklich nicht mehr drin ist.

                //Aktualisiere das DataGrid "DataGridKnoten" auch nur, wenn es auch Verbindungen mit anderen Knoten hat.
                if (this.GetSelectedKnoten().Vertex.Degree != 0)
                {
                    int position = this.Graph.Graph.Vertices.IndexOf(knoten.Vertex);
                    HashSet<ElementsKnoten> list = new();
                    for (int i = 0; i < this.Graph.Graph.Vertices.Count; ++i)
                    {
                        int contentListe = this.Graph.Graph[position, i];
                        if (contentListe != 0)
                        {
                            list.Add(new ElementsKnoten(this.Graph.Graph.Vertices[i].Name, contentListe));
                        }
                    }
                    this.Knoten.Header = VisualGraph.Resman.GetString("Knoten", VisualGraph.Cul);
                    this.Werte.Header = VisualGraph.Resman.GetString("Kanten", VisualGraph.Cul);
                    this.DataGridKnoten.ItemsSource = list;
                    this.KnotenContent.Children.Add(this.DataGridKnoten);//Füge das DataGrid "DataGridKnoten" wieder hinzu
                }
                #endregion
            }
            catch { }
        }

        private void SetKnotenVisibility(Visibility visibility)
        {
            this.Knoten_Design_Text.Visibility = visibility;
        }

        private void KantenPickerChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Suche nach der ausgewählten Kante.
                VisualEdge kante = this.GetSelectedKante();

                //Lege die Eigenschaften fest.
                this.KantenName.Text = kante.Edge.Name;
                this.KantenParent.Text = kante.Edge.Parent.Name;
                this.KantenStart.Text = kante[0].Vertex.Name;
                this.KantenEnde.Text = kante[1].Vertex.Name;

                //Design der Kante
                this.KantenBorder.SetSolidColorBrush(kante.Line.GetStroke());
                double strokeWidth = Math.Round(kante.Line.GetStrokeThickness(), 0);
                this.Slider_Kanten_SizeStroke.Value = strokeWidth;
            }
            catch { }
        }


        //Records, die die Daten für die DataGrids "KnotenDataGrid" und "DataGrid" übergeben
        #region
        //Record, der dem DataGrid "DataGrid" die Daten übergibt
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

        //Record, der dem DataGrid "KnotenDataGrid" die Daten übergibt
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
        #endregion


        private void KnotenName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                //Falls sich der Text in der TexBox "KnotenName" ändert
                if (this.KnotenName.Text != this.GetSelectedKnoten().Vertex.Name)
                {
                    this.UmbennenKnoten.Visibility = Visibility.Visible;
                }
                else
                {
                    this.UmbennenKnoten.Visibility = Visibility.Hidden;
                }

                //Falls Enter gedrückt wird, benenne den Knoten um.
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    UmbennenKnoten_Click(sender, new());
                }
            }
            catch { }
        }

        private void UmbennenKnoten_Click(object sender, RoutedEventArgs e)
        {
            //Falls der Knoten umbenannt wird
            int index = this.KnotenPicker.SelectedIndex;//Index des aktuell geöffneten Knotens
            try
            {
                this.GetSelectedKnoten().Vertex.Name = this.KnotenName.Text;
            }
            catch
            {
                if (this.AllowSounds)
                {
                    SystemSounds.Asterisk.Play();
                }
            }
            this.KnotenName.Text = this.GetSelectedKnoten().Vertex.Name;
            this.KnotenPicker.SelectedIndex = index;//Öffne den vorher ausgewählten Knoten
            this.UmbennenKnoten.Visibility = Visibility.Hidden;
        }

        private void KantenName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                //Falls sich der Text in der TexBox "KantenName" ändert
                if (this.KantenName.Text != this.GetSelectedKante().Edge.Name)
                {
                    this.UmbennenKanten.Visibility = Visibility.Visible;
                }
                else
                {
                    this.UmbennenKanten.Visibility = Visibility.Hidden;
                }

                //Falls Enter gedrückt wird, benenne die Kante um.
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    UmbennenKanten_Click(sender, new());
                }
            }
            catch { }
        }

        private void UmbennenKanten_Click(object sender, RoutedEventArgs e)
        {
            //Falls die Kante umbenannt wird.
            int index = this.KantenPicker.SelectedIndex;//Index der aktuell geöffneten Kante.
            try
            {
                this.GetSelectedKante().Edge.Name = this.KantenName.Text;
            }
            catch
            {
                if (this.AllowSounds)
                {
                    SystemSounds.Asterisk.Play();
                }
            }
            this.KantenName.Text = this.GetSelectedKante().Edge.Name;
            this.KantenPicker.SelectedIndex = index;//Öffne die vorher ausgewählte Kante.
            this.UmbennenKanten.Visibility = Visibility.Hidden;
        }

        public VisualVertex GetSelectedKnoten()
        {
            try
            {
                return this.Graph.SearchVertex(((ComboBoxItem)this.KnotenPicker.SelectedItem).Content.ToString());
            }
            catch
            {
                string[] vs = this.KnotenPicker.SelectedItem.ToString().Split(' ');
                return this.Graph.SearchVertex(vs[vs.Length - 1]);
            }
        }

        public string GetSelectedKnotenName()
        {
            try
            {
                return ((ComboBoxItem)this.KnotenPicker.SelectedItem).Content.ToString();
            }
            catch
            {
                string[] vs = this.KnotenPicker.SelectedItem.ToString().Split(' ');
                return vs[vs.Length - 1];
            }
        }

        public VisualEdge GetSelectedKante()
        {
            try
            {
                return this.Graph.SearchEdge(((ComboBoxItem)this.KantenPicker.SelectedItem).Content.ToString());
            }
            catch
            {
                string[] vs = this.KantenPicker.SelectedItem.ToString().Split(' ');
                return this.Graph.SearchEdge(vs[vs.Length - 1]);
            }
        }

        public string GetSelectedKantenName()
        {
            try
            {
                return ((ComboBoxItem)this.KantenPicker.SelectedItem).Content.ToString();
            }
            catch
            {
                string[] vs = this.KantenPicker.SelectedItem.ToString().Split(' ');
                return vs[vs.Length - 1];
            }
        }

        public void AktualisiereGrid()
        {
            try
            {
                //Aktualisiere das Fenster

                //DataGrid
                #region
                //erstelle Elemente für das DataGrid und rechne sie aus, stelle dann das DataGrid dar
                HashSet<Elements> list = new HashSet<Elements>();
                Graph thestiasGraph = this.Graph.Graph;
                list.Add(new Elements("Ist Eulersch", thestiasGraph.IstEulersch.ToString()));
                list.Add(new Elements("Ist ein Baum", thestiasGraph.IstBaum.ToString()));
                list.Add(new Elements("Ist Bipartit", thestiasGraph.IstBipartit.ToString()));

                //Eigenschafts-Wert stimmt noch nicht ganz, wird bald überarbeitet
                //list.Add(new Elements("Ist Hamiltonsch", graph.IstHamiltonsch.ToString()));
                list.Add(new Elements("Ist Zusammenhängend", thestiasGraph.IstZusammenhängend.ToString()));
                list.Add(new Elements("Komponenten", thestiasGraph.AnzahlKomponenten.ToString()));
                list.Add(new Elements("Ist ein einfacher Graph", thestiasGraph.EinfacherGraph.ToString()));
                list.Add(new Elements("Parallele Kanten", thestiasGraph.ParalleleKanten.ToString()));
                list.Add(new Elements("Schlingen", thestiasGraph.Schlingen.ToString()));
                list.Add(new Elements("Anzahl an Knoten", thestiasGraph.AnzahlKnoten.ToString()));
                list.Add(new Elements("Anzahl an Kanten", thestiasGraph.AnzahlKanten.ToString()));
                this.DataGrid.ItemsSource = list;
                #endregion

                //KnotenPicker
                #region
                this.KnotenContent.Children.Clear();
                //schreibe in die ComboBox, welche Ecken es alle gibt.
                if (this.Graph.Vertices.Count == 0)
                {
                    this.VisibilityKnotenContent(Visibility.Collapsed);

                    //falls es keine Knoten gibt, schreibe das anstatt von dem restlichen Inhalt von "KnotenContent"
                    Thickness thickness = new Thickness();//für Margin
                    thickness.Left = 20;
                    thickness.Top = 10;
                    thickness.Bottom = 10;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = VisualGraph.Resman.GetString("KeineKnoten", VisualGraph.Cul);
                    textBlock.Margin = thickness;
                    this.KnotenContent.Children.Add(textBlock);
                }
                else
                {
                    //Aktualisiere "KnotenPicker"
                    int index = this.KnotenPicker.SelectedIndex;
                    List<ComboBoxItem> removeThoseNodes = new();
                    foreach (ComboBoxItem i in this.KnotenPicker.Items)
                    {
                        if (!this.Graph.ContainsVertex((string)i.Content))
                        {
                            removeThoseNodes.Add(i);
                        }
                    }

                    foreach (ComboBoxItem i in removeThoseNodes)
                    {
                        this.KnotenPicker.Items.Remove(i);
                    }

                    foreach (VisualVertex i in this.Graph.Vertices)
                    {
                        if ((from ComboBoxItem n in this.KnotenPicker.Items where (string)n.Content == i.Vertex.Name select n).Count() == 0)
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = i.Vertex.Name;
                            this.KnotenPicker.Items.Add(comboBoxItem);
                        }
                    }
                    this.KnotenPicker.SelectedIndex = (index < 0) ? 0 : (index >= this.KnotenPicker.Items.Count) ? this.KnotenPicker.Items.Count - 1 : index;

                    //Füge wieder alle Elemente zum StackPanel "KnotenContent" hinzu
                    this.KnotenContent.Children.Add(this.KnotenPickerText);
                    this.KnotenContent.Children.Add(this.KnotenPicker);

                    this.KnotenContent.Children.Add(this.KnotenNameText);
                    this.KnotenContent.Children.Add(this.KnotenName);
                    this.KnotenContent.Children.Add(this.UmbennenKnoten);

                    this.KnotenContent.Children.Add(this.KnotenLöschen);

                    this.KnotenContent.Children.Add(this.KnotenParentText);
                    this.KnotenContent.Children.Add(this.KnotenParent);

                    this.KnotenContent.Children.Add(this.KnotenGradText);
                    this.KnotenContent.Children.Add(this.KnotenGrad);

                    this.KnotenContent.Children.Add(this.KnotenKantenText);
                    this.KnotenContent.Children.Add(this.KnotenKanten);

                    this.KnotenContent.Children.Add(this.Knoten_Design_Text);

                    if (this.KnotenContent.Children.Contains(this.DataGridKnoten))
                    {
                        this.KnotenContent.Children.Remove(this.DataGridKnoten);
                    }
                    if (this.GetSelectedKnoten().Vertex.Degree != 0)
                    {
                        this.KnotenContent.Children.Add(this.DataGridKnoten);
                    }
                }
                #endregion

                //KantenPicker
                #region
                //schreibe in die ComboBox, welche Kanten es alle gibt
                this.GridKanten.Children.Clear();//Leere das Grid "GridKanten"
                if (this.Graph.Edges.Count == 0)
                {
                    //falls es keine Kanten gibt, schreibe das anstatt von dem restlichen Inhalt von "GridKanten"
                    Thickness thickness = new Thickness();//für Margin
                    thickness.Left = 20;
                    thickness.Top = 10;
                    thickness.Bottom = 10;

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = VisualGraph.Resman.GetString("KeineKanten", VisualGraph.Cul);
                    textBlock.Margin = thickness;
                    this.GridKanten.Children.Add(textBlock);
                }
                else
                {
                    //schreibe in die ComboBox, welche Kanten es alle gibt
                    int index = this.KantenPicker.SelectedIndex;
                    List<ComboBoxItem> removeThoseEdges = new();
                    foreach (ComboBoxItem i in this.KantenPicker.Items)
                    {
                        if (!this.Graph.ContainsEdge((string)i.Content))
                        {
                            removeThoseEdges.Add(i);
                        }
                    }

                    foreach (ComboBoxItem i in removeThoseEdges)
                    {
                        this.KantenPicker.Items.Remove(i);
                    }

                    foreach (VisualEdge i in this.Graph.Edges)
                    {
                        if ((from ComboBoxItem n in this.KantenPicker.Items where (string)n.Content == i.Edge.Name select n).Count() == 0)
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = i.Edge.Name;
                            this.KantenPicker.Items.Add(comboBoxItem);
                        }
                    }
                    this.KantenPicker.SelectedIndex = (index < 0) ? 0 : (index >= this.KantenPicker.Items.Count) ? this.KantenPicker.Items.Count - 1 : index;

                    //Füge wieder alle Elemente zum Grid "GridKanten" hinzu
                    this.GridKanten.Children.Add(this.KantenPickerText);
                    this.GridKanten.Children.Add(this.KantenPicker);

                    this.GridKanten.Children.Add(this.KantenNameText);
                    this.GridKanten.Children.Add(this.KantenName);
                    this.GridKanten.Children.Add(this.UmbennenKanten);

                    this.GridKanten.Children.Add(this.KanteLöschen);

                    this.GridKanten.Children.Add(this.KantenParentText);
                    this.GridKanten.Children.Add(this.KantenParent);

                    this.GridKanten.Children.Add(this.KantenStartText);
                    this.GridKanten.Children.Add(this.KantenStart);

                    this.GridKanten.Children.Add(this.KantenEndeText);
                    this.GridKanten.Children.Add(this.KantenEnde);

                    this.GridKanten.Children.Add(this.Kanten_Design_Text);
                }
            }
            catch { }
            #endregion
        }

        private void VisibilityKnotenContent(Visibility visibility)
        {
            foreach (UIElement i in this.KnotenContent.Children)
            {
                i.Visibility = visibility;
            }
        }

        //Methoden, die für die Darstellung der Knoten verantwortlich sind
        #region
        private void Knoten_DesignFilling2_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.KnotenFilling2.Enabled = true;
                this.Vertex_ValuesChanged(sender, e);
            }
            catch { }
        }

        private void Knoten_DesignFilling2_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.KnotenFilling2.Enabled = false;
                this.Vertex_ValuesChanged(sender, e);
            }
            catch { }
        }

        private void TextBoxes_KnotenDarstellung_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Falls in einer der TextBoxen "Enter" gedrückt wird, synchronisiere die TextBoxen mit den Slidern
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.SyncSlidersAndTextBoxes_KnotenDarstellung(true);
            }
        }

        private void TextBoxes_KnotenDarstellung_LostFocus(object sender, RoutedEventArgs e)
        {
            //Wenn eine TextBox den Fokus verliert, synchronisiere die TextBoxen mit den Slidern
            this.SyncSlidersAndTextBoxes_KnotenDarstellung(true);
        }

        private void SyncSlidersAndTextBoxes_KnotenDarstellung(bool playErrorSound)
        {
            bool errorSound = false;//Variable, die angibt, ob nachher wirklich ein Error-Sound gespielt werden muss

            //Größe der Knoten
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_Knoten_Size.Value = double.Parse(this.TextBox_Knoten_Size.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_Knoten_Size" zurück
                errorSound = playErrorSound;
                this.TextBox_Knoten_Size.Text = this.Slider_Knoten_Size.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_Knoten_SizeStroke.Value = double.Parse(this.TextBox_Knoten_SizeStroke.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_Knoten_SizeStroke" zurück
                errorSound = playErrorSound;
                this.TextBox_Knoten_SizeStroke.Text = this.Slider_Knoten_SizeStroke.Value.ToString();
            }

            //Spiele einen Error-Sound, falls etwas schiefging
            if (errorSound && this.AllowSounds)
            {
                SystemSounds.Asterisk.Play();
            }
        }

        private void Vertex_ValuesChanged(object sender, EventArgs e)
        {
            try
            {
                //Finde den Wert für die Größe der Knoten heraus
                double vertex_Size = Math.Round(this.Slider_Knoten_Size.Value, 2);
                double vertex_SizeStroke = Math.Round(this.Slider_Knoten_SizeStroke.Value, 2);

                //Synchronisiere die TextBoxen mit den Slidern
                this.TextBox_Knoten_Size.Text = vertex_Size.ToString();
                this.TextBox_Knoten_SizeStroke.Text = vertex_SizeStroke.ToString();

                //Wende die Änderungen für den Knoten an
                //Suche den Knoten heraus
                VisualVertex vertex = this.GetSelectedKnoten();

                if (vertex.UIElement is VertexEllipse vertexEllipse)
                {
                    //Lege die Höhen/Breiten/Dicken für die Knoten fest
                    vertexEllipse.Ellipse.StrokeThickness = vertex_SizeStroke;
                    vertexEllipse.Ellipse.Height = vertex_Size;
                    vertexEllipse.Ellipse.Width = vertex_Size;
                    vertexEllipse.Height = vertex_Size;
                    vertexEllipse.Width = vertex_Size;

                    //Finde die Farben für den Vertex heraus und lege sie fest.
                    vertexEllipse.Ellipse.Stroke = this.KnotenBorder.GetBrush();
                    if (Knoten_DesignFilling2_CheckBox.IsChecked == true)
                    {
                        vertexEllipse.Ellipse.Fill = new LinearGradientBrush(this.KnotenFilling.GetColor(), this.KnotenFilling2.GetColor(), 45);
                    }
                    else
                    {

                        vertexEllipse.Ellipse.Fill = new LinearGradientBrush(this.KnotenFilling.GetColor(), this.KnotenFilling.GetColor(), 45);
                    }
                }

                vertex.Redraw(false);
            }
            catch { }
        }
        #endregion


        //Methoden, die für die Darstellung der Kanten verantwortlich sind
        #region
        private void Edge_ValuesChanged(object sender, EventArgs e)
        {
            try
            {
                //Finde den Wert für die Größen der Kante aus.
                double kante_SizeStroke = Math.Round(this.Slider_Kanten_SizeStroke.Value, 2);

                //Lege die gerundeten Werte für die Slider fest, sodass sie keine Gleitkommazahlen enthalten können.
                this.Slider_Kanten_SizeStroke.Value = kante_SizeStroke;

                //Wende die Änderungen für die Kante an.
                //Suche die ausgewählte Kante
                VisualEdge kante = this.GetSelectedKante();

                //Fahre die Farbe der Kante nach
                kante.Line.SetStroke(this.KantenBorder.GetColor());
                kante.Line.SetStrokeThicknes(kante_SizeStroke);

                //Synchronisiere die TextBoxen mit den Slidern.
                this.TextBox_Kanten_SizeStroke.Text = kante_SizeStroke.ToString();
            }
            catch { }
        }

        private void TextBoxes_KantenDarstellung_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Falls in einer der TextBoxen "Enter" gedrückt wird, synchronisiere die TextBoxen mit den Slidern.
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.SyncSlidersAndTextBoxes_KantenDarstellung(true);
            }
        }

        private void TextBoxes_KantenDarstellung_LostFocus(object sender, RoutedEventArgs e)
        {
            //Wenn eine TextBox den Fokus verliert, synchronisiere die TextBoxen mit den Slidern.
            this.SyncSlidersAndTextBoxes_KantenDarstellung(true);
        }

        private void SyncSlidersAndTextBoxes_KantenDarstellung(bool playErrorSound)
        {
            //Dicke der Kanten
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten.
                this.Slider_Kanten_SizeStroke.Value = double.Parse(this.TextBox_Kanten_SizeStroke.Text);
            }
            catch
            {
                //Spiele einen Error-Sound, falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den Wert in der TextBox "TextBox_Kanten_SizeStroke" zurück.
                if (playErrorSound && this.AllowSounds)
                {
                    SystemSounds.Asterisk.Play();
                }
                this.TextBox_Kanten_SizeStroke.Text = this.Slider_Kanten_SizeStroke.Value.ToString();
            }
        }
        #endregion


        //Methoden, um die Eigenschaften von Kanten und Knoten zu öffnen
        #region
        public void OpenNode(string name)
        {
            //Gehe zu dem Tab.
            this.TabControl.SelectedIndex = 1;

            //Versuche den Vertex zu öffnen.
            try
            {
                this.KnotenPicker.SelectedItem = (from ComboBoxItem item in this.KnotenPicker.Items where item.Content.ToString() == name select item).First();
            }
            catch { }
        }

        public void OpenNode(VisualVertex vertex)
        {
            //Gehe zu dem Tab.
            this.TabControl.SelectedIndex = 1;

            //Versuche den Vertex zu öffnen.
            try
            {
                this.KnotenPicker.SelectedItem = (from ComboBoxItem item in this.KnotenPicker.Items where item.Content.ToString() == vertex.Vertex.Name select item).First();
            }
            catch { }
        }

        public void OpenEdge(string name)
        {
            //Gehe zu dem Tab.
            this.TabControl.SelectedIndex = 2;

            //Versuche die Edge zu öffnen.
            try
            {
                this.KantenPicker.SelectedItem = (from ComboBoxItem item in this.KantenPicker.Items where item.Content.ToString() == name select item).First();
            }
            catch { }
        }

        public void OpenEdge(VisualEdge edge)
        {
            //Gehe zu dem Tab.
            this.TabControl.SelectedIndex = 2;

            //Versuche die Edge zu öffnen.
            try
            {
                this.KantenPicker.SelectedItem = (from ComboBoxItem item in this.KantenPicker.Items where item.Content.ToString() == edge.Edge.Name select item).First();
            }
            catch { }
        }
        #endregion

        private void KnotenLöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Graph.RemoveVertex(this.GetSelectedKnotenName());
            }
            catch { }
        }

        private void KanteLöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Graph.RemoveEdge(this.GetSelectedKante());
            }
            catch { }
        }
    }
}
