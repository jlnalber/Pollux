using System.Collections.Generic;

namespace Thestias
{
    public partial class Graph
    {
        public static class GraphTemplates
        {
            //Methoden, die Vorlagen erstellen

            // Methode, die ein vollständiges Vieleck mit n-Ecken zurückgibt
            public static Graph VollständigesVieleck(int ecken)
            {
                //erstelle Graph
                Graph graph = new Graph();

                //erstelle die Ecken
                for (int i = 0; i < ecken; ++i)
                {
                    graph.AddVertex();
                }

                //erstelle die Edge
                for (int i = 0; i < ecken - 1; ++i)
                {
                    for (int f = i + 1; f < ecken; ++f)
                    {
                        graph.AddEdge(graph.Vertices[i], graph.Vertices[f]);
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

                //erstelle "eckenErsteMenge"-viele Vertex
                List<Graph.Vertex> knoten1 = new List<Vertex>();
                for (int i = 0; i < eckenErsteMenge; ++i)
                {
                    knoten1.Add(graph.AddVertex());
                }

                //erstelle "eckenZweiteMenge"-viele Vertex
                List<Graph.Vertex> knoten2 = new List<Vertex>();
                for (int i = 0; i < eckenZweiteMenge; ++i)
                {
                    knoten2.Add(graph.AddVertex());
                }

                //mache eine Kante zwischen jedem Vertex der Menge "knoten1" und "knoten2"
                foreach (Graph.Vertex i in knoten1)
                {
                    foreach (Graph.Vertex f in knoten2)
                    {
                        graph.AddEdge(i, f);
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
                graph.AddVertex();
                for (int i = 1; i < länge; ++i)
                {
                    graph.AddVertex();
                    graph.AddEdge(graph.Vertices[i - 1], graph.Vertices[i]);
                }
                graph.AddEdge(graph.Vertices[länge - 1], graph.Vertices[0]);

                //Rückgabe
                return graph;
            }

            //Methode, die ein Vieleck mit n-Ecken zurück gibt
            public static Graph Vieleck(int ecken)
            {
                //Erstelle einen Graphen
                Graph graph = new Graph();

                //Füge dem Graphen "Ecken"-viele Vertex hinzu
                for (int i = 0; i < ecken; ++i)
                {
                    graph.AddVertex();
                }

                //Rückgabe
                return graph;
            }

            //Methode, die einen Baum zurück gibt
            public static Graph Baum(int stufen, int verzweigungen)
            {
                Graph graph = new();
                List<Graph.Vertex> lastStep = new() { graph.AddVertex() };
                for (int i = 1; i < stufen; ++i)
                {
                    List<Graph.Vertex> newStep = new();
                    foreach (Graph.Vertex knoten in lastStep)
                    {
                        for (int f = 0; f < verzweigungen; ++f)
                        {
                            Vertex knoten1 = graph.AddVertex();
                            newStep.Add(knoten1);
                            graph.AddEdge(knoten, knoten1);
                        }
                    }
                    lastStep.Clear();
                    foreach (Graph.Vertex f in newStep)
                    {
                        lastStep.Add(f);
                    }
                }
                return graph;
            }
        }
    }
}
