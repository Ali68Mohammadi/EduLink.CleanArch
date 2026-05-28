
namespace EduLink.Application.Courses.Dtos; 

public class GetCourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }

}
