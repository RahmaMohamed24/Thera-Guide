using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheraGuide.Entity
{
    public class Answer
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
        public long QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}
