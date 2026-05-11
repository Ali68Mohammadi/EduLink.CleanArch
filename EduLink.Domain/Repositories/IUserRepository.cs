using EduLink.Domain.Entities;

namespace EduLink.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<int> Create(User entity);
    Task Delete(User entity);
    Task SaveChangesAsync();

    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<bool> IsEmailExistAsync(string email);
    Task<bool> IsPhoneNumberExistAsync(string mobile);
}
