using System;
using System.Collections.Generic;
using System.Linq;

namespace Pollux.Verschlüsselungen
{
    public static partial class Verschlüsselungen
    {
        /// <summary>
        /// Verschlüsseln, Entschlüsseln und Knacken mit Vigenère
        /// </summary>
        public static class Vigenère
        {
            //Einstellungen
            #region
            public static int TextLänge { get; set; }

            public static int MinZeichenfolgeLänge { get; set; }

            public static int MindestesVorkommen { get; set; }
            #endregion

            //Methode zum Verschlüsseln mit Vigenère
            #region
            public static string Verschlüsseln(string klartext, string schlüssel)
            {
                //intialisiere eine Variable zum Abspeichern des Kryptotextes / großmachen des Textes
                string kryptotext = "";
                klartext = klartext.ToUpper();

                //mache einen counter (for Erweiterung?!)
                int counter = 0;

                //gehe den Klartext durch und verschlüssele jede char mit Caesar einzeln und dem Schlüssel, der für diese Stelle vorhergesehen ist
                foreach (char i in klartext)
                {
                    if (Strings.Alphabet.Contains(i))
                    {
                        kryptotext += Caesar.Verschlüsseln(i, schlüssel[counter % schlüssel.Length]);
                        counter++;
                    }
                    else
                    {
                        kryptotext += i;
                    }
                }

                //Rückgabe
                return kryptotext;
            }
            #endregion

            //Methode zum Entschlüsseln mit Vigenère
            #region
            public static string Entschlüsseln(string kryptotext, string schlüssel)
            {
                //intitialisierung der Variablen
                string klartext = "";
                kryptotext = kryptotext.ToUpper();

                //erstelle counter
                int counter = 0;

                //gehe kryptotext durch und entschlüssele jedes Zeichen mit dem richtigen Schlüssel (gezählt von counter)
                foreach (char i in kryptotext)
                {
                    if (Strings.Alphabet.Contains(i))
                    {
                        klartext += Caesar.Entschlüsseln(i, schlüssel[counter % schlüssel.Length]);
                        counter++;
                    }
                    //Falls das Zeichen nicht entschlüsselt werden kann, füge es einfach zum KLartext hinzu
                    else
                    {
                        klartext += i;
                    }
                }

                //Rückgabe
                return klartext;
            }
            #endregion

            //Methoden zum Knacken von Vigenère
            #region
            public static string KnackenMitLänge(string kryptotext, int schlüssellänge)
            {
                //deklariere und initialisiere die Variablen
                string[] arrAufgeteilt = new string[schlüssellänge]; //diese array wird später den aufgeteilten Krsptotext enthalten, und zwar so, dass alle Zeichen, die mit dem gleichen Buchstaben verschlüsselt wurden, in einer string stehen
                kryptotext = kryptotext.ToUpper();
                string alphabet = Strings.Alphabet;
                string schlüssel = "";//Hier wird später der Schlüssel abgespeichert werden

                //teilt den Kryptotext so auf, dass alle Buchstaben, die mit dem gleichen Buchstaben verschlüsselt wurden, in einer string stehen (in arrAufgeteilt)
                int counter = 0;
                foreach (char i in kryptotext)
                {
                    if (alphabet.Contains(i))
                    {
                        arrAufgeteilt[counter % schlüssellänge] += i;
                        counter++;
                    }
                }

                //zählt die Häufigkeiten in den aufgeteilten strings und finde somit den jeweiligen Schlüssel heraus, mit dem das jeweilige string verschlüsselt wurde
                foreach (string i in arrAufgeteilt)
                {
                    schlüssel += alphabet[(alphabet.IndexOf(Strings.HäufigsterBuchstabe(i)) + 22) % 26];
                }

                //Rückgabe; entschlüssele den Kryptotext, mit dem herausgefundenem Schlüssel
                Console.WriteLine("Der Schlüssel \"{0}\" wurde verwendet.", schlüssel);
                return Entschlüsseln(kryptotext, schlüssel);
            }

            public static int LängeKnacken(string kryptotext)
            {
                Console.WriteLine("Berechne...");

                //Teil 1: Versuche alle Zeichenfolgen herauszuschreiben (braucht wahrscheinlich am Längsten von den drei Teilen; wurde allerdings schon stark verbessert)
                #region
                //deklariere Listen
                List<string> list = new List<string>();//enthält alle strings, die Auftauchen
                List<int> listInt = new List<int>();//enthält die Anzahl, wie oft diese strings aus "list" auftauchen in "kryptotext"

                //verändere "kryptotext" (mache es groß, entferne Zeichenund mache es nur "TextLänge" Zeichen lang, sodass es schnell berechnet werden kann)
                kryptotext = Strings.EntferneZeichen(kryptotext.ToUpper());
                if (kryptotext.Length > TextLänge)
                {
                    kryptotext = kryptotext.Substring(0, TextLänge);
                }

                //schreibe alle Zeichenfolgen (Länge >= "MinZeichenfolgeLänge", Anzahl der Vorkommen >= "MindestesVorkommen"), die in "kryptotext" vorkommen in die Liste "list" und ihre Anzahl an Vorkommen in "listInt"
                for (int i = 0; i < kryptotext.Length; i++)
                {
                    for (int f = MinZeichenfolgeLänge; f < kryptotext.Length - i; f++)
                    {
                        //"i" gibt den Anfang der Zeichenfolge an, "f" das Ende; die Substring wird in "substr" gespeichert; counter zählt (im if-Ausdruck) wie oft die Zeichenfolge im Text zu finden ist
                        string substr = kryptotext.Substring(i, f);
                        int counter;

                        if ((!list.Contains(substr)) & (counter = Strings.ContainsHowOften(kryptotext, substr)) >= MindestesVorkommen)
                        {
                            list.Add(substr);
                            listInt.Add(counter);
                        }
                    }
                }

                //falls nichts gefunden wurde, gebe 0 zurück (schlüssellänge = 0?!)
                if (list.Count() == 0)
                {
                    return 0;
                }

                //schreibe alle gefundenen Zeichenfolgen mit ihrer Anzahl an Vorkommen heraus
                for (int i = 0; i < list.Count(); i++)
                {
                    Console.WriteLine(list[i] + "  -  " + listInt[i]);
                }
                #endregion

                //Teil 2: schreibe die Postionen der Items heraus und berechen dadurch die Distanzen zwischen ihnen (Vorsicht: Berechnungen laufen größtenteils in der Klasse "Strings")
                #region
                //deklariere Listen
                List<int> distanzen = new List<int>();//Liste enthält später die Distanzen der Items zueinander

                Console.WriteLine();

                //gehe die Liste durch und berechne die Distanzen der ITems untereinander zueinander
                for (int i = 0; i < list.Count(); i++)
                {
                    //schreibe das Item
                    Console.Write("\n" + list[i]);

                    //rechne (in der Klasse "Strings") die Distanzen des Items "i" in "kryptotext", gebe sie aus und speichere sie in der Liste "distanzen" ab
                    foreach (int f in Strings.Distanzen(kryptotext, list[i], listInt[i]))
                    {
                        //Ausgabe
                        Console.Write("   -   " + f);

                        //Abspeichern
                        distanzen.Add(f);
                    }
                }

                //Absatz
                Console.WriteLine();
                #endregion

                //Teil 3: prüfe nach einem Teiler, der viele Distanzen teilt und am Besten auch noch groß ist, sodass die Schlüssellänge herausgefunden werden kann
                #region
                //Initialisierung
                int[] teiler = new int[100];//enthält die Anzahl, wie oft der jeweilige Index + 1 eine der Distanzen teilt; Bsp: teiler[4] = 2 -> die Zahl 4 + 1 = 5 teilt 2 von den Distanzen
                int sum = 0;

                //prüfe auf Teilbarkeit
                for (int i = 2; i <= 100; i++)
                {
                    //probiert alle Zahlen n mit 2 <= n <= 100, ob sie die Distanzen teilen, speichere es ab
                    foreach (int f in distanzen)
                    {
                        if (f % i == 0)
                        {
                            teiler[i - 1]++;
                            //erhöhe "sum", weil sich auch ein Element in "teiler" erhöht
                            sum++;
                        }
                    }
                }

                //Zuweisung von Variablen
                int indexOfBiggestNumber = 0;
                int biggest = 0;

                //suche nach dem Index der größten Zahl in dem Array "Teiler"; dieser Index ist gleich der Teiler, der am Öftesten teilt (Teiler t > 2)
                for (int i = 0; i < teiler.Length; i++)
                {
                    if (teiler[i] + sum / 500 >= biggest)
                    {
                        biggest = teiler[i];
                        indexOfBiggestNumber = i;
                    }
                }
                #endregion

                //Rückgabe (+1, weil eine array nullbasiert ist)
                return indexOfBiggestNumber + 1;
            }

            public static string Knacken(string kryptotext)
            {
                //Kombiniere die beiden Methoden "LängeKnacken" und "KnackenMitLänge"
                return KnackenMitLänge(kryptotext, LängeKnacken(kryptotext));
            }
            #endregion
        }
    }
}