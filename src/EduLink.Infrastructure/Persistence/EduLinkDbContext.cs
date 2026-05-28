
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduLink.Domain.Entities.Persistence;

internal class EduLinkDbContext(DbContextOptions<EduLinkDbContext> options) :
    IdentityDbContext<User>(options)
{
    internal DbSet<Academy> Academies { get; set; }
    internal DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<User>()
          .HasMany(m => m.ManagedAcademies)
          .WithOne(a=>a.Manager)
          .HasForeignKey(a => a.ManagerId);

    }




}

