using Microsoft.EntityFrameworkCore;
using MVCIDentity.Models;
using MVCIDentity.Models.Entity;

namespace MVCIDentity.Data
{
    public class IdentityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<Identity>()
                .HasMany(a => a.Cars)
                .WithOne(a => a.Identity)
                .HasForeignKey(a => a.UserId);
        }
    }
}
