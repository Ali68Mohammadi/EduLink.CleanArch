using EduLink.Domain.Entities;
using EduLink.Domain.Entities.Persistence;
using EduLink.Domain.Interfaces;
using EduLink.Domain.Repositories;
using EduLink.Infrastructure.Authorization;
using EduLink.Infrastructure.Authorization.Requirements.CreateMinimumAcademiesRequirement;
using EduLink.Infrastructure.Authorization.Requirements.MinimumAgeRequirement;
using EduLink.Infrastructure.Authorization.Services;
using EduLink.Infrastructure.Repositories;
using EduLink.Infrastructure.Seeder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<EduLinkUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<EduLinkDbContext>();

            services.AddScoped<IEdulinkSeeder, EdulinkSeeder>();

            services.AddScoped<IAcademiesRepository, AcademiesRepository>();

            services.AddScoped<ICoursesRepository, CoursesRepository>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PlicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "Deutsch", "Iran"))
                .AddPolicy(PlicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PlicyNames.AtLeast2Academies,
                builder => builder.AddRequirements(new CreateMinimumAcademiesRequirement(2)));


            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreateMinimumAcademiesRequirementHandler>();
            services.AddScoped<IAcademyAuthorizationService, AcademyAuthorizationService>();
            services.AddScoped<IProfileAuthorizationService, ProfileAuthorizationService>();
        }
    }
}
