using EduLink.Application.Users;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduLink.Infrastructure.Authorization.Requirements.MinimumAcademiesRequirement;

public class MinimumAcademiesRequirementHandler(ILogger<MinimumAcademiesRequirementHandler> logger,
    IUserContext userContext, IAcademiesRepository academiesRepository
    ) : AuthorizationHandler<MinimumAcademiesRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAcademiesRequirement requirement)
    {
        var currentUser = userContext.GetCurrenUser();
        var academies = await academiesRepository.GetAllAsync();
        var userAcademiesCount = academies.Count(a => a.ManagerId == currentUser!.Id);
        logger.LogInformation("user :{Email}, count {Academies} ", currentUser!.Email, userAcademiesCount);

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
