namespace EduLink.Application.Courses.Dtos
{
    public class CreateCourseDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }

        public int AcademyId { get; set; }

    }
}
