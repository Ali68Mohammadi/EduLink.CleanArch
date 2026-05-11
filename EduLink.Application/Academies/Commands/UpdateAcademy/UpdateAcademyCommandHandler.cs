using AutoMapper;
using EduLink.Application.Academies.Commands.CreateAcademy;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.UpdateAcademy;

public class UpdateAcademyCommandHandler(IAcademiesRepository academiesRepository,
    ILogger<CreateAcademyCommandHandler> logger,
    IMapper mapper) : IRequestHandler<UpdateAcademyCommand>
{
    public async Task Handle(UpdateAcademyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Academy with id: {AcademyId} with {@UpdatedAcademy}", request.Id, request);
        var academy = await academiesRepository.GetByIdAsync(request.Id);
        if (academy is null)
            throw new NotFoundException(nameof(Academy), request.Id.ToString());


        mapper.Map(request, academy);
        await academiesRepository.SaveChangesAsync();

    }
}
