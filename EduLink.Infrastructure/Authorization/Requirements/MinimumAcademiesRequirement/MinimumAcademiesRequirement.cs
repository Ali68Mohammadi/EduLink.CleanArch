using Microsoft.AspNetCore.Authorization;

namespace EduLink.Infrastructure.Authorization.Requirements.MinimumAcademiesRequirement;

public class MinimumAcademiesRequirement(int minimumAcademyCreated) : IAuthorizationRequirement
{
    public int MinimumAcademyCreated { get; } = minimumAcademyCreated;
}
