using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Castor
{
    public partial class VisualGraph
    {
        public void SetZoom(double zoom, System.Windows.Point point)
        {
            //Initialisierung
            const double zoomMax = 5;
            const double zoomMin = 0.25;
            double height = this.Canvas.ActualHeight;
            double width = this.Canvas.ActualWidth;

            //Zoome herein oder heraus
            if (zoom >= 1 && zoom < zoomMax)
            {
                this.Canvas.RenderTransform = new ScaleTransform(zoom, zoom, point.X, point.Y);
            }
            else if (zoom > zoomMin && zoom < 1)
            {
                this.Canvas.RenderTransform = new ScaleTransform(zoom, zoom, 0, 0);
                this.Canvas.Width = width / zoom;
                this.Canvas.Height = height / zoom;
            }
        }

        public void SetScrollY(double y)
        {
            //Methode, um vertikal zu scrollen
            foreach (UIElement i in this.Canvas.Children)
            {
                Canvas.SetTop(i, y);
            }
        }

        public void SetScrollX(double x)
        {
            //Methode, um horizontal zu scrollen
            foreach (UIElement i in this.Canvas.Children)
            {
                Canvas.SetLeft(i, x);
            }
        }
    }
}
