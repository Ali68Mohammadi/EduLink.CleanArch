using AutoMapper;
using EduLink.Application.Academies.Commands.CreateAcademy;
using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Interfaces;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.UpdateAcademy;

public class UpdateAcademyCommandHandler(IAcademiesRepository academiesRepository,
    ILogger<UpdateAcademyCommandHandler> logger,
    IMapper mapper,
    IAcademyAuthorizationService academyAuthorizationService) : IRequestHandler<UpdateAcademyCommand>
{
    public async Task Handle(UpdateAcademyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Academy with id: {AcademyId} with {@UpdatedAcademy}", request.Id, request);
        var academy = await academiesRepository.GetByIdAsync(request.Id);
        if (academy is null)
            throw new NotFoundException(nameof(Academy), request.Id.ToString());

        if (!academyAuthorizationService.Authorize(academy, ResourceOperationEnm.Update))
            throw new ForbidExeption();

        mapper.Map(request, academy);
        await academiesRepository.SaveChangesAsync();

        await academiesRepository.SaveChangesAsync();
    }
}
