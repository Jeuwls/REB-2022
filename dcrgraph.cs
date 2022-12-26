using System.Collections;
using System.Linq;

namespace REB{
    class DCRGraph{
        List<Activity> Activities = new List<Activity>();

        private List<Activity> included = new List<Activity>();
        private List<Activity> responding = new List<Activity>();
        private List<Activity> executed = new List<Activity>();

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
    }
}