using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Admin.Commands.DeActiveUserById;

public class DeActiveUserByIdCommandHandler(IUserRepository userRepository,
     ILogger<DeActiveUserByIdCommandHandler> logger
    ) : IRequestHandler<DeActiveUserByIdCommand>
{
    public async Task Handle(DeActiveUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id)
                             ?? throw new NotFoundException(nameof(User), request.Id.ToString());

        user.IsActive = false;
        await userRepository.SaveChangesAsync();

        logger.LogInformation("Deactivated user with ID {UserId}", user.Id);

    }
}
