namespace REB {
    public class Activity {
        public string eId { get; private set;}
        public string lId { get; private set;}
        // private List<Constraint> conditions = new List<Constraint>();
        // private List<Constraint> responses = new List<Constraint>();
        // private List<Constraint> milestones = new List<Constraint>();
        // private List<Constraint> includes = new List<Constraint>();
        // private List<Constraint> excludes = new List<Constraint>();
        private List<Constraint> constraints = new List<Constraint>();
        private bool _pending = false;

        public bool Executed {get; private set;} = false;
        public bool Included {get; private set;} = false;
        public bool Enabled {get; private set;} = true;
        private List<Activity> nestedActivities = new List<Activity>();
        public bool Pending 
        {
            get { if (Included) { return _pending; } return false;}
            private set { _pending = value;}
        }
        public bool Accepting {get; private set;} = true;
        
        public Activity (string eventId) {
            this.eId = eventId;
            this.lId = eventId;
            Included = false;
        }
        
        public Activity (string eventId, string labelId) {
            this.eId = eventId;
            this.lId = labelId;
            Included = false;
        }        

        public void AddCondition (Constraint conditional) {
            conditional.Target.SetNotExecuteable();
            constraints.Add(conditional);
        }

        public void AddResponse (Constraint response) {
            constraints.Add(response);
        }

        public void AddConstraint(Constraint Constraint)
        {
            if (this == Constraint.Source)
            {
                switch (Constraint.ConstraintType)
                {
                    case ConstraintType.Condition:
                        Constraint.Target.SetNotExecuteable();
                        break;
                    case ConstraintType.Milestone:
                        if (Pending) {
                            Constraint.Target.SetNotExecuteable();
                        }
                        break;
                }
            }
            constraints.Add(Constraint);
        }
    
        public void Execute () {
            if (!Enabled)
            {
                Console.WriteLine($"Activity {eId}->{lId} is not enabled!");
                return;
            }
            Executed = true;
            Pending = false;

            foreach (Constraint r in constraints.Where(x => x.Source == this))
            {
                switch (r.ConstraintType)
                {
                    case ConstraintType.Response:
                        r.Target.SetPending();
                        break;
                    case ConstraintType.Exclusion:
                        r.Target.SetIncluded(false);
                        break;
                    case ConstraintType.Inclusion:
                        r.Target.SetIncluded(true);
                        break;
                    case ConstraintType.Condition:
                        r.Target.SetExecuteable();
                        break;
                }
            }

            // Console.WriteLine($"Activity {eId}->{lId} executed successfully.");
            
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

        public bool IsEnabled()
        {
            // Check if I'm included
            if (!Included) {
                Enabled = false;
                return Enabled;
            }

            // Check if we are blocked by unexecuted condition events 
            // or pending milestones
            foreach (Constraint r in constraints.Where(x => x.Target == this))
            {
                switch (r.ConstraintType)
                {
                    case ConstraintType.Condition:
                        if (r.Source.Included && !r.Source.Executed)
                        {
                            Enabled = false;
                            return Enabled;
                        }
                        break;
                    
                    case ConstraintType.Milestone:
                        if (r.Source.Included && r.Source.Pending)
                        {
                            Enabled = false;
                            return Enabled;
                        }
                        break;
                    
                    default:
                        break;
                }
            }
            Enabled = true;
            return Enabled;
        }

        public void GetConstraint()
        {
            foreach (Constraint r in constraints)
            {
                Console.WriteLine(r);
            }
        }

        public void SetExecuteable () {
            Enabled = true;
        }

        public void SetNotExecuteable () {
            Enabled = false;
        }

        public void SetIncluded (bool value) {
            Included = value; 
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

        public void SetLabel(string label) {
            lId = label;
        }
        
        public void AddNestedActivity(Activity activity){
            nestedActivities.Add(activity);
        }    
    }
}