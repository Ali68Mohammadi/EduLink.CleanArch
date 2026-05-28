using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler
    (ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("assign User Role : {@Request} ", request);

        var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

        var role = await roleManager.FindByNameAsync(request.RoleName)
             ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await userManager.AddToRoleAsync(user, role.Name!);

    }
}
