using EduLink.Application.Users;
using EduLink.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace EduLink.Infrastructure.Authorization.Requirements.CreateMinimumAcademiesRequirement;

internal class CreateMinimumAcademiesRequirementHandler(
    IAcademiesRepository academiesRepository ,IUserContext userContext
    ) : AuthorizationHandler<CreateMinimumAcademiesRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CreateMinimumAcademiesRequirement requirement)
    {
        var currentUser = userContext.GetCurrenUser();

        var academies = await academiesRepository.GetAllAsync();
        var userAcademiesCount = academies.Count(a => a.ManagerId == currentUser!.Id);

        if (userAcademiesCount >= requirement.MinimumAcademyCreated)
        {
            context.Succeed(requirement);
        }
        else
        {       
            context.Fail();
        }

    }
}
