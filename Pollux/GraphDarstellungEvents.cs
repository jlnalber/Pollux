using System.Windows;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        //Bei einem Doppelklick auf den Canvas soll an der Position der Maus ein Knoten hinzugefügt werden
        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                try
                {
                    //Suche nach der Position der Maus
                    Point point = e.GetPosition(this.Canvas);

                    //Mache es so, dass es mit dem Scroll übereinstimmt und der Knoten seinen Mittelpunkt dort hat, wo die Maus ist
                    double canvasLeft = this.Canvas.Children.Count != 0 ? System.Windows.Controls.Canvas.GetLeft(this.Canvas.Children[0]) : 0;
                    double canvasTop = this.Canvas.Children.Count != 0 ? System.Windows.Controls.Canvas.GetTop(this.Canvas.Children[0]) : 0;
                    point.X -= Properties.Settings.Default.Knoten_Breite / 2 + canvasLeft;
                    point.Y -= Properties.Settings.Default.Knoten_Höhe / 2 + canvasTop;

                    //Gebe der CommandConsole den Befehl den Knoten hinzuzufügen
                    int number = this.GraphKnoten.Count;
                    for (; this.SucheKnoten("NODE" + number) != null; ++number) ;
                    MainWindow.main.GetOpenConsole().Command("ADD NODE" + number + " AT " + point.X + " AND " + point.Y);
                }
                catch { }
            }
        }
    }
}
