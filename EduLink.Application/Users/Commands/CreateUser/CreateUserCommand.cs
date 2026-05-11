using MediatR;

namespace EduLink.Application.Users.Commands.CreateUser;

public class CreateUserCommand:IRequest<int>
{
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RePassword { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

}
