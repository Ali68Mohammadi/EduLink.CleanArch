using EduLink.Application.Users.Commands.AssignUserRole;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Users.Commands.UnassignUserRole;

public class UnassignUserRoleCommandHandler
    (ILogger<UnassignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<UnassignUserRoleCommand>
{
    public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unassign User Role : {@Request} ", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
             ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, request.RoleName);

    }
}