using EduLink.Application.Courses.Dtos;
using MediatR;

namespace EduLink.Application.Courses.Queries.GetCoursesForAcademy;

public class GetCoursesForAcademyQuery(int academyId) : IRequest<IEnumerable<GetCourseDto>>
{
    public int AcademyId { get; } = academyId;
}
