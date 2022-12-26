namespace REB
{
    public enum RelationType
    {
        Condition,
        Response,
        Milestone,
        Inclusion,
        Exclusion
    }
    
    public class Relation
    {
        public Activity Source { get; private set; }
        public Activity Target { get; private set; }
        public RelationType RelationType { get; private set; }

        public Relation(Activity src, Activity tgt, RelationType rel)
        {
            Source = src;
            Target = tgt;
            RelationType = rel;
        }
    }
}