using System;
using System.Collections;
namespace REB {
    class Program {
        static void Main(string[] args) {
        // Display the number of command line arguments.
            //Console.WriteLine(args.Length);

            //Get two string paths 
            //path/csv path/xml
            //string pathcsv = args[0];
            string pathxml = "files\\assignment-1-part-2-task-2.xml";
            string pathcsv = "files\\convertcsv.csv";

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
                    System.Console.WriteLine(tmp);
                    if (!tmp) thisTrace = false;
                }
                else {
                    if (thisTrace) traceValid++;
                    else traceInvalid ++;

                    // System.Console.WriteLine(preID);
                    if (thisTrace) validtraces.Add(preID);
                    preID = csv[i,0];
                    currentRun = graph;
                    thisTrace = true;
                    bool tmp = graph.ExecuteActivity(csv[i,1]);
                    if (!tmp) thisTrace = false;
                }

                // System.Console.Write($"{i}\t");
                // System.Console.WriteLine("id = {0}, Title = {1}, Date = {2}",csv[i,0], csv[i,1], csv[i,2]);
            }

            if (thisTrace) traceValid++;
            else traceInvalid ++;
            Console.WriteLine("Valid traces: {0}, Invalid traces: {1}", traceValid, traceInvalid);

            foreach (string trace in validtraces) {
                System.Console.WriteLine(trace);
            }

            // graph.ExecuteActivity()

        }
    }
}