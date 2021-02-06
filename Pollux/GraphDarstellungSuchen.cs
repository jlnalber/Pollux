namespace Pollux.Graph
{
    public partial class GraphDarstellung
    {
        //Methoden zum Suchen von Kanten und Knoten in einem Graphen

        public new Knoten SucheKnoten(string name)
        {
            //suche nach dem Knoten mit dem Namen
            Knoten knoten = null;

            //Gucke, ob ein Knoten diesen Namen hat, speichere es in "knoten" ab
            foreach (Knoten i in this.GraphKnoten)
            {
                if (i.Name == name)
                {
                    knoten = i;
                }
            }

            //Rückgabe
            return knoten;
        }

        public new Kanten SucheKanten(string name)
        {
            //suche nach dem Kanten mit dem Namen
            Kanten kante = null;

            //Gucke, ob eine Kanten diesen Namen hat, speichere es in "kante" ab
            foreach (Kanten i in this.GraphKanten)
            {
                if (i.Name == name)
                {
                    kante = i;
                }
            }

            //Rückgabe
            return kante;
        }

        public override bool ContainsKnoten(string name)
        {
            //durchsuche die Liste "GraphKnoten", ob schon ein Knoten mit diesem Namen existiert
            foreach (Knoten i in this.GraphKnoten)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKnoten(Knoten knoten)
        {
            return this.GraphKnoten.Contains(knoten);
        }

        public override bool ContainsKanten(string name)
        {
            //durchsuche die Liste "GraphKanten", ob schon eine Kante mit diesem Namen existiert
            foreach (Kanten i in this.GraphKanten)
            {
                if (i.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsKanten(Kanten kante)
        {
            return this.GraphKanten.Contains(kante);
        }
    }
}
