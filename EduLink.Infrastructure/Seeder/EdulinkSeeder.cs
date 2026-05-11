using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Entities;

namespace EduLink.Infrastructure.Seeder;

internal class EdulinkSeeder(EduLinkDbContext context) : IEdulinkSeeder
{
    public async Task SeedAsync()
    {
        if (await context.Database.CanConnectAsync())
        {
            if (!context.Academies.Any())
            {
                var academies = GetAcademies();
                await context.AddRangeAsync(academies);
                await context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Academy> GetAcademies()
    {
        List<Academy> academies = [
             new()
        {
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
