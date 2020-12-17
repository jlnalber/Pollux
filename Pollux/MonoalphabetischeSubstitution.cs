//Keine Using-Direktiven nötig

namespace Pollux.Verschlüsselungen
{
    public static partial class Verschlüsselungen
    {
        /// <summary>
        /// Verschlüsseln und Entschlüsseln mit Monoalphabetischer Substitution
        /// </summary>
        public static class MonoalphabetischeSubstitution
        {
            //Methoden zum Verschlüsseln mit Monoalphabetischer Substitution
            #region
            public static char Verschlüsseln(char klartext, string schlüssel)
            {
                klartext = char.Parse(klartext.ToString().ToUpper());
                schlüssel = schlüssel.ToUpper();
                int index;

                if ((index = Strings.Alphabet.IndexOf(klartext)) >= 0)
                {
                    return schlüssel[index];
                }
                else
                {
                    return klartext;
                }
            }

            public static string Verschlüsseln(string klartext, string schlüssel)
            {
                //Initailisierung von Varibalen/ Veränderung des Attributs
                string alphabet = Strings.Alphabet;
                string kryptotext = "";
                klartext = klartext.ToUpper();
                schlüssel = schlüssel.ToUpper();

                //geht den Klartext durch
                foreach (char i in klartext)
                {
                    if (alphabet.Contains(i))
                    {
                        //fügt das Zeichen im Schlüssel an der Position des Zeichens (aus dem Klartext) im Alphabet zum Kryptotext hinzu
                        kryptotext += schlüssel[alphabet.IndexOf(i)];
                    }
                    else
                    {
                        //falls das Zeichen nicht verschlüsselt werden kann, füge es einfach so hinzu
                        kryptotext += i;
                    }
                }

                //Rückgabe
                return kryptotext;
            }
            #endregion

            //Methoden zum Entschlüsseln mit Monoalphabetischer Substitution
            #region
            public static char Entschlüsseln(char kryptotext, string schlüssel)
            {
                kryptotext = char.Parse(kryptotext.ToString().ToUpper());
                schlüssel = schlüssel.ToUpper();
                int index;

                if ((index = schlüssel.IndexOf(kryptotext)) >= 0)
                {
                    return Strings.Alphabet[index];
                }
                else
                {
                    return kryptotext;
                }
            }

            public static string Entschlüsseln(string kryptotext, string schlüssel)
            {
                //Initailisierung von Varibalen/ Veränderung des Attributs
                string alphabet = Strings.Alphabet;
                string klartext = "";
                kryptotext = kryptotext.ToUpper();
                schlüssel = schlüssel.ToUpper();

                //geht den Kryptotext durch
                foreach (char i in kryptotext)
                {
                    if (schlüssel.Contains(i))
                    {
                        //fügt das Zeichen im Alphabet an der Position des Zeichens (aus dem Kryptotext) im Schlüssel zum Klartext hinzu
                        klartext += alphabet[schlüssel.IndexOf(i)];
                    }
                    else
                    {
                        //falls das Zeichen nicht verschlüsselt werden kann, füge es einfach so hinzu
                        klartext += i;
                    }
                }

                //Rückgabe
                return klartext;
            }
            #endregion
        }
    }
}