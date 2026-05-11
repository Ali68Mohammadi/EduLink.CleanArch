using AutoMapper;
using EduLink.Application.Academies.Dtos;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Queries.GetAllAcademies;

public class GetAllAcademiesQueryHnadler(IAcademiesRepository academiesRepository,
    ILogger<GetAllAcademiesQueryHnadler> logger,
    IMapper mapper) : IRequestHandler<GetAllAcademiesQuery, IEnumerable<AcademyDto>>
{
    public async Task<IEnumerable<AcademyDto>> Handle(GetAllAcademiesQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get All Academies");
        var Academies = await academiesRepository.GetAllAsync();
        var AcademoDtoList = mapper.Map<List<AcademyDto>>(Academies);
        return AcademoDtoList!;

    }
}
