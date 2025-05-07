namespace TheraGuide.Entity
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
