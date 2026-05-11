using EduLink.Domain.Entities;

namespace EduLink.Domain.Repositories;

public interface IAuthRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task SaveChangesAsync();
}
