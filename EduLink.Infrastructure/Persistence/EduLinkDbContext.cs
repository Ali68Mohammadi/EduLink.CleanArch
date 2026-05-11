
using Microsoft.EntityFrameworkCore;

namespace EduLink.Domain.Entities.Persistence;

internal class EduLinkDbContext(DbContextOptions<EduLinkDbContext> options) : DbContext(options)
{
    internal DbSet<Academy> Academies { get; set; }
    internal DbSet<Course> Courses { get; set; }
    internal DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            // Enforce unique constraint on Email to prevent duplicate accounts
            entity.HasIndex(u => u.Email)
                .IsUnique();

            // Enforce unique constraint on PhoneNumber for data integrity
            entity.HasIndex(u => u.PhoneNumber)
                .IsUnique();
        });

        // Academy entity configuration
        modelBuilder.Entity<Academy>(entity =>
        {
            // Configure Address as an Owned Entity (Value Object pattern)
            entity.OwnsOne(a => a.Address);

            // Define One-to-Many relationship between Academy and Courses
            entity.HasMany(a => a.Courses)
                .WithOne()
                .HasForeignKey(c => c.AcademyId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Delete courses if academy is deleted
        });
    }




}

