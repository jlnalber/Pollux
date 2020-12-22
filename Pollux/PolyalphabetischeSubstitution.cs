namespace Pollux.Verschlüsselungen
{
    public static partial class Verschlüsselungen
    {
        /// <summary>
        /// Verschlüsseln und Entschlüsseln mit Polyalphabetischer Substitution
        /// </summary>
        public static class PolyalphabetischeSubstitution
        {
            //Methoden zum Verschlüsseln mit Polyalphabetischer Substitution
            #region
            public static char Verschlüsseln(char klartext, string[] schlüssel)
            {
                return MonoalphabetischeSubstitution.Verschlüsseln(klartext, schlüssel[0]);
            }

            public static string Verschlüsseln(string klartext, string[] schlüssel)
            {
                string kryptotext = "";
                klartext = klartext.ToUpper();
                int length = schlüssel.Length;

                int counter = 0;
                foreach (char i in klartext)
                {
                    if (Strings.Alphabet.Contains(i))
                    {
                        kryptotext += MonoalphabetischeSubstitution.Verschlüsseln(i, schlüssel[counter % length]);
                        counter++;
                    }
                    else
                    {
                        kryptotext += i;
                    }
                }

                return kryptotext;
            }
            #endregion

            //Methoden zum Entschlüsseln mit Polyalphabetischer Substitution
            #region
            public static char Entschlüsseln(char kryptotext, string[] schlüssel)
            {
                return MonoalphabetischeSubstitution.Entschlüsseln(kryptotext, schlüssel[0]);
            }

            public static string Entschlüsseln(string kryptotext, string[] schlüssel)
            {
                string klartext = "";
                kryptotext = kryptotext.ToUpper();

                int counter = 0;
                foreach (char i in kryptotext)
                {
                    string strSchlüssel = schlüssel[counter % schlüssel.Length].ToUpper();
                    if (strSchlüssel.Contains(i))
                    {
                        klartext += MonoalphabetischeSubstitution.Entschlüsseln(i, strSchlüssel);
                        counter++;
                    }
                    else
                    {
                        klartext += i;
                    }
                }

                return klartext;
            }
            #endregion
        }
    }
}
