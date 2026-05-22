using AutoMapper;
using EduLink.Application.Users;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Commands.CreateAcademy;

public class CreateAcademyCommandHandler(IAcademiesRepository academiesRepository,
    ILogger<CreateAcademyCommandHandler> logger,
    IUserContext userContext, IMapper mapper) : IRequestHandler<CreateAcademyCommand, int>
{
    public async Task<int> Handle(CreateAcademyCommand request, CancellationToken cancellationToken)
    {
        var currenUser = userContext.GetCurrenUser();

        logger.LogInformation("{UserEmeil} [{userId}] is Creating a new  Academy {@Academy}",
            currenUser.Email,
            currenUser.Id,
            request);

        var academy = mapper.Map<Academy>(request);

        academy.ManagerId = currenUser!.Id;
        int id = await academiesRepository.Create(academy);
        return id;
    }
}
