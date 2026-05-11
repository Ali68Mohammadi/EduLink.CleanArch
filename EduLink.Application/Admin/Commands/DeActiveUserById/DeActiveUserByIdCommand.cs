using MediatR;

namespace EduLink.Application.Admin.Commands.DeActiveUserById;

public class DeActiveUserByIdCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
