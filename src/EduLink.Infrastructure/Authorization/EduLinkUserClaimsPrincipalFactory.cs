using EduLink.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EduLink.Infrastructure.Authorization;

public class EduLinkUserClaimsPrincipalFactory(UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    /// <summary>
    /// Custom factory to inject custom user properties (like Nationality and Date of Birth) as claims upon user authentication.
    /// </summary>
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);

        if (user.Nationality != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
        }

        if (user.DateOfBirth != null)
        {
            id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }
}