using System;
using System.Media;
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

            this.Node_DesignSizes_Text.Text = MainWindow.resman.GetString("Node_DesignSizes_Text", MainWindow.cul);
            this.Slider_Node_Size_Text.Text = MainWindow.resman.GetString("Slider_Node_Size_Text", MainWindow.cul);
            this.Slider_Node_SizeStroke_Text.Text = MainWindow.resman.GetString("Slider_Node_SizeStroke_Text", MainWindow.cul);

            this.Edge_Design_Text.Text = MainWindow.resman.GetString("Edge_Design_Text", MainWindow.cul);
            this.Edge_DesignBorder_Text.Text = MainWindow.resman.GetString("Edge_DesignBorder_Text", MainWindow.cul);
            this.Slider_REdge_Border_Text.Text = MainWindow.resman.GetString("R", MainWindow.cul);
            this.Slider_GEdge_Border_Text.Text = MainWindow.resman.GetString("G", MainWindow.cul);
            this.Slider_BEdge_Border_Text.Text = MainWindow.resman.GetString("B", MainWindow.cul);
            this.Slider_AEdge_Border_Text.Text = MainWindow.resman.GetString("A", MainWindow.cul);

            this.Edge_DesignSizes_Text.Text = MainWindow.resman.GetString("Edge_DesignSizes_Text", MainWindow.cul);
            this.Slider_Edge_SizeStroke_Text.Text = MainWindow.resman.GetString("Slider_Edge_SizeStroke_Text", MainWindow.cul);

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

            this.Slider_Node_Size.Value = Properties.Settings.Default.Knoten_Höhe;
            this.Slider_Node_SizeStroke.Value = Properties.Settings.Default.Knoten_Border_Thickness;

            this.Slider_RNode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.R;
            this.Slider_GNode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.G;
            this.Slider_BNode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.B;
            this.Slider_ANode_Border.Value = Properties.Settings.Default.Knoten_FarbeBorder.A;

            this.Slider_REdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.R;
            this.Slider_GEdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.G;
            this.Slider_BEdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.B;
            this.Slider_AEdge_Border.Value = Properties.Settings.Default.Kante_FarbeBorder.A;

            this.Slider_Edge_SizeStroke.Value = Properties.Settings.Default.Kanten_Thickness;
            #endregion

            //Stelle die TextBoxes nach den Slidern ein
            SyncSlidersAndTextBoxes(false);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            //Wende die Eintellungen an
            Properties.Settings.Default.Knoten_FarbeFilling = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Filling.Value).ToString()));
            Properties.Settings.Default.Knoten_FarbeBorder = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Border.Value).ToString()));
            Properties.Settings.Default.Kante_FarbeBorder = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_AEdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_REdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_GEdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_BEdge_Border.Value).ToString()));
            Properties.Settings.Default.Knoten_Höhe = this.Slider_Node_Size.Value;
            Properties.Settings.Default.Knoten_Breite = this.Slider_Node_Size.Value;
            Properties.Settings.Default.Knoten_Border_Thickness = this.Slider_Node_SizeStroke.Value;
            Properties.Settings.Default.Kanten_Thickness = this.Slider_Edge_SizeStroke.Value;

            //Speichere ab
            Properties.Settings.Default.Save();

            //Schließe das Fenster
            Close();
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                //Finde die Werte heraus
                #region
                //Finde die Werte für die Farbe der Füllung der Knoten heraus und runde sie
                byte knoten_AFill = byte.Parse(Math.Round(this.Slider_ANode_Filling.Value).ToString());
                byte knoten_RFill = byte.Parse(Math.Round(this.Slider_RNode_Filling.Value).ToString());
                byte knoten_GFill = byte.Parse(Math.Round(this.Slider_GNode_Filling.Value).ToString());
                byte knoten_BFill = byte.Parse(Math.Round(this.Slider_BNode_Filling.Value).ToString());

                //Finde den Wert für die Größe der Knoten heraus
                double knoten_Size = Math.Round(this.Slider_Node_Size.Value, 2);
                double knoten_SizeStroke = Math.Round(this.Slider_Node_SizeStroke.Value, 2);

                //Finde die Werte für die Farbe der Border der Knoten heraus und runde sie
                byte knoten_AStroke = byte.Parse(Math.Round(this.Slider_ANode_Border.Value).ToString());
                byte knoten_RStroke = byte.Parse(Math.Round(this.Slider_RNode_Border.Value).ToString());
                byte knoten_GStroke = byte.Parse(Math.Round(this.Slider_GNode_Border.Value).ToString());
                byte knoten_BStroke = byte.Parse(Math.Round(this.Slider_BNode_Border.Value).ToString());

                //Finde die Werte für die Farbe der Kanten heraus und runde sie
                byte kanten_AStroke = byte.Parse(Math.Round(this.Slider_AEdge_Border.Value).ToString());
                byte kanten_RStroke = byte.Parse(Math.Round(this.Slider_REdge_Border.Value).ToString());
                byte kanten_GStroke = byte.Parse(Math.Round(this.Slider_GEdge_Border.Value).ToString());
                byte kanten_BStroke = byte.Parse(Math.Round(this.Slider_BEdge_Border.Value).ToString());

                //Finde den Wert für die Dicke der Kanten heraus
                double kanten_Thickness = Math.Round(Slider_Edge_SizeStroke.Value, 2);
                #endregion

                //Lege die gerundeten Werte für die Slider fest, sodass sie keine Gleitkommazahlen enthalten können
                #region
                this.Slider_ANode_Filling.Value = knoten_AFill;
                this.Slider_RNode_Filling.Value = knoten_RFill;
                this.Slider_GNode_Filling.Value = knoten_GFill;
                this.Slider_BNode_Filling.Value = knoten_BFill;
                this.Slider_ANode_Border.Value = knoten_AStroke;
                this.Slider_RNode_Border.Value = knoten_RStroke;
                this.Slider_GNode_Border.Value = knoten_GStroke;
                this.Slider_BNode_Border.Value = knoten_BStroke;
                this.Slider_AEdge_Border.Value = kanten_AStroke;
                this.Slider_REdge_Border.Value = kanten_RStroke;
                this.Slider_GEdge_Border.Value = kanten_GStroke;
                this.Slider_BEdge_Border.Value = kanten_BStroke;
                #endregion

                //Wende die Änderungen für die Preview-Elemente an
                #region
                //Lege die Höhen/Breiten/Dicken für die Knoten fest
                this.EllipsePreview.StrokeThickness = knoten_SizeStroke;
                this.EllipsePreview.Height = knoten_Size;
                this.EllipsePreview.Width = knoten_Size;

                //Lege die Dicken der Kanten fest
                this.Kanten_Preview.StrokeThickness = kanten_Thickness;
                this.KantenSchlinge_Preview.StrokeThickness = kanten_Thickness;

                //Finde die eben berechneten Farben für die Knoten heraus und lege sie fest
                this.EllipsePreview.Stroke = new SolidColorBrush(Color.FromArgb(knoten_AStroke, knoten_RStroke, knoten_GStroke, knoten_BStroke));
                this.EllipsePreview.Fill = new SolidColorBrush(Color.FromArgb(knoten_AFill, knoten_RFill, knoten_GFill, knoten_BFill));

                //Finde die eben berechneten Farben für die Kanten heraus und lege sie fest
                SolidColorBrush kanten_Stroke = new SolidColorBrush(Color.FromArgb(kanten_AStroke, kanten_RStroke, kanten_GStroke, kanten_BStroke));
                this.Kanten_Preview.Stroke = kanten_Stroke;
                this.KantenSchlinge_Preview.Stroke = kanten_Stroke;
                #endregion

                //Synchronisiere die TextBoxen mit den Slidern
                #region
                this.TextBox_ANode_Filling.Text = knoten_AFill.ToString();
                this.TextBox_RNode_Filling.Text = knoten_RFill.ToString();
                this.TextBox_GNode_Filling.Text = knoten_GFill.ToString();
                this.TextBox_BNode_Filling.Text = knoten_BFill.ToString();
                this.TextBox_ANode_Border.Text = knoten_AStroke.ToString();
                this.TextBox_RNode_Border.Text = knoten_RStroke.ToString();
                this.TextBox_GNode_Border.Text = knoten_GStroke.ToString();
                this.TextBox_BNode_Border.Text = knoten_BStroke.ToString();
                this.TextBox_AEdge_Border.Text = kanten_AStroke.ToString();
                this.TextBox_REdge_Border.Text = kanten_RStroke.ToString();
                this.TextBox_GEdge_Border.Text = kanten_GStroke.ToString();
                this.TextBox_BEdge_Border.Text = kanten_BStroke.ToString();
                this.TextBox_Node_Size.Text = knoten_Size.ToString();
                this.TextBox_Node_SizeStroke.Text = knoten_SizeStroke.ToString();
                this.TextBox_Edge_SizeStroke.Text = kanten_Thickness.ToString();
                #endregion
            }
            catch { }
        }

        private void TextBoxes_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Falls in einer der TextBoxen "Enter" gedrückt wird, synchronisiere die TextBoxen mit den Slidern
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SyncSlidersAndTextBoxes(true);
            }
        }

        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            //Wenn eine TextBox den Fokus verliert, synchronisiere die TextBoxen mit den Slidern
            SyncSlidersAndTextBoxes(true);
        }

        private void SyncSlidersAndTextBoxes(bool playErrorSound)
        {
            bool errorSound = false;//Variable, die angibt, ob nachher wirklich ein Error-Sound gespielt werden muss

            //Knoten-Füllung
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_ANode_Filling.Value = byte.Parse(this.TextBox_ANode_Filling.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_ANode_Filling" zurück
                errorSound = playErrorSound;
                this.TextBox_ANode_Filling.Text = this.Slider_ANode_Filling.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_RNode_Filling.Value = byte.Parse(this.TextBox_RNode_Filling.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_RNode_Filling" zurück
                errorSound = playErrorSound;
                this.TextBox_RNode_Filling.Text = this.Slider_RNode_Filling.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_GNode_Filling.Value = byte.Parse(this.TextBox_GNode_Filling.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_GNode_Filling" zurück
                errorSound = playErrorSound;
                this.TextBox_GNode_Filling.Text = this.Slider_GNode_Filling.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_BNode_Filling.Value = byte.Parse(this.TextBox_BNode_Filling.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_BNode_Filling" zurück
                errorSound = playErrorSound;
                this.TextBox_BNode_Filling.Text = this.Slider_BNode_Filling.Value.ToString();
            }

            //Knoten-Stroke
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_ANode_Border.Value = byte.Parse(this.TextBox_ANode_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_ANode_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_ANode_Border.Text = this.Slider_ANode_Border.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_RNode_Border.Value = byte.Parse(this.TextBox_RNode_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_RNode_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_RNode_Border.Text = this.Slider_RNode_Border.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_GNode_Border.Value = byte.Parse(this.TextBox_GNode_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_GNode_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_GNode_Border.Text = this.Slider_GNode_Border.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_BNode_Border.Value = byte.Parse(this.TextBox_BNode_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_BNode_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_BNode_Border.Text = this.Slider_BNode_Border.Value.ToString();
            }

            //Kanten-Stroke
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_AEdge_Border.Value = byte.Parse(this.TextBox_AEdge_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_AEdge_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_AEdge_Border.Text = this.Slider_AEdge_Border.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_REdge_Border.Value = byte.Parse(this.TextBox_REdge_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_REdge_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_REdge_Border.Text = this.Slider_REdge_Border.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_GEdge_Border.Value = byte.Parse(this.TextBox_GEdge_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_GEdge_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_GEdge_Border.Text = this.Slider_GEdge_Border.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_BEdge_Border.Value = byte.Parse(this.TextBox_BEdge_Border.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_BEdge_Border" zurück
                errorSound = playErrorSound;
                this.TextBox_BEdge_Border.Text = this.Slider_BEdge_Border.Value.ToString();
            }

            //Größe der Knoten
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_Node_Size.Value = double.Parse(this.TextBox_Node_Size.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_Node_Size" zurück
                errorSound = playErrorSound;
                this.TextBox_Node_Size.Text = this.Slider_Node_Size.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_Node_SizeStroke.Value = double.Parse(this.TextBox_Node_SizeStroke.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_Node_SizeStroke" zurück
                errorSound = playErrorSound;
                this.TextBox_Node_SizeStroke.Text = this.Slider_Node_SizeStroke.Value.ToString();
            }

            //Dicke der Kanten
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_Edge_SizeStroke.Value = double.Parse(this.TextBox_Edge_SizeStroke.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_Edge_SizeStroke" zurück
                errorSound = playErrorSound;
                this.TextBox_Edge_SizeStroke.Text = this.Slider_Edge_SizeStroke.Value.ToString();
            }

            //Spiele einen Error-Sound, falls etwas schiefging
            if (errorSound)
            {
                SystemSounds.Asterisk.Play();
            }
        }
    }
}
