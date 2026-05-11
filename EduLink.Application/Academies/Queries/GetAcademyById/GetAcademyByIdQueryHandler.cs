using AutoMapper;
using EduLink.Application.Academies.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Academies.Queries.GetAcademyById;

public class GetAcademyByIdQueryHandler(IAcademiesRepository academiesRepository,
    ILogger<GetAcademyByIdQueryHandler> logger,
    IMapper mapper) : IRequestHandler<GetAcademyByIdQuery, AcademyDto>
{
    public async Task<AcademyDto> Handle(GetAcademyByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Academy {AcademyId}", request.Id);
        var academy = await academiesRepository.GetByIdAsync(request.Id)
                  ?? throw new NotFoundException(nameof(Academy), request.Id.ToString());

        var academyDto = mapper.Map<AcademyDto>(academy);
        return academyDto!;

    }
}
