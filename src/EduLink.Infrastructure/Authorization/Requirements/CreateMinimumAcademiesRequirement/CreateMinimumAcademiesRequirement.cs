using Microsoft.AspNetCore.Authorization;

namespace EduLink.Infrastructure.Authorization.Requirements.CreateMinimumAcademiesRequirement;

public class CreateMinimumAcademiesRequirement(int minimumAcademyCreated) : IAuthorizationRequirement
{
    public int MinimumAcademyCreated { get; } = minimumAcademyCreated;
}
