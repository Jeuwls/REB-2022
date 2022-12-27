namespace REB
{
    
    public class Constraint
    {
        public Activity Source { get; private set; }
        public Activity Target { get; private set; }
        public ConstraintType ConstraintType { get; private set; }

        public Constraint(Activity src, Activity tgt, ConstraintType rel)
        {
            Source = src;
            Target = tgt;
            ConstraintType = rel;
        }
        
        public override string ToString()
        {
            return $"Source: {Source.eId}, Target: {Target.eId}, Type: {ConstraintType}";
        }
    }
}