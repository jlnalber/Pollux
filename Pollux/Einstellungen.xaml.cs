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
            try
            {
                this.InitializeComponent();

                //Übersetze die Texte
                #region
                this.Title = MainWindow.Resman.GetString("EinstellungenTitle", MainWindow.Cul);
                this.AppearanceHeader.Text = MainWindow.Resman.GetString("Appearance", MainWindow.Cul);

                this.Node_Design_Text.Text = MainWindow.Resman.GetString("Node_Design_Text", MainWindow.Cul);

                this.Node_DesignFilling_Text.Text = MainWindow.Resman.GetString("Node_DesignFilling_Text", MainWindow.Cul);
                this.Slider_RNode_Filling_Text.Text = MainWindow.Resman.GetString("R", MainWindow.Cul);
                this.Slider_GNode_Filling_Text.Text = MainWindow.Resman.GetString("G", MainWindow.Cul);
                this.Slider_BNode_Filling_Text.Text = MainWindow.Resman.GetString("B", MainWindow.Cul);
                this.Slider_ANode_Filling_Text.Text = MainWindow.Resman.GetString("A", MainWindow.Cul);

                this.Node_DesignFilling2_CheckBox.Content = MainWindow.Resman.GetString("Node_DesignFilling2_CheckBox", MainWindow.Cul);
                this.Slider_RNode_Filling2_Text.Text = MainWindow.Resman.GetString("R", MainWindow.Cul);
                this.Slider_GNode_Filling2_Text.Text = MainWindow.Resman.GetString("G", MainWindow.Cul);
                this.Slider_BNode_Filling2_Text.Text = MainWindow.Resman.GetString("B", MainWindow.Cul);
                this.Slider_ANode_Filling2_Text.Text = MainWindow.Resman.GetString("A", MainWindow.Cul);

                this.Node_DesignBorder_Text.Text = MainWindow.Resman.GetString("Node_DesignBorder_Text", MainWindow.Cul);
                this.Slider_RNode_Border_Text.Text = MainWindow.Resman.GetString("R", MainWindow.Cul);
                this.Slider_GNode_Border_Text.Text = MainWindow.Resman.GetString("G", MainWindow.Cul);
                this.Slider_BNode_Border_Text.Text = MainWindow.Resman.GetString("B", MainWindow.Cul);
                this.Slider_ANode_Border_Text.Text = MainWindow.Resman.GetString("A", MainWindow.Cul);

                this.Node_DesignSizes_Text.Text = MainWindow.Resman.GetString("Node_DesignSizes_Text", MainWindow.Cul);
                this.Slider_Node_Size_Text.Text = MainWindow.Resman.GetString("Slider_Node_Size_Text", MainWindow.Cul);
                this.Slider_Node_SizeStroke_Text.Text = MainWindow.Resman.GetString("Slider_Node_SizeStroke_Text", MainWindow.Cul);

                this.Edge_Design_Text.Text = MainWindow.Resman.GetString("Edge_Design_Text", MainWindow.Cul);
                this.Edge_DesignBorder_Text.Text = MainWindow.Resman.GetString("Edge_DesignBorder_Text", MainWindow.Cul);
                this.Slider_REdge_Border_Text.Text = MainWindow.Resman.GetString("R", MainWindow.Cul);
                this.Slider_GEdge_Border_Text.Text = MainWindow.Resman.GetString("G", MainWindow.Cul);
                this.Slider_BEdge_Border_Text.Text = MainWindow.Resman.GetString("B", MainWindow.Cul);
                this.Slider_AEdge_Border_Text.Text = MainWindow.Resman.GetString("A", MainWindow.Cul);

                this.Edge_DesignSizes_Text.Text = MainWindow.Resman.GetString("Edge_DesignSizes_Text", MainWindow.Cul);
                this.Slider_Edge_SizeStroke_Text.Text = MainWindow.Resman.GetString("Slider_Edge_SizeStroke_Text", MainWindow.Cul);

                this.Apply.Content = MainWindow.Resman.GetString("Apply", MainWindow.Cul);
                this.Apply.ToolTip = MainWindow.Resman.GetString("ToolTipApply", MainWindow.Cul);
                this.Preview_Text.Text = MainWindow.Resman.GetString("Preview_Text", MainWindow.Cul);
                this.Knoten_Preview_Text.Text = MainWindow.Resman.GetString("Knoten_Preview_Text", MainWindow.Cul);
                this.Kanten_Preview_Text.Text = MainWindow.Resman.GetString("Kanten_Preview_Text", MainWindow.Cul);
                this.KantenSchlinge_Preview_Text.Text = MainWindow.Resman.GetString("KantenSchlinge_Preview_Text", MainWindow.Cul);

                this.ConsoleHeader.Text = MainWindow.Resman.GetString("ConsoleHeaderText", MainWindow.Cul);
                this.HeaderConsoleText.Text = MainWindow.Resman.GetString("HeaderConsoleText", MainWindow.Cul);
                this.FontFamilyPickerText.Text = MainWindow.Resman.GetString("FontFamilyPickerText", MainWindow.Cul);
                this.FontSizeBoxText.Text = MainWindow.Resman.GetString("FontSizeBoxText", MainWindow.Cul);
                this.PreviewConsoleText.Text = MainWindow.Resman.GetString("PreviewConsoleText", MainWindow.Cul);
                #endregion

                //Stelle die Slider je nach Einstellung ein
                #region
                this.Slider_RNode_Filling.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling.R;
                this.Slider_GNode_Filling.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling.G;
                this.Slider_BNode_Filling.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling.B;
                this.Slider_ANode_Filling.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling.A;

                this.Node_DesignFilling2_CheckBox.IsChecked = Castor.Properties.Settings.Default.Transition;
                this.Slider_RNode_Filling2.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling2.R;
                this.Slider_GNode_Filling2.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling2.G;
                this.Slider_BNode_Filling2.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling2.B;
                this.Slider_ANode_Filling2.Value = Castor.Properties.Settings.Default.Knoten_FarbeFilling2.A;

                this.Slider_Node_Size.Value = Castor.Properties.Settings.Default.Knoten_Höhe;
                this.Slider_Node_SizeStroke.Value = Castor.Properties.Settings.Default.Knoten_Border_Thickness;

                this.Slider_RNode_Border.Value = Castor.Properties.Settings.Default.Knoten_FarbeBorder.R;
                this.Slider_GNode_Border.Value = Castor.Properties.Settings.Default.Knoten_FarbeBorder.G;
                this.Slider_BNode_Border.Value = Castor.Properties.Settings.Default.Knoten_FarbeBorder.B;
                this.Slider_ANode_Border.Value = Castor.Properties.Settings.Default.Knoten_FarbeBorder.A;

                this.Slider_REdge_Border.Value = Castor.Properties.Settings.Default.Kante_FarbeBorder.R;
                this.Slider_GEdge_Border.Value = Castor.Properties.Settings.Default.Kante_FarbeBorder.G;
                this.Slider_BEdge_Border.Value = Castor.Properties.Settings.Default.Kante_FarbeBorder.B;
                this.Slider_AEdge_Border.Value = Castor.Properties.Settings.Default.Kante_FarbeBorder.A;

                this.Slider_Edge_SizeStroke.Value = Castor.Properties.Settings.Default.Kanten_Thickness;
                #endregion

                //Einstellungen für die Konsole
                #region
                switch (Properties.Settings.Default.FontConsole)
                {
                    case "Consolas": this.FontFamilyPicker.SelectedItem = this.FontFamilyConsolas; break;
                    case "Courier New": this.FontFamilyPicker.SelectedItem = this.FontFamilyCourierNew; break;
                    case "Lucida Console": this.FontFamilyPicker.SelectedItem = this.FontFamilyLucidaConsole; break;
                }

                FontFamily fontFamily = new(this.FontFamilyPicker.Text);
                double fontSize = Properties.Settings.Default.FontSizeConsole;
                this.FontFamilyPicker.FontFamily = fontFamily;
                this.PreviewConsole.FontFamily = fontFamily;
                this.FontSizeBox.Text = fontSize.ToString();
                this.PreviewConsole.FontSize = fontSize;
                #endregion

                this.FontFamilyPicker.SelectionChanged += this.Font_SelectionChanged;

                //Stelle die TextBoxes nach den Slidern ein
                this.SyncSlidersAndTextBoxes(false);
            }
            catch
            {
                MessageBox.Show(MainWindow.Resman.GetString("ErrorEinstellungen", MainWindow.Cul));
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            //Wende die Eintellungen an
            Castor.Properties.Settings.Default.Knoten_FarbeFilling = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Filling.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Filling.Value).ToString()));
            Castor.Properties.Settings.Default.Knoten_FarbeFilling2 = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Filling2.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Filling2.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Filling2.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Filling2.Value).ToString()));
            Castor.Properties.Settings.Default.Knoten_FarbeBorder = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_ANode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_RNode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_GNode_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_BNode_Border.Value).ToString()));
            Castor.Properties.Settings.Default.Kante_FarbeBorder = System.Drawing.Color.FromArgb(int.Parse(Math.Round(this.Slider_AEdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_REdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_GEdge_Border.Value).ToString()), int.Parse(Math.Round(this.Slider_BEdge_Border.Value).ToString()));
            Castor.Properties.Settings.Default.Knoten_Höhe = this.Slider_Node_Size.Value;
            Castor.Properties.Settings.Default.Knoten_Breite = this.Slider_Node_Size.Value;
            Castor.Properties.Settings.Default.Knoten_Border_Thickness = this.Slider_Node_SizeStroke.Value;
            Castor.Properties.Settings.Default.Kanten_Thickness = this.Slider_Edge_SizeStroke.Value;
            Castor.Properties.Settings.Default.Transition = this.Node_DesignFilling2_CheckBox.IsChecked == true;
            Properties.Settings.Default.FontConsole = this.FontFamilyPicker.Text;
            try
            {
                Properties.Settings.Default.FontSizeConsole = double.Parse(this.FontSizeBox.Text);
            }
            catch { }

            //Speichere ab
            Properties.Settings.Default.Save();
            Castor.Properties.Settings.Default.Save();

            //Schließe das Fenster
            this.Close();
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

                //Finde die Werte für die Farbe der 2. Füllung der Knoten heraus und runde sie
                byte knoten_AFill2 = byte.Parse(Math.Round(this.Slider_ANode_Filling2.Value).ToString());
                byte knoten_RFill2 = byte.Parse(Math.Round(this.Slider_RNode_Filling2.Value).ToString());
                byte knoten_GFill2 = byte.Parse(Math.Round(this.Slider_GNode_Filling2.Value).ToString());
                byte knoten_BFill2 = byte.Parse(Math.Round(this.Slider_BNode_Filling2.Value).ToString());

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
                double kanten_Thickness = Math.Round(this.Slider_Edge_SizeStroke.Value, 2);
                #endregion

                //Lege die gerundeten Werte für die Slider fest, sodass sie keine Gleitkommazahlen enthalten können
                #region
                this.Slider_ANode_Filling.Value = knoten_AFill;
                this.Slider_RNode_Filling.Value = knoten_RFill;
                this.Slider_GNode_Filling.Value = knoten_GFill;
                this.Slider_BNode_Filling.Value = knoten_BFill;
                this.Slider_ANode_Filling2.Value = knoten_AFill2;
                this.Slider_RNode_Filling2.Value = knoten_RFill2;
                this.Slider_GNode_Filling2.Value = knoten_GFill2;
                this.Slider_BNode_Filling2.Value = knoten_BFill2;
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
                if (Node_DesignFilling2_CheckBox.IsChecked == true)
                {
                    this.EllipsePreview.Fill = new LinearGradientBrush(Color.FromArgb(knoten_AFill, knoten_RFill, knoten_GFill, knoten_BFill), Color.FromArgb(knoten_AFill2, knoten_RFill2, knoten_GFill2, knoten_BFill2), 45);
                }
                else
                {

                    this.EllipsePreview.Fill = new LinearGradientBrush(Color.FromArgb(knoten_AFill, knoten_RFill, knoten_GFill, knoten_BFill), Color.FromArgb(knoten_AFill, knoten_RFill, knoten_GFill, knoten_BFill), 45);
                }

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
                this.TextBox_ANode_Filling2.Text = knoten_AFill2.ToString();
                this.TextBox_RNode_Filling2.Text = knoten_RFill2.ToString();
                this.TextBox_GNode_Filling2.Text = knoten_GFill2.ToString();
                this.TextBox_BNode_Filling2.Text = knoten_BFill2.ToString();
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
                this.SyncSlidersAndTextBoxes(true);
            }
        }

        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            //Wenn eine TextBox den Fokus verliert, synchronisiere die TextBoxen mit den Slidern
            this.SyncSlidersAndTextBoxes(true);
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

            //Knoten-Füllung2
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_ANode_Filling2.Value = byte.Parse(this.TextBox_ANode_Filling2.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_ANode_Filling2" zurück
                errorSound = playErrorSound;
                this.TextBox_ANode_Filling2.Text = this.Slider_ANode_Filling2.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_RNode_Filling2.Value = byte.Parse(this.TextBox_RNode_Filling2.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_RNode_Filling2" zurück
                errorSound = playErrorSound;
                this.TextBox_RNode_Filling2.Text = this.Slider_RNode_Filling2.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_GNode_Filling2.Value = byte.Parse(this.TextBox_GNode_Filling2.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_GNode_Filling2" zurück
                errorSound = playErrorSound;
                this.TextBox_GNode_Filling2.Text = this.Slider_GNode_Filling2.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_BNode_Filling2.Value = byte.Parse(this.TextBox_BNode_Filling2.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_BNode_Filling2" zurück
                errorSound = playErrorSound;
                this.TextBox_BNode_Filling2.Text = this.Slider_BNode_Filling2.Value.ToString();
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

        private void Node_DesignFilling2_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Slider_ANode_Filling2.IsEnabled = true;
                this.Slider_RNode_Filling2.IsEnabled = true;
                this.Slider_GNode_Filling2.IsEnabled = true;
                this.Slider_BNode_Filling2.IsEnabled = true;
                this.Slider_ANode_Filling2_Text.IsEnabled = true;
                this.Slider_RNode_Filling2_Text.IsEnabled = true;
                this.Slider_GNode_Filling2_Text.IsEnabled = true;
                this.Slider_BNode_Filling2_Text.IsEnabled = true;
                this.TextBox_ANode_Filling2.IsEnabled = true;
                this.TextBox_RNode_Filling2.IsEnabled = true;
                this.TextBox_GNode_Filling2.IsEnabled = true;
                this.TextBox_BNode_Filling2.IsEnabled = true;
                this.Sliders_ValueChanged(sender, new RoutedPropertyChangedEventArgs<double>(0, 100));
            }
            catch { }
        }

        private void Node_DesignFilling2_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Slider_ANode_Filling2.IsEnabled = false;
                this.Slider_RNode_Filling2.IsEnabled = false;
                this.Slider_GNode_Filling2.IsEnabled = false;
                this.Slider_BNode_Filling2.IsEnabled = false;
                this.Slider_ANode_Filling2_Text.IsEnabled = false;
                this.Slider_RNode_Filling2_Text.IsEnabled = false;
                this.Slider_GNode_Filling2_Text.IsEnabled = false;
                this.Slider_BNode_Filling2_Text.IsEnabled = false;
                this.TextBox_ANode_Filling2.IsEnabled = false;
                this.TextBox_RNode_Filling2.IsEnabled = false;
                this.TextBox_GNode_Filling2.IsEnabled = false;
                this.TextBox_BNode_Filling2.IsEnabled = false;
                this.Sliders_ValueChanged(sender, new RoutedPropertyChangedEventArgs<double>(0, 100));
            }
            catch { }
        }

        private void Font_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                //FontFamily "fontFamily"
                FontFamily fontFamily = new("");
                if (this.FontFamilyPicker.SelectedItem == this.FontFamilyConsolas)
                {
                    fontFamily = new("Consolas");
                }
                else if (this.FontFamilyPicker.SelectedItem == this.FontFamilyCourierNew)
                {
                    fontFamily = new("Courier New");
                }
                else if (this.FontFamilyPicker.SelectedItem == this.FontFamilyLucidaConsole)
                {
                    fontFamily = new("Lucida Console");
                }
                this.FontFamilyPicker.FontFamily = fontFamily;
                this.PreviewConsole.FontFamily = fontFamily;

                //FontSize
                if (double.Parse(this.FontSizeBox.Text) > 512)
                {
                    this.FontSizeBox.Text = "512";
                    this.FontSizeBox.SelectAll();
                }
                this.FontSizeBox.Text = Stuff.ToNumberAsString(this.FontSizeBox.Text);
                this.PreviewConsole.FontSize = double.Parse(this.FontSizeBox.Text);
            }
            catch
            {
                //Bei einem Fehler einfach zurücksetzen
                this.FontSizeBox.Text = "15";
                this.FontSizeBox.SelectAll();
            }
        }
    }
}
