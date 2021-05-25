using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Thestias;
using System;

namespace Castor
{
    /// <summary>
    /// Interaktionslogik für KanteHinzufügen.xaml
    /// </summary>
    public partial class KanteHinzufügen : Window
    {
        private VisualGraph Graph;

        public KanteHinzufügen(VisualGraph graph)
        {
            //Stelle das Fenster dar
            this.InitializeComponent();

            //Lege die Members fest
            this.Graph = graph;

            //Mache verschieden Feinheiten zur Darstellung
            #region
            //Übersetze die Texte
            this.Title = VisualGraph.Resman.GetString("GraphBearbeiten", VisualGraph.Cul) + " > " + VisualGraph.Resman.GetString("KanteHinzufügen", VisualGraph.Cul);
            this.NameText.Text = VisualGraph.Resman.GetString("Name", VisualGraph.Cul);
            this.Knoten1.Text = VisualGraph.Resman.GetString("Knoten1", VisualGraph.Cul);
            this.Knoten2.Text = VisualGraph.Resman.GetString("Knoten2", VisualGraph.Cul);
            this.Bestätigen.Content = VisualGraph.Resman.GetString("Bestätigen", VisualGraph.Cul);

            //Lege einen ersten Namen fest
            this.AusgewählterName.Text = this.Graph.GetValidEdgeName();

            //Gebe der TextBox den Fokus
            this.AusgewählterName.Focus();
            this.AusgewählterName.Select(0, this.AusgewählterName.Text.Length);
            #endregion

            //Stelle die ComboBoxen dar
            #region
            //schreibe in die ComboBox "KnotenPicker1", welche Ecken es alle gibt
            foreach (Graph.Vertex i in graph.Graph.Vertices)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = i.Name;
                this.KnotenPicker1.Items.Add(comboBoxItem);
            }
            this.KnotenPicker1.SelectedIndex = 0;

            //schreibe in die ComboBox "KnotenPicker2", welche Ecken es alle gibt
            foreach (Graph.Vertex i in graph.Graph.Vertices)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = i.Name;
                this.KnotenPicker2.Items.Add(comboBoxItem);
            }
            this.KnotenPicker2.SelectedIndex = graph.Graph.Vertices.Count == 1 ? 0 : 1;
            #endregion
        }

        private void Bestätigen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Suche nach die ausgewählten Vertices.
                string knoten1 = ((ComboBoxItem)this.KnotenPicker1.SelectedItem).Content.ToString();
                string knoten2 = ((ComboBoxItem)this.KnotenPicker2.SelectedItem).Content.ToString();

                //Füge die Kante zum Graphen hinzu.
                try
                {
                    this.Graph.AddEdge(new Graph.Edge(this.Graph.Graph, new Graph.Vertex[2], this.AusgewählterName.Text), this.Graph.SearchVertex(knoten1), this.Graph.SearchVertex(knoten2));
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                    //Spiele den Error-Sound
                    SystemSounds.Asterisk.Play();
                }

                //Schließe dieses Fenster wieder
                this.Close();
            }
            catch (Graph.GraphExceptions.NameAlreadyExistsException)
            {
                //Zeige, dass der ausgewählte Name schon existiert
                this.AusgewählterName.Foreground = Brushes.Red;
            }
            catch
            {
                //Spiele einen Error-Sound
                SystemSounds.Asterisk.Play();
            }
        }

        private void AusgewählterName_KeyUp(object sender, KeyEventArgs e)
        {
            //Falls Enter gedrückt wird, löse das "Bestätigen_Click"-Event aus
            if (e.Key == Key.Enter)
            {
                this.Bestätigen_Click(sender, e);
            }
        }

        private void Escape(object sender, RoutedEventArgs e)
        {
            //Schließe das Fenster
            this.Close();
        }
    }
}
