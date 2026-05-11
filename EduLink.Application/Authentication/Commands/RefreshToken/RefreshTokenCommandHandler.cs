using EduLink.Application.Abstractions;
using EduLink.Application.Authentication.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EduLink.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IAuthRepository authRepository,
        IConfiguration configuration, IJwtProvider jwtProvider)
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResult>
{
    public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await authRepository.GetByRefreshTokenAsync(request.RefreshToken)
                ?? throw new NotFoundException(nameof(User));


        if (!user.IsActive)
        {
            throw new BadRequestException("Your account is deactivated. Please contact support.");
        }

        user.RefreshTokenExpires = DateTime.UtcNow.AddDays(int.Parse(configuration["Jwt:RefreshTokenDurationInDays"]));
        user.RefreshToken = Guid.NewGuid().ToString();
        await authRepository.SaveChangesAsync();

        return new RefreshTokenResult
        {
            RefreshToken = user.RefreshToken,
            AccessToken = jwtProvider.Generate(user),
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:DurationInMinutes"])),
        };



    }
}
