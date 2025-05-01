using System.ComponentModel.DataAnnotations;

namespace TheraGuide.ViewModels
{
    public class NoteViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        [MaxLength(2000)]
        public string Content { get; set; }

        public DateTime? Date { get; set; }
    }
}
