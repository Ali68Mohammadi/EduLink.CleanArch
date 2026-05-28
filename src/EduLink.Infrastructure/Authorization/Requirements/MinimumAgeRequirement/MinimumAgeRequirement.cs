using Microsoft.AspNetCore.Authorization;

namespace EduLink.Infrastructure.Authorization.Requirements.MinimumAgeRequirement;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;
}
