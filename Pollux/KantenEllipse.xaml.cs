using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KantenEllipse.xaml
    /// </summary>
    public partial class KantenEllipse : UserControl
    {
        public KantenEllipse()
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

            //lege eine Konstante für die Höhe fest
            const int height = 40;

            //Lege noch andere Werte der Linie fest
            this.Ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            this.Ellipse.VerticalAlignment = VerticalAlignment.Bottom;
            this.Ellipse.Stroke = kanten_FarbeBorder;
            this.Ellipse.StrokeThickness = kanten_Thickness;
            this.Ellipse.Fill = Brushes.Transparent;
            this.Ellipse.Height = height;
            this.Ellipse.Width = height;

            /*//Bearbeite das ContextMenu
            #region
            //MenuItem zum Öffnen der Eigenschaften
            this.Eigenschaften.Header = MainWindow.resman.GetString("EigenschaftenKante", MainWindow.cul);
            this.Eigenschaften.Click += MainWindow.main.EigenschaftenKanten_Click;

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
            #endregion*/
        }
    }
}
