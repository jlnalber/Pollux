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
        public GraphDarstellung usingGraph;
        private TextBox output;
        public string user = "User";
        public string lastCommand;
        public string path;
        public MainWindow MainWindow;
        public TabItem TabItem;
        #endregion

        //Konstruktor
        public CommandConsole(GraphDarstellung usingGraph, TextBox output, string path, MainWindow main, TabItem tabItem)
        {
            this.usingGraph = usingGraph;
            this.output = output;
            this.path = path;
            this.MainWindow = main;
            this.TabItem = tabItem;
        }

        public void Command(string command, string name)
        {
            bool changed = false;

            //verarbeite die Eingabe
            #region
            //setze den letzten Command
            this.lastCommand = command;

            //Verändere den Befehle, sodass er Sinn ergibt
            for (; command.Contains("  "); command = command.Replace("  ", " ")) ;
            command = (command[command.Length - 1] == ' ') ? command.Remove(command.Length - 1).ToUpper() : command.ToUpper(); //entferne Leerzeichen am Ende und mach es groß

            //Zeige den Command im Output-Fenster
            this.output.Text += "[" + DateTime.Now.ToString() + "] " + name + ": " + command + "\n";

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
                    this.WriteLine("Graph \"" + this.usingGraph.Name + "\" is empty!");
                }
                else
                {
                    try
                    {
                        //öffne ein neues EIgenschaften-Fenster "Show"
                        Show show = new Show(this.usingGraph, this);
                        show.Show();
                        this.WriteLine("Task complete!");
                    }
                    catch
                    {
                        //Schreibe die Error-Nachricht
                        this.WriteLine("Seems like something went wrong!");
                    }
                }
            }

            //das Schlüsselwort "ADD" fügt eine Kante/einen Knoten zum Graphen hinzu
            else if (command_splitted[0] == "ADD")
            {
                if (command_splitted.Length == 1)
                {
                    //Error-Nachricht, für den Fall, dass nicht die richtige Anzahl an Argumenten gebracht wurde
                    this.WriteLine("ERROR: No argument given, where at least one is needed: " + command);
                }
                else if (command_splitted.Length == 2)
                {
                    try
                    {
                        //Ausgabe
                        this.WriteLine("Adding node \"" + command_splitted[1] + "\"...");

                        //Füge den Knoten hinzu
                        this.usingGraph.AddKnoten(command_splitted[1]);

                        //Ausgabe
                        this.WriteLine("Node \"" + command_splitted[1] + "\" added... Task complete!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    catch (GraphDarstellung.GraphExceptions.NameAlreadyExistsException)
                    {
                        //Gebe eine Error-Nachricht
                        this.WriteLine("Error: Name already exists!");
                    }
                }
                else if (command_splitted.Length == 6 && command_splitted[2] == "BETWEEN" && command_splitted[4] == "AND")
                {
                    try
                    {
                        //Ausgabe
                        this.WriteLine("Adding edge \"" + command_splitted[1] + "\"...");

                        //Erstelle eine Kante und suche nach den angegebenen Knoten
                        GraphDarstellung.Knoten start = this.usingGraph.SucheKnoten(command_splitted[3]);
                        GraphDarstellung.Knoten ende = this.usingGraph.SucheKnoten(command_splitted[5]);

                        if (this.usingGraph.GraphKnoten.Contains(start) && this.usingGraph.GraphKnoten.Contains(ende))
                        {
                            //Falls die Knoten tatsächlich existieren, füge die erstellte Kante hinzu
                            this.usingGraph.AddKante(command_splitted[1], start, ende);

                            //Ausgabe
                            this.WriteLine("Edge \"" + command_splitted[1] + "\" added... Task complete!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                        else
                        {
                            //Falls es keine solche Knoten gibt, gebe eine Error-Nachricht
                            this.WriteLine("Error: Node \"" + command_splitted[3] + "\" or node \"" + command_splitted[5] + "\" not found!");
                        }
                    }
                    catch (GraphDarstellung.GraphExceptions.NameAlreadyExistsException)
                    {
                        //Gebe eine Error-Nachricht
                        this.WriteLine("Error: Name already exists!");
                    }
                }
                else if (command_splitted.Length == 6 && command_splitted[2] == "AT" && command_splitted[4] == "AND")
                {
                    try
                    {
                        //Versuche die angegebenen Koordinaten in Zahlen umzuwandeln
                        double x = double.Parse(command_splitted[3]);
                        double y = double.Parse(command_splitted[5]);

                        //Ausgabe
                        this.WriteLine("Adding node \"" + command_splitted[1] + "\" at x: " + x + " and y: " + y + " ...");

                        //Füge den Knoten hinzu an der angegebenen Position hinzu
                        this.usingGraph.AddKnoten(command_splitted[1], x, y);

                        //Ausgabe
                        this.WriteLine("Node \"" + command_splitted[1] + "\" at x: " + x + " and y: " + y + "  added... Task complete!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    catch (GraphDarstellung.GraphExceptions.NameAlreadyExistsException)
                    {
                        //Gebe eine Error-Nachricht
                        this.WriteLine("Error: Name already exists!");
                    }
                    catch (FormatException)
                    {
                        this.WriteLine("Error: Could not parse the given arguments to numbers!");
                    }
                }
                else
                {
                    //Error-Nachricht, für den Fall, dass nicht die richtige Anzahl an Argumenten gebracht wurde
                    this.WriteLine("ERROR: \"" + command_splitted[2] + "\" in " + "\"" + command + "\"" + " is an unknown command!");
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
                        this.WriteLine("Trying to empty the Graph!");
                        while (this.usingGraph.GraphKnoten.Count != 0)
                        {
                            this.usingGraph.RemoveKnoten(this.usingGraph[0]);
                        }
                        this.WriteLine("Done!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    else
                    {
                        //schreibe etwas in die Konsole
                        this.WriteLine("Searching for \"" + command_splitted[1] + "\"...");

                        //suche nach dem Knoten/der Kante mit dem Namen
                        GraphDarstellung.Knoten knoten = this.usingGraph.SucheKnoten(command_splitted[1]);
                        GraphDarstellung.Kanten kante = this.usingGraph.SucheKanten(command_splitted[1]);

                        //falls nichts gefunden wurde, versuche die Kante dazu zu finden
                        if (knoten == null)
                        {
                            if (kante == null)
                            {
                                //falls keine Kante gefunden wurde, schreibe das aus
                                this.WriteLine("Error: Object \"" + command_splitted[1] + "\" not found!");
                            }
                            else
                            {
                                //entferne die Kante und schreibe es aus
                                this.usingGraph.RemoveKante(kante);
                                this.WriteLine("Edge was successfully removed!");

                                //Setze "changed" auf "true", weil etwas verändert wurde
                                changed = true;
                            }
                        }
                        else
                        {
                            //entferne den Knoten und schreibe es aus
                            this.usingGraph.RemoveKnoten(knoten);
                            this.WriteLine("Node was successfully removed!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                    }
                }
                else
                {
                    //schreibe die Fehlermeldung für einen unbekannten Befehl
                    this.WriteLine("Error: \"" + command + "\" needs one argument but none or too many were given!");
                }
            }

            //das Schlüsselwort "SAVE" speichert den Graphen ab
            else if (command_splitted[0] == "SAVE")
            {
                this.WriteLine("Trying to save graph \"" + this.usingGraph.Name + "\" in \"" + this.path + "\"...");//Ausgabe
                this.Save();//speichere es ab
                this.WriteLine("successfully saved graph \"" + this.usingGraph.Name + "\" in \"" + this.path + "\"...");//Bestätigung, dass alles geklappt hat
            }

            //das Schlüsselwort "RENAME" ändert Namen von Elementen
            else if (command_splitted[0] == "RENAME")
            {
                if (command_splitted.Length == 4 && command_splitted[2] == "TO")
                {
                    //schreibe etwas in die Konsole
                    this.WriteLine("Searching for \"" + command_splitted[1] + "\"...");

                    //gucke nach, ob der Graph so heißt, ansonsten gucke nach einem Knoten oder einer Kante, die so heißt
                    if (this.usingGraph.Name == command_splitted[1])
                    {
                        //Benenne den Graphen um
                        this.usingGraph.Name = command_splitted[3];
                        this.WriteLine("Graph was successfully renamed!");

                        //Setze "changed" auf "true", weil etwas verändert wurde
                        changed = true;
                    }
                    else
                    {
                        //suche nach dem Knoten/der Kante mit dem Namen
                        GraphDarstellung.Knoten knoten = this.usingGraph.SucheKnoten(command_splitted[1]);
                        GraphDarstellung.Kanten kante = this.usingGraph.SucheKanten(command_splitted[1]);

                        //falls nichts gefunden wurde, versuche die Kante dazu zu finden
                        if (knoten == null)
                        {
                            if (kante == null)
                            {
                                //falls keine Kante gefunden wurde, schreibe das aus
                                this.WriteLine("Error: Object \"" + command_splitted[1] + "\" not found!");
                            }
                            else
                            {
                                if (this.usingGraph.SucheKanten(command_splitted[3]) == null)
                                {
                                    //benenne die Kante neu und schreibe es aus
                                    kante.Name = command_splitted[3];
                                    kante[0].RedrawName();
                                    this.WriteLine("Edge was successfully renamed!");

                                    //Setze "changed" auf "true", weil etwas verändert wurde
                                    changed = true;
                                }
                                else
                                {
                                    //Schreibe eine Fehlermeldung
                                    this.WriteLine("Edge of the same name already exists!");
                                }
                            }
                        }
                        else
                        {
                            if (this.usingGraph.SucheKnoten(command_splitted[3]) == null)
                            {
                                //benenne den Knoten neu und schreibe es aus
                                knoten.Name = command_splitted[3];
                                knoten.RedrawName();
                                this.WriteLine("Node was successfully renamed!");

                                //Setze "changed" auf "true", weil etwas verändert wurde
                                changed = true;
                            }
                            else
                            {
                                //Schreibe eine Fehlermeldung
                                this.WriteLine("Node of the same name already exists!");
                            }
                        }
                    }
                }
                else
                {
                    //schreibe die Fehlermeldung für einen unbekannten Befehl
                    try
                    {
                        this.WriteLine("Error: \"" + command_splitted[2] + "\" in \"" + command + "\" is an unknown command!");
                    }
                    catch
                    {
                        this.WriteLine("Error: \"" + command + "\" is an unknown command!");
                    }
                }
            }

            //falls kein Schlüsselwort gefunden wurde, schreibe, das es ein Error gibt
            else
            {
                this.WriteLine("ERROR: \"" + command_splitted[0] + "\" in " + "\"" + command + "\"" + " is an unknown command!");
            }
            #endregion

            //Speichere die Graphen ab, falls etwas verändert wurde
            if (changed)
            {
                this.Save();
                this.MainWindow.AktualisiereEigenschaftenFenster(this.TabItem);
            }
        }

        public void Command(string command)
        {
            this.Command(command, this.user);
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

        public static Graph.Graph TransformFileToGraph(string path)
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

        public static GraphDarstellung TransformFileToGraphDarstellung(string path, Canvas canvas)
        {
            //Initialisiere Variablen
            #region
            GraphDarstellung graph = new(new List<GraphDarstellung.Knoten>(), new List<GraphDarstellung.Kanten>(), new int[0, 0], "", canvas);//Erstelle den Graphen
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
                graph.AddKnoten(new GraphDarstellung.Knoten(graph, new List<GraphDarstellung.Kanten>(), file[i], canvas));
            }

            //Suche nach Kanten
            int positionKanten = file.IndexOf("[EDGES]");
            for (int i = positionKanten + 1; file[i] != "[/EDGES]"; i++)
            {
                string[] liste = file[i].Split('\t');
                graph.AddKante(liste[0], graph.SucheKnoten(liste[1]), graph.SucheKnoten(liste[2]));
            }
            #endregion

            //Rückgabe
            return graph;
        }

        public static string TransformGraphToString(GraphDarstellung graph)
        {
            //Intialisiere die Variable "file"
            string file = "";

            //speichere den Namen vom Graphen
            file += "[NAME]\n";
            file += graph.Name + "\n";
            file += "[/NAME]\n";

            //speichere die Knoten ab
            file += "[NODES]\n";
            foreach (GraphDarstellung.Knoten i in graph.GraphKnoten)
            {
                file += i.Name + "\n";
            }
            file += "[/NODES]\n";

            //speichere die Kanten ab
            file += "[EDGES]\n";
            foreach (GraphDarstellung.Kanten i in graph.GraphKanten)
            {
                string str = "";
                foreach (GraphDarstellung.Knoten f in i.Knoten)
                {
                    str += "\t" + f.Name;
                }
                file += i.Name + str + "\n";
            }
            file += "[/EDGES]\n";

            /*//speichere die Liste des Graphs ab
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
            file += "[/GRAPH]\n";*/

            //Rückgabe
            return file;
        }

        public static string TransformGraphToString(Graph.Graph graph)
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

            /*//speichere die Liste des Graphs ab
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
            file += "[/GRAPH]\n";*/

            //Rückgabe
            return file;
        }

        public static void TransformGraphToFile(GraphDarstellung graph, string path)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine(TransformGraphToString(graph));
            streamWriter.Close();
        }

        public static void TransformGraphToFile(Graph.Graph graph, string path)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine(TransformGraphToString(graph));
            streamWriter.Close();
        }
    }
}
