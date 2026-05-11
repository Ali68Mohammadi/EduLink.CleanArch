using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduLink.Infrastructure.Repositories;

internal class UserRepository(EduLinkDbContext context) : IUserRepository
{
    public async Task<int> Create(User entity)
    {
        await context.Users.AddAsync(entity);
        await SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(User entity)
    {
        context.Users.Remove(entity);
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var Users = await context.Users
            .Include(u => u.Roles)
            .ToListAsync();
        return Users;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }



    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {

        User? user = await context.Users.Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        return user;
    }

    public async Task<bool> IsEmailExistAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email.ToLowerInvariant());
    }

    public async Task<bool> IsPhoneNumberExistAsync(string phoneNumber)
    {
        return await context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }




}
