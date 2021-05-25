using Castor;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using Thestias;

namespace Pollux
{
    public class CommandConsole
    {
        //Members
        #region
        public VisualGraph UsingGraph;
        private TextBox Output;
        public string User = "User";
        public string LastCommand;
        public string Path;
        public MainWindow MainWindow;
        public TabItem TabItem;
        #endregion

        //Konstruktor
        public CommandConsole(VisualGraph usingGraph, TextBox output, string path, MainWindow main, TabItem tabItem)
        {
            this.UsingGraph = usingGraph;
            this.Output = output;
            this.Path = path;
            this.MainWindow = main;
            this.TabItem = tabItem;
        }

        public async Task CommandAsync(string command, string name)
        {
            try
            {
                bool changed = false;

                //verarbeite die Eingabe
                #region
                //setze den letzten Command
                this.LastCommand = command;

                //Verändere den Befehle, sodass er Sinn ergibt
                for (; command.Contains("  "); command = command.Replace("  ", " ")) ;
                command = (command[command.Length - 1] == ' ') ? command.Remove(command.Length - 1).ToUpper() : command.ToUpper(); //entferne Leerzeichen am Ende und mach es groß

                //Zeige den Command im Output-Fenster
                this.Output.Text += "[" + DateTime.Now.ToString() + "] " + name + ": " + command + "\n";

                //splitte den command auf, sodass er gleich verarbeitet werden kann
                string[] command_splitted = command.Split(' ');
                #endregion

                //Gucke mit welchem Schlüsselwort eingeleitet wurde, handle dann entsprechend
                #region
                //das Schlüsselwort "SHOW" stellt den Graph dar
                if (command_splitted[0] == "SHOW")
                {
                    //falls der Graph leer ist, schreibe das aus, ansonsten öffne Eigenschaften-Fenster "Show"
                    if (this.UsingGraph.Vertices.Count == 0)
                    {
                        this.WriteLine("Graph \"" + this.UsingGraph.Name + "\" is empty!");
                    }
                    else
                    {
                        try
                        {
                            //öffne ein neues EIgenschaften-Fenster "Show"
                            this.UsingGraph.OpenPropertiesWindow();
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
                            this.UsingGraph.AddVertex(command_splitted[1]);

                            //Ausgabe
                            this.WriteLine("Node \"" + command_splitted[1] + "\" added... Task complete!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                        catch (Graph.GraphExceptions.NameAlreadyExistsException)
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
                            //Falls die Knoten tatsächlich existieren, füge die erstellte Kante hinzu
                            this.UsingGraph.AddEdge(command_splitted[1], command_splitted[3], command_splitted[5]);

                            //Ausgabe
                            this.WriteLine("Edge \"" + command_splitted[1] + "\" added... Task complete!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                        catch (Graph.GraphExceptions.NameAlreadyExistsException)
                        {
                            //Gebe eine Error-Nachricht.
                            this.WriteLine("Error: Name already exists!");
                        }
                        catch
                        {
                            //Gebe eine Error-Nachricht.
                            this.WriteLine("Error: Node \"" + command_splitted[3] + "\" or node \"" + command_splitted[5] + "\" not found!");
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
                            this.UsingGraph.AddVertex(command_splitted[1], x, y);

                            //Ausgabe
                            this.WriteLine("Node \"" + command_splitted[1] + "\" at x: " + x + " and y: " + y + "  added... Task complete!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                        catch (Graph.GraphExceptions.NameAlreadyExistsException)
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
                            while (this.UsingGraph.Vertices.Count != 0)
                            {
                                this.UsingGraph.RemoveVertex(this.UsingGraph.Vertices[0]);
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
                            VisualVertex knoten = this.UsingGraph.SearchVertex(command_splitted[1]);
                            VisualEdge kante = this.UsingGraph.SearchEdge(command_splitted[1]);

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
                                    this.UsingGraph.RemoveEdge(kante);
                                    this.WriteLine("Edge was successfully removed!");

                                    //Setze "changed" auf "true", weil etwas verändert wurde
                                    changed = true;
                                }
                            }
                            else
                            {
                                //entferne den Knoten und schreibe es aus
                                this.UsingGraph.RemoveVertex(knoten);
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
                    this.WriteLine("Trying to save graph \"" + this.UsingGraph.Name + "\" in \"" + this.Path + "\"...");//Ausgabe
                    this.Save();//speichere es ab
                    this.WriteLine("Successfully saved graph \"" + this.UsingGraph.Name + "\" in \"" + this.Path + "\"...");//Bestätigung, dass alles geklappt hat
                }

                //das Schlüsselwort "RENAME" ändert Namen von Elementen
                else if (command_splitted[0] == "RENAME")
                {
                    if (command_splitted.Length == 4 && command_splitted[2] == "TO")
                    {
                        //schreibe etwas in die Konsole
                        this.WriteLine("Searching for \"" + command_splitted[1] + "\"...");

                        //gucke nach, ob der Graph so heißt, ansonsten gucke nach einem Knoten oder einer Kante, die so heißt
                        if (this.UsingGraph.Graph.Name == command_splitted[1])
                        {
                            //Benenne den Graphen um
                            this.UsingGraph.Graph.Name = command_splitted[3];
                            this.WriteLine("Graph was successfully renamed!");

                            //Setze "changed" auf "true", weil etwas verändert wurde
                            changed = true;
                        }
                        else
                        {
                            //suche nach dem Knoten/der Kante mit dem Namen
                            VisualVertex knoten = this.UsingGraph.SearchVertex(command_splitted[1]);
                            VisualEdge kante = this.UsingGraph.SearchEdge(command_splitted[1]);

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
                                    //benenne die Kante neu und schreibe es aus
                                    kante.Edge.Name = command_splitted[3];
                                    //((GraphDarstellung.Knoten)kante[0]).RedrawName();
                                    this.WriteLine("Edge was successfully renamed!");

                                    //Setze "changed" auf "true", weil etwas verändert wurde
                                    changed = true;
                                }
                            }
                            else
                            {
                                //benenne den Knoten neu und schreibe es aus
                                knoten.Vertex.Name = command_splitted[3];
                                //knoten.RedrawName();
                                this.WriteLine("Node was successfully renamed!");

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
                    this.UsingGraph.ReloadProperties();
                    this.Save();
                }
            }
            catch
            {
                //Error-Nachricht ausgeben...
                this.WriteLine("Error!");
                SystemSounds.Asterisk.Play();
            }
        }

        public void Command(string command)
        {
            this.CommandAsync(command, this.User);
        }

        public void WriteLine(string line)
        {
            //schreibt Text von der "CommandConsole" in die Konsole, scrolle runter
            this.Output.Text += "[" + DateTime.Now.ToString() + "] CommandLine: " + line + "\n";
            this.Output.ScrollToEnd();
        }

        public void Save()
        {
            //Speicher den Graphen ab
            VisualGraph.TransformGraphToFile(this.UsingGraph, this.Path, this.Path.EndsWith(".poll") ? Graph.FileMode.POLL : Graph.FileMode.GRAPHML);
        }
    }
}
