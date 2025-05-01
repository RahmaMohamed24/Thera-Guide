using System.ComponentModel.DataAnnotations.Schema;

namespace TheraGuide.Entity
{
    public class UserAnswers
    {
        public long Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public long AnswerId { get; set; }

        [ForeignKey(nameof(AnswerId))]
        public virtual Answer Answer { get; set; }
    }
}
