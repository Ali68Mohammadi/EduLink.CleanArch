using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Courses.Command.DeleteCourses;

public class DeleteCoursesForAcademyCommandHandler(ILogger<DeleteCoursesForAcademyCommandHandler> logger,
    IAcademiesRepository academiesRepository,
    ICoursesRepository coursesRepository)
    : IRequestHandler<DeleteCoursesForAcademyCommand>
{
    public async Task Handle(DeleteCoursesForAcademyCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Delete all Courses For Academy :{AcademyId}", request.AcademyId);

        var academy = await academiesRepository.GetByIdAsync(request.AcademyId);
        if (academy == null) throw new NotFoundException(nameof(academy), request.AcademyId.ToString());

        await coursesRepository.DeleteCourses(academy.Courses);

    }

}
