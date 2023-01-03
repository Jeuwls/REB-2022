using System.Xml;
using System.Xml.Linq;
using System;

namespace REB {
    public static class XmlReadFile {
        
        public static DCRGraph ReadFile (string pathxml) {
            Console.WriteLine("executed Readfile");

            try {
                File.Exists(pathxml);
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            }

            Console.WriteLine("Found path");

            XmlDocument doc = new XmlDocument();
            doc.Load(pathxml);
            
            XmlNodeList activityNodeList = (doc.SelectNodes("dcrgraph/specification/resources/labelMappings/labelMapping"));
            
            DCRGraph Result = new DCRGraph();

            ExtractActivities(activityNodeList, Result);


            ConstraintExtractor.Extract(doc, Result);

            XmlNodeList includedNodeList = (doc.SelectNodes("dcrgraph/runtime/marking/included/event"));

            GetIncludedActivities(includedNodeList, Result);

            return Result;

        }
        
        
        private static void ExtractActivities(XmlNodeList aq, DCRGraph graph) {

        foreach (XmlNode elem in aq) {
                // eventId, labelID
                Activity tmp = new Activity(elem.Attributes[0].Value.ToLower(), elem.Attributes[1].Value.ToLower());
                graph.AddActivity(tmp);
            }
        }
        private static void GetIncludedActivities(XmlNodeList gia, DCRGraph graph) {

        foreach (XmlNode elem in gia) {
                (graph.Activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower())).SetIncluded(true);
            }
        }
    }
}