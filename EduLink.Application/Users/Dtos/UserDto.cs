namespace EduLink.Application.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin { get; set; }

    public bool IsActive { get; set; }
}
