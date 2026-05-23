using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<IEnumerable<Academy>> GetByManagerIdAsync(string managerId)
    {
        var academies = await context.Academies.Where(t => t.ManagerId == managerId).ToListAsync();
        return academies;
    }

    public async Task<(IEnumerable<Academy>, int)> GetAllMatchingAsync(string? SearchPhrase,
        int pageNumber, int pageSize, string? sortBy, SortDirectionEnm sortDirection)
    {
        string? searchPhraseLower = SearchPhrase?.ToLower();

        var baseQuery = context.Academies
            .Where(a => searchPhraseLower == null ||
                        a.Name.ToLower().Contains(searchPhraseLower) ||
                        a.Description.ToLower().Contains(searchPhraseLower));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Academy, object>>>
            {
                { nameof(Academy.Name) ,a=>a.Name},
                { nameof(Academy.Description), a=>a.Description },
                { nameof(Academy.Category), a => a.Category },
            };

            var selectedColumn = columnSelector[sortBy];

            baseQuery = sortDirection == SortDirectionEnm.Ascending ?
                baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        var academies = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (academies, totalCount);
    }
}
