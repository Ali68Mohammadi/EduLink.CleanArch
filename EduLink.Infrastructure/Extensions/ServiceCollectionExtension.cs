using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Repositories;
using EduLink.Infrastructure.Repositories;
using EduLink.Infrastructure.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduLink.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EduLinkDb");
            services.AddDbContext<EduLinkDbContext>(option => option.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());


            services.AddScoped<IEdulinkSeeder, EdulinkSeeder>();
            services.AddScoped<IAcademiesRepository, AcademiesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}
