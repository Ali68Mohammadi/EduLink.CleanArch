using AutoMapper;
using EduLink.Application.Courses.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Courses.Queries.GetCourseByIdForAcademy;

public class GetCourseByIdForAcademyQueryHandler(IAcademiesRepository academiesRepository,
    IMapper mapper,
    ILogger<GetCourseByIdForAcademyQueryHandler> logger)
    : IRequestHandler<GetCourseByIdForAcademyQuery, GetCourseDto>
{
    public async Task<GetCourseDto> Handle(GetCourseByIdForAcademyQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving course :{CourseId}, for academy with id : {AcademyId}"
            , request.CourseId
            , request.AcademyId);

        var academy = await academiesRepository.GetByIdAsync(request.AcademyId);
        if (academy == null) throw new NotFoundException(nameof(Academy), request.AcademyId.ToString());

        var course = academy.Courses.FirstOrDefault(c => c.Id == request.CourseId);

        if (course == null) throw new NotFoundException(nameof(course), request.CourseId.ToString());

        GetCourseDto getCourseDto = mapper.Map<GetCourseDto>(course);
        return getCourseDto;

    }


}
