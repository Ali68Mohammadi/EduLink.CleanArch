using MediatR;

namespace EduLink.Application.Academies.Commands.DeleteAcademy;

public class DeleteAcademyCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
