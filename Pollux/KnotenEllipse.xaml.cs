using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für KnotenEllipse.xaml
    /// </summary>
    public partial class KnotenEllipse : UserControl
    {
        private static string EigenschaftenHeader = null;
        private static string LöschenHeader = null;
        private static string MenuItem1Header = null;
        private static string MenuItem2Header = null;
        private static string MenuItem3Header = null;
        private static string MenuItem4Header = null;


        public KnotenEllipse()
        {
            //Mache eventuell die Übersetzungen
            if (EigenschaftenHeader == null)
            {
                EigenschaftenHeader = MainWindow.resman.GetString("EigenschaftenKnoten", MainWindow.cul);
                LöschenHeader = MainWindow.resman.GetString("LöschenKnoten", MainWindow.cul);
                MenuItem1Header = MainWindow.resman.GetString("GraphBearbeiten", MainWindow.cul);
                MenuItem2Header = MainWindow.resman.GetString("KanteHinzufügen", MainWindow.cul);
                MenuItem3Header = MainWindow.resman.GetString("KnotenHinzufügen", MainWindow.cul);
                MenuItem4Header = MainWindow.resman.GetString("EigenschaftenFenster", MainWindow.cul);
            }

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
            this.Eigenschaften.Header = EigenschaftenHeader;
            this.Eigenschaften.Click += MainWindow.main.EigenschaftenKnoten_Click;

            //MenuItem zum Löschen des Knoten
            this.Löschen.Header = LöschenHeader;
            this.Löschen.Icon = " - ";
            this.Löschen.Click += MainWindow.main.LöschenKnoten_Click;

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
            #endregion
        }

        public static ContextMenu GetContextMenu()
        {
            //Mache eventuell die Übersetzungen
            if (EigenschaftenHeader == null)
            {
                EigenschaftenHeader = MainWindow.resman.GetString("EigenschaftenKnoten", MainWindow.cul);
                LöschenHeader = MainWindow.resman.GetString("LöschenKnoten", MainWindow.cul);
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
            eigenschaften.Click += MainWindow.main.EigenschaftenKnoten_Click;

            //MenuItem zum Löschen des Knoten
            MenuItem löschen = new MenuItem();
            löschen.Header = LöschenHeader;
            löschen.Icon = " - ";
            löschen.Click += MainWindow.main.LöschenKnoten_Click;

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
