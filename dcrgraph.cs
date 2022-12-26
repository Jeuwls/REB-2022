using System.Collections;
using System.Linq;

namespace REB{
    class DCRGraph{
        List<Activity> Activities = new List<Activity>();

        private List<Activity> included = new List<Activity>();
        private List<Activity> responding = new List<Activity>();
        private List<Activity> executed = new List<Activity>();

        public DCRGraph()
        {

        }

        public List<Activity> GetIncluded(List<Activity> activities){
            return included;
        }

        private void UpdateResponding()
        {
            responding = (List<Activity>)Activities.Where(x => x.IsPending());
        }

        private void UpdateIncluded()
        {
            included = (List<Activity>)Activities.Where(x => x.Included);
        }

        private void UpdateExecuted()
        {
            executed = (List<Activity>)Activities.Where(x => x.Executed);
        }

        public void AddEvent(string nid)
        {
            Activity a = new Activity(nid);
            Activities.Add(a);
        }

        public void AddRelation(string sid, string tid, RelationType rt)
        {
            Activity src = Activities.Find(x => x.id == sid);
            if (src == null) return;

            Activity tgt = Activities.Find(x => x.id == tid);
            if (tgt == null) return;

            Relation rel = new Relation(src, tgt, rt);
            src.AddRelation(rel);
            tgt.AddRelation(rel);

            tgt.GetRelations();
            // Console.WriteLine(tgt.Enabled());
            // Relation rel = new Relation()
        }

        public void ExecuteActivity(string id)
        {
            Activity e = Activities.Find(x => x.id == id);
            if (e == null) return;

            e.Execute();
        }
    }
}