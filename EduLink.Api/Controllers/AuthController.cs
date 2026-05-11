using EduLink.Api.Middlewares;
using EduLink.Application.Authentication.Commands.LoginUser;
using EduLink.Application.Authentication.Commands.RefreshToken;
using EduLink.Application.Authentication.Dtos;
using EduLink.Application.Authentication.LogOut;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduLink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var LoginResault = await mediator.Send(command);
        return Ok(LoginResault);
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var loginResult = await mediator.Send(new RefreshTokenCommand(request.RefreshToken ));
        return Ok(loginResult);
    }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        //get userid
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim))
            throw new NotFoundException(nameof(Domain.Entities.User));

        await mediator.Send(new LogOutCommand(int.Parse(userIdClaim)));

        return NoContent();

    }

    /////////////////todo :

    /////2 : Logout user
    //// 3 : forget password 
    //// 4 : reset pasword

}
