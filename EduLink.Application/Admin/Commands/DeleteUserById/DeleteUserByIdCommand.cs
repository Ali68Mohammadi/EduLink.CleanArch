using MediatR;

namespace EduLink.Application.Admin.Commands.DeleteUserById;

public class DeleteUserByIdCommand(int id) :IRequest
{
    public int Id { get; } = id;
}
