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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KnotenEllipse.xaml
    /// </summary>
    public partial class KnotenEllipse : UserControl
    {
        public KnotenEllipse(RoutedEventHandler eigenschaften_Click)
        {
            InitializeComponent();

            //Lese die Farben in den Einstellungen nach
            LinearGradientBrush knoten_FarbeFilling = new();
            if (Properties.Settings.Default.Transition)
            {
                knoten_FarbeFilling = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B), Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling2.A, Properties.Settings.Default.Knoten_FarbeFilling2.R, Properties.Settings.Default.Knoten_FarbeFilling2.G, Properties.Settings.Default.Knoten_FarbeFilling2.B), 45);
            }
            else
            {
                knoten_FarbeFilling = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B), Color.FromArgb(Properties.Settings.Default.Knoten_FarbeFilling.A, Properties.Settings.Default.Knoten_FarbeFilling.R, Properties.Settings.Default.Knoten_FarbeFilling.G, Properties.Settings.Default.Knoten_FarbeFilling.B), 45);
            }
            SolidColorBrush knoten_FarbeBorder = new(Color.FromArgb(Properties.Settings.Default.Knoten_FarbeBorder.A, Properties.Settings.Default.Knoten_FarbeBorder.R, Properties.Settings.Default.Knoten_FarbeBorder.G, Properties.Settings.Default.Knoten_FarbeBorder.B));

            //Lese die Höhen und die Thickness in den Einstellungen nach
            double knoten_Height = Properties.Settings.Default.Knoten_Höhe;
            double knoten_Width = Properties.Settings.Default.Knoten_Breite;
            double knoten_Border_Thickness = Properties.Settings.Default.Knoten_Border_Thickness;

            //Mache Feinheiten an der Ellipse
            this.Ellipse.Fill = knoten_FarbeFilling;
            this.Ellipse.Stroke = knoten_FarbeBorder;
            this.Ellipse.StrokeThickness = knoten_Border_Thickness;
            this.Ellipse.Height = knoten_Height;
            this.Ellipse.Width = knoten_Width;
            this.Ellipse.Cursor = Cursors.Hand;
            this.Ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            this.Ellipse.VerticalAlignment = VerticalAlignment.Top;

            //Füge ein ContextMenu hinzu
            #region
            //MenuItem zum Anzeigen seiner Eigenschaften
            this.Eigenschaften.Header = MainWindow.resman.GetString("EigenschaftenKnoten", MainWindow.cul);
            this.Eigenschaften.Click += eigenschaften_Click;

            //MenuItem zum Löschen des Knoten
            this.Löschen.Header = MainWindow.resman.GetString("LöschenKnoten", MainWindow.cul);
            this.Löschen.Icon = " - ";
            this.Löschen.Click += MainWindow.main.LöschenKnoten_Click;

            //MenuItem zur Bearbeitung von Graph
            this.MenuItem1.Header = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul);

            //MenuItem zum Hinzufügen von Kanten
            this.MenuItem2.Header = MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
            this.MenuItem2.Icon = " + ";
            this.MenuItem2.Click += MainWindow.main.KanteHinzufügen_Click;

            //MenuItem zum Hinzufügen von Knoten
            this.MenuItem3.Header = MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
            this.MenuItem3.Icon = " + ";
            this.MenuItem3.Click += MainWindow.main.KnotenHinzufügen_Click;

            //MenuItem zum Öffnen des Eiganschaften-Fensters
            this.MenuItem4.Click += MainWindow.main.EigenschaftenFenster_Click;
            this.MenuItem4.Header = MainWindow.resman.GetString("EigenschaftenFenster", MainWindow.cul);
            #endregion
        }
    }
}
