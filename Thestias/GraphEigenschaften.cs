using System;
using System.Collections.Generic;
using System.Linq;

namespace Thestias
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
                for (int i = 0; i < copy.Vertices.Count; ++i)
                {
                    if (copy.Vertices[i].IsIsolated)
                    {
                        copy.RemoveVertex(copy.Vertices[i]);
                        i--;
                    }
                }

                if (!copy.IstZusammenhängend)
                {
                    return false;
                }

                foreach (Graph.Vertex i in copy.Vertices)
                {
                    if (i.Degree % 2 != 0)
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
                throw new NotImplementedException();
                return false;
            }
        }

        public virtual int Taillenweite//muss noch überarbeitet werden
        {
            get
            {
                //Prüfe den Wert der Taillenweite
                throw new NotImplementedException();
                return 0;
            }
        }

        public virtual int Durchmesser//muss noch überarbeitet werden
        {
            get
            {
                //Überprüfe den Wert des Durchmessers
                throw new NotImplementedException();
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
                bool AddNachbarnZuAndererFarbe(Graph.Vertex knoten, HashSet<Graph.Vertex> meineFarbe, HashSet<Graph.Vertex> andereFarbe)
                {
                    if (!meineFarbe.Contains(knoten))
                    {
                        meineFarbe.Add(knoten);
                    }

                    //gehe die Nachbarn des Knotens durch und füge diese, falls noch nicht getan, zur anderen Farbe (Liste) hinzu, gehe dann auch dessen Nachbarn durch (recursive method)
                    foreach (Graph.Vertex i in knoten.AdjacentVertices)
                    {
                        //falls diese Farbe den Vertex "i" schon enthält, so gebe falsch zurück
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
                }//Methode (recursive method), die alle Nachbarn eines Vertex in die andere Farbe (Liste) schreibet, dessen Nachbarn wieder usw.

                HashSet<Graph.Vertex> rot = new HashSet<Vertex>();
                HashSet<Graph.Vertex> blau = new HashSet<Vertex>();
                foreach (Graph.Vertex knoten in this.Vertices)
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

        public virtual HashSet<HashSet<Graph.Vertex>> Komponenten
        {
            get
            {
                //Prüfen, aus welchen Komponenten der Graph besteht

                //nur hier gebraucht
                void AddNachbarnZumKomponent(Graph.Vertex knoten, HashSet<Graph.Vertex> knotenListe)
                {
                    //gehe die Nachbarn des Knotens durch und füge diese, falls noch nicht getan, zum Komponent hinzu, gehe dann auch dessen Nachbarn durch (recursive method)
                    foreach (Graph.Vertex i in knoten.AdjacentVertices)
                    {
                        if (0 == (from n in knotenListe where (n.Name == i.Name) select n).Count())
                        {
                            knotenListe.Add((Graph.Vertex)i);
                            AddNachbarnZumKomponent((Graph.Vertex)i, knotenListe);
                        }
                    }
                }//Methode (recursive method), die alle Nachbarn eines Vertex in seinen Komponenten schreiben, dessen Nachbarn wieder usw.

                bool SchonVorhanden(HashSet<HashSet<Graph.Vertex>> liste, Graph.Vertex knoten)
                {
                    //gucke, ob dieser Vertex "knoten" schon in einem Komponent drin ist
                    foreach (HashSet<Graph.Vertex> n in liste)
                    {
                        if (0 != (from f in n where (f.Name == knoten.Name) select f).Count())
                        {
                            return true;
                        }
                    }

                    return false;
                }


                HashSet<HashSet<Graph.Vertex>> liste = new HashSet<HashSet<Vertex>>();//Liste mit den Komponenten

                foreach (Graph.Vertex i in this.Vertices)
                {
                    //falls der Vertex schon in einem Komponenten auftaucht, erstelle einen neuen Komponenten und füge dort alle zusammengehörenden Vertex hinzu
                    if (!SchonVorhanden(liste, (Graph.Vertex)i))
                    {
                        //erstelle neuen Komponent, füge ihn hinzu
                        HashSet<Graph.Vertex> komponent = new HashSet<Vertex>() { (Graph.Vertex)i };
                        liste.Add(komponent);

                        //schreibe seine benachbarten Ecken in den gleichen Komponenten, und dessen benachbarten Ecken auch, usw.... (recursive method)
                        AddNachbarnZumKomponent((Graph.Vertex)i, komponent);
                    }
                }

                //Rückgabe
                return liste;
            }
        }

        public virtual int Schlingen
        {
            get
            {
                //Prüfen, wieviele Schlingen der Graph enthält

                int schlingen = 0;

                for (int i = 0; i < this.Vertices.Count; ++i)
                {
                    schlingen += this.List[i, i] / 2;
                }

                return schlingen;
            }
        }

        public virtual int ParalleleKanten
        {
            get
            {
                //Prüfen, wieviele parallele Edge der Graph enthält

                int paralleleKanten = 0;

                //gehe die Liste durch, dabei aber nur die Hälfte, da sich der Rest ja doppelt
                for (int i = 0; i < this.List.GetLength(0); ++i)
                {
                    for (int f = 0; f <= i; ++f)
                    {
                        if (i == f)
                        {
                            if (this.List[i, f] / 2 >= 2)
                            {
                                paralleleKanten += this.List[i, f] / 2;
                            }
                        }
                        else if (this.List[i, f] >= 2)
                        {
                            paralleleKanten += this.List[i, f];
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
                //gebe die Länge der Liste mit den Edge zurück

                return this.Edges.Count();
            }
        }

        public virtual int AnzahlKnoten
        {
            get
            {
                //gebe die Länge der Liste mit den Vertex zurück

                return this.Vertices.Count();
            }
        }
    }
}
