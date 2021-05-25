using Castor.Properties;
using System.Globalization;
using System.Resources;
using System.Windows.Controls;
using Thestias;

namespace Castor
{
    /// <summary>
    /// Interaktionslogik für GraphDarstellung.xaml
    /// </summary>
    public partial class VisualGraph : UserControl
    {
        //TODO: implement constructor with name-list/hashset
        public VisualGraph()
        {
            InitializeComponent();

            if (Resman == null)
            {
                //initialisiere Resource und rufe die Kultur ab
                Resman = new ResourceManager(typeof(Resources));
                Cul = CultureInfo.CurrentUICulture;
                //cul = new CultureInfo("en");
                //cul = new CultureInfo("fr");
            }

            this.Eigenschaften.DataContext = this;
            this.EigenschaftenFenster_MenuItem.DataContext = this;
            this.GraphBearbeiten_MenuItem.DataContext = this;
            this.Graph = new();
            this.Edges = new();
            this.Vertices = new();
            this.Graph.IsEditable = false;

            //Gebe Daten an "Show" weiter.
            this.PropertiesGrid.Graph = this;

            //Übersetzungen
            this.GraphBearbeiten_MenuItem.Header = Resman.GetString("GraphBearbeiten", Cul);
            this.KnotenHinzufügen_MenuItem.Header = Resman.GetString("KnotenHinzufügen", Cul);
            this.KanteHinzufügen_MenuItem.Header = Resman.GetString("KanteHinzufügen", Cul);
            this.EigenschaftenFenster_MenuItem.Header = Resman.GetString("EigenschaftenFenster", Cul);
        }

        public VisualGraph(Graph graph)
        {
            InitializeComponent();

            //initialisiere Resource und rufe die Kultur ab
            Resman = new ResourceManager(typeof(Resources));
            Cul = CultureInfo.CurrentUICulture;
            //cul = new CultureInfo("en");
            //cul = new CultureInfo("fr");

            this.Eigenschaften.DataContext = this;
            this.EigenschaftenFenster_MenuItem.DataContext = this;
            this.GraphBearbeiten_MenuItem.DataContext = this;
            this.Graph = graph;
            this.Edges = new();
            this.Vertices = new();
            this.Graph.IsEditable = false;

            //Gebe Daten an "Show" weiter.
            this.PropertiesGrid.Graph = this;

            //Übersetzungen
            this.GraphBearbeiten_MenuItem.Header = Resman.GetString("GraphBearbeiten", Cul);
            this.KnotenHinzufügen_MenuItem.Header = Resman.GetString("KnotenHinzufügen", Cul);
            this.KanteHinzufügen_MenuItem.Header = Resman.GetString("KanteHinzufügen", Cul);
            this.EigenschaftenFenster_MenuItem.Header = Resman.GetString("EigenschaftenFenster", Cul);
        }
    }
}
