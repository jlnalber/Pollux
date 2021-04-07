using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KnotenHInzufügen.xaml
    /// </summary>
    public partial class KnotenHinzufügen : Window
    {
        private MainWindow MainWindow;
        private GraphDarstellung Graph;

        public KnotenHinzufügen(GraphDarstellung graph, MainWindow mainWindow)
        {
            //Stelle das Fenster dar
            this.InitializeComponent();

            //Lege die Members fest
            #region
            this.MainWindow = mainWindow;
            this.Graph = graph;
            #endregion

            //Mache verschieden Feinheiten zur Darstellung
            #region
            //Übersetze die Texte
            this.Title = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul) + " > " + MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
            this.NameText.Text = MainWindow.resman.GetString("Name", MainWindow.cul);
            this.Bestätigen.Content = MainWindow.resman.GetString("Bestätigen", MainWindow.cul);

            //Lege einen ersten Namen fest
            int suffix = 0;
            for (; this.Graph.SucheKnoten("NODE" + suffix) != null; ++suffix) ;
            this.AusgewählterName.Text = "NODE" + suffix;

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
                this.MainWindow.GetOpenConsole().Command("ADD " + this.AusgewählterName.Text);

                //Schließe dieses Fenster wieder
                this.Close();
            }
            catch (Pollux.Graph.Graph.GraphExceptions.NameAlreadyExistsException)
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
