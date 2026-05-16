using EduLink.Domain.Entities;

namespace EduLink.Domain.Repositories;

public interface ICoursesRepository
{
    Task<List<Course>> GetAllByAcademyIdAsync(int academyId);
    Task<int> Create(Course course);
    Task<Course> GetCourseByIdAsync(int id);
    Task<Course> GetCourseByAcademyIdAsync(int AcademyId, int id);
    Task DeleteCourses (IEnumerable<Course> entities);
    Task SaveChangesAsync();
    Task UpdateAsync(Course course, int id);
}
