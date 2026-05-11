using AutoMapper;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository userRepository,
    ILogger<UpdateUserCommandHandler> logger,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id)
                   ?? throw new NotFoundException(nameof(User), request.Id.ToString());


        mapper.Map(request, user);

        await userRepository.SaveChangesAsync();
        logger.LogInformation("Update user with ID {UserId}", user.Id);

    }
}
