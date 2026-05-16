using AutoMapper;
using EduLink.Application.Courses.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Courses.Queries.GetCoursesForAcademy;

internal class GetCoursesForAcademyQueryHandler(
    IAcademiesRepository academiesRepository,
    ILogger<GetCoursesForAcademyQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetCoursesForAcademyQuery, IEnumerable<GetCourseDto>>
{
    public async Task<IEnumerable<GetCourseDto>> Handle(GetCoursesForAcademyQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving courses for academy with id : {AcademyId}", request.AcademyId);

        var academy = await academiesRepository.GetByIdAsync(request.AcademyId);

        if (academy == null ) 
            throw new NotFoundException(nameof(Academy), request.AcademyId.ToString()); 

        var resault= mapper.Map<IEnumerable<GetCourseDto>>(academy.Courses);

        return resault;
    }
}
