using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheraGuide.Entity
{
    public class Notes
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Note { get; set; }

        public DateTime Date { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}
