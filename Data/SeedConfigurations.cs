using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MVCIDentity.Data
{
    public class SeedConfigurations
    {
        #region Seeding

        public void Seed(ModelBuilder builder)
        {
            SeedRoles(builder);
        }


        public void SeedRoles(ModelBuilder builder)
        {

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
                );

        }
        #endregion
    }
}
