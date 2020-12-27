using System;
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

            this.Node_Design_Text.Text = MainWindow.resman.GetString("Node_Design_Text", MainWindow.cul);

            this.Node_DesignFilling_Text.Text = MainWindow.resman.GetString("Node_DesignFilling_Text", MainWindow.cul);
            this.Slider_RNode_Filling_Text.Text = MainWindow.resman.GetString("R", MainWindow.cul);
            this.Slider_GNode_Filling_Text.Text = MainWindow.resman.GetString("G", MainWindow.cul);
            this.Slider_BNode_Filling_Text.Text = MainWindow.resman.GetString("B", MainWindow.cul);
            this.Slider_ANode_Filling_Text.Text = MainWindow.resman.GetString("A", MainWindow.cul);

            this.Node_DesignBorder_Text.Text = MainWindow.resman.GetString("Node_DesignBorder_Text", MainWindow.cul);
            this.Slider_RNode_Border_Text.Text = MainWindow.resman.GetString("R", MainWindow.cul);
            this.Slider_GNode_Border_Text.Text = MainWindow.resman.GetString("G", MainWindow.cul);
            this.Slider_BNode_Border_Text.Text = MainWindow.resman.GetString("B", MainWindow.cul);
            this.Slider_ANode_Border_Text.Text = MainWindow.resman.GetString("A", MainWindow.cul);

            this.Edge_Design_Text.Text = MainWindow.resman.GetString("Edge_Design_Text", MainWindow.cul);
            this.Edge_DesignBorder_Text.Text = MainWindow.resman.GetString("Edge_DesignBorder_Text", MainWindow.cul);
            this.Slider_REdge_Border_Text.Text = MainWindow.resman.GetString("R", MainWindow.cul);
            this.Slider_GEdge_Border_Text.Text = MainWindow.resman.GetString("G", MainWindow.cul);
            this.Slider_BEdge_Border_Text.Text = MainWindow.resman.GetString("B", MainWindow.cul);
            this.Slider_AEdge_Border_Text.Text = MainWindow.resman.GetString("A", MainWindow.cul);

            this.Apply.Content = MainWindow.resman.GetString("Apply", MainWindow.cul);
            #endregion

            //Stelle die Slider (je nach Einstellung) ein
            #region
            this.Slider_RNode_Filling.Value = Properties.Settings.Default.Knoten_FarbeFilling.R;
            this.Slider_GNode_Filling.Value = Properties.Settings.Default.Knoten_FarbeFilling.G;
            this.Slider_BNode_Filling.Value = Properties.Settings.Default.Knoten_FarbeFilling.B;
            this.Slider_ANode_Filling.Value = Properties.Settings.Default.Knoten_FarbeFilling.A;

            this.Slider_RNode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.R;
            this.Slider_GNode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.G;
            this.Slider_BNode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.B;
            this.Slider_ANode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.A;

            this.Slider_REdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.R;
            this.Slider_GEdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.G;
            this.Slider_BEdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.B;
            this.Slider_AEdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.A;
            #endregion
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            //Wende die Eintellungen an
            Properties.Settings.Default.Knoten_FarbeFilling = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Filling.Value).ToString()));
            Properties.Settings.Default.Knoten_FarbeBorder = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Border.Value).ToString()));
            Properties.Settings.Default.Kante_FarbeBorder = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_AEdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_REdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_GEdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_BEdge_Border.Value).ToString()));

            //Speichere ab
            Properties.Settings.Default.Save();

            //Schließe das Fenster
            Close();
        }
    }
}
