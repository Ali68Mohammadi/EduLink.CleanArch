using AutoMapper;
using EduLink.Domain.Entities;

namespace EduLink.Application.Courses.Dtos
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap() ;
        }
    }
}
