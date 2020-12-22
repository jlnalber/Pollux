namespace Pollux.Verschlüsselungen
{
    public static partial class Verschlüsselungen
    {
        /// <summary>
        /// Verschlüsseln, Entschlüsseln und Knacken mit Caesar
        /// </summary>
        public static class Caesar
        {
            //Methoden zum Verschlüsseln mit Caesar
            #region
            public static char Verschlüsseln(char buchstabe, char schlüssel)
            {
                //Intialisiere Variablen
                string alphabet = Strings.Alphabet;
                char buchstabeGroß = char.Parse(buchstabe.ToString().ToUpper());

                //falls es gar kein Buchstabe ist, mach ein Error
                if (!alphabet.Contains(buchstabeGroß))
                {
                    throw new System.IndexOutOfRangeException();
                }

                //speichere die Schlüsselnummer ab
                int schlüsselnummer = alphabet.IndexOf(char.Parse(schlüssel.ToString().ToUpper()));

                //sucht im alphabet den zu verschlüsselnden Buchstaben (großgeschrieben) und gibt den Buchstaben im alphabet, nachdem der Schlüssel daraufgerechnet wurde, zurück
                return alphabet[(alphabet.IndexOf(buchstabeGroß) + schlüsselnummer) % 26];
            }

            public static char Verschlüsseln(char buchstabe, int schlüsselnummer)
            {
                //Intialisiere Variablen
                string alphabet = Strings.Alphabet;
                char buchstabeGroß = char.Parse(buchstabe.ToString().ToUpper());

                //falls es gar kein Buchstabe ist, mach ein Error
                if (!alphabet.Contains(buchstabeGroß))
                {
                    throw new System.IndexOutOfRangeException();
                }

                //sucht im alphabet den zu verschlüsselnden Buchstaben (großgeschrieben) und gibt den Buchstaben im alphabet, nachdem der Schlüssel daraufgerechnet wurde, zurück
                return alphabet[(alphabet.IndexOf(buchstabeGroß) + schlüsselnummer) % 26];
            }

            public static string Verschlüsseln(string klartext, char schlüssel)
            {
                //nutzt seine Überladung, um jeden Buchstaben einzeln zu verschlüsseln; gibt dann die string zurück
                string kryptotext = "";
                foreach (char i in klartext)
                {
                    try
                    {
                        kryptotext += Verschlüsseln(i, schlüssel);
                    }
                    catch (System.IndexOutOfRangeException e)
                    {
                        string nothing = e.Message;
                        kryptotext += i;
                    }
                }
                return kryptotext;
            }

            public static string Verschlüsseln(string klartext, int schlüsselnummer)
            {
                //nutzt seine Überladung, um jeden Buchstaben einzeln zu verschlüsseln; gibt dann die string zurück
                string kryptotext = "";
                foreach (char i in klartext)
                {
                    try
                    {
                        kryptotext += Verschlüsseln(i, schlüsselnummer);
                    }
                    catch (System.IndexOutOfRangeException e)
                    {
                        string nothing = e.Message;
                        kryptotext += i;
                    }
                }
                return kryptotext;
            }
            #endregion

            //Methoden zum Entschlüsseln mit Caesar
            #region
            public static char Entschlüsseln(char buchstabe, char schlüssel)
            {
                //Intialisiere Variablen
                string alphabet = Strings.Alphabet;
                char buchstabeGroß = char.Parse(buchstabe.ToString().ToUpper());

                //falls es gar kein Buchstabe ist, mach ein Error
                if (!alphabet.Contains(buchstabeGroß))
                {
                    throw new System.IndexOutOfRangeException();
                }

                //speichere die Schlüsselnummer ab
                int schlüsselnummer = 26 - alphabet.IndexOf(char.Parse(schlüssel.ToString().ToUpper()));

                //sucht im alphabet den zu verschlüsselnden Buchstaben (großgeschrieben) und gibt den Buchstaben im alphabet, nachdem der Schlüssel daraufgerechnet wurde, zurück
                return alphabet[(alphabet.IndexOf(buchstabeGroß) + schlüsselnummer) % 26];
            }

            public static char Entschlüsseln(char buchstabe, int schlüsselnummer)
            {
                //Intialisiere Variablen
                string alphabet = Strings.Alphabet;
                char buchstabeGroß = char.Parse(buchstabe.ToString().ToUpper());

                //falls es gar kein Buchstabe ist, mach ein Error
                if (!alphabet.Contains(buchstabeGroß))
                {
                    throw new System.IndexOutOfRangeException();
                }

                //sucht im alphabet den zu verschlüsselnden Buchstaben (großgeschrieben) und gibt den Buchstaben im alphabet, nachdem der Schlüssel daraufgerechnet wurde, zurück
                return alphabet[(alphabet.IndexOf(buchstabeGroß) + 26 - schlüsselnummer) % 26];
            }

            public static string Entschlüsseln(string kryptotext, char schlüssel)
            {
                //initialisiere Variablen
                string klartext = "";

                //gehe den Kryptotext durch und entschlüssle jeweils
                foreach (char i in kryptotext)
                {
                    try
                    {
                        klartext += Entschlüsseln(i, schlüssel);
                    }
                    //falls das Zeichen nicht verschlüsselt werden kann, füge es einfach dem klartext so hinzu
                    catch (System.IndexOutOfRangeException e)
                    {
                        string nothing = e.Message;
                        klartext += i;
                    }
                }

                //Rückgabe
                return klartext;
            }

            public static string Entschlüsseln(string kryptotext, int schlüsselnummer)
            {
                //initialisiere Variablen
                string klartext = "";

                //gehe den Kryptotext durch und entschlüssle jeweils
                foreach (char i in kryptotext)
                {
                    try
                    {
                        klartext += Entschlüsseln(i, schlüsselnummer);
                    }
                    //falls das Zeichen nicht verschlüsselt werden kann, füge es einfach dem klartext so hinzu
                    catch (System.IndexOutOfRangeException e)
                    {
                        string nothing = e.Message;
                        klartext += i;
                    }
                }

                //Rückgabe
                return klartext;
            }
            #endregion

            //Methode zum Kancken von Caesar
            #region
            public static string Knacken(string kryptotext)
            {
                //Alphabet
                string alphabet = Strings.Alphabet;

                //mache eine Haüfigkeitsanalyse und finde so den Schlüssel (sucht im Alphabet die Stelle des Häufigsten Buchstabens und rechnet es plus 22 -> Schlüssel)
                return Entschlüsseln(kryptotext, alphabet[(alphabet.IndexOf(Strings.HäufigsterBuchstabe(kryptotext)) + 22) % 26]);
            }
            #endregion
        }
    }
}