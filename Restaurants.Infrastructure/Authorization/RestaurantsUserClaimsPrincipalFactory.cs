using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantsUserClaimsPrincipalFactory(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<UserEntity, IdentityRole>(userManager, roleManager, options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(UserEntity user)
        {
            var id = await GenerateClaimsAsync(user);
            if (user.DateofBirth != DateOnly.MinValue)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateofBirth.ToString("yyyy-MM-dd")));
            }
            if (user.Nationality != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality.ToString()));
            }
            return new ClaimsPrincipal(id);
        }
    }
}
