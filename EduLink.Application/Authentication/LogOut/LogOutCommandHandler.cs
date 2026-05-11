using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Authentication.LogOut;

public class LogOutCommandHandler(IAuthRepository authRepository,
    IUserRepository userRepository,
    ILogger<LogOutCommandHandler> logger)
    : IRequestHandler<LogOutCommand, bool>
{
    public async Task<bool> Handle(LogOutCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId)
            ?? throw new NotFoundException(nameof(Academy));
        
        user.RefreshTokenExpires = null;
        user.RefreshToken = null;
        await authRepository.SaveChangesAsync();
        logger.LogInformation("user Logout {userId}", user.Id);
        return true;
    }
}
