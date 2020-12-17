using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KnotenHInzufügen.xaml
    /// </summary>
    public partial class KnotenHinzufügen : Window
    {
        private MainWindow MainWindow;
        private Pollux.Graph.Graph Graph;

        public KnotenHinzufügen(Pollux.Graph.Graph graph, MainWindow mainWindow)
        {
            //Stelle das Fenster dar
            InitializeComponent();

            //Lege die Members fest
            #region
            this.MainWindow = mainWindow;
            this.Graph = graph;
            #endregion

            //Mache verschieden Feinheiten zur Darstellung
            #region
            //Übersetze die Texte
            this.Title = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul) + " > " + MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
            this.Name.Text = MainWindow.resman.GetString("Name", MainWindow.cul);
            this.Bestätigen.Content = MainWindow.resman.GetString("Bestätigen", MainWindow.cul);

            //Lege einen ersten Namen fest
            this.AusgewählterName.Text = "NODE" + this.Graph.GraphKnoten.Count;

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
                MainWindow.GetOpenConsole().Command("ADD " + this.AusgewählterName.Text);

                //Male den Graphen neu
                this.MainWindow.DrawGraph();

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
    }
}
