using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EduLink.Infrastructure.Repositories;

internal class AuthRepository(EduLinkDbContext context) : IAuthRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        var normalizedEmail = email.ToLower();

        return await context.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == normalizedEmail);
    }

    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        var user = await context.Users.Include(u => u.Roles)
        .FirstOrDefaultAsync(u => u.RefreshToken ==refreshToken);
        return user;
    }

    public async Task SaveChangesAsync()
    {
       await context.SaveChangesAsync();
    }
}

