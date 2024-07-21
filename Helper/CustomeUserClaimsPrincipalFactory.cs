using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCIDentity.Core.Extensions;
using MVCIDentity.Models;
using System.Security.Claims;

namespace MVCIDentity.Helper
{
    public class CustomeUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Identity>
    {
        private readonly UserManager<Identity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomeUserClaimsPrincipalFactory(UserManager<Identity> userManager, IOptions<IdentityOptions> optionsAccessor, RoleManager<IdentityRole> roleManager) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Identity user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var roles = (await _userManager.GetRolesAsync(user)).SingleOrDefault();



            identity.AddClaim(new Claim("FirstName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("Address", user.Address));

            if (roles.IsNotNullOrEmpty())
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roles));
            }
            else
            {  
                var userRole = await _roleManager.Roles.FirstOrDefaultAsync(x=>x.NormalizedName == "USER");
                var status =  await _userManager.AddToRoleAsync(user, userRole.Name);
                if (status.Succeeded)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, userRole.Name));
                }
            }


             return identity;
        }
    }
}
