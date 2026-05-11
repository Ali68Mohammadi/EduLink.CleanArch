using EduLink.Application.Authentication.Dtos;
using MediatR;

namespace EduLink.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommand(string refreshToken) : IRequest<RefreshTokenResult>
{
    public string RefreshToken { get; } = refreshToken;
}
