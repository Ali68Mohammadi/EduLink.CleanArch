using EduLink.Application.Courses.Dtos;

namespace EduLink.Application.Academies.Dtos;

public class AcademyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool IsOnline { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? LogoSasUrl { get; set; }

    public List<CourseDto> Courses { get; set; } = [];

}

