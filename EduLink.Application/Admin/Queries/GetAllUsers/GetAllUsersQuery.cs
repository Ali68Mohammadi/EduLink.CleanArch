using EduLink.Application.Users.Dtos;
using MediatR;

namespace EduLink.Application.Admin.Queries.GetAllUsers;

public class GetAllUsersQuery:IRequest<IEnumerable<UserDto>>
{
}
