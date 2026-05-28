using EduLink.Application.Academies.Dtos;
using MediatR;

namespace EduLink.Application.Academies.Queries.GetAcademyById;

public class GetAcademyByIdQuery(int id) : IRequest<AcademyDto>
{
    public int Id { get; } = id;
}
