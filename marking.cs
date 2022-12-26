using System.Collections;
namespace REB{
    public class Marking {
        public bool Included { get; set; }
        public bool Pending { get; set; }
        public bool Executed { get; set; }
        
        public Marking(bool i, bool p, bool e)
        {
            Included = i;
            Pending = p;
            Executed = e;
        }
        public override string ToString()
        {
            return $"(Included: {Included}, Pending: {Pending}, Executed: {Executed})\n";
        }
    }
}