using AutoMapper;
using EduLink.Application.Users.Commands.CreateUser;
using EduLink.Application.Users.Commands.UpdateUser;
using EduLink.Domain.Entities;

namespace EduLink.Application.Users.Dtos;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => false));

        CreateMap<User, UserDto>();

        CreateMap<UpdateUserCommand, User>();
    }

}
