using Castor;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Pollux
{
    public partial class MainWindow
    {
        //Methoden um die geöffneten Graphen, Tabs, etc. herauszufinden
        #region
        public TabItem GetOpenTab()
        {
            //finde den geöffneten Tab heraus
            TabItem tab = new TabItem();
            switch (this.TabControl.SelectedItem)
            {
                case TabItem tabItem: tab = tabItem; break;
            }

            //Rückgabe
            return tab;
        }

        public string GetOpenPath()
        {
            //Gebe den Pfad des aktuell geöffneten Tabs zurück
            return this.OpenedFiles[this.GetOpenTab()];
        }

        public TextBox GetOpenOutput()
        {
            //Suche nach der Output-TextBox des geöffneten Tabs
            return this.Outputs[this.GetOpenTab()];
        }

        public TextBox GetOpenInput()
        {
            //Suche nach der Input-TextBox des geöffneten Tabs
            return this.Inputs[this.GetOpenTab()];
        }

        public CommandConsole GetOpenConsole()
        {
            //suche nach dem geöffneten Tab und gib dann seine CommanConsole zurück
            return this.Consoles[this.GetOpenTab()];
        }

        public VisualGraph GetOpenGraph()
        {
            //find den geöffneten Tab heraus
            TabItem tab = this.GetOpenTab();

            //Rückgabe
            return this.Graphs[tab];
        }

        public TextBlock GetOpenHeader()
        {
            //suche nach dem geöffneten Tab und gib dann seinen Header zurück
            return this.Headers[this.GetOpenTab()];
        }
        #endregion

        public void SaveOpenedFiles()
        {
            //in der Setting "OpenedFiles" abspeicheren, welche Dateien alle geöffnet sind
            string allPaths = "";
            foreach (KeyValuePair<TabItem, string> kvp in this.OpenedFiles)
            {
                allPaths += kvp.Value;
                allPaths += "\n";
            }
            Properties.Settings.Default.OpenedFiles = allPaths;
            Properties.Settings.Default.Save();
        }

        //Methoden zum Abspeichern von den Dateien
        #region
        public void SaveAll()
        {
            //Speicher alle Tabs
            foreach (CommandConsole commandConsole in this.Consoles.Values)
            {
                commandConsole.Command("SAVE");
            }
        }

        public void Save()
        {
            //Speichere nur den geöffneten Tab ab
            this.GetOpenConsole().Command("SAVE");
        }
        #endregion
    }
}
