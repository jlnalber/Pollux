using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        public class KnotenDarstellung
        {
            //members von der Klasse
            public Pollux.Graph.Graph.Knoten Knoten;
            public Ellipse Ellipse;
            public Canvas Canvas;
            public MouseButtonEventArgs MouseButtonEventArgs;
            public Label Label;
            public const int LabelToRight = 25;
            public const int LabelToTop = -20;

            //Konstruktor der Klasse
            public KnotenDarstellung(Graph.Graph.Knoten knoten, Ellipse ellipse, Label label, Canvas canvas)
            {
                this.Knoten = knoten;
                this.Ellipse = ellipse;
                this.Canvas = canvas;
                this.Ellipse.MouseMove += Ellipse_MouseMove;
                this.Label = label;
            }

            //Event-Methode, die die Ellipsen bewegt, und die Kanten neu malt
            private void Ellipse_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Thickness thickness = new Thickness();
                    Point point = e.GetPosition(this.Canvas);
                    thickness.Left = point.X - this.Ellipse.Width / 2;
                    thickness.Top = point.Y - this.Ellipse.Height / 2;
                    this.Ellipse.Margin = thickness;
                    this.Label.Margin = new(this.Ellipse.Margin.Left + LabelToRight, this.Ellipse.Margin.Top - LabelToTop, 10, 10);
                    MainWindow.main.DrawGraph(this.Canvas);
                }
            }

            //Methode, um den Graph neu malen zu lassen
            public void Redraw()
            {
                MainWindow.main.DrawGraph(this.Canvas);
            }
        }
    }
}
