using Castor.Properties;
using System;
using System.Globalization;
using System.Media;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Castor
{
    public partial class Farbpicker : UserControl
    {
        public Farbpicker()
        {
            InitializeComponent();

            if (VisualGraph.Resman == null)
            {
                //initialisiere Resource und rufe die Kultur ab
                VisualGraph.Resman = new ResourceManager(typeof(Resources));
                VisualGraph.Cul = CultureInfo.CurrentUICulture;
                //cul = new CultureInfo("en");
                //cul = new CultureInfo("fr");
            }

            //Texte übersetzen.
            this.Text_A.Text = VisualGraph.Resman.GetString("A", VisualGraph.Cul);
            this.Text_R.Text = VisualGraph.Resman.GetString("R", VisualGraph.Cul);
            this.Text_G.Text = VisualGraph.Resman.GetString("G", VisualGraph.Cul);
            this.Text_B.Text = VisualGraph.Resman.GetString("B", VisualGraph.Cul);
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                //Finde die Werte für die Farbe der Füllung der Kante heraus und runde sie
                byte a = byte.Parse(Math.Round(this.Slider_A.Value).ToString());
                byte r = byte.Parse(Math.Round(this.Slider_R.Value).ToString());
                byte g = byte.Parse(Math.Round(this.Slider_G.Value).ToString());
                byte b = byte.Parse(Math.Round(this.Slider_B.Value).ToString());

                //Lege die gerundeten Werte für die Slider fest, sodass sie keine Gleitkommazahlen enthalten können
                this.Slider_A.Value = a;
                this.Slider_R.Value = r;
                this.Slider_G.Value = g;
                this.Slider_B.Value = b;

                //Synchronisiere die TextBoxen mit den Slidern
                this.TextBox_A.Text = a.ToString();
                this.TextBox_R.Text = r.ToString();
                this.TextBox_G.Text = g.ToString();
                this.TextBox_B.Text = b.ToString();
            }
            catch { }
        }

        private void TextBoxes_KeyUp(object sender, KeyEventArgs e)
        {
            //Falls in einer der TextBoxen "Enter" gedrückt wird, synchronisiere die TextBoxen mit den Slidern.
            if (e.Key == Key.Enter)
            {
                this.SyncSlidersAndTextBoxes(true);
            }
        }

        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            //Wenn eine TextBox den Fokus verliert, synchronisiere die TextBoxen mit den Slidern.
            this.SyncSlidersAndTextBoxes(true);
        }

        private void SyncSlidersAndTextBoxes(bool playErrorSound)
        {
            bool errorSound = false;//Variable, die angibt, ob nachher wirklich ein Error-Sound gespielt werden muss

            //Kanten-Stroke
            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_A.Value = byte.Parse(this.TextBox_A.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_A" zurück
                errorSound = playErrorSound;
                this.TextBox_A.Text = this.Slider_A.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_R.Value = byte.Parse(this.TextBox_R.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_R" zurück
                errorSound = playErrorSound;
                this.TextBox_R.Text = this.Slider_R.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_G.Value = byte.Parse(this.TextBox_G.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_G" zurück
                errorSound = playErrorSound;
                this.TextBox_G.Text = this.Slider_G.Value.ToString();
            }

            try
            {
                //Versuche den Wert des Sliders auf den Wert der TextBox zu setzten
                this.Slider_B.Value = byte.Parse(this.TextBox_B.Text);
            }
            catch
            {
                //Spiele einen Error-Sound (erst später, damit nicht mehrere auf einmal), falls der Wert nicht umgewandelt werden konnte und es gewollt ist, und setze dann den wert in der TextBox "TextBox_B" zurück
                errorSound = playErrorSound;
                this.TextBox_B.Text = this.Slider_B.Value.ToString();
            }

            //Spiele einen Error-Sound, falls etwas schiefging
            if (errorSound && this.PlayErrorSound)
            {
                SystemSounds.Asterisk.Play();
            }
        }

        private bool enabled = true;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                this.TextBox_A.IsEnabled = value;
                this.TextBox_R.IsEnabled = value;
                this.TextBox_G.IsEnabled = value;
                this.TextBox_B.IsEnabled = value;
                this.Text_A.IsEnabled = value;
                this.Text_R.IsEnabled = value;
                this.Text_G.IsEnabled = value;
                this.Text_B.IsEnabled = value;
                this.Slider_A.IsEnabled = value;
                this.Slider_R.IsEnabled = value;
                this.Slider_G.IsEnabled = value;
                this.Slider_B.IsEnabled = value;
            }
        }

        public bool PlayErrorSound = true;

        public Color GetColor()
        {
            return Color.FromArgb((byte)(this.Slider_A.Value), (byte)(this.Slider_R.Value), (byte)(this.Slider_G.Value), (byte)(this.Slider_B.Value));
        }

        public SolidColorBrush GetSolidColorBrush()
        {
            return new SolidColorBrush(this.GetColor());
        }

        public Brush GetBrush()
        {
            return this.GetSolidColorBrush();
        }

        public void SetColor(Color color)
        {
            this.Slider_A.Value = color.A;
            this.Slider_R.Value = color.R;
            this.Slider_G.Value = color.G;
            this.Slider_B.Value = color.B;
        }

        public void SetSolidColorBrush(SolidColorBrush solidColorBrush)
        {
            this.SetColor(solidColorBrush.Color);
        }
    }
}
