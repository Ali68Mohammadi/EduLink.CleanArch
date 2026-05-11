using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Admin.Commands.ActiveUserById;

public class ActiveUserByIdCommandHandler(IUserRepository userRepository,
    ILogger<ActiveUserByIdCommandHandler> logger) : IRequestHandler<ActiveUserByIdCommand>
{
    public async Task Handle(ActiveUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(User), request.Id.ToString());


        user.IsActive = true;
        await userRepository.SaveChangesAsync();
        logger.LogInformation("active user by userId {UserId}",user.Id);

    }
}
