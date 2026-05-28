using AutoMapper;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Courses.Command.CreateCourse;

public class CreateCourseCommandHandler(IAcademiesRepository academiesRepository,
    ICoursesRepository coursesRepository,
    ILogger<CreateCourseCommand> logger,
    IMapper mapper) : IRequestHandler<CreateCourseCommand,int>


{
    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
     {

        logger.LogInformation("creating new Course : {@CourseRequest}", request);

        var academy = await academiesRepository.GetByIdAsync(request.AcademyId)
            ?? throw new NotFoundException(nameof(Academy), request.AcademyId.ToString());

        Course course = mapper.Map<Course>(request);
        return await coursesRepository.Create(course);
  
    }

}