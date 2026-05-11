using EduLink.Application.Authentication.Dtos;
using MediatR;

namespace EduLink.Application.Authentication.Commands.LoginUser;

public class LoginUserCommand :IRequest<LoginResultDto>
{
    /// <summary>
    /// Email Or PhoneNumber
    /// </summary>
    public string Email { get; set; }
    public string Password { get; set; }
}
