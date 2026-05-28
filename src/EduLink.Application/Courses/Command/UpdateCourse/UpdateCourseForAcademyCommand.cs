using MediatR;

namespace EduLink.Application.Courses.Command.UpdateCourse;

public class UpdateCourseForAcademyCommand :IRequest
{
    public int AcademyId { get; set; } 
    public int CourseId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}
