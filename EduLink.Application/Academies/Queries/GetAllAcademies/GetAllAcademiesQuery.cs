using EduLink.Application.Academies.Dtos;
using MediatR;

namespace EduLink.Application.Academies.Queries.GetAllAcademies;

public class GetAllAcademiesQuery: IRequest<IEnumerable<AcademyDto>>
{

}
