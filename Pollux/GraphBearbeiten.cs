﻿using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class Graph
    {
        //Methoden zum hinzufügen, entfernen von Knoten/Kanten
        //Methoden zum Hinzufügen von Knoten
        #region
        public Graph.Knoten AddKnoten(Knoten knote)
        {
            knote.Name = knote.Name.ToUpper();
            GraphKnoten.Add(knote);
            knote.Parent = this;

            int[,] copy = new int[Liste.GetLength(0) + 1, Liste.GetLength(1) + 1];
            for (int i = 0; i < Liste.GetLength(0); i++)
            {
                for (int f = 0; f < Liste.GetLength(1); f++)
                {
                    copy[i, f] = Liste[i, f];
                }
            }
            this.Liste = copy;

            return knote;
        }

        public Graph.Knoten AddKnoten()
        {
            return this.AddKnoten("KNOTEN" + (GraphKnoten.Count + 1).ToString());
        }

        public Graph.Knoten AddKnoten(string name)
        {
            Knoten knote = new Knoten(this, new List<Kanten>(), name.ToUpper());
            return this.AddKnoten(knote);
        }
        #endregion

        //Methode zum Hinzufügen von Kanten
        #region
        public Graph.Kanten AddKante(Kanten kante, Knoten start, Knoten ende)
        {
            kante.Name = kante.Name.ToUpper();
            kante.Parent = this;
            GraphKanten.Add(kante);
            kante.Knoten[0] = start;
            kante.Knoten[1] = ende;
            start.Kanten.Add(kante);
            ende.Kanten.Add(kante);
            this.Liste[GraphKnoten.IndexOf(start), GraphKnoten.IndexOf(ende)]++;
            this.Liste[GraphKnoten.IndexOf(ende), GraphKnoten.IndexOf(start)]++;

            return kante;
        }

        public Graph.Kanten AddKante(string name, Knoten start, Knoten ende)
        {
            Kanten kante = new Kanten(this, new Knoten[2], name.ToUpper());
            return this.AddKante(kante, start, ende);
        }

        public Graph.Kanten AddKante(Knoten start, Knoten ende)
        {
            return this.AddKante("KANTE" + (GraphKanten.Count + 1).ToString(), start, ende);
        }
        #endregion

        //Methoden zum Entfernen von Knoten und Kanten
        #region
        public Graph.Knoten RemoveKnoten(Graph.Knoten knoten)
        {
            while (knoten.Kanten.Count != 0)
            {
                RemoveKante(knoten.Kanten[0]);
            }
            knoten.Parent = null;
            knoten.Kanten.Clear();

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

            this.GraphKnoten.Remove(knoten);

            return knoten;
        }

        public Graph.Kanten RemoveKante(Graph.Kanten kante)
        {
            kante.Parent = null;
            this.Liste[GraphKnoten.IndexOf(kante.Knoten[0]), GraphKnoten.IndexOf(kante.Knoten[1])]--;
            this.Liste[GraphKnoten.IndexOf(kante.Knoten[1]), GraphKnoten.IndexOf(kante.Knoten[0])]--;
            kante.Knoten[0].Kanten.Remove(kante);
            kante.Knoten[1].Kanten.Remove(kante);
            kante.Knoten = null;
            this.GraphKanten.Remove(kante);

            return kante;
        }
        #endregion
    }
}
