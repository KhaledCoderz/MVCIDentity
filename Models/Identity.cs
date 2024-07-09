using Microsoft.AspNetCore.Identity;

namespace MVCIDentity.Models
{
    public class Identity : IdentityUser
    {
        public int FirstName { get; set; }
        public int LastName { get; set; }
    }
}
