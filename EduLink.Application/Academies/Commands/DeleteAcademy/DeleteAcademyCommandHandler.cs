using AutoMapper;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.DeleteAcademy;

public class DeleteAcademyCommandHandler(IAcademiesRepository academiesRepository,
    ILogger<DeleteAcademyCommandHandler> logger) : IRequestHandler<DeleteAcademyCommand>
{
    public async Task Handle(DeleteAcademyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Academy with id :{request.Id} ", request.Id);
        Academy? academy = await academiesRepository.GetByIdAsync(request.Id);
        if (academy is null)
            throw new NotFoundException(nameof(Academy), request.Id.ToString());

        await academiesRepository.Delete(academy);

    }
}
