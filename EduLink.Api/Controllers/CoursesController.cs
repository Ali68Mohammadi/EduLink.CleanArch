using EduLink.Application.Academies.Commands.UpdateAcademy;
using EduLink.Application.Courses.Command.CreateCourse;
using EduLink.Application.Courses.Command.DeleteCourses;
using EduLink.Application.Courses.Command.UpdateCourse;
using EduLink.Application.Courses.Queries.GetCourseByIdForAcademy;
using EduLink.Application.Courses.Queries.GetCoursesForAcademy;
using EduLink.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EduLink.Api.Controllers;

[Route("api/academies/{academyId}/courses")]
[ApiController]
[Authorize]
public class CoursesController(IMediator mediator) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> GetAllForAcademy([FromRoute] int academyId)
    {

        var courses = await mediator.Send(new GetCoursesForAcademyQuery(academyId));
        return Ok(courses);
    }


    [HttpGet("{courseId}")]
    [Authorize(Policy = PlicyNames.AtLeast20)]
    public async Task<ActionResult> GetByIdForAcademy([FromRoute] int academyId, [FromRoute] int courseId)
    {

        var course = await mediator.Send(new GetCourseByIdForAcademyQuery(academyId, courseId));
        return Ok(course);
    }


    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromRoute] int academyId, [FromBody] CreateCourseCommand command)
    {
        command.AcademyId = academyId;
        var courseId = await mediator.Send(command);

        return CreatedAtAction(nameof(GetByIdForAcademy), new { academyId, courseId }, null);
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCoursesForAcademy([FromRoute] int academyId)
    {
        await mediator.Send(new DeleteCoursesForAcademyCommand(academyId));
        return NoContent();
    }


    [HttpPatch("{courseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCourseForAcademy([FromRoute] int academyId, [FromRoute] int courseId,
        [FromBody] UpdateCourseForAcademyCommand command)
    {
        command.AcademyId = academyId;
        command.CourseId = courseId;
        await mediator.Send(command);

        return NoContent();

    }





}
