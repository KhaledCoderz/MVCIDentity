using Microsoft.AspNetCore.Identity;
using MVCIDentity.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace MVCIDentity.Models
{
    public class Identity : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [DataType("NVARCHAR(MAX)")]
        public string Address { get; set; } = string.Empty; 
        public ICollection<Car?> Cars { get; set; }
    }
}
