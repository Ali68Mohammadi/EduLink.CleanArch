using AutoMapper;
using EduLink.Application.Users.Dtos;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using MediatR;

namespace EduLink.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository,IMapper mapper) : 
    
    IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.Id);
        return user == null ? null : mapper.Map<UserDto>(user);
    }
}
