using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EduLink.Infrastructure.Repositories;

internal class CoursesRepository(EduLinkDbContext context) : ICoursesRepository
{

    public async Task<List<Course>> GetAllByAcademyIdAsync(int academyId)
    {
        return await context.Courses.AsNoTracking().Where(c => c.AcademyId == academyId).ToListAsync();
    }


    public async Task<int> Create(Course entity)
    {
        await context.Courses.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Course?> GetCourseByAcademyIdAsync(int academyId, int id)
    {
        return await context.Courses
            .FirstOrDefaultAsync(c => c.AcademyId == academyId && c.Id == id);
    }

    public async Task DeleteCourses(IEnumerable<Course> entities)
    {
        context.Courses.RemoveRange(entities);
        await context.SaveChangesAsync();
    }


    public async Task UpdateAsync(Course course, int courseId)
    {
        var entity = await context.Courses.FindAsync(courseId);
        if (course != null)
        {
            entity.Name = course.Name;
            entity.Description = course.Description;
            entity.Price = course.Price;
            await context.SaveChangesAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
