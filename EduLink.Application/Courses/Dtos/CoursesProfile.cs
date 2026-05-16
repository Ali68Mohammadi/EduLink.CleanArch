using AutoMapper;
using EduLink.Application.Courses.Command.CreateCourse;
using EduLink.Application.Courses.Command.UpdateCourse;
using EduLink.Domain.Entities;

namespace EduLink.Application.Courses.Dtos
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course,GetCourseDto>();
            CreateMap<UpdateCourseForAcademyCommand, Course>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
