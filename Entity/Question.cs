using System.ComponentModel.DataAnnotations;

namespace TheraGuide.Entity
{
    public class Question
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
    }
}
