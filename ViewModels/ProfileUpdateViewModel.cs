using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheraGuide.Entity;

namespace TheraGuide.ViewModels
{
    public class ProfileUpdateViewModel
    {
        public string? UserId { get; set; }
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
       

    }
}
