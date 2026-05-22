using Microsoft.AspNetCore.Identity;

namespace EduLink.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    public IEnumerable<Academy> ManagedAcademies { get; set; } = [];
}
