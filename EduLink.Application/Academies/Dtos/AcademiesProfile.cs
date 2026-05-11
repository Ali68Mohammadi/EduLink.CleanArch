using AutoMapper;
using EduLink.Application.Academies.Commands.CreateAcademy;
using EduLink.Application.Academies.Commands.UpdateAcademy;
using EduLink.Domain.Entities;

namespace EduLink.Application.Academies.Dtos;

public class AcademiesProfile : Profile
{
    public AcademiesProfile()
    {
        CreateMap<UpdateAcademyCommand, Academy>();

        CreateMap<CreateAcademyCommand, Academy>()
            .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));


        CreateMap<Academy, AcademyDto>()
           .ForMember(d => d.City, opt =>
                 opt.MapFrom(src => src.Address == null ? null : src.Address.City))
           .ForMember(d => d.Street, opt =>
                 opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
           .ForMember(d => d.PostalCode, opt =>
                 opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
           .ForMember(d => d.Courses, opt => opt.MapFrom(src => src.Courses));




    }
}
