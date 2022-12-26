using System;
using System.Collections;
namespace REB {
    class Program {
        static void Main(string[] args) {
        // Display the number of command line arguments.
            Console.WriteLine(args.Length);
            
            DCRGraph gr = new DCRGraph();
            gr.AddEvent("EventA");
            gr.AddEvent("EventB");
            gr.AddRelation("EventA", "EventB", RelationType.Condition);
            gr.ExecuteActivity("EventB");
            gr.ExecuteActivity("EventA");
            gr.ExecuteActivity("EventB");
        }
    }
}