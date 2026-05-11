using EduLink.Application.Users.Dtos;
using MediatR;

namespace EduLink.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery(int id) : IRequest<UserDto?>
{
    public int Id { get; } = id;
}
