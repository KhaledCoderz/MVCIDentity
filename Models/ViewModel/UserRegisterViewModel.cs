using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCIDentity.Models.ViewModel
{
    public class UserRegisterViewModel
    {
        [Required]
        [Description("Username")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Description("Email")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [Description("Password")]
        public string Password { get; set; }

        [Required]
        [PasswordPropertyText]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
