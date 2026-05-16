using MediatR;

namespace EduLink.Application.Academies.Commands.CreateAcademy;

public class CreateAcademyCommand:IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Descriptaion { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool IsOnline { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactNumber { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }

}
