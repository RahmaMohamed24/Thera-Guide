using System.ComponentModel.DataAnnotations;
using TheraGuide.Entity;

namespace TheraGuide.ViewModels
{
    public class QuestionViewModel
    {
        public long Id { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
        public long CategoryId { get; set; }
        public  List<Answer> Answers { get; set; }
    }
}
