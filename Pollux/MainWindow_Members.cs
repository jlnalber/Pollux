using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Windows.Controls;

namespace Pollux
{
    public partial class MainWindow
    {
        //statiche Members
        #region
        public static CultureInfo cul;
        public static ResourceManager resman;
        public static MainWindow main;
        public static string AppDirectory;
        public static string Files;
        #endregion

        //nicht-statische Members, die also zu einem Objekt dazugehören
        #region
        public Dictionary<TabItem, string> OpenedFiles { get; set; }//enthält alle gerade geöffneten Dateien mit ihren Tabs
        public Dictionary<TabItem, TextBox> Outputs { get; set; }//enthält alle output-Konsolen mit ihren jeweiligen Tabs
        public Dictionary<TabItem, TextBox> Inputs { get; set; }//enthält alle intput-TextBoxen mit ihren jeweiligen Tabs
        public Dictionary<TabItem, CommandConsole> Consoles { get; set; }//enthält alle CommandConsoles mit ihren jeweiligen Tabs
        public Dictionary<TabItem, Canvas> Canvases { get; set; }//enthält alle Canvases mit ihren jeweiligen Tabs
        public Dictionary<TabItem, GraphDarstellung> Graphs { get; set; }//enthält alle Graphen (natürlich mit visuellen Elementen) mit ihren jeweiligen Tabs
        public Dictionary<TabItem, TextBlock> Headers { get; set; }//enthält alle Headers von den Tabs mit ihren jeweiligen Tabs
        public Dictionary<TabItem, Show> OpenedEigenschaftenFenster { get; set; }//Enthält alle Eigenschaften-Fenster ("Show"'s) mit ihren jeweiligen Tabs
        public Dictionary<TabItem, Grid> OpenedEigenschaftenFensterGrid { get; set; }//Enthält alle Eigenschaften-Fenster-Grids mit ihren jeweiligen Tabs
        #endregion
    }
}
