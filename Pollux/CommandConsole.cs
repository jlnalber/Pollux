using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace Pollux
{
    public class CommandConsole
    {
        //Members
        #region
        public Graph.Graph usingGraph;
        public GraphDarstellung usingGraphDarstellung;
        private TextBox output;
        public string user = "User";
        public string lastCommand;
        public string path;
        public MainWindow MainWindow;
        public TabItem TabItem;
        #endregion

        //Konstruktor
        public CommandConsole(Graph.Graph usingGraph, GraphDarstellung usingGraphDarstellung, TextBox output, string path, MainWindow main, TabItem tabItem)
        {
            this.usingGraph = usingGraph;
            this.usingGraphDarstellung = usingGraphDarstellung;
            this.output = output;
            this.path = path;
            this.MainWindow = main;
            this.TabItem = tabItem;
        }

        public void Command(string command)
        {
            bool changed = false;

            //verarbeite die Eingabe
            #region
            //setze den letzten Command
            this.lastCommand = command;

            //Verändere den Befehle, sodass er Sinn ergibt
            while (command.Contains("  "))
            {
                command = command.Replace("  ", " ");
            }
            command = (command[command.Length - 1] == ' ') ? command.Remove(command.Length - 1).ToUpper() : command.ToUpper(); //entferne Leerzeichen am Ende und mach es groß

            //Zeige den Command im Output-Fenster
            this.output.Text += "[" + DateTime.Now.ToString() + "] " + this.user + ": " + command + "\n";

            //splitte den command auf, sodass er gleich verarbeitet werden kann
            string[] command_splitted = command.Split(' ');
            #endregion

            //Gucke mit welchem Schlüsselwort eingeleitet wurde, handle dann entsprechend
            #region
            //das Schlüsselwort "SHOW" stellt den Graph dar
            if (command_splitted[0] == "SHOW")
            {
                //falls der Graph leer ist, schreibe das aus, ansonsten öffne Eigenschaften-Fenster "Show"
                if (this.usingGraph.GraphKnoten.Count == 0)
                {
                    WriteLine("Graph \"" + this.usingGraph.Name + "\" is empty!");
                }
                else
                {
                    try
                    {
                        //öffne ein neues EIgenschaften-Fenster "Show"
                        Show show = new Show(this.usingGraphDarstellung, this);
                        show.Show();
                        WriteLine("Task complete!");
                    }
                    catch
                    {
                        //Schreibe die Error-Nachricht
                        WriteLine("Seems like something went wrong!");
                    }
                }
            }

            //das Schlüsselwort "ADD" fügt eine Kante/einen Knoten zum Graphen hinzu
            else if (command_splitted[0] == "ADD")
            {
                if (command_splitted.Length == 1)
                {
                    //Error-Nachricht, für den Fall, dass nicht die richtige Anzahl an Argumenten gebracht wurde
                    WriteLine("ERROR: No argument given, where at least one is needed: " + command);
                }
                else if (command_splitted.Length == 2)
                {
                    //Ausgabe
                    WriteLine("Adding node \"" + command_splitted[1] + "\"...");

                    //Füge den Knoten hinzu
                    Graph.Graph.Knoten knoten = new Graph.Graph.Knoten(this.usingGraph, new List<Graph.Graph.Kanten>(), command_splitted[1]);
                    this.usingGraph.AddKnoten(knoten);

                    //Ausgabe
                    WriteLine("Node \"" + command_splitted[1] + "\" added... Task complete!");

                    //Setze "changed" auf "true", weil etwas verändert wurde
                    changed = true;
                }
                else if (command_splitted.Length == 6 && command_splitted[2] == "BETWEEN" && command_splitted[4] == "AND")
                {
                    //Ausgabe
                    WriteLine("Adding edge \"" + command_splitted[1] + "\"...");

                    //Erstelle eine Kante und suche nach den angegebenen Knoten
                    Graph.Graph.Kanten kante = new Graph.Graph.Kanten(this.usingGraph, new Graph.Graph.Knoten[2], command_splitted[1]);
                    Graph.Graph.Knoten start = this.usingGraph.SucheKnoten(command_splitted[3]);
                    Graph.Graph.Knoten ende = this.usingGraph.SucheKnoten(command_splitted[5]);

                    if (this.usingGraph.GraphKnoten.Contains(start) && this.usingGraph.GraphKnoten.Contains(ende))
                    {
                        //Falls die Knoten tatsächlich existieren, füge die erstellte Kante hinzu
                        this.usingGraph.AddKante(kante, start, ende);

                        //Ausgabe
                        WriteLine("Edge \"" + command_splitted[1] + "\" added... Task complete!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    else
                    {
                        //Falls es keine solche Knoten gibt, gebe eine Error-Nachricht
                        WriteLine("Error: Node \"" + command_splitted[3] + "\" or node \"" + command_splitted[5] + "\" not found!");
                    }
                }
                else
                {
                    //Error-Nachricht, für den Fall, dass nicht die richtige Anzahl an Argumenten gebracht wurde
                    WriteLine("ERROR: \"" + command_splitted[2] + "\" in " + "\"" + command + "\"" + " is an unknown command!");
                }
            }

            //das Schlüsselwort "REMOVE" entfernt eine Kante/einen Knoten vom Graphen
            else if (command_splitted[0] == "REMOVE")
            {
                if (command_splitted.Length == 2)
                {
                    //falls, das weitere Wort ein "*" ist, erstelle einen neuen Graphen, ansonsten versuche den Knoten oder die Kante zu finden
                    if (command_splitted[1] == "*")
                    {
                        //versuche einen neuen Graphen zu erstellen, schreibe das in die Konsole
                        WriteLine("Trying to empty the Graph!");
                        this.usingGraph = new Graph.Graph(new List<Graph.Graph.Knoten>(), new List<Graph.Graph.Kanten>(), new int[0, 0], this.usingGraph.Name);
                        WriteLine("Created a new Graph!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    else
                    {
                        //schreibe etwas in die Konsole
                        WriteLine("Searching for \"" + command_splitted[1] + "\"...");

                        //suche nach dem Knoten/der Kante mit dem Namen
                        Graph.Graph.Knoten knoten = new Graph.Graph.Knoten(this.usingGraph, new List<Graph.Graph.Kanten>(), "");
                        Graph.Graph.Kanten kante = new Graph.Graph.Kanten(this.usingGraph, new Graph.Graph.Knoten[2], "");

                        //Gucke, ob ein Knoten diesen Namen hat
                        foreach (Graph.Graph.Knoten i in this.usingGraph.GraphKnoten)
                        {
                            if (i.Name == command_splitted[1])
                            {
                                knoten = i;
                            }
                        }

                        //falls nichts gefunden wurde, versuche die Kante dazu zu finden
                        if (knoten.Name == "")
                        {
                            foreach (Graph.Graph.Kanten i in this.usingGraph.GraphKanten)
                            {
                                if (i.Name == command_splitted[1])
                                {
                                    kante = i;
                                }
                            }

                            if (kante.Name == "")
                            {
                                //falls keine Kante gefunden wurde, schreibe das aus
                                WriteLine("Error: Object \"" + command_splitted[1] + "\" not found!");
                            }
                            else
                            {
                                //entferne die Kante und schreibe es aus
                                this.usingGraph.RemoveKante(kante);
                                WriteLine("Edge was succesfully removed!");

                                //Setze "changed" auf "true", weil etwas verändert wurde
                                changed = true;
                            }
                        }
                        else
                        {
                            //entferne den Knoten und schreibe es aus
                            this.usingGraph.RemoveKnoten(knoten);
                            WriteLine("Node was succesfully removed!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                    }
                }
                else
                {
                    //schreibe die Fehlermeldung für einen unbekannten Befehl
                    WriteLine("Error: \"" + command + "\" needs one argument but none or too many were given!");
                }
            }

            //das Schlüsselwort "SAVE" speichert den Graphen ab
            else if (command_splitted[0] == "SAVE")
            {
                WriteLine("Trying to save graph \"" + this.usingGraph.Name + "\" in \"" + this.path + "\"...");//Ausgabe
                Save();//speichere es ab
                WriteLine("Succesfully saved graph \"" + this.usingGraph.Name + "\" in \"" + this.path + "\"...");//Bestätigung, dass alles geklappt hat
            }

            //das Schlüsselwort "RENAME" ändert Namen von Elementen
            else if (command_splitted[0] == "RENAME")
            {
                if (command_splitted.Length == 4 && command_splitted[2] == "TO")
                {
                    //schreibe etwas in die Konsole
                    WriteLine("Searching for \"" + command_splitted[1] + "\"...");

                    //gucke nach, ob der Graph so heißt, ansonsten gucke nach einem Knoten oder einer Kante, die so heißt
                    if (this.usingGraph.Name == command_splitted[1])
                    {
                        //Benenne den Graphen um
                        this.usingGraph.Name = command_splitted[3];
                        WriteLine("Graph was succesfully renamed!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    else
                    {
                        //suche nach dem Knoten/der Kante mit dem Namen
                        Graph.Graph.Knoten knoten = new Graph.Graph.Knoten(this.usingGraph, new List<Graph.Graph.Kanten>(), "");
                        Graph.Graph.Kanten kante = new Graph.Graph.Kanten(this.usingGraph, new Graph.Graph.Knoten[2], "");

                        //Gucke, ob ein Knoten diesen Namen hat
                        foreach (Graph.Graph.Knoten i in this.usingGraph.GraphKnoten)
                        {
                            if (i.Name == command_splitted[1])
                            {
                                knoten = i;
                            }
                        }

                        //falls nichts gefunden wurde, versuche die Kante dazu zu finden
                        if (knoten.Name == "")
                        {
                            foreach (Graph.Graph.Kanten i in this.usingGraph.GraphKanten)
                            {
                                if (i.Name == command_splitted[1])
                                {
                                    kante = i;
                                }
                            }

                            if (kante.Name == "")
                            {
                                //falls keine Kante gefunden wurde, schreibe das aus
                                WriteLine("Error: Object \"" + command_splitted[1] + "\" not found!");
                            }
                            else
                            {
                                //benenne die Kante neu und schreibe es aus
                                kante.Name = command_splitted[3];
                                WriteLine("Edge was succesfully renamed!");

                                //Setze "changed" auf "true", weil etwas verändert wurde
                                changed = true;
                            }
                        }
                        else
                        {
                            //benenne den Knoten neu und schreibe es aus
                            knoten.Name = command_splitted[3];
                            WriteLine("Node was succesfully renamed!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                    }
                }
                else
                {
                    //schreibe die Fehlermeldung für einen unbekannten Befehl
                    try
                    {
                        WriteLine("Error: \"" + command_splitted[2] + "\" in \"" + command + "\" is an unknown command!");
                    }
                    catch
                    {
                        WriteLine("Error: \"" + command + "\" is an unknown command!");
                    }
                }
            }

            //falls kein Schlüsselwort gefunden wurde, schreibe, das es ein Error gibt
            else
            {
                WriteLine("ERROR: \"" + command_splitted[0] + "\" in " + "\"" + command + "\"" + " is an unknown command!");
            }
            #endregion

            //Speichere die Graphen ab, falls etwas verändert wurde
            if (changed)
            {
                this.MainWindow.DrawGraph();
                this.MainWindow.SaveAll();
                this.MainWindow.AktualisiereEigenschaftenFenster(TabItem);
            }
        }

        public void WriteLine(string line)
        {
            //schreibt Text von der "CommandConsole" in die Konsole, scrolle runter
            this.output.Text += "[" + DateTime.Now.ToString() + "] CommandLine: " + line + "\n";
            this.output.ScrollToEnd();
        }

        public void Save()
        {
            //Speicher den Graphen ab
            TransformGraphToFile(this.usingGraph, this.path);
        }

        public static Pollux.Graph.Graph TransformFileToGraph(string path)
        {
            //Initialisiere Variablen
            #region
            Pollux.Graph.Graph graph = new();//Erstelle den Graphen
            List<string> file = new List<string>();//Eine Liste für die ausgelesene Datei
            #endregion

            //auslesen der Datei
            #region
            //lese die Datei aus
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                file.Add(reader.ReadLine());
            }
            reader.Close();
            #endregion

            //nutze die Informationen aus der Datei, um den Graphen zu vervollständigen
            #region
            //Suche nach dem Namen
            int positionName = file.IndexOf("[NAME]");
            graph.Name = file[positionName + 1];

            //Suche nach den Knoten
            int positionKnoten = file.IndexOf("[NODES]");
            for (int i = positionKnoten + 1; file[i] != "[/NODES]"; i++)
            {
                graph.AddKnoten(new Graph.Graph.Knoten(graph, new List<Graph.Graph.Kanten>(), file[i]));
            }

            //Suche nach Kanten
            int positionKanten = file.IndexOf("[EDGES]");
            for (int i = positionKanten + 1; file[i] != "[/EDGES]"; i++)
            {
                string[] liste = file[i].Split('\t');
                Graph.Graph.Kanten kante = new Graph.Graph.Kanten(graph, new Graph.Graph.Knoten[2], liste[0]);
                graph.AddKante(kante, graph.SucheKnoten(liste[1]), graph.SucheKnoten(liste[2]));
            }
            #endregion

            //Rückgabe
            return graph;
        }

        public static string TransformGraphToString(Pollux.Graph.Graph graph)
        {
            //Intialisiere die Variable "file"
            string file = "";

            //speichere den Namen vom Graphen
            file += "[NAME]\n";
            file += graph.Name + "\n";
            file += "[/NAME]\n";

            //speichere die Knoten ab
            file += "[NODES]\n";
            foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
            {
                file += i.Name + "\n";
            }
            file += "[/NODES]\n";

            //speichere die Kanten ab
            file += "[EDGES]\n";
            foreach (Graph.Graph.Kanten i in graph.GraphKanten)
            {
                string str = "";
                foreach (Graph.Graph.Knoten f in i.Knoten)
                {
                    str += "\t" + f.Name;
                }
                file += i.Name + str + "\n";
            }
            file += "[/EDGES]\n";

            //speichere die Liste des Graphs ab
            file += "[GRAPH]\n";
            for (int i = 0; i < graph.Liste.GetLength(1); i++)
            {
                string str = "";
                for (int f = 0; f < graph.Liste.GetLength(0); f++)
                {
                    str += graph.Liste[f, i] + "\t";
                }
                str = str.Remove(str.Length - 1);
                file += str + "\n";
            }
            file += "[/GRAPH]\n";

            //Rückgabe
            return file;
        }

        public static void TransformGraphToFile(Pollux.Graph.Graph graph, string path)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine(TransformGraphToString(graph));
            streamWriter.Close();
        }
    }
}
