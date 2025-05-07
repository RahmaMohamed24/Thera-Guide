using System.ComponentModel.DataAnnotations;

namespace TheraGuide.ViewModels
{
    public class EmailVerificationViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string VerificationCode { get; set; }
    }
}
