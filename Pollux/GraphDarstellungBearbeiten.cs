using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class GraphDarstellung
    {
        //Methoden zum hinzufügen, entfernen von Knoten/Kanten
        //Methoden zum Hinzufügen von Knoten
        #region
        public Knoten AddKnoten(Knoten knote)
        {
            knote.Name = knote.Name.ToUpper();
            this.GraphKnoten.Add((Knoten)knote);
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

            return (Knoten)knote;
        }

        public new Knoten AddKnoten()
        {
            return this.AddKnoten("KNOTEN" + (this.GraphKnoten.Count + 1).ToString());
        }

        public new Knoten AddKnoten(string name)
        {
            return this.AddKnoten(new Knoten(this, new List<Kanten>(), name.ToUpper(), this.Canvas));
        }
        #endregion

        //Methode zum Hinzufügen von Kanten
        #region
        public Kanten AddKante(Kanten kante, Knoten start, Knoten ende)
        {
            kante.Name = kante.Name.ToUpper();
            kante.Parent = this;
            this.GraphKanten.Add(kante);
            kante.Knoten[0] = start;
            kante.Knoten[1] = ende;
            start.Kanten.Add(kante);
            ende.Kanten.Add(kante);
            this.Liste[this.GraphKnoten.IndexOf((Knoten)start), this.GraphKnoten.IndexOf((Knoten)ende)]++;
            this.Liste[this.GraphKnoten.IndexOf((Knoten)ende), this.GraphKnoten.IndexOf((Knoten)start)]++;

            return kante;
        }

        public Kanten AddKante(string name, Knoten start, Knoten ende)
        {
            return this.AddKante(new Kanten(this, new Knoten[2] { start, ende }, name.ToUpper(), this.Canvas), start, ende);
        }

        public Kanten AddKante(Knoten start, Knoten ende)
        {
            return this.AddKante("KANTE" + (this.GraphKanten.Count + 1).ToString(), start, ende);
        }
        #endregion

        //Methoden zum Entfernen von Knoten und Kanten
        #region
        public Knoten RemoveKnoten(Knoten knoten)
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

            //Entferne den Knoten "knoten" aus dem Canvas "this.Canvas"
            knoten.Disappear();

            //Rückgabe
            return knoten;
        }

        public Kanten RemoveKante(Kanten kante)
        {
            //Entferne den Graph aus der Kante
            kante.Parent = null;

            //Bearbeite die Liste "Liste"
            this.Liste[this.GraphKnoten.IndexOf((Knoten)kante.Knoten[0]), this.GraphKnoten.IndexOf((Knoten)kante.Knoten[1])]--;
            this.Liste[this.GraphKnoten.IndexOf((Knoten)kante.Knoten[1]), this.GraphKnoten.IndexOf((Knoten)kante.Knoten[0])]--;

            //Entferne die Kante aus ihren Knoten
            kante.Knoten[0].Kanten.Remove(kante);
            kante.Knoten[1].Kanten.Remove(kante);

            //Entferne die Kante aus der Liste "GraphKanten" im Graphen
            this.GraphKanten.Remove((Kanten)kante);

            //Entferne die Kante "kante" aus dem Canvas "this.Canvas"
            kante.Disappear();

            //Rückgabe
            return (Kanten)kante;
        }
        #endregion
    }
}
