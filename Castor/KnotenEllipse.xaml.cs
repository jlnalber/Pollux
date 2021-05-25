using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Castor
{
    /// <summary>
    /// Interaktionslogik für KnotenEllipse.xaml
    /// </summary>
    public partial class VertexEllipse : UserControl
    {
        private static string EigenschaftenHeader = null;
        private static string LöschenHeader = null;
        private static string MenuItem1Header = null;
        private static string MenuItem2Header = null;
        private static string MenuItem3Header = null;
        private static string MenuItem4Header = null;


        public VertexEllipse(VisualGraph graph, VisualVertex vertex)
        {
            //Mache eventuell die Übersetzungen
            if (EigenschaftenHeader == null)
            {
                EigenschaftenHeader = VisualGraph.Resman.GetString("EigenschaftenKnoten", VisualGraph.Cul);
                LöschenHeader = VisualGraph.Resman.GetString("LöschenKnoten", VisualGraph.Cul);
                MenuItem1Header = VisualGraph.Resman.GetString("GraphBearbeiten", VisualGraph.Cul);
                MenuItem2Header = VisualGraph.Resman.GetString("KanteHinzufügen", VisualGraph.Cul);
                MenuItem3Header = VisualGraph.Resman.GetString("KnotenHinzufügen", VisualGraph.Cul);
                MenuItem4Header = VisualGraph.Resman.GetString("EigenschaftenFenster", VisualGraph.Cul);
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
            this.Width = knoten_Width;
            this.Height = knoten_Height;
            this.Ellipse.Height = knoten_Height;
            this.Ellipse.Width = knoten_Width;
            this.Ellipse.Cursor = Cursors.Hand;
            this.Ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            this.Ellipse.VerticalAlignment = VerticalAlignment.Top;

            //Lege die Position fest.
            int index = graph.Vertices.Contains(vertex) ? graph.Vertices.IndexOf(vertex) : graph.Vertices.Count;
            this.Margin = new Thickness((index % 10) * 100 + 20, index / 10 * 100 + 25, 0, 0);

            //Füge ein ContextMenu hinzu
            #region
            //MenuItem zum Anzeigen seiner Eigenschaften
            this.Eigenschaften.Header = EigenschaftenHeader;
            this.Eigenschaften.Click += vertex.OpenProperties_Click;

            //MenuItem zum Löschen des Knoten
            this.Löschen.Header = LöschenHeader;
            this.Löschen.Icon = " - ";
            this.Löschen.Click += vertex.DeleteVertex_Click;

            //MenuItem zur Bearbeitung von Graph
            this.MenuItem1.Header = MenuItem1Header;

            //MenuItem zum Hinzufügen von Kanten
            this.MenuItem2.Header = MenuItem2Header;
            this.MenuItem2.Icon = " + ";
            this.MenuItem2.Click += graph.KanteHinzufügen_Click;

            //MenuItem zum Hinzufügen von Knoten
            this.MenuItem3.Header = MenuItem3Header;
            this.MenuItem3.Icon = " + ";
            this.MenuItem3.Click += graph.KnotenHinzufügen_Click;

            //MenuItem zum Öffnen des Eiganschaften-Fensters
            this.MenuItem4.Click += graph.EigenschaftenFenster_Click;
            this.MenuItem4.Header = MenuItem4Header;
            #endregion
        }
    }
}
