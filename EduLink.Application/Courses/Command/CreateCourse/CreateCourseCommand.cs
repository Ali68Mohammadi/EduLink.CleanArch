using EduLink.Application.Courses.Dtos;
using MediatR;
using System.Text.Json.Serialization;

namespace EduLink.Application.Courses.Command.CreateCourse;

public class CreateCourseCommand : IRequest<int>
{

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int AcademyId { get; set; }

}
