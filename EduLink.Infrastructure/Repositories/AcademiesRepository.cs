using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EduLink.Infrastructure.Repositories;

internal class AcademiesRepository(EduLinkDbContext context) : IAcademiesRepository
{
    public async Task<IEnumerable<Academy>> GetAllAsync()
    {
        var academies = await context.Academies.Include(t => t.Courses).ToListAsync();
        return academies;
    }

    public async Task<Academy?> GetByIdAsync(int id)
    {
        Academy? academy = await context.Academies
            .Include(a => a.Courses)
            .FirstOrDefaultAsync(a => a.Id == id);
        return academy!;
    }

    public async Task<int> Create(Academy entity)
    {
        await context.Academies.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Academy entity)
    {
        context.Academies.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();

    }
}
