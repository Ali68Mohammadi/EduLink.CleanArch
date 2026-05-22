using EduLink.Application.Users.Commands.AssignUserRole;
using EduLink.Application.Users.Commands.UnassignUserRole;
using EduLink.Application.Users.Commands.UpdateUserDetails;
using EduLink.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.Api.Controllers;

[Route("api/identity")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpadteUserDetails(UpdateUserDetailsCommand command)
    {

        await mediator.Send(command);
        return NoContent();

    }


    [HttpPost("userRole")]
    [Authorize(Roles =UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {

        await mediator.Send(command);
        return NoContent();

    }

    [HttpDelete("userRole")]
    [Authorize(Roles =UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
    {

        await mediator.Send(command);
        return NoContent();

    }
}
