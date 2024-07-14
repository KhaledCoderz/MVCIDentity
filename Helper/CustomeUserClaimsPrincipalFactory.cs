using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVCIDentity.Models;
using System.Security.Claims;

namespace MVCIDentity.Helper
{
    public class CustomeUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Identity>
    {
        public CustomeUserClaimsPrincipalFactory(UserManager<Identity> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Identity user)
        {
            var identity = await base.GenerateClaimsAsync(user);


            identity.AddClaim(new Claim("FirstName", user.UserName));
            identity.AddClaim(new Claim("LastName", user.LastName));
            identity.AddClaim(new Claim("Address", user.Address));


            return identity;
        }
    }
}
