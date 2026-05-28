using AutoMapper;
using EduLink.Application.Academies.Dtos;
using EduLink.Application.Common;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Queries.GetAllAcademies;

public class GetAllAcademiesQueryHnadler(IAcademiesRepository academiesRepository,
    ILogger<GetAllAcademiesQueryHnadler> logger,
    IMapper mapper) : IRequestHandler<GetAllAcademiesQuery, PagedResult<AcademyDto>>
{
    public async Task<PagedResult<AcademyDto>> Handle(GetAllAcademiesQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get All Academies");
        var (academies,totalCount) = await academiesRepository.GetAllMatchingAsync(
            request.SearchPhrase,
            request.PageNumber,
            request.PageSize,
            request.SortBy,
            request.SortDirection);

        var academyDtos = mapper.Map<List<AcademyDto>>(academies);
       
        var result = new PagedResult<AcademyDto>(academyDtos, totalCount, request.PageSize, request.PageNumber);
        return (result);

    }
}
