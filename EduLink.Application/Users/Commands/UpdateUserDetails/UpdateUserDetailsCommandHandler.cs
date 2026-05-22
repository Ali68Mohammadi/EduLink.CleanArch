using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Users.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler(
    ILogger<UpdateUserDetailsCommandHandler> logger,
    IUserStore<User> userStore,
    IUserContext userContext)
    : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrenUser();

        logger.LogInformation("upadting user :{UserId} , with {@Request}", user!.Id, request);

        var dbUser = await userStore.FindByIdAsync(user.Id, cancellationToken);

        if (dbUser == null) throw new NotFoundException(nameof(User), user!.Id);

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);

    }
}
