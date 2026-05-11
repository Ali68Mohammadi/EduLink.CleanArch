using EduLink.Application.Courses.Dtos;
using MediatR;

namespace EduLink.Application.Academies.Commands.UpdateAcademy;

public class UpdateAcademyCommand : IRequest
{
    public int Id { get; set; } 
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool IsOnline { get; set; }

}
