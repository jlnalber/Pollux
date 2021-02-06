﻿using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Methoden zum hinzufügen, entfernen von Knoten/Kanten
        //Methoden zum Hinzufügen von Knoten
        #region
        public virtual Graph.Knoten AddKnoten(Knoten knote)
        {
            knote.Name = knote.Name.ToUpper();
            this.GraphKnoten.Add(knote);
            knote.Parent = this;

            int[,] copy = new int[this.Liste.GetLength(0) + 1, this.Liste.GetLength(1) + 1];
            for (int i = 0; i < this.Liste.GetLength(0); i++)
            {
                for (int f = 0; f < this.Liste.GetLength(1); f++)
                {
                    copy[i, f] = this.Liste[i, f];
                }
            }
            this.Liste = copy;

            return knote;
        }

        public virtual Graph.Knoten AddKnoten()
        {
            return this.AddKnoten("KNOTEN" + (this.GraphKnoten.Count + 1).ToString());
        }

        public virtual Graph.Knoten AddKnoten(string name)
        {
            Knoten knote = new Knoten(this, new List<Kanten>(), name.ToUpper());
            return this.AddKnoten(knote);
        }
        #endregion

        //Methode zum Hinzufügen von Kanten
        #region
        public virtual Graph.Kanten AddKante(Kanten kante, Knoten start, Knoten ende)
        {
            kante.Name = kante.Name.ToUpper();
            kante.Parent = this;
            this.GraphKanten.Add(kante);
            kante.Knoten[0] = start;
            kante.Knoten[1] = ende;
            start.Kanten.Add(kante);
            ende.Kanten.Add(kante);
            this.Liste[this.GraphKnoten.IndexOf(start), this.GraphKnoten.IndexOf(ende)]++;
            this.Liste[this.GraphKnoten.IndexOf(ende), this.GraphKnoten.IndexOf(start)]++;

            return kante;
        }

        public virtual Graph.Kanten AddKante(string name, Knoten start, Knoten ende)
        {
            Kanten kante = new Kanten(this, new Knoten[2], name.ToUpper());
            return this.AddKante(kante, start, ende);
        }

        public virtual Graph.Kanten AddKante(Knoten start, Knoten ende)
        {
            return this.AddKante("KANTE" + (this.GraphKanten.Count + 1).ToString(), start, ende);
        }
        #endregion

        //Methoden zum Entfernen von Knoten und Kanten
        #region
        public virtual Graph.Knoten RemoveKnoten(Graph.Knoten knoten)
        {
            //Entferne alle zugehörigen Kanten aus dem Graphen
            while (knoten.Kanten.Count != 0)
            {
                this.RemoveKante(knoten.Kanten[0]);
            }
            knoten.Parent = null;
            knoten.Kanten.Clear();

            //Bearbeite die Liste "List"
            int position = this.GraphKnoten.IndexOf(knoten);
            int[,] copy = new int[this.Liste.GetLength(0) - 1, this.Liste.GetLength(1) - 1];
            for (int i = 0; i < this.Liste.GetLength(0); i++)
            {
                for (int f = 0; f < this.Liste.GetLength(1); f++)
                {
                    if (i != position && f != position)
                    {
                        copy[(i < position) ? i : i - 1, (f < position) ? f : f - 1] = this.Liste[i, f];
                    }
                }
            }
            this.Liste = copy;

            //Entferne den Knoten "knoten" aus der Liste "GraphKnoten"
            this.GraphKnoten.Remove(knoten);

            //Rückgabe
            return knoten;
        }

        public virtual Graph.Kanten RemoveKante(Graph.Kanten kante)
        {
            //Entferne den Graph aus der Kante
            kante.Parent = null;

            //Bearbeite die Liste "Liste"
            this.Liste[this.GraphKnoten.IndexOf(kante.Knoten[0]), this.GraphKnoten.IndexOf(kante.Knoten[1])]--;
            this.Liste[this.GraphKnoten.IndexOf(kante.Knoten[1]), this.GraphKnoten.IndexOf(kante.Knoten[0])]--;

            //Entferne die Kante aus ihren Knoten
            kante.Knoten[0].Kanten.Remove(kante);
            kante.Knoten[1].Kanten.Remove(kante);

            //Entferne die Kante aus der Liste "GraphKanten" im Graphen
            this.GraphKanten.Remove(kante);

            //Rückgabe
            return kante;
        }
        #endregion
    }
}
