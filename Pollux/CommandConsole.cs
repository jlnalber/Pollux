using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

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

        public async Task CommandAsync(string command, string name)
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
                            this.usingGraph.RemoveKnoten(this.usingGraph.GraphKnoten[0]);
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
                this.WriteLine("Successfully saved graph \"" + this.usingGraph.Name + "\" in \"" + this.path + "\"...");//Bestätigung, dass alles geklappt hat
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
                                    ((GraphDarstellung.Knoten)kante[0]).RedrawName();
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
                this.MainWindow.AktualisiereEigenschaftenFenster(this.TabItem);
                this.Save();
            }
        }

        public void Command(string command)
        {
            this.CommandAsync(command, this.user);
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
            TransformGraphToFile(this.usingGraph, this.path, this.path.EndsWith(".poll") ? FileMode.POLL : FileMode.GRAPHML);
        }

        public static Graph.Graph TransformFileToGraph(string path, FileMode fileMode = FileMode.GRAPHML)
        {
            try
            {
                if (fileMode == FileMode.POLL)
                {
                    //Initialisiere Variablen
                    List<string> file = new List<string>();//Eine Liste für die ausgelesene Datei

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
                    string name = file[positionName + 1];

                    //Suche nach den Knoten
                    int positionKnoten = file.IndexOf("[NODES]");
                    HashSet<string> knotenNamen = new();
                    for (int i = positionKnoten + 1; file[i] != "[/NODES]"; ++i)
                    {
                        knotenNamen.Add(file[i]);
                    }

                    Graph.Graph graph = new(knotenNamen, name);

                    //Suche nach Kanten
                    int positionKanten = file.IndexOf("[EDGES]");
                    for (int i = positionKanten + 1; file[i] != "[/EDGES]"; ++i)
                    {
                        string[] liste = file[i].Split('\t');
                        graph.AddKante(new Graph.Graph.Kanten(graph, new Graph.Graph.Knoten[2], liste[0]), graph.SucheKnoten(liste[1]), graph.SucheKnoten(liste[2]));
                    }
                    #endregion

                    //Rückgabe
                    return graph;
                }
                else
                {
                    XmlDocument xmlReader = new();
                    StreamReader streamReader = new(path);
                    string xml = streamReader.ReadToEnd();
                    streamReader.Close();
                    xmlReader.LoadXml(xml);

                    XmlNodeList keys = xmlReader.GetElementsByTagName("key");

                    XmlNodeList nodes = xmlReader.GetElementsByTagName("node");

                    HashSet<string> nodeNames = new();
                    foreach (XmlElement i in nodes)
                    {
                        if (i.HasAttribute("id"))
                        {
                            nodeNames.Add(i.GetAttribute("id"));
                        }
                        else
                        {
                            int suffix = 0;
                            for (; nodeNames.Contains("NODE" + suffix); ++suffix) ;
                            nodeNames.Add("NODE" + suffix);
                        }
                    }

                    XmlElement graphXml = (XmlElement)xmlReader.GetElementsByTagName("graph")[0];
                    string name = graphXml.HasAttribute("id") ? graphXml.GetAttribute("id").ToUpper() : "GRAPH";

                    Graph.Graph graph = new(nodeNames, name);

                    XmlNodeList edges = xmlReader.GetElementsByTagName("edge");

                    foreach (XmlElement i in edges)
                    {
                        if (i.HasAttribute("id") && i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            graph.AddKante(i.GetAttribute("id"), graph.SucheKnoten(i.GetAttribute("source")), graph.SucheKnoten(i.GetAttribute("target")));
                        }
                        else if (i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            int suffix = 0;
                            for (; graph.SucheKanten("EDGE" + suffix) != null; ++suffix) ;
                            graph.AddKante("EDGE" + suffix, graph.SucheKnoten(i.GetAttribute("source")), graph.SucheKnoten(i.GetAttribute("target")));
                        }
                    }

                    return graph;
                }
            }
            catch
            {
                return new();
            }
        }

        public static GraphDarstellung TransformFileToGraphDarstellung(string path, Canvas canvas, FileMode fileMode = FileMode.GRAPHML)
        {
            try
            {
                if (fileMode == FileMode.POLL)
                {
                    //Initialisiere Variablen
                    #region
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
                    string name = file[positionName + 1];

                    //Suche nach den Knoten
                    int positionKnoten = file.IndexOf("[NODES]");
                    HashSet<string> knotenNamen = new();
                    for (int i = positionKnoten + 1; file[i] != "[/NODES]"; ++i)
                    {
                        knotenNamen.Add(file[i]);
                    }

                    GraphDarstellung graph = new(knotenNamen, name, canvas);//Erstelle den Graphen

                    //Suche nach Kanten
                    int positionKanten = file.IndexOf("[EDGES]");
                    for (int i = positionKanten + 1; file[i] != "[/EDGES]"; ++i)
                    {
                        string[] liste = file[i].Split('\t');
                        if (liste[1] == liste[2])
                        {
                            GraphDarstellung.Knoten knoten = graph.SucheKnoten(liste[1]);
                            graph.AddKante(liste[0], knoten, knoten);
                        }
                        else
                        {
                            graph.AddKante(liste[0], graph.SucheKnoten(liste[1]), graph.SucheKnoten(liste[2]));
                        }
                    }
                    #endregion

                    //Rückgabe
                    return graph;
                }
                else
                {
                    XmlDocument xmlReader = new();
                    StreamReader streamReader = new(path);
                    string xml = streamReader.ReadToEnd();
                    streamReader.Close();
                    xmlReader.LoadXml(xml);

                    XmlNodeList keys = xmlReader.GetElementsByTagName("key");

                    XmlNodeList nodes = xmlReader.GetElementsByTagName("node");

                    HashSet<string> nodeNames = new();
                    foreach (XmlElement i in nodes)
                    {
                        if (i.HasAttribute("id"))
                        {
                            nodeNames.Add(i.GetAttribute("id"));
                        }
                        else
                        {
                            int suffix = 0;
                            for (; nodeNames.Contains("NODE" + suffix); ++suffix) ;
                            nodeNames.Add("NODE" + suffix);
                        }
                    }

                    XmlElement graphXml = (XmlElement)xmlReader.GetElementsByTagName("graph")[0];
                    string name = graphXml.HasAttribute("id") ? graphXml.GetAttribute("id").ToUpper() : "GRAPH";

                    GraphDarstellung graph = new(nodeNames, name, canvas);

                    foreach (XmlElement i in nodes)
                    {
                        foreach (XmlElement n in i.ChildNodes)
                        {
                            if (n.Name == "data" && n.HasAttribute("key") && i.HasAttribute("id"))
                            {
                                string keyID = n.GetAttribute("key");

                                XmlElement key = (from XmlElement k in keys where k.GetAttribute("id") == keyID select k).First();

                                if (key.HasAttribute("for") && key.HasAttribute("attr.name") && key.GetAttribute("for") == "node")
                                {
                                    switch (key.GetAttribute("attr.name"))
                                    {
                                        case "positionx":
                                            GraphDarstellung.Knoten knoten = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            Thickness margin = knoten.Ellipse.Margin;
                                            margin.Left = double.Parse(n.InnerText);
                                            knoten.Ellipse.Margin = margin;
                                            knoten.RedrawName(); break;
                                        case "positiony":
                                            GraphDarstellung.Knoten knoten0 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            Thickness margin0 = knoten0.Ellipse.Margin;
                                            margin0.Top = double.Parse(n.InnerText);
                                            knoten0.Ellipse.Margin = margin0;
                                            knoten0.RedrawName(); break;
                                        case "strokeThickness":
                                            GraphDarstellung.Knoten knoten1 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten1.Ellipse.StrokeThickness = double.Parse(n.InnerText);
                                            knoten1.RedrawName(); break;
                                        case "height":
                                            GraphDarstellung.Knoten knoten2 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten2.Ellipse.Height = double.Parse(n.InnerText);
                                            knoten2.RedrawName(); break;
                                        case "width":
                                            GraphDarstellung.Knoten knoten3 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten3.Ellipse.Width = double.Parse(n.InnerText);
                                            knoten3.RedrawName(); break;
                                        case "stroke":
                                            GraphDarstellung.Knoten knoten4 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten4.Ellipse.Stroke = Stuff.StringToColor(n.InnerText);
                                            knoten4.RedrawName(); break;
                                        case "fill":
                                            GraphDarstellung.Knoten knoten5 = graph.SucheKnoten(i.GetAttribute("id").ToUpper());
                                            knoten5.Ellipse.Fill = Stuff.StringToColor(n.InnerText);
                                            knoten5.RedrawName(); break;
                                    }
                                }
                            }
                        }
                    }

                    XmlNodeList edges = xmlReader.GetElementsByTagName("edge");

                    foreach (XmlElement i in edges)
                    {
                        if (i.HasAttribute("id") && i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            graph.AddKante(i.GetAttribute("id"), graph.SucheKnoten(i.GetAttribute("source")), graph.SucheKnoten(i.GetAttribute("target")));
                        }
                        else if (i.HasAttribute("source") && i.HasAttribute("target"))
                        {
                            int suffix = 0;
                            for (; graph.SucheKanten("EDGE" + suffix) != null; ++suffix) ;
                            graph.AddKante("EDGE" + suffix, graph.SucheKnoten(i.GetAttribute("source")), graph.SucheKnoten(i.GetAttribute("target")));
                        }

                        foreach (XmlElement n in i.ChildNodes)
                        {
                            if (n.Name == "data" && n.HasAttribute("key") && i.HasAttribute("id"))
                            {
                                string keyID = n.GetAttribute("key");

                                XmlElement key = (from XmlElement k in keys where k.GetAttribute("id") == keyID select k).First();

                                if (key.HasAttribute("for") && key.HasAttribute("attr.name") && key.GetAttribute("for") == "edge")
                                {
                                    switch (key.GetAttribute("attr.name"))
                                    {
                                        case "strokeThickness":
                                            GraphDarstellung.Kanten kanten = graph.SucheKanten(i.GetAttribute("id").ToUpper());
                                            if (kanten.Line is Line line0)
                                            {
                                                line0.StrokeThickness = double.Parse(n.InnerText);
                                            }
                                            else if (kanten.Line is Ellipse ellipse0)
                                            {
                                                ellipse0.StrokeThickness = double.Parse(n.InnerText);
                                            }
                                            break;
                                        case "stroke":
                                            GraphDarstellung.Kanten kanten0 = graph.SucheKanten(i.GetAttribute("id").ToUpper());
                                            if (kanten0.Line is Line line1)
                                            {
                                                line1.Stroke = Stuff.StringToColor(n.InnerText);
                                            }
                                            else if (kanten0.Line is Ellipse ellipse1)
                                            {
                                                ellipse1.Stroke = Stuff.StringToColor(n.InnerText);
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    return graph;
                }
            }
            catch
            {
                return new GraphDarstellung(new(), new(), new int[0, 0], "GRAPH", canvas);
            }
        }

        public static string TransformGraphToString(GraphDarstellung graph, FileMode fileMode = FileMode.GRAPHML)
        {
            if (fileMode == FileMode.POLL)
            {
                return TransformGraphToString(graph.CastToGraph(), FileMode.POLL);
            }
            else
            {
                StringWriter stringBuilder = new();
                XmlWriterSettings xmlWriterSetting = new();
                xmlWriterSetting.Encoding = Encoding.UTF8;
                xmlWriterSetting.Indent = true;
                xmlWriterSetting.NewLineChars = "\n";
                xmlWriterSetting.OmitXmlDeclaration = false;
                XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSetting);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("graphml", "http://graphml.graphdrawing.org/xmlns");
                xmlWriter.WriteAttributeString("xmlns", "xsi", "", "http://www.w3.org/2001/XMLSchema-instance");
                xmlWriter.WriteAttributeString("xsi", "schemaLocation", null, "http://graphml.graphdrawing.org/xmlns http://graphml.graphdrawing.org/xmlns/1.1/graphml.xsd");

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d0");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "positionx");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d1");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "positiony");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d2");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "strokeThickness");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d3");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "height");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d4");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "width");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d5");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "stroke");
                xmlWriter.WriteAttributeString("attr.type", "string");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d6");
                xmlWriter.WriteAttributeString("for", "node");
                xmlWriter.WriteAttributeString("attr.name", "fill");
                xmlWriter.WriteAttributeString("attr.type", "string");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d7");
                xmlWriter.WriteAttributeString("for", "edge");
                xmlWriter.WriteAttributeString("attr.name", "stroke");
                xmlWriter.WriteAttributeString("attr.type", "string");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("key");
                xmlWriter.WriteAttributeString("id", "d8");
                xmlWriter.WriteAttributeString("for", "edge");
                xmlWriter.WriteAttributeString("attr.name", "strokeThickness");
                xmlWriter.WriteAttributeString("attr.type", "double");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("graph");
                xmlWriter.WriteAttributeString("id", graph.Name);
                xmlWriter.WriteAttributeString("edgedefault", "undirected");

                foreach (GraphDarstellung.Knoten i in graph.GraphKnoten)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", i.Name);

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d0");
                    xmlWriter.WriteString(i.Ellipse.Margin.Left.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d1");
                    xmlWriter.WriteString(i.Ellipse.Margin.Top.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d2");
                    xmlWriter.WriteString(i.Ellipse.StrokeThickness.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d3");
                    xmlWriter.WriteString(i.Ellipse.Height.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d4");
                    xmlWriter.WriteString(i.Ellipse.Width.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d5");
                    xmlWriter.WriteString(Stuff.ColorToString((SolidColorBrush)i.Ellipse.Stroke));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d6");
                    xmlWriter.WriteString(Stuff.ColorToString((LinearGradientBrush)i.Ellipse.Fill));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                foreach (GraphDarstellung.Kanten i in graph.GraphKanten)
                {
                    xmlWriter.WriteStartElement("edge");
                    xmlWriter.WriteAttributeString("id", i.Name);
                    xmlWriter.WriteAttributeString("source", i.Knoten[0].Name);
                    xmlWriter.WriteAttributeString("target", i.Knoten[1].Name);

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d7");
                    if (i.Line is Line line)
                    {
                        xmlWriter.WriteString(Stuff.ColorToString((SolidColorBrush)line.Stroke));
                    }
                    else if (i.Line is Ellipse ellipse)
                    {
                        xmlWriter.WriteString(Stuff.ColorToString((SolidColorBrush)ellipse.Stroke));
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("data");
                    xmlWriter.WriteAttributeString("key", "d8");
                    if (i.Line is Line line0)
                    {
                        xmlWriter.WriteString(line0.StrokeThickness.ToString());
                    }
                    else if (i.Line is Ellipse ellipse0)
                    {
                        xmlWriter.WriteString(ellipse0.StrokeThickness.ToString());
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                return stringBuilder.ToString();
            }
        }

        public static string TransformGraphToString(Graph.Graph graph, FileMode fileMode = FileMode.GRAPHML)
        {
            if (fileMode == FileMode.POLL)
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

                //Rückgabe
                return file;
            }
            else
            {
                StringWriter stringBuilder = new();
                XmlWriterSettings xmlWriterSetting = new();
                xmlWriterSetting.Encoding = Encoding.UTF8;
                xmlWriterSetting.Indent = true;
                xmlWriterSetting.NewLineChars = "\n";
                xmlWriterSetting.OmitXmlDeclaration = false;
                XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSetting);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("graphml", "http://graphml.graphdrawing.org/xmlns");
                xmlWriter.WriteAttributeString("xmlns", "xsi", "", "http://www.w3.org/2001/XMLSchema-instance");
                xmlWriter.WriteAttributeString("xsi", "schemaLocation", null, "http://graphml.graphdrawing.org/xmlns http://graphml.graphdrawing.org/xmlns/1.1/graphml.xsd");

                xmlWriter.WriteStartElement("graph");
                xmlWriter.WriteAttributeString("id", graph.Name);
                xmlWriter.WriteAttributeString("edgedefault", "undirected");

                foreach (Graph.Graph.Knoten i in graph.GraphKnoten)
                {
                    xmlWriter.WriteStartElement("node");
                    xmlWriter.WriteAttributeString("id", i.Name);
                    xmlWriter.WriteEndElement();
                }

                foreach (Graph.Graph.Kanten i in graph.GraphKanten)
                {
                    xmlWriter.WriteStartElement("edge");
                    xmlWriter.WriteAttributeString("id", i.Name);
                    xmlWriter.WriteAttributeString("source", i.Knoten[0].Name);
                    xmlWriter.WriteAttributeString("target", i.Knoten[1].Name);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                return stringBuilder.ToString();
            }
        }

        public static void TransformGraphToFile(GraphDarstellung graph, string path, FileMode fileMode = FileMode.GRAPHML)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            //TransformGraphToString(graph, fileMode);
            streamWriter.WriteLine(TransformGraphToString(graph, fileMode));
            streamWriter.Close();
        }

        public static void TransformGraphToFile(Graph.Graph graph, string path, FileMode fileMode = FileMode.GRAPHML)
        {
            //Erstelle den StreamWriter "streamWriter", füge dann den Inhalt hinzu und schließe den StreamWriter "streamWriter"
            StreamWriter streamWriter = new(path);
            streamWriter.WriteLine(TransformGraphToString(graph, fileMode));
            streamWriter.Close();
        }

        public enum FileMode
        {
            POLL,
            GRAPHML
        }
    }
}
