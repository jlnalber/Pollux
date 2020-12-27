using System.Windows;

namespace Pollux
{
    /// <summary>
    /// Interaktionslogik für Einstellungen.xaml
    /// </summary>
    public partial class Einstellungen : Window
    {
        public Einstellungen()
        {
            InitializeComponent();

            //Übersetze die Texte
            #region
            this.Title = MainWindow.resman.GetString("EinstellungenTitle", MainWindow.cul);
            this.Appearance.Header = MainWindow.resman.GetString("Appearance", MainWindow.cul);
            #endregion
        }
    }
}
