using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCIDentity.Models.ViewModel
{
    public class UserLoginViewModel
    {
        [Required]
        [Description("Username")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [PasswordPropertyText]
        [Description("Password")]
        public string Password { get; set; }

    }
}
