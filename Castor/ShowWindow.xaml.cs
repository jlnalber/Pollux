using System.Windows;

namespace Castor
{
    /// <summary>
    /// Interaktionslogik für Show.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        public ShowWindow(VisualGraph graph)
        {
            //Erstelle das Fenster
            InitializeComponent();

            //Übergebe Daten an "Show".
            this.ShowGrid.Graph = graph;

            //Übersetze die Texte und stelle sie dar
            this.Title = graph.Graph.Name + " - " + VisualGraph.Resman.GetString("Eigenschaften", VisualGraph.Cul);

        }

        private void Escape(object sender, RoutedEventArgs e)
        {
            //Schließe das Fenster.
            this.Close();
        }
    }
}
