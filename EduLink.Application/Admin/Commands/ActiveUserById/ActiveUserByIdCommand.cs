using MediatR;

namespace EduLink.Application.Admin.Commands.ActiveUserById;
public class ActiveUserByIdCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
