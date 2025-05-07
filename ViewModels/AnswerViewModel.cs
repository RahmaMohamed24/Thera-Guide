using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheraGuide.Entity;

namespace TheraGuide.ViewModels
{
    public class AnswerViewModel
    {
        [MaxLength(500)]
        public string Content { get; set; }
        public long QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; }
    }
}
