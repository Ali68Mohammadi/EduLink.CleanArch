using EduLink.Application.Courses.Dtos;
using MediatR;

namespace EduLink.Application.Courses.Queries.GetCourseByIdForAcademy;

public class GetCourseByIdForAcademyQuery(int academyId,int courseId) : IRequest<GetCourseDto>
{
    public int CourseId { get; set; } = courseId;
    public int AcademyId { get; set; } = academyId;
}
