using AutoMapper;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.CreateAcademy;

public class CreateAcademyCommandHandler(IAcademiesRepository academiesRepository,
    ILogger<CreateAcademyCommandHandler> logger,
    IMapper mapper) : IRequestHandler<CreateAcademyCommand, int>
{
    public async Task<int> Handle(CreateAcademyCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create a new  Academy {@Academy}", request);
        var academy = mapper.Map<Academy>(request);
        int id = await academiesRepository.Create(academy);
        return id;
    }
}
