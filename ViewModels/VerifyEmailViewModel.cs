using System.ComponentModel.DataAnnotations;

namespace TheraGuide.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string VerificationCode { get; set; }
    }
}
