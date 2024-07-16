using System.ComponentModel.DataAnnotations;

namespace MVCIDentity.Models.Entity
{
    public class Car
    {
        [Key]
        public long Id { get; set; }    
        public string Type { get; set; }
        public string UserId { get; set; }
        public Identity Identity { get; set; }
    }
}
