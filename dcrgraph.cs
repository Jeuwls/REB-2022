using System.Collections;
using System.Linq;

namespace REB {
    public class DCRGraph {
        public List<Activity> Activities = new List<Activity>();

        private List<Activity> included = new List<Activity>();
        private List<Activity> responding = new List<Activity>();
        private List<Activity> executed = new List<Activity>();
        private List<DCRGraph> childGraph = new List<DCRGraph>();
        private List<DCRGraph> parentGraph = new List<DCRGraph>();
        // private Activity parent;

        public DCRGraph() {
            
        }

        public DCRGraph(DCRGraph dcr)
        {
            //parentGraph
        }

        public List<Activity> GetIncluded(List<Activity> activities){
            return included;
        }

        public bool IsAccepting()
        {
            responding = (List<Activity>)Activities.Where(x => x.IsPending()).ToList<Activity>();
            return (responding.Count == 0);
            // if (responding.Count != 0) return true;
            // else return false;
        }

        private void UpdateIncluded()
        {
            included = (List<Activity>)Activities.Where(x => x.Included);
        }

        private void UpdateExecuted()
        {
            executed = (List<Activity>)Activities.Where(x => x.Executed);
        }

        public void AddActivity(Activity e)
        {
            Activities.Add(e);
        }

        public void AddConstraint(Activity src, Activity tgt, ConstraintType rt)
        {
            Constraint con = new Constraint(src, tgt, rt);
            src.AddConstraint(con);
            tgt.AddConstraint(con);
        }

        public bool ExecuteActivity(string id)
        {
            Activity e = Activities.Find(x => x.lId == id);
            if (e == null) return false;
            if (e.Enabled) e.Execute();
            return e.Enabled;
        }

        public void GetState(){
            foreach (Activity a in Activities) {
                Console.WriteLine($"eventid = {a.eId}, label = {a.lId}, isPending = {a.Pending}");
            }
        }
        public List<Activity> GetActivities(){
            return Activities;
        }
    }
}