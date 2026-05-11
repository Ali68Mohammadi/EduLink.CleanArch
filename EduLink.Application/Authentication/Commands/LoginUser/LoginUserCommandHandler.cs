using EduLink.Application.Abstractions;
using EduLink.Application.Authentication.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Authentication.Commands.LoginUser;

public class LoginUserCommandHandler(IAuthRepository authRepository,
    ILogger<LoginUserCommandHandler> logger,
    IJwtProvider jwtProvider, IConfiguration configuration)
    : IRequestHandler<LoginUserCommand, LoginResultDto>
{
    public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var email = request.Email.ToLower().Trim();
        var user = await authRepository.GetByEmailAsync(email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            logger.LogWarning("Invalid login attempt for: {Email}", email);
            throw new NotFoundException(nameof(User));
        }

        if (!user.IsActive)
        {
            logger.LogWarning("Access denied for inactive account: {Email}", email);
            throw new BadRequestException("Your account is deactivated. Please contact support.");
        }

        var refreshToken = Guid.NewGuid().ToString();
        var refreshTokenDays = configuration.GetValue<int>("Jwt:RefreshTokenDurationInDays", 7);
        var accessTokenMinutes = configuration.GetValue<int>("Jwt:DurationInMinutes", 60);

        // 6. State Update
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(refreshTokenDays);
        user.LastLogin = DateTime.UtcNow;

        // 7. Persistence
        await authRepository.SaveChangesAsync();

        logger.LogInformation("User {Email} authenticated successfully.", user.Email);

        // 8. Result Mapping
        return new LoginResultDto
        {
            AccessToken = jwtProvider.Generate(user),
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(accessTokenMinutes),
            RefreshToken = refreshToken
        };
    }


}
