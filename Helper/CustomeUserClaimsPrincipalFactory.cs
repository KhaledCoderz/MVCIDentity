using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVCIDentity.Models;
using System.Security.Claims;

namespace MVCIDentity.Helper
{
    public class CustomeUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Identity>
    {
        private readonly UserManager<Identity> _userManager;

        public CustomeUserClaimsPrincipalFactory(UserManager<Identity> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Identity user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var roles = (await _userManager.GetRolesAsync(user)).SingleOrDefault();

            identity.AddClaim(new Claim("FirstName", user.FirstName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("Address", user.Address));
            identity.AddClaim(new Claim( ClaimTypes.Role, roles));


            return identity;
        }
    }
}
