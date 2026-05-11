using EduLink.Application.Users.Commands.CreateUser;
using EduLink.Application.Users.Commands.UpdateUser;
using EduLink.Application.Users.Dtos;
using EduLink.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {

        UserDto? user = await mediator.Send(new GetUserByIdQuery(id));
        if (user is null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        int id = await mediator.Send(command);
        //send 201 rsponse code and user id
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }
}