namespace REB {
    public class Activity {
        private string id;
        // private List<Relation> conditions = new List<Relation>();
        // private List<Relation> responses = new List<Relation>();
        // private List<Relation> milestones = new List<Relation>();
        // private List<Relation> includes = new List<Relation>();
        // private List<Relation> excludes = new List<Relation>();
        private List<Relation> relations = new List<Relation>();
        private bool _pending = false;
        private bool _executable;

        public bool Executed {get; private set;} = true;
        public bool Included {get; private set;} = true;
        public bool Executable
        {
            get { if (Included) {return _executable;} else {return false;} }
            private set { _executable = value; }
        }
        public bool Pending 
        {
            get { if (Included) { return _pending; } return false;}
            private set { _pending = value;}
        }
        public bool Accepting {get; private set;} = true;
        
        public Activity (string nid) {
            this.id = nid;
        }

        public void AddCondition (Relation conditional) {
            conditional.Target.SetNotExecuteable();
            relations.Add(conditional);
        }

        public void AddResponse (Relation response) {
            relations.Add(response);
        }

        public void AddRelation(Relation relation)
        {
            switch (relation.RelationType)
            {
                case RelationType.Condition:
                    relation.Target.SetNotExecuteable();
                    break;
                case RelationType.Milestone:
                    if (Pending)
                    {
                        relation.Target.SetNotExecuteable();
                    }
                    break;
            }
            relations.Add(relation);
        }
    
        public void Execute () {
            if (!Enabled()) return;
            Executed = true;
            Pending = false;

            foreach (Relation r in relations.Where(x => x.Source == this))
            {
                switch (r.RelationType)
                {
                    case RelationType.Condition:
                        r.Target.SetExecuteable();
                        break;
                    case RelationType.Response:
                        r.Target.SetPending();
                        break;
                    case RelationType.Exclusion:
                        r.Target.SetExclude();
                        break;
                    case RelationType.Inclusion:
                        r.Target.SetIncluded();
                        break;
                }
            }
            
            // if (!Executed) {
            //     foreach (Activity condition in conditions) {
            //         condition.SetExecuteable();
            //     }
            //     foreach (Activity response in responses) {
            //         response.SetPending();
            //     }
            //     this.SetNotPending();
            // }
        }

        public bool Enabled()
        {
            foreach (Relation r in relations.Where(x => x.Target == this))
            {
                switch (r.RelationType)
                {
                    case RelationType.Condition:
                        if (r.Source.Included && !r.Source.Executed)
                        {
                            return false;
                        }
                        break;
                    
                    case RelationType.Milestone:
                        if (r.Source.Included && r.Source.Pending)
                        {
                            return false;
                        }
                        break;
                    
                    default:
                        break;
                }
            }
            return true;
        }

        public void SetExecuteable () {
            if (Executable) return;
            Executable = true;
        }

        public void SetNotExecuteable () {
            if (!Executable) return; 
            Executable = false;
        }

        public void SetExclude () {
            if (!Included) return; 
            Included = false; 
        }

        public void SetIncluded () {
            Included = true; 
        } 

        public void SetPending() {
            Pending = true;
        }

        public void SetNotPending() {
            if (!Pending) return;
            Pending = false;
        }

        public bool IsPending () {
            if (Included) {
                return Pending;
            } 
            return false;
        }
        

        //if not included 

        
    
    }
}