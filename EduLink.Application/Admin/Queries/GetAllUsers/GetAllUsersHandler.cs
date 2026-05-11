using AutoMapper;
using EduLink.Application.Users.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EduLink.Application.Admin.Queries.GetAllUsers;

public class GetAllUsersHandler(IUserRepository userRepository, IMapper mapper,ILogger<GetAllUsersHandler> logger)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get ALl Users");
        IEnumerable<User>? users = await userRepository.GetAllAsync();
        var UserDtoList = mapper.Map<List<UserDto>>(users);
        return UserDtoList;

    }
}
