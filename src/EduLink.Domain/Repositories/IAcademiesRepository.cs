using EduLink.Domain.Constants;
using EduLink.Domain.Entities;

namespace EduLink.Domain.Repositories;

public interface IAcademiesRepository
{
    Task<IEnumerable<Academy>> GetAllAsync();
    Task<Academy?> GetByIdAsync(int id);
    Task<int> Create(Academy entity);
    Task Delete(Academy entity);
    Task SaveChangesAsync();
    Task<IEnumerable<Academy>> GetByManagerIdAsync(string managerId);
    Task<(IEnumerable<Academy>, int)> GetAllMatchingAsync(string SearchPhrase,
        int pageNumber, int pageSize, string? sortBy, SortDirectionEnm sortDirection);
    Task AddPhotoUrlAsync(int academyId, string photoUrl);
}
