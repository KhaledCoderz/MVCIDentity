using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCIDentity.Models;
using System.Reflection.Emit;

namespace MVCIDentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<Identity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new Configurations().Seed(builder);
        }

    }


}
