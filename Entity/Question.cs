using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheraGuide.Entity
{
    public class Question
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}
