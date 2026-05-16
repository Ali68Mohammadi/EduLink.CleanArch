using EduLink.Domain.Entities;

namespace EduLink.Domain.Repositories;

public interface IAcademiesRepository
{
    Task<IEnumerable<Academy>> GetAllAsync();
    Task<Academy?> GetByIdAsync(int id);
    Task<int> Create(Academy entity);
    Task Delete(Academy entity);
    Task SaveChangesAsync();

}
