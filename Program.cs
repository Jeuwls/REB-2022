using System;
using System.Threading;
using System.Collections;
namespace REB {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 2) {
                System.Console.WriteLine("No input given, running tests");
                Thread.Sleep(1000);
                Test.RunTest();
            } 
            else {
                string pathxml = args[0];
                string pathcsv = args[1];
                TestGraph(pathxml, pathcsv);
            }
        }


        public static void TestGraph(string XML, string CSV) {

            string[,] csv = ImportCSV.Import(CSV);

            DCRGraph graph= XmlReadFile.ReadFile(XML);

            //graph.GetState();
            // foreach (Activity activity in graph.Activities) {
            //     System.Console.WriteLine("Activity: {0} with eID: {1}, is enabled = {2}", activity.lId, activity.eId, activity.IsEnabled());
            // }
        
            //System.Console.WriteLine($"# csv lines: {csv.Length/3}");

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
                    bool tmp = currentRun.ExecuteActivity(csv[i,1]);
                    if (!tmp) thisTrace = false;
                }
                else {
                    if (thisTrace && (currentRun.IsAccepting() == true)) {
                        validtraces.Add(preID);
                        traceValid++; // Add valid trace to list
                    }
                    else traceInvalid ++;
                    preID = csv[i,0];
                    currentRun = graph;
                    thisTrace = true;
                    bool tmp = currentRun.ExecuteActivity(csv[i,1]);
                    if (!tmp) thisTrace = false;
                }

            }

            if (thisTrace && currentRun.IsAccepting()) traceValid++;
            else traceInvalid ++;
            Console.WriteLine("Total Valid traces: {0}, Invalid traces: {1}", traceValid, traceInvalid);

            // foreach (string trace in validtraces) {
            for (int i = 0; i < validtraces.Count; ++i)
            {
                System.Console.WriteLine($"\tValid trace {i+1} trace ID: {validtraces[i]}");
            }
            System.Console.WriteLine("");
        }
    }
}