using System.Xml;

namespace REB
{
    public class ConstraintExtractor
    {
        public static void Extract (XmlDocument doc, DCRGraph graph) {
            List<Constraint> result = new List<Constraint>();
            XmlNodeList conditionNodeList = (doc.SelectNodes("dcrgraph/specification/constraints/conditions/condition"));
            XmlNodeList excludeNodeList = (doc.SelectNodes("dcrgraph/specification/constraints/excludes/exclude"));
            XmlNodeList inludeNodeList = (doc.SelectNodes("dcrgraph/specification/constraints/includes/include"));
            XmlNodeList milestoneNodeList = (doc.SelectNodes("dcrgraph/specification/constraints/milestones/milestone"));
            XmlNodeList responseNodeList = (doc.SelectNodes("dcrgraph/specification/constraints/responses/response"));
            
            ExtractConditions(conditionNodeList, graph.Activities, graph);
            ExtractExclude(excludeNodeList, graph.Activities, graph);
            ExtractInclude(inludeNodeList, graph.Activities, graph);
            ExtractMilestone(milestoneNodeList, graph.Activities, graph);
            ExtractResponse(responseNodeList, graph.Activities, graph);

            // return result;
        }

        private static void ExtractConditions(XmlNodeList cond, List<Activity> activities, DCRGraph graph) {
            foreach (XmlNode elem in cond) {
                Activity src = activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower());
                if (src == null) {
                    System.Console.WriteLine("Source: {0} does not exist in the DCR graph", elem.Attributes[0].Value);

                }
                Activity tgt = activities.Find(x => x.eId == elem.Attributes[1].Value.ToLower());
                if (tgt == null) {
                    System.Console.WriteLine("Target: {0} does not exist in the DCR graph", elem.Attributes[1].Value);

                };
                graph.AddConstraint(src, tgt, ConstraintType.Condition);
            }
        }
        private static void ExtractExclude(XmlNodeList cond, List<Activity> activities, DCRGraph graph) {
            foreach (XmlNode elem in cond) {
                Activity src = activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower());
                if (src == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[0].Value);
                }
    
                Activity tgt = activities.Find(x => x.eId == elem.Attributes[1].Value.ToLower());
                if (tgt == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[1].Value);
                };
            
                graph.AddConstraint(src, tgt, ConstraintType.Exclusion);
            }
        }
        private static void ExtractInclude(XmlNodeList cond, List<Activity> activities, DCRGraph graph) {
            foreach (XmlNode elem in cond) {
                Activity src = activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower());
                if (src == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[0].Value);
                }
    
                Activity tgt = activities.Find(x => x.eId == elem.Attributes[1].Value.ToLower());
                if (tgt == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[1].Value);
                };
            
                graph.AddConstraint(src, tgt, ConstraintType.Inclusion);
            }
        }
        private static void ExtractResponse(XmlNodeList cond, List<Activity> activities, DCRGraph graph) {
            foreach (XmlNode elem in cond) {
                Activity src = activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower());
                if (src == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[0].Value);
                }
    
                Activity tgt = activities.Find(x => x.eId == elem.Attributes[1].Value.ToLower());
                if (tgt == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[1].Value);
                };
            
                graph.AddConstraint(src, tgt, ConstraintType.Response);
            }
        }
        private static void ExtractMilestone(XmlNodeList cond, List<Activity> activities, DCRGraph graph) {
            foreach (XmlNode elem in cond) {
                Activity src = activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower());
                if (src == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[0].Value);
                }
    
                Activity tgt = activities.Find(x => x.eId == elem.Attributes[1].Value.ToLower());
                if (tgt == null) {
                    System.Console.WriteLine("Activity: {0} does not exist in the DCR graph", elem.Attributes[1].Value);
                };
            
                graph.AddConstraint(src, tgt, ConstraintType.Milestone);
            }
        }

        private static void ExtractConstraint(XmlNodeList cond, List<Activity> activities, DCRGraph graph) {
            foreach (XmlNode elem in cond) {
                Activity src = activities.Find(x => x.eId == elem.Attributes[0].Value.ToLower());
                if (src == null) {
                    System.Console.WriteLine("Source: {0} does not exist in the DCR graph", elem.Attributes[0].Value);

                }
                Activity tgt = activities.Find(x => x.eId == elem.Attributes[1].Value.ToLower());
                if (tgt == null) {
                    System.Console.WriteLine("Target: {0} does not exist in the DCR graph", elem.Attributes[1].Value);

                };
                graph.AddConstraint(src, tgt, ConstraintType.Condition);
            }
        }
    }
}
