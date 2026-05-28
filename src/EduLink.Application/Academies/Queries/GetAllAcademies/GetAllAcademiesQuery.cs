using EduLink.Application.Academies.Dtos;
using EduLink.Application.Common;
using EduLink.Domain.Constants;
using MediatR;

namespace EduLink.Application.Academies.Queries.GetAllAcademies;

public class GetAllAcademiesQuery : IRequest<PagedResult<AcademyDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirectionEnm SortDirection { get; set; }

}
