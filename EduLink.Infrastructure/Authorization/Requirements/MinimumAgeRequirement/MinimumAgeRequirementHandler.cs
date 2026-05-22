using EduLink.Application.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace EduLink.Infrastructure.Authorization.Requirements.MinimumAgeRequirement;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrenUser();

        logger.LogInformation("user:{Email}, Date of birth {DOB} -  handling MinimumAgeRequirement ",
            currentUser.Email,
            currentUser.DateOfBirth);

        if (currentUser.DateOfBirth == null)
        {
            logger.LogWarning("user date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
