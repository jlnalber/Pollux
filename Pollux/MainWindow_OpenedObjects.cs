using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace Pollux
{
    public partial class MainWindow
    {
        //Methoden um die geöffneten Graphen/Tabs etc. herauszufinden
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

        public GraphDarstellung GetOpenGraphDarstellung()
        {
            //find den geöffneten Tab heraus
            TabItem tab = GetOpenTab();

            //Rückgabe
            return this.Graphs[tab];
        }

        public Pollux.Graph.Graph GetOpenGraph()
        {
            //Suche den geöffneten Graphen
            return GetOpenGraphDarstellung().graph;
        }

        public Canvas GetOpenCanvas()
        {
            //Suche den geöffneten Graphen
            return this.Canvases[GetOpenTab()];
        }

        public CommandConsole GetOpenConsole()
        {
            //suche nach dem geöffneten Tab und gib dann seine CommanConsole zurück
            return this.Consoles[GetOpenTab()];
        }

        public TextBlock GetOpenHeader()
        {
            //suche nach dem geöffneten Tab und gib dann seinen Header zurück
            return this.Headers[GetOpenTab()];
        }
        #endregion

        public void SaveOpenedFiles()
        {
            //abspeicheren, welche Dateien alle geöffnet sind
            try
            {
                StreamWriter streamWriter = new StreamWriter(AppDirectory + @"\Data\openedFiles.txt", false);
                string allPaths = "";
                foreach (KeyValuePair<TabItem, string> kvp in this.OpenedFiles)
                {
                    allPaths += kvp.Value;
                    allPaths += "\n";
                }
                streamWriter.WriteLine(allPaths);
                streamWriter.Close();
            }
            catch
            {
                //erstelle Ordner
                DirectoryInfo di = Directory.CreateDirectory(AppDirectory + @"\Data");

                //erstelle Datei in diesem Ordner und speichere die Daten ab
                StreamWriter streamWriter = new StreamWriter(AppDirectory + @"\Data\openedFiles.txt", false);
                string allPaths = "";
                foreach (KeyValuePair<TabItem, string> kvp in this.OpenedFiles)
                {
                    allPaths += kvp.Value;
                    allPaths += "\n";
                }
                streamWriter.WriteLine(allPaths);
                streamWriter.Close();
            }
        }

        //Methoden zum Abspeichern von den Dateien
        #region
        public void SaveAll()
        {
            //Speicher alle Tabs
            foreach (CommandConsole commandConsole in this.Consoles.Values)
            {
                commandConsole.Save();
            }
        }

        public void SaveOpenFile()
        {
            //Speichere nur den geöffneten Tab ab
            GetOpenConsole().Save();
        }
        #endregion
    }
}
