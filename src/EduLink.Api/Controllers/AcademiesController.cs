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
using EduLink.Application.Academies.Commands.UploadAcademyLogo;
using EduLink.Application.Academies.Commands.UploadAcademyPhotos;

namespace EduLink.Api.Controllers;

[Route("api/academies")]
[ApiController]
[Authorize]
public class AcademiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    //[Authorize(Policy = PlicyNames.AtLeast2Academies)]
    public async Task<ActionResult<IEnumerable<AcademyDto>>> GetAllAcademies([FromQuery] GetAllAcademiesQuery query)
    {
        var academies = await mediator.Send(query);
        return Ok(academies);
    }


    [HttpGet("{id}")]
    //[Authorize(Policy = PlicyNames.HasNationality)]
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



    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var command = new UploadAcademyLogoCommand
        {
            AcademyId = id,
            FileName = $"{id}-{file.FileName}",
            File = stream
        };

        await mediator.Send(command);
        return NoContent();
    }



    [HttpPost("{id}/photos")]
    public async Task<IActionResult> UploadAcademyPhotos([FromRoute] int id, List<IFormFile> files)
    {

        var command = new UploadAcademyPhotosCommand()
        {
            AcademyId = id,
            Files = files
        };

        await mediator.Send(command);
        return NoContent();
    }
}
