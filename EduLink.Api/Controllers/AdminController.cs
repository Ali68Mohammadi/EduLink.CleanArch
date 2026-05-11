using EduLink.Application.Admin.Commands.ActiveUserById;
using EduLink.Application.Admin.Commands.DeActiveUserById;
using EduLink.Application.Admin.Commands.DeleteUserById;
using EduLink.Application.Admin.Queries.GetAllUsers;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        var userDtoList = await mediator.Send(new GetAllUsersQuery());
        return Ok(userDtoList);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] int id)
    {
        //checking in Handler id not exist Throw Exeption
        await mediator.Send(new DeleteUserByIdCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/activate")]
    public async Task<IActionResult> ActivationUser([FromRoute] int id)
    {
        //checking in Handler id not exist Throw Exeption
        await mediator.Send(new ActiveUserByIdCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/deactivate")]
    public async Task<IActionResult> DeActivationUser([FromRoute] int id)
    {
        //checking in Handler id not exist Throw Exeption
        await mediator.Send(new DeActiveUserByIdCommand(id));
        return NoContent();
    }
}
