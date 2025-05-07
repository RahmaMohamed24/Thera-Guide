using TheraGuide.Enums;

namespace TheraGuide.Entity
{
    public class Tip
    {
        public long Id { get; set; }
        public TypeEnum Type { get; set; } 
        public string Content { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
