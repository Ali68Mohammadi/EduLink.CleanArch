using AutoMapper;
using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Interfaces;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Courses.Command.UpdateCourse;

public class UpdateCourseForAcademyCommandHandler(IAcademiesRepository academiesRepository,
    ICoursesRepository coursesRepository,
    ILogger<UpdateCourseForAcademyCommandHandler> logger,
    IMapper mapper,IAcademyAuthorizationService academyAuthorizationService) : IRequestHandler<UpdateCourseForAcademyCommand>
{
    public async Task Handle(UpdateCourseForAcademyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating course with id: {CourseId} with {@UpdatedCourse}", request.CourseId, request);


        Academy? academy = await academiesRepository.GetByIdAsync(request.AcademyId);
        if (academy is null) throw new NotFoundException(nameof(Academy), request.AcademyId.ToString());

        Course? course = await coursesRepository.GetCourseByAcademyIdAsync(request.AcademyId, request.CourseId);
        if (course is null) throw new NotFoundException(nameof(Course), request.CourseId.ToString());

        if (!academyAuthorizationService.Authorize(academy, ResourceOperationEnm.Update))
            throw new ForbidExeption();

        mapper.Map(request, course);

        await coursesRepository.SaveChangesAsync();
    }

}
