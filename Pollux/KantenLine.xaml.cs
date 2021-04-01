using System.Windows.Controls;
using System.Windows.Media;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KantenLine.xaml
    /// </summary>
    public partial class KantenLine : UserControl
    {
        private static string EigenschaftenHeader = null;
        private static string LöschenHeader = null;
        private static string MenuItem1Header = null;
        private static string MenuItem2Header = null;
        private static string MenuItem3Header = null;
        private static string MenuItem4Header = null;

        public KantenLine()
        {
            /*//Mache eventuell die Übersetzungen
            if (EigenschaftenHeader == null)
            {
                EigenschaftenHeader = MainWindow.resman.GetString("EigenschaftenKante", MainWindow.cul);
                LöschenHeader = MainWindow.resman.GetString("LöschenKante", MainWindow.cul);
                MenuItem1Header = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul);
                MenuItem2Header = MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
                MenuItem3Header = MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
                MenuItem4Header = MainWindow.resman.GetString("EigenschaftenFenster", MainWindow.cul);
            }*/

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
            /*#region
            //MenuItem zum Öffnen der Eigenschaften
            this.Eigenschaften.Header = EigenschaftenHeader;
            this.Eigenschaften.Click += MainWindow.main.EigenschaftenKanten_Click;

            //MenuItem zum Löschen des Knoten
            this.Löschen.Header = LöschenHeader;
            this.Löschen.Icon = " - ";
            this.Löschen.Click += MainWindow.main.LöschenKante_Click;

            //MenuItem zur Bearbeitung von Graph
            this.MenuItem1.Header = MenuItem1Header;

            //MenuItem zum Hinzufügen von Kanten
            this.MenuItem2.Header = MenuItem2Header;
            this.MenuItem2.Icon = " + ";
            this.MenuItem2.Click += MainWindow.main.KanteHinzufügen_Click;

            //MenuItem zum Hinzufügen von Knoten
            this.MenuItem3.Header = MenuItem3Header;
            this.MenuItem3.Icon = " + ";
            this.MenuItem3.Click += MainWindow.main.KnotenHinzufügen_Click;

            //MenuItem zum Öffnen des Eiganschaften-Fensters
            this.MenuItem4.Click += MainWindow.main.EigenschaftenFenster_Click;
            this.MenuItem4.Header = MenuItem4Header;
            #endregion*/
        }

        public static ContextMenu GetContextMenu()
        {
            //Mache eventuell die Übersetzungen
            if (EigenschaftenHeader == null)
            {
                EigenschaftenHeader = MainWindow.resman.GetString("EigenschaftenKante", MainWindow.cul);
                LöschenHeader = MainWindow.resman.GetString("LöschenKante", MainWindow.cul);
                MenuItem1Header = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul);
                MenuItem2Header = MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
                MenuItem3Header = MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
                MenuItem4Header = MainWindow.resman.GetString("EigenschaftenFenster", MainWindow.cul);
            }

            //ContextMenu "contextMenu"
            ContextMenu contextMenu = new ContextMenu();

            //MenuItem zum Öffnen der Eigenschaften
            MenuItem eigenschaften = new();
            eigenschaften.Header = EigenschaftenHeader;
            eigenschaften.Click += MainWindow.main.EigenschaftenKanten_Click;

            //MenuItem zum Löschen des Knoten
            MenuItem löschen = new MenuItem();
            löschen.Header = LöschenHeader;
            löschen.Icon = " - ";
            löschen.Click += MainWindow.main.LöschenKante_Click;

            //MenuItem zur Bearbeitung von Graph
            MenuItem menuItem1 = new();
            menuItem1.Header = MenuItem1Header;

            //MenuItem zum Hinzufügen von Kanten
            MenuItem menuItem2 = new();
            menuItem2.Header = MenuItem2Header;
            menuItem2.Icon = " + ";
            menuItem2.Click += MainWindow.main.KanteHinzufügen_Click;

            //MenuItem zum Hinzufügen von Knoten
            MenuItem menuItem3 = new();
            menuItem3.Header = MenuItem3Header;
            menuItem3.Icon = " + ";
            menuItem3.Click += MainWindow.main.KnotenHinzufügen_Click;

            //Füge die MenuItems "menuItem2" und "menuItem3" zu "menuItem1" hinzu
            menuItem1.Items.Add(menuItem2);
            menuItem1.Items.Add(menuItem3);

            //MenuItem zum Öffnen des Eiganschaften-Fensters
            MenuItem menuItem4 = new();
            menuItem4.Click += MainWindow.main.EigenschaftenFenster_Click;
            menuItem4.Header = MenuItem4Header;

            //Füge alle MenuItems zum ContextMenu "contextMenu" hinzu
            contextMenu.Items.Add(eigenschaften);
            contextMenu.Items.Add(löschen);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(menuItem1);
            contextMenu.Items.Add(menuItem4);

            //Rückgabe
            return contextMenu;
        }
    }
}
