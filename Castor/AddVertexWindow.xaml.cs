using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Thestias;

namespace Castor
{
    /// <summary>
    /// Interaktionslogik für KnotenHInzufügen.xaml
    /// </summary>
    public partial class KnotenHinzufügen : Window
    {
        private VisualGraph Graph;

        public KnotenHinzufügen(VisualGraph graph)
        {
            //Stelle das Fenster dar
            this.InitializeComponent();

            //Lege die Members fest
            this.Graph = graph;

            //Mache verschieden Feinheiten zur Darstellung
            #region
            //Übersetze die Texte
            this.Title = VisualGraph.Resman.GetString("GraphBearbeiten", VisualGraph.Cul) + " > " + VisualGraph.Resman.GetString("KnotenHinzufügen", VisualGraph.Cul);
            this.NameText.Text = VisualGraph.Resman.GetString("Name", VisualGraph.Cul);
            this.Bestätigen.Content = VisualGraph.Resman.GetString("Bestätigen", VisualGraph.Cul);

            //Lege einen ersten Namen fest
            this.AusgewählterName.Text = this.Graph.GetValidVertexName();

            //Gebe der TextBox den Fokus
            this.AusgewählterName.Focus();
            this.AusgewählterName.Select(0, this.AusgewählterName.Text.Length);
            #endregion
        }

        private void Bestätigen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Führe den Command aus
                this.Graph.AddVertex(this.AusgewählterName.Text);

                //Schließe dieses Fenster wieder
                this.Close();
            }
            catch (Graph.GraphExceptions.NameAlreadyExistsException)
            {
                //Zeige, dass der ausgewählte Name schon existiert
                this.AusgewählterName.Foreground = Brushes.Red;
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
