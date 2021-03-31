﻿using System.Collections.Generic;
using System.Linq;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Eigenschaften des Graphs

        public virtual bool IstEulersch
        {
            get
            {
                //Prüfung, ob er eulersch ist

                //erstelle eine Kopie von diesem Graphen
                Graph copy = this.Kopie();
                for (int i = 0; i < copy.GraphKnoten.Count; ++i)
                {
                    if (copy.GraphKnoten[i].IstIsolierteEcke)
                    {
                        copy.RemoveKnoten(copy.GraphKnoten[i]);
                        i--;
                    }
                }

                if (!copy.IstZusammenhängend)
                {
                    return false;
                }

                foreach (Graph.Knoten i in copy.GraphKnoten)
                {
                    if (i.Grad % 2 != 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public virtual bool IstHamiltonsch//muss noch überarbeitet werden
        {
            get
            {
                //Prüfe, ob der Graph hamiltonsch ist

                return false;
            }
        }

        public virtual int Taillenweite//muss noch überarbeitet werden
        {
            get
            {
                //Prüfe den Wert der Taillenweite

                return 0;
            }
        }

        public virtual int Durchmesser//muss noch überarbeitet werden
        {
            get
            {
                //Überprüfe den Wert des Durchmessers

                return 0;
            }
        }

        public virtual int? Flächen
        {
            get
            {
                //Überprüfe, aus wievielen Flächen der Graph bestehen muss

                if (this.Planar)
                {
                    return -this.AnzahlKnoten + this.AnzahlKanten + 2;
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual bool Planar
        {
            get
            {
                //Überprüfe, ob der Graph planar ist

                //Erstelle zwei Graph, die später zur Überprüfung genutzt werden
                Graph vollständigesFünfeck = GraphTemplates.VollständigesVieleck(5);
                Graph bipartiter33Graph = GraphTemplates.VollständigerBipartiterGraph(3, 3);

                //Prüfe, ob dieser Graph ein Teilgraph oder eine Unterteilung von den beiden Grpahen "bipartiter33Graph" oder "vollständigesFünfeck" ist
                return vollständigesFünfeck.IstTeilgraphVon(this) || vollständigesFünfeck.IstUnterteilungVon(this) || bipartiter33Graph.IstTeilgraphVon(this) || bipartiter33Graph.IstUnterteilungVon(this);
            }
        }

        public virtual bool IstBipartit
        {
            get
            {
                //Prüfe, ob der Graph bipartit ist


                //nur hier gebraucht
                bool AddNachbarnZuAndererFarbe(Graph.Knoten knoten, List<Graph.Knoten> meineFarbe, List<Graph.Knoten> andereFarbe)
                {
                    if (!meineFarbe.Contains(knoten))
                    {
                        meineFarbe.Add(knoten);
                    }

                    //gehe die Nachbarn des Knotens durch und füge diese, falls noch nicht getan, zur anderen Farbe (Liste) hinzu, gehe dann auch dessen Nachbarn durch (recursive method)
                    foreach (Graph.Knoten i in knoten.BenachbarteKnoten)
                    {
                        //falls diese Farbe den Knoten "i" schon enthält, so gebe falsch zurück
                        if (meineFarbe.Contains(i))
                        {
                            return false;
                        }

                        //versuche alle Nachbarn des Knotens "i" zur anderen Farbe hinzuzufügen, falls das nicht geht, gebe "false" zurück
                        if (!andereFarbe.Contains(i))
                        {
                            andereFarbe.Add(i);
                            if (!AddNachbarnZuAndererFarbe(i, andereFarbe, meineFarbe))
                            {
                                return false;
                            }
                        }
                    }

                    //gebe, wenn alles funktioniert hat "true" zurück
                    return true;
                }//Methode (recursive method), die alle Nachbarn eines Knoten in die andere Farbe (Liste) schreibet, dessen Nachbarn wieder usw.



                List<Graph.Knoten> rot = new List<Knoten>();
                List<Graph.Knoten> blau = new List<Knoten>();
                foreach (Graph.Knoten knoten in this.GraphKnoten)
                {
                    if (!rot.Contains(knoten) && !blau.Contains(knoten))
                    {
                        if (!AddNachbarnZuAndererFarbe(knoten, rot, blau))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public virtual bool IstBaum
        {
            get
            {
                //Prüfen, ob der Graph ein Baum ist

                return this.IstZusammenhängend && this.AnzahlKnoten - 1 == this.AnzahlKanten;
            }
        }

        public virtual bool IstZusammenhängend
        {
            get
            {
                //Prüfen, ob der Graph zusammmenhängend ist
                return this.AnzahlKomponenten == 1;
            }
        }

        public virtual int AnzahlKomponenten
        {
            get
            {
                //Prüfen aus wievielen Komponenten der Graph besteht

                return this.Komponenten.Count;
            }
        }

        public virtual List<List<Graph.Knoten>> Komponenten
        {
            get
            {
                //Prüfen, aus welchen Komponenten der Graph besteht

                //nur hier gebraucht
                void AddNachbarnZumKomponent(Graph.Knoten knoten, List<Graph.Knoten> knotenListe)
                {
                    //gehe die Nachbarn des Knotens durch und füge diese, falls noch nicht getan, zum Komponent hinzu, gehe dann auch dessen Nachbarn durch (recursive method)
                    foreach (Graph.Knoten i in knoten.BenachbarteKnoten)
                    {
                        if (!knotenListe.Contains(i))
                        {
                            knotenListe.Add(i);
                            AddNachbarnZumKomponent(i, knotenListe);
                        }
                    }
                }//Methode (recursive method), die alle Nachbarn eines Knoten in seinen Komponenten schreiben, dessen Nachbarn wieder usw.


                List<List<Graph.Knoten>> liste = new List<List<Knoten>>();//Liste mit den Komponenten

                foreach (Graph.Knoten i in this.GraphKnoten)
                {
                    //gucke, ob dieser Knoten "i" schon in einem Komponent drin ist, wenn nicht erstelle einen neuen Komponenten
                    bool schonVorhanden = false;
                    foreach (List<Graph.Knoten> n in liste)
                    {
                        if (n.Contains(i))
                        {
                            schonVorhanden = true;
                        }
                    }

                    //falls der Knoten schon in einem Komponenten auftaucht, erstelle einen neuen Komponenten und füge dort alle zusammengehörenden Knoten hinzu
                    if (!schonVorhanden)
                    {
                        //erstelle neuen Komponent, füge ihn hinzu
                        List<Graph.Knoten> komponent = new List<Knoten>() { i };
                        liste.Add(komponent);

                        //schreibe seine benachbarten Ecken in den gleichen Komponenten, und dessen benachbarten Ecken auch, usw.... (recursive method)
                        AddNachbarnZumKomponent(i, komponent);
                    }
                }

                return liste;
            }
        }

        public virtual int Schlingen
        {
            get
            {
                //Prüfen, wieviele Schlingen der Graph enthält

                int schlingen = 0;

                for (int i = 0; i < this.GraphKnoten.Count; ++i)
                {
                    schlingen += this.Liste[i, i] / 2;
                }

                return schlingen;
            }
        }

        public virtual int ParalleleKanten
        {
            get
            {
                //Prüfen, wieviele parallele Kanten der Graph enthält

                int paralleleKanten = 0;

                //gehe die Liste durch, dabei aber nur die Hälfte, da sich der Rest ja doppelt
                for (int i = 0; i < this.Liste.GetLength(0); ++i)
                {
                    for (int f = 0; f <= i; ++f)
                    {
                        if (i == f)
                        {
                            if (this.Liste[i, f] / 2 >= 2)
                            {
                                paralleleKanten += this.Liste[i, f] / 2;
                            }
                        }
                        else if (this.Liste[i, f] >= 2)
                        {
                            paralleleKanten += this.Liste[i, f];
                        }
                    }
                }

                //Rückgabe
                return paralleleKanten;
            }
        }

        public virtual bool EinfacherGraph
        {
            get
            {
                //Prüfen, ob der Graph einfach ist
                return this.Schlingen == 0 && this.ParalleleKanten == 0;
            }
        }

        public virtual int AnzahlKanten
        {
            get
            {
                //gebe die Länge der Liste mit den Kanten zurück
                return this.GraphKanten.Count();
            }
        }

        public virtual int AnzahlKnoten
        {
            get
            {
                //gebe die Länge der Liste mit den Knoten zurück
                return this.GraphKnoten.Count();
            }
        }
    }
}
