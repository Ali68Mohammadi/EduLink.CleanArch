using MediatR;

namespace EduLink.Application.Courses.Command.DeleteCourses;

public class DeleteCoursesForAcademyCommand(int academyId) : IRequest
{
    public int AcademyId { get; } = academyId;
}
