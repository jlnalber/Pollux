using System.Collections.Generic;

namespace Pollux.Graph
{
    public partial class Graph
    {
        public static class GraphTemplates
        {
            // Methode, die ein vollständiges Vieleck mit n-Ecken zurückgibt
            public static Graph VollständigesVieleck(int ecken)
            {
                //erstelle Graph
                Graph graph = new Graph();

                //erstelle die Ecken
                for (int i = 0; i < ecken; i++)
                {
                    graph.AddKnoten();
                }

                //erstelle die Kanten
                for (int i = 0; i < ecken - 1; i++)
                {
                    for (int f = i + 1; f < ecken; f++)
                    {
                        graph.AddKante(graph.GraphKnoten[i], graph.GraphKnoten[f]);
                    }
                }

                //Rückgabe
                return graph;
            }

            // Methode, die einen bipartiten n-n-Graph erstellt
            public static Graph VollständigerBipartiterGraph(int eckenErsteMenge, int eckenZweiteMenge)
            {
                //erstelle einen neuen Graphen
                Graph graph = new Graph();

                //erstelle "eckenErsteMenge"-viele Knoten
                List<Graph.Knoten> knoten1 = new List<Knoten>();
                for (int i = 0; i < eckenErsteMenge; i++)
                {
                    knoten1.Add(graph.AddKnoten());
                }

                //erstelle "eckenZweiteMenge"-viele Knoten
                List<Graph.Knoten> knoten2 = new List<Knoten>();
                for (int i = 0; i < eckenZweiteMenge; i++)
                {
                    knoten2.Add(graph.AddKnoten());
                }

                //mache eine Kante zwischen jedem Knoten der Menge "knoten1" und "knoten2"
                foreach (Graph.Knoten i in knoten1)
                {
                    foreach (Graph.Knoten f in knoten2)
                    {
                        graph.AddKante(i, f);
                    }
                }

                //Rückgabe
                return graph;
            }

            //Methode, die einen Kreis erstellt
            public static Graph Kreis(int länge)
            {
                //Erstelle einen neuen Graphen
                Graph graph = new Graph();

                //erstelle den Kreis, indem immer eine neue Ecke hinzugefügt wird und dann die Kante zur vorherigen Ecke
                graph.AddKnoten();
                for (int i = 1; i < länge; i++)
                {
                    graph.AddKnoten();
                    graph.AddKante(graph.GraphKnoten[i - 1], graph.GraphKnoten[i]);
                }
                graph.AddKante(graph.GraphKnoten[länge - 1], graph.GraphKnoten[0]);

                //Rückgabe
                return graph;
            }

            //Methode, die ein Vieleck mit n-Ecken zurück gibt
            public static Graph Vieleck(int Ecken)
            {
                //Erstelle einen Graphen
                Graph graph = new Graph();

                //Füge dem Graphen "Ecken"-viele Knoten hinzu
                for (int i = 0; i < Ecken; i++)
                {
                    graph.AddKnoten();
                }

                //Rückgabe
                return graph;
            }

            //Methode, die einen Baum zurück gibt
            public static Graph Baum(int Stufen, int Verzweigungen)
            {
                Graph graph = new();
                List<Graph.Knoten> lastStep = new() { graph.AddKnoten() };
                for (int i = 1; i < Stufen; i++)
                {
                    List<Graph.Knoten> newStep = new();
                    foreach (Graph.Knoten knoten in lastStep)
                    {
                        for (int f = 0; f < Verzweigungen; f++)
                        {
                            Knoten knoten1 = graph.AddKnoten();
                            newStep.Add(knoten1);
                            graph.AddKante(knoten, knoten1);
                        }
                    }
                    lastStep.Clear();
                    foreach (Graph.Knoten f in newStep)
                    {
                        lastStep.Add(f);
                    }
                }
                return graph;
            }
        }
    }
}
