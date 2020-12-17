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
    /// Interaktionslogik für KanteHinzufügen.xaml
    /// </summary>
    public partial class KanteHinzufügen : Window
    {
        private MainWindow MainWindow;
        private Pollux.Graph.Graph Graph;

        public KanteHinzufügen(Pollux.Graph.Graph graph, MainWindow mainWindow)
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
            this.Title = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul) + " > " + MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
            this.Name.Text = MainWindow.resman.GetString("Name", MainWindow.cul);
            this.Knoten1.Text = MainWindow.resman.GetString("Knoten1", MainWindow.cul);
            this.Knoten2.Text = MainWindow.resman.GetString("Knoten2", MainWindow.cul);
            this.Bestätigen.Content = MainWindow.resman.GetString("Bestätigen", MainWindow.cul);

            //Lege einen ersten Namen fest
            this.AusgewählterName.Text = "EDGE" + this.Graph.GraphKanten.Count;

            //Gebe der TextBox den Fokus
            this.AusgewählterName.Focus();
            this.AusgewählterName.Select(0, this.AusgewählterName.Text.Length);
            #endregion

            //Stelle die ComboBoxen dar
            #region
            //schreibe in die ComboBox "KnotenPicker1", welche Ecken es alle gibt
            foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = i.Name;
                this.KnotenPicker1.Items.Add(comboBoxItem);
            }
            this.KnotenPicker1.SelectedIndex = 0;

            //schreibe in die ComboBox "KnotenPicker2", welche Ecken es alle gibt
            foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = i.Name;
                this.KnotenPicker2.Items.Add(comboBoxItem);
            }
            this.KnotenPicker2.SelectedIndex = 1;
            #endregion
        }

        private void Bestätigen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Gucke nach dem ausgewählten ersten Knoten
                ComboBoxItem Knoten1 = new();
                switch (KnotenPicker1.SelectedItem)
                {
                    case ComboBoxItem item: Knoten1 = item; break;
                }

                //Gucke nach dem ausgewählten zweiten Knoten
                ComboBoxItem Knoten2 = new();
                switch (KnotenPicker2.SelectedItem)
                {
                    case ComboBoxItem item: Knoten2 = item; break;
                }

                //Führe den Command aus
                MainWindow.GetOpenConsole().Command("ADD " + this.AusgewählterName.Text + " BETWEEN " + Knoten1.Content.ToString() + " AND " + Knoten2.Content.ToString());

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
