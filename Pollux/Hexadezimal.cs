using System.Windows.Media;

namespace Pollux
{
    public static class Hexadezimal
    {
        public static string NumberAsHexa(int number)
        {
            if (number == 0)
            {
                return "00";
            }

            //Initialisierung
            string numberAsString = "";
            string singleNumbersAsChar = "0123456789ABCDEF";

            //Rechne die Zahl aus
            int restNumber = number;
            while (restNumber != 0)
            {
                int rest = restNumber % 16;
                restNumber = (restNumber - rest) / 16;
                numberAsString = numberAsString.Insert(0, singleNumbersAsChar[rest].ToString());
            }

            //Rückgabe
            return numberAsString;
        }

        public static string BrushAsHexa(SolidColorBrush brush)
        {
            return "#" + NumberAsHexa(brush.Color.R) + NumberAsHexa(brush.Color.G) + NumberAsHexa(brush.Color.B) + NumberAsHexa(brush.Color.A);
        }

        public static string BrushAsHexa(Brush brush)
        {
            return BrushAsHexa((SolidColorBrush)brush);
        }
    }
}
