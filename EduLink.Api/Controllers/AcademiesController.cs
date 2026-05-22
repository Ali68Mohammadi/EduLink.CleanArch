using EduLink.Application.Academies.Commands.CreateAcademy;
using EduLink.Application.Academies.Commands.DeleteAcademy;
using EduLink.Application.Academies.Commands.UpdateAcademy;
using EduLink.Application.Academies.Dtos;
using EduLink.Application.Academies.Queries.GetAcademyById;
using EduLink.Application.Academies.Queries.GetAllAcademies;
using EduLink.Domain.Constants;
using EduLink.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AcademiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = PlicyNames.AtLeast2Academies)]
    public async Task<IActionResult> GetAllAcademies()
    {
        var academies = await mediator.Send(new GetAllAcademiesQuery());
        return Ok(academies);
    }


    [HttpGet("{id}")]
    [Authorize(Policy = PlicyNames.HasNationality)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        AcademyDto academy = await mediator.Send(new GetAcademyByIdQuery(id));

        return Ok(academy);
    }


    [HttpPost]
    [Authorize(Roles = UserRoles.Manager)]
    public async Task<IActionResult> CreateAcademy([FromBody] CreateAcademyCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAcademy([FromRoute] int id)
    {
        await mediator.Send(new DeleteAcademyCommand(id));

        return NoContent();

    }


    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAcademy([FromRoute] int id, [FromBody] UpdateAcademyCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();

    }

}
