using System.Linq;

namespace Pollux
{
    public partial class GraphDarstellung
    {
        //Methoden zum Suchen von Kanten und Knoten in einem Graphen

        public new Knoten SucheKnoten(string name)
        {
            for (int i = this.GraphKnoten.Count() - 1; i >= 0; --i)
            {
                if (this.GraphKnoten[i].Name == name)
                {
                    return this.GraphKnoten[i];
                }
            }

            //Rückgabe
            return null;
        }

        public new Kanten SucheKanten(string name)
        {
            for (int i = this.GraphKanten.Count() - 1; i >= 0; --i)
            {
                if (this.GraphKanten[i].Name == name)
                {
                    return this.GraphKanten[i];
                }
            }

            //Rückgabe
            return null;
        }

        public override bool ContainsKnoten(string name)
        {
            //durchsuche die Liste "GraphKnoten", ob schon ein Knoten mit diesem Namen existiert
            for (int i = this.GraphKnoten.Count() - 1; i >= 0; --i)
            {
                if (this.GraphKnoten[i].Name == name)
                {
                    return true;
                }
            }

            //Rückgabe
            return false;
        }

        public bool ContainsKnoten(Knoten knoten)
        {
            return this.GraphKnoten.Contains(knoten);
        }

        public override bool ContainsKanten(string name)
        {
            //durchsuche die Liste "GraphKanten", ob schon eine Kante mit diesem Namen existiert
            for (int i = this.GraphKanten.Count() - 1; i >= 0; --i)
            {
                if (this.GraphKanten[i].Name == name)
                {
                    return true;
                }
            }

            //Rückgabe
            return false;
        }

        public bool ContainsKanten(Kanten kante)
        {
            return this.GraphKanten.Contains(kante);
        }
    }
}
