using System.Xml;
using System.Xml.Linq;
using System;

namespace REB {
    public static class XmlReadFile {
        
        public static DCRGraph ReadFile (string pathxml) {

            try {
                File.Exists(pathxml);
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(pathxml);

            DCRGraph Result = new DCRGraph();
            //ExtractActivities(eventNodeList, Result);

            XmlNodeList eventNodeList = (doc.SelectNodes("dcrgraph/specification/resources/events/event"));
            XmlNodeList activityNodeList = (doc.SelectNodes("dcrgraph/specification/resources/labelMappings/labelMapping"));
            

            ExtractActivities(eventNodeList, activityNodeList, Result);


            ConstraintExtractor.Extract(doc, Result);

            XmlNodeList includedNodeList = (doc.SelectNodes("dcrgraph/runtime/marking/included/event"));

            GetIncludedActivities(includedNodeList, Result);

            return Result;
        }
        
        private static void ExtractActivities(XmlNodeList activities, XmlNodeList labelmapping, DCRGraph graph) {

        foreach (XmlNode elem in activities) {
                // eventId, labelID
                Activity tmp = new Activity(elem.Attributes[0].Value.ToLower());
                graph.AddActivity(tmp);
                foreach (XmlNode node in  elem.SelectNodes("event")) {
                    RecNodeSearchActivities(node, elem, graph);
                }
            }

        foreach (XmlNode elem in labelmapping) {
                graph.Activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower()).SetLabel(elem.Attributes[1].Value.ToLower());
            }
        }
        private static void GetIncludedActivities(XmlNodeList gia, DCRGraph graph) {

        System.Console.WriteLine("Printing nodes in graph");
        // foreach (Activity node in graph.Activities) {
        //     System.Console.WriteLine(node.eId);
        // }

        // System.Console.WriteLine("\nPrinting elem in gia");
        foreach (XmlNode elem in gia) {
                // System.Console.WriteLine(elem.Attributes[0].Value);
                (graph.Activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower())).SetIncluded(true);
            }
        }

        private static void RecNodeSearchActivities(XmlNode elem, XmlNode Parent, DCRGraph graph) {
            // eventId, labelID
            Activity tmp = new Activity(elem.Attributes[0].Value.ToLower());
            graph.AddActivity(tmp);
            foreach (XmlNode node in  elem.SelectNodes("event")) {
                RecNodeSearchActivities(node, elem, graph);
            }
        }
    }
}