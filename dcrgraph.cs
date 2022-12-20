using System.Collections;
using System.Linq;

namespace REB{
    class DCRGraph{
        List<Activity> Activities = new List<Activity>();
        

        private List<Activity> included = new List<Activity>();
        
        public List<Activity> GetIncluded(List<Activity> activities){
            return included;
        }

        private void UpdateIncluded()
        {
            included = (List<Activity>)Activities.Where(x => x.IsPending());
        }
    }
}