using System;
using System.Windows;
using System.Windows.Media;

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
            this.Preview_Text.Text = MainWindow.resman.GetString("Preview_Text", MainWindow.cul);
            this.Knoten_Preview_Text.Text = MainWindow.resman.GetString("Knoten_Preview_Text", MainWindow.cul);
            this.Kanten_Preview_Text.Text = MainWindow.resman.GetString("Kanten_Preview_Text", MainWindow.cul);
            this.KantenSchlinge_Preview_Text.Text = MainWindow.resman.GetString("KantenSchlinge_Preview_Text", MainWindow.cul);
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

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.EllipsePreview.StrokeThickness = Properties.Settings.Default.Knoten_Border_Thickness;
            this.EllipsePreview.Height = Properties.Settings.Default.Knoten_Höhe;
            this.EllipsePreview.Width = Properties.Settings.Default.Knoten_Breite;

            this.Kanten_Preview.StrokeThickness = Properties.Settings.Default.Kanten_Thickness;
            this.KantenSchlinge_Preview.StrokeThickness = Properties.Settings.Default.Kanten_Thickness;

            byte knoten_AStroke = byte.Parse(Math.Round(this.Slider_ANode_Border.Value).ToString());
            byte knoten_RStroke = byte.Parse(Math.Round(this.Slider_RNode_Border.Value).ToString());
            byte knoten_GStroke = byte.Parse(Math.Round(this.Slider_GNode_Border.Value).ToString());
            byte knoten_BStroke = byte.Parse(Math.Round(this.Slider_BNode_Border.Value).ToString());

            byte knoten_AFill = byte.Parse(Math.Round(this.Slider_ANode_Filling.Value).ToString());
            byte knoten_RFill = byte.Parse(Math.Round(this.Slider_RNode_Filling.Value).ToString());
            byte knoten_GFill = byte.Parse(Math.Round(this.Slider_GNode_Filling.Value).ToString());
            byte knoten_BFill = byte.Parse(Math.Round(this.Slider_BNode_Filling.Value).ToString());

            byte kanten_AStroke = byte.Parse(Math.Round(this.Slider_AEdge_Border.Value).ToString());
            byte kanten_RStroke = byte.Parse(Math.Round(this.Slider_REdge_Border.Value).ToString());
            byte kanten_GStroke = byte.Parse(Math.Round(this.Slider_GEdge_Border.Value).ToString());
            byte kanten_BStroke = byte.Parse(Math.Round(this.Slider_BEdge_Border.Value).ToString());

            this.EllipsePreview.Stroke = new SolidColorBrush(Color.FromArgb(knoten_AStroke, knoten_RStroke, knoten_GStroke, knoten_BStroke));
            this.EllipsePreview.Fill = new SolidColorBrush(Color.FromArgb(knoten_AFill, knoten_RFill, knoten_GFill, knoten_BFill));

            SolidColorBrush kanten_Stroke = new SolidColorBrush(Color.FromArgb(kanten_AStroke, kanten_RStroke, kanten_GStroke, kanten_BStroke));
            this.Kanten_Preview.Stroke = kanten_Stroke;
            this.KantenSchlinge_Preview.Stroke = kanten_Stroke;
        }
    }
}
