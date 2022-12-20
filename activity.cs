namespace REB {
    class Activity {
        private string id;
        private List<Activity> conditions = new List<Activity>();

        private List<Activity> responses = new List<Activity>();

        public bool Executed {get; private set;} = true;
        public bool Executeable {get; private set;} = true;
        public bool Include {get; private set;} = true;
        public bool Pending {get; private set;} = true;
        public bool Accepting {get; private set;} = true;
        
        public Activity (string nid) {
            this.id = nid;
        }

        public void AddCondition (Activity place_Constraint) {
            conditions.Add(place_Constraint);
            place_Constraint.SetNotExecuteable();
        }

        public void AddResponse (Activity add_response) {
            responses.Add(add_response);
        }
    
        public void Execute () {
            if (!Executeable) return;
            if (!Executed) {
                foreach (Activity condition in conditions) {
                    condition.SetExecuteable();
                }
                foreach (Activity response in responses) {
                    response.SetPending();
                }
                this.SetNotPending();
            }
            Executed = true;
        }

        public void SetExecuteable () {
            if (Executeable) return;
            Executeable = true;
        }

        public void SetNotExecuteable () {
            if (!Executeable) return; 
            Executeable = false;
        }

        public void SetExclude () {
            if (!Include) return; 
            Include = false; 
        }

        public void SetInclude () {
            Include = true; 
        } 

        public void SetPending() {
            Pending = true;
        }

        public void SetNotPending() {
            if (!Pending) return;
            Pending = false;
        }

        public bool IsPending () {
            if (Include) {
                return Pending;
            } 
            return false;
        }
        

        //if not included 

        
    
    }
}