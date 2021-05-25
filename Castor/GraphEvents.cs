using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Thestias;

namespace Castor
{
    public partial class VisualGraph
    {
        public void KanteHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            this.OpenAddEdgeWindow();
        }

        public void KnotenHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            this.OpenAddVertexWindow();
        }

        public void EigenschaftenFenster_Click(object sender, RoutedEventArgs e)
        {
            this.OpenPropertiesWindow();
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            const double scrollSpeed = 0.25;
            const double zoomSpeed = 0.001;
            if (this.Canvas.Children.Count != 0)
            {
                //Zoom
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    //Berechne den Zoom und suche nach der Position der Maus
                    Point point = e.GetPosition(this.Canvas);
                    double zoom = 1 + e.Delta * zoomSpeed;
                    try
                    {
                        zoom = e.Delta * zoomSpeed + ((ScaleTransform)this.Canvas.RenderTransform).ScaleX;
                    }
                    catch { }

                    //Zoome herein oder heraus
                    this.SetZoom(zoom, point);
                }

                //Horizontaler Scroll
                else if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    this.SetScrollX(Canvas.GetLeft(this.Canvas.Children[0]) + e.Delta * scrollSpeed);
                }

                //Vertikaler Scroll
                else
                {
                    this.SetScrollY(Canvas.GetTop(this.Canvas.Children[0]) + e.Delta * scrollSpeed);
                }
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //TODO: IsEditable implementieren
            if (e.ClickCount == 2)
            {
                try
                {
                    //Suche nach der Position der Maus
                    Point point = e.GetPosition(this.Canvas);

                    //Mache es so, dass es mit dem Scroll übereinstimmt und der Knoten seinen Mittelpunkt dort hat, wo die Maus ist
                    double canvasLeft = this.Canvas.Children.Count != 0 ? Canvas.GetLeft(this.Canvas.Children[0]) : 0;
                    double canvasTop = this.Canvas.Children.Count != 0 ? Canvas.GetTop(this.Canvas.Children[0]) : 0;
                    point.X -= Properties.Settings.Default.Knoten_Breite / 2 + canvasLeft;
                    point.Y -= Properties.Settings.Default.Knoten_Höhe / 2 + canvasTop;

                    //Gebe der CommandConsole den Befehl den Knoten hinzuzufügen
                    this.AddVertex(point.X, point.Y);
                }
                catch { }
            }
        }

        private void ReloadProperties_Event(object sender, Graph.ChangedEventArgs e)
        {
            this.ReloadProperties();
        }
    }
}
