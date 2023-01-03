using System;
using System.Collections;
namespace REB {
    class Program {
        static void Main(string[] args) {
        // Display the number of command line arguments.
            //Console.WriteLine(args.Length);

            //Get two string paths 
            //path/csv path/xml

            if (args.Length < 2) {
                System.Console.WriteLine("Input XML and CSV file path");
                System.Environment.Exit(0);
            } 
            string pathxml = args[0];
            string pathcsv = args[1];

            string[,] csv = ImportCSV.Import(pathcsv);

            DCRGraph graph= XmlReadFile.ReadFile(pathxml);

            graph.GetState();
            foreach (Activity activity in graph.Activities) {
                System.Console.WriteLine("Activity: {0} with eID: {1}, is enabled = {2}", activity.lId, activity.eId, activity.IsEnabled());
            }
        
            System.Console.WriteLine($"# csv lines: {csv.Length/3}");

            // loop setup:

            List<string> validtraces= new List<string>();

            int traceValid =0;
            int traceInvalid =0;
            string preID = csv[1,0];
            DCRGraph currentRun = graph;
            bool thisTrace = true;
            for (int i = 1; i < csv.Length/3; i++) {
                if (csv[i,0]== preID){
                    // lookup and run event
                    bool tmp = graph.ExecuteActivity(csv[i,1]);
                    if (!tmp) thisTrace = false;
                }
                else {
                    if (thisTrace) traceValid++; // Add valid trace to list
                    else traceInvalid ++;
                    if (thisTrace) validtraces.Add(preID);
                    preID = csv[i,0];
                    currentRun = graph;
                    thisTrace = true;
                    bool tmp = graph.ExecuteActivity(csv[i,1]);
                    if (!tmp) thisTrace = false;
                }

            }

            if (thisTrace && graph.IsAccepting()) traceValid++;
            else traceInvalid ++;
            Console.WriteLine("Valid traces: {0}, Invalid traces: {1}", traceValid, traceInvalid);

            // foreach (string trace in validtraces) {
            for (int i = 0; i < validtraces.Count; ++i)
            {
                System.Console.WriteLine($"Valid trace {i+1} trace ID: {validtraces[i]}");
            }

            // graph.ExecuteActivity()

        }
    }
}