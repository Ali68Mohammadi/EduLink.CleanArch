using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduLink.Infrastructure.Seeder;

internal class EdulinkSeeder(EduLinkDbContext context) : IEdulinkSeeder
{
    public async Task SeedAsync()
    {
        try
        {
            Console.WriteLine("Seeder Started");

            if (context.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Running Migrations");
                await context.Database.MigrateAsync();
                Console.WriteLine("Migrations Done");
            }

            if (await context.Database.CanConnectAsync())
            {
                Console.WriteLine("Database Connected");

                if (!await context.Academies.AnyAsync())
                {
                    Console.WriteLine("Creating Academies");

                    var academies = GetAcademies();

                    await context.AddRangeAsync(academies);

                    await context.SaveChangesAsync();

                    Console.WriteLine("Academies Created");
                }
            }

            if (!await context.Roles.AnyAsync())
            {
                Console.WriteLine("Creating Roles");

                var roles = GetRoles();

                await context.Roles.AddRangeAsync(roles);

                await context.SaveChangesAsync();

                Console.WriteLine("Roles Created");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new(UserRoles.User)
                {
                    NormalizedName=UserRoles.User.ToUpper()
                }, 
                new(UserRoles.Manager)
                {
                    NormalizedName=UserRoles.Manager.ToUpper()
                },
                new(UserRoles.Admin)
                {
                    NormalizedName=UserRoles.Admin.ToUpper()
                },

            ];
        return roles;
    }

    private IEnumerable<Academy> GetAcademies()
    {
        User manager = new()
        {
            Email="seeder@test.com"
        };
        List<Academy> academies = [
             new()
        {
                 Manager=manager,
            Name = "expert",
            Category = "Software",
            Description = "....",
            ContactEmail = "sedeerEmail@Gmail.com",
            IsOnline = true,
            Courses = [
                new Course
                {
                    Name="C#",
                    Description="C# 14 ",
                    Price=256
                },
                new Course
                {
                    Name="F#",
                    Description="C# 10 ",
                    Price=145
                }
            ],
            Address =
            new()
            {
                City = "tehran",
                PostalCode = "79156",
                Street = "Azadi",
            }
        } ,
            new()
        {
                Manager= manager,
            Name = "danesh",
            Category = "Software",
            Description = "....",
            ContactEmail = "sedeerEmail@Gmail.com",
            IsOnline = true,
            Courses = [
                new Course
                {
                    Name="java",
                    Description="java 14 ",
                    Price=139
                },
                new Course
                {
                    Name=".net",
                    Description=".net 8 ",
                    Price=421
                }
            ],
            Address =
            new()
            {
                City = "Sari",
                PostalCode = "12345",
                Street = "esteghlal",
            }
        }
        ];
        return academies;

    }
}
