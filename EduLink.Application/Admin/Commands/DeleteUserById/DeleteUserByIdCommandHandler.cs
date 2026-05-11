using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Admin.Commands.DeleteUserById;

public class DeleteUserByIdCommandHandler(IUserRepository userRepository,
    ILogger<DeleteUserByIdCommandHandler> logger)
    : IRequestHandler<DeleteUserByIdCommand> // نیازی به نوشتنِ bool در اینجا نیست
{
    public async Task Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id)
                   ?? throw new NotFoundException(nameof(User), request.Id.ToString());
        ;

        await userRepository.Delete(user);
        
        logger.LogInformation("Deleted user with ID: {userId}", user.Id);
        
    }
}