using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KantenLine.xaml
    /// </summary>
    public partial class KantenLine : UserControl
    {
        public KantenLine(RoutedEventHandler eigenschaften_Click)
        {
            InitializeComponent();

            //Lese die Einstellungen aus
            #region
            //Lese die Farben in den Einstellungen nach
            System.Drawing.Color c = Properties.Settings.Default.Kante_FarbeBorder;
            SolidColorBrush kanten_FarbeBorder = new(Color.FromArgb(c.A, c.R, c.G, c.B));

            //Lese die Höhen und die Thickness in den Einstellungen nach
            double knoten_Height = Properties.Settings.Default.Knoten_Höhe;
            double kanten_Thickness = Properties.Settings.Default.Kanten_Thickness;
            #endregion

            //Erstelle die visuelle Kante "kanten"

            //Lege  Werte der Linie fest
            this.Line.Stroke = kanten_FarbeBorder;
            this.Line.StrokeThickness = kanten_Thickness;

            //Bearbeite das ContextMenu
            #region
            //MenuItem zum Öffnen der Eigenschaften
            this.Eigenschaften.Header = MainWindow.resman.GetString("EigenschaftenKante", MainWindow.cul);
            this.Eigenschaften.Click += eigenschaften_Click;

            //MenuItem zum Löschen des Knoten
            this.Löschen.Header = MainWindow.resman.GetString("LöschenKante", MainWindow.cul);
            this.Löschen.Icon = " - ";
            this.Löschen.Click += MainWindow.main.LöschenKante_Click;

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
