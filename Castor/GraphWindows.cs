namespace Castor
{
    public partial class VisualGraph
    {
        public void OpenPropertiesWindow()
        {
            try
            {
                //Öffne ein neues Eigenschaften-Fenster.
                ShowWindow show = new(this);
                show.Show();
            }
            catch { }
        }

        public void OpenAddVertexWindow()
        {
            try
            {
                //Öffne ein neues "KnotenHinzufügen"-Fenster
                KnotenHinzufügen window = new(this);
                window.Show();
            }
            catch { }
        }

        public void OpenAddEdgeWindow()
        {
            try
            {
                //Öffne ein neues "KantenHinzufügen"-Fenster
                KanteHinzufügen window = new(this);
                window.Show();
            }
            catch { }
        }
    }
}
